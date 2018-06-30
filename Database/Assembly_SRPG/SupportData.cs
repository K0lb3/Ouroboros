namespace SRPG
{
    using GR;
    using System;

    public class SupportData
    {
        public UnitData Unit;
        public string FUID;
        public string PlayerName;
        public int PlayerLevel;
        public string UnitID;
        public int UnitLevel;
        public int UnitRarity;
        public string JobID;
        public int LeaderSkillLevel;
        public int Cost;
        public int mIsFriend;

        public SupportData()
        {
            base..ctor();
            return;
        }

        public void Deserialize(Json_Support json)
        {
            Json_Unit unit;
            int num;
            UnitData data;
            this.FUID = json.fuid;
            this.PlayerName = json.name;
            this.PlayerLevel = json.lv;
            this.Cost = json.cost;
            if (json.unit == null)
            {
                goto Label_00F1;
            }
            unit = json.unit;
            this.UnitID = unit.iname;
            this.UnitLevel = unit.lv;
            this.UnitRarity = unit.rare;
            if (unit.select == null)
            {
                goto Label_00C6;
            }
            this.JobID = null;
            num = 0;
            goto Label_00B8;
        Label_007F:
            if (unit.jobs[num].iid != unit.select.job)
            {
                goto Label_00B4;
            }
            this.JobID = unit.jobs[num].iname;
            goto Label_00C6;
        Label_00B4:
            num += 1;
        Label_00B8:
            if (num < ((int) unit.jobs.Length))
            {
                goto Label_007F;
            }
        Label_00C6:
            this.LeaderSkillLevel = SRPG.UnitParam.GetLeaderSkillLevel(this.UnitRarity, unit.plus);
            data = new UnitData();
            data.Deserialize(unit);
            this.Unit = data;
        Label_00F1:
            this.mIsFriend = json.isFriend;
            return;
        }

        public int GetCost()
        {
            return this.Cost;
        }

        public bool IsFriend()
        {
            return ((this.mIsFriend != 1) ? 0 : 1);
        }

        public SRPG.UnitParam UnitParam
        {
            get
            {
                return ((string.IsNullOrEmpty(this.UnitID) != null) ? null : MonoSingleton<GameManager>.Instance.GetUnitParam(this.UnitID));
            }
        }

        public SkillParam LeaderSkill
        {
            get
            {
                SkillData data;
                if (this.Unit == null)
                {
                    goto Label_0024;
                }
                data = this.Unit.LeaderSkill;
                if (data == null)
                {
                    goto Label_0024;
                }
                return data.SkillParam;
            Label_0024:
                return null;
            }
        }

        public string UnitName
        {
            get
            {
                return this.UnitParam.name;
            }
        }

        public EElement UnitElement
        {
            get
            {
                return this.Unit.Element;
            }
        }

        public string IconPath
        {
            get
            {
                SRPG.UnitParam param;
                param = this.UnitParam;
                if (param != null)
                {
                    goto Label_000F;
                }
                return null;
            Label_000F:
                return AssetPath.UnitSkinIconSmall(param, this.Unit.GetSelectedSkin(-1), this.Unit.CurrentJob.JobID);
            }
        }
    }
}

