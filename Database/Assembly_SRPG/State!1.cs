namespace SRPG
{
    using System;

    public class State<T>
    {
        public T self;

        public State()
        {
            base..ctor();
            return;
        }

        public virtual void Begin(T self)
        {
        }

        public virtual void Command(T self, string cmd)
        {
        }

        public virtual void End(T self)
        {
        }

        public virtual void Update(T self)
        {
        }
    }
}

