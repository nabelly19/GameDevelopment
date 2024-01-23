using System;
using System.Collections.Generic;

public class StateManager
{
    public State Current { get; set; }
    private List<State> initialStateList = new();

    public void AddList(State state)
    {
        this.initialStateList.Add(state);
        state.SetContext(this);
    }
    
    public void Act()
    {
        Current ??= initialStateList[
            Random.Shared.Next(initialStateList.Count)
        ];
        
        Current.Act();
    }

    public void Randomize()
    {

    }
    public void Damage01_Boss01()
    {

    }

}

// //TODO REVER POSICAO DESSE BLOCO AI KK
        // if (last.nextState is null)
        // {
        //     Random rand = new Random();
        //     rand.Next(stateList.Count);
        // }