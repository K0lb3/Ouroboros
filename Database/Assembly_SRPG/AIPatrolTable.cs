namespace SRPG
{
    using System;

    [Serializable]
    public class AIPatrolTable
    {
        public AIPatrolPoint[] routes;
        public int looped;
        public int keeped;

        public AIPatrolTable()
        {
            base..ctor();
            return;
        }

        public void Clear()
        {
            this.routes = null;
            this.looped = 0;
            this.keeped = 0;
            return;
        }

        public void CopyTo(AIPatrolTable dst)
        {
            int num;
            dst.routes = null;
            dst.looped = 0;
            dst.keeped = 0;
            if (this.routes == null)
            {
                goto Label_002D;
            }
            if (((int) this.routes.Length) != null)
            {
                goto Label_002E;
            }
        Label_002D:
            return;
        Label_002E:
            dst.routes = new AIPatrolPoint[(int) this.routes.Length];
            num = 0;
            goto Label_006E;
        Label_0048:
            dst.routes[num] = new AIPatrolPoint();
            this.routes[num].CopyTo(dst.routes[num]);
            num += 1;
        Label_006E:
            if (num < ((int) this.routes.Length))
            {
                goto Label_0048;
            }
            dst.looped = this.looped;
            dst.keeped = this.keeped;
            return;
        }
    }
}

