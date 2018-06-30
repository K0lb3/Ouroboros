namespace SRPG
{
    using GR;
    using System;

    public class UnitParam
    {
        public static int MASTER_QUEST_RARITY;
        public static int MASTER_QUEST_LV;
        public static string AI_TYPE_DEFAULT;
        public FlagManager flag;
        public string iname;
        public string name;
        public string model;
        public OString birth;
        public int birthID;
        public OString grow;
        public string[] jobsets;
        public string[] tags;
        public string piece;
        public string ability;
        public string ma_quest;
        public ESex sex;
        public byte rare;
        public byte raremax;
        public EUnitType type;
        public EElement element;
        public byte search;
        public short height;
        public short weight;
        public DateTime available_at;
        public string image;
        public string voice;
        public Status ini_status;
        public Status max_status;
        public string[] leader_skills;
        public string[] recipes;
        public NoJobStatus no_job_status;
        public string[] skins;
        private JobSetParam[] mJobSetCache;
        public string unlock_time;
        private static EElement[] WeakElements;
        private static EElement[] ResistElements;

        static UnitParam()
        {
            EElement[] elementArray2;
            EElement[] elementArray1;
            MASTER_QUEST_RARITY = 5;
            MASTER_QUEST_LV = 1;
            AI_TYPE_DEFAULT = "AI_ENEMY";
            elementArray1 = new EElement[7];
            elementArray1[1] = 2;
            elementArray1[2] = 4;
            elementArray1[3] = 1;
            elementArray1[4] = 3;
            elementArray1[5] = 6;
            elementArray1[6] = 5;
            WeakElements = elementArray1;
            elementArray2 = new EElement[7];
            elementArray2[1] = 3;
            elementArray2[2] = 1;
            elementArray2[3] = 4;
            elementArray2[4] = 2;
            ResistElements = elementArray2;
            return;
        }

        public UnitParam()
        {
            this.birth = null;
            this.grow = null;
            this.available_at = DateTime.MinValue;
            this.ini_status = new Status();
            this.max_status = new Status();
            this.leader_skills = new string[RarityParam.MAX_RARITY];
            this.recipes = new string[RarityParam.MAX_RARITY];
            base..ctor();
            return;
        }

        public void CacheReferences(MasterParam master)
        {
            int num;
            if (this.jobsets == null)
            {
                goto Label_0058;
            }
            if (this.mJobSetCache != null)
            {
                goto Label_0058;
            }
            this.mJobSetCache = new JobSetParam[(int) this.jobsets.Length];
            num = 0;
            goto Label_004A;
        Label_0030:
            this.mJobSetCache[num] = master.GetJobSetParam(this.jobsets[num]);
            num += 1;
        Label_004A:
            if (num < ((int) this.jobsets.Length))
            {
                goto Label_0030;
            }
        Label_0058:
            return;
        }

        public static void CalcUnitElementStatus(EElement element, ref BaseStatus status)
        {
            FixParam param;
            int num;
            int num2;
            EElement element2;
            EElement element3;
            ElementParam param2;
            EElement element4;
            OShort @short;
            ElementParam param3;
            param = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.FixParam;
            num = param.ElementResistUpRate;
            num2 = param.ElementResistDownRate;
            element2 = GetWeakElement(element);
            element3 = GetResistElement(element);
            @short = param2[element4];
            (param2 = *(status).element_resist)[element4 = element2] = @short + num2;
            @short = param3[element4];
            (param3 = *(status).element_resist)[element4 = element3] = @short + num;
            return;
        }

        public static void CalcUnitLevelStatus(UnitParam unit, int unitLv, ref BaseStatus status)
        {
            GrowParam param;
            FixParam param2;
            param = MonoSingleton<GameManager>.GetInstanceDirect().GetGrowParam(unit.grow);
            if (param == null)
            {
                goto Label_0040;
            }
            if (param.curve == null)
            {
                goto Label_0040;
            }
            param.CalcLevelCurveStatus(unitLv, status, unit.ini_status, unit.max_status);
            goto Label_007E;
        Label_0040:
            unit.ini_status.param.CopyTo(*(status).param);
            if (unit.ini_status.enchant_resist == null)
            {
                goto Label_007E;
            }
            unit.ini_status.enchant_resist.CopyTo(*(status).enchant_resist);
        Label_007E:
            param2 = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.FixParam;
            *(status).enchant_assist.esa_fire = param2.EsaAssist;
            *(status).enchant_assist.esa_water = param2.EsaAssist;
            *(status).enchant_assist.esa_wind = param2.EsaAssist;
            *(status).enchant_assist.esa_thunder = param2.EsaAssist;
            *(status).enchant_assist.esa_shine = param2.EsaAssist;
            *(status).enchant_assist.esa_dark = param2.EsaAssist;
            *(status).enchant_resist.esa_fire = param2.EsaResist;
            *(status).enchant_resist.esa_water = param2.EsaResist;
            *(status).enchant_resist.esa_wind = param2.EsaResist;
            *(status).enchant_resist.esa_thunder = param2.EsaResist;
            *(status).enchant_resist.esa_shine = param2.EsaResist;
            *(status).enchant_resist.esa_dark = param2.EsaResist;
            *(status).param.rec = param2.IniValRec;
            return;
        }

        public bool CheckAvailable(DateTime now)
        {
            if ((now < this.available_at) == null)
            {
                goto Label_0013;
            }
            return 0;
        Label_0013:
            return 1;
        }

        public bool CheckEnableUnlock()
        {
            int num;
            int num2;
            UnitListWindow.Data data;
            if (string.IsNullOrEmpty(this.piece) != null)
            {
                goto Label_001B;
            }
            if (this.IsSummon() != null)
            {
                goto Label_001D;
            }
        Label_001B:
            return 0;
        Label_001D:
            if (MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueParam(this) == null)
            {
                goto Label_0034;
            }
            return 0;
        Label_0034:
            num = this.GetUnlockNeedPieces();
            if (MonoSingleton<GameManager>.Instance.Player.GetItemAmount(this.piece) >= num)
            {
                goto Label_005A;
            }
            return 0;
        Label_005A:
            if (this.CheckAvailable(TimeManager.ServerTime) != null)
            {
                goto Label_006C;
            }
            return 0;
        Label_006C:
            data = new UnitListWindow.Data(this);
            if (data.unlockable != null)
            {
                goto Label_0080;
            }
            return 0;
        Label_0080:
            return 1;
        }

        public unsafe bool Deserialize(JSON_UnitParam json)
        {
            char[] chArray1;
            int num;
            int num2;
            if (json != null)
            {
                goto Label_0008;
            }
            return 0;
        Label_0008:
            this.iname = json.iname;
            this.name = json.name;
            this.model = json.mdl;
            this.grow = json.grow;
            this.piece = json.piece;
            this.birth = json.birth;
            this.birthID = json.birth_id;
            this.ability = json.ability;
            this.ma_quest = json.ma_quest;
            this.sex = json.sex;
            this.rare = (byte) json.rare;
            this.raremax = (byte) json.raremax;
            this.type = json.type;
            this.element = json.elem;
            &this.flag.Set(0, (json.hero == 0) == 0);
            this.search = (byte) json.search;
            &this.flag.Set(1, json.notsmn == 0);
            if (string.IsNullOrEmpty(json.available_at) != null)
            {
                goto Label_012D;
            }
        Label_0106:
            try
            {
                this.available_at = DateTime.Parse(json.available_at);
                goto Label_012D;
            }
            catch
            {
            Label_011C:
                this.available_at = DateTime.MaxValue;
                goto Label_012D;
            }
        Label_012D:
            this.height = (short) json.height;
            this.weight = (short) json.weight;
            this.jobsets = null;
            this.mJobSetCache = null;
            this.tags = null;
            if (json.skins == null)
            {
                goto Label_01B1;
            }
            if (((int) json.skins.Length) < 1)
            {
                goto Label_01B1;
            }
            this.skins = new string[(int) json.skins.Length];
            num = 0;
            goto Label_01A3;
        Label_018F:
            this.skins[num] = json.skins[num];
            num += 1;
        Label_01A3:
            if (num < ((int) json.skins.Length))
            {
                goto Label_018F;
            }
        Label_01B1:
            if (NoJobStatus.IsExistParam(json) == null)
            {
                goto Label_01D3;
            }
            this.no_job_status = new NoJobStatus();
            this.no_job_status.SetParam(json);
        Label_01D3:
            if (this.type != 3)
            {
                goto Label_01E1;
            }
            return 1;
        Label_01E1:
            if (json.jobsets == null)
            {
                goto Label_0228;
            }
            this.jobsets = new string[(int) json.jobsets.Length];
            num2 = 0;
            goto Label_021A;
        Label_0206:
            this.jobsets[num2] = json.jobsets[num2];
            num2 += 1;
        Label_021A:
            if (num2 < ((int) this.jobsets.Length))
            {
                goto Label_0206;
            }
        Label_0228:
            if (json.tag == null)
            {
                goto Label_024F;
            }
            chArray1 = new char[] { 0x2c };
            this.tags = json.tag.Split(chArray1);
        Label_024F:
            if (this.ini_status != null)
            {
                goto Label_0265;
            }
            this.ini_status = new Status();
        Label_0265:
            this.ini_status.SetParamIni(json);
            this.ini_status.SetEnchentParamIni(json);
            if (this.max_status != null)
            {
                goto Label_0293;
            }
            this.max_status = new Status();
        Label_0293:
            this.max_status.SetParamMax(json);
            this.max_status.SetEnchentParamMax(json);
            this.leader_skills[0] = json.ls1;
            this.leader_skills[1] = json.ls2;
            this.leader_skills[2] = json.ls3;
            this.leader_skills[3] = json.ls4;
            this.leader_skills[4] = json.ls5;
            this.leader_skills[5] = json.ls6;
            this.recipes[0] = json.recipe1;
            this.recipes[1] = json.recipe2;
            this.recipes[2] = json.recipe3;
            this.recipes[3] = json.recipe4;
            this.recipes[4] = json.recipe5;
            this.recipes[5] = json.recipe6;
            this.image = json.img;
            this.voice = json.vce;
            &this.flag.Set(2, json.no_trw == 0);
            &this.flag.Set(3, json.no_kb == 0);
            this.unlock_time = json.unlck_t;
            return 1;
        }

        public static string GetBirthplaceName(int birth_id)
        {
            if (birth_id != null)
            {
                goto Label_000C;
            }
            return string.Empty;
        Label_000C:
            return LocalizedText.Get(string.Format("sys.BIRTH_PLACE_{0}", (int) birth_id));
        }

        public string GetJobId(int jobIndex)
        {
            JobSetParam param;
            param = this.GetJobSetFast(jobIndex);
            return ((param == null) ? string.Empty : param.job);
        }

        public string GetJobImage(string jobName)
        {
            return this.GetJobOptions(jobName, 1);
        }

        private string GetJobOptions(string jobName, bool isImage)
        {
            return string.Empty;
        }

        public JobSetParam GetJobSetFast(int index)
        {
            if (this.mJobSetCache == null)
            {
                goto Label_0029;
            }
            if (0 > index)
            {
                goto Label_0029;
            }
            if (index >= ((int) this.mJobSetCache.Length))
            {
                goto Label_0029;
            }
            return this.mJobSetCache[index];
        Label_0029:
            return null;
        }

        public string GetJobVoice(string jobName)
        {
            return this.GetJobOptions(jobName, 0);
        }

        public static int GetLeaderSkillLevel(int rarity, int awakeLv)
        {
            return 1;
        }

        public RecipeParam GetRarityUpRecipe(int rarity)
        {
            string str;
            if (this.recipes != null)
            {
                goto Label_000D;
            }
            return null;
        Label_000D:
            if (rarity < 0)
            {
                goto Label_002B;
            }
            if (rarity >= RarityParam.MAX_RARITY)
            {
                goto Label_002B;
            }
            if (rarity < this.raremax)
            {
                goto Label_002D;
            }
        Label_002B:
            return null;
        Label_002D:
            str = this.recipes[rarity];
            if (string.IsNullOrEmpty(str) == null)
            {
                goto Label_0043;
            }
            return null;
        Label_0043:
            return MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.GetRecipeParam(str);
        }

        public static EElement GetResistElement(EElement type)
        {
            return ResistElements[type];
        }

        public int GetUnlockNeedPieces()
        {
            RarityParam param;
            param = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.GetRarityParam(this.rare);
            if (param == null)
            {
                goto Label_0028;
            }
            return param.UnitUnlockPieceNum;
        Label_0028:
            return 0;
        }

        public static EElement GetWeakElement(EElement type)
        {
            return WeakElements[type];
        }

        public unsafe bool IsHero()
        {
            return &this.flag.Is(0);
        }

        public unsafe bool IsKnockBack()
        {
            return &this.flag.Is(3);
        }

        public bool IsStopped()
        {
            return ((this.type == 2) ? 1 : (this.type == 1));
        }

        public unsafe bool IsSummon()
        {
            return &this.flag.Is(1);
        }

        public unsafe bool IsThrow()
        {
            return &this.flag.Is(2);
        }

        public string SexPrefix
        {
            get
            {
                return SRPG_Extensions.ToPrefix(this.sex);
            }
        }

        private enum FLAG_TYPE
        {
            HERO,
            SUMMON,
            THROW,
            KNOCK_BACK
        }

        public class NoJobStatus
        {
            public string default_skill;
            public JobTypes jobtype;
            public RoleTypes role;
            public byte mov;
            public byte jmp;
            public int inimp;

            public NoJobStatus()
            {
                this.default_skill = string.Empty;
                base..ctor();
                return;
            }

            public static bool IsExistParam(JSON_UnitParam json)
            {
                if (json.dskl != null)
                {
                    goto Label_0042;
                }
                if (json.jt != null)
                {
                    goto Label_0042;
                }
                if (json.role != null)
                {
                    goto Label_0042;
                }
                if (json.mov != null)
                {
                    goto Label_0042;
                }
                if (json.jmp != null)
                {
                    goto Label_0042;
                }
                if (json.inimp == null)
                {
                    goto Label_0044;
                }
            Label_0042:
                return 1;
            Label_0044:
                return 0;
            }

            public void SetParam(JSON_UnitParam json)
            {
                this.default_skill = json.dskl;
                this.jobtype = json.jt;
                this.role = json.role;
                this.mov = (byte) json.mov;
                this.jmp = (byte) json.jmp;
                this.inimp = json.inimp;
                return;
            }
        }

        public class Status
        {
            public StatusParam param;
            public EnchantParam enchant_resist;

            public Status()
            {
                this.param = new StatusParam();
                base..ctor();
                return;
            }

            public BaseStatus CreateBaseStatus()
            {
                BaseStatus status;
                status = new BaseStatus();
                this.param.CopyTo(status.param);
                if (this.enchant_resist == null)
                {
                    goto Label_0033;
                }
                this.enchant_resist.CopyTo(status.enchant_resist);
            Label_0033:
                return status;
            }

            private bool IsExistEnchentParamIni(JSON_UnitParam json)
            {
                if (json.rpo != null)
                {
                    goto Label_0108;
                }
                if (json.rpa != null)
                {
                    goto Label_0108;
                }
                if (json.rst != null)
                {
                    goto Label_0108;
                }
                if (json.rsl != null)
                {
                    goto Label_0108;
                }
                if (json.rch != null)
                {
                    goto Label_0108;
                }
                if (json.rsn != null)
                {
                    goto Label_0108;
                }
                if (json.rbl != null)
                {
                    goto Label_0108;
                }
                if (json.rns != null)
                {
                    goto Label_0108;
                }
                if (json.rnm != null)
                {
                    goto Label_0108;
                }
                if (json.rna != null)
                {
                    goto Label_0108;
                }
                if (json.rzo != null)
                {
                    goto Label_0108;
                }
                if (json.rde != null)
                {
                    goto Label_0108;
                }
                if (json.rkn != null)
                {
                    goto Label_0108;
                }
                if (json.rdf != null)
                {
                    goto Label_0108;
                }
                if (json.rbe != null)
                {
                    goto Label_0108;
                }
                if (json.rcs != null)
                {
                    goto Label_0108;
                }
                if (json.rcu != null)
                {
                    goto Label_0108;
                }
                if (json.rcd != null)
                {
                    goto Label_0108;
                }
                if (json.rdo != null)
                {
                    goto Label_0108;
                }
                if (json.rra != null)
                {
                    goto Label_0108;
                }
                if (json.rsa != null)
                {
                    goto Label_0108;
                }
                if (json.raa != null)
                {
                    goto Label_0108;
                }
                if (json.rdc != null)
                {
                    goto Label_0108;
                }
                if (json.ric == null)
                {
                    goto Label_010A;
                }
            Label_0108:
                return 1;
            Label_010A:
                return 0;
            }

            private bool IsExistEnchentParamMax(JSON_UnitParam json)
            {
                if (json.mrpo != null)
                {
                    goto Label_0108;
                }
                if (json.mrpa != null)
                {
                    goto Label_0108;
                }
                if (json.mrst != null)
                {
                    goto Label_0108;
                }
                if (json.mrsl != null)
                {
                    goto Label_0108;
                }
                if (json.mrch != null)
                {
                    goto Label_0108;
                }
                if (json.mrsn != null)
                {
                    goto Label_0108;
                }
                if (json.mrbl != null)
                {
                    goto Label_0108;
                }
                if (json.mrns != null)
                {
                    goto Label_0108;
                }
                if (json.mrnm != null)
                {
                    goto Label_0108;
                }
                if (json.mrna != null)
                {
                    goto Label_0108;
                }
                if (json.mrzo != null)
                {
                    goto Label_0108;
                }
                if (json.mrde != null)
                {
                    goto Label_0108;
                }
                if (json.mrkn != null)
                {
                    goto Label_0108;
                }
                if (json.mrdf != null)
                {
                    goto Label_0108;
                }
                if (json.mrbe != null)
                {
                    goto Label_0108;
                }
                if (json.mrcs != null)
                {
                    goto Label_0108;
                }
                if (json.mrcu != null)
                {
                    goto Label_0108;
                }
                if (json.mrcd != null)
                {
                    goto Label_0108;
                }
                if (json.mrdo != null)
                {
                    goto Label_0108;
                }
                if (json.mrra != null)
                {
                    goto Label_0108;
                }
                if (json.mrsa != null)
                {
                    goto Label_0108;
                }
                if (json.mraa != null)
                {
                    goto Label_0108;
                }
                if (json.mrdc != null)
                {
                    goto Label_0108;
                }
                if (json.mric == null)
                {
                    goto Label_010A;
                }
            Label_0108:
                return 1;
            Label_010A:
                return 0;
            }

            public void SetEnchentParamIni(JSON_UnitParam json)
            {
                if (this.IsExistEnchentParamIni(json) != null)
                {
                    goto Label_000D;
                }
                return;
            Label_000D:
                this.enchant_resist = new EnchantParam();
                this.enchant_resist.poison = json.rpo;
                this.enchant_resist.paralyse = json.rpa;
                this.enchant_resist.stun = json.rst;
                this.enchant_resist.sleep = json.rsl;
                this.enchant_resist.charm = json.rch;
                this.enchant_resist.stone = json.rsn;
                this.enchant_resist.blind = json.rbl;
                this.enchant_resist.notskl = json.rns;
                this.enchant_resist.notmov = json.rnm;
                this.enchant_resist.notatk = json.rna;
                this.enchant_resist.zombie = json.rzo;
                this.enchant_resist.death = json.rde;
                this.enchant_resist.knockback = json.rkn;
                this.enchant_resist.resist_debuff = json.rdf;
                this.enchant_resist.berserk = json.rbe;
                this.enchant_resist.stop = json.rcs;
                this.enchant_resist.fast = json.rcu;
                this.enchant_resist.slow = json.rcd;
                this.enchant_resist.donsoku = json.rdo;
                this.enchant_resist.rage = json.rra;
                this.enchant_resist.single_attack = json.rsa;
                this.enchant_resist.area_attack = json.raa;
                this.enchant_resist.dec_ct = json.rdc;
                this.enchant_resist.inc_ct = json.ric;
                return;
            }

            public void SetEnchentParamMax(JSON_UnitParam json)
            {
                if (this.IsExistEnchentParamMax(json) != null)
                {
                    goto Label_000D;
                }
                return;
            Label_000D:
                this.enchant_resist = new EnchantParam();
                this.enchant_resist.poison = json.mrpo;
                this.enchant_resist.paralyse = json.mrpa;
                this.enchant_resist.stun = json.mrst;
                this.enchant_resist.sleep = json.mrsl;
                this.enchant_resist.charm = json.mrch;
                this.enchant_resist.stone = json.mrsn;
                this.enchant_resist.blind = json.mrbl;
                this.enchant_resist.notskl = json.mrns;
                this.enchant_resist.notmov = json.mrnm;
                this.enchant_resist.notatk = json.mrna;
                this.enchant_resist.zombie = json.mrzo;
                this.enchant_resist.death = json.mrde;
                this.enchant_resist.knockback = json.mrkn;
                this.enchant_resist.resist_debuff = json.mrdf;
                this.enchant_resist.berserk = json.mrbe;
                this.enchant_resist.stop = json.mrcs;
                this.enchant_resist.fast = json.mrcu;
                this.enchant_resist.slow = json.mrcd;
                this.enchant_resist.donsoku = json.mrdo;
                this.enchant_resist.rage = json.mrra;
                this.enchant_resist.single_attack = json.mrsa;
                this.enchant_resist.area_attack = json.mraa;
                this.enchant_resist.dec_ct = json.mrdc;
                this.enchant_resist.inc_ct = json.mric;
                return;
            }

            public void SetParamIni(JSON_UnitParam json)
            {
                this.param.hp = json.hp;
                this.param.mp = json.mp;
                this.param.atk = json.atk;
                this.param.def = json.def;
                this.param.mag = json.mag;
                this.param.mnd = json.mnd;
                this.param.dex = json.dex;
                this.param.spd = json.spd;
                this.param.cri = json.cri;
                this.param.luk = json.luk;
                return;
            }

            public void SetParamMax(JSON_UnitParam json)
            {
                this.param.hp = json.mhp;
                this.param.mp = json.mmp;
                this.param.atk = json.matk;
                this.param.def = json.mdef;
                this.param.mag = json.mmag;
                this.param.mnd = json.mmnd;
                this.param.dex = json.mdex;
                this.param.spd = json.mspd;
                this.param.cri = json.mcri;
                this.param.luk = json.mluk;
                return;
            }
        }
    }
}

