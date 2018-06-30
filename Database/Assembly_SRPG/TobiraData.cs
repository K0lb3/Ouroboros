namespace SRPG
{
    using GR;
    using System;
    using System.Text;

    public class TobiraData
    {
        private int mLv;
        private SkillData mParameterBuffSkill;
        private TobiraParam mTobiraParam;
        private string mLearnedLeaderSkillIname;

        public TobiraData()
        {
            base..ctor();
            return;
        }

        public bool Setup(string unit_iname, TobiraParam.Category category, int lv)
        {
            this.mLv = lv;
            this.mTobiraParam = MonoSingleton<GameManager>.Instance.MasterParam.GetTobiraParam(unit_iname, category);
            if (this.mTobiraParam != null)
            {
                goto Label_002B;
            }
            return 0;
        Label_002B:
            this.mParameterBuffSkill = TobiraUtility.CreateParameterBuffSkill(this.mTobiraParam, this.mLv);
            if (this.mTobiraParam.HasLeaerSkill == null)
            {
                goto Label_0074;
            }
            if (lv < this.mTobiraParam.OverwriteLeaderSkillLevel)
            {
                goto Label_0074;
            }
            this.mLearnedLeaderSkillIname = this.mTobiraParam.OverwriteLeaderSkillIname;
        Label_0074:
            if (this.mParameterBuffSkill != null)
            {
                goto Label_0081;
            }
            return 0;
        Label_0081:
            return 1;
        }

        public Json_Tobira ToJson()
        {
            Json_Tobira tobira;
            tobira = new Json_Tobira();
            tobira.category = this.Param.TobiraCategory;
            tobira.lv = this.Lv;
            return tobira;
        }

        public string ToJsonString()
        {
            StringBuilder builder;
            builder = new StringBuilder(0x200);
            builder.Append("{\"lv\":");
            builder.Append(this.Lv);
            builder.Append(",");
            builder.Append("\"category\":");
            builder.Append(this.Param.TobiraCategory);
            builder.Append("}");
            return builder.ToString();
        }

        public int Lv
        {
            get
            {
                return this.mLv;
            }
            set
            {
                this.mLv = value;
                return;
            }
        }

        public int ViewLv
        {
            get
            {
                return (this.mLv - 1);
            }
        }

        public SkillData ParameterBuffSkill
        {
            get
            {
                return this.mParameterBuffSkill;
            }
        }

        public string LearnedLeaderSkillIname
        {
            get
            {
                return this.mLearnedLeaderSkillIname;
            }
        }

        public bool IsUnlocked
        {
            get
            {
                return (this.mLv > 0);
            }
        }

        public TobiraParam Param
        {
            get
            {
                return this.mTobiraParam;
            }
        }

        public bool IsLearnedLeaderSkill
        {
            get
            {
                return (string.IsNullOrEmpty(this.mLearnedLeaderSkillIname) == 0);
            }
        }

        public bool IsMaxLv
        {
            get
            {
                return ((this.Lv < MonoSingleton<GameManager>.Instance.MasterParam.FixParam.TobiraLvCap) == 0);
            }
        }
    }
}

