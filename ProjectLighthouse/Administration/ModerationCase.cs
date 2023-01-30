using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Threading.Tasks;
using LBPUnion.ProjectLighthouse.Levels;
using LBPUnion.ProjectLighthouse.PlayerData.Profiles;
using Microsoft.EntityFrameworkCore;

namespace LBPUnion.ProjectLighthouse.Administration;
#nullable enable

public class ModerationCase
{
    [Key]
    public int CaseId { get; set; }
    
    public CaseType Type { get; set; }

    public string Reason { get; set; } = "";

    public string ModeratorNotes { get; set; } = "";

    public bool Processed { get; set; } = false;
    
    public DateTime CreatedAt { get; set; }
    
    public DateTime? ExpiresAt { get; set; }
    public bool Expired => this.ExpiresAt != null && this.ExpiresAt < DateTime.Now;

    public DateTime? DismissedAt { get; set; }
    public bool Dismissed => this.DismissedAt != null;

    public int? DismisserId { get; set; }
    public string? DismisserUsername { get; set; }

    [ForeignKey(nameof(DismisserId))]
    public virtual User? Dismisser { get; set; }
    
    public int CreatorId { get; set; }
    public required string CreatorUsername { get; set; }

    [ForeignKey(nameof(CreatorId))]
    public virtual User? Creator { get; set; }
    
    public int AffectedId { get; set; }

    #region Get affected id result
    public Task<User?> GetUserAsync(Database database)
    {
        Debug.Assert(this.Type.AffectsUser());
        return database.Users.FirstOrDefaultAsync(u => u.UserId == this.AffectedId);
    }
    
    public Task<Slot?> GetSlotAsync(Database database)
    {
        Debug.Assert(this.Type.AffectsLevel());
        return database.Slots.FirstOrDefaultAsync(u => u.SlotId == this.AffectedId);
    }
    #endregion
}