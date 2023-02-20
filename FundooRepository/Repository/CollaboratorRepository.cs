// <copyright file="CollaboratorRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace FundooRepository.Repository
{
    using System.Data;
    using System.Data.SqlClient;
    using FundooModel;
    using FundooRepository.Interface;
    using Microsoft.Extensions.Configuration;

    /// <summary>
    /// CollaboratorRepository.
    /// </summary>
    public class CollaboratorRepository : ICollaboratorRepository
    {
        private string? connectionString;

        /// <summary>
        /// Initializes a new instance of the <see cref="CollaboratorRepository"/> class.
        /// </summary>
        /// <param name="configuration">configuration.</param>
        public CollaboratorRepository(IConfiguration configuration)
        {
            this.connectionString = configuration.GetConnectionString("UserDBConnection");
        }

        /// <summary>
        /// AddCollaborator.
        /// </summary>
        /// <param name="collaboratorEmail">collaboratorEmail.</param>
        /// <param name="userID">userID.</param>
        /// <param name="noteID">noteID.</param>
        /// <returns>bool.</returns>
        /// <exception cref="Exception">Exception.</exception>
        public bool AddCollaborator(string collaboratorEmail, int userID, int noteID)
        {
            SqlConnection connection = new SqlConnection(this.connectionString);
            try
            {
                SqlCommand command = new SqlCommand("SPAddcollaborator", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@CollabratorEmail", collaboratorEmail);
                command.Parameters.AddWithValue("@NoteID", noteID);
                command.Parameters.AddWithValue("@UserID", userID);
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

        /// <summary>
        /// RemoveCollaborator.
        /// </summary>
        /// <param name="collaboratorID">collaboratorID.</param>
        /// <returns>bool.</returns>
        /// <exception cref="Exception">Exception.</exception>
        public bool RemoveCollaborator(int collaboratorID)
        {
            SqlConnection connection = new SqlConnection(this.connectionString);
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

        /// <summary>
        /// GetCollaborators.
        /// </summary>
        /// <param name="noteID">noteID.</param>
        /// <returns>List of CollaboratorModel.</returns>
        /// <exception cref="Exception">Exception.</exception>
        public List<CollaboratorModel> GetCollaborators(int noteID)
        {
            SqlConnection connection = new SqlConnection(this.connectionString);
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
                            UserID = dataReader.IsDBNull("UserID") ? 0 : dataReader.GetInt32("UserID"),
                        };
                        collaborators.Add(collaboratorModel);
                    }

                    return collaborators;
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