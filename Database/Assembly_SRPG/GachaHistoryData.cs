namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using UnityEngine;

    public class GachaHistoryData
    {
        public GachaDropData.Type type;
        public UnitParam unit;
        public ItemParam item;
        public ArtifactParam artifact;
        public ConceptCardParam conceptcard;
        public int num;
        public bool isConvert;
        public bool isNew;
        public int rarity;
        [CompilerGenerated]
        private static Dictionary<string, int> <>f__switch$map13;

        public GachaHistoryData()
        {
            base..ctor();
            return;
        }

        public unsafe bool Deserialize(Json_GachaHistoryItem json)
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
            str = json.itype;
            if (str == null)
            {
                goto Label_02C9;
            }
            if (<>f__switch$map13 != null)
            {
                goto Label_0062;
            }
            dictionary = new Dictionary<string, int>(4);
            dictionary.Add("item", 0);
            dictionary.Add("unit", 1);
            dictionary.Add("artifact", 2);
            dictionary.Add("concept_card", 3);
            <>f__switch$map13 = dictionary;
        Label_0062:
            if (<>f__switch$map13.TryGetValue(str, &num) == null)
            {
                goto Label_02C9;
            }
            switch (num)
            {
                case 0:
                    goto Label_008F;

                case 1:
                    goto Label_00EE;

                case 2:
                    goto Label_014D;

                case 3:
                    goto Label_026A;
            }
            goto Label_02C9;
        Label_008F:
            this.type = 1;
            this.item = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.GetItemParam(json.iname);
            if (this.item != null)
            {
                goto Label_00D8;
            }
            DebugUtility.LogError("iname:" + json.iname + " => Not ItemParam!");
            return 0;
        Label_00D8:
            this.rarity = this.item.rare;
            goto Label_02C9;
        Label_00EE:
            this.type = 2;
            this.unit = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.GetUnitParam(json.iname);
            if (this.unit != null)
            {
                goto Label_0137;
            }
            DebugUtility.LogError("iname:" + json.iname + " => Not UnitParam!");
            return 0;
        Label_0137:
            this.rarity = this.unit.rare;
            goto Label_02C9;
        Label_014D:
            this.type = 3;
            this.artifact = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.GetArtifactParam(json.iname);
            if (this.artifact != null)
            {
                goto Label_0196;
            }
            DebugUtility.LogError("iname:" + json.iname + " => Not ArtifactParam!");
            return 0;
        Label_0196:
            if ((json.rare == -1) || (json.rare <= this.artifact.raremax))
            {
                goto Label_01DC;
            }
            DebugUtility.LogError("武具:" + this.artifact.name + "の最大レアリティより大きい値が設定されています.");
            goto Label_021D;
        Label_01DC:
            if ((json.rare == -1) || (json.rare >= this.artifact.rareini))
            {
                goto Label_021D;
            }
            DebugUtility.LogError("武具:" + this.artifact.name + "の初期レアリティより小さい値が設定されています.");
        Label_021D:
            this.rarity = (json.rare <= -1) ? this.artifact.rareini : Mathf.Min(Mathf.Max(this.artifact.rareini, json.rare), this.artifact.raremax);
            goto Label_02C9;
        Label_026A:
            this.type = 4;
            this.conceptcard = MonoSingleton<GameManager>.Instance.MasterParam.GetConceptCardParam(json.iname);
            if (this.conceptcard != null)
            {
                goto Label_02B3;
            }
            DebugUtility.LogError("iname:" + json.iname + " => Not ConceptCardParam!");
            return 0;
        Label_02B3:
            this.rarity = this.conceptcard.rare;
        Label_02C9:
            this.num = json.num;
            this.isConvert = json.convert_piece == 1;
            this.isNew = json.is_new == 1;
            return 1;
        }

        public void Init()
        {
            this.type = 0;
            this.unit = null;
            this.item = null;
            this.artifact = null;
            this.conceptcard = null;
            this.num = 0;
            this.isNew = 0;
            this.rarity = 0;
            return;
        }
    }
}

