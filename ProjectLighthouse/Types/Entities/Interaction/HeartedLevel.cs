using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LBPUnion.ProjectLighthouse.Types.Entities.Level;
using LBPUnion.ProjectLighthouse.Types.Entities.Profile;

namespace LBPUnion.ProjectLighthouse.Types.Entities.Interaction;

public class HeartedLevel
{
    // ReSharper disable once UnusedMember.Global
    [Key]
    public int HeartedLevelId { get; set; }

    public int UserId { get; set; }

    [ForeignKey(nameof(UserId))]
    public User User { get; set; }

    public int SlotId { get; set; }

    [ForeignKey(nameof(SlotId))]
    public Slot Slot { get; set; }
}