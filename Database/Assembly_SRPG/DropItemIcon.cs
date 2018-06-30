namespace SRPG
{
    using GR;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using UnityEngine;

    public class DropItemIcon : ItemIcon
    {
        private ConceptCardParam mSecretConceptCardParam;

        public DropItemIcon()
        {
            base..ctor();
            return;
        }

        [DebuggerHidden]
        protected override IEnumerator exchgSecretIcon()
        {
            <exchgSecretIcon>c__Iterator106 iterator;
            iterator = new <exchgSecretIcon>c__Iterator106();
            iterator.<>f__this = this;
            return iterator;
        }

        public override void ExchgSecretIcon()
        {
            if (base.IsSecret == null)
            {
                goto Label_002C;
            }
            if (base.mReqExchgSecretIcon != null)
            {
                goto Label_002C;
            }
            if (base.mSecretItemParam != null)
            {
                goto Label_002D;
            }
            if (this.mSecretConceptCardParam != null)
            {
                goto Label_002D;
            }
        Label_002C:
            return;
        Label_002D:
            base.mReqExchgSecretIcon = 1;
            base.StartCoroutine(this.exchgSecretIcon());
            return;
        }

        private Sprite GetFrameSprite(ConceptCardParam param, bool isSecret)
        {
            if (isSecret == null)
            {
                goto Label_002F;
            }
            return this.GetSecretFrameSprite(((base.Frame != null) == null) ? null : base.Frame.get_sprite());
        Label_002F:
            if (param != null)
            {
                goto Label_0037;
            }
            return null;
        Label_0037:
            return GameSettings.Instance.GetConceptCardFrame(param.rare);
        }

        private Sprite GetFrameSprite(ItemData data, bool isSecret)
        {
            return this.GetFrameSprite((data == null) ? null : data.Param, isSecret);
        }

        private Sprite GetFrameSprite(ItemParam param, bool isSecret)
        {
            if (isSecret == null)
            {
                goto Label_002F;
            }
            return this.GetSecretFrameSprite(((base.Frame != null) == null) ? null : base.Frame.get_sprite());
        Label_002F:
            if (param != null)
            {
                goto Label_0037;
            }
            return null;
        Label_0037:
            return GameSettings.Instance.GetItemFrame(param);
        }

        private int GetHaveNum(ConceptCardParam param, int default_value)
        {
            return default_value;
        }

        private int GetHaveNum(ItemParam param, int default_value)
        {
            ItemData data;
            if ((param.iname == "$COIN") == null)
            {
                goto Label_0025;
            }
            return MonoSingleton<GameManager>.Instance.Player.Coin;
        Label_0025:
            data = MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemParam(param);
            if (data == null)
            {
                goto Label_0043;
            }
            return data.Num;
        Label_0043:
            return default_value;
        }

        private string GetIconPath(ConceptCardParam param, bool isSecret)
        {
            if (isSecret == null)
            {
                goto Label_000D;
            }
            return this.GetSecretIconPath();
        Label_000D:
            if (param != null)
            {
                goto Label_0019;
            }
            return string.Empty;
        Label_0019:
            return AssetPath.ConceptCardIcon(param);
        }

        private string GetIconPath(ItemData data, bool isSecret)
        {
            return this.GetIconPath((data == null) ? null : data.Param, isSecret);
        }

        private string GetIconPath(ItemParam param, bool isSecret)
        {
            if (isSecret == null)
            {
                goto Label_000D;
            }
            return this.GetSecretIconPath();
        Label_000D:
            if (param != null)
            {
                goto Label_0019;
            }
            return string.Empty;
        Label_0019:
            return AssetPath.ItemIcon(param);
        }

        private void GetParam(ref ConceptCardParam conceptCardParam, ref QuestResult.DropItemData dropItemData)
        {
            *(conceptCardParam) = DataSource.FindDataOfClass<ConceptCardParam>(base.get_gameObject(), null);
            if (*(conceptCardParam) == null)
            {
                goto Label_0016;
            }
            return;
        Label_0016:
            *(dropItemData) = DataSource.FindDataOfClass<QuestResult.DropItemData>(base.get_gameObject(), null);
            if (*(dropItemData) == null)
            {
                goto Label_002C;
            }
            return;
        Label_002C:
            return;
        }

        private unsafe Sprite GetSecretFrameSprite(Sprite defaultSprite)
        {
            if (&GameSettings.Instance.ItemIcons.NormalFrames == null)
            {
                goto Label_003C;
            }
            if (((int) &GameSettings.Instance.ItemIcons.NormalFrames.Length) == null)
            {
                goto Label_003C;
            }
            return &GameSettings.Instance.ItemIcons.NormalFrames[0];
        Label_003C:
            return defaultSprite;
        }

        private string GetSecretIconPath()
        {
            return AssetPath.ItemIcon("IT_UNKNOWN");
        }

        private void LoadIcon(ConceptCardParam param, bool isSecret)
        {
            if ((base.Icon == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            base.Icon.set_texture(AssetManager.Load<Texture2D>(this.GetIconPath(param, isSecret)));
            return;
        }

        private void LoadIcon(ItemParam param, bool isSecret)
        {
            if ((base.Icon == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            base.Icon.set_texture(AssetManager.Load<Texture2D>(this.GetIconPath(param, isSecret)));
            return;
        }

        private void Refresh_ConceptCard(ConceptCardParam param)
        {
            if (param != null)
            {
                goto Label_0007;
            }
            return;
        Label_0007:
            this.mSecretConceptCardParam = param;
            this.SetIconAsync(param, base.IsSecret);
            this.SetFrameSprite(param, base.IsSecret);
            this.SwapIconFramePriority(base.IsSecret);
            if (base.IsSecret == null)
            {
                goto Label_0077;
            }
            if (base.SecretAmount == null)
            {
                goto Label_005B;
            }
            base.SecretAmount.SetActive(0);
        Label_005B:
            if (base.SecretBadge == null)
            {
                goto Label_0077;
            }
            base.SecretBadge.set_enabled(0);
        Label_0077:
            return;
        }

        private unsafe void Refresh_DropItem(QuestResult.DropItemData data)
        {
            object[] objArray1;
            int num;
            int num2;
            if (data != null)
            {
                goto Label_0007;
            }
            return;
        Label_0007:
            if (data.IsItem == null)
            {
                goto Label_0023;
            }
            this.Refresh_Item(data.itemParam);
            goto Label_003A;
        Label_0023:
            if (data.IsConceptCard == null)
            {
                goto Label_003A;
            }
            this.Refresh_ConceptCard(data.conceptCardParam);
        Label_003A:
            if ((base.Num != null) == null)
            {
                goto Label_0064;
            }
            base.Num.set_text(&data.Num.ToString());
        Label_0064:
            if ((base.HaveNum != null) == null)
            {
                goto Label_00D9;
            }
            num = -1;
            if (data.IsItem == null)
            {
                goto Label_0095;
            }
            num = this.GetHaveNum(data.itemParam, -1);
            goto Label_00AE;
        Label_0095:
            if (data.IsConceptCard == null)
            {
                goto Label_00AE;
            }
            num = this.GetHaveNum(data.conceptCardParam, -1);
        Label_00AE:
            if (num < 0)
            {
                goto Label_00D9;
            }
            objArray1 = new object[] { (int) num };
            base.HaveNum.set_text(LocalizedText.Get("sys.QUESTRESULT_REWARD_ITEM_HAVE", objArray1));
        Label_00D9:
            return;
        }

        private void Refresh_Item(ItemParam param)
        {
            if (param != null)
            {
                goto Label_0007;
            }
            return;
        Label_0007:
            this.SetIconAsync(param, base.IsSecret);
            this.SetFrameSprite(param, base.IsSecret);
            this.SwapIconFramePriority(1);
            if (base.IsSecret == null)
            {
                goto Label_006B;
            }
            if (base.SecretAmount == null)
            {
                goto Label_004F;
            }
            base.SecretAmount.SetActive(0);
        Label_004F:
            if (base.SecretBadge == null)
            {
                goto Label_006B;
            }
            base.SecretBadge.set_enabled(0);
        Label_006B:
            return;
        }

        private void SetFrameSprite(ConceptCardParam param, bool isSecret)
        {
            if ((base.Frame == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            base.Frame.set_sprite(this.GetFrameSprite(param, isSecret));
            return;
        }

        private void SetFrameSprite(ItemParam param, bool isSecret)
        {
            if ((base.Frame == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            base.Frame.set_sprite(this.GetFrameSprite(param, isSecret));
            return;
        }

        private void SetIconAsync(ConceptCardParam param, bool isSecret)
        {
            if ((base.Icon == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            MonoSingleton<GameManager>.Instance.ApplyTextureAsync(base.Icon, this.GetIconPath(param, isSecret));
            return;
        }

        private void SetIconAsync(ItemParam param, bool isSecret)
        {
            if ((base.Icon == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            MonoSingleton<GameManager>.Instance.ApplyTextureAsync(base.Icon, this.GetIconPath(param, isSecret));
            return;
        }

        protected override unsafe void ShowTooltip(Vector2 screen)
        {
            RectTransform transform;
            Tooltip tooltip;
            Tooltip tooltip2;
            ItemParam param;
            int num;
            string str;
            string str2;
            ConceptCardParam param2;
            QuestResult.DropItemData data;
            GameParameter parameter;
            GameParameter parameter2;
            CanvasStack stack;
            Rect rect;
            transform = base.get_transform() as RectTransform;
            Tooltip.TooltipPosition = screen + ((Vector2.get_up() * &transform.get_rect().get_height()) * 0.5f);
            tooltip = AssetManager.Load<Tooltip>("UI/ItemTooltip");
            if ((tooltip != null) == null)
            {
                goto Label_01FF;
            }
            tooltip2 = Object.Instantiate<Tooltip>(tooltip);
            param = null;
            num = 0;
            SRPG_Extensions.GetInstanceData(base.InstanceType, base.InstanceIndex, base.get_gameObject(), &param, &num);
            str = string.Empty;
            str2 = string.Empty;
            if (base.IsSecret == null)
            {
                goto Label_00A4;
            }
            str = "sys.ITEMTOOLTIP_SECRET_NAME";
            str2 = "sys.ITEMTOOLTIP_SECRET_DESC";
            goto Label_015E;
        Label_00A4:
            if (param == null)
            {
                goto Label_00CB;
            }
            str = param.name;
            str2 = param.Expr;
            DataSource.Bind<ItemParam>(tooltip2.get_gameObject(), param);
            goto Label_015E;
        Label_00CB:
            param2 = null;
            data = null;
            this.GetParam(&param2, &data);
            if (param2 == null)
            {
                goto Label_00F9;
            }
            str = param2.name;
            str2 = param2.expr;
            goto Label_015E;
        Label_00F9:
            if (data == null)
            {
                goto Label_015E;
            }
            if (data.IsItem == null)
            {
                goto Label_012D;
            }
            str = data.itemParam.name;
            str2 = data.itemParam.Expr;
            goto Label_0155;
        Label_012D:
            if (data.IsConceptCard == null)
            {
                goto Label_0155;
            }
            str = data.conceptCardParam.name;
            str2 = data.conceptCardParam.expr;
        Label_0155:
            num = data.Num;
        Label_015E:
            if (tooltip2.TextName == null)
            {
                goto Label_019C;
            }
            parameter = tooltip2.TextName.GetComponent<GameParameter>();
            if (parameter == null)
            {
                goto Label_018F;
            }
            parameter.set_enabled(0);
        Label_018F:
            tooltip2.TextName.set_text(str);
        Label_019C:
            if (tooltip2.TextDesc == null)
            {
                goto Label_01DA;
            }
            parameter2 = tooltip2.TextDesc.GetComponent<GameParameter>();
            if (parameter2 == null)
            {
                goto Label_01CD;
            }
            parameter2.set_enabled(0);
        Label_01CD:
            tooltip2.TextDesc.set_text(str2);
        Label_01DA:
            stack = tooltip2.GetComponent<CanvasStack>();
            if ((stack != null) == null)
            {
                goto Label_01FF;
            }
            stack.SystemModal = 1;
            stack.Priority = 1;
        Label_01FF:
            return;
        }

        private void SwapIconFramePriority(bool iconIsTop)
        {
            int num;
            int num2;
            int num3;
            int num4;
            if ((base.Icon != null) == null)
            {
                goto Label_00A3;
            }
            if ((base.Frame != null) == null)
            {
                goto Label_00A3;
            }
            num = base.Icon.get_transform().GetSiblingIndex();
            num2 = base.Frame.get_transform().GetSiblingIndex();
            num3 = Mathf.Min(num, num2);
            num4 = Mathf.Max(num, num2);
            if (iconIsTop == null)
            {
                goto Label_0081;
            }
            base.Icon.get_transform().SetSiblingIndex(num4);
            base.Frame.get_transform().SetSiblingIndex(num3);
            goto Label_00A3;
        Label_0081:
            base.Icon.get_transform().SetSiblingIndex(num3);
            base.Frame.get_transform().SetSiblingIndex(num4);
        Label_00A3:
            return;
        }

        public override unsafe void UpdateValue()
        {
            ItemParam param;
            int num;
            ConceptCardParam param2;
            QuestResult.DropItemData data;
            param = null;
            num = 0;
            SRPG_Extensions.GetInstanceData(base.InstanceType, base.InstanceIndex, base.get_gameObject(), &param, &num);
            if (param == null)
            {
                goto Label_0030;
            }
            base.UpdateValue();
            goto Label_005D;
        Label_0030:
            param2 = null;
            data = null;
            this.GetParam(&param2, &data);
            if (param2 == null)
            {
                goto Label_0050;
            }
            this.Refresh_ConceptCard(param2);
            goto Label_005D;
        Label_0050:
            if (data == null)
            {
                goto Label_005D;
            }
            this.Refresh_DropItem(data);
        Label_005D:
            return;
        }

        public override bool HasTooltip
        {
            get
            {
                ItemParam param;
                int num;
                ConceptCardParam param2;
                QuestResult.DropItemData data;
                if (base.Tooltip == null)
                {
                    goto Label_0052;
                }
                param = null;
                num = 0;
                SRPG_Extensions.GetInstanceData(base.InstanceType, base.InstanceIndex, base.get_gameObject(), &param, &num);
                if (param == null)
                {
                    goto Label_0032;
                }
                return 1;
            Label_0032:
                param2 = null;
                data = null;
                this.GetParam(&param2, &data);
                if (param2 == null)
                {
                    goto Label_0048;
                }
                return 1;
            Label_0048:
                if (data == null)
                {
                    goto Label_0050;
                }
                return 1;
            Label_0050:
                return 0;
            Label_0052:
                return 0;
            }
        }

        [CompilerGenerated]
        private sealed class <exchgSecretIcon>c__Iterator106 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal int $PC;
            internal object $current;
            internal DropItemIcon <>f__this;

            public <exchgSecretIcon>c__Iterator106()
            {
                base..ctor();
                return;
            }

            [DebuggerHidden]
            public void Dispose()
            {
                this.$PC = -1;
                return;
            }

            public bool MoveNext()
            {
                uint num;
                bool flag;
                num = this.$PC;
                this.$PC = -1;
                switch (num)
                {
                    case 0:
                        goto Label_0025;

                    case 1:
                        goto Label_005C;

                    case 2:
                        goto Label_00C3;
                }
                goto Label_01BB;
            Label_0025:
                if (this.<>f__this.SecretWaitSec <= 0f)
                {
                    goto Label_005C;
                }
                this.$current = new WaitForSeconds(this.<>f__this.SecretWaitSec);
                this.$PC = 1;
                goto Label_01BD;
            Label_005C:
                if (string.IsNullOrEmpty(this.<>f__this.SecretAnimName) != null)
                {
                    goto Label_008C;
                }
                GameUtility.SetAnimatorTrigger(this.<>f__this.get_gameObject(), this.<>f__this.SecretAnimName);
            Label_008C:
                if (this.<>f__this.SecretAnimWaitSec <= 0f)
                {
                    goto Label_00C3;
                }
                this.$current = new WaitForSeconds(this.<>f__this.SecretAnimWaitSec);
                this.$PC = 2;
                goto Label_01BD;
            Label_00C3:
                if (this.<>f__this.mSecretItemParam == null)
                {
                    goto Label_0106;
                }
                this.<>f__this.LoadIcon(this.<>f__this.mSecretItemParam, 0);
                this.<>f__this.SetFrameSprite(this.<>f__this.mSecretItemParam, 0);
                goto Label_0150;
            Label_0106:
                if (this.<>f__this.mSecretConceptCardParam == null)
                {
                    goto Label_0150;
                }
                this.<>f__this.LoadIcon(this.<>f__this.mSecretConceptCardParam, 0);
                this.<>f__this.SetFrameSprite(this.<>f__this.mSecretConceptCardParam, 0);
                this.<>f__this.SwapIconFramePriority(0);
            Label_0150:
                if (this.<>f__this.SecretAmount == null)
                {
                    goto Label_0176;
                }
                this.<>f__this.SecretAmount.SetActive(1);
            Label_0176:
                if (this.<>f__this.SecretBadge == null)
                {
                    goto Label_019C;
                }
                this.<>f__this.SecretBadge.set_enabled(1);
            Label_019C:
                this.<>f__this.mReqExchgSecretIcon = 0;
                this.<>f__this.IsSecret = 0;
                this.$PC = -1;
            Label_01BB:
                return 0;
            Label_01BD:
                return 1;
                return flag;
            }

            [DebuggerHidden]
            public void Reset()
            {
                throw new NotSupportedException();
            }

            object IEnumerator<object>.Current
            {
                [DebuggerHidden]
                get
                {
                    return this.$current;
                }
            }

            object IEnumerator.Current
            {
                [DebuggerHidden]
                get
                {
                    return this.$current;
                }
            }
        }
    }
}

