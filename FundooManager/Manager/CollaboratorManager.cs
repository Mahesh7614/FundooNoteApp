// <copyright file="CollaboratorManager.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace FundooManager.Manager
{
    using FundooManager.Interface;
    using FundooModel;
    using FundooRepository.Interface;

    /// <summary>
    /// CollaboratorManager.
    /// </summary>
    public class CollaboratorManager : ICollaboratorManager
    {
        private readonly ICollaboratorRepository collaboratorRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="CollaboratorManager"/> class.
        /// CollaboratorManager.
        /// </summary>
        /// <param name="collaboratorRepository">collaboratorRepository.</param>
        public CollaboratorManager(ICollaboratorRepository collaboratorRepository)
        {
            this.collaboratorRepository = collaboratorRepository;
        }

        /// <summary>
        /// AddCollaborator.
        /// </summary>
        /// <param name="collaboratorEmail">collaboratorEmail.</param>
        /// <param name="userID">userID.</param>
        /// <param name="noteID">noteID.</param>
        /// <returns>bool</returns>
        /// <exception cref="Exception">Exception.</exception>
        public bool AddCollaborator(string collaboratorEmail, int userID, int noteID)
        {
            try
            {
                return this.collaboratorRepository.AddCollaborator(collaboratorEmail, userID, noteID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
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
            try
            {
                return this.collaboratorRepository.RemoveCollaborator(collaboratorID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
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
            try
            {
                return this.collaboratorRepository.GetCollaborators(noteID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
