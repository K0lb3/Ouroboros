namespace SRPG
{
    using GR;
    using System;

    public class ItemData
    {
        private long mUniqueID;
        private ItemParam mItemParam;
        private SRPG.RarityParam mRarityParam;
        private SkillData mSkill;
        private ItemFlags mFlags;
        protected int mNum;
        private bool mIsNew;

        public ItemData()
        {
            base..ctor();
            return;
        }

        public bool CheckEquipEnhanceMaterial()
        {
            return ((this.mItemParam == null) ? 0 : this.mItemParam.CheckEquipEnhanceMaterial());
        }

        public bool Deserialize(Json_Item json)
        {
            return ((json == null) ? 0 : this.Setup(json.iid, json.iname, json.num));
        }

        public void Gain(int num)
        {
            this.mNum = Math.Max(this.mNum + num, 0);
            return;
        }

        public bool GetFlag(ItemFlags flag)
        {
            return ((0 == (this.mFlags & flag)) == 0);
        }

        public int GetRankCap()
        {
            if (this.mRarityParam == null)
            {
                goto Label_0021;
            }
            return this.mRarityParam.EquipEnhanceParam.rankcap;
        Label_0021:
            return 1;
        }

        public void ResetAllFlag(ItemFlags flag)
        {
            this.mFlags = 0;
            return;
        }

        public void ResetFlag(ItemFlags flag)
        {
            this.mFlags &= ~flag;
            return;
        }

        public void SetFlag(ItemFlags flag)
        {
            this.mFlags |= flag;
            return;
        }

        public void SetNum(int num)
        {
            this.mNum = Math.Max(num, 0);
            return;
        }

        public bool Setup(long iid, ItemParam itemParam, int num)
        {
            int num2;
            this.mItemParam = itemParam;
            this.mUniqueID = iid;
            this.mNum = num;
            if (string.IsNullOrEmpty(this.mItemParam.skill) != null)
            {
                goto Label_0055;
            }
            num2 = 1;
            this.mSkill = new SkillData();
            this.mSkill.Setup(this.mItemParam.skill, num2, this.GetRankCap(), null);
        Label_0055:
            return 1;
        }

        public bool Setup(long iid, string iname, int num)
        {
            GameManager manager;
            int num2;
            manager = MonoSingleton<GameManager>.GetInstanceDirect();
            this.mItemParam = manager.GetItemParam(iname);
            DebugUtility.Assert((this.mItemParam == null) == 0, "Failed ItemParam iname \"" + iname + "\" not found.");
            this.mRarityParam = manager.GetRarityParam(this.mItemParam.rare);
            this.mUniqueID = iid;
            this.mNum = num;
            if (string.IsNullOrEmpty(this.mItemParam.skill) != null)
            {
                goto Label_0099;
            }
            num2 = 1;
            this.mSkill = new SkillData();
            this.mSkill.Setup(this.mItemParam.skill, num2, this.GetRankCap(), null);
        Label_0099:
            return 1;
        }

        public override string ToString()
        {
            return (((this.mItemParam == null) ? "None" : this.mItemParam.name) + base.GetType().FullName);
        }

        public void Used(int num)
        {
            this.mNum = Math.Max(this.mNum - num, 0);
            return;
        }

        public int No
        {
            get
            {
                return this.mItemParam.no;
            }
        }

        public long UniqueID
        {
            get
            {
                return this.mUniqueID;
            }
        }

        public ItemParam Param
        {
            get
            {
                return this.mItemParam;
            }
        }

        public string ItemID
        {
            get
            {
                return ((this.mItemParam == null) ? null : this.mItemParam.iname);
            }
        }

        public int Num
        {
            get
            {
                if (this.mItemParam == null)
                {
                    goto Label_0022;
                }
                return Math.Min(this.mNum, this.mItemParam.cap);
            Label_0022:
                return this.mNum;
            }
        }

        public int NumNonCap
        {
            get
            {
                return this.mNum;
            }
        }

        public SkillData Skill
        {
            get
            {
                return this.mSkill;
            }
        }

        public bool IsUsed
        {
            get
            {
                return (this.mNum > 0);
            }
        }

        public EItemType ItemType
        {
            get
            {
                return ((this.mItemParam == null) ? 0 : this.mItemParam.type);
            }
        }

        public int Rarity
        {
            get
            {
                return ((this.mItemParam == null) ? 0 : this.mItemParam.rare);
            }
        }

        public SRPG.RarityParam RarityParam
        {
            get
            {
                return this.mRarityParam;
            }
        }

        public int HaveCap
        {
            get
            {
                return ((this.mItemParam == null) ? 0 : this.mItemParam.cap);
            }
        }

        public int InventoryCap
        {
            get
            {
                return ((this.mItemParam == null) ? 0 : this.mItemParam.invcap);
            }
        }

        public int Buy
        {
            get
            {
                return ((this.mItemParam == null) ? 0 : this.mItemParam.buy);
            }
        }

        public int Sell
        {
            get
            {
                return ((this.mItemParam == null) ? 0 : this.mItemParam.sell);
            }
        }

        public RecipeParam Recipe
        {
            get
            {
                return ((this.mItemParam == null) ? null : this.mItemParam.Recipe);
            }
        }

        public bool IsNew
        {
            get
            {
                return this.GetFlag(1);
            }
            set
            {
                if (value == null)
                {
                    goto Label_0012;
                }
                this.SetFlag(1);
                goto Label_0019;
            Label_0012:
                this.ResetFlag(1);
            Label_0019:
                return;
            }
        }

        public bool IsNewSkin
        {
            get
            {
                return this.GetFlag(2);
            }
            set
            {
                if (value == null)
                {
                    goto Label_0012;
                }
                this.SetFlag(2);
                goto Label_0019;
            Label_0012:
                this.ResetFlag(2);
            Label_0019:
                return;
            }
        }

        [Flags]
        public enum ItemFlags
        {
            NewItem = 1,
            NewSkin = 2
        }
    }
}

