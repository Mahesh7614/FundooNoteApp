using FundooModel;
using FundooRepository.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.Security.Claims;
using System.Data.SqlClient;
using System.Text;
using System.IdentityModel.Tokens.Jwt;

namespace FundooRepository.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IConfiguration config;
        string connectionString;

        public UserRepository(IConfiguration configuration, IConfiguration config)
        {
            connectionString = configuration.GetConnectionString("UserDBConnection");
            this.config = config;
        }
        public static string EncryptPassword(string password)
        {
            byte[] encode = new byte[password.Length];
            encode = Encoding.UTF8.GetBytes(password);
            return Convert.ToBase64String(encode);
        }
        public string GenerateJWTToken(string emailID, int UserID)
        {
            try
            {
                var loginSecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(this.config[("Jwt:key")]));
                var loginTokenDescripter = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Email, emailID),
                        new Claim("UserID",UserID.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddHours(5),
                    SigningCredentials = new SigningCredentials(loginSecurityKey, SecurityAlgorithms.HmacSha256Signature)
                };
                var token = new JwtSecurityTokenHandler().CreateToken(loginTokenDescripter);
                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        public UserRegistrationModel Registration(UserRegistrationModel userRegistration)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                using (connection)
                {
                    SqlCommand command = new SqlCommand("SPRegister", connection);

                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@FirstName", userRegistration.FirstName);
                    command.Parameters.AddWithValue("@LastName", userRegistration.LastName);
                    command.Parameters.AddWithValue("@EmailID", userRegistration.EmailID);
                    command.Parameters.AddWithValue("@Password", EncryptPassword(userRegistration.Password));

                    connection.Open();
                    int registerOrNot = command.ExecuteNonQuery();

                    if (registerOrNot >= 1)
                    {
                        return userRegistration;
                    }
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }
        public string Login(UserLoginModel userLogin)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                UserRegistrationModel userRegistration = new UserRegistrationModel();
                using (connection)
                {
                    SqlCommand command = new SqlCommand("SPLogin", connection);

                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@EmailID", userLogin.EmailID);
                    command.Parameters.AddWithValue("@Password", EncryptPassword(userLogin.Password));

                    connection.Open();
                    SqlDataReader Reader = command.ExecuteReader();

                    if (Reader.HasRows)
                    {
                        while (Reader.Read())
                        {
                            userRegistration.UserID = Reader.IsDBNull("UserID") ? 0 : Reader.GetInt32("UserID");
                        }
                        var token = GenerateJWTToken(userLogin.EmailID, userRegistration.UserID);
                        return token;
                    }
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }
        public string ForgotPassword(string emailID)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                UserRegistrationModel userRegistration = new UserRegistrationModel();

                using (connection)
                {
                    SqlCommand command = new SqlCommand("SPForgot", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@EmailID", emailID);

                    connection.Open();
                    SqlDataReader Reader = command.ExecuteReader();

                    if (Reader.HasRows)
                    {
                        while (Reader.Read())
                        {
                            userRegistration.UserID = Reader.IsDBNull("UserID") ? 0 : Reader.GetInt32("UserID");
                            userRegistration.FirstName = Reader.IsDBNull("FirstName") ? string.Empty : Reader.GetString("FirstName");
                        }
                        var token = GenerateJWTToken(emailID, userRegistration.UserID);
                        MSMQModel mSMQModel = new MSMQModel();
                        mSMQModel.SendMessage(token, emailID, userRegistration.FirstName);
                        return token.ToString();
                    }
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }
        public bool ResetPassword(UserResetPasswordModel userResetPassword, string emailID)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                using (connection)
                {
                    SqlCommand command = new SqlCommand("SPResetPassword", connection);

                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@EmailID", emailID);
                    command.Parameters.AddWithValue("@Password", EncryptPassword(userResetPassword.Password));
                    connection.Open();
                    int resetOrNot = command.ExecuteNonQuery();

                    if (resetOrNot >= 1)
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
