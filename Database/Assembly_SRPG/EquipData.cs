namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;

    public class EquipData
    {
        private long mUniqueID;
        private SRPG.ItemParam mItemParam;
        private SRPG.RarityParam mRarityParam;
        private int mExp;
        private int mRank;
        private SkillData mSkill;
        private bool mEquiped;

        public EquipData()
        {
            base..ctor();
            return;
        }

        public int CalcRank()
        {
            return this.CalcRankFromExp(this.Exp);
        }

        public int CalcRankFromExp(int current)
        {
            int num;
            int num2;
            int num3;
            int num4;
            num = this.GetRankCap();
            num2 = 0;
            num3 = 0;
            num4 = 0;
            goto Label_0032;
        Label_0012:
            num2 += this.GetNextExp(num4 + 1);
            if (num2 > current)
            {
                goto Label_002E;
            }
            num3 += 1;
        Label_002E:
            num4 += 1;
        Label_0032:
            if (num4 < num)
            {
                goto Label_0012;
            }
            return Math.Min(Math.Max(num3, 1), num);
        }

        public void Equip(Json_Equip json)
        {
            if (json != null)
            {
                goto Label_0007;
            }
            return;
        Label_0007:
            this.Equip(json.iname, json.iid, json.exp);
            return;
        }

        public void Equip(string iname, long iid, int exp)
        {
            if (this.IsValid() == null)
            {
                goto Label_0027;
            }
            if ((this.mItemParam.iname != iname) != null)
            {
                goto Label_0027;
            }
            if (iid != null)
            {
                goto Label_0028;
            }
        Label_0027:
            return;
        Label_0028:
            this.mUniqueID = iid;
            this.mExp = exp;
            this.mRank = this.CalcRank();
            this.mEquiped = 1;
            if (this.mSkill != null)
            {
                goto Label_0074;
            }
            if (string.IsNullOrEmpty(this.mItemParam.skill) != null)
            {
                goto Label_0074;
            }
            this.mSkill = new SkillData();
        Label_0074:
            if (this.mSkill == null)
            {
                goto Label_00A2;
            }
            this.mSkill.Setup(this.mItemParam.skill, this.mRank, this.GetRankCap(), null);
        Label_00A2:
            return;
        }

        public void GainExp(int exp)
        {
            this.mExp += exp;
            this.mRank = this.CalcRank();
            if (this.mSkill == null)
            {
                goto Label_0053;
            }
            if (this.ItemParam == null)
            {
                goto Label_0053;
            }
            this.mSkill.Setup(this.ItemParam.skill, this.mRank, this.GetRankCap(), null);
        Label_0053:
            return;
        }

        public int GetEnhanceCostScale()
        {
            if (this.RarityParam == null)
            {
                goto Label_001B;
            }
            if (this.RarityParam.EquipEnhanceParam != null)
            {
                goto Label_001D;
            }
        Label_001B:
            return 0;
        Label_001D:
            return this.RarityParam.EquipEnhanceParam.cost_scale;
        }

        public int GetExp()
        {
            return this.GetExpFromExp(this.Exp);
        }

        public int GetExpFromExp(int current)
        {
            int num;
            int num2;
            int num3;
            num = this.CalcRankFromExp(current);
            num2 = this.GetNeedExp(num);
            num3 = current - num2;
            return num3;
        }

        public int GetNeedExp(int rank)
        {
            object[] objArray1;
            RarityEquipEnhanceParam param;
            int num;
            int num2;
            param = (this.RarityParam == null) ? null : this.RarityParam.EquipEnhanceParam;
            objArray1 = new object[] { "アイテムのレアリティ", (int) this.mItemParam.rare, "には指定ランク", (int) rank, "の情報に存在しない。" };
            DebugUtility.Assert((rank <= 0) ? 0 : ((rank > param.rankcap) == 0), string.Concat(objArray1));
            num = 0;
            num2 = 0;
            goto Label_009E;
        Label_0085:
            num += param.ranks[num2].need_point;
            num2 += 1;
        Label_009E:
            if (num2 < rank)
            {
                goto Label_0085;
            }
            return num;
        }

        public int GetNextExp()
        {
            return this.GetNextExpFromExp(this.Exp);
        }

        public int GetNextExp(int rank)
        {
            object[] objArray1;
            RarityEquipEnhanceParam param;
            int num;
            param = (this.RarityParam == null) ? null : this.RarityParam.EquipEnhanceParam;
            objArray1 = new object[] { "アイテムのレアリティ", (int) this.mItemParam.rare, "には指定ランク", (int) rank, "の情報に存在しない。" };
            DebugUtility.Assert((rank <= 0) ? 0 : ((rank > param.rankcap) == 0), string.Concat(objArray1));
            num = rank - 1;
            if (num >= param.rankcap)
            {
                goto Label_00A4;
            }
            return param.ranks[num].need_point;
        Label_00A4:
            return 0;
        }

        public int GetNextExpFromExp(int current)
        {
            int num;
            int num2;
            int num3;
            num = this.GetRankCap();
            num2 = 0;
            num3 = 0;
            goto Label_0030;
        Label_0010:
            num2 += this.GetNextExp(num3 + 1);
            if (num2 > current)
            {
                goto Label_0028;
            }
            goto Label_002C;
        Label_0028:
            return (num2 - current);
        Label_002C:
            num3 += 1;
        Label_0030:
            if (num3 < num)
            {
                goto Label_0010;
            }
            return 0;
        }

        public int GetRankCap()
        {
            if (this.mRarityParam == null)
            {
                goto Label_0021;
            }
            return this.RarityParam.EquipEnhanceParam.rankcap;
        Label_0021:
            return 1;
        }

        public List<ItemData> GetReturnItemList()
        {
            RarityEquipEnhanceParam param;
            RarityEquipEnhanceParam.RankParam param2;
            ReturnItem[] itemArray;
            List<ItemData> list;
            int num;
            ItemData data;
            if ((this.IsValid() != null) && (this.IsEquiped() != null))
            {
                goto Label_0018;
            }
            return null;
        Label_0018:
            param = (this.RarityParam == null) ? null : this.RarityParam.EquipEnhanceParam;
            if (param == null)
            {
                goto Label_0046;
            }
            if (param.ranks != null)
            {
                goto Label_0048;
            }
        Label_0046:
            return null;
        Label_0048:
            param2 = param.GetRankParam(this.Rank);
            if (param2 == null)
            {
                goto Label_0066;
            }
            if (param2.return_item != null)
            {
                goto Label_0068;
            }
        Label_0066:
            return null;
        Label_0068:
            itemArray = param2.return_item;
            list = new List<ItemData>();
            num = 0;
            goto Label_00DA;
        Label_007D:
            if (string.IsNullOrEmpty(itemArray[num].iname) != null)
            {
                goto Label_00D4;
            }
            if (itemArray[num].num <= 0)
            {
                goto Label_00D4;
            }
            data = new ItemData();
            data.Setup(0L, itemArray[num].iname, itemArray[num].num);
            list.Add(data);
        Label_00D4:
            num += 1;
        Label_00DA:
            if (num < ((int) itemArray.Length))
            {
                goto Label_007D;
            }
            return list;
        }

        public bool IsEquiped()
        {
            return this.mEquiped;
        }

        public bool IsValid()
        {
            return ((this.mItemParam == null) == 0);
        }

        public void Reset()
        {
            this.mUniqueID = 0L;
            this.mItemParam = null;
            this.mExp = 0;
            this.mRank = 1;
            this.mSkill = null;
            this.mEquiped = 0;
            return;
        }

        public bool Setup(string item_iname)
        {
            GameManager manager;
            int num;
            this.Reset();
            if (string.IsNullOrEmpty(item_iname) == null)
            {
                goto Label_0013;
            }
            return 0;
        Label_0013:
            manager = MonoSingleton<GameManager>.Instance;
            this.mItemParam = manager.GetItemParam(item_iname);
            this.mRarityParam = manager.GetRarityParam(this.mItemParam.rare);
            if (string.IsNullOrEmpty(this.mItemParam.skill) != null)
            {
                goto Label_0091;
            }
            num = this.CalcRank();
            this.mSkill = new SkillData();
            this.mSkill.Setup(this.mItemParam.skill, num, this.mRarityParam.EquipEnhanceParam.rankcap, null);
        Label_0091:
            return 1;
        }

        public override string ToString()
        {
            return string.Format("ItemParam=[{0}] ({1})", this.ItemParam, base.GetType().Name);
        }

        public void UpdateParam()
        {
            if (this.mSkill == null)
            {
                goto Label_0016;
            }
            this.mSkill.UpdateParam();
        Label_0016:
            return;
        }

        public long UniqueID
        {
            get
            {
                return this.mUniqueID;
            }
        }

        public SRPG.ItemParam ItemParam
        {
            get
            {
                return this.mItemParam;
            }
        }

        public SRPG.RarityParam RarityParam
        {
            get
            {
                return this.mRarityParam;
            }
        }

        public string ItemID
        {
            get
            {
                return ((this.mItemParam == null) ? null : this.mItemParam.iname);
            }
        }

        public int Rank
        {
            get
            {
                return this.mRank;
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

        public int Exp
        {
            get
            {
                return this.mExp;
            }
        }

        public SkillData Skill
        {
            get
            {
                return this.mSkill;
            }
        }
    }
}

