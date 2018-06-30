namespace SRPG
{
    using System;
    using System.Runtime.CompilerServices;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.UI;

    public class ArtifactLevelUpListItem : MonoBehaviour
    {
        [SerializeField]
        private Slider UseExpItemSlider;
        [SerializeField]
        private Button PlusBtn;
        [SerializeField]
        private Button MinusBtn;
        [SerializeField]
        private Text SelectUseNum;
        [SerializeField]
        private Toggle CheckUseMax;
        [SerializeField]
        private Text UseItemNum;
        public SelectExpItem OnSelect;
        public ChangeToggleEvent ChangeUseMax;
        public CheckSliderValue OnCheck;
        private string mCurrentItemID;
        private int mMaxValue;
        private bool IsLock;
        private float mPrevValue;

        public ArtifactLevelUpListItem()
        {
            this.mCurrentItemID = string.Empty;
            base..ctor();
            return;
        }

        private unsafe void Init()
        {
            ArtifactData data;
            ItemData data2;
            int num;
            int num2;
            float num3;
            float num4;
            if (((this.UseExpItemSlider == null) == null) && ((this.SelectUseNum == null) == null))
            {
                goto Label_0023;
            }
            return;
        Label_0023:
            data = DataSource.FindDataOfClass<ArtifactData>(base.get_gameObject(), null);
            if (data != null)
            {
                goto Label_0037;
            }
            return;
        Label_0037:
            data2 = DataSource.FindDataOfClass<ItemData>(base.get_gameObject(), null);
            if (data2 != null)
            {
                goto Label_004B;
            }
            return;
        Label_004B:
            num = data.GetGainExpCap() - data.Exp;
            num2 = Mathf.Max(1, Mathf.Min(data2.Num, Mathf.CeilToInt(((float) num) / ((float) data2.Param.value))));
            this.mCurrentItemID = data2.Param.iname;
            this.UseExpItemSlider.get_onValueChanged().RemoveAllListeners();
            this.UseExpItemSlider.set_minValue(0f);
            this.UseExpItemSlider.set_maxValue((float) num2);
            this.UseExpItemSlider.get_onValueChanged().AddListener(new UnityAction<float>(this, this.OnSelectUseNumChanged));
            this.UseExpItemSlider.set_value(this.UseExpItemSlider.get_minValue());
            this.SelectUseNum.set_text(&this.UseExpItemSlider.get_value().ToString());
            if ((this.UseItemNum != null) == null)
            {
                goto Label_013E;
            }
            this.UseItemNum.set_text(&this.UseExpItemSlider.get_value().ToString());
        Label_013E:
            if ((this.CheckUseMax != null) == null)
            {
                goto Label_016B;
            }
            this.CheckUseMax.onValueChanged.AddListener(new UnityAction<bool>(this, this.OnChangeUseMax));
        Label_016B:
            this.mMaxValue = num2;
            if ((this.PlusBtn != null) == null)
            {
                goto Label_01BA;
            }
            this.PlusBtn.set_interactable(((this.UseExpItemSlider != null) == null) ? 0 : (this.UseExpItemSlider.get_value() < this.UseExpItemSlider.get_maxValue()));
        Label_01BA:
            if ((this.MinusBtn != null) == null)
            {
                goto Label_0202;
            }
            this.MinusBtn.set_interactable(((this.UseExpItemSlider != null) == null) ? 0 : (this.UseExpItemSlider.get_value() > this.UseExpItemSlider.get_minValue()));
        Label_0202:
            return;
        }

        public bool IsUseMax()
        {
            if ((this.CheckUseMax == null) == null)
            {
                goto Label_0013;
            }
            return 0;
        Label_0013:
            return this.CheckUseMax.get_isOn();
        }

        private void OnAddNum()
        {
            if ((this.UseExpItemSlider != null) == null)
            {
                goto Label_0028;
            }
            this.UseExpItemSlider.set_value(this.UseExpItemSlider.get_value() + 1f);
        Label_0028:
            return;
        }

        public void OnChangeUseMax(bool value)
        {
            if (this.ChangeUseMax == null)
            {
                goto Label_001D;
            }
            this.ChangeUseMax(this.mCurrentItemID, value);
        Label_001D:
            return;
        }

        private void OnRemoveNum()
        {
            if ((this.UseExpItemSlider != null) == null)
            {
                goto Label_003B;
            }
            this.UseExpItemSlider.set_value(this.UseExpItemSlider.get_value() - 1f);
            this.IsLock = 0;
            this.PlusBtn.set_interactable(1);
        Label_003B:
            return;
        }

        private void OnSelectUseNumChanged(float value)
        {
            int num;
            if (this.OnCheck == null)
            {
                goto Label_0042;
            }
            if (value <= this.mPrevValue)
            {
                goto Label_0042;
            }
            num = this.OnCheck(this.mCurrentItemID, (int) value);
            if (num < 0)
            {
                goto Label_0042;
            }
            value = (float) num;
            this.UseExpItemSlider.set_value(value);
        Label_0042:
            if (value <= this.mPrevValue)
            {
                goto Label_006B;
            }
            if (this.IsLock == null)
            {
                goto Label_006B;
            }
            this.UseExpItemSlider.set_value(this.mPrevValue);
            return;
        Label_006B:
            if (this.OnSelect == null)
            {
                goto Label_0089;
            }
            this.OnSelect(this.mCurrentItemID, (int) value);
        Label_0089:
            this.Refresh(value);
            return;
        }

        private unsafe void Refresh(float value)
        {
            int num;
            int num2;
            if ((this.UseExpItemSlider == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            this.UseExpItemSlider.set_value((float) Mathf.Min(this.mMaxValue, (int) value));
            if ((this.SelectUseNum != null) == null)
            {
                goto Label_005B;
            }
            num = (int) this.UseExpItemSlider.get_value();
            this.SelectUseNum.set_text(&num.ToString());
        Label_005B:
            if ((this.UseItemNum != null) == null)
            {
                goto Label_008B;
            }
            num2 = (int) this.UseExpItemSlider.get_value();
            this.UseItemNum.set_text(&num2.ToString());
        Label_008B:
            if ((this.PlusBtn != null) == null)
            {
                goto Label_00E1;
            }
            this.PlusBtn.set_interactable((this.IsLock != null) ? 0 : (((this.UseExpItemSlider != null) == null) ? 0 : (this.UseExpItemSlider.get_value() < this.UseExpItemSlider.get_maxValue())));
        Label_00E1:
            if ((this.MinusBtn != null) == null)
            {
                goto Label_0129;
            }
            this.MinusBtn.set_interactable(((this.UseExpItemSlider != null) == null) ? 0 : (this.UseExpItemSlider.get_value() > this.UseExpItemSlider.get_minValue()));
        Label_0129:
            this.mPrevValue = this.UseExpItemSlider.get_value();
            return;
        }

        public void Reset()
        {
            if ((this.UseExpItemSlider != null) == null)
            {
                goto Label_0027;
            }
            this.UseExpItemSlider.set_value(this.UseExpItemSlider.get_minValue());
        Label_0027:
            if ((this.SelectUseNum != null) == null)
            {
                goto Label_0048;
            }
            this.SelectUseNum.set_text("0");
        Label_0048:
            return;
        }

        public void SetInputLock(bool islock)
        {
            if ((this.PlusBtn != null) == null)
            {
                goto Label_001D;
            }
            this.PlusBtn.set_interactable(islock);
        Label_001D:
            if ((this.UseExpItemSlider != null) == null)
            {
                goto Label_005A;
            }
            this.UseExpItemSlider.set_interactable((islock != null) ? islock : ((this.UseExpItemSlider.get_value() == 0f) == 0));
        Label_005A:
            this.IsLock = islock == 0;
            return;
        }

        public void SetUseExpItemSliderValue(int value)
        {
            if (value >= 0)
            {
                goto Label_0008;
            }
            return;
        Label_0008:
            this.UseExpItemSlider.get_onValueChanged().RemoveAllListeners();
            this.Refresh((float) value);
            this.UseExpItemSlider.get_onValueChanged().AddListener(new UnityAction<float>(this, this.OnSelectUseNumChanged));
            return;
        }

        public void SetUseMax(bool is_on)
        {
            this.CheckUseMax.set_isOn(is_on);
            return;
        }

        private void Start()
        {
            if ((this.PlusBtn != null) == null)
            {
                goto Label_002D;
            }
            this.PlusBtn.get_onClick().AddListener(new UnityAction(this, this.OnAddNum));
        Label_002D:
            if ((this.MinusBtn != null) == null)
            {
                goto Label_005A;
            }
            this.MinusBtn.get_onClick().AddListener(new UnityAction(this, this.OnRemoveNum));
        Label_005A:
            this.Init();
            return;
        }

        public delegate void ChangeToggleEvent(string iname, bool is_on);

        public delegate int CheckSliderValue(string iname, int value);

        public delegate void SelectExpItem(string iname, int value);
    }
}

