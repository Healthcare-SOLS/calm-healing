using System;
using System.Collections.Generic;

namespace Calm_Healing.DAL.Models;

public partial class Notification
{
    public long Id { get; set; }

    public Guid? Uuid { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public string? NotificationType { get; set; }

    public bool? MarkedAsRead { get; set; }

    public string? Data { get; set; }

    public Guid? UserId { get; set; }

    public string? CreatedBy { get; set; }

    public string? ModifiedBy { get; set; }

    public DateTime Created { get; set; }

    public DateTime? Modified { get; set; }
}
