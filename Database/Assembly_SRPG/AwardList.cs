namespace SRPG
{
    using GR;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.UI;

    [Pin(0x33, "TabExtra", 0, 0x33), Pin(100, "Select", 1, 100), Pin(0x65, "SelectEnd", 1, 0x65), Pin(0x66, "ResetAward", 1, 0x66), Pin(50, "TabNormal", 0, 50), Pin(10, "StartUpEnd", 1, 10), Pin(1, "IsSelectAward", 0, 1), Pin(0, "StartUp", 0, 0)]
    public class AwardList : MonoBehaviour, IFlowInterface
    {
        [SerializeField]
        private GameObject AwardListRoot;
        [SerializeField]
        private Text Pager;
        [SerializeField]
        private SRPG_Button Prev;
        [SerializeField]
        private SRPG_Button Next;
        [SerializeField]
        private GameObject Blank;
        private List<GameObject> mAwardItems;
        private GameManager gm;
        private AwardParam[] mAwards;
        private List<string> mOpenAwards;
        private int mMaxViewItems;
        private int mCurrentPage;
        private int mMaxPage;
        private bool IsRefresh;
        private AwardParam.Tab mCurrentTab;
        private AwardParam.Tab mPrevTab;
        private Dictionary<AwardParam.Tab, List<AwardParam>> mAwardDatas;

        public AwardList()
        {
            this.mCurrentTab = -1;
            this.mPrevTab = -1;
            this.mAwardDatas = new Dictionary<AwardParam.Tab, List<AwardParam>>();
            base..ctor();
            return;
        }

        public void Activated(int pinID)
        {
            if (pinID != null)
            {
                goto Label_0047;
            }
            this.gm = MonoSingleton<GameManager>.GetInstanceDirect();
            if ((this.gm == null) == null)
            {
                goto Label_002D;
            }
            DebugUtility.LogWarning("AwardList.cs -> Activated():GameManager is Null References!");
            return;
        Label_002D:
            this.RefreshAwardDatas();
            this.TabChange(0);
            FlowNode_GameObject.ActivateOutputLinks(this, 10);
            goto Label_007D;
        Label_0047:
            if (pinID != 1)
            {
                goto Label_005A;
            }
            this.IsRefresh = 1;
            goto Label_007D;
        Label_005A:
            if (pinID != 50)
            {
                goto Label_006E;
            }
            this.TabChange(0);
            goto Label_007D;
        Label_006E:
            if (pinID != 0x33)
            {
                goto Label_007D;
            }
            this.TabChange(1);
        Label_007D:
            return;
        }

        private void Awake()
        {
            int num;
            int num2;
            Transform transform;
            if ((this.AwardListRoot != null) == null)
            {
                goto Label_0088;
            }
            num = this.AwardListRoot.get_transform().get_childCount();
            this.mAwardItems = new List<GameObject>();
            if (num <= 0)
            {
                goto Label_0081;
            }
            num2 = 0;
            goto Label_007A;
        Label_003B:
            transform = this.AwardListRoot.get_transform().GetChild(num2);
            if ((transform != null) == null)
            {
                goto Label_0076;
            }
            transform.get_gameObject().SetActive(0);
            this.mAwardItems.Add(transform.get_gameObject());
        Label_0076:
            num2 += 1;
        Label_007A:
            if (num2 < num)
            {
                goto Label_003B;
            }
        Label_0081:
            this.mMaxViewItems = num;
        Label_0088:
            if ((this.Blank != null) == null)
            {
                goto Label_00A5;
            }
            this.Blank.SetActive(0);
        Label_00A5:
            if ((this.Prev != null) == null)
            {
                goto Label_00D2;
            }
            this.Prev.get_onClick().AddListener(new UnityAction(this, this.OnPrevPage));
        Label_00D2:
            if ((this.Next != null) == null)
            {
                goto Label_00FF;
            }
            this.Next.get_onClick().AddListener(new UnityAction(this, this.OnNextPage));
        Label_00FF:
            return;
        }

        private AwardParam CreateRemoveAwardData()
        {
            AwardParam param;
            param = new AwardParam();
            param.grade = -1;
            param.iname = string.Empty;
            param.name = LocalizedText.Get("sys.TEXT_REMOVE_AWARD");
            return param;
        }

        private void OnNextPage()
        {
            this.mCurrentPage = Mathf.Min(this.mCurrentPage + 1, this.mMaxPage);
            this.Refresh();
            return;
        }

        private void OnPrevPage()
        {
            this.mCurrentPage = Mathf.Max(this.mCurrentPage - 1, 0);
            this.Refresh();
            return;
        }

        private void OnSelected(string select)
        {
            if (string.IsNullOrEmpty(select) != null)
            {
                goto Label_0023;
            }
            FlowNode_Variable.Set("CONFIRM_SELECT_AWARD", select);
            FlowNode_GameObject.ActivateOutputLinks(this, 100);
            goto Label_0036;
        Label_0023:
            FlowNode_Variable.Set("CONFIRM_SELECT_AWARD", select);
            FlowNode_GameObject.ActivateOutputLinks(this, 0x66);
        Label_0036:
            return;
        }

        private unsafe void Refresh()
        {
            object[] objArray2;
            object[] objArray1;
            GameObject obj2;
            List<GameObject>.Enumerator enumerator;
            AwardParam[] paramArray;
            int num;
            int num2;
            int num3;
            int num4;
            int num5;
            AwardParam param;
            GameObject obj3;
            AwardListItem item;
            bool flag;
            Button button;
            <Refresh>c__AnonStorey300 storey;
            <Refresh>c__AnonStorey301 storey2;
            int num6;
            if ((this.mAwardItems != null) && (this.mAwardItems.Count > 0))
            {
                goto Label_001D;
            }
            return;
        Label_001D:
            if (this.mAwardDatas != null)
            {
                goto Label_0029;
            }
            return;
        Label_0029:
            enumerator = this.mAwardItems.GetEnumerator();
        Label_0035:
            try
            {
                goto Label_0049;
            Label_003A:
                obj2 = &enumerator.Current;
                obj2.SetActive(0);
            Label_0049:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_003A;
                }
                goto Label_0066;
            }
            finally
            {
            Label_005A:
                ((List<GameObject>.Enumerator) enumerator).Dispose();
            }
        Label_0066:
            this.Blank.SetActive(0);
            paramArray = (this.mAwardDatas.ContainsKey(this.mCurrentTab) == null) ? null : this.mAwardDatas[this.mCurrentTab].ToArray();
            if ((paramArray != null) && (((int) paramArray.Length) > 0))
            {
                goto Label_0139;
            }
            this.Blank.SetActive(1);
            if ((this.Pager != null) == null)
            {
                goto Label_00FE;
            }
            objArray1 = new object[] { (int) 0, (int) 0 };
            this.Pager.set_text(LocalizedText.Get("sys.TEXT_PAGER_TEMP", objArray1));
        Label_00FE:
            if (((this.Prev != null) == null) || ((this.Next != null) == null))
            {
                goto Label_0138;
            }
            this.Prev.set_interactable(0);
            this.Next.set_interactable(0);
        Label_0138:
            return;
        Label_0139:
            num = (int) paramArray.Length;
            num2 = this.mMaxViewItems * this.mCurrentPage;
            num2 = (num2 >= num) ? 0 : num2;
            num3 = this.mMaxViewItems * (this.mCurrentPage + 1);
            num3 = (num3 >= num) ? num : num3;
            num4 = num2;
            num5 = 0;
            goto Label_031C;
        Label_018D:
            param = (num4 >= ((int) paramArray.Length)) ? null : paramArray[num4];
            if (param != null)
            {
                goto Label_01AF;
            }
            goto Label_0316;
        Label_01AF:
            obj3 = this.mAwardItems[num5];
            if ((obj3 != null) == null)
            {
                goto Label_0307;
            }
            item = obj3.GetComponent<AwardListItem>();
            if ((item != null) == null)
            {
                goto Label_0307;
            }
            obj3.SetActive(1);
            obj3.SetActive(1);
            flag = (this.mOpenAwards == null) ? 0 : this.mOpenAwards.Contains(param.iname);
            item.SetUp(param.iname, flag, param.iname == this.gm.Player.SelectedAward, (param.grade > 0) == 0);
            button = obj3.GetComponent<SRPG_Button>();
            if ((button != null) == null)
            {
                goto Label_0307;
            }
            button.get_onClick().RemoveAllListeners();
            if (param.grade > 0)
            {
                goto Label_02C1;
            }
            storey = new <Refresh>c__AnonStorey300();
            storey.<>f__this = this;
            storey.iname = param.iname;
            button.get_onClick().AddListener(new UnityAction(storey, this.<>m__293));
            button.set_interactable(1);
            goto Label_0307;
        Label_02C1:
            if (flag == null)
            {
                goto Label_02FE;
            }
            storey2 = new <Refresh>c__AnonStorey301();
            storey2.<>f__this = this;
            storey2.iname = param.iname;
            button.get_onClick().AddListener(new UnityAction(storey2, this.<>m__294));
        Label_02FE:
            button.set_interactable(flag);
        Label_0307:
            if (num4 > num3)
            {
                goto Label_0316;
            }
            num4 += 1;
        Label_0316:
            num5 += 1;
        Label_031C:
            if (num5 < this.mAwardItems.Count)
            {
                goto Label_018D;
            }
            if ((this.Pager != null) == null)
            {
                goto Label_037C;
            }
            objArray2 = new object[2];
            num6 = this.mCurrentPage + 1;
            objArray2[0] = &num6.ToString();
            objArray2[1] = &this.mMaxPage.ToString();
            this.Pager.set_text(LocalizedText.Get("sys.TEXT_PAGER_TEMP", objArray2));
        Label_037C:
            if ((this.Prev != null) == null)
            {
                goto Label_03D2;
            }
            if ((this.Next != null) == null)
            {
                goto Label_03D2;
            }
            this.Prev.set_interactable(((this.mCurrentPage - 1) < 0) == 0);
            this.Next.set_interactable((this.mCurrentPage + 1) < this.mMaxPage);
        Label_03D2:
            return;
        }

        private void RefreshAwardDatas()
        {
            AwardParam[] paramArray;
            object obj2;
            IEnumerator enumerator;
            AwardParam.Tab tab;
            int num;
            AwardParam param;
            AwardParam.Tab tab2;
            IDisposable disposable;
            this.mAwardDatas.Clear();
            paramArray = this.gm.MasterParam.GetAllAwards();
            if (paramArray == null)
            {
                goto Label_002B;
            }
            if (((int) paramArray.Length) > 0)
            {
                goto Label_0036;
            }
        Label_002B:
            DebugUtility.LogWarning("AwardList.cs => RefreshAwardDatas():awards is Null or Count 0.");
            return;
        Label_0036:
            enumerator = Enum.GetValues(typeof(AwardParam.Tab)).GetEnumerator();
        Label_004B:
            try
            {
                goto Label_0080;
            Label_0050:
                obj2 = enumerator.Current;
                tab = (int) obj2;
                if (this.mAwardDatas.ContainsKey(tab) != null)
                {
                    goto Label_0080;
                }
                this.mAwardDatas.Add(tab, new List<AwardParam>());
            Label_0080:
                if (enumerator.MoveNext() != null)
                {
                    goto Label_0050;
                }
                goto Label_00A5;
            }
            finally
            {
            Label_0090:
                disposable = enumerator as IDisposable;
                if (disposable != null)
                {
                    goto Label_009D;
                }
            Label_009D:
                disposable.Dispose();
            }
        Label_00A5:
            num = 0;
            goto Label_0148;
        Label_00AD:
            param = paramArray[num];
            if (paramArray != null)
            {
                goto Label_00BE;
            }
            goto Label_0142;
        Label_00BE:
            tab2 = param.tab;
            if (this.mAwardDatas.ContainsKey(tab2) != null)
            {
                goto Label_00EB;
            }
            this.mAwardDatas.Add(tab2, new List<AwardParam>());
        Label_00EB:
            if (tab2 != 1)
            {
                goto Label_012E;
            }
            if (this.mOpenAwards == null)
            {
                goto Label_0142;
            }
            if (this.mOpenAwards.Contains(param.iname) == null)
            {
                goto Label_0142;
            }
            this.mAwardDatas[tab2].Add(param);
            goto Label_0142;
        Label_012E:
            this.mAwardDatas[tab2].Add(param);
        Label_0142:
            num += 1;
        Label_0148:
            if (num < ((int) paramArray.Length))
            {
                goto Label_00AD;
            }
            this.mAwardDatas[0].Insert(0, this.CreateRemoveAwardData());
            if (this.mAwardDatas[1].Count <= 0)
            {
                goto Label_0199;
            }
            this.mAwardDatas[1].Insert(0, this.CreateRemoveAwardData());
        Label_0199:
            return;
        }

        public void SetOpenAwards(string[] awards)
        {
            int num;
            if (awards == null)
            {
                goto Label_000F;
            }
            if (((int) awards.Length) > 0)
            {
                goto Label_0010;
            }
        Label_000F:
            return;
        Label_0010:
            this.mOpenAwards = new List<string>((int) awards.Length);
            num = 0;
            goto Label_0044;
        Label_0025:
            if (string.IsNullOrEmpty(awards[num]) != null)
            {
                goto Label_0040;
            }
            this.mOpenAwards.Add(awards[num]);
        Label_0040:
            num += 1;
        Label_0044:
            if (num < ((int) awards.Length))
            {
                goto Label_0025;
            }
            return;
        }

        private void Start()
        {
        }

        private void TabChange(AwardParam.Tab tab)
        {
            this.mPrevTab = this.mCurrentTab;
            if (this.mPrevTab != tab)
            {
                goto Label_0019;
            }
            return;
        Label_0019:
            this.mCurrentTab = tab;
            if (this.mAwardDatas.ContainsKey(this.mCurrentTab) != null)
            {
                goto Label_0037;
            }
            return;
        Label_0037:
            this.mMaxPage = ((this.mAwardDatas[this.mCurrentTab].Count % this.mMaxViewItems) <= 0) ? (this.mAwardDatas[this.mCurrentTab].Count / this.mMaxViewItems) : ((this.mAwardDatas[this.mCurrentTab].Count / this.mMaxViewItems) + 1);
            this.mCurrentPage = 0;
            this.IsRefresh = 1;
            return;
        }

        private void Update()
        {
            if (this.IsRefresh == null)
            {
                goto Label_0018;
            }
            this.IsRefresh = 0;
            this.Refresh();
        Label_0018:
            return;
        }

        [CompilerGenerated]
        private sealed class <Refresh>c__AnonStorey300
        {
            internal string iname;
            internal AwardList <>f__this;

            public <Refresh>c__AnonStorey300()
            {
                base..ctor();
                return;
            }

            internal void <>m__293()
            {
                this.<>f__this.OnSelected(this.iname);
                return;
            }
        }

        [CompilerGenerated]
        private sealed class <Refresh>c__AnonStorey301
        {
            internal string iname;
            internal AwardList <>f__this;

            public <Refresh>c__AnonStorey301()
            {
                base..ctor();
                return;
            }

            internal void <>m__294()
            {
                this.<>f__this.OnSelected(this.iname);
                return;
            }
        }
    }
}

