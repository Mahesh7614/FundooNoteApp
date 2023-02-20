// <copyright file="ErrorHandleMiddleware.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace FundooNoteApp.Middleware
{
    using System.Net;
    using System.Text.Json;

    /// <summary>
    /// ErrorHandleMiddleware.
    /// </summary>
    public class ErrorHandleMiddleware
    {
        private readonly RequestDelegate next;

        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorHandleMiddleware"/> class.
        /// ErrorHandleMiddleware Constructor.
        /// </summary>
        /// <param name="next">next.</param>
        public ErrorHandleMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        /// <summary>
        /// Invoke.
        /// </summary>
        /// <param name="context">context.</param>
        /// <returns>Task.</returns>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await this.next(context);
            }
            catch (Exception error)
            {
                var resp = context.Response;
                resp.ContentType = "application/json";
                switch (error)
                {
                    case ApplicationException e:
                        resp.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    case KeyNotFoundException e:
                        resp.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    case UnauthorizedAccessException e:
                        resp.StatusCode = (int)HttpStatusCode.Unauthorized;
                        break;
                    default:
                        resp.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                var result = JsonSerializer.Serialize(new { success = false, message = error.Message });
                await resp.WriteAsync(result);
            }
        }
    }
}