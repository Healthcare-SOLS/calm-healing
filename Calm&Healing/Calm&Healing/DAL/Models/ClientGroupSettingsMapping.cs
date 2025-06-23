using System;
using System.Collections.Generic;

namespace Calm_Healing.DAL.Models;

public partial class ClientGroupSettingsMapping
{
    public long Id { get; set; }

    public long GroupId { get; set; }

    public long ClientId { get; set; }

    public virtual Client Client { get; set; } = null!;

    public virtual GroupSetting Group { get; set; } = null!;
}
