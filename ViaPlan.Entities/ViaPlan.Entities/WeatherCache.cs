using System;
using System.Collections.Generic;

namespace ViaPlan.Entities;

public partial class WeatherCache
{
    public int Id { get; set; }

    public string City { get; set; } = null!;

    public string? Summary { get; set; }

    public double? Temperature { get; set; }

    public DateTime RetrievedAt { get; set; }
}
