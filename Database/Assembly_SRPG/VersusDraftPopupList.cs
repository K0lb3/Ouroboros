namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using UnityEngine;
    using UnityEngine.UI;

    [Pin(1, "Generate List", 0, 1), Pin(11, "Generated", 1, 2), Pin(2, "Page Prev", 0, 3), Pin(3, "Page Next", 0, 4), Pin(0x15, "Is First Page", 1, 5), Pin(0x16, "Is Last Page", 1, 6)]
    public class VersusDraftPopupList : MonoBehaviour, IFlowInterface
    {
        private const int PIN_INPUT_GENERATE_LIST = 1;
        private const int PIN_INPUT_PAGE_PREV = 2;
        private const int PIN_INPUT_PAGE_NEXT = 3;
        private const int PIN_OUTPUT_GENERATED = 11;
        private const int PIN_OUTPUT_FIRST_PAGE = 0x15;
        private const int PIN_OUTPUT_LAST_PAGE = 0x16;
        private const int UNIT_COUNT_PER_PAGE = 0x1c;
        [SerializeField]
        private Transform mUnitParentTransform;
        [SerializeField]
        private GameObject mGOUnitItem;
        [SerializeField]
        private Text mPageMaxTxt;
        [SerializeField]
        private Text mPageNowTxt;
        private List<VersusDraftUnitParam> mDraftUnitListCache;
        private List<GameObject> mUnitList;
        private int mPage;
        private int mPageMax;
        [CompilerGenerated]
        private static Action<GameObject> <>f__am$cache8;

        public VersusDraftPopupList()
        {
            base..ctor();
            return;
        }

        [CompilerGenerated]
        private static void <GenerateList>m__495(GameObject go)
        {
            Object.Destroy(go);
            return;
        }

        public void Activated(int pinID)
        {
            int num;
            int num2;
            num = pinID;
            switch ((num - 1))
            {
                case 0:
                    goto Label_001B;

                case 1:
                    goto Label_002F;

                case 2:
                    goto Label_005E;
            }
            goto Label_0092;
        Label_001B:
            this.GenerateList(0);
            FlowNode_GameObject.ActivateOutputLinks(this, 11);
            goto Label_0092;
        Label_002F:
            if ((this.mPage - 1) >= 0)
            {
                goto Label_0042;
            }
            goto Label_0092;
        Label_0042:
            this.GenerateList(this.mPage -= 1);
            goto Label_0092;
        Label_005E:
            if ((this.mPage + 1) < this.mPageMax)
            {
                goto Label_0076;
            }
            goto Label_0092;
        Label_0076:
            this.GenerateList(this.mPage += 1);
        Label_0092:
            return;
        }

        private unsafe void Awake()
        {
            GameManager manager;
            manager = MonoSingleton<GameManager>.Instance;
            this.mDraftUnitListCache = manager.GetVersusDraftUnits(manager.VSDraftId);
            this.mGOUnitItem.SetActive(0);
            this.mUnitList = new List<GameObject>();
            this.mPage = 0;
            this.mPageMax = Mathf.CeilToInt(((float) this.mDraftUnitListCache.Count) / 28f);
            this.mPageMaxTxt.set_text(&this.mPageMax.ToString());
            return;
        }

        private unsafe void GenerateList(int page)
        {
            int num;
            UnitData data;
            int num2;
            Json_Unit unit;
            GameObject obj2;
            int num3;
            if (<>f__am$cache8 != null)
            {
                goto Label_001E;
            }
            <>f__am$cache8 = new Action<GameObject>(VersusDraftPopupList.<GenerateList>m__495);
        Label_001E:
            this.mUnitList.ForEach(<>f__am$cache8);
            this.mUnitList.Clear();
            num = 0;
            goto Label_00C1;
        Label_003A:
            data = null;
            num2 = num + (page * 0x1c);
            if (num2 >= this.mDraftUnitListCache.Count)
            {
                goto Label_0079;
            }
            unit = this.mDraftUnitListCache[num2].GetJson_Unit();
            if (unit == null)
            {
                goto Label_0079;
            }
            data = new UnitData();
            data.Deserialize(unit);
        Label_0079:
            obj2 = Object.Instantiate<GameObject>(this.mGOUnitItem);
            DataSource.Bind<UnitData>(obj2, data);
            GameParameter.UpdateAll(obj2);
            obj2.get_transform().SetParent(this.mUnitParentTransform, 0);
            obj2.SetActive(1);
            this.mUnitList.Add(obj2);
            num += 1;
        Label_00C1:
            if (num < 0x1c)
            {
                goto Label_003A;
            }
            num3 = this.mPage + 1;
            this.mPageNowTxt.set_text(&num3.ToString());
            if ((this.mPage - 1) >= 0)
            {
                goto Label_00FB;
            }
            FlowNode_GameObject.ActivateOutputLinks(this, 0x15);
        Label_00FB:
            if ((this.mPage + 1) < this.mPageMax)
            {
                goto Label_0116;
            }
            FlowNode_GameObject.ActivateOutputLinks(this, 0x16);
        Label_0116:
            return;
        }
    }
}

