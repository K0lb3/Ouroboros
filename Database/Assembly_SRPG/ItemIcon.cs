namespace SRPG
{
    using GR;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using UnityEngine;
    using UnityEngine.UI;

    public class ItemIcon : BaseIcon
    {
        protected const string TooltipPath = "UI/ItemTooltip";
        protected const string ICON_NAME_UNKNOWN = "IT_UNKNOWN";
        [Space(10f)]
        public GameParameter.ItemInstanceTypes InstanceType;
        public int InstanceIndex;
        [Space(10f)]
        public RawImage Icon;
        public Image Frame;
        public Text Num;
        public Slider NumSlider;
        public bool Tooltip;
        public Text HaveNum;
        public bool IsSecret;
        protected ItemParam mSecretItemParam;
        [Description("個数表記GameObjectへの参照")]
        public GameObject SecretAmount;
        [Description("装備可能なユニットが存在する場合に表示状態を変更するバッジへの参照")]
        public Image SecretBadge;
        [Description("「？」アイコン⇒正規アイコン変更アニメ開始までの時間")]
        public float SecretWaitSec;
        [Description("「？」アイコン⇒正規アイコン変更アニメトリガー名")]
        public string SecretAnimName;
        [Description("「？」アイコン⇒正規アイコン変更アニメ開始後、アイコンを差し替えるまでの時間")]
        public float SecretAnimWaitSec;
        protected bool mReqExchgSecretIcon;

        public ItemIcon()
        {
            this.SecretWaitSec = 1f;
            this.SecretAnimName = string.Empty;
            this.SecretAnimWaitSec = 0.2f;
            base..ctor();
            return;
        }

        [DebuggerHidden]
        protected virtual IEnumerator exchgSecretIcon()
        {
            <exchgSecretIcon>c__Iterator105 iterator;
            iterator = new <exchgSecretIcon>c__Iterator105();
            iterator.<>f__this = this;
            return iterator;
        }

        public virtual void ExchgSecretIcon()
        {
            if (this.IsSecret == null)
            {
                goto Label_0021;
            }
            if (this.mReqExchgSecretIcon != null)
            {
                goto Label_0021;
            }
            if (this.mSecretItemParam != null)
            {
                goto Label_0022;
            }
        Label_0021:
            return;
        Label_0022:
            this.mReqExchgSecretIcon = 1;
            base.StartCoroutine(this.exchgSecretIcon());
            return;
        }

        protected override unsafe void ShowTooltip(Vector2 screen)
        {
            RectTransform transform;
            SRPG.Tooltip tooltip;
            SRPG.Tooltip tooltip2;
            float num;
            Vector2 vector;
            float num2;
            Canvas canvas;
            RectTransform transform2;
            float num3;
            float num4;
            ItemParam param;
            int num5;
            Rect rect;
            Rect rect2;
            Rect rect3;
            transform = base.get_transform() as RectTransform;
            tooltip = AssetManager.Load<SRPG.Tooltip>("UI/ItemTooltip");
            if ((tooltip != null) == null)
            {
                goto Label_0145;
            }
            tooltip2 = Object.Instantiate<SRPG.Tooltip>(tooltip);
            LayoutRebuilder.ForceRebuildLayoutImmediate(tooltip2.Body);
            num = &tooltip2.Body.get_rect().get_width();
            vector = screen;
            num2 = &screen.x - (num / 2f);
            if (num2 >= 0f)
            {
                goto Label_0080;
            }
            vector += Vector2.get_right() * -num2;
        Label_0080:
            transform2 = base.get_transform().GetComponentInParent<Canvas>().get_rootCanvas().GetComponent<RectTransform>();
            LayoutRebuilder.ForceRebuildLayoutImmediate(transform2);
            num4 = &transform2.get_rect().get_width() - (&screen.x + (num / 2f));
            if (num4 >= 0f)
            {
                goto Label_00EA;
            }
            vector += Vector2.get_left() * -num4;
        Label_00EA:
            vector += (Vector2.get_up() * &transform.get_rect().get_height()) * 0.5f;
            SRPG.Tooltip.TooltipPosition = vector;
            SRPG_Extensions.GetInstanceData(this.InstanceType, this.InstanceIndex, base.get_gameObject(), &param, &num5);
            DataSource.Bind<ItemParam>(tooltip2.get_gameObject(), param);
        Label_0145:
            return;
        }

        public override unsafe void UpdateValue()
        {
            object[] objArray1;
            ItemParam param;
            int num;
            Sprite sprite;
            int num2;
            ItemData data;
            SRPG_Extensions.GetInstanceData(this.InstanceType, this.InstanceIndex, base.get_gameObject(), &param, &num);
            if (param == null)
            {
                goto Label_0202;
            }
            this.mSecretItemParam = param;
            if ((this.Icon != null) == null)
            {
                goto Label_00B1;
            }
            if (this.IsSecret == null)
            {
                goto Label_009B;
            }
            MonoSingleton<GameManager>.Instance.ApplyTextureAsync(this.Icon, AssetPath.ItemIcon("IT_UNKNOWN"));
            if (this.SecretAmount == null)
            {
                goto Label_007A;
            }
            this.SecretAmount.SetActive(0);
        Label_007A:
            if (this.SecretBadge == null)
            {
                goto Label_00B1;
            }
            this.SecretBadge.set_enabled(0);
            goto Label_00B1;
        Label_009B:
            MonoSingleton<GameManager>.Instance.ApplyTextureAsync(this.Icon, AssetPath.ItemIcon(param));
        Label_00B1:
            if ((this.Frame != null) == null)
            {
                goto Label_0130;
            }
            if (this.IsSecret == null)
            {
                goto Label_0118;
            }
            if (&GameSettings.Instance.ItemIcons.NormalFrames == null)
            {
                goto Label_0130;
            }
            if (((int) &GameSettings.Instance.ItemIcons.NormalFrames.Length) == null)
            {
                goto Label_0130;
            }
            this.Frame.set_sprite(&GameSettings.Instance.ItemIcons.NormalFrames[0]);
            goto Label_0130;
        Label_0118:
            sprite = GameSettings.Instance.GetItemFrame(param);
            this.Frame.set_sprite(sprite);
        Label_0130:
            if ((this.Num != null) == null)
            {
                goto Label_0153;
            }
            this.Num.set_text(&num.ToString());
        Label_0153:
            if ((this.NumSlider != null) == null)
            {
                goto Label_0179;
            }
            this.NumSlider.set_value(((float) num) / ((float) param.cap));
        Label_0179:
            if ((this.HaveNum != null) == null)
            {
                goto Label_0202;
            }
            num2 = -1;
            if ((param.iname == "$COIN") == null)
            {
                goto Label_01B6;
            }
            num2 = MonoSingleton<GameManager>.Instance.Player.Coin;
            goto Label_01D7;
        Label_01B6:
            data = MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemParam(param);
            if (data == null)
            {
                goto Label_01D7;
            }
            num2 = data.Num;
        Label_01D7:
            if (num2 < 0)
            {
                goto Label_0202;
            }
            objArray1 = new object[] { (int) num2 };
            this.HaveNum.set_text(LocalizedText.Get("sys.QUESTRESULT_REWARD_ITEM_HAVE", objArray1));
        Label_0202:
            return;
        }

        public override bool HasTooltip
        {
            get
            {
                ItemParam param;
                int num;
                if (this.Tooltip == null)
                {
                    goto Label_002E;
                }
                SRPG_Extensions.GetInstanceData(this.InstanceType, this.InstanceIndex, base.get_gameObject(), &param, &num);
                return ((param == null) == 0);
            Label_002E:
                return 0;
            }
        }

        [CompilerGenerated]
        private sealed class <exchgSecretIcon>c__Iterator105 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal int $PC;
            internal object $current;
            internal ItemIcon <>f__this;

            public <exchgSecretIcon>c__Iterator105()
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
                goto Label_01B4;
            Label_0025:
                if (this.<>f__this.SecretWaitSec <= 0f)
                {
                    goto Label_005C;
                }
                this.$current = new WaitForSeconds(this.<>f__this.SecretWaitSec);
                this.$PC = 1;
                goto Label_01B6;
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
                goto Label_01B6;
            Label_00C3:
                if (this.<>f__this.mSecretItemParam == null)
                {
                    goto Label_0149;
                }
                if ((this.<>f__this.Icon != null) == null)
                {
                    goto Label_010E;
                }
                this.<>f__this.Icon.set_texture(AssetManager.Load<Texture2D>(AssetPath.ItemIcon(this.<>f__this.mSecretItemParam)));
            Label_010E:
                if ((this.<>f__this.Frame != null) == null)
                {
                    goto Label_0149;
                }
                this.<>f__this.Frame.set_sprite(GameSettings.Instance.GetItemFrame(this.<>f__this.mSecretItemParam));
            Label_0149:
                if (this.<>f__this.SecretAmount == null)
                {
                    goto Label_016F;
                }
                this.<>f__this.SecretAmount.SetActive(1);
            Label_016F:
                if (this.<>f__this.SecretBadge == null)
                {
                    goto Label_0195;
                }
                this.<>f__this.SecretBadge.set_enabled(1);
            Label_0195:
                this.<>f__this.mReqExchgSecretIcon = 0;
                this.<>f__this.IsSecret = 0;
                this.$PC = -1;
            Label_01B4:
                return 0;
            Label_01B6:
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

