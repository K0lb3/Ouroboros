namespace SRPG
{
    using System;

    public abstract class BaseObject
    {
        private bool mInitialized;
        private bool mPaused;

        protected BaseObject()
        {
            base..ctor();
            return;
        }

        public virtual bool Load()
        {
            return 1;
        }

        public virtual void Release()
        {
        }

        public virtual void Update()
        {
        }

        public bool IsInitialized
        {
            get
            {
                return this.mInitialized;
            }
            set
            {
                this.mInitialized = value;
                return;
            }
        }

        public bool IsPaused
        {
            get
            {
                return this.mPaused;
            }
            set
            {
                this.mPaused = value;
                return;
            }
        }
    }
}

