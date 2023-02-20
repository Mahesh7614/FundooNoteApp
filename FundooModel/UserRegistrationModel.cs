// <copyright file="UserRegistrationModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace FundooModel
{
    /// <summary>
    /// UserRegistrationModel.
    /// </summary>
    public class UserRegistrationModel
    {
        /// <summary>
        /// Gets or sets userID.
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        /// Gets or sets FirstName.
        /// </summary>
        public string? FirstName { get; set; }

        /// <summary>
        /// Gets or sets LastName.
        /// </summary>
        public string? LastName { get; set; }

        /// <summary>
        /// Gets or sets EmailID.
        /// </summary>
        public string? EmailID { get; set; }

        /// <summary>
        /// Gets or sets Password.
        /// </summary>
        public string? Password { get; set; }
    }
}
