using System;
using System.Collections.Generic;

namespace ViaPlan.Entities;

public partial class User
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string? Role { get; set; }

    public virtual ICollection<Trip> Trips { get; set; } = new List<Trip>();
}
