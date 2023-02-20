// <copyright file="WeatherForecast.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace FundooNoteApp
{
    /// <summary>
    /// WeatherForecast.
    /// </summary>
    public class WeatherForecast
    {
        /// <summary>
        /// Gets or sets date.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets TemperatureC.
        /// </summary>
        public int TemperatureC { get; set; }

        /// <summary>
        /// Gets temperatureF.
        /// </summary>
        public int TemperatureF => 32 + (int)(this.TemperatureC / 0.5556);

        /// <summary>
        /// Gets or sets Summary.
        /// </summary>
        public string? Summary { get; set; }
    }
}