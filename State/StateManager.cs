using System;
using System.Collections.Generic;

public class StateManager
{
    public State Current { get; set; } = null;
    public List<State> initialStateList = new();
    private Boss boss = null;

    public StateManager(Boss boss)
    {
        this.boss = boss;
    }

    public void AddList(State state)
    {
        this.initialStateList.Add(state);
        state.SetContext(this);
    }

    public void AddContext(params State[] states)
    {   
        foreach (var state in states)
            state.SetContext(this);
    }

    public void Act()
    {
        Current ??= initialStateList[Random.Shared.Next(initialStateList.Count)];
        Current.Act(boss);
    }

    public void Randomize() { }

    public void Damage01_Boss01() { }
}