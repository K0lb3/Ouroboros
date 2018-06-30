namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class JobParam
    {
        public static readonly int MAX_JOB_RANK;
        public string iname;
        public string name;
        public string expr;
        public string model;
        public string ac2d;
        public string modelp;
        public string pet;
        public string buki;
        public string origin;
        public JobTypes type;
        public RoleTypes role;
        public OInt mov;
        public OInt jmp;
        public string wepmdl;
        public string[] atkskill;
        public string artifact;
        public string ai;
        public string master;
        public string fixed_ability;
        public string MapEffectAbility;
        public bool IsMapEffectRevReso;
        public string DescCharacteristic;
        public string DescOther;
        public StatusParam status;
        public OInt avoid;
        public OInt inimp;
        public JobRankParam[] ranks;
        public string unit_image;

        static JobParam()
        {
            MAX_JOB_RANK = 11;
            return;
        }

        public JobParam()
        {
            this.atkskill = new string[7];
            this.status = new StatusParam();
            this.avoid = 0;
            this.inimp = 0;
            this.ranks = new JobRankParam[MAX_JOB_RANK + 1];
            base..ctor();
            return;
        }

        private unsafe void CreateBuffList(MasterParam master_param)
        {
            int num;
            List<BuffEffect.BuffValues> list;
            int num2;
            ItemParam param;
            SkillData data;
            int num3;
            num = 0;
            goto Label_0142;
        Label_0007:
            if (this.ranks[num] != null)
            {
                goto Label_0019;
            }
            goto Label_013E;
        Label_0019:
            list = new List<BuffEffect.BuffValues>();
            if (this.ranks[num].equips != null)
            {
                goto Label_0044;
            }
            if (num != ((int) this.ranks.Length))
            {
                goto Label_0044;
            }
            goto Label_013E;
        Label_0044:
            num2 = 0;
            goto Label_00C9;
        Label_004B:
            if (string.IsNullOrEmpty(this.ranks[num].equips[num2]) == null)
            {
                goto Label_0069;
            }
            goto Label_00C5;
        Label_0069:
            param = master_param.GetItemParam(this.ranks[num].equips[num2]);
            if (param == null)
            {
                goto Label_00C5;
            }
            if (string.IsNullOrEmpty(param.skill) == null)
            {
                goto Label_009A;
            }
            goto Label_00C5;
        Label_009A:
            data = new SkillData();
            data.Setup(param.skill, 1, 1, master_param);
            data.BuffSkill(1, 0, null, null, null, null, null, null, null, 0, 0, list);
        Label_00C5:
            num2 += 1;
        Label_00C9:
            if (num2 < ((int) this.ranks[num].equips.Length))
            {
                goto Label_004B;
            }
            if (list.Count <= 0)
            {
                goto Label_013E;
            }
            this.ranks[num].buff_list = new BuffEffect.BuffValues[list.Count];
            num3 = 0;
            goto Label_0131;
        Label_010A:
            *(&(this.ranks[num].buff_list[num3])) = list[num3];
            num3 += 1;
        Label_0131:
            if (num3 < list.Count)
            {
                goto Label_010A;
            }
        Label_013E:
            num += 1;
        Label_0142:
            if (num < ((int) this.ranks.Length))
            {
                goto Label_0007;
            }
            return;
        }

        public bool Deserialize(JSON_JobParam json, MasterParam master_param)
        {
            int num;
            if (json != null)
            {
                goto Label_0008;
            }
            return 0;
        Label_0008:
            this.iname = json.iname;
            this.name = json.name;
            this.expr = json.expr;
            this.model = json.mdl;
            this.ac2d = json.ac2d;
            this.modelp = json.mdlp;
            this.pet = json.pet;
            this.buki = json.buki;
            this.origin = json.origin;
            this.type = json.type;
            this.role = json.role;
            this.wepmdl = json.wepmdl;
            this.mov = json.jmov;
            this.jmp = json.jjmp;
            this.atkskill[0] = (string.IsNullOrEmpty(json.atkskl) != null) ? string.Empty : json.atkskl;
            this.atkskill[1] = (string.IsNullOrEmpty(json.atkfi) != null) ? string.Empty : json.atkfi;
            this.atkskill[2] = (string.IsNullOrEmpty(json.atkwa) != null) ? string.Empty : json.atkwa;
            this.atkskill[3] = (string.IsNullOrEmpty(json.atkwi) != null) ? string.Empty : json.atkwi;
            this.atkskill[4] = (string.IsNullOrEmpty(json.atkth) != null) ? string.Empty : json.atkth;
            this.atkskill[5] = (string.IsNullOrEmpty(json.atksh) != null) ? string.Empty : json.atksh;
            this.atkskill[6] = (string.IsNullOrEmpty(json.atkda) != null) ? string.Empty : json.atkda;
            this.fixed_ability = json.fixabl;
            this.artifact = json.artifact;
            this.ai = json.ai;
            this.master = json.master;
            this.MapEffectAbility = json.me_abl;
            this.IsMapEffectRevReso = (json.is_me_rr == 0) == 0;
            this.DescCharacteristic = json.desc_ch;
            this.DescOther = json.desc_ot;
            this.status.hp = json.hp;
            this.status.mp = json.mp;
            this.status.atk = json.atk;
            this.status.def = json.def;
            this.status.mag = json.mag;
            this.status.mnd = json.mnd;
            this.status.dex = json.dex;
            this.status.spd = json.spd;
            this.status.cri = json.cri;
            this.status.luk = json.luk;
            this.avoid = json.avoid;
            this.inimp = json.inimp;
            Array.Clear(this.ranks, 0, (int) this.ranks.Length);
            if (json.ranks == null)
            {
                goto Label_0397;
            }
            num = 0;
            goto Label_0389;
        Label_035C:
            this.ranks[num] = new JobRankParam();
            if (this.ranks[num].Deserialize(json.ranks[num]) != null)
            {
                goto Label_0385;
            }
            return 0;
        Label_0385:
            num += 1;
        Label_0389:
            if (num < ((int) json.ranks.Length))
            {
                goto Label_035C;
            }
        Label_0397:
            if (master_param == null)
            {
                goto Label_03A4;
            }
            this.CreateBuffList(master_param);
        Label_03A4:
            this.unit_image = json.unit_image;
            return 1;
        }

        public int FindRankOfAbility(string abilityID)
        {
            int num;
            <FindRankOfAbility>c__AnonStorey2C0 storeyc;
            storeyc = new <FindRankOfAbility>c__AnonStorey2C0();
            storeyc.abilityID = abilityID;
            if (this.ranks == null)
            {
                goto Label_006E;
            }
            num = 0;
            goto Label_0060;
        Label_001F:
            if (this.ranks[num].learnings != null)
            {
                goto Label_0036;
            }
            goto Label_005C;
        Label_0036:
            if (Array.FindIndex<OString>(this.ranks[num].learnings, new Predicate<OString>(storeyc.<>m__236)) == -1)
            {
                goto Label_005C;
            }
            return num;
        Label_005C:
            num += 1;
        Label_0060:
            if (num < ((int) this.ranks.Length))
            {
                goto Label_001F;
            }
        Label_006E:
            return -1;
        }

        public int GetJobChangeCost(int lv)
        {
            if (this.ranks == null)
            {
                goto Label_0020;
            }
            if (lv < 0)
            {
                goto Label_0020;
            }
            if (lv < ((int) this.ranks.Length))
            {
                goto Label_0022;
            }
        Label_0020:
            return 0;
        Label_0022:
            return this.ranks[lv].JobChangeCost;
        }

        public int[] GetJobChangeItemNums(int lv)
        {
            if (this.ranks == null)
            {
                goto Label_0020;
            }
            if (lv < 0)
            {
                goto Label_0020;
            }
            if (lv < ((int) this.ranks.Length))
            {
                goto Label_0022;
            }
        Label_0020:
            return null;
        Label_0022:
            return this.ranks[lv].JobChangeItemNums;
        }

        public string[] GetJobChangeItems(int lv)
        {
            if (this.ranks == null)
            {
                goto Label_0020;
            }
            if (lv < 0)
            {
                goto Label_0020;
            }
            if (lv < ((int) this.ranks.Length))
            {
                goto Label_0022;
            }
        Label_0020:
            return null;
        Label_0022:
            return this.ranks[lv].JobChangeItems;
        }

        public int GetJobRankAvoidRate(int lv)
        {
            if (this.ranks == null)
            {
                goto Label_0020;
            }
            if (lv < 0)
            {
                goto Label_0020;
            }
            if (lv < ((int) this.ranks.Length))
            {
                goto Label_0022;
            }
        Label_0020:
            return 0;
        Label_0022:
            return this.avoid;
        }

        public static int GetJobRankCap(int unitRarity)
        {
            RarityParam param;
            param = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.GetRarityParam(unitRarity);
            return ((param == null) ? 1 : param.UnitJobLvCap);
        }

        public int GetJobRankInitJewelRate(int lv)
        {
            if (this.ranks == null)
            {
                goto Label_0020;
            }
            if (lv < 0)
            {
                goto Label_0020;
            }
            if (lv < ((int) this.ranks.Length))
            {
                goto Label_0022;
            }
        Label_0020:
            return 0;
        Label_0022:
            return this.inimp;
        }

        public StatusParam GetJobRankStatus(int lv)
        {
            if (this.ranks == null)
            {
                goto Label_0020;
            }
            if (lv < 0)
            {
                goto Label_0020;
            }
            if (lv < ((int) this.ranks.Length))
            {
                goto Label_0022;
            }
        Label_0020:
            return null;
        Label_0022:
            return this.status;
        }

        public unsafe BaseStatus GetJobTransfarStatus(int lv, EElement element)
        {
            BaseStatus status;
            int num;
            BuffEffect.BuffValues values;
            BuffEffect.BuffValues[] valuesArray;
            int num2;
            bool flag;
            ParamTypes types;
            if (this.ranks == null)
            {
                goto Label_0020;
            }
            if (lv < 0)
            {
                goto Label_0020;
            }
            if (lv < ((int) this.ranks.Length))
            {
                goto Label_0022;
            }
        Label_0020:
            return null;
        Label_0022:
            status = new BaseStatus();
            num = 0;
            goto Label_012B;
        Label_002F:
            if (this.ranks[num].buff_list == null)
            {
                goto Label_0127;
            }
            valuesArray = this.ranks[num].buff_list;
            num2 = 0;
            goto Label_011D;
        Label_0057:
            values = *(&(valuesArray[num2]));
            flag = 0;
            switch ((&values.param_type - 0x8f))
            {
                case 0:
                    goto Label_009B;

                case 1:
                    goto Label_00A9;

                case 2:
                    goto Label_00B7;

                case 3:
                    goto Label_00C5;

                case 4:
                    goto Label_00D3;

                case 5:
                    goto Label_00E1;
            }
            goto Label_00EF;
        Label_009B:
            flag = (element == 1) == 0;
            goto Label_00EF;
        Label_00A9:
            flag = (element == 2) == 0;
            goto Label_00EF;
        Label_00B7:
            flag = (element == 3) == 0;
            goto Label_00EF;
        Label_00C5:
            flag = (element == 4) == 0;
            goto Label_00EF;
        Label_00D3:
            flag = (element == 5) == 0;
            goto Label_00EF;
        Label_00E1:
            flag = (element == 6) == 0;
        Label_00EF:
            if (flag == null)
            {
                goto Label_00FB;
            }
            goto Label_0117;
        Label_00FB:
            BuffEffect.SetBuffValues(&values.param_type, &values.method_type, &status, &values.value);
        Label_0117:
            num2 += 1;
        Label_011D:
            if (num2 < ((int) valuesArray.Length))
            {
                goto Label_0057;
            }
        Label_0127:
            num += 1;
        Label_012B:
            if (num < lv)
            {
                goto Label_002F;
            }
            return status;
        }

        public OString[] GetLearningAbilitys(int lv)
        {
            if (this.ranks == null)
            {
                goto Label_0020;
            }
            if (lv < 0)
            {
                goto Label_0020;
            }
            if (lv < ((int) this.ranks.Length))
            {
                goto Label_0022;
            }
        Label_0020:
            return null;
        Label_0022:
            return this.ranks[lv].learnings;
        }

        public int GetRankupCost(int lv)
        {
            if (this.ranks == null)
            {
                goto Label_0020;
            }
            if (lv < 0)
            {
                goto Label_0020;
            }
            if (lv < ((int) this.ranks.Length))
            {
                goto Label_0022;
            }
        Label_0020:
            return 0;
        Label_0022:
            return this.ranks[lv].cost;
        }

        public string GetRankupItemID(int lv, int index)
        {
            string[] strArray;
            strArray = this.GetRankupItems(lv);
            if (strArray == null)
            {
                goto Label_0022;
            }
            if (0 > index)
            {
                goto Label_0022;
            }
            if (index >= ((int) strArray.Length))
            {
                goto Label_0022;
            }
            return strArray[index];
        Label_0022:
            return null;
        }

        public string[] GetRankupItems(int lv)
        {
            if (this.ranks == null)
            {
                goto Label_0020;
            }
            if (lv < 0)
            {
                goto Label_0020;
            }
            if (lv < ((int) this.ranks.Length))
            {
                goto Label_0022;
            }
        Label_0020:
            return null;
        Label_0022:
            return this.ranks[lv].equips;
        }

        [CompilerGenerated]
        private sealed class <FindRankOfAbility>c__AnonStorey2C0
        {
            internal string abilityID;

            public <FindRankOfAbility>c__AnonStorey2C0()
            {
                base..ctor();
                return;
            }

            internal bool <>m__236(OString p)
            {
                return (p == this.abilityID);
            }
        }
    }
}

