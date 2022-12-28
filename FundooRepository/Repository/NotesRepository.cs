
using FundooModel;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using FundooRepository.Interface;

namespace FundooRepository.Repository
{
    public class NotesRepository : INotesRepository
    {
        string connectionString;

        public NotesRepository(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("UserDBConnection");
        }
        public NoteModel CreateNotes(NoteModel noteModel, int userID)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                using (connection)
                {
                    SqlCommand command = new SqlCommand("SPCreateNote", connection);

                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Title", noteModel.Title);
                    command.Parameters.AddWithValue("@Description", noteModel.Description);
                    command.Parameters.AddWithValue("@Reminder", noteModel.Reminder);
                    command.Parameters.AddWithValue("@Color", noteModel.Color);
                    command.Parameters.AddWithValue("@Image", noteModel.Image);
                    command.Parameters.AddWithValue("@Archive", noteModel.Archive);
                    command.Parameters.AddWithValue("@PinNotes", noteModel.PinNotes);
                    command.Parameters.AddWithValue("@Trash", noteModel.Trash);
                    command.Parameters.AddWithValue("@Created", noteModel.Created);
                    command.Parameters.AddWithValue("@Modified", noteModel.Modified);
                    command.Parameters.AddWithValue("@UserID", userID);

                    connection.Open();
                    int registerOrNot = command.ExecuteNonQuery();

                    if (registerOrNot >= 1)
                    {
                        return noteModel;
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
        public NoteModel DisplayNotes(int userID)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                NoteModel noteModel = new NoteModel();
                using (connection)
                {
                    SqlCommand command = new SqlCommand("SPGetNotes", connection);

                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserID", userID);

                    connection.Open();
                    SqlDataReader Reader = command.ExecuteReader();

                    if (Reader.HasRows)
                    {
                        while (Reader.Read())
                        {
                            noteModel.NoteID = Reader.IsDBNull("NoteID") ? 0 : Reader.GetInt32("NoteID");
                            noteModel.UserID = Reader.IsDBNull("UserID") ? 0 : Reader.GetInt32("UserID");
                            noteModel.Title = Reader.IsDBNull("Title") ? string.Empty : Reader.GetString("Title");
                            noteModel.Description = Reader.IsDBNull("Description") ? string.Empty : Reader.GetString("Description");
                            noteModel.Reminder = Reader.IsDBNull("Reminder") ?  DateTime.MinValue: Reader.GetDateTime("Reminder");
                            noteModel.Color = Reader.IsDBNull("Color") ? string.Empty : Reader.GetString("Color");
                            noteModel.Image = Reader.IsDBNull("Image") ? string.Empty : Reader.GetString("Image");
                            noteModel.Archive = Reader.IsDBNull("Archive") ? false : Reader.GetBoolean("Archive");
                            noteModel.PinNotes = Reader.IsDBNull("PinNotes") ? false : Reader.GetBoolean("PinNotes");
                            noteModel.Trash = Reader.IsDBNull("Trash") ? false : Reader.GetBoolean("Archive");
                            noteModel.Created = Reader.IsDBNull("Created") ? DateTime.MinValue : Reader.GetDateTime("Created");
                            noteModel.Modified = Reader.IsDBNull("Modified") ? DateTime.MinValue : Reader.GetDateTime("Modified");
                        }
                        return noteModel;
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
        public bool DeleteNote(int userID, int noteID)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                NoteModel noteModel = new NoteModel();
                using (connection)
                {
                    SqlCommand command = new SqlCommand("SPDeleteNote", connection);

                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserID", userID);
                    command.Parameters.AddWithValue("@NoteID", noteID);

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
