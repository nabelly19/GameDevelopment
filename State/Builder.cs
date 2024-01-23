using System;
using System.Collections.Generic;
public class StateBuilder
{
    private Boss boss = null;
    private State initial = null;
    private State last = null;

    public StateBuilder SetContext(Boss enemy)
    {
        this.boss = enemy;
        return this;
    }

    // cadeias de comportamento :))
    public StateBuilder AddState(State state)
    {
        // state.SetContext(this.boss);

        

        if (last is not null)
            last.SetNextState(state);
        
        if (initial == null)
            initial = state;

        last = state;
        return this;
    }

    

    public State Build()
    {
        // last.nextState = initial;
        // this.boss.manager = initial;
        return initial;
    }
}
