namespace SRPG
{
    using System;

    public class StateMachine<T>
    {
        private T mOwner;
        private State<T> mState;

        public StateMachine(T owner)
        {
            base..ctor();
            this.mOwner = owner;
            return;
        }

        public void Command(string cmd)
        {
            if (this.mState == null)
            {
                goto Label_001D;
            }
            this.mState.Command(this.mOwner, cmd);
        Label_001D:
            return;
        }

        public void GotoState<StateType>() where StateType: State<T>, new()
        {
            if (this.mState == null)
            {
                goto Label_001C;
            }
            this.mState.End(this.mOwner);
        Label_001C:
            this.mState = (StateType) Activator.CreateInstance<StateType>();
            this.mState.self = this.mOwner;
            this.mState.Begin(this.mOwner);
            return;
        }

        public void GotoState(Type stateType)
        {
            if (this.mState == null)
            {
                goto Label_001C;
            }
            this.mState.End(this.mOwner);
        Label_001C:
            this.mState = (State<T>) Activator.CreateInstance(stateType);
            this.mState.self = this.mOwner;
            this.mState.Begin(this.mOwner);
            return;
        }

        public bool IsInKindOfState<StateType>() where StateType: State<T>
        {
            return ((this.mState == null) ? 0 : ((this.mState as StateType) > null));
        }

        public bool IsInState<StateType>() where StateType: State<T>
        {
            return ((this.mState == null) ? 0 : (this.mState.GetType() == typeof(StateType)));
        }

        public void Update()
        {
            if (this.mState == null)
            {
                goto Label_001C;
            }
            this.mState.Update(this.mOwner);
        Label_001C:
            return;
        }

        public State<T> State
        {
            get
            {
                return this.mState;
            }
        }

        public string StateName
        {
            get
            {
                return ((this.mState == null) ? "NULL" : this.mState.GetType().Name);
            }
        }

        public Type CurrentState
        {
            get
            {
                return ((this.mState == null) ? null : this.mState.GetType());
            }
        }
    }
}

