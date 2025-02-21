using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LBPUnion.ProjectLighthouse.Types.Entities.Profile;

namespace LBPUnion.ProjectLighthouse.Types.Entities.Token;

public class EmailVerificationToken
{
    // ReSharper disable once UnusedMember.Global
    [Key]
    public int EmailVerificationTokenId { get; set; }

    public int UserId { get; set; }

    [ForeignKey(nameof(UserId))]
    public User User { get; set; }

    public string EmailToken { get; set; }

    public DateTime ExpiresAt { get; set; }
}