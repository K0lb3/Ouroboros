namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using UnityEngine;

    [AddComponentMenu("UI/リスト/宿屋で表示するフレンドリスト"), Pin(1, "Refresh", 0, 1)]
    public class FriendList : MonoBehaviour, IFlowInterface
    {
        [Description("リストアイテムとして使用するゲームオブジェクト")]
        public GameObject ItemTemplate;
        [Description("リストが空のときに表示するゲームオブジェクト")]
        public GameObject ItemEmpty;
        [Description("表示するフレンドの種類")]
        public FriendStates FriendType;
        [Description("ソート用プルダウン")]
        public Pulldown SortPulldown;
        private string[] mSortString;
        private eSortType mSortType;
        private List<GameObject> mItems;
        private CanvasGroup mCanvasGroup;
        [CompilerGenerated]
        private static Comparison<FriendData> <>f__am$cache8;
        [CompilerGenerated]
        private static Comparison<FriendData> <>f__am$cache9;

        public FriendList()
        {
            string[] textArray1;
            textArray1 = new string[] { "sys.FRIEND_SORT_ENTRY_DATE", "sys.FRIEND_SORT_LAST_LOGIN", "sys.FRIEND_SORT_PLAYER_LEVEL" };
            this.mSortString = textArray1;
            this.mItems = new List<GameObject>();
            base..ctor();
            return;
        }

        [CompilerGenerated]
        private static int <SortByLastLogin>m__319(FriendData fr1, FriendData fr2)
        {
            return (int) (fr2.LastLogin - fr1.LastLogin);
        }

        [CompilerGenerated]
        private static int <SortByPlayerLevel>m__31A(FriendData fr1, FriendData fr2)
        {
            int num;
            num = fr2.PlayerLevel - fr1.PlayerLevel;
            if (num == null)
            {
                goto Label_0016;
            }
            return num;
        Label_0016:
            return string.Compare(fr2.CreatedAt, fr1.CreatedAt);
        }

        public void Activated(int pinID)
        {
            this.Refresh();
            return;
        }

        private void Awake()
        {
            if ((this.ItemTemplate != null) == null)
            {
                goto Label_002D;
            }
            if (this.ItemTemplate.get_activeInHierarchy() == null)
            {
                goto Label_002D;
            }
            this.ItemTemplate.SetActive(0);
        Label_002D:
            this.mCanvasGroup = base.GetComponent<CanvasGroup>();
            if ((this.mCanvasGroup == null) == null)
            {
                goto Label_005B;
            }
            this.mCanvasGroup = base.get_gameObject().AddComponent<CanvasGroup>();
        Label_005B:
            return;
        }

        private unsafe void entryItems()
        {
            List<FriendData> list;
            Transform transform;
            FriendData data;
            List<FriendData>.Enumerator enumerator;
            GameObject obj2;
            ListItemEvents events;
            FriendStates states;
            eSortType type;
            list = new List<FriendData>();
            switch ((this.FriendType - 1))
            {
                case 0:
                    goto Label_0028;

                case 1:
                    goto Label_0052;

                case 2:
                    goto Label_003D;
            }
            goto Label_0067;
        Label_0028:
            list = MonoSingleton<GameManager>.Instance.Player.Friends;
            goto Label_0068;
        Label_003D:
            list = MonoSingleton<GameManager>.Instance.Player.FriendsFollower;
            goto Label_0068;
        Label_0052:
            list = MonoSingleton<GameManager>.Instance.Player.FriendsFollow;
            goto Label_0068;
        Label_0067:
            return;
        Label_0068:
            if (list.Count != null)
            {
                goto Label_0074;
            }
            return;
        Label_0074:
            switch (this.mSortType)
            {
                case 0:
                    goto Label_0094;

                case 1:
                    goto Label_00A0;

                case 2:
                    goto Label_00AC;
            }
            goto Label_00B8;
        Label_0094:
            this.SortByEntryDate(list);
            goto Label_00B8;
        Label_00A0:
            this.SortByLastLogin(list);
            goto Label_00B8;
        Label_00AC:
            this.SortByPlayerLevel(list);
        Label_00B8:
            transform = base.get_transform();
            enumerator = list.GetEnumerator();
        Label_00C6:
            try
            {
                goto Label_0152;
            Label_00CB:
                data = &enumerator.Current;
                obj2 = Object.Instantiate<GameObject>(this.ItemTemplate);
                if (obj2 != null)
                {
                    goto Label_00F1;
                }
                goto Label_0152;
            Label_00F1:
                obj2.get_transform().SetParent(transform, 0);
                events = obj2.GetComponent<ListItemEvents>();
                if ((events != null) == null)
                {
                    goto Label_0128;
                }
                events.OnSelect = new ListItemEvents.ListItemEvent(this.OnSelectItem);
            Label_0128:
                DataSource.Bind<FriendData>(obj2, data);
                DataSource.Bind<UnitData>(obj2, data.Unit);
                obj2.SetActive(1);
                this.mItems.Add(obj2);
            Label_0152:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_00CB;
                }
                goto Label_016F;
            }
            finally
            {
            Label_0163:
                ((List<FriendData>.Enumerator) enumerator).Dispose();
            }
        Label_016F:
            return;
        }

        private void OnSelectItem(GameObject go)
        {
            FriendData data;
            FlowNode_OnFriendSelect select;
            data = DataSource.FindDataOfClass<FriendData>(go, null);
            if (data != null)
            {
                goto Label_000F;
            }
            return;
        Label_000F:
            GlobalVars.SelectedFriendID = data.FUID;
            GlobalVars.SelectedFriend = data;
            select = base.GetComponentInParent<FlowNode_OnFriendSelect>();
            if ((select == null) == null)
            {
                goto Label_0039;
            }
            select = Object.FindObjectOfType<FlowNode_OnFriendSelect>();
        Label_0039:
            if ((select != null) == null)
            {
                goto Label_004B;
            }
            select.Selected();
        Label_004B:
            return;
        }

        private void OnSortChange(int idx)
        {
            if (idx != this.mSortType)
            {
                goto Label_000D;
            }
            return;
        Label_000D:
            if (0 > idx)
            {
                goto Label_001B;
            }
            if (idx < 3)
            {
                goto Label_001C;
            }
        Label_001B:
            return;
        Label_001C:
            this.mSortType = idx;
            this.Refresh();
            if (string.IsNullOrEmpty(PlayerPrefsUtility.PREFS_KEY_FRIEND_SORT) != null)
            {
                goto Label_0045;
            }
            PlayerPrefsUtility.SetInt(PlayerPrefsUtility.PREFS_KEY_FRIEND_SORT, idx, 1);
        Label_0045:
            return;
        }

        private void Refresh()
        {
            int num;
            GameObject obj2;
            if ((this.mCanvasGroup != null) == null)
            {
                goto Label_0021;
            }
            this.mCanvasGroup.set_alpha(0f);
        Label_0021:
            num = 0;
            goto Label_004B;
        Label_0028:
            obj2 = this.mItems[num];
            if ((obj2 != null) == null)
            {
                goto Label_0047;
            }
            Object.Destroy(obj2);
        Label_0047:
            num += 1;
        Label_004B:
            if (num < this.mItems.Count)
            {
                goto Label_0028;
            }
            this.mItems.Clear();
            this.entryItems();
            if (this.mItems.Count <= 0)
            {
                goto Label_00A0;
            }
            if ((this.ItemEmpty != null) == null)
            {
                goto Label_00A0;
            }
            this.ItemEmpty.SetActive(0);
            goto Label_00AC;
        Label_00A0:
            this.ItemEmpty.SetActive(1);
        Label_00AC:
            return;
        }

        private void SortByEntryDate(List<FriendData> lists)
        {
            <SortByEntryDate>c__AnonStorey33B storeyb;
            storeyb = new <SortByEntryDate>c__AnonStorey33B();
            storeyb.created_at1 = DateTime.Now;
            storeyb.created_at2 = DateTime.Now;
            storeyb.str_datetime_fmt = TimeManager.ISO_8601_FORMAT;
            storeyb.ci = new CultureInfo("ja-JP");
            lists.Sort(new Comparison<FriendData>(storeyb.<>m__318));
            return;
        }

        private void SortByLastLogin(List<FriendData> lists)
        {
            if (<>f__am$cache8 != null)
            {
                goto Label_0019;
            }
            <>f__am$cache8 = new Comparison<FriendData>(FriendList.<SortByLastLogin>m__319);
        Label_0019:
            lists.Sort(<>f__am$cache8);
            return;
        }

        private void SortByPlayerLevel(List<FriendData> lists)
        {
            if (<>f__am$cache9 != null)
            {
                goto Label_0019;
            }
            <>f__am$cache9 = new Comparison<FriendData>(FriendList.<SortByPlayerLevel>m__31A);
        Label_0019:
            lists.Sort(<>f__am$cache9);
            return;
        }

        private void SortPulldownInit()
        {
            int num;
            int num2;
            if (string.IsNullOrEmpty(PlayerPrefsUtility.PREFS_KEY_FRIEND_SORT) != null)
            {
                goto Label_003F;
            }
            if (PlayerPrefsUtility.HasKey(PlayerPrefsUtility.PREFS_KEY_FRIEND_SORT) == null)
            {
                goto Label_003F;
            }
            num = PlayerPrefsUtility.GetInt(PlayerPrefsUtility.PREFS_KEY_FRIEND_SORT, 0);
            if (0 > num)
            {
                goto Label_003F;
            }
            if (num >= 3)
            {
                goto Label_003F;
            }
            this.mSortType = num;
        Label_003F:
            if ((this.SortPulldown != null) == null)
            {
                goto Label_00D3;
            }
            this.SortPulldown.OnSelectionChangeDelegate = new Pulldown.SelectItemEvent(this.OnSortChange);
            this.SortPulldown.ClearItems();
            num2 = 0;
            goto Label_0097;
        Label_0079:
            this.SortPulldown.AddItem(LocalizedText.Get(this.mSortString[num2]), num2);
            num2 += 1;
        Label_0097:
            if (num2 < ((int) this.mSortString.Length))
            {
                goto Label_0079;
            }
            this.SortPulldown.Selection = this.mSortType;
            this.SortPulldown.set_interactable(1);
            this.SortPulldown.get_gameObject().SetActive(1);
        Label_00D3:
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
            this.SortPulldownInit();
            this.Refresh();
            return;
        }

        private void Update()
        {
            if ((this.mCanvasGroup != null) == null)
            {
                goto Label_004D;
            }
            if (this.mCanvasGroup.get_alpha() >= 1f)
            {
                goto Label_004D;
            }
            this.mCanvasGroup.set_alpha(Mathf.Clamp01(this.mCanvasGroup.get_alpha() + (Time.get_unscaledDeltaTime() * 3.333333f)));
        Label_004D:
            return;
        }

        [CompilerGenerated]
        private sealed class <SortByEntryDate>c__AnonStorey33B
        {
            internal string str_datetime_fmt;
            internal CultureInfo ci;
            internal DateTime created_at1;
            internal DateTime created_at2;

            public <SortByEntryDate>c__AnonStorey33B()
            {
                base..ctor();
                return;
            }

            internal unsafe int <>m__318(FriendData fr1, FriendData fr2)
            {
                object[] objArray2;
                object[] objArray1;
                if (DateTime.TryParseExact(fr1.CreatedAt, this.str_datetime_fmt, this.ci, 0, &this.created_at1) != null)
                {
                    goto Label_004E;
                }
                objArray1 = new object[] { fr1.FUID, fr1.PlayerName, fr1.CreatedAt };
                Debug.LogWarningFormat("FriendList/SortByEntryDate ParseExact error! FUID={0}, PlayerName={1}, mCreatedAt={2}", objArray1);
            Label_004E:
                if (DateTime.TryParseExact(fr2.CreatedAt, this.str_datetime_fmt, this.ci, 0, &this.created_at2) != null)
                {
                    goto Label_009C;
                }
                objArray2 = new object[] { fr2.FUID, fr2.PlayerName, fr2.CreatedAt };
                Debug.LogWarningFormat("FriendList/SortByEntryDate ParseExact error! FUID={0}, PlayerName={1}, mCreatedAt={2}", objArray2);
            Label_009C:
                return (int) (TimeManager.FromDateTime(this.created_at2) - TimeManager.FromDateTime(this.created_at1));
            }
        }

        private enum eSortType
        {
            MIN = 0,
            ENTRY_DATE = 0,
            LAST_LOGIN = 1,
            PLAYER_LEVEL = 2,
            MAX = 3,
            DEFAULT = 0
        }
    }
}

