namespace SRPG
{
    using System;
    using System.Text;

    public abstract class BattleLog
    {
        protected BattleLog()
        {
            base..ctor();
            return;
        }

        public virtual void Deserialize(string log)
        {
        }

        public virtual void Serialize(StringBuilder dst)
        {
        }
    }
}

