// <copyright file="UserRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace FundooRepository.Repository
{
    using System.Data;
    using System.Data.SqlClient;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    using FundooModel;
    using FundooRepository.Interface;
    using Microsoft.Extensions.Configuration;
    using Microsoft.IdentityModel.Tokens;
    using StackExchange.Redis;

    /// <summary>
    /// UserRepository.
    /// </summary>
    public class UserRepository : IUserRepository
    {
        private readonly IConfiguration config;
        private readonly string? connectionString;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRepository"/> class.
        /// </summary>
        /// <param name="configuration">configuration.</param>
        /// <param name="config">config.</param>
        public UserRepository(IConfiguration configuration, IConfiguration config)
        {
            this.connectionString = configuration.GetConnectionString("UserDBConnection");
            this.config = config;
        }

        /// <summary>
        /// EncryptPassword.
        /// </summary>
        /// <param name="password">password.</param>
        /// <returns>string.</returns>
        public static string EncryptPassword(string password)
        {
            byte[] encode = new byte[password.Length];
            encode = Encoding.UTF8.GetBytes(password);
            return Convert.ToBase64String(encode);
        }

        /// <summary>
        /// GenerateJWTToken.
        /// </summary>
        /// <param name="emailID">emailID.</param>
        /// <param name="UserID">UserID.</param>
        /// <returns>string.</returns>
        public string GenerateJWTToken(string emailID, int UserID)
        {
            try
            {
#pragma warning disable CS8604 // Possible null reference argument.
                var loginSecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(this.config["Jwt:key"]));
#pragma warning restore CS8604 // Possible null reference argument.
                var loginTokenDescripter = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Email, emailID),
                        new Claim("UserID", UserID.ToString()),
                    }),
                    Expires = DateTime.UtcNow.AddHours(5),
                    SigningCredentials = new SigningCredentials(loginSecurityKey, SecurityAlgorithms.HmacSha256Signature),
                };
                var token = new JwtSecurityTokenHandler().CreateToken(loginTokenDescripter);
                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Registration.
        /// </summary>
        /// <param name="userRegistration">userRegistration.</param>
        /// <returns>UserRegistrationModel.</returns>
        /// <exception cref="Exception">Exception.</exception>
        public UserRegistrationModel Registration(UserRegistrationModel userRegistration)
        {
            SqlConnection connection = new SqlConnection(this.connectionString);
            try
            {
                using (connection)
                {
                    SqlCommand command = new SqlCommand("SPRegister", connection);

                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@FirstName", userRegistration.FirstName);
                    command.Parameters.AddWithValue("@LastName", userRegistration.LastName);
                    command.Parameters.AddWithValue("@EmailID", userRegistration.EmailID);
#pragma warning disable CS8604 // Possible null reference argument.
                    command.Parameters.AddWithValue("@Password", EncryptPassword(userRegistration.Password));
#pragma warning restore CS8604 // Possible null reference argument.

                    connection.Open();
                    int registerOrNot = command.ExecuteNonQuery();

                    if (registerOrNot >= 1)
                    {
                        return userRegistration;
                    }

#pragma warning disable CS8603 // Possible null reference return.
                    return null;
#pragma warning restore CS8603 // Possible null reference return.
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

        /// <summary>
        /// Login.
        /// </summary>
        /// <param name="userLogin">userLogin.</param>
        /// <returns>UserRegistrationModel.</returns>
        /// <exception cref="Exception">Exception.</exception>
        public UserRegistrationModel Login(UserLoginModel userLogin)
        {
            SqlConnection connection = new SqlConnection(this.connectionString);
            try
            {
                UserRegistrationModel userRegistration = new UserRegistrationModel();
                using (connection)
                {
                    ConnectionMultiplexer connectionMultiplexer = ConnectionMultiplexer.Connect("127.0.0.1:6379");
                    IDatabase database = connectionMultiplexer.GetDatabase();

                    SqlCommand command = new SqlCommand("SPLogin", connection);

                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@EmailID", userLogin.EmailID);
#pragma warning disable CS8604 // Possible null reference argument.
                    command.Parameters.AddWithValue("@Password", EncryptPassword(userLogin.Password));
#pragma warning restore CS8604 // Possible null reference argument.

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            userRegistration.UserID = reader.IsDBNull("UserID") ? 0 : reader.GetInt32("UserID");
                            userRegistration.FirstName = reader.IsDBNull("FirstName") ? string.Empty : reader.GetString("FirstName");
                            userRegistration.LastName = reader.IsDBNull("LastName") ? string.Empty : reader.GetString("LastName");
                            userRegistration.EmailID = reader.IsDBNull("EmailID") ? string.Empty : reader.GetString("EmailID");
                        }

                        database.StringSet(key: "UserID", userRegistration.UserID.ToString());
                        database.StringSet(key: "FirstName", userRegistration.FirstName);
                        database.StringSet(key: "LastName", userRegistration.LastName);

                        // var token = GenerateJWTToken(userLogin.EmailID, userRegistration.UserID);
                        return userRegistration;
                    }

#pragma warning disable CS8603 // Possible null reference return.
                    return null;
#pragma warning restore CS8603 // Possible null reference return.
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

        /// <summary>
        /// CreateTicketForPassword.
        /// </summary>
        /// <param name="emailID">emailID.</param>
        /// <param name="token">token.</param>
        /// <returns>UserTicket.</returns>
        /// <exception cref="Exception">Exception.</exception>
        public UserTicket CreateTicketForPassword(string emailID, string token)
        {
            SqlConnection connection = new SqlConnection(this.connectionString);
            try
            {
                UserTicket ticket = new UserTicket();
                using (connection)
                {
                    SqlCommand command = new SqlCommand("SPForgot", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@EmailID", emailID);

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            ticket.FirstName = reader.IsDBNull("FirstName") ? string.Empty : reader.GetString("FirstName");
                            ticket.EmailId = reader.IsDBNull("EmailID") ? string.Empty : reader.GetString("EmailID");
                            ticket.Token = token;
                            ticket.IssueAt = DateTime.Now;
                        }

                        return ticket;
                    }
#pragma warning disable CS8603 // Possible null reference return.
                    return null;
#pragma warning restore CS8603 // Possible null reference return.
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// ForgotPassword.
        /// </summary>
        /// <param name="emailID">emailID.</param>
        /// <returns>string.</returns>
        /// <exception cref="Exception">Exception.</exception>
        public string ForgotPassword(string emailID)
        {
            SqlConnection connection = new SqlConnection(this.connectionString);
            try
            {
                UserRegistrationModel userRegistration = new UserRegistrationModel();

                using (connection)
                {
                    SqlCommand command = new SqlCommand("SPForgot", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@EmailID", emailID);

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            userRegistration.UserID = reader.IsDBNull("UserID") ? 0 : reader.GetInt32("UserID");
                            userRegistration.FirstName = reader.IsDBNull("FirstName") ? string.Empty : reader.GetString("FirstName");
                        }

                        var token = this.GenerateJWTToken(emailID, userRegistration.UserID);
                        MSMQModel mSMQModel = new MSMQModel();
#pragma warning disable CS8604 // Possible null reference argument.
                        mSMQModel.SendMessage(token, emailID, userRegistration.FirstName);
#pragma warning restore CS8604 // Possible null reference argument.
                        return token;
                    }

#pragma warning disable CS8603 // Possible null reference return.
                    return null;
#pragma warning restore CS8603 // Possible null reference return.
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

        /// <summary>
        /// ResetPassword.
        /// </summary>
        /// <param name="userResetPassword">userResetPassword.</param>
        /// <param name="emailID">emailID.</param>
        /// <returns>bool.</returns>
        /// <exception cref="Exception">Exception.</exception>
        public bool ResetPassword(UserResetPasswordModel userResetPassword, string emailID)
        {
            SqlConnection connection = new SqlConnection(this.connectionString);
            try
            {
                using (connection)
                {
                    SqlCommand command = new SqlCommand("SPResetPassword", connection);

                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@EmailID", emailID);
#pragma warning disable CS8604 // Possible null reference argument.
                    command.Parameters.AddWithValue("@Password", EncryptPassword(userResetPassword.Password));
#pragma warning restore CS8604 // Possible null reference argument.
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

        /// <summary>
        /// GetUser.
        /// </summary>
        /// <param name="userID">userID.</param>
        /// <returns>UserRegistrationModel.</returns>
        /// <exception cref="Exception">Exception.</exception>
        public UserRegistrationModel GetUser(int userID)
        {
            SqlConnection connection = new SqlConnection(this.connectionString);
            try
            {
                UserRegistrationModel model = new UserRegistrationModel();
                SqlCommand command = new SqlCommand("GetUser", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@UserID", userID);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        model.UserID = reader.IsDBNull("UserID") ? 0 : reader.GetInt32("UserID");
                        model.FirstName = reader.IsDBNull("FirstName") ? string.Empty : reader.GetString("FirstName");
                        model.LastName = reader.IsDBNull("LastName") ? string.Empty : reader.GetString("LastName");
                        model.EmailID = reader.IsDBNull("EmailID") ? string.Empty : reader.GetString("EmailID");
                    }

                    return model;
                }

#pragma warning disable CS8603 // Possible null reference return.
                return null;
#pragma warning restore CS8603 // Possible null reference return.
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
    }
}
