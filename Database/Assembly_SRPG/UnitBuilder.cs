namespace SRPG
{
    using GR;
    using System;

    internal class UnitBuilder
    {
        private string m_UnitIname;
        private int m_Exp;
        private int m_Awake;
        private string m_JobIname;
        private int m_Rarity;
        private int m_JobRank;
        private int m_UnlockTobiraNum;

        public UnitBuilder(string unitIname)
        {
            base..ctor();
            this.m_UnitIname = unitIname;
            return;
        }

        public UnitData Build()
        {
            UnitData data;
            UnitParam param;
            data = new UnitData();
            param = MonoSingleton<GameManager>.Instance.GetUnitParam(this.m_UnitIname);
            data.Setup(this.m_UnitIname, this.m_Exp, this.m_Rarity, this.m_Awake, this.m_JobIname, this.m_JobRank, param.element, this.m_UnlockTobiraNum);
            return data;
        }

        public UnitBuilder SetAwake(int value)
        {
            this.m_Awake = value;
            return this;
        }

        public UnitBuilder SetExpByLevel(int level)
        {
            this.m_Exp = MonoSingleton<GameManager>.Instance.MasterParam.GetUnitLevelExp(level);
            return this;
        }

        public UnitBuilder SetJob(string jobIname, int jobRank)
        {
            this.m_JobIname = jobIname;
            this.m_JobRank = jobRank;
            return this;
        }

        public UnitBuilder SetLevelByExp(int exp)
        {
            this.m_Exp = MonoSingleton<GameManager>.Instance.MasterParam.CalcUnitLevel(exp, 250);
            this.m_Exp = exp;
            return this;
        }

        public UnitBuilder SetRarity(int value)
        {
            this.m_Rarity = value;
            return this;
        }

        public UnitBuilder SetUnlockTobiraNum(int value)
        {
            this.m_UnlockTobiraNum = value;
            return this;
        }
    }
}

