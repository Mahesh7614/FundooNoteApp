// <copyright file="ILabelRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace FundooRepository.Interface
{
    using FundooModel;

    /// <summary>
    /// ILabelRepository interface.
    /// </summary>
    public interface ILabelRepository
    {
        /// <summary>
        /// AddLabel.
        /// </summary>
        /// <param name="label">label.</param>
        /// <param name="noteID">noteID.</param>
        /// <param name="userID">userID.</param>
        /// <returns>bool.</returns>
        public bool AddLabel(string label, int noteID, int userID);

        /// <summary>
        /// GetLables.
        /// </summary>
        /// <param name="noteID">noteID.</param>
        /// <returns>List of LabelModel.</returns>
        public List<LabelModel> GetLables(int noteID);

        /// <summary>
        /// UpdateLabel.
        /// </summary>
        /// <param name="newlabel">newlabel.</param>
        /// <param name="labelID">labelID.</param>
        /// <returns>bool.</returns>
        public bool UpdateLabel(string newlabel, int labelID);

        /// <summary>
        /// DeleteLabel.
        /// </summary>
        /// <param name="labelID">labelID.</param>
        /// <returns>bool.</returns>
        public bool DeleteLabel(int labelID);
    }
}
