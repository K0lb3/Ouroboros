namespace SRPG
{
    using GR;
    using System;
    using UnityEngine;

    public class ConceptCardTrustRewardItemParam
    {
        public eRewardType reward_type;
        public string iname;
        public int reward_num;

        public ConceptCardTrustRewardItemParam()
        {
            base..ctor();
            return;
        }

        public bool Deserialize(JSON_ConceptCardTrustRewardItemParam json)
        {
            this.reward_type = json.reward_type;
            this.iname = json.reward_iname;
            this.reward_num = json.reward_num;
            return 1;
        }

        public Sprite GetArtifactSprite(ArtifactParam param)
        {
            int num;
            Sprite[] spriteArray;
            if (param != null)
            {
                goto Label_0008;
            }
            return null;
        Label_0008:
            num = param.rareini;
            spriteArray = GameSettings.Instance.ArtifactIcon_Frames;
            if (num < 0)
            {
                goto Label_002E;
            }
            if (num >= ((int) spriteArray.Length))
            {
                goto Label_002E;
            }
            return spriteArray[num];
        Label_002E:
            return null;
        }

        public Sprite GetConceptCardSprite(ConceptCardParam param)
        {
            Sprite[] spriteArray;
            if (param != null)
            {
                goto Label_0008;
            }
            return null;
        Label_0008:
            spriteArray = GameSettings.Instance.ConceptCardIcon_Rarity;
            if (param.rare < 0)
            {
                goto Label_0036;
            }
            if (param.rare >= ((int) spriteArray.Length))
            {
                goto Label_0036;
            }
            return spriteArray[param.rare];
        Label_0036:
            return null;
        }

        public Sprite GetFrameSprite()
        {
            ItemParam param;
            ArtifactParam param2;
            ConceptCardParam param3;
            eRewardType type;
            switch ((this.reward_type - 1))
            {
                case 0:
                    goto Label_0028;

                case 1:
                    goto Label_0045;

                case 2:
                    goto Label_0081;

                case 3:
                    goto Label_0081;

                case 4:
                    goto Label_0063;
            }
            goto Label_0081;
        Label_0028:
            param = MonoSingleton<GameManager>.Instance.GetItemParam(this.iname);
            return GameSettings.Instance.GetItemFrame(param);
        Label_0045:
            param2 = MonoSingleton<GameManager>.Instance.MasterParam.GetArtifactParam(this.iname);
            return this.GetArtifactSprite(param2);
        Label_0063:
            param3 = MonoSingleton<GameManager>.Instance.MasterParam.GetConceptCardParam(this.iname);
            return this.GetConceptCardSprite(param3);
        Label_0081:
            return null;
        }

        public string GetIconPath()
        {
            ItemParam param;
            ArtifactParam param2;
            ConceptCardParam param3;
            eRewardType type;
            switch ((this.reward_type - 1))
            {
                case 0:
                    goto Label_0028;

                case 1:
                    goto Label_0040;

                case 2:
                    goto Label_007A;

                case 3:
                    goto Label_007A;

                case 4:
                    goto Label_005D;
            }
            goto Label_007A;
        Label_0028:
            return AssetPath.ItemIcon(MonoSingleton<GameManager>.Instance.GetItemParam(this.iname));
        Label_0040:
            return AssetPath.ArtifactIcon(MonoSingleton<GameManager>.Instance.MasterParam.GetArtifactParam(this.iname));
        Label_005D:
            return AssetPath.ConceptCardIcon(MonoSingleton<GameManager>.Instance.MasterParam.GetConceptCardParam(this.iname));
        Label_007A:
            return string.Empty;
        }
    }
}

