namespace SRPG
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    public class LogFall : BattleLog
    {
        public List<Param> mLists;
        public bool mIsPlayDamageMotion;

        public LogFall()
        {
            this.mLists = new List<Param>();
            base..ctor();
            return;
        }

        public unsafe void Add(Unit self, Grid landing)
        {
            Param param;
            param = new Param();
            &param.mSelf = self;
            &param.mLanding = landing;
            this.mLists.Add(param);
            return;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct Param
        {
            public Unit mSelf;
            public Grid mLanding;
        }
    }
}

