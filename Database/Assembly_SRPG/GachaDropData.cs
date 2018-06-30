namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class GachaDropData
    {
        public Type type;
        public UnitParam unit;
        public ItemParam item;
        public ArtifactParam artifact;
        public int num;
        public UnitParam unitOrigin;
        public bool isNew;
        public int[] excites;
        private int rarity;
        public ConceptCardParam conceptcard;
        public UnitParam cardunit;
        public bool isGift;
        [CompilerGenerated]
        private static Dictionary<string, int> <>f__switch$map8;

        public GachaDropData()
        {
            base..ctor();
            return;
        }

        public unsafe bool Deserialize(Json_DropInfo json)
        {
            string str;
            Dictionary<string, int> dictionary;
            int num;
            this.Init();
            if (json != null)
            {
                goto Label_000E;
            }
            return 0;
        Label_000E:
            str = json.type;
            if (str == null)
            {
                goto Label_02BF;
            }
            if (<>f__switch$map8 != null)
            {
                goto Label_0062;
            }
            dictionary = new Dictionary<string, int>(4);
            dictionary.Add("item", 0);
            dictionary.Add("unit", 1);
            dictionary.Add("artifact", 2);
            dictionary.Add("concept_card", 3);
            <>f__switch$map8 = dictionary;
        Label_0062:
            if (<>f__switch$map8.TryGetValue(str, &num) == null)
            {
                goto Label_02BF;
            }
            switch (num)
            {
                case 0:
                    goto Label_008F;

                case 1:
                    goto Label_00E9;

                case 2:
                    goto Label_0143;

                case 3:
                    goto Label_0260;
            }
            goto Label_02BF;
        Label_008F:
            this.type = 1;
            this.item = MonoSingleton<GameManager>.Instance.GetItemParam(json.iname);
            if (this.item != null)
            {
                goto Label_00D3;
            }
            DebugUtility.LogError("iname:" + json.iname + " => Not ItemParam!");
            return 0;
        Label_00D3:
            this.rarity = this.item.rare;
            goto Label_02BF;
        Label_00E9:
            this.unit = MonoSingleton<GameManager>.Instance.GetUnitParam(json.iname);
            if (this.unit != null)
            {
                goto Label_0126;
            }
            DebugUtility.LogError("iname:" + json.iname + " => Not UnitParam!");
            return 0;
        Label_0126:
            this.rarity = this.unit.rare;
            this.type = 2;
            goto Label_02BF;
        Label_0143:
            this.artifact = MonoSingleton<GameManager>.Instance.MasterParam.GetArtifactParam(json.iname);
            if (this.artifact != null)
            {
                goto Label_0185;
            }
            DebugUtility.LogError("iname:" + json.iname + " => Not ArtifactParam!");
            return 0;
        Label_0185:
            if ((json.rare == -1) || (json.rare <= this.artifact.raremax))
            {
                goto Label_01CB;
            }
            DebugUtility.LogError("武具:" + this.artifact.name + "の最大レアリティより大きい値が設定されています.");
            goto Label_020C;
        Label_01CB:
            if ((json.rare == -1) || (json.rare >= this.artifact.rareini))
            {
                goto Label_020C;
            }
            DebugUtility.LogError("武具:" + this.artifact.name + "の初期レアリティより小さい値が設定されています.");
        Label_020C:
            this.rarity = (json.rare <= -1) ? this.artifact.rareini : Math.Min(Math.Max(this.artifact.rareini, json.rare), this.artifact.raremax);
            this.type = 3;
            goto Label_02BF;
        Label_0260:
            this.conceptcard = MonoSingleton<GameManager>.Instance.MasterParam.GetConceptCardParam(json.iname);
            if (this.conceptcard != null)
            {
                goto Label_02A2;
            }
            DebugUtility.LogError("iname:" + json.iname + " => Not ConceptCardParam!");
            return 0;
        Label_02A2:
            this.rarity = this.conceptcard.rare;
            this.type = 4;
        Label_02BF:
            this.num = json.num;
            if (0 >= json.iname_origin.Length)
            {
                goto Label_02F2;
            }
            this.unitOrigin = MonoSingleton<GameManager>.Instance.GetUnitParam(json.iname_origin);
        Label_02F2:
            this.isNew = 1 == json.is_new;
            if (this.type != 4)
            {
                goto Label_035A;
            }
            if (string.IsNullOrEmpty(json.get_unit) != null)
            {
                goto Label_035A;
            }
            this.cardunit = MonoSingleton<GameManager>.Instance.GetUnitParam(json.get_unit);
            if (this.cardunit != null)
            {
                goto Label_035A;
            }
            DebugUtility.LogError("get_unit:" + json.get_unit + " => Not UnitParam!");
            return 0;
        Label_035A:
            this.isGift = json.is_gift == 1;
            return 1;
        }

        public void Init()
        {
            this.type = 0;
            this.unit = null;
            this.item = null;
            this.artifact = null;
            this.num = 0;
            this.unitOrigin = null;
            this.isNew = 0;
            this.rarity = 0;
            this.excites = new int[3];
            this.isGift = 0;
            return;
        }

        public override string ToString()
        {
            object[] objArray3;
            object[] objArray2;
            object[] objArray1;
            string str;
            Type type;
            string str2;
            str = "type: " + ((Type) this.type) + "\n";
            switch ((this.type - 1))
            {
                case 0:
                    goto Label_003B;

                case 1:
                    goto Label_0083;

                case 2:
                    goto Label_00CB;
            }
            goto Label_0113;
        Label_003B:
            str2 = str;
            objArray1 = new object[] { str2, "name: ", this.item.name, " rare: ", (int) this.item.rare };
            str = string.Concat(objArray1);
            goto Label_0113;
        Label_0083:
            str2 = str;
            objArray2 = new object[] { str2, "name: ", this.unit.name, " rare: ", (byte) this.unit.rare };
            str = string.Concat(objArray2);
            goto Label_0113;
        Label_00CB:
            str2 = str;
            objArray3 = new object[] { str2, "name: ", this.artifact.name, " rare: ", (int) this.artifact.rareini };
            str = string.Concat(objArray3);
        Label_0113:
            if (this.unitOrigin == null)
            {
                goto Label_0135;
            }
            str = str + " origin: " + this.unitOrigin.name;
        Label_0135:
            return (str + " isNew: " + ((bool) this.isNew));
        }

        public int Rare
        {
            get
            {
                return this.rarity;
            }
            set
            {
                this.rarity = value;
                return;
            }
        }

        public enum Type
        {
            None,
            Item,
            Unit,
            Artifact,
            ConceptCard,
            End
        }
    }
}

