﻿// <copyright file="NoteModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace FundooModel
{
    /// <summary>
    /// NoteModel.
    /// </summary>
    public class NoteModel
    {
        /// <summary>
        /// Gets or sets noteID.
        /// </summary>
        public int NoteID { get; set; }

        /// <summary>
        /// Gets or sets Title.
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        /// Gets or sets Description.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets Reminder.
        /// </summary>
        public DateTime Reminder { get; set; }

        /// <summary>
        /// Gets or sets Color.
        /// </summary>
        public string? Color { get; set; }

        /// <summary>
        /// Gets or sets Image.
        /// </summary>
        public string? Image { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether Archive.
        /// </summary>
        public bool Archive { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether PinNotes.
        /// </summary>
        public bool PinNotes { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether trash.
        /// </summary>
        public bool Trash { get; set; }

        /// <summary>
        /// Gets or sets Created.
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// Gets or sets Modified.
        /// </summary>
        public DateTime Modified { get; set; }

        /// <summary>
        /// Gets or sets UserID.
        /// </summary>
        public int UserID { get; set; }
    }
}
