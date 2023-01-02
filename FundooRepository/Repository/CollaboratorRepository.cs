using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using FundooModel;
using FundooRepository.Interface;

namespace FundooRepository.Repository
{
    public class CollaboratorRepository : ICollaboratorRepository
    {
        string connectionString;

        public CollaboratorRepository(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("UserDBConnection");
        }
        public bool AddCollaborator(string collaboratorEmail, int UserID, int NoteID)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                SqlCommand command = new SqlCommand("SPAddcollaborator", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@CollabratorEmail", collaboratorEmail);
                command.Parameters.AddWithValue("@NoteID", NoteID);
                command.Parameters.AddWithValue("@UserID", UserID);
                command.Parameters.AddWithValue("@Modified", DateTime.Now);

                connection.Open();
                int addOrNot = command.ExecuteNonQuery();

                if (addOrNot >= 1)
                {
                    return true;
                }
                return false;
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
        public bool RemoveCollaborator(int collaboratorID)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                SqlCommand command = new SqlCommand("SPRemoveCollaborator", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@CollabratorID", collaboratorID);

                connection.Open();
                int removeOrNot = command.ExecuteNonQuery();

                if (removeOrNot >= 1)
                {
                    return true;
                }
                return false;
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
        public List<CollaboratorModel> GetCollaborators(int noteID)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                List<CollaboratorModel> collaborators = new List<CollaboratorModel>();
                SqlCommand command = new SqlCommand("SPGetCollaborators", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@NoteID", noteID);

                connection.Open();
                SqlDataReader dataReader = command.ExecuteReader();

                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        CollaboratorModel collaboratorModel = new CollaboratorModel()
                        {
                            CollaboratorID = dataReader.IsDBNull("CollabratorID") ? 0 : dataReader.GetInt32("CollabratorID"),
                            CollaboratorEmail = dataReader.IsDBNull("CollabratorEmail") ? string.Empty : dataReader.GetString("CollabratorEmail"),
                            ModifiedTime = dataReader.IsDBNull("Modified") ? DateTime.MinValue : dataReader.GetDateTime("Modified"),
                            NoteID = dataReader.IsDBNull("NoteID") ? 0 : dataReader.GetInt32("NoteID"),
                            UserID = dataReader.IsDBNull("UserID") ? 0 : dataReader.GetInt32("UserID")
                        };
                        collaborators.Add(collaboratorModel);
                    }
                    return collaborators;
                }
                return null;
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

