namespace FrigidRogue.MonoGame.Core.Components
{
    public class StateMachine<T>
    {
        private readonly T _owner;

        public StateMachine(T owner)
        {
            _owner = owner;
            CurrentState = null;
            PreviousState = null;
        }

        public State<T> CurrentState { get; private set; }
        public State<T> PreviousState { get; private set; }

        public void SetCurrentState(State<T> currentState)
        {
            CurrentState = currentState;
        }

        public void SetPreviousState(State<T> previousState)
        {
            PreviousState = previousState;
        }

        public void Update()
        {
            CurrentState?.Execute(_owner);
        }

        public void ChangeState(State<T> newState)
        {
            if (PreviousState != null)
                PreviousState = CurrentState;

            CurrentState?.Exit(_owner);

            CurrentState = newState;
            CurrentState.Enter(_owner);
        }

        public string GetCurrentStateTypeName()
        {
            return CurrentState.GetType().Name;
        }
    }
}