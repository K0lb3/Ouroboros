namespace SRPG
{
    using System;
    using System.Runtime.InteropServices;
    using UnityEngine;

    public class RecommendTeamWindow : MonoBehaviour
    {
        [SerializeField]
        private ScrollablePulldown TypePullDown;
        [SerializeField]
        private ElementDropdown ElemmentPullDown;
        private readonly TypeAndStr[] items;
        private readonly ElemAndStr[] elements;
        private int currentTypeIndex;
        private int currentElemmentIndex;

        public unsafe RecommendTeamWindow()
        {
            ElemAndStr[] strArray2;
            TypeAndStr[] strArray1;
            strArray1 = new TypeAndStr[13];
            *(&(strArray1[0])) = new TypeAndStr(0, "sys.RECOMMEND_TEAM_SORT_TOTAL_TEXT");
            *(&(strArray1[1])) = new TypeAndStr(12, "sys.RECOMMEND_TEAM_SORT_HP_TEXT");
            *(&(strArray1[2])) = new TypeAndStr(1, "sys.RECOMMEND_TEAM_SORT_ATTACK_TEXT");
            *(&(strArray1[3])) = new TypeAndStr(2, "sys.RECOMMEND_TEAM_SORT_DEFENCE_TEXT");
            *(&(strArray1[4])) = new TypeAndStr(3, "sys.RECOMMEND_TEAM_SORT_MAGIC_TEXT");
            *(&(strArray1[5])) = new TypeAndStr(4, "sys.RECOMMEND_TEAM_SORT_MIND_TEXT");
            *(&(strArray1[6])) = new TypeAndStr(5, "sys.RECOMMEND_TEAM_SORT_SPEED_TEXT");
            *(&(strArray1[7])) = new TypeAndStr(6, "sys.RECOMMEND_TEAM_SORT_ATTACK_TYPE_SLASH_TEXT");
            *(&(strArray1[8])) = new TypeAndStr(7, "sys.RECOMMEND_TEAM_SORT_ATTACK_TYPE_STAB_TEXT");
            *(&(strArray1[9])) = new TypeAndStr(8, "sys.RECOMMEND_TEAM_SORT_ATTACK_TYPE_BLOW_TEXT");
            *(&(strArray1[10])) = new TypeAndStr(9, "sys.RECOMMEND_TEAM_SORT_ATTACK_TYPE_SHOT_TEXT");
            *(&(strArray1[11])) = new TypeAndStr(10, "sys.RECOMMEND_TEAM_SORT_ATTACK_TYPE_MAGIC_TEXT");
            *(&(strArray1[12])) = new TypeAndStr(11, "sys.RECOMMEND_TEAM_SORT_ATTACK_TYPE_NONE_TEXT");
            this.items = strArray1;
            strArray2 = new ElemAndStr[7];
            *(&(strArray2[0])) = new ElemAndStr(0, "sys.RECOMMEND_TEAM_ELEMENT_ALL_TEXT");
            *(&(strArray2[1])) = new ElemAndStr(1, "sys.RECOMMEND_TEAM_ELEMENT_FIRE_TEXT");
            *(&(strArray2[2])) = new ElemAndStr(2, "sys.RECOMMEND_TEAM_ELEMENT_WATER_TEXT");
            *(&(strArray2[3])) = new ElemAndStr(3, "sys.RECOMMEND_TEAM_ELEMENT_WIND_TEXT");
            *(&(strArray2[4])) = new ElemAndStr(4, "sys.RECOMMEND_TEAM_ELEMENT_THUNDER_TEXT");
            *(&(strArray2[5])) = new ElemAndStr(5, "sys.RECOMMEND_TEAM_ELEMENT_SHINE_TEXT");
            *(&(strArray2[6])) = new ElemAndStr(6, "sys.RECOMMEND_TEAM_ELEMENT_DARK_TEXT");
            this.elements = strArray2;
            base..ctor();
            return;
        }

        private void Awake()
        {
            this.TypePullDown.OnSelectionChangeDelegate = new ScrollablePulldownBase.SelectItemEvent(this.OnTypeItemSelect);
            this.ElemmentPullDown.OnSelectionChangeDelegate = new Pulldown.SelectItemEvent(this.OnElemmentItemSelect);
            if (GlobalVars.RecommendTeamSettingValue == null)
            {
                goto Label_0062;
            }
            this.currentTypeIndex = PartyUtility.RecommendTypeToComparatorOrder(GlobalVars.RecommendTeamSettingValue.recommendedType);
            this.currentElemmentIndex = GlobalVars.RecommendTeamSettingValue.recommendedElement;
            goto Label_0070;
        Label_0062:
            this.currentTypeIndex = 0;
            this.currentElemmentIndex = 0;
        Label_0070:
            this.Refresh();
            return;
        }

        public void Cancel()
        {
            GlobalVars.RecommendTeamSettingValue = null;
            return;
        }

        private void OnElemmentItemSelect(int value)
        {
            if (value == this.currentElemmentIndex)
            {
                goto Label_0013;
            }
            this.currentElemmentIndex = value;
        Label_0013:
            return;
        }

        private void OnTypeItemSelect(int value)
        {
            if (value < 0)
            {
                goto Label_0015;
            }
            if (value < ((int) this.items.Length))
            {
                goto Label_0016;
            }
        Label_0015:
            return;
        Label_0016:
            if (value == this.currentTypeIndex)
            {
                goto Label_0029;
            }
            this.currentTypeIndex = value;
        Label_0029:
            return;
        }

        private unsafe void Refresh()
        {
            int num;
            GameSettings settings;
            int num2;
            Sprite sprite;
            if ((this.TypePullDown != null) == null)
            {
                goto Label_007A;
            }
            this.TypePullDown.ClearItems();
            num = 0;
            goto Label_004A;
        Label_0023:
            this.TypePullDown.AddItem(LocalizedText.Get(&(this.items[num]).title), num);
            num += 1;
        Label_004A:
            if (num < ((int) this.items.Length))
            {
                goto Label_0023;
            }
            this.TypePullDown.Selection = this.currentTypeIndex;
            this.TypePullDown.get_gameObject().SetActive(1);
        Label_007A:
            if ((this.ElemmentPullDown != null) == null)
            {
                goto Label_012A;
            }
            this.ElemmentPullDown.ClearItems();
            settings = GameSettings.Instance;
            num2 = 0;
            goto Label_00FA;
        Label_00A3:
            sprite = null;
            if (num2 >= ((int) settings.Elements_IconSmall.Length))
            {
                goto Label_00D2;
            }
            if (num2 == null)
            {
                goto Label_00D2;
            }
            sprite = settings.Elements_IconSmall[&(this.elements[num2]).element];
        Label_00D2:
            this.ElemmentPullDown.AddItem(LocalizedText.Get(&(this.elements[num2]).title), sprite, num2);
            num2 += 1;
        Label_00FA:
            if (num2 < ((int) this.elements.Length))
            {
                goto Label_00A3;
            }
            this.ElemmentPullDown.Selection = this.currentElemmentIndex;
            this.ElemmentPullDown.get_gameObject().SetActive(1);
        Label_012A:
            return;
        }

        public unsafe void SaveSettings()
        {
            GlobalVars.RecommendType type;
            EElement element;
            type = &(this.items[this.currentTypeIndex]).type;
            element = &(this.elements[this.currentElemmentIndex]).element;
            if (Enum.IsDefined(typeof(GlobalVars.RecommendType), (GlobalVars.RecommendType) type) == null)
            {
                goto Label_0073;
            }
            if (Enum.IsDefined(typeof(EElement), (EElement) element) == null)
            {
                goto Label_0073;
            }
            GlobalVars.RecommendTeamSettingValue = new GlobalVars.RecommendTeamSetting(type, element);
            goto Label_0079;
        Label_0073:
            GlobalVars.RecommendTeamSettingValue = null;
        Label_0079:
            return;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct ElemAndStr
        {
            public readonly EElement element;
            public readonly string title;
            public ElemAndStr(EElement element, string title)
            {
                this.element = element;
                this.title = title;
                return;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct TypeAndStr
        {
            public readonly GlobalVars.RecommendType type;
            public readonly string title;
            public TypeAndStr(GlobalVars.RecommendType type, string title)
            {
                this.type = type;
                this.title = title;
                return;
            }
        }
    }
}

