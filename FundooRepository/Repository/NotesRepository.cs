using FundooModel;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using FundooRepository.Interface;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace FundooRepository.Repository
{
    public class NotesRepository : INotesRepository
    {
        string connectionString;

        public NotesRepository(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("UserDBConnection");
        }
        public NoteCreateModel CreateNotes(NoteCreateModel notecreateModel, int userID)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                using (connection)
                {
                    SqlCommand command = new SqlCommand("SPCreateNote", connection);

                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Title", notecreateModel.Title);
                    command.Parameters.AddWithValue("@Description", notecreateModel.Description);
                    command.Parameters.AddWithValue("@Reminder", notecreateModel.Reminder);
                    command.Parameters.AddWithValue("@Color", notecreateModel.Color);
                    command.Parameters.AddWithValue("@Image", notecreateModel.Image);
                    command.Parameters.AddWithValue("@Archive", notecreateModel.Archive);
                    command.Parameters.AddWithValue("@PinNotes", notecreateModel.PinNotes);
                    command.Parameters.AddWithValue("@Trash", notecreateModel.Trash);
                    command.Parameters.AddWithValue("@Created", DateTime.Now);
                    command.Parameters.AddWithValue("@Modified", DateTime.Now);
                    command.Parameters.AddWithValue("@UserID", userID);

                    connection.Open();
                    int registerOrNot = command.ExecuteNonQuery();

                    if (registerOrNot >= 1)
                    {
                        return notecreateModel;
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
        public List<NoteModel> DisplayNotes(int userID)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                List<NoteModel> listNote = new List<NoteModel>();
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
                            NoteModel noteModel = new NoteModel()
                            {
                                NoteID = Reader.IsDBNull("NoteID") ? 0 : Reader.GetInt32("NoteID"),
                                UserID = Reader.IsDBNull("UserID") ? 0 : Reader.GetInt32("UserID"),
                                Title = Reader.IsDBNull("Title") ? string.Empty : Reader.GetString("Title"),
                                Description = Reader.IsDBNull("Description") ? string.Empty : Reader.GetString("Description"),
                                Reminder = Reader.IsDBNull("Reminder") ? DateTime.MinValue : Reader.GetDateTime("Reminder"),
                                Color = Reader.IsDBNull("Color") ? string.Empty : Reader.GetString("Color"),
                                Image = Reader.IsDBNull("Image") ? string.Empty : Reader.GetString("Image"),
                                Archive = Reader.IsDBNull("Archive") ? false : Reader.GetBoolean("Archive"),
                                PinNotes = Reader.IsDBNull("PinNotes") ? false : Reader.GetBoolean("PinNotes"),
                                Trash = Reader.IsDBNull("Trash") ? false : Reader.GetBoolean("Archive"),
                                Created = Reader.IsDBNull("Created") ? DateTime.MinValue : Reader.GetDateTime("Created"),
                                Modified = Reader.IsDBNull("Modified") ? DateTime.MinValue : Reader.GetDateTime("Modified"),
                            };
                            listNote.Add(noteModel);
                        }
                        return listNote;
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
        public UpdateNoteModel UpdateNotes(UpdateNoteModel updateNote, int userID, int noteID)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                using (connection)
                {
                    SqlCommand command = new SqlCommand("SPUpdateNote", connection);

                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserID", userID);
                    command.Parameters.AddWithValue("@NoteID", noteID);
                    command.Parameters.AddWithValue("@Title", updateNote.Title);
                    command.Parameters.AddWithValue("@Description", updateNote.Description);
                    command.Parameters.AddWithValue("@Reminder", updateNote.Reminder);
                    command.Parameters.AddWithValue("@Color", updateNote.Color);
                    command.Parameters.AddWithValue("@Image", updateNote.Image);
                    command.Parameters.AddWithValue("@Archive", updateNote.Archive);
                    command.Parameters.AddWithValue("@PinNotes", updateNote.PinNotes);
                    command.Parameters.AddWithValue("@Trash", updateNote.Trash);
                    command.Parameters.AddWithValue("@Modified", DateTime.Now);

                    connection.Open();
                    int deleteOrNot = command.ExecuteNonQuery();

                    if (deleteOrNot >= 1)
                    {
                        return updateNote;
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
        public bool PinNote(bool pinNote, int userID, int noteID)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                using (connection)
                {
                    SqlCommand command = new SqlCommand("SPPinNotes", connection);

                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserID", userID);
                    command.Parameters.AddWithValue("@NoteID", noteID);
                    command.Parameters.AddWithValue("@PinNotes", pinNote);

                    connection.Open();
                    int pinOrNot = command.ExecuteNonQuery();

                    if (pinOrNot >= 1)
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
        public bool ArchiveNote(bool archiveNote, int userID, int noteID)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                using (connection)
                {
                    SqlCommand command = new SqlCommand("SPArchiveNote", connection);

                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserID", userID);
                    command.Parameters.AddWithValue("@NoteID", noteID);
                    command.Parameters.AddWithValue("@Archive", archiveNote);

                    connection.Open();
                    int archiveOrNot = command.ExecuteNonQuery();

                    if (archiveOrNot >= 1)
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
        public bool TrashNote(bool trashNote, int userID, int noteID)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                using (connection)
                {
                    SqlCommand command = new SqlCommand("SPTrashNote", connection);

                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserID", userID);
                    command.Parameters.AddWithValue("@NoteID", noteID);
                    command.Parameters.AddWithValue("@Trash", trashNote);

                    connection.Open();
                    int trashOrNot = command.ExecuteNonQuery();

                    if (trashOrNot >= 1)
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
        public bool Color(string color, int userID, int noteID)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                using (connection)
                {
                    SqlCommand command = new SqlCommand("SPColor", connection);


                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserID", userID);
                    command.Parameters.AddWithValue("@NoteID", noteID);
                    command.Parameters.AddWithValue("@Color", color);

                    connection.Open();
                    int updateColorOrNot = command.ExecuteNonQuery();

                    if (updateColorOrNot >= 1)
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
        public bool Remainder(DateTime remainder, int userID, int noteID)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                using (connection)
                {
                    SqlCommand command = new SqlCommand("SPReminder", connection);

                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserID", userID);
                    command.Parameters.AddWithValue("@NoteID", noteID);
                    command.Parameters.AddWithValue("@Reminder", remainder);

                    connection.Open();
                    int setRemainderOrNot = command.ExecuteNonQuery();

                    if (setRemainderOrNot >= 1)
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
        //public bool UploadImage(string filePath, int noteID, int userID)
        //{
        //    SqlConnection connection = new SqlConnection(connectionString);

        //    try
        //    {
        //        Account account= new Account("dygwgjiug", "169716436645923", "5uefq_ETzI-wFz53UbvbDp7S-yk");
        //        Cloudinary cloudinary = new Cloudinary(account);
        //        ImageUploadParams uploadParams = new ImageUploadParams()
        //        {
        //            File = new FileDescription(filePath),
        //            PublicId = note
        //        };
        //        ImageUploadResult uploadResult = cloudinary.Upload(uploadParams);
        //        using (connection)
        //        {
        //            SqlCommand command = new SqlCommand("SPUploadImage", connection);

        //            command.CommandType = CommandType.StoredProcedure;
        //            command.Parameters.AddWithValue("@UserID", userID);
        //            command.Parameters.AddWithValue("@NoteID", noteID);
        //            command.Parameters.AddWithValue("@Modified", DateTime.Now);
        //            command.Parameters.AddWithValue("@Image", uploadResult.Url.ToString());

        //            connection.Open();
        //            int deleteOrNot = command.ExecuteNonQuery();

        //            if (deleteOrNot >= 1)
        //            {
        //                return true;
        //            }
        //            return false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}
    }
}