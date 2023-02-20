// <copyright file="UserTicket.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace FundooModel
{
    /// <summary>
    /// UserTicket.
    /// </summary>
    public class UserTicket
    {
        /// <summary>
        /// Gets or sets firstName.
        /// </summary>
        public string? FirstName { get; set; }

        /// <summary>
        /// Gets or sets EmailId.
        /// </summary>
        public string? EmailId { get; set; }

        /// <summary>
        /// Gets or sets Token.
        /// </summary>
        public string? Token { get; set; }

        /// <summary>
        /// Gets or sets IssueAt.
        /// </summary>
        public DateTime IssueAt { get; set; }
    }
}
