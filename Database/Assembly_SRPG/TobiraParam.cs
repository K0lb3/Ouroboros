namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;

    public class TobiraParam
    {
        public static readonly int MAX_TOBIRA_COUNT;
        private string mUnitIname;
        private bool mEnable;
        private Category mCategory;
        private string mRecipeId;
        private string mSkillIname;
        private List<TobiraLearnAbilityParam> mLearnAbilities;
        private string mOverwriteLeaderSkillIname;
        private int mOverwriteLeaderSkillLevel;
        private int mPriority;

        static TobiraParam()
        {
            MAX_TOBIRA_COUNT = 8;
            return;
        }

        public TobiraParam()
        {
            this.mLearnAbilities = new List<TobiraLearnAbilityParam>();
            base..ctor();
            return;
        }

        public void Deserialize(JSON_TobiraParam json)
        {
            int num;
            TobiraLearnAbilityParam param;
            GameManager manager;
            if (json != null)
            {
                goto Label_0007;
            }
            return;
        Label_0007:
            this.mUnitIname = json.unit_iname;
            this.mEnable = json.enable == 1;
            this.mCategory = json.category;
            this.mRecipeId = json.recipe_id;
            this.mSkillIname = json.skill_iname;
            this.mLearnAbilities.Clear();
            if (json.learn_abils == null)
            {
                goto Label_0095;
            }
            num = 0;
            goto Label_0087;
        Label_0063:
            param = new TobiraLearnAbilityParam();
            param.Deserialize(json.learn_abils[num]);
            this.mLearnAbilities.Add(param);
            num += 1;
        Label_0087:
            if (num < ((int) json.learn_abils.Length))
            {
                goto Label_0063;
            }
        Label_0095:
            this.mOverwriteLeaderSkillIname = json.overwrite_ls_iname;
            if (string.IsNullOrEmpty(this.mOverwriteLeaderSkillIname) != null)
            {
                goto Label_00E9;
            }
            manager = MonoSingleton<GameManager>.GetInstanceDirect();
            if ((manager != null) == null)
            {
                goto Label_00E9;
            }
            if (manager.MasterParam == null)
            {
                goto Label_00E9;
            }
            this.mOverwriteLeaderSkillLevel = manager.MasterParam.FixParam.TobiraLvCap;
        Label_00E9:
            this.mPriority = json.priority;
            return;
        }

        public static string GetCategoryName(Category category)
        {
            Category category2;
            category2 = category;
            switch ((category2 - 1))
            {
                case 0:
                    goto Label_002B;

                case 1:
                    goto Label_0036;

                case 2:
                    goto Label_0041;

                case 3:
                    goto Label_004C;

                case 4:
                    goto Label_0057;

                case 5:
                    goto Label_0062;

                case 6:
                    goto Label_006D;
            }
            goto Label_0078;
        Label_002B:
            return LocalizedText.Get("sys.CMD_TOBIRA_ENVY");
        Label_0036:
            return LocalizedText.Get("sys.CMD_TOBIRA_WRATH");
        Label_0041:
            return LocalizedText.Get("sys.CMD_TOBIRA_SLOTH");
        Label_004C:
            return LocalizedText.Get("sys.CMD_TOBIRA_LUST");
        Label_0057:
            return LocalizedText.Get("sys.CMD_TOBIRA_GLUTTONY");
        Label_0062:
            return LocalizedText.Get("sys.CMD_TOBIRA_GREED");
        Label_006D:
            return LocalizedText.Get("sys.CMD_TOBIRA_PRIDE");
        Label_0078:
            return string.Empty;
        }

        public string UnitIname
        {
            get
            {
                return this.mUnitIname;
            }
        }

        public bool Enable
        {
            get
            {
                return this.mEnable;
            }
        }

        public Category TobiraCategory
        {
            get
            {
                return this.mCategory;
            }
        }

        public string RecipeId
        {
            get
            {
                return this.mRecipeId;
            }
        }

        public string SkillIname
        {
            get
            {
                return this.mSkillIname;
            }
        }

        public TobiraLearnAbilityParam[] LeanAbilityParam
        {
            get
            {
                return this.mLearnAbilities.ToArray();
            }
        }

        public string OverwriteLeaderSkillIname
        {
            get
            {
                return this.mOverwriteLeaderSkillIname;
            }
        }

        public int OverwriteLeaderSkillLevel
        {
            get
            {
                return this.mOverwriteLeaderSkillLevel;
            }
        }

        public int Priority
        {
            get
            {
                return this.mPriority;
            }
        }

        public bool HasLeaerSkill
        {
            get
            {
                return (string.IsNullOrEmpty(this.mOverwriteLeaderSkillIname) == 0);
            }
        }

        public enum Category
        {
            START = 0,
            Unlock = 0,
            Envy = 1,
            Wrath = 2,
            Sloth = 3,
            Lust = 4,
            Gluttony = 5,
            Greed = 6,
            Pride = 7,
            MAX = 8
        }
    }
}

