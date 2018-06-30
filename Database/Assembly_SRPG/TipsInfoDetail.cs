namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using UnityEngine;
    using UnityEngine.UI;

    [Pin(3, "閉じる", 0, 3), Pin(1, "次のページボタン", 0, 1), Pin(2, "前のページボタン", 0, 2)]
    public class TipsInfoDetail : MonoBehaviour, IFlowInterface
    {
        private const int PIN_NEXT_BUTTON = 1;
        private const int PIN_PREV_BUTTON = 2;
        private const int PIN_CLOSE = 3;
        [SerializeField]
        private ImageArray ImageData;
        [SerializeField]
        private Button PrevButton;
        [SerializeField]
        private Button NextButton;
        [SerializeField]
        private GameObject ParentPageIcon;
        [SerializeField]
        private GameObject TemplatePageIcon;
        [SerializeField]
        private Button CloseButton;
        [SerializeField]
        private BackHandler CloseButtonBackHandler;
        [SerializeField]
        private Text TitleText;
        private List<Toggle> mToggleIconList;
        private TipsParam mTipsParam;
        [CompilerGenerated]
        private static Func<TipsParam, bool> <>f__am$cacheA;

        public TipsInfoDetail()
        {
            this.mToggleIconList = new List<Toggle>();
            base..ctor();
            return;
        }

        [CompilerGenerated]
        private static bool <Start>m__41D(TipsParam t)
        {
            return (t.iname == GlobalVars.LastReadTips);
        }

        public void Activated(int pinID)
        {
            int num;
            num = pinID;
            switch ((num - 1))
            {
                case 0:
                    goto Label_001B;

                case 1:
                    goto Label_005E;

                case 2:
                    goto Label_0093;
            }
            goto Label_0098;
        Label_001B:
            if (this.ImageData.ImageIndex >= (((int) this.ImageData.Images.Length) - 1))
            {
                goto Label_0098;
            }
            this.ImageData.ImageIndex += 1;
            this.SetButtonInteractable();
            this.SetTobbleIcon();
            goto Label_0098;
        Label_005E:
            if (this.ImageData.ImageIndex <= 0)
            {
                goto Label_0098;
            }
            this.ImageData.ImageIndex -= 1;
            this.SetButtonInteractable();
            this.SetTobbleIcon();
            goto Label_0098;
        Label_0093:;
        Label_0098:
            return;
        }

        private void EnabledCloseButton(bool isEnable)
        {
            this.CloseButton.set_interactable(isEnable);
            this.CloseButtonBackHandler.set_enabled(isEnable);
            return;
        }

        private void SetButtonInteractable()
        {
            bool flag;
            if (((int) this.ImageData.Images.Length) != 1)
            {
                goto Label_0037;
            }
            this.NextButton.set_interactable(0);
            this.PrevButton.set_interactable(0);
            this.EnabledCloseButton(1);
            goto Label_00C4;
        Label_0037:
            if (this.ImageData.ImageIndex != (((int) this.ImageData.Images.Length) - 1))
            {
                goto Label_007A;
            }
            this.NextButton.set_interactable(0);
            this.PrevButton.set_interactable(1);
            this.EnabledCloseButton(1);
            goto Label_00C4;
        Label_007A:
            if (this.ImageData.ImageIndex != null)
            {
                goto Label_00C4;
            }
            this.NextButton.set_interactable(1);
            this.PrevButton.set_interactable(0);
            flag = MonoSingleton<GameManager>.Instance.Tips.Contains(this.mTipsParam.iname);
            this.EnabledCloseButton(flag);
        Label_00C4:
            return;
        }

        private void SetTobbleIcon()
        {
            int num;
            num = 0;
            goto Label_0045;
        Label_0007:
            if (num != this.ImageData.ImageIndex)
            {
                goto Label_002F;
            }
            this.mToggleIconList[num].set_isOn(1);
            goto Label_0041;
        Label_002F:
            this.mToggleIconList[num].set_isOn(0);
        Label_0041:
            num += 1;
        Label_0045:
            if (num < this.mToggleIconList.Count)
            {
                goto Label_0007;
            }
            return;
        }

        private unsafe void Start()
        {
            bool flag;
            List<Sprite> list;
            SpriteSheet sheet;
            string str;
            string[] strArray;
            int num;
            int num2;
            GameObject obj2;
            Vector2 vector;
            Toggle toggle;
            int num3;
            if (<>f__am$cacheA != null)
            {
                goto Label_0028;
            }
            <>f__am$cacheA = new Func<TipsParam, bool>(TipsInfoDetail.<Start>m__41D);
        Label_0028:
            this.mTipsParam = Enumerable.FirstOrDefault<TipsParam>(MonoSingleton<GameManager>.Instance.MasterParam.Tips, <>f__am$cacheA);
            if (this.mTipsParam != null)
            {
                goto Label_0043;
            }
            return;
        Label_0043:
            if ((this.TitleText != null) == null)
            {
                goto Label_006A;
            }
            this.TitleText.set_text(this.mTipsParam.title);
        Label_006A:
            if ((this.CloseButton != null) == null)
            {
                goto Label_009D;
            }
            flag = MonoSingleton<GameManager>.Instance.Tips.Contains(this.mTipsParam.iname);
            this.EnabledCloseButton(flag);
        Label_009D:
            list = new List<Sprite>();
            sheet = AssetManager.Load<SpriteSheet>("Tips/tips_images");
            if ((sheet != null) == null)
            {
                goto Label_0103;
            }
            if (this.mTipsParam.images == null)
            {
                goto Label_0103;
            }
            strArray = this.mTipsParam.images;
            num = 0;
            goto Label_00F8;
        Label_00DF:
            str = strArray[num];
            list.Add(sheet.GetSprite(str));
            num += 1;
        Label_00F8:
            if (num < ((int) strArray.Length))
            {
                goto Label_00DF;
            }
        Label_0103:
            this.ImageData.Images = list.ToArray();
            if ((this.ImageData == null) == null)
            {
                goto Label_0142;
            }
            if (((int) this.ImageData.Images.Length) != null)
            {
                goto Label_0142;
            }
            Debug.LogError("ImageData not data.");
            return;
        Label_0142:
            this.ImageData.ImageIndex = 0;
            this.TemplatePageIcon.SetActive(0);
            num2 = 0;
            goto Label_01F9;
        Label_0162:
            obj2 = Object.Instantiate<GameObject>(this.TemplatePageIcon);
            vector = obj2.get_transform().get_localScale();
            obj2.get_transform().SetParent(this.ParentPageIcon.get_transform());
            obj2.get_transform().set_localScale(vector);
            obj2.get_gameObject().SetActive(1);
            num3 = num2 + 1;
            obj2.set_name(this.TemplatePageIcon.get_name() + &num3.ToString());
            toggle = obj2.GetComponent<Toggle>();
            this.mToggleIconList.Add(toggle);
            num2 += 1;
        Label_01F9:
            if (num2 < ((int) this.ImageData.Images.Length))
            {
                goto Label_0162;
            }
            this.mToggleIconList[0].set_isOn(1);
            this.SetButtonInteractable();
            return;
        }
    }
}

