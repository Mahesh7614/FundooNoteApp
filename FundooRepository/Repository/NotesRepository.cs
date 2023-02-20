// <copyright file="NotesRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace FundooRepository.Repository
{
    using System.Data;
    using System.Data.SqlClient;
    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using FundooModel;
    using FundooRepository.Interface;
    using Microsoft.Extensions.Configuration;

    /// <summary>
    /// NotesRepository.
    /// </summary>
    public class NotesRepository : INotesRepository
    {
        private string? connectionString;

        /// <summary>
        /// Initializes a new instance of the <see cref="NotesRepository"/> class.
        /// </summary>
        /// <param name="configuration">configuration.</param>
        public NotesRepository(IConfiguration configuration)
        {
            this.connectionString = configuration.GetConnectionString("UserDBConnection");
        }

        /// <summary>
        /// CreateNotes.
        /// </summary>
        /// <param name="notecreateModel">notecreateModel.</param>
        /// <param name="userID">userID.</param>
        /// <returns>NoteCreateModel.</returns>
        /// <exception cref="Exception">Exception.</exception>
        public NoteCreateModel CreateNotes(NoteCreateModel notecreateModel, int userID)
        {
            SqlConnection connection = new SqlConnection(this.connectionString);
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
        /// DisplayNotes.
        /// </summary>
        /// <param name="userID">userID.</param>
        /// <returns>List of NoteModel.</returns>
        /// <exception cref="Exception">Exception.</exception>
        public List<NoteModel> DisplayNotes(int userID)
        {
            SqlConnection connection = new SqlConnection(this.connectionString);
            try
            {
                List<NoteModel> listNote = new List<NoteModel>();
                using (connection)
                {
                    SqlCommand command = new SqlCommand("SPGetNotes", connection);

                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserID", userID);

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            NoteModel noteModel = new NoteModel()
                            {
                                NoteID = reader.IsDBNull("NoteID") ? 0 : reader.GetInt32("NoteID"),
                                UserID = reader.IsDBNull("UserID") ? 0 : reader.GetInt32("UserID"),
                                Title = reader.IsDBNull("Title") ? string.Empty : reader.GetString("Title"),
                                Description = reader.IsDBNull("Description") ? string.Empty : reader.GetString("Description"),
                                Reminder = reader.IsDBNull("Reminder") ? DateTime.MinValue : reader.GetDateTime("Reminder"),
                                Color = reader.IsDBNull("Color") ? string.Empty : reader.GetString("Color"),
                                Image = reader.IsDBNull("Image") ? string.Empty : reader.GetString("Image"),
                                Archive = reader.IsDBNull("Archive") ? false : reader.GetBoolean("Archive"),
                                PinNotes = reader.IsDBNull("PinNotes") ? false : reader.GetBoolean("PinNotes"),
                                Trash = reader.IsDBNull("Trash") ? false : reader.GetBoolean("Archive"),
                                Created = reader.IsDBNull("Created") ? DateTime.MinValue : reader.GetDateTime("Created"),
                                Modified = reader.IsDBNull("Modified") ? DateTime.MinValue : reader.GetDateTime("Modified"),
                            };
                            listNote.Add(noteModel);
                        }

                        return listNote;
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
        /// UpdateNotes.
        /// </summary>
        /// <param name="updateNote">updateNote.</param>
        /// <param name="userID">userID.</param>
        /// <param name="noteID">noteID.</param>
        /// <returns>UpdateNoteModel.</returns>
        /// <exception cref="Exception">Exception.</exception>
        public UpdateNoteModel UpdateNotes(UpdateNoteModel updateNote, int userID, int noteID)
        {
            SqlConnection connection = new SqlConnection(this.connectionString);
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
        /// DeleteNote.
        /// </summary>
        /// <param name="userID">userID.</param>
        /// <param name="noteID">noteID.</param>
        /// <returns>bool.</returns>
        /// <exception cref="Exception">Exception.</exception>
        public bool DeleteNote(int userID, int noteID)
        {
            SqlConnection connection = new SqlConnection(this.connectionString);
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

        /// <summary>
        /// PinNote.
        /// </summary>
        /// <param name="pinNote">pinNote.</param>
        /// <param name="userID">userID.</param>
        /// <param name="noteID">noteID.</param>
        /// <returns>bool.</returns>
        /// <exception cref="Exception">Exception.</exception>
        public bool PinNote(bool pinNote, int userID, int noteID)
        {
            SqlConnection connection = new SqlConnection(this.connectionString);
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

        /// <summary>
        /// ArchiveNote.
        /// </summary>
        /// <param name="archiveNote">archiveNote.</param>
        /// <param name="userID">userID.</param>
        /// <param name="noteID">noteID.</param>
        /// <returns>bool.</returns>
        /// <exception cref="Exception">Exception.</exception>
        public bool ArchiveNote(bool archiveNote, int userID, int noteID)
        {
            SqlConnection connection = new SqlConnection(this.connectionString);
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

        /// <summary>
        /// TrashNote.
        /// </summary>
        /// <param name="trashNote">trashNote.</param>
        /// <param name="userID">userID.</param>
        /// <param name="noteID">noteID.</param>
        /// <returns>bool.</returns>
        /// <exception cref="Exception">Exception.</exception>
        public bool TrashNote(bool trashNote, int userID, int noteID)
        {
            SqlConnection connection = new SqlConnection(this.connectionString);
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

        /// <summary>
        /// Color.
        /// </summary>
        /// <param name="color">color.</param>
        /// <param name="userID">userID.</param>
        /// <param name="noteID">noteID.</param>
        /// <returns>bool.</returns>
        /// <exception cref="Exception">Exception.</exception>
        public bool Color(string color, int userID, int noteID)
        {
            SqlConnection connection = new SqlConnection(this.connectionString);
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

        /// <summary>
        /// Remainder.
        /// </summary>
        /// <param name="remainder">remainder.</param>
        /// <param name="userID">userID.</param>
        /// <param name="noteID">noteID.</param>
        /// <returns>bool.</returns>
        /// <exception cref="Exception">Exception.</exception>
        public bool Remainder(DateTime remainder, int userID, int noteID)
        {
            SqlConnection connection = new SqlConnection(this.connectionString);
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

        // public bool UploadImage(string filePath, int noteID, int userID)
        // {
        //    SqlConnection connection = new SqlConnection(connectionString);

        // try
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

        // command.CommandType = CommandType.StoredProcedure;
        //            command.Parameters.AddWithValue("@UserID", userID);
        //            command.Parameters.AddWithValue("@NoteID", noteID);
        //            command.Parameters.AddWithValue("@Modified", DateTime.Now);
        //            command.Parameters.AddWithValue("@Image", uploadResult.Url.ToString());

        // connection.Open();
        //            int deleteOrNot = command.ExecuteNonQuery();

        // if (deleteOrNot >= 1)
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
        // }
    }
}