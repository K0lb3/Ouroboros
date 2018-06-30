namespace SRPG
{
    using GR;
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    public class GiftRecieveItem : MonoBehaviour
    {
        public RawImage Icon;
        public Image Frame;
        public Text NameText;
        public Text AddText;
        public Text NumText;
        [HeaderBar("▼IconとFrameの描画順自動入れかえを行うか？")]
        public bool EnableSwapIconFramePriority;

        public GiftRecieveItem()
        {
            base..ctor();
            return;
        }

        private Sprite GetFrameSprite(ArtifactParam param, int rarity)
        {
            Sprite sprite;
            sprite = this.Frame.get_sprite();
            if (param != null)
            {
                goto Label_0014;
            }
            return sprite;
        Label_0014:
            sprite = GameSettings.Instance.ArtifactIcon_Frames[rarity];
            return sprite;
        }

        private Sprite GetFrameSprite(ConceptCardParam param, int rarity)
        {
            Sprite sprite;
            sprite = this.Frame.get_sprite();
            if (param != null)
            {
                goto Label_0014;
            }
            return sprite;
        Label_0014:
            return GameSettings.Instance.GetConceptCardFrame(param);
        }

        private Sprite GetFrameSprite(ItemParam param, int rarity)
        {
            Sprite sprite;
            sprite = this.Frame.get_sprite();
            if (param != null)
            {
                goto Label_0014;
            }
            return sprite;
        Label_0014:
            return GameSettings.Instance.GetItemFrame(param);
        }

        private string GetIconPath(ArtifactParam param)
        {
            if (param != null)
            {
                goto Label_0008;
            }
            return null;
        Label_0008:
            return AssetPath.ArtifactIcon(param);
        }

        private string GetIconPath(ConceptCardParam param)
        {
            if (param != null)
            {
                goto Label_0008;
            }
            return null;
        Label_0008:
            return AssetPath.ConceptCardIcon(param);
        }

        private string GetIconPath(ItemParam param)
        {
            if (param != null)
            {
                goto Label_0008;
            }
            return null;
        Label_0008:
            return AssetPath.ItemIcon(param);
        }

        private string GetName(ArtifactParam param)
        {
            if (param != null)
            {
                goto Label_0008;
            }
            return null;
        Label_0008:
            return param.name;
        }

        private string GetName(ConceptCardParam param)
        {
            if (param != null)
            {
                goto Label_0008;
            }
            return null;
        Label_0008:
            return param.name;
        }

        private string GetName(ItemParam param)
        {
            if (param != null)
            {
                goto Label_0008;
            }
            return null;
        Label_0008:
            return param.name;
        }

        private void Start()
        {
            this.UpdateValue();
            return;
        }

        private void SwapIconFramePriority(bool iconIsTop)
        {
            int num;
            int num2;
            int num3;
            int num4;
            if ((this.Icon != null) == null)
            {
                goto Label_00A3;
            }
            if ((this.Frame != null) == null)
            {
                goto Label_00A3;
            }
            num = this.Icon.get_transform().GetSiblingIndex();
            num2 = this.Frame.get_transform().GetSiblingIndex();
            num3 = Mathf.Min(num, num2);
            num4 = Mathf.Max(num, num2);
            if (iconIsTop == null)
            {
                goto Label_0081;
            }
            this.Icon.get_transform().SetSiblingIndex(num4);
            this.Frame.get_transform().SetSiblingIndex(num3);
            goto Label_00A3;
        Label_0081:
            this.Icon.get_transform().SetSiblingIndex(num3);
            this.Frame.get_transform().SetSiblingIndex(num4);
        Label_00A3:
            return;
        }

        public unsafe void UpdateValue()
        {
            GiftRecieveItemData data;
            string str;
            Sprite sprite;
            string str2;
            string str3;
            ArtifactParam param;
            ConceptCardParam param2;
            ItemParam param3;
            AwardParam param4;
            ItemParam param5;
            GiftTypes types;
            data = DataSource.FindDataOfClass<GiftRecieveItemData>(base.get_gameObject(), null);
            if (data == null)
            {
                goto Label_023E;
            }
            str = null;
            sprite = null;
            str2 = null;
            str3 = null;
            types = data.type;
            if (types == 1L)
            {
                goto Label_00F7;
            }
            if (types == 0x40L)
            {
                goto Label_0063;
            }
            if (types == 0x80L)
            {
                goto Label_00F7;
            }
            if (types == 0x800L)
            {
                goto Label_013C;
            }
            if (types == 0x1000L)
            {
                goto Label_00AD;
            }
            goto Label_0195;
        Label_0063:
            param = MonoSingleton<GameManager>.Instance.MasterParam.GetArtifactParam(data.iname);
            str = this.GetIconPath(param);
            sprite = this.GetFrameSprite(param, data.rarity);
            str2 = this.GetName(param);
            str3 = &data.num.ToString();
            goto Label_0195;
        Label_00AD:
            param2 = MonoSingleton<GameManager>.Instance.MasterParam.GetConceptCardParam(data.iname);
            str = this.GetIconPath(param2);
            sprite = this.GetFrameSprite(param2, data.rarity);
            str2 = this.GetName(param2);
            str3 = &data.num.ToString();
            goto Label_0195;
        Label_00F7:
            param3 = MonoSingleton<GameManager>.Instance.GetItemParam(data.iname);
            str = this.GetIconPath(param3);
            sprite = this.GetFrameSprite(param3, data.rarity);
            str2 = this.GetName(param3);
            str3 = &data.num.ToString();
            goto Label_0195;
        Label_013C:
            param5 = MonoSingleton<GameManager>.Instance.GetAwardParam(data.iname).ToItemParam();
            str = this.GetIconPath(param5);
            sprite = this.GetFrameSprite(param5, data.rarity);
            str2 = LocalizedText.Get("sys.MAILBOX_ITEM_AWARD_RECEIVE") + this.GetName(param5);
            this.HideNumText = 0;
        Label_0195:
            if ((this.Icon != null) == null)
            {
                goto Label_01B7;
            }
            MonoSingleton<GameManager>.Instance.ApplyTextureAsync(this.Icon, str);
        Label_01B7:
            if ((this.Frame != null) == null)
            {
                goto Label_01D4;
            }
            this.Frame.set_sprite(sprite);
        Label_01D4:
            if ((this.NameText != null) == null)
            {
                goto Label_01F1;
            }
            this.NameText.set_text(str2);
        Label_01F1:
            if ((this.NumText != null) == null)
            {
                goto Label_020F;
            }
            this.NumText.set_text(str3);
        Label_020F:
            if (this.EnableSwapIconFramePriority == null)
            {
                goto Label_023E;
            }
            if (data.type != 0x1000L)
            {
                goto Label_0237;
            }
            this.SwapIconFramePriority(0);
            goto Label_023E;
        Label_0237:
            this.SwapIconFramePriority(1);
        Label_023E:
            return;
        }

        private bool HideNumText
        {
            set
            {
                GameUtility.SetGameObjectActive(this.AddText, value);
                GameUtility.SetGameObjectActive(this.NumText, value);
                return;
            }
        }
    }
}

