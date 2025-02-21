﻿#nullable enable
using System.Text.Json;
using LBPUnion.ProjectLighthouse.Configuration;
using LBPUnion.ProjectLighthouse.Database;
using LBPUnion.ProjectLighthouse.Extensions;
using LBPUnion.ProjectLighthouse.Files;
using LBPUnion.ProjectLighthouse.Helpers;
using LBPUnion.ProjectLighthouse.Types.Entities.Moderation;
using LBPUnion.ProjectLighthouse.Types.Entities.Token;
using LBPUnion.ProjectLighthouse.Types.Moderation.Reports;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LBPUnion.ProjectLighthouse.Servers.GameServer.Controllers;

[ApiController]
[Authorize]
[Route("LITTLEBIGPLANETPS3_XML/")]
[Produces("text/xml")]
public class ReportController : ControllerBase
{
    private readonly DatabaseContext database;

    public ReportController(DatabaseContext database)
    {
        this.database = database;
    }

    [HttpPost("grief")]
    public async Task<IActionResult> Report()
    {
        GameToken token = this.GetToken();

        string username = await this.database.UsernameFromGameToken(token);

        GriefReport? report = await this.DeserializeBody<GriefReport>();
        if (report == null) return this.BadRequest();

        SanitizationHelper.SanitizeStringsInClass(report);

        if (string.IsNullOrWhiteSpace(report.JpegHash)) return this.BadRequest();

        if (!FileHelper.ResourceExists(report.JpegHash)) return this.BadRequest();

        if (report.XmlPlayers.Length > 4) return this.BadRequest();

        if (report.XmlPlayers.Any(p => !this.database.IsUsernameValid(p.Name))) return this.BadRequest();

        report.Bounds = JsonSerializer.Serialize(report.XmlBounds.Rect, typeof(Rectangle));
        report.Players = JsonSerializer.Serialize(report.XmlPlayers, typeof(ReportPlayer[]));
        report.Timestamp = TimeHelper.TimestampMillis;
        report.ReportingPlayerId = token.UserId;

        this.database.Reports.Add(report);
        await this.database.SaveChangesAsync();

        await WebhookHelper.SendWebhook(
            title: "New grief report",
            description: $"Submitted by {username}\n" +
                         $"To view it, click [here]({ServerConfiguration.Instance.ExternalUrl}/moderation/report/{report.ReportId}).",
            dest: WebhookHelper.WebhookDestination.Moderation
        );

        return this.Ok();
    }

}