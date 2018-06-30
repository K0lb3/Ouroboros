namespace SRPG
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    public class LogDead : BattleLog
    {
        public List<Param> list_normal;
        public List<Param> list_sentence;

        public LogDead()
        {
            this.list_normal = new List<Param>();
            this.list_sentence = new List<Param>();
            base..ctor();
            return;
        }

        public unsafe void Add(Unit unit, DeadTypes type)
        {
            Param param;
            param = new Param();
            &param.self = unit;
            &param.type = type;
            if (type != 2)
            {
                goto Label_0030;
            }
            this.list_sentence.Add(param);
            goto Label_003C;
        Label_0030:
            this.list_normal.Add(param);
        Label_003C:
            return;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct Param
        {
            public Unit self;
            public DeadTypes type;
        }
    }
}

