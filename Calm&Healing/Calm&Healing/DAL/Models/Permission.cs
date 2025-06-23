using System;
using System.Collections.Generic;

namespace Calm_Healing.DAL.Models;

public partial class Permission
{
    public long Id { get; set; }

    public Guid? Uuid { get; set; }

    public string? Permission1 { get; set; }

    public string? PermissionKey { get; set; }

    public string? CreatedBy { get; set; }

    public string? ModifiedBy { get; set; }

    public DateTime Created { get; set; }

    public DateTime? Modified { get; set; }

    public string? Group { get; set; }

    public virtual ICollection<RolePermissionMapping> RolePermissionMappings { get; set; } = new List<RolePermissionMapping>();
}
