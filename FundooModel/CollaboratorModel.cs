// <copyright file="CollaboratorModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace FundooModel
{
    /// <summary>
    /// CollaboratorModel.
    /// </summary>
    public class CollaboratorModel
    {
        /// <summary>
        /// Gets or sets collaboratorID.
        /// </summary>
        public int CollaboratorID { get; set; }

        /// <summary>
        /// Gets or sets collaboratorEmail.
        /// </summary>
        public string? CollaboratorEmail { get; set; }

        /// <summary>
        /// Gets or sets modifiedTime.
        /// </summary>
        public DateTime ModifiedTime { get; set; }

        /// <summary>
        /// Gets or sets userID.
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        /// Gets or sets noteID.
        /// </summary>
        public int NoteID { get; set; }
    }
}