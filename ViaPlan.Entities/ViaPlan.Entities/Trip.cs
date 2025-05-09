using System;
using System.Collections.Generic;

namespace ViaPlan.Entities;

public partial class Trip
{
    public int Id { get; set; }

    public string Destination { get; set; } = null!;

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public string? WeatherSummary { get; set; }

    public string? HotelRecommendation { get; set; }

    public int UserId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual User User { get; set; } = null!;
}
