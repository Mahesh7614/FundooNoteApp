// <copyright file="ICollaboratorRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace FundooRepository.Interface
{
    using FundooModel;

    /// <summary>
    /// ICollaboratorRepository interface.
    /// </summary>
    public interface ICollaboratorRepository
    {
        /// <summary>
        /// AddCollaborator.
        /// </summary>
        /// <param name="collaboratorEmail">collaboratorEmail.</param>
        /// <param name="userID">UserID.</param>
        /// <param name="noteID">NoteID.</param>
        /// <returns>bool.</returns>
        public bool AddCollaborator(string collaboratorEmail, int userID, int noteID);

        /// <summary>
        /// RemoveCollaborator.
        /// </summary>
        /// <param name="collaboratorID">collaboratorID.</param>
        /// <returns>bool.</returns>
        public bool RemoveCollaborator(int collaboratorID);

        /// <summary>
        /// GetCollaborators.
        /// </summary>
        /// <param name="noteID">noteID.</param>
        /// <returns>List of CollaboratorModel.</returns>
        public List<CollaboratorModel> GetCollaborators(int noteID);
    }
}