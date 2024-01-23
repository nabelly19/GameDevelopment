public abstract class State
{
    private StateManager manager = null;
    public State nextState { get; private set; }

    public void SetContext(StateManager manager)
        => this.manager = manager;
    public void SetNextState (State state)
    {
        this.nextState = state;
        state.SetContext(manager);
    }
    public void GoToNext()
        => this.manager.Current = nextState;
    public abstract void Act();
}
