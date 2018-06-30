namespace SRPG
{
    using System;

    [Serializable]
    public class AIPatrolPoint
    {
        public int x;
        public int y;
        public int length;

        public AIPatrolPoint()
        {
            base..ctor();
            return;
        }

        public void CopyTo(AIPatrolPoint dst)
        {
            dst.x = this.x;
            dst.y = this.y;
            dst.length = this.length;
            return;
        }
    }
}

