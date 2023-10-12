using Fluxor;
using System.ComponentModel;

namespace BlazorClientApp.Features.Counter;

public record CounterState(int CurrentCount);


public class CounterFeature : Feature<CounterState>
{
    public override string GetName() => "Counter";
    protected override CounterState GetInitialState() => new(0);
}

public class CounterIncrementAction {}

public record CounterIncrementByValueAction(int Value);


public static class CounterReducers
{
    [ReducerMethod(typeof(CounterIncrementAction))]
    public static CounterState OnIncrement(CounterState state) => state with
    {
        CurrentCount = state.CurrentCount + 1
    };

    [ReducerMethod]
    public static CounterState OnIncrement(CounterState state, CounterIncrementByValueAction action) => state with
    {
        CurrentCount = state.CurrentCount + action.Value
    };
}

