// <copyright file="LabelRepository.cs" company="PlaceholderCompany">
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
    /// LabelRepository.
    /// </summary>
    public class LabelRepository : ILabelRepository
    {
        private string? connectionString;

        /// <summary>
        /// Initializes a new instance of the <see cref="LabelRepository"/> class.
        /// </summary>
        /// <param name="configuration">configuration.</param>
        public LabelRepository(IConfiguration configuration)
        {
            this.connectionString = configuration.GetConnectionString("UserDBConnection");
        }

        /// <summary>
        /// AddLabel.
        /// </summary>
        /// <param name="label">label.</param>
        /// <param name="noteID">noteID.</param>
        /// <param name="userID">userID.</param>
        /// <returns>bool.</returns>
        /// <exception cref="Exception">Exception.</exception>
        public bool AddLabel(string label, int noteID, int userID)
        {
            SqlConnection connection = new SqlConnection(this.connectionString);

            try
            {
                using (connection)
                {
                    SqlCommand command = new SqlCommand("SPAddLabel", connection);

                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@LabelName", label);
                    command.Parameters.AddWithValue("@NoteID", noteID);
                    command.Parameters.AddWithValue("@UserID", userID);

                    connection.Open();
                    int addOrNot = command.ExecuteNonQuery();

                    if (addOrNot >= 1)
                    {
                        return true;
                    }

                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// GetLables.
        /// </summary>
        /// <param name="noteID">noteID.</param>
        /// <returns>List of LabelModel.</returns>
        /// <exception cref="Exception">Exception.</exception>
        public List<LabelModel> GetLables(int noteID)
        {
            SqlConnection connection = new SqlConnection(this.connectionString);

            try
            {
                List<LabelModel> listLabel = new List<LabelModel>();
                using (connection)
                {
                    SqlCommand command = new SqlCommand("SPGetLabels", connection);

                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@NoteID", noteID);

                    connection.Open();
                    SqlDataReader Reader = command.ExecuteReader();

                    if (Reader.HasRows)
                    {
                        while (Reader.Read())
                        {
                            LabelModel label = new LabelModel()
                            {
                                LabelID = Reader.IsDBNull("LabelID") ? 0 : Reader.GetInt32("LabelID"),
                                LabelName = Reader.IsDBNull("LabelName") ? string.Empty : Reader.GetString("LabelName"),
                                NoteID = Reader.IsDBNull("NoteID") ? 0 : Reader.GetInt32("NoteID"),
                                UserID = Reader.IsDBNull("UserID") ? 0 : Reader.GetInt32("UserID"),
                            };
                            listLabel.Add(label);
                        }

                        return listLabel;
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
        /// UpdateLabel.
        /// </summary>
        /// <param name="newlabel">newlabel.</param>
        /// <param name="labelID">labelID.</param>
        /// <returns>bool.</returns>
        /// <exception cref="Exception">Exception.</exception>
        public bool UpdateLabel(string newlabel, int labelID)
        {
            SqlConnection connection = new SqlConnection(this.connectionString);

            try
            {
                using (connection)
                {
                    SqlCommand command = new SqlCommand("SPUpdateLabel", connection);

                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@LabelName", newlabel);
                    command.Parameters.AddWithValue("@LabelID", labelID);

                    connection.Open();
                    int updateOrNot = command.ExecuteNonQuery();

                    if (updateOrNot >= 1)
                    {
                        return true;
                    }

                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// DeleteLabel.
        /// </summary>
        /// <param name="labelID">labelID.</param>
        /// <returns>bool.</returns>
        /// <exception cref="Exception">Exception.</exception>
        public bool DeleteLabel(int labelID)
        {
            SqlConnection connection = new SqlConnection(this.connectionString);

            try
            {
                using (connection)
                {
                    SqlCommand command = new SqlCommand("SPDeleteLabel", connection);

                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@LabelID", labelID);

                    connection.Open();
                    int deleteOrNot = command.ExecuteNonQuery();

                    if (deleteOrNot >= 1)
                    {
                        return true;
                    }

                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
