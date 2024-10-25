using System;
using System.Collections.Generic;

namespace RealStateAgency.Models;

public partial class Agent
{
    public int AgentId { get; set; }

    public string? Name { get; set; }

    public string? Lastname { get; set; }

    public string? Mail { get; set; }

    public string? Telephone { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}
