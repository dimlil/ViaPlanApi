namespace ViaPlan.Entities;

public partial class Hotel
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string City { get; set; } = null!;

    public string? Address { get; set; }

    public decimal? PricePerNight { get; set; }

    public double? Rating { get; set; }

    public DateTime CachedAt { get; set; }
}
