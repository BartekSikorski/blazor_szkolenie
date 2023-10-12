using BlazorClientApp.Services;
using Fluxor;

namespace BlazorClientApp.Features.Issue;

public record IssueState(bool IsLoading, IEnumerable<Domain.Models.Issue> Issues);


public class IssueFeature : Feature<IssueState>
{
    public override string GetName() => "Issue";
    protected override IssueState GetInitialState() => new(false, Array.Empty<Domain.Models.Issue>());
}


public record GetAllIssuesAction { }

public record GetAllIssuesResultAction(IEnumerable<Domain.Models.Issue> Issues);

public static class IssueReducers
{
    [ReducerMethod(typeof(GetAllIssuesAction))]
    public static IssueState OnGetAllIssues(IssueState state) => new IssueState(true, Array.Empty<Domain.Models.Issue>());

    [ReducerMethod]
    public static IssueState OnGetAllResultIssues(IssueState state, GetAllIssuesResultAction action) => new IssueState(false, action.Issues);
}


public class IssueEffects
{
    private readonly IssueApiService Api;

    public IssueEffects(IssueApiService api)
    {
        Api = api;
    }

    [EffectMethod]
    public async Task HandleGetAllIssuesAction(GetAllIssuesAction action, IDispatcher dispatcher)
    {
        var issues = await Api.GetAllAsync();

        if (issues is not null)
        {
            dispatcher.Dispatch(new GetAllIssuesResultAction(issues));
        }
    }
}

