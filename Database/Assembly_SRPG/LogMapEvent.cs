namespace SRPG
{
    using System;

    public class LogMapEvent : BattleLog
    {
        public Unit self;
        public Unit target;
        public EEventType type;
        public EEventGimmick gimmick;
        public int heal;
        public BuffBit buff;
        public BuffBit debuff;

        public LogMapEvent()
        {
            this.buff = new BuffBit();
            this.debuff = new BuffBit();
            base..ctor();
            return;
        }

        public bool IsBuffEffect()
        {
            int num;
            int num2;
            num = 0;
            goto Label_001F;
        Label_0007:
            if (this.buff.bits[num] == null)
            {
                goto Label_001B;
            }
            return 1;
        Label_001B:
            num += 1;
        Label_001F:
            if (num < ((int) this.buff.bits.Length))
            {
                goto Label_0007;
            }
            num2 = 0;
            goto Label_0051;
        Label_0039:
            if (this.debuff.bits[num2] == null)
            {
                goto Label_004D;
            }
            return 1;
        Label_004D:
            num2 += 1;
        Label_0051:
            if (num2 < ((int) this.debuff.bits.Length))
            {
                goto Label_0039;
            }
            return 0;
        }
    }
}

