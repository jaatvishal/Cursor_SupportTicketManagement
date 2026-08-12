using Microsoft.EntityFrameworkCore;
using SupportTicket.Core.Entities;

namespace SupportTicket.Infrastructure.Data;

public static class DatabaseInitializer
{
    public static async Task InitializeAsync(AppDbContext db)
    {
        await db.Database.EnsureCreatedAsync();

        if (await db.Users.AnyAsync())
            return;

        var alice = new User { Name = "Alice Admin", Email = "alice@company.com", Role = "Admin" };
        var bob = new User { Name = "Bob Agent", Email = "bob@company.com", Role = "Agent" };
        var carol = new User { Name = "Carol Agent", Email = "carol@company.com", Role = "Agent" };
        var dave = new User { Name = "Dave User", Email = "dave@company.com", Role = "User" };
        var eve = new User { Name = "Eve User", Email = "eve@company.com", Role = "User" };

        db.Users.AddRange(alice, bob, carol, dave, eve);
        await db.SaveChangesAsync();

        var now = DateTime.UtcNow;
        var loginTicket = new Ticket
        {
            Title = "Cannot login to portal",
            Description = "User reports 500 error when attempting to login via SSO.",
            Priority = "High",
            Status = "Open",
            AssignedTo = bob.Id,
            CreatedBy = dave.Id,
            CreatedAt = now.AddDays(-5),
            UpdatedAt = now.AddDays(-5),
        };
        var printerTicket = new Ticket
        {
            Title = "Printer not working",
            Description = "Office printer on 3rd floor shows offline status.",
            Priority = "Medium",
            Status = "In Progress",
            AssignedTo = carol.Id,
            CreatedBy = eve.Id,
            CreatedAt = now.AddDays(-3),
            UpdatedAt = now.AddDays(-1),
        };
        var licenseTicket = new Ticket
        {
            Title = "Request new software license",
            Description = "Need Adobe Creative Suite license for design team.",
            Priority = "Low",
            Status = "Resolved",
            AssignedTo = bob.Id,
            CreatedBy = dave.Id,
            CreatedAt = now.AddDays(-10),
            UpdatedAt = now.AddDays(-2),
        };
        var vpnTicket = new Ticket
        {
            Title = "VPN connection drops",
            Description = "VPN disconnects every 15 minutes on macOS Sonoma.",
            Priority = "Critical",
            Status = "Open",
            CreatedBy = eve.Id,
            CreatedAt = now.AddDays(-1),
            UpdatedAt = now.AddDays(-1),
        };
        var emailTicket = new Ticket
        {
            Title = "Email sync issue",
            Description = "Outlook not syncing sent items folder.",
            Priority = "Medium",
            Status = "Cancelled",
            AssignedTo = carol.Id,
            CreatedBy = dave.Id,
            CreatedAt = now.AddDays(-7),
            UpdatedAt = now.AddDays(-4),
        };

        db.Tickets.AddRange(loginTicket, printerTicket, licenseTicket, vpnTicket, emailTicket);
        await db.SaveChangesAsync();

        db.Comments.AddRange(
            new Comment
            {
                TicketId = loginTicket.Id,
                Message = "Investigating SSO configuration. Will update shortly.",
                CreatedBy = bob.Id,
                CreatedAt = now.AddDays(-4),
            },
            new Comment
            {
                TicketId = loginTicket.Id,
                Message = "Found misconfigured SAML endpoint. Applying fix.",
                CreatedBy = bob.Id,
                CreatedAt = now.AddDays(-3),
            },
            new Comment
            {
                TicketId = printerTicket.Id,
                Message = "Checked printer network cable. Replaced faulty cable.",
                CreatedBy = carol.Id,
                CreatedAt = now.AddDays(-2),
            },
            new Comment
            {
                TicketId = printerTicket.Id,
                Message = "Printer back online. Monitoring for 24 hours.",
                CreatedBy = carol.Id,
                CreatedAt = now.AddDays(-1),
            },
            new Comment
            {
                TicketId = licenseTicket.Id,
                Message = "License request submitted to procurement.",
                CreatedBy = bob.Id,
                CreatedAt = now.AddDays(-8),
            },
            new Comment
            {
                TicketId = licenseTicket.Id,
                Message = "License approved and installed. Closing ticket.",
                CreatedBy = bob.Id,
                CreatedAt = now.AddDays(-2),
            },
            new Comment
            {
                TicketId = emailTicket.Id,
                Message = "User resolved issue independently. Cancelling ticket.",
                CreatedBy = dave.Id,
                CreatedAt = now.AddDays(-4),
            });

        await db.SaveChangesAsync();
    }
}
