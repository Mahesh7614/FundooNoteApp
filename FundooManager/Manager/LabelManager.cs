// <copyright file="LabelManager.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace FundooManager.Manager
{
    using FundooManager.Interface;
    using FundooModel;
    using FundooRepository.Interface;

    /// <summary>
    /// LabelManager.
    /// </summary>
    public class LabelManager : ILabelManager
    {
        private readonly ILabelRepository labelRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="LabelManager"/> class.
        /// LabelManager Constructor.
        /// </summary>
        /// <param name="labelRepository">labelRepository.</param>
        public LabelManager(ILabelRepository labelRepository)
        {
            this.labelRepository = labelRepository;
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
            try
            {
                return this.labelRepository.AddLabel(label, noteID, userID);
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
            try
            {
                return this.labelRepository.GetLables(noteID);
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
            try
            {
                return this.labelRepository.UpdateLabel(newlabel, labelID);
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
            try
            {
                return this.labelRepository.DeleteLabel(labelID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}