namespace SRPG
{
    using GR;
    using System;
    using System.Runtime.InteropServices;

    public class ArtifactParam
    {
        private const string ARTIFACT_EXPR_PREFIX = "_EXPR";
        private const string ARTIFACT_FLAVOR_PREFIX = "_FLAVOR";
        public string iname;
        public string name;
        public string spec;
        public string asset;
        public string voice;
        public string icon;
        public ArtifactTypes type;
        public int rareini;
        public int raremax;
        public string kakera;
        public bool is_create;
        public int maxnum;
        public string skill;
        public int kcoin;
        public int tcoin;
        public int acoin;
        public int mcoin;
        public int pcoin;
        public int buy;
        public int sell;
        public int enhance_cost;
        public string[] skills;
        public string[] equip_effects;
        public string[] attack_effects;
        public string[] abil_inames;
        public string[] abil_conds;
        public int[] abil_levels;
        public int[] abil_rareties;
        public int[] abil_shows;
        public string tag;
        public int condition_lv;
        public string[] condition_units;
        public string[] condition_jobs;
        public string condition_birth;
        public ESex condition_sex;
        public EElement condition_element;
        public OInt condition_raremin;
        public OInt condition_raremax;
        public int CachedKakeraCount;

        public ArtifactParam()
        {
            this.equip_effects = new string[RarityParam.MAX_RARITY];
            this.attack_effects = new string[RarityParam.MAX_RARITY];
            base..ctor();
            return;
        }

        public bool CheckEnableEquip(UnitData self, int jobIndex)
        {
            JobParam param;
            JobParam param2;
            JobParam param3;
            JobParam param4;
            JobParam param5;
            ArtifactParam param6;
            if ((this.condition_units == null) || (Array.IndexOf<string>(this.condition_units, self.UnitParam.iname) >= 0))
            {
                goto Label_0029;
            }
            return 0;
        Label_0029:
            if (this.condition_jobs == null)
            {
                goto Label_012E;
            }
            if (self.CurrentJob != null)
            {
                goto Label_0041;
            }
            return 0;
        Label_0041:
            if (jobIndex >= 0)
            {
                goto Label_00B2;
            }
            param = self.CurrentJob.Param;
            if (Array.IndexOf<string>(this.condition_jobs, param.iname) >= 0)
            {
                goto Label_012E;
            }
            if (string.IsNullOrEmpty(param.origin) == null)
            {
                goto Label_007D;
            }
            return 0;
        Label_007D:
            param2 = MonoSingleton<GameManager>.GetInstanceDirect().GetJobParam(param.origin);
            if ((param2 != null) && (Array.IndexOf<string>(this.condition_jobs, param2.iname) >= 0))
            {
                goto Label_012E;
            }
            return 0;
            goto Label_012E;
        Label_00B2:
            if (jobIndex >= ((int) self.Jobs.Length))
            {
                goto Label_012C;
            }
            param3 = self.Jobs[jobIndex].Param;
            if (Array.IndexOf<string>(this.condition_jobs, param3.iname) >= 0)
            {
                goto Label_012E;
            }
            if (string.IsNullOrEmpty(param3.origin) == null)
            {
                goto Label_00F7;
            }
            return 0;
        Label_00F7:
            param4 = MonoSingleton<GameManager>.GetInstanceDirect().GetJobParam(param3.origin);
            if ((param4 != null) && (Array.IndexOf<string>(this.condition_jobs, param4.iname) >= 0))
            {
                goto Label_012E;
            }
            return 0;
            goto Label_012E;
        Label_012C:
            return 0;
        Label_012E:
            if ((string.IsNullOrEmpty(this.condition_birth) != null) || ((self.UnitParam.birth != this.condition_birth) == null))
            {
                goto Label_0160;
            }
            return 0;
        Label_0160:
            if ((this.condition_sex == null) || (self.UnitParam.sex == this.condition_sex))
            {
                goto Label_0183;
            }
            return 0;
        Label_0183:
            if ((this.condition_element == null) || (self.Element == this.condition_element))
            {
                goto Label_01A1;
            }
            return 0;
        Label_01A1:
            if ((this.condition_raremax == null) || (((this.condition_raremin <= self.Rarity) && (this.condition_raremax >= self.Rarity)) && (this.condition_raremax >= this.condition_raremin)))
            {
                goto Label_01FA;
            }
            return 0;
        Label_01FA:
            if ((this.type != 1) || (self.Jobs == null))
            {
                goto Label_0292;
            }
            param5 = null;
            if (jobIndex >= 0)
            {
                goto Label_022D;
            }
            param5 = self.CurrentJob.Param;
            goto Label_0250;
        Label_022D:
            param5 = (jobIndex >= ((int) self.Jobs.Length)) ? null : self.Jobs[jobIndex].Param;
        Label_0250:
            if (string.IsNullOrEmpty(param5.artifact) != null)
            {
                goto Label_0292;
            }
            param6 = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.GetArtifactParam(param5.artifact);
            if ((this.tag != param6.tag) == null)
            {
                goto Label_0292;
            }
            return 0;
        Label_0292:
            return 1;
        }

