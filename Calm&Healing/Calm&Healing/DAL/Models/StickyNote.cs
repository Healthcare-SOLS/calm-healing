using System;
using System.Collections.Generic;

namespace Calm_Healing.DAL.Models;

public partial class StickyNote
{
    public long Id { get; set; }

    public Guid Uuid { get; set; }

    public string? Note { get; set; }

    public Guid? ClientId { get; set; }

    public string? CreatedBy { get; set; }

    public string? ModifiedBy { get; set; }

    public DateTime Created { get; set; }

    public DateTime? Modified { get; set; }

    public string? AlertNote { get; set; }

    public string? AlertNoteCreatedBy { get; set; }

    public string? StickyNoteCreatedBy { get; set; }

    public DateTime? AlertNoteModified { get; set; }

    public DateTime? StickyNoteModified { get; set; }
}
