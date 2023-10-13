using Microsoft.AspNetCore.Authorization;
using System.Net;
using System.Security.Claims;

namespace BlazorClientApp.Authentication;

public static class Policies
{
    public static AuthorizationPolicy IsAdultPolicy()
    {
        return new AuthorizationPolicyBuilder()
            .RequireAuthenticatedUser()
            .RequireClaim(ClaimTypes.DateOfBirth)
            .AddRequirements(new MinimumAgeRequirement(18), new TrainerRequirement())            
            .Build();        
    }

    
}

public record MinimumAgeRequirement(byte age) : IAuthorizationRequirement;

public record TrainerRequirement() : IAuthorizationRequirement;

public class TrainerRequirementAuthorizationHandler : AuthorizationHandler<TrainerRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, TrainerRequirement requirement)
    {
        context.Succeed(requirement);

        return Task.CompletedTask;
    }
}

public class MinimumAgeAuthorizationHandler : AuthorizationHandler<MinimumAgeRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumAgeRequirement requirement)
    {
        if (!context.User.HasClaim(p => p.Type == ClaimTypes.DateOfBirth))
        {
            context.Fail();
            return Task.CompletedTask;
        }

        var dateOfBirthString = context.User.FindFirst(c => c.Type == ClaimTypes.DateOfBirth).Value;

        var dateOfBirth = DateTime.Parse(dateOfBirthString);

        var age = DateTime.Today.Year - dateOfBirth.Year;

        if (age >= requirement.age)
        {
            context.Succeed(requirement);
        }
        else
            context.Fail();

        return Task.CompletedTask;


    }
}
