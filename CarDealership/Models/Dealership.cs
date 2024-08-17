using System;
using System.Collections.Generic;

namespace CarDealership.Models;

public partial class Dealership
{
    public int DealershipId { get; set; }

    public string Name { get; set; } = null!;

    public string Location { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<Car> Cars { get; set; } = new List<Car>();
}
