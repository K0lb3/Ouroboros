namespace SRPG
{
    using GR;
    using System;
    using System.Runtime.CompilerServices;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.UI;

    public class ConceptCardLevelUpListItem : MonoBehaviour
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
        [SerializeField]
        private Text ConceptCardEnExp;
        [SerializeField]
        private ConceptCardIcon CardIcon;
        [SerializeField]
        private Text CardNum;
        [SerializeField]
        private GameObject ExpObject;
        [SerializeField]
        private int MaxCardNum;
        public SelectExpItem OnSelect;
        public ChangeToggleEvent ChangeUseMax;
        public CheckSliderValue OnCheck;
        private string mCurrentItemID;
        private int mMaxValue;
        private bool IsLock;
        private float mPrevValue;
        private ConceptCardMaterialData mConceptCardMaterialData;
        private ConceptCardData mCardData;

        public ConceptCardLevelUpListItem()
        {
            this.MaxCardNum = 0x3e7;
            this.mCurrentItemID = string.Empty;
            base..ctor();
            return;
        }

        public void AddConceptCardData(ConceptCardMaterialData material_data)
        {
            this.mConceptCardMaterialData = material_data;
            return;
        }

        public ConceptCardData GetConceptCardData()
        {
            return this.mCardData;
        }

        public string GetConceptCardIName()
        {
            if (this.mConceptCardMaterialData != null)
            {
                goto Label_000D;
            }
            return null;
        Label_000D:
            return this.mConceptCardMaterialData.IName;
        }

        private unsafe void Init()
        {
            ConceptCardParam param;
            JSON_ConceptCard card;
            ConceptCardManager manager;
            int num;
            int num2;
            float num3;
            float num4;
            if ((((this.UseExpItemSlider == null) == null) && ((this.SelectUseNum == null) == null)) && ((this.CardIcon == null) == null))
            {
                goto Label_0034;
            }
            return;
        Label_0034:
            if (this.mConceptCardMaterialData != null)
            {
                goto Label_0040;
            }
            return;
        Label_0040:
            param = MonoSingleton<GameManager>.Instance.MasterParam.GetConceptCardParam(this.mConceptCardMaterialData.IName);
            if (param != null)
            {
                goto Label_0067;
            }
            return;
        Label_0067:
            card = new JSON_ConceptCard();
            card.iid = this.mConceptCardMaterialData.UniqueID;
            card.iname = this.mConceptCardMaterialData.IName;
            card.exp = param.en_exp;
            card.trust = param.en_trust;
            this.mCardData = new ConceptCardData();
            this.mCardData.Deserialize(card);
            this.CardIcon.Setup(this.mCardData);
            this.mCurrentItemID = this.mConceptCardMaterialData.IName;
            manager = ConceptCardManager.Instance;
            if ((manager == null) == null)
            {
                goto Label_0103;
            }
            return;
        Label_0103:
            num = 0;
            if (this.mConceptCardMaterialData.Param.type != 2)
            {
                goto Label_018E;
            }
            num = Mathf.Min(manager.SelectedConceptCardData.GetExpToLevelMax() / this.mConceptCardMaterialData.Param.en_exp, this.mConceptCardMaterialData.Num);
            if (((this.mConceptCardMaterialData.Param.en_exp * num) >= manager.SelectedConceptCardData.GetExpToLevelMax()) || (num >= this.mConceptCardMaterialData.Num))
            {
                goto Label_0218;
            }
            num += 1;
            goto Label_0218;
        Label_018E:
            if (this.mConceptCardMaterialData.Param.type != 3)
            {
                goto Label_0217;
            }
            num = Mathf.Min(manager.SelectedConceptCardData.GetTrustToLevelMax() / this.mConceptCardMaterialData.Param.en_trust, this.mConceptCardMaterialData.Num);
            if (((this.mConceptCardMaterialData.Param.en_trust * num) >= manager.SelectedConceptCardData.GetTrustToLevelMax()) || (num >= this.mConceptCardMaterialData.Num))
            {
                goto Label_0218;
            }
            num += 1;
            goto Label_0218;
        Label_0217:
            return;
        Label_0218:
            this.UseExpItemSlider.get_onValueChanged().RemoveAllListeners();
            this.UseExpItemSlider.set_minValue(0f);
            this.UseExpItemSlider.set_maxValue((float) num);
            this.UseExpItemSlider.get_onValueChanged().AddListener(new UnityAction<float>(this, this.OnSelectUseNumChanged));
            this.UseExpItemSlider.set_value(this.UseExpItemSlider.get_minValue());
            this.SelectUseNum.set_text(&this.UseExpItemSlider.get_value().ToString());
            if ((this.UseItemNum != null) == null)
            {
                goto Label_02C6;
            }
            this.UseItemNum.set_text(&this.UseExpItemSlider.get_value().ToString());
        Label_02C6:
            if ((this.ConceptCardEnExp != null) == null)
            {
                goto Label_02F7;
            }
            this.ConceptCardEnExp.set_text(&this.mCardData.Param.en_exp.ToString());
        Label_02F7:
            this.mMaxValue = num;
            if ((this.CardNum != null) == null)
            {
                goto Label_033E;
            }
            num2 = Mathf.Min(this.mConceptCardMaterialData.Num, this.MaxCardNum);
            this.CardNum.set_text(&num2.ToString());
        Label_033E:
            if ((this.CheckUseMax != null) == null)
            {
                goto Label_036B;
            }
            this.CheckUseMax.onValueChanged.AddListener(new UnityAction<bool>(this, this.OnChangeUseMax));
        Label_036B:
            if ((this.PlusBtn != null) == null)
            {
                goto Label_03B3;
            }
            this.PlusBtn.set_interactable(((this.UseExpItemSlider != null) == null) ? 0 : (this.UseExpItemSlider.get_value() < this.UseExpItemSlider.get_maxValue()));
        Label_03B3:
            if ((this.MinusBtn != null) == null)
            {
                goto Label_03FB;
            }
            this.MinusBtn.set_interactable(((this.UseExpItemSlider != null) == null) ? 0 : (this.UseExpItemSlider.get_value() > this.UseExpItemSlider.get_minValue()));
        Label_03FB:
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
                goto Label_00DF;
            }
            this.PlusBtn.set_interactable((((this.UseExpItemSlider != null) == null) || (this.UseExpItemSlider.get_value() >= this.UseExpItemSlider.get_maxValue())) ? 0 : (this.IsLock == 0));
        Label_00DF:
            if ((this.MinusBtn != null) == null)
            {
                goto Label_0127;
            }
            this.MinusBtn.set_interactable(((this.UseExpItemSlider != null) == null) ? 0 : (this.UseExpItemSlider.get_value() > this.UseExpItemSlider.get_minValue()));
        Label_0127:
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

        public void SetExpObject(bool flag)
        {
            if ((this.ExpObject == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            this.ExpObject.SetActive(flag);
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

        public void SetUseMax(bool is_on)
        {
            this.CheckUseMax.set_isOn(is_on);
            return;
        }

        public void SetUseParamItemSliderValue(int value)
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

