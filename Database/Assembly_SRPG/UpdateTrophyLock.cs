namespace SRPG
{
    using System;

    public class UpdateTrophyLock
    {
        private int lock_count;

        public UpdateTrophyLock()
        {
            this.lock_count = 1;
            base..ctor();
            return;
        }

        public void Lock()
        {
            this.lock_count += 1;
            return;
        }

        public void LockClear()
        {
            this.lock_count = 0;
            return;
        }

        public void Unlock()
        {
            if (0 >= this.lock_count)
            {
                goto Label_001A;
            }
            this.lock_count -= 1;
        Label_001A:
            return;
        }

        public bool IsLock
        {
            get
            {
                return (0 < this.lock_count);
            }
        }
    }
}