        public bool Deserialize(JSON_ArtifactParam json)
        {
            int num;
            int num2;
            int num3;
            int num4;
            if (json != null)
            {
                goto Label_0008;
            }
            return 0;
        Label_0008:
            this.iname = json.iname;
            this.name = json.name;
            this.spec = json.spec;
            this.asset = json.asset;
            this.voice = json.voice;
            this.icon = json.icon;
            this.tag = json.tag;
            this.type = json.type;
            this.rareini = json.rini;
            this.raremax = json.rmax;
            this.kakera = json.kakera;
            this.maxnum = json.maxnum;
            this.is_create = json.notsmn == 0;
            this.skills = null;
            if (json.skills == null)
            {
                goto Label_00F5;
            }
            this.skills = new string[(int) json.skills.Length];
            num = 0;
            goto Label_00E7;
        Label_00D3:
            this.skills[num] = json.skills[num];
            num += 1;
        Label_00E7:
            if (num < ((int) json.skills.Length))
            {
                goto Label_00D3;
            }
        Label_00F5:
            Array.Clear(this.equip_effects, 0, (int) this.equip_effects.Length);
            this.equip_effects[0] = json.equip1;
            this.equip_effects[1] = json.equip2;
            this.equip_effects[2] = json.equip3;
            this.equip_effects[3] = json.equip4;
            this.equip_effects[4] = json.equip5;
            Array.Clear(this.attack_effects, 0, (int) this.attack_effects.Length);
            this.attack_effects[0] = json.attack1;
            this.attack_effects[1] = json.attack2;
            this.attack_effects[2] = json.attack3;
            this.attack_effects[3] = json.attack4;
            this.attack_effects[4] = json.attack5;
            this.abil_inames = null;
            this.abil_levels = null;
            this.abil_rareties = null;
            this.abil_shows = null;
            this.abil_conds = null;
            if (json.abils == null)
            {
                goto Label_031F;
            }
            if (json.ablvs == null)
            {
                goto Label_031F;
            }
            if (json.abrares == null)
            {
                goto Label_031F;
            }
            if (json.abshows == null)
            {
                goto Label_031F;
            }
            if (json.abconds == null)
            {
                goto Label_031F;
            }
            if (((int) json.abils.Length) != ((int) json.ablvs.Length))
            {
                goto Label_031F;
            }
            if (((int) json.abils.Length) != ((int) json.abrares.Length))
            {
                goto Label_031F;
            }
            if (((int) json.abils.Length) != ((int) json.abshows.Length))
            {
                goto Label_031F;
            }
            if (((int) json.abils.Length) != ((int) json.abconds.Length))
            {
                goto Label_031F;
            }
            this.abil_inames = new string[(int) json.abils.Length];
            this.abil_levels = new int[(int) json.ablvs.Length];
            this.abil_rareties = new int[(int) json.abrares.Length];
            this.abil_shows = new int[(int) json.abshows.Length];
            this.abil_conds = new string[(int) json.abconds.Length];
            num2 = 0;
            goto Label_0311;
        Label_02BD:
            this.abil_inames[num2] = json.abils[num2];
            this.abil_levels[num2] = json.ablvs[num2];
            this.abil_rareties[num2] = json.abrares[num2];
            this.abil_shows[num2] = json.abshows[num2];
            this.abil_conds[num2] = json.abconds[num2];
            num2 += 1;
        Label_0311:
            if (num2 < ((int) json.ablvs.Length))
            {
                goto Label_02BD;
            }
        Label_031F:
            this.kcoin = json.kc;
            this.tcoin = json.tc;
            this.acoin = json.ac;
            this.mcoin = json.mc;
            this.pcoin = json.pp;
            this.buy = json.buy;
            this.sell = json.sell;
            this.enhance_cost = json.ecost;
            this.condition_lv = json.eqlv;
            this.condition_sex = json.sex;
            this.condition_birth = json.birth;
            this.condition_element = json.elem;
            this.condition_units = null;
            this.condition_jobs = null;
            this.condition_raremin = json.eqrmin;
            this.condition_raremax = json.eqrmax;
            if (json.units == null)
            {
                goto Label_0434;
            }
            if (((int) json.units.Length) <= 0)
            {
                goto Label_0434;
            }
            this.condition_units = new string[(int) json.units.Length];
            num3 = 0;
            goto Label_0426;
        Label_0412:
            this.condition_units[num3] = json.units[num3];
            num3 += 1;
        Label_0426:
            if (num3 < ((int) json.units.Length))
            {
                goto Label_0412;
            }
        Label_0434:
            if (json.jobs == null)
            {
                goto Label_0489;
            }
            if (((int) json.jobs.Length) <= 0)
            {
                goto Label_0489;
            }
            this.condition_jobs = new string[(int) json.jobs.Length];
            num4 = 0;
            goto Label_047B;
        Label_0467:
            this.condition_jobs[num4] = json.jobs[num4];
            num4 += 1;
        Label_047B:
            if (num4 < ((int) json.jobs.Length))
            {
                goto Label_0467;
            }
        Label_0489:
            return 1;
        }

        public int GetBuyNum(ESaleType type)
        {
            ESaleType type2;
            type2 = type;
            switch (type2)
            {
                case 0:
                    goto Label_002D;

                case 1:
                    goto Label_0034;

                case 2:
                    goto Label_0042;

                case 3:
                    goto Label_0049;

                case 4:
                    goto Label_0050;

                case 5:
                    goto Label_0057;

                case 6:
                    goto Label_005E;

                case 7:
                    goto Label_003B;
            }
            goto Label_0060;
        Label_002D:
            return this.buy;
        Label_0034:
            return this.kcoin;
        Label_003B:
            return this.kcoin;
        Label_0042:
            return this.tcoin;
        Label_0049:
            return this.acoin;
        Label_0050:
            return this.pcoin;
        Label_0057:
            return this.mcoin;
        Label_005E:
            return 0;
        Label_0060:
            return 0;
        }

        public string GetText(string table, string key)
        {
            string str;
            str = LocalizedText.Get(table + "." + key);
            return ((str.Equals(key) == null) ? str : string.Empty);
        }

        public string Expr
        {
            get
            {
                return this.GetText("external_artifact", this.iname + "_EXPR");
            }
        }

        public string Flavor
        {
            get
            {
                return this.GetText("external_artifact", this.iname + "_FLAVOR");
            }
        }
    }
}

