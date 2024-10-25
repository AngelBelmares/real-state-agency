using System;
using System.Collections.Generic;

namespace RealStateAgency.Models;

public partial class Appointment
{
    public int AppointmentId { get; set; }

    public int? UserId { get; set; }

    public int? AgentId { get; set; }

    public int? HouseId { get; set; }

    public DateTime? Date { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Agent? Agent { get; set; }

    public virtual House? House { get; set; }

    public virtual User? User { get; set; }
}

public class AppointmentDto
{
    public int AppointmentId { get; set; }
    public int UserId { get; set; }
    public string UserCompleteName { get; set; } = string.Empty;
    public int? AgentId { get; set; }
    public string AgentCompleteName { get; set; } = string.Empty;
    public int? HouseId { get; set; }
    public string HouseLocation { get; set; } = string.Empty;
    public DateTime? Date { get; set; }
}

public class AppointmentFilter
{
    public int? AppointmentId { get; set; }

    public int? UserId { get; set; }

    public int? AgentId { get; set; }

    public int? HouseId { get; set; }
}