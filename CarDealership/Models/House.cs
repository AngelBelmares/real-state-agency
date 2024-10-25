using System;
using System.Collections.Generic;

namespace CarDealership.Models;

public partial class House
{
    public int HouseId { get; set; }

    public string? Location { get; set; }

    public int? Price { get; set; }

    public string? Description { get; set; }

    public string? Image { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}

public class HouseFilter
{
    public int? HouseId { get; set; }
}
