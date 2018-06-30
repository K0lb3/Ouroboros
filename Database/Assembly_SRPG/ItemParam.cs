namespace SRPG
{
    using GR;
    using System;

    public class ItemParam
    {
        public const string CoinID = "$COIN";
        private const string ITEM_EXPR_PREFIX = "_EXPR";
        private const string ITEM_FLAVOR_PREFIX = "_FLAVOR";
        public int no;
        public string iname;
        public string name;
        public EItemType type;
        public EItemTabType tabtype;
        public int rare;
        public int cap;
        public int invcap;
        public int equipLv;
        public int coin;
        public int tour_coin;
        public int arena_coin;
        public int multi_coin;
        public int piece_point;
        public int buy;
        public int sell;
        public int enhace_cost;
        public int enhace_point;
        public int value;
        public string icon;
        public string skill;
        public string recipe;
        public string[] quests;
        public bool is_valuables;
        public byte cmn_type;

        public ItemParam()
        {
            base..ctor();
            return;
        }

        public bool CheckCanShowInList()
        {
            EItemType type;
            switch ((this.type - 12))
            {
                case 0:
                    goto Label_0035;

                case 1:
                    goto Label_0037;

                case 2:
                    goto Label_0037;

                case 3:
                    goto Label_0035;

                case 4:
                    goto Label_0037;

                case 5:
                    goto Label_0035;

                case 6:
                    goto Label_0037;

                case 7:
                    goto Label_0035;
            }
            goto Label_0037;
        Label_0035:
            return 0;
        Label_0037:
            return 1;
        }

        public bool CheckEquipEnhanceMaterial()
        {
            EItemType type;
            type = this.type;
            switch ((type - 2))
            {
                case 0:
                    goto Label_0044;

                case 1:
                    goto Label_0044;

                case 2:
                    goto Label_0044;

                case 3:
                    goto Label_0037;

                case 4:
                    goto Label_0037;

                case 5:
                    goto Label_0037;

                case 6:
                    goto Label_0044;

                case 7:
                    goto Label_0037;

                case 8:
                    goto Label_0037;

                case 9:
                    goto Label_0044;
            }
        Label_0037:
            if (type == 14)
            {
                goto Label_0044;
            }
            goto Label_0046;
        Label_0044:
            return 1;
        Label_0046:
            return 0;
        }

        public bool Deserialize(JSON_ItemParam json)
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
            this.type = json.type;
            this.tabtype = json.tabtype;
            this.rare = json.rare;
            this.cap = json.cap;
            this.invcap = json.invcap;
            this.equipLv = Math.Max(json.eqlv, 1);
            this.coin = json.coin;
            this.tour_coin = json.tc;
            this.arena_coin = json.ac;
            this.multi_coin = json.mc;
            this.piece_point = json.pp;
            this.buy = json.buy;
            this.sell = json.sell;
            this.enhace_cost = json.encost;
            this.enhace_point = json.enpt;
            this.value = json.val;
            this.icon = json.icon;
            this.skill = json.skill;
            this.recipe = json.recipe;
            this.quests = null;
            this.is_valuables = json.is_valuables > 0;
            this.cmn_type = json.cmn_type;
            if (json.quests == null)
            {
                goto Label_0173;
            }
            this.quests = new string[(int) json.quests.Length];
            num = 0;
            goto Label_0165;
        Label_0151:
            this.quests[num] = json.quests[num];
            num += 1;
        Label_0165:
            if (num < ((int) json.quests.Length))
            {
                goto Label_0151;
            }
        Label_0173:
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
            return this.coin;
        Label_003B:
            return this.coin;
        Label_0042:
            return this.tour_coin;
        Label_0049:
            return this.arena_coin;
        Label_0050:
            return this.piece_point;
        Label_0057:
            return this.multi_coin;
        Label_005E:
            return 0;
        Label_0060:
            return 0;
        }

        public int GetEnhanceRankCap()
        {
            RarityParam param;
            if (this.type == 3)
            {
                goto Label_000E;
            }
            return 1;
        Label_000E:
            return MonoSingleton<GameManager>.GetInstanceDirect().GetRarityParam(this.rare).EquipEnhanceParam.rankcap;
        }

        public int GetPiercePoint()
        {
            RarityParam param;
            if (this.type == 1)
            {
                goto Label_000E;
            }
            return 0;
        Label_000E:
            return MonoSingleton<GameManager>.GetInstanceDirect().GetRarityParam(this.rare).PieceToPoint;
        }

        public string GetText(string table, string key)
        {
            string str;
            str = LocalizedText.Get(table + "." + key);
            return ((str.Equals(key) == null) ? str : string.Empty);
        }

        public override string ToString()
        {
            return string.Format("{0} [ItemParam]", this.iname);
        }

        public string Expr
        {
            get
            {
                return this.GetText("external_item", this.iname + "_EXPR");
            }
        }

        public string Flavor
        {
            get
            {
                return this.GetText("external_item", this.iname + "_FLAVOR");
            }
        }

        public RecipeParam Recipe
        {
            get
            {
                return ((string.IsNullOrEmpty(this.recipe) != null) ? null : MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.GetRecipeParam(this.recipe));
            }
        }

        public bool IsCommon
        {
            get
            {
                return (this.cmn_type > 0);
            }
        }
    }
}

