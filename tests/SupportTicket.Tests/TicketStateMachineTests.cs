using SupportTicket.Core.Enums;
using SupportTicket.Core.Services;

namespace SupportTicket.Tests;

public class TicketStateMachineTests
{
    public static TheoryData<string, string> ValidTransitions => new()
    {
        { TicketStatus.Open, TicketStatus.InProgress },
        { TicketStatus.Open, TicketStatus.Cancelled },
        { TicketStatus.InProgress, TicketStatus.Resolved },
        { TicketStatus.InProgress, TicketStatus.Cancelled },
        { TicketStatus.Resolved, TicketStatus.Closed },
    };

    public static TheoryData<string, string> InvalidTransitions => new()
    {
        { TicketStatus.Open, TicketStatus.Resolved },
        { TicketStatus.Open, TicketStatus.Closed },
        { TicketStatus.InProgress, TicketStatus.Open },
        { TicketStatus.InProgress, TicketStatus.Closed },
        { TicketStatus.Resolved, TicketStatus.Open },
        { TicketStatus.Resolved, TicketStatus.InProgress },
        { TicketStatus.Resolved, TicketStatus.Cancelled },
        { TicketStatus.Closed, TicketStatus.Open },
        { TicketStatus.Closed, TicketStatus.InProgress },
        { TicketStatus.Closed, TicketStatus.Resolved },
        { TicketStatus.Closed, TicketStatus.Cancelled },
        { TicketStatus.Cancelled, TicketStatus.Open },
        { TicketStatus.Cancelled, TicketStatus.InProgress },
        { TicketStatus.Cancelled, TicketStatus.Resolved },
        { TicketStatus.Cancelled, TicketStatus.Closed },
    };

    [Theory]
    [MemberData(nameof(ValidTransitions))]
    public void ValidTransition_ShouldSucceed(string from, string to)
    {
        Assert.True(TicketStateMachine.IsValidTransition(from, to));
        var result = TicketStateMachine.ValidateTransition(from, to);
        Assert.True(result.Valid);
    }

    [Theory]
    [MemberData(nameof(InvalidTransitions))]
    public void InvalidTransition_ShouldBeRejected(string from, string to)
    {
        Assert.False(TicketStateMachine.IsValidTransition(from, to));
        var result = TicketStateMachine.ValidateTransition(from, to);
        Assert.False(result.Valid);
        Assert.Contains("Invalid status transition", result.Message);
        Assert.Contains(from, result.Message);
        Assert.Contains(to, result.Message);
    }

    [Theory]
    [InlineData(TicketStatus.Open)]
    [InlineData(TicketStatus.InProgress)]
    [InlineData(TicketStatus.Resolved)]
    [InlineData(TicketStatus.Closed)]
    [InlineData(TicketStatus.Cancelled)]
    public void SameStatus_ShouldBeAllowed(string status)
    {
        Assert.True(TicketStateMachine.IsValidTransition(status, status));
    }

    [Fact]
    public void Closed_ShouldHaveNoAllowedTransitions()
    {
        Assert.Empty(TicketStateMachine.GetAllowedTransitions(TicketStatus.Closed));
    }

    [Fact]
    public void Cancelled_ShouldHaveNoAllowedTransitions()
    {
        Assert.Empty(TicketStateMachine.GetAllowedTransitions(TicketStatus.Cancelled));
    }

    [Fact]
    public void Open_ShouldAllowInProgressAndCancelled()
    {
        var allowed = TicketStateMachine.GetAllowedTransitions(TicketStatus.Open);
        Assert.Equal([TicketStatus.InProgress, TicketStatus.Cancelled], allowed);
    }
}
