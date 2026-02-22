using Xunit;
using CustomerEngagement.Application.Validators;
using CustomerEngagement.Domain.Enums;
using CustomerEngagement.Domain.Exceptions;

namespace CustomerEngagement.Tests;

public class TicketWorkflowValidatorTests
{
    [Fact]
    public void Open_To_InProgress_Should_Pass()
    {
        // Act
        var exception = Record.Exception(() =>
            TicketWorkflowValidator.ValidateTransition(
                TicketStatus.Open,
                TicketStatus.InProgress));

        // Assert
        Assert.Null(exception);
    }

    [Fact]
    public void InProgress_To_Resolved_Should_Pass()
    {
        var exception = Record.Exception(() =>
            TicketWorkflowValidator.ValidateTransition(
                TicketStatus.InProgress,
                TicketStatus.Resolved));

        Assert.Null(exception);
    }

    [Fact]
    public void Resolved_To_Closed_Should_Pass()
    {
        var exception = Record.Exception(() =>
            TicketWorkflowValidator.ValidateTransition(
                TicketStatus.Resolved,
                TicketStatus.Closed));

        Assert.Null(exception);
    }

    [Fact]
    public void Open_To_Resolved_Should_Throw_Exception()
    {
        Assert.Throws<InvalidTicketStatusTransitionException>(() =>
            TicketWorkflowValidator.ValidateTransition(
                TicketStatus.Open,
                TicketStatus.Resolved));
    }

    [Fact]
    public void Resolved_To_Open_Should_Throw_Exception()
    {
        Assert.Throws<InvalidTicketStatusTransitionException>(() =>
            TicketWorkflowValidator.ValidateTransition(
                TicketStatus.Resolved,
                TicketStatus.Open));
    }

    [Fact]
    public void Open_To_Open_Should_Throw_Exception()
    {
        Assert.Throws<InvalidTicketStatusTransitionException>(() =>
            TicketWorkflowValidator.ValidateTransition(
                TicketStatus.Open,
                TicketStatus.Open));
    }

    [Fact]
    public void Closed_To_Open_Should_Throw_Exception()
    {
        Assert.Throws<InvalidTicketStatusTransitionException>(() =>
            TicketWorkflowValidator.ValidateTransition(
                TicketStatus.Closed,
                TicketStatus.Open));
    }

    [Fact]
    public void Closed_To_InProgress_Should_Throw_Exception()
    {
        Assert.Throws<InvalidTicketStatusTransitionException>(() =>
            TicketWorkflowValidator.ValidateTransition(
                TicketStatus.Closed,
                TicketStatus.InProgress));
    }
}