// <copyright file="FundooAppException.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace FundooNoteApp.Helpers
{
    /// <summary>
    /// FundooAppException.
    /// </summary>
    public class FundooAppException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FundooAppException"/> class.
        /// </summary>
        public FundooAppException()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FundooAppException"/> class.
        /// </summary>
        /// <param name="message">message.</param>
        public FundooAppException(string message)
            : base(message)
        {
        }
    }
}
