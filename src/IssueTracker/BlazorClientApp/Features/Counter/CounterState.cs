using Fluxor;

namespace BlazorClientApp.Features.Counter;

public record CounterState(int CurrentCount);


public class CounterFeature : Feature<CounterState>
{
    public override string GetName() => "Counter";
    protected override CounterState GetInitialState() => new(0);
}

public class CounterIncrementAction {}


public static class CounterReducers
{
    [ReducerMethod(typeof(CounterIncrementAction))]
    public static CounterState OnIncrement(CounterState state) => state with
    {
        CurrentCount = state.CurrentCount + 1
    };
}
