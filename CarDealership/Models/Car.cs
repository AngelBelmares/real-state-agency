using System;
using System.Collections.Generic;

namespace CarDealership.Models;

public partial class Car
{
    public int CarId { get; set; }

    public int? DealershipId { get; set; }

    public int Name { get; set; }

    public int Year { get; set; }

    public string Description { get; set; } = null!;

    public string ImageUrl { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    public virtual Dealership? Dealership { get; set; }
}

public class CarFilter
{
    public int? CarId { get; set; }
    public int? DealershipId { get; set; }
}

public class CarDto
{
    public int CarId { get; set; }

    public int? DealershipId { get; set; }

    public int Name { get; set; }

    public int Year { get; set; }

    public string Description { get; set; } = null!;

    public string ImageUrl { get; set; } = null!;
    public string DealerShipName { get; set; } = null!;
}
