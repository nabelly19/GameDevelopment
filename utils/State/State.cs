public abstract class State
{
    private StateManager manager;
    public State nextState { get; private set; }
    public bool isChain { get; set; } = false;

    public void SetContext(StateManager manager) => this.manager = manager;

    public void SetNextState(State state)
    {
        this.nextState = state;
        state.SetContext(manager);
    }

    public void GoToNext() => this.manager.Current = nextState;

    public abstract void Act(Boss boss);
}
