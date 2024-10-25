using System;
using System.Collections.Generic;

namespace RealStateAgency.Models;

public partial class User
{
    public int UserId { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Mail { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Lastname { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}

public partial class UserDto
{
    public int UserId { get; set; }

    public string Username { get; set; } = null!;

    public string Mail { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Lastname { get; set; } = null!;
}

public partial class Login
{
    public string? Username { get; set; } = null!;
    public string? Mail { get; set; } = null!;
    public string Password { get; set; } = null!;
}

public partial class Session
{
    public UserDto User { get; set; } = null!;
    public string Token { get; set; } = null!;

}