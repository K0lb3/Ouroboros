namespace SRPG
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.UI;

    [Pin(10, "Detail Request", 1, 10), Pin(11, "BlackList Request", 1, 11), Pin(2, "Prev", 0, 2), Pin(1, "Next", 0, 1), Pin(0, "Refresh", 0, 0)]
    public class BlackList : MonoBehaviour, IFlowInterface
    {
        [SerializeField]
        private Transform ItemRoot;
        [SerializeField]
        private GameObject ItemTemplate;
        [SerializeField]
        private GameObject ItemEmpty;
        [SerializeField]
        private Button Prev;
        [SerializeField]
        private Button Next;
        [SerializeField]
        private Text Pager;
        [SerializeField]
        private ListExtras ScrollView;
        [SerializeField]
        private Text ChatMaintenanceMsg;
        public int LimitView;
        private List<GameObject> mItems;
        private ChatBlackList mBlackList;
        private int mListTotal;
        private int mCurrentPage;

        public BlackList()
        {
            this.LimitView = 10;
            this.mItems = new List<GameObject>();
            this.mCurrentPage = 1;
            base..ctor();
            return;
        }

        public void Activated(int pinID)
        {
            int num;
            int num2;
            num2 = pinID;
            switch (num2)
            {
                case 0:
                    goto Label_0019;

                case 1:
                    goto Label_0024;

                case 2:
                    goto Label_00A1;
            }
            goto Label_00DB;
        Label_0019:
            this.Refresh();
            goto Label_00E0;
        Label_0024:
            if (this.mBlackList == null)
            {
                goto Label_00E0;
            }
            num = ((this.mBlackList.total % this.LimitView) != null) ? ((this.mBlackList.total / this.LimitView) + 1) : (this.mBlackList.total / this.LimitView);
            if ((this.mCurrentPage + 1) > num)
            {
                goto Label_00E0;
            }
            this.OnSelectPage(this.mCurrentPage + 1);
            this.mCurrentPage += 1;
            goto Label_00E0;
        Label_00A1:
            if (this.mBlackList == null)
            {
                goto Label_00E0;
            }
            if ((this.mCurrentPage - 1) <= 0)
            {
                goto Label_00E0;
            }
            this.OnSelectPage(this.mCurrentPage - 1);
            this.mCurrentPage -= 1;
            goto Label_00E0;
        Label_00DB:;
        Label_00E0:
            return;
        }

        private void Awake()
        {
            if ((this.ItemTemplate != null) == null)
            {
                goto Label_001D;
            }
            this.ItemTemplate.SetActive(0);
        Label_001D:
            if ((this.ItemEmpty != null) == null)
            {
                goto Label_003A;
            }
            this.ItemEmpty.SetActive(0);
        Label_003A:
            return;
        }

        private void OnDestroy()
        {
            FlowNode_Variable.Set("BLACKLIST_OFFSET", string.Empty);
            this.ResetBlackListItems();
            return;
        }

        private void OnSelectItems(string uid)
        {
            if (string.IsNullOrEmpty(uid) == null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            FlowNode_Variable.Set("SelectUserID", uid);
            FlowNode_Variable.Set("IsBlackList", "1");
            FlowNode_GameObject.ActivateOutputLinks(this, 10);
            return;
        }

        private unsafe void OnSelectPage(int offset)
        {
            FlowNode_Variable.Set("BLACKLIST_OFFSET", &offset.ToString());
            FlowNode_GameObject.ActivateOutputLinks(this, 11);
            return;
        }

        private unsafe void Refresh()
        {
            object[] objArray2;
            object[] objArray1;
            int num;
            GameObject obj2;
            BlackListItem item;
            SRPG_Button button;
            int num2;
            <Refresh>c__AnonStorey303 storey;
            FlowNode_Variable.Set("BLACKLIST_OFFSET", &this.mCurrentPage.ToString());
            this.ResetBlackListItems();
            if ((this.mBlackList != null) && (((int) this.mBlackList.lists.Length) > 0))
            {
                goto Label_009A;
            }
            if ((this.ItemEmpty != null) == null)
            {
                goto Label_0056;
            }
            this.ItemEmpty.SetActive(1);
        Label_0056:
            objArray1 = new object[] { "0", "0" };
            this.Pager.set_text(LocalizedText.Get("sys.TEXT_PAGER_TEMP", objArray1));
            this.Next.set_interactable(0);
            this.Prev.set_interactable(0);
            return;
        Label_009A:
            num = 0;
            goto Label_0138;
        Label_00A1:
            storey = new <Refresh>c__AnonStorey303();
            storey.<>f__this = this;
            obj2 = Object.Instantiate<GameObject>(this.ItemTemplate);
            obj2.get_transform().SetParent(this.ItemRoot, 0);
            item = obj2.GetComponent<BlackListItem>();
            storey.param = this.mBlackList.lists[num];
            item.Refresh(storey.param);
            button = obj2.GetComponent<SRPG_Button>();
            if ((button != null) == null)
            {
                goto Label_0121;
            }
            button.get_onClick().AddListener(new UnityAction(storey, this.<>m__29D));
        Label_0121:
            obj2.SetActive(1);
            this.mItems.Add(obj2);
            num += 1;
        Label_0138:
            if (num < ((int) this.mBlackList.lists.Length))
            {
                goto Label_00A1;
            }
            num2 = ((this.mBlackList.total % this.LimitView) != null) ? ((this.mBlackList.total / this.LimitView) + 1) : (this.mBlackList.total / this.LimitView);
            if ((this.Pager != null) == null)
            {
                goto Label_01D3;
            }
            objArray2 = new object[] { (int) this.mCurrentPage, (int) num2 };
            this.Pager.set_text(LocalizedText.Get("sys.TEXT_PAGER_TEMP", objArray2));
        Label_01D3:
            this.Next.set_interactable(((this.mCurrentPage + 1) > num2) ? 0 : 1);
            this.Prev.set_interactable(((this.mCurrentPage - 1) <= 0) ? 0 : 1);
            if ((this.ScrollView != null) == null)
            {
                goto Label_0235;
            }
            this.ScrollView.SetScrollPos(0f);
        Label_0235:
            return;
        }

        public void RefreshMaintenanceMessage(string message)
        {
            if (string.IsNullOrEmpty(message) != null)
            {
                goto Label_0028;
            }
            if ((this.ChatMaintenanceMsg != null) == null)
            {
                goto Label_0028;
            }
            this.ChatMaintenanceMsg.set_text(message);
        Label_0028:
            return;
        }

        private void ResetBlackListItems()
        {
            if (this.mItems == null)
            {
                goto Label_0032;
            }
            if (this.mItems.Count <= 0)
            {
                goto Label_0032;
            }
            GameUtility.DestroyGameObjects(this.mItems);
            this.mItems.Clear();
        Label_0032:
            return;
        }

        private void Start()
        {
            if ((this.ItemTemplate == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            return;
        }

        public ChatBlackList BList
        {
            get
            {
                return this.mBlackList;
            }
            set
            {
                this.mBlackList = value;
                return;
            }
        }

        public int ListTotal
        {
            get
            {
                return this.mListTotal;
            }
            set
            {
                this.mListTotal = value;
                return;
            }
        }

        [CompilerGenerated]
        private sealed class <Refresh>c__AnonStorey303
        {
            internal ChatBlackListParam param;
            internal BlackList <>f__this;

            public <Refresh>c__AnonStorey303()
            {
                base..ctor();
                return;
            }

            internal void <>m__29D()
            {
                this.<>f__this.OnSelectItems(this.param.uid);
                return;
            }
        }
    }
}

