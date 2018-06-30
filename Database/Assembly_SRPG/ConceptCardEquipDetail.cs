namespace SRPG
{
    using GR;
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    [Pin(0, "更新", 0, 0), Pin(10, "更新終了", 1, 10)]
    public class ConceptCardEquipDetail : MonoBehaviour
    {
        public const int PIN_REFRESH = 0;
        public const int PIN_REFRESH_END = 10;
        [HeaderBar("▼ConceptCardDescriptionの参照方式"), SerializeField]
        private DescriptionInstanceType m_DescriptionInstanceType;
        [SerializeField]
        private ConceptCardDescription mConceptCardDescription;
        [SerializeField, HeaderBar("▼複製したConceptCardDescriptionを入れる親")]
        private RectTransform mConceptCardDescriptionRoot;
        [SerializeField]
        private GameObject mConceptCardIconRoot;
        [SerializeField]
        private Text mCardNameText;
        [SerializeField]
        private ConceptCardIcon mConceptCardIcon;
        [SerializeField]
        private Text mConceptCardNum;
        private ConceptCardData mConceptCardData;
        private UnitData mUnitData;
        private static UnitData s_UnitData;

        public ConceptCardEquipDetail()
        {
            base..ctor();
            return;
        }

        private bool CheckGetUnitFrame()
        {
            bool flag;
            SerializeValueList list;
            flag = 0;
            list = FlowNode_ButtonEvent.currentValue as SerializeValueList;
            if (list == null)
            {
                goto Label_001F;
            }
            flag = list.GetBool("is_first_get_unit");
        Label_001F:
            return flag;
        }

        private void OnDestroy()
        {
            s_UnitData = null;
            return;
        }

        private void Refresh()
        {
            if (this.mConceptCardData != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            this.SetText(this.mCardNameText, this.mConceptCardData.Param.name);
            if ((this.mConceptCardIconRoot != null) == null)
            {
                goto Label_0045;
            }
            this.mConceptCardIconRoot.SetActive(1);
        Label_0045:
            if ((this.mConceptCardIcon != null) == null)
            {
                goto Label_0067;
            }
            this.mConceptCardIcon.Setup(this.mConceptCardData);
        Label_0067:
            this.SetText(this.mConceptCardNum, MonoSingleton<GameManager>.Instance.Player.GetConceptCardNum(this.mConceptCardData.Param.iname));
            return;
        }

        public void SetParam()
        {
            bool flag;
            this.mConceptCardData = GlobalVars.SelectedConceptCardData;
            this.mUnitData = s_UnitData;
            if (this.mConceptCardData != null)
            {
                goto Label_0027;
            }
            return;
        Label_0027:
            this.Refresh();
            flag = this.CheckGetUnitFrame();
            this.mConceptCardDescription.SetConceptCardData(this.mConceptCardData, base.get_gameObject(), 0, flag, this.mUnitData);
            return;
        }

        public static void SetSelectedUnitData(UnitData mUnitData)
        {
            s_UnitData = mUnitData;
            return;
        }

        public unsafe void SetText(Text text, int value)
        {
            this.SetText(text, &value.ToString());
            return;
        }

        public void SetText(Text text, string str)
        {
            if ((text != null) == null)
            {
                goto Label_0013;
            }
            text.set_text(str);
        Label_0013:
            return;
        }

        private void Start()
        {
            if (this.m_DescriptionInstanceType != 1)
            {
                goto Label_0034;
            }
            this.mConceptCardDescription = Object.Instantiate<ConceptCardDescription>(this.mConceptCardDescription);
            this.mConceptCardDescription.get_transform().SetParent(this.mConceptCardDescriptionRoot, 0);
        Label_0034:
            this.SetParam();
            return;
        }

        private enum DescriptionInstanceType
        {
            DirectUse,
            PrefabInstantiate
        }
    }
}

