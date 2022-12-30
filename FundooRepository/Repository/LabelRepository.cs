
using FundooModel;
using FundooRepository.Interface;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace FundooRepository.Repository
{
    public class LabelRepository : ILabelRepository
    {
        string connectionString;

        public LabelRepository(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("UserDBConnection");
        }
        public bool AddLabel(string label,int noteID, int userID)
        {
            SqlConnection connection = new SqlConnection(connectionString);

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
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<LabelModel> GetLables(int noteID)
        {
            SqlConnection connection = new SqlConnection(connectionString);

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
                                UserID = Reader.IsDBNull("UserID") ? 0 : Reader.GetInt32("UserID")
                            };
                            listLabel.Add(label);
                        }
                        return listLabel;
                    }
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool UpdateLabel(string newlabel,int labelID)
        {
            SqlConnection connection = new SqlConnection(connectionString);

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
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool DeleteLabel(int labelID)
        {
            SqlConnection connection = new SqlConnection(connectionString);

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
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
