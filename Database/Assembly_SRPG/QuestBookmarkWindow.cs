namespace SRPG
{
    using GR;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using UnityEngine;
    using UnityEngine.UI;

    [Pin(100, "クエスト選択", 1, 100), Pin(1, "PartyEditor2から戻ってきた", 0, 1)]
    public class QuestBookmarkWindow : MonoBehaviour, IFlowInterface
    {
        [SerializeField]
        private SRPG_Button ButtonBookmarkBeginEdit;
        [SerializeField]
        private SRPG_Button ButtonBookmarkEndEdit;
        [SerializeField]
        private SRPG_Button[] ButtonSections;
        [SerializeField]
        private GameObject ItemTemplate;
        [SerializeField]
        private GameObject ItemContainer;
        [SerializeField]
        private GameObject QuestSelectorTemplate;
        [SerializeField]
        private GameObject BookmarkNotFoundText;
        [SerializeField]
        private ScrollRect ScrollRectObj;
        [SerializeField]
        private Text TitleText;
        [SerializeField]
        private Text DescriptionText;
        private readonly string BookmarkTitle;
        private readonly string BookmarkEditTitle;
        private readonly string BookmarkDescription;
        private readonly string BookmarkEditDescription;
        private readonly string BookmarkSectionName;
        private readonly int MaxBookmarkCount;
        private string mLastSectionName;
        private Dictionary<string, List<ItemAndQuests>> mSectionToPieces;
        private List<ItemAndQuests> mBookmarkedPieces;
        private List<ItemAndQuests> mBookmarkedPiecesOrigin;
        private List<GameObject> mCurrentUnitObjects;
        private bool mIsBookmarkEditing;
        private string[] mAvailableSections;
        private bool mSelectQuestFlag;
        [CompilerGenerated]
        private static Func<QuestParam, bool> <>f__am$cache18;
        [CompilerGenerated]
        private static Func<DictionaryEntry, ItemAndQuests> <>f__am$cache19;
        [CompilerGenerated]
        private static Func<DictionaryEntry, ItemAndQuests> <>f__am$cache1A;
        [CompilerGenerated]
        private static Func<ItemAndQuests, string> <>f__am$cache1B;
        [CompilerGenerated]
        private static Func<ItemAndQuests, string> <>f__am$cache1C;
        [CompilerGenerated]
        private static Func<QuestParam, string> <>f__am$cache1D;

        public QuestBookmarkWindow()
        {
            this.BookmarkTitle = "sys.TITLE_QUESTBOOKMARK";
            this.BookmarkEditTitle = "sys.TITLE_QUESTBOOKMARK_EDITING";
            this.BookmarkDescription = "sys.TXT_QUESTBOOKMARK_DESCRIPTION";
            this.BookmarkEditDescription = "sys.TXT_QUESTBOOKMARK_DESCRIPTION_EDIT";
            this.BookmarkSectionName = string.Empty;
            this.MaxBookmarkCount = 20;
            this.mSectionToPieces = new Dictionary<string, List<ItemAndQuests>>();
            this.mBookmarkedPieces = new List<ItemAndQuests>();
            this.mBookmarkedPiecesOrigin = new List<ItemAndQuests>();
            this.mCurrentUnitObjects = new List<GameObject>();
            this.mAvailableSections = new string[0];
            base..ctor();
            return;
        }

        [CompilerGenerated]
        private static string <CreateUnitPanels>m__3BE(QuestParam quest)
        {
            return quest.iname;
        }

        [CompilerGenerated]
        private static bool <Initialize>m__3B8(QuestParam q)
        {
            return (q.type == 4);
        }

        [CompilerGenerated]
        private static unsafe ItemAndQuests <Initialize>m__3B9(DictionaryEntry kv)
        {
            ItemAndQuests quests;
            quests = new ItemAndQuests();
            quests.itemName = &kv.Key as string;
            quests.quests = &kv.Value as List<QuestParam>;
            return quests;
        }

        [CompilerGenerated]
        private static unsafe ItemAndQuests <Initialize>m__3BA(DictionaryEntry kv)
        {
            ItemAndQuests quests;
            quests = new ItemAndQuests();
            quests.itemName = &kv.Key as string;
            quests.quests = &kv.Value as List<QuestParam>;
            return quests;
        }

        [CompilerGenerated]
        private static string <OnBookmarkEndEditButtonClick>m__3BB(ItemAndQuests x)
        {
            return x.itemName;
        }

        [CompilerGenerated]
        private static string <OnBookmarkEndEditButtonClick>m__3BC(ItemAndQuests x)
        {
            return x.itemName;
        }

        public void Activated(int pinID)
        {
            if (pinID != 1)
            {
                goto Label_000E;
            }
            this.mSelectQuestFlag = 0;
        Label_000E:
            return;
        }

        private bool AddBookmark(ItemAndQuests item)
        {
            if (this.mBookmarkedPieces.Count > this.MaxBookmarkCount)
            {
                goto Label_0041;
            }
            this.mBookmarkedPieces.Add(item);
            if (this.mBookmarkedPieces.Count < this.MaxBookmarkCount)
            {
                goto Label_003F;
            }
            this.SetActivateWithoutBookmarkedUnit(0);
        Label_003F:
            return 1;
        Label_0041:
            return 0;
        }

        private void Awake()
        {
            SRPG_Button button;
            SRPG_Button[] buttonArray;
            int num;
            BookmarkToggleButton button2;
            if (this.ButtonSections == null)
            {
                goto Label_0049;
            }
            buttonArray = this.ButtonSections;
            num = 0;
            goto Label_0040;
        Label_0019:
            button = buttonArray[num];
            button2 = button.get_gameObject().GetComponent<BookmarkToggleButton>();
            if ((button2 != null) == null)
            {
                goto Label_003C;
            }
            button2.Activate(0);
        Label_003C:
            num += 1;
        Label_0040:
            if (num < ((int) buttonArray.Length))
            {
                goto Label_0019;
            }
        Label_0049:
            if ((this.ItemTemplate != null) == null)
            {
                goto Label_0066;
            }
            this.ItemTemplate.SetActive(0);
        Label_0066:
            if ((this.ButtonBookmarkBeginEdit != null) == null)
            {
                goto Label_009F;
            }
            this.ButtonBookmarkBeginEdit.AddListener(new SRPG_Button.ButtonClickEvent(this.OnBookmarkBeginEditButtonClick));
            this.ButtonBookmarkBeginEdit.get_gameObject().SetActive(1);
        Label_009F:
            if ((this.ButtonBookmarkEndEdit != null) == null)
            {
                goto Label_00D8;
            }
            this.ButtonBookmarkEndEdit.AddListener(new SRPG_Button.ButtonClickEvent(this.OnBookmarkEndEditButtonClick));
            this.ButtonBookmarkEndEdit.get_gameObject().SetActive(0);
        Label_00D8:
            this.ResetScrollPosition();
            this.RequestQuestBookmark();
            return;
        }

        private unsafe void CreateUnitPanels(IEnumerable<ItemAndQuests> targetPieces, string sectionName)
        {
            UnitParam[] paramArray;
            Dictionary<string, QuestParam> dictionary;
            IEnumerator<ItemAndQuests> enumerator;
            GameObject obj2;
            BookmarkUnit unit;
            bool flag;
            long num;
            bool flag2;
            IEnumerable<QuestParam> enumerable;
            QuestParam param;
            IEnumerator<QuestParam> enumerator2;
            QuestParam param2;
            UnitParam param3;
            <CreateUnitPanels>c__AnonStorey37D storeyd;
            <CreateUnitPanels>c__AnonStorey37C storeyc;
            <CreateUnitPanels>c__AnonStorey37E storeye;
            storeyd = new <CreateUnitPanels>c__AnonStorey37D();
            storeyd.sectionName = sectionName;
            paramArray = MonoSingleton<GameManager>.Instance.MasterParam.GetAllUnits();
            if (<>f__am$cache1D != null)
            {
                goto Label_0046;
            }
            <>f__am$cache1D = new Func<QuestParam, string>(QuestBookmarkWindow.<CreateUnitPanels>m__3BE);
        Label_0046:
            dictionary = Enumerable.ToDictionary<QuestParam, string>(MonoSingleton<GameManager>.Instance.Player.AvailableQuests, <>f__am$cache1D);
            storeyc = new <CreateUnitPanels>c__AnonStorey37C();
            enumerator = targetPieces.GetEnumerator();
        Label_005F:
            try
            {
                goto Label_0227;
            Label_0064:
                storeyc.itemQuests = enumerator.Current;
                storeye = new <CreateUnitPanels>c__AnonStorey37E();
                storeye.<>f__ref$893 = storeyd;
                storeye.<>f__ref$892 = storeyc;
                obj2 = Object.Instantiate<GameObject>(this.ItemTemplate);
                unit = obj2.GetComponent<BookmarkUnit>();
                flag = this.mBookmarkedPieces.Exists(new Predicate<ItemAndQuests>(storeye.<>m__3BF));
                unit.BookmarkIcon.SetActive(flag);
                num = Network.GetServerTime();
                flag2 = 0;
                if ((storeyd.sectionName == this.BookmarkSectionName) == null)
                {
                    goto Label_00FA;
                }
                enumerable = storeyc.itemQuests.quests;
                goto Label_011A;
            Label_00FA:
                enumerable = Enumerable.Where<QuestParam>(storeyc.itemQuests.quests, new Func<QuestParam, bool>(storeye.<>m__3C0));
            Label_011A:
                enumerator2 = enumerable.GetEnumerator();
            Label_0123:
                try
                {
                    goto Label_015C;
                Label_0128:
                    param = enumerator2.Current;
                    if (dictionary.TryGetValue(param.iname, &param2) == null)
                    {
                        goto Label_015C;
                    }
                    if (this.IsAvailableQuest(param2, num) == null)
                    {
                        goto Label_015C;
                    }
                    flag2 = 1;
                    goto Label_0168;
                Label_015C:
                    if (enumerator2.MoveNext() != null)
                    {
                        goto Label_0128;
                    }
                Label_0168:
                    goto Label_017A;
                }
                finally
                {
                Label_016D:
                    if (enumerator2 != null)
                    {
                        goto Label_0172;
                    }
                Label_0172:
                    enumerator2.Dispose();
                }
            Label_017A:
                unit.Overlay.SetActive(flag2 == 0);
                unit.Button.AddListener(new SRPG_Button.ButtonClickEvent(this.OnUnitSelect));
                storeye.itemParam = MonoSingleton<GameManager>.Instance.GetItemParam(storeyc.itemQuests.itemName);
                param3 = Enumerable.FirstOrDefault<UnitParam>(paramArray, new Func<UnitParam, bool>(storeye.<>m__3C1));
                DataSource.Bind<ItemParam>(obj2, storeye.itemParam);
                DataSource.Bind<UnitParam>(obj2, param3);
                DataSource.Bind<ItemAndQuests>(obj2, storeyc.itemQuests);
                obj2.get_transform().SetParent(this.ItemContainer.get_transform(), 0);
                this.mCurrentUnitObjects.Add(obj2);
                GameParameter.UpdateAll(obj2);
                obj2.SetActive(1);
            Label_0227:
                if (enumerator.MoveNext() != null)
                {
                    goto Label_0064;
                }
                goto Label_0242;
            }
            finally
            {
            Label_0237:
                if (enumerator != null)
                {
                    goto Label_023B;
                }
            Label_023B:
                enumerator.Dispose();
            }
        Label_0242:
            return;
        }

        private void DeleteBookmark(ItemAndQuests item)
        {
            int num;
            <DeleteBookmark>c__AnonStorey381 storey;
            storey = new <DeleteBookmark>c__AnonStorey381();
            storey.item = item;
            num = this.mBookmarkedPieces.FindIndex(new Predicate<ItemAndQuests>(storey.<>m__3C6));
            this.mBookmarkedPieces.RemoveAt(num);
            if (this.mBookmarkedPieces.Count >= this.MaxBookmarkCount)
            {
                goto Label_0054;
            }
            this.SetActivateWithoutBookmarkedUnit(1);
            this.SetDeactivateNotAvailableUnit();
        Label_0054:
            return;
        }

        private void EndBookmarkEditing()
        {
            this.ButtonBookmarkBeginEdit.get_gameObject().SetActive(1);
            this.ButtonBookmarkEndEdit.get_gameObject().SetActive(0);
            this.SetActivateWithoutBookmarkedUnit(1);
            this.SetDeactivateNotAvailableUnit();
            if ((this.TitleText != null) == null)
            {
                goto Label_0056;
            }
            this.TitleText.set_text(LocalizedText.Get(this.BookmarkTitle));
        Label_0056:
            if ((this.DescriptionText != null) == null)
            {
                goto Label_007D;
            }
            this.DescriptionText.set_text(LocalizedText.Get(this.BookmarkDescription));
        Label_007D:
            this.mIsBookmarkEditing = 0;
            return;
        }

        private unsafe void Initialize(JSON_Item[] bookmarkItems)
        {
            GameManager manager;
            PlayerData data;
            SectionParam[] paramArray;
            List<string> list;
            List<string> list2;
            long num;
            QuestParam param;
            QuestParam[] paramArray2;
            int num2;
            string str;
            List<string>.Enumerator enumerator;
            ChapterParam param2;
            Dictionary<string, List<QuestParam>> dictionary;
            IEnumerable<QuestParam> enumerable;
            QuestParam param3;
            IEnumerator<QuestParam> enumerator2;
            List<QuestParam> list3;
            int num3;
            JSON_Item item;
            JSON_Item[] itemArray;
            int num4;
            ItemParam param4;
            List<QuestParam> list4;
            ItemAndQuests quests;
            SRPG_Button button;
            SectionParam param5;
            SRPG_Button button2;
            BookmarkToggleButton button3;
            List<QuestParam> list5;
            OrderedDictionary dictionary2;
            QuestParam param6;
            List<QuestParam>.Enumerator enumerator3;
            ItemParam param7;
            List<QuestParam> list6;
            SRPG_Button button4;
            BookmarkToggleButton button5;
            SRPG_Button button6;
            BookmarkToggleButton button7;
            SectionParam param8;
            SectionParam[] paramArray3;
            int num5;
            SRPG_Button button8;
            List<QuestParam> list7;
            OrderedDictionary dictionary3;
            QuestParam param9;
            List<QuestParam>.Enumerator enumerator4;
            ItemParam param10;
            List<QuestParam> list8;
            SRPG_Button button9;
            SRPG_Button[] buttonArray;
            int num6;
            ItemAndQuests quests2;
            manager = MonoSingleton<GameManager>.Instance;
            data = manager.Player;
            paramArray = manager.Sections;
            list = new List<string>();
            list2 = new List<string>();
            num = Network.GetServerTime();
            paramArray2 = data.AvailableQuests;
            num2 = 0;
            goto Label_0073;
        Label_0038:
            param = paramArray2[num2];
            if (this.IsAvailableQuest(param, num) == null)
            {
                goto Label_006D;
            }
            if (list.Contains(param.ChapterID) != null)
            {
                goto Label_006D;
            }
            list.Add(param.ChapterID);
        Label_006D:
            num2 += 1;
        Label_0073:
            if (num2 < ((int) paramArray2.Length))
            {
                goto Label_0038;
            }
            enumerator = list.GetEnumerator();
        Label_0086:
            try
            {
                goto Label_00C6;
            Label_008B:
                str = &enumerator.Current;
                param2 = manager.FindArea(str);
                if (param2 == null)
                {
                    goto Label_00C6;
                }
                if (list2.Contains(param2.section) != null)
                {
                    goto Label_00C6;
                }
                list2.Add(param2.section);
            Label_00C6:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_008B;
                }
                goto Label_00E4;
            }
            finally
            {
            Label_00D7:
                ((List<string>.Enumerator) enumerator).Dispose();
            }
        Label_00E4:
            this.mAvailableSections = list2.ToArray();
            dictionary = new Dictionary<string, List<QuestParam>>();
            if (<>f__am$cache18 != null)
            {
                goto Label_011A;
            }
            <>f__am$cache18 = new Func<QuestParam, bool>(QuestBookmarkWindow.<Initialize>m__3B8);
        Label_011A:
            enumerator2 = Enumerable.Where<QuestParam>(MonoSingleton<GameManager>.Instance.Quests, <>f__am$cache18).GetEnumerator();
        Label_012F:
            try
            {
                goto Label_0172;
            Label_0134:
                param3 = enumerator2.Current;
                if (dictionary.TryGetValue(param3.world, &list3) != null)
                {
                    goto Label_0169;
                }
                list3 = new List<QuestParam>();
                dictionary[param3.world] = list3;
            Label_0169:
                list3.Add(param3);
            Label_0172:
                if (enumerator2.MoveNext() != null)
                {
                    goto Label_0134;
                }
                goto Label_0190;
            }
            finally
            {
            Label_0183:
                if (enumerator2 != null)
                {
                    goto Label_0188;
                }
            Label_0188:
                enumerator2.Dispose();
            }
        Label_0190:
            num3 = 0;
            if (bookmarkItems == null)
            {
                goto Label_0230;
            }
            if (((int) bookmarkItems.Length) <= 0)
            {
                goto Label_0230;
            }
            itemArray = bookmarkItems;
            num4 = 0;
            goto Label_0214;
        Label_01AD:
            item = itemArray[num4];
            param4 = MonoSingleton<GameManager>.Instance.MasterParam.GetItemParam(item.iname);
            list4 = QuestDropParam.Instance.GetItemDropQuestList(param4, GlobalVars.GetDropTableGeneratedDateTime());
            quests2 = new ItemAndQuests();
            quests2.itemName = param4.iname;
            quests2.quests = list4;
            quests = quests2;
            this.mBookmarkedPiecesOrigin.Add(quests);
            num4 += 1;
        Label_0214:
            if (num4 < ((int) itemArray.Length))
            {
                goto Label_01AD;
            }
            this.mBookmarkedPieces = Enumerable.ToList<ItemAndQuests>(this.mBookmarkedPiecesOrigin);
        Label_0230:
            this.mSectionToPieces[this.BookmarkSectionName] = this.mBookmarkedPieces;
            if (num3 >= ((int) this.ButtonSections.Length))
            {
                goto Label_0273;
            }
            button = this.ButtonSections[num3];
            DataSource.Bind<string>(button.get_gameObject(), this.BookmarkSectionName);
        Label_0273:
            num3 += 1;
            goto Label_0419;
        Label_027E:
            if ((num3 - 1) >= ((int) paramArray.Length))
            {
                goto Label_03F7;
            }
            param5 = paramArray[num3 - 1];
            if (param5.IsDateUnlock() == null)
            {
                goto Label_03D6;
            }
            if (list2.Contains(param5.iname) == null)
            {
                goto Label_03D6;
            }
            button2 = this.ButtonSections[num3];
            DataSource.Bind<string>(button2.get_gameObject(), param5.iname);
            button2.GetComponent<BookmarkToggleButton>().EnableShadow(0);
            list5 = dictionary[param5.iname];
            dictionary2 = new OrderedDictionary();
            enumerator3 = list5.GetEnumerator();
        Label_0300:
            try
            {
                goto Label_0373;
            Label_0305:
                param6 = &enumerator3.Current;
                param7 = QuestDropParam.Instance.GetHardDropPiece(param6.iname, GlobalVars.GetDropTableGeneratedDateTime());
                if (dictionary2.Contains(param7.iname) == null)
                {
                    goto Label_0353;
                }
                list6 = dictionary2[param7.iname] as List<QuestParam>;
                goto Label_036A;
            Label_0353:
                list6 = new List<QuestParam>();
                dictionary2[param7.iname] = list6;
            Label_036A:
                list6.Add(param6);
            Label_0373:
                if (&enumerator3.MoveNext() != null)
                {
                    goto Label_0305;
                }
                goto Label_0391;
            }
            finally
            {
            Label_0384:
                ((List<QuestParam>.Enumerator) enumerator3).Dispose();
            }
        Label_0391:
            if (<>f__am$cache19 != null)
            {
                goto Label_03BD;
            }
            <>f__am$cache19 = new Func<DictionaryEntry, ItemAndQuests>(QuestBookmarkWindow.<Initialize>m__3B9);
        Label_03BD:
            this.mSectionToPieces[param5.iname] = Enumerable.ToList<ItemAndQuests>(Enumerable.Select<DictionaryEntry, ItemAndQuests>(Enumerable.Cast<DictionaryEntry>(dictionary2), <>f__am$cache19));
            goto Label_03F2;
        Label_03D6:
            button4 = this.ButtonSections[num3];
            button4.GetComponent<BookmarkToggleButton>().EnableShadow(1);
        Label_03F2:
            goto Label_0413;
        Label_03F7:
            button6 = this.ButtonSections[num3];
            button6.GetComponent<BookmarkToggleButton>().EnableShadow(1);
        Label_0413:
            num3 += 1;
        Label_0419:
            if (num3 < ((int) this.ButtonSections.Length))
            {
                goto Label_027E;
            }
            paramArray3 = paramArray;
            num5 = 0;
            goto Label_057D;
        Label_0433:
            param8 = paramArray3[num5];
            if (param8.IsDateUnlock() == null)
            {
                goto Label_0577;
            }
            if (list2.Contains(param8.iname) == null)
            {
                goto Label_0577;
            }
            if (num3 >= ((int) this.ButtonSections.Length))
            {
                goto Label_0577;
            }
            button8 = this.ButtonSections[num3];
            DataSource.Bind<string>(button8.get_gameObject(), param8.iname);
            list7 = dictionary[param8.iname];
            dictionary3 = new OrderedDictionary();
            enumerator4 = list7.GetEnumerator();
        Label_04A6:
            try
            {
                goto Label_0519;
            Label_04AB:
                param9 = &enumerator4.Current;
                param10 = QuestDropParam.Instance.GetHardDropPiece(param9.iname, GlobalVars.GetDropTableGeneratedDateTime());
                if (dictionary3.Contains(param10.iname) == null)
                {
                    goto Label_04F9;
                }
                list8 = dictionary3[param10.iname] as List<QuestParam>;
                goto Label_0510;
            Label_04F9:
                list8 = new List<QuestParam>();
                dictionary3[param10.iname] = list8;
            Label_0510:
                list8.Add(param9);
            Label_0519:
                if (&enumerator4.MoveNext() != null)
                {
                    goto Label_04AB;
                }
                goto Label_0537;
            }
            finally
            {
            Label_052A:
                ((List<QuestParam>.Enumerator) enumerator4).Dispose();
            }
        Label_0537:
            if (<>f__am$cache1A != null)
            {
                goto Label_0563;
            }
            <>f__am$cache1A = new Func<DictionaryEntry, ItemAndQuests>(QuestBookmarkWindow.<Initialize>m__3BA);
        Label_0563:
            this.mSectionToPieces[param8.iname] = Enumerable.ToList<ItemAndQuests>(Enumerable.Select<DictionaryEntry, ItemAndQuests>(Enumerable.Cast<DictionaryEntry>(dictionary3), <>f__am$cache1A));
        Label_0577:
            num5 += 1;
        Label_057D:
            if (num5 < ((int) paramArray3.Length))
            {
                goto Label_0433;
            }
            buttonArray = this.ButtonSections;
            num6 = 0;
            goto Label_05B8;
        Label_0598:
            button9 = buttonArray[num6];
            button9.AddListener(new SRPG_Button.ButtonClickEvent(this.OnSectionSelect));
            num6 += 1;
        Label_05B8:
            if (num6 < ((int) buttonArray.Length))
            {
                goto Label_0598;
            }
            if ((this.TitleText != null) == null)
            {
                goto Label_05EA;
            }
            this.TitleText.set_text(LocalizedText.Get(this.BookmarkTitle));
        Label_05EA:
            if ((this.DescriptionText != null) == null)
            {
                goto Label_0611;
            }
            this.DescriptionText.set_text(LocalizedText.Get(this.BookmarkDescription));
        Label_0611:
            this.RefreshSection(0);
            return;
        }

        private bool IsAvailableQuest(QuestParam questParam, long currentTime)
        {
            if (string.IsNullOrEmpty(questParam.ChapterID) != null)
            {
                goto Label_0029;
            }
            if (questParam.IsMulti != null)
            {
                goto Label_0029;
            }
            if (questParam.IsDateUnlock(currentTime) == null)
            {
                goto Label_0029;
            }
            return 1;
        Label_0029:
            return 0;
        }

        private void OnBookmarkBeginEditButtonClick(SRPG_Button button)
        {
            if (this.mIsBookmarkEditing != null)
            {
                goto Label_0016;
            }
            if (this.mSelectQuestFlag == null)
            {
                goto Label_0017;
            }
        Label_0016:
            return;
        Label_0017:
            this.ButtonBookmarkBeginEdit.get_gameObject().SetActive(0);
            this.ButtonBookmarkEndEdit.get_gameObject().SetActive(1);
            if (this.mBookmarkedPieces.Count < this.MaxBookmarkCount)
            {
                goto Label_0056;
            }
            this.SetActivateWithoutBookmarkedUnit(0);
        Label_0056:
            if ((this.TitleText != null) == null)
            {
                goto Label_007D;
            }
            this.TitleText.set_text(LocalizedText.Get(this.BookmarkEditTitle));
        Label_007D:
            if ((this.DescriptionText != null) == null)
            {
                goto Label_00A4;
            }
            this.DescriptionText.set_text(LocalizedText.Get(this.BookmarkEditDescription));
        Label_00A4:
            this.mIsBookmarkEditing = 1;
            return;
        }

        private void OnBookmarkEndEditButtonClick(SRPG_Button button)
        {
            string[] strArray;
            string[] strArray2;
            if (this.mIsBookmarkEditing != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            if (<>f__am$cache1B != null)
            {
                goto Label_0035;
            }
            <>f__am$cache1B = new Func<ItemAndQuests, string>(QuestBookmarkWindow.<OnBookmarkEndEditButtonClick>m__3BB);
        Label_0035:
            strArray = Enumerable.ToArray<string>(Enumerable.Select<ItemAndQuests, string>(Enumerable.Except<ItemAndQuests>(this.mBookmarkedPiecesOrigin, this.mBookmarkedPieces), <>f__am$cache1B));
            if (<>f__am$cache1C != null)
            {
                goto Label_006E;
            }
            <>f__am$cache1C = new Func<ItemAndQuests, string>(QuestBookmarkWindow.<OnBookmarkEndEditButtonClick>m__3BC);
        Label_006E:
            strArray2 = Enumerable.ToArray<string>(Enumerable.Select<ItemAndQuests, string>(Enumerable.Except<ItemAndQuests>(this.mBookmarkedPieces, this.mBookmarkedPiecesOrigin), <>f__am$cache1C));
            if (((int) strArray.Length) > 0)
            {
                goto Label_0090;
            }
            if (((int) strArray2.Length) <= 0)
            {
                goto Label_009D;
            }
        Label_0090:
            this.RequestQuestBookmarkUpdate(strArray2, strArray);
            goto Label_00A3;
        Label_009D:
            this.EndBookmarkEditing();
        Label_00A3:
            return;
        }

        private void OnSectionSelect(SRPG_Button button)
        {
            object[] objArray1;
            string str;
            int num;
            string str2;
            if (this.mSelectQuestFlag == null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            str = DataSource.FindDataOfClass<string>(button.get_gameObject(), null);
            if ((str == this.mLastSectionName) == null)
            {
                goto Label_002B;
            }
            return;
        Label_002B:
            if ((str != this.BookmarkSectionName) == null)
            {
                goto Label_0081;
            }
            if (Enumerable.Contains<string>(this.mAvailableSections, str) != null)
            {
                goto Label_0081;
            }
            num = Array.IndexOf<SRPG_Button>(this.ButtonSections, button);
            objArray1 = new object[] { (int) num };
            str2 = LocalizedText.Get("sys.TXT_QUESTBOOKMARK_BOOKMARK_NOT_AVAIABLE_SECTION", objArray1);
            UIUtility.SystemMessage(null, str2, null, null, 1, -1);
            return;
        Label_0081:
            this.RefreshSection(str, button);
            return;
        }

        private void OnUnitSelect(SRPG_Button button)
        {
            object[] objArray1;
            ItemAndQuests quests;
            QuestParam[] paramArray;
            QuestParam[] paramArray2;
            ItemParam param;
            List<QuestParam> list;
            List<QuestParam> list2;
            QuestParam param2;
            QuestParam[] paramArray3;
            int num;
            QuestParam param3;
            IEnumerator<QuestParam> enumerator;
            QuestParam param4;
            string str;
            GameObject obj2;
            QuestBookmarkKakeraWindow window;
            UnitParam param5;
            <OnUnitSelect>c__AnonStorey37F storeyf;
            storeyf = new <OnUnitSelect>c__AnonStorey37F();
            storeyf.<>f__this = this;
            if (button.get_interactable() == null)
            {
                goto Label_0025;
            }
            if (this.mSelectQuestFlag == null)
            {
                goto Label_0026;
            }
        Label_0025:
            return;
        Label_0026:
            quests = DataSource.FindDataOfClass<ItemAndQuests>(button.get_gameObject(), null);
            storeyf.currentTime = Network.GetServerTime();
            paramArray = MonoSingleton<GameManager>.Instance.Player.AvailableQuests;
            if ((this.mLastSectionName == this.BookmarkSectionName) == null)
            {
                goto Label_00AC;
            }
            param = MonoSingleton<GameManager>.Instance.MasterParam.GetItemParam(quests.itemName);
            paramArray2 = Enumerable.ToArray<QuestParam>(Enumerable.Where<QuestParam>(QuestDropParam.Instance.GetItemDropQuestList(param, GlobalVars.GetDropTableGeneratedDateTime()), new Func<QuestParam, bool>(storeyf.<>m__3C2)));
            goto Label_00CA;
        Label_00AC:
            paramArray2 = Enumerable.ToArray<QuestParam>(Enumerable.Where<QuestParam>(quests.quests, new Func<QuestParam, bool>(storeyf.<>m__3C3)));
        Label_00CA:
            if (((int) paramArray2.Length) > 0)
            {
                goto Label_00D4;
            }
            return;
        Label_00D4:
            list2 = new List<QuestParam>();
            paramArray3 = paramArray2;
            num = 0;
            goto Label_015A;
        Label_00E6:
            param2 = paramArray3[num];
            enumerator = Enumerable.Where<QuestParam>(paramArray, new Func<QuestParam, bool>(storeyf.<>m__3C4)).GetEnumerator();
        Label_0107:
            try
            {
                goto Label_0136;
            Label_010C:
                param3 = enumerator.Current;
                if ((param2.iname == param3.iname) == null)
                {
                    goto Label_0136;
                }
                list2.Add(param2);
            Label_0136:
                if (enumerator.MoveNext() != null)
                {
                    goto Label_010C;
                }
                goto Label_0154;
            }
            finally
            {
            Label_0147:
                if (enumerator != null)
                {
                    goto Label_014C;
                }
            Label_014C:
                enumerator.Dispose();
            }
        Label_0154:
            num += 1;
        Label_015A:
            if (num < ((int) paramArray3.Length))
            {
                goto Label_00E6;
            }
            if (list2.Count > 0)
            {
                goto Label_01AB;
            }
            param4 = paramArray2[0];
            objArray1 = new object[] { param4.title, param4.name };
            str = LocalizedText.Get("sys.TXT_QUESTBOOKMARK_BOOKMARK_NOT_AVAIABLE_QUEST", objArray1);
            UIUtility.SystemMessage(null, str, null, null, 1, -1);
            return;
        Label_01AB:
            if (this.mIsBookmarkEditing == null)
            {
                goto Label_01C8;
            }
            this.OnUnitSelectBookmark(quests, button.GetComponent<BookmarkUnit>());
            goto Label_0256;
        Label_01C8:
            if (((int) paramArray2.Length) <= 1)
            {
                goto Label_023A;
            }
            if ((this.QuestSelectorTemplate != null) == null)
            {
                goto Label_0256;
            }
            obj2 = Object.Instantiate<GameObject>(this.QuestSelectorTemplate);
            obj2.get_transform().SetParent(base.get_transform().get_parent(), 0);
            window = obj2.GetComponent<QuestBookmarkKakeraWindow>();
            if ((window != null) == null)
            {
                goto Label_0256;
            }
            param5 = DataSource.FindDataOfClass<UnitParam>(button.get_gameObject(), null);
            window.Refresh(param5, paramArray2);
            goto Label_0256;
        Label_023A:
            this.mSelectQuestFlag = 1;
            GlobalVars.SelectedQuestID = paramArray2[0].iname;
            FlowNode_GameObject.ActivateOutputLinks(this, 100);
        Label_0256:
            return;
        }

        private void OnUnitSelectBookmark(ItemAndQuests target, BookmarkUnit unit)
        {
            bool flag;
            <OnUnitSelectBookmark>c__AnonStorey382 storey;
            storey = new <OnUnitSelectBookmark>c__AnonStorey382();
            storey.target = target;
            if (this.mBookmarkedPieces.Exists(new Predicate<ItemAndQuests>(storey.<>m__3C7)) == null)
            {
                goto Label_0052;
            }
            this.DeleteBookmark(storey.target);
            if ((unit != null) == null)
            {
                goto Label_007D;
            }
            unit.BookmarkIcon.SetActive(0);
            goto Label_007D;
        Label_0052:
            flag = this.AddBookmark(storey.target);
            if ((unit != null) == null)
            {
                goto Label_007D;
            }
            if (flag == null)
            {
                goto Label_007D;
            }
            unit.BookmarkIcon.SetActive(1);
        Label_007D:
            return;
        }

        private unsafe void QuestBookmarkResponseCallback(WWWResult www)
        {
            WebAPI.JSON_BodyResponse<JSON_Body> response;
            Network.EErrCode code;
            if (FlowNode_Network.HasCommonError(www) == null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            if (Network.IsError == null)
            {
                goto Label_005D;
            }
            code = Network.ErrCode;
            if (code == 0x2774)
            {
                goto Label_0037;
            }
            if (code == 0x2775)
            {
                goto Label_0037;
            }
            goto Label_0057;
        Label_0037:
            Network.RemoveAPI();
            Network.ResetError();
            UIUtility.SystemMessage(null, LocalizedText.Get("sys.TXT_QUESTBOOKMARK_BOOKMARK_NOT_FOUND"), null, null, 1, -1);
            return;
        Label_0057:
            FlowNode_Network.Retry();
            return;
        Label_005D:
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<JSON_Body>>(&www.text);
            this.Initialize(response.body.result);
            Network.RemoveAPI();
            return;
        }

        private void QuestBookmarkUpdateResponseCallback(WWWResult www)
        {
            Network.EErrCode code;
            if (FlowNode_Network.HasCommonError(www) == null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            if (Network.IsError == null)
            {
                goto Label_005D;
            }
            code = Network.ErrCode;
            if (code == 0x2774)
            {
                goto Label_0037;
            }
            if (code == 0x2775)
            {
                goto Label_0037;
            }
            goto Label_0057;
        Label_0037:
            Network.RemoveAPI();
            Network.ResetError();
            UIUtility.SystemMessage(null, LocalizedText.Get("sys.TXT_QUESTBOOKMARK_BOOKMARK_NOT_FOUND"), null, null, 1, -1);
            return;
        Label_0057:
            FlowNode_Network.Retry();
            return;
        Label_005D:
            this.mBookmarkedPiecesOrigin = Enumerable.ToList<ItemAndQuests>(this.mBookmarkedPieces);
            if ((this.mLastSectionName == this.BookmarkSectionName) == null)
            {
                goto Label_008B;
            }
            this.RefreshSection(0);
        Label_008B:
            this.EndBookmarkEditing();
            Network.RemoveAPI();
            return;
        }

        private void RefreshSection(int index)
        {
            SRPG_Button button;
            string str;
            if (index < ((int) this.ButtonSections.Length))
            {
                goto Label_000F;
            }
            return;
        Label_000F:
            button = this.ButtonSections[index];
            str = DataSource.FindDataOfClass<string>(button.get_gameObject(), null);
            this.RefreshSection(str, button);
            return;
        }

        private unsafe void RefreshSection(string sectionName, SRPG_Button button)
        {
            GameObject obj2;
            List<GameObject>.Enumerator enumerator;
            List<ItemAndQuests> list;
            int num;
            bool flag;
            enumerator = this.mCurrentUnitObjects.GetEnumerator();
        Label_000C:
            try
            {
                goto Label_001F;
            Label_0011:
                obj2 = &enumerator.Current;
                Object.Destroy(obj2);
            Label_001F:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0011;
                }
                goto Label_003C;
            }
            finally
            {
            Label_0030:
                ((List<GameObject>.Enumerator) enumerator).Dispose();
            }
        Label_003C:
            this.mCurrentUnitObjects.Clear();
            list = this.mSectionToPieces[sectionName];
            this.CreateUnitPanels(list, sectionName);
            if (this.mIsBookmarkEditing == null)
            {
                goto Label_0084;
            }
            if (this.mBookmarkedPieces.Count < this.MaxBookmarkCount)
            {
                goto Label_0084;
            }
            this.SetActivateWithoutBookmarkedUnit(0);
        Label_0084:
            num = Array.IndexOf<SRPG_Button>(this.ButtonSections, button);
            this.ToggleSectionButton(num);
            this.ResetScrollPosition();
            if ((sectionName == this.BookmarkSectionName) == null)
            {
                goto Label_00E9;
            }
            flag = (this.mCurrentUnitObjects.Count > 0) == 0;
            this.BookmarkNotFoundText.SetActive(flag);
            this.DescriptionText.get_gameObject().SetActive(flag == 0);
            goto Label_0106;
        Label_00E9:
            this.BookmarkNotFoundText.SetActive(0);
            this.DescriptionText.get_gameObject().SetActive(1);
        Label_0106:
            this.mLastSectionName = sectionName;
            return;
        }

        private void RequestQuestBookmark()
        {
            Network.RequestAPI(new ReqQuestBookmark(new Network.ResponseCallback(this.QuestBookmarkResponseCallback)), 0);
            return;
        }

        private void RequestQuestBookmarkUpdate(IEnumerable<string> add, IEnumerable<string> delete)
        {
            Network.RequestAPI(new ReqQuestBookmarkUpdate(add, delete, new Network.ResponseCallback(this.QuestBookmarkUpdateResponseCallback)), 0);
            return;
        }

        private void ResetScrollPosition()
        {
            if ((this.ScrollRectObj != null) == null)
            {
                goto Label_002B;
            }
            this.ScrollRectObj.set_normalizedPosition(new Vector2(1f, 1f));
        Label_002B:
            return;
        }

        private unsafe void SetActivateWithoutBookmarkedUnit(bool doActivate)
        {
            GameObject obj2;
            List<GameObject>.Enumerator enumerator;
            BookmarkUnit unit;
            <SetActivateWithoutBookmarkedUnit>c__AnonStorey380 storey;
            enumerator = this.mCurrentUnitObjects.GetEnumerator();
        Label_000C:
            try
            {
                goto Label_007A;
            Label_0011:
                obj2 = &enumerator.Current;
                storey = new <SetActivateWithoutBookmarkedUnit>c__AnonStorey380();
                unit = obj2.GetComponent<BookmarkUnit>();
                storey.param = DataSource.FindDataOfClass<ItemParam>(unit.get_gameObject(), null);
                if (storey.param == null)
                {
                    goto Label_007A;
                }
                if (Enumerable.FirstOrDefault<ItemAndQuests>(this.mBookmarkedPieces, new Func<ItemAndQuests, bool>(storey.<>m__3C5)) != null)
                {
                    goto Label_007A;
                }
                unit.Button.set_interactable(doActivate);
                unit.Overlay.SetActive(doActivate == 0);
            Label_007A:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0011;
                }
                goto Label_0097;
            }
            finally
            {
            Label_008B:
                ((List<GameObject>.Enumerator) enumerator).Dispose();
            }
        Label_0097:
            return;
        }

        private unsafe void SetDeactivateNotAvailableUnit()
        {
            GameObject obj2;
            List<GameObject>.Enumerator enumerator;
            ItemAndQuests quests;
            QuestParam[] paramArray;
            bool flag;
            List<QuestParam>.Enumerator enumerator2;
            BookmarkUnit unit;
            <SetDeactivateNotAvailableUnit>c__AnonStorey37A storeya;
            <SetDeactivateNotAvailableUnit>c__AnonStorey37B storeyb;
            enumerator = this.mCurrentUnitObjects.GetEnumerator();
        Label_000C:
            try
            {
                goto Label_00DE;
            Label_0011:
                obj2 = &enumerator.Current;
                storeya = new <SetDeactivateNotAvailableUnit>c__AnonStorey37A();
                storeya.<>f__this = this;
                quests = DataSource.FindDataOfClass<ItemAndQuests>(obj2, null);
                paramArray = MonoSingleton<GameManager>.Instance.Player.AvailableQuests;
                flag = 0;
                storeya.currentTime = Network.GetServerTime();
                storeyb = new <SetDeactivateNotAvailableUnit>c__AnonStorey37B();
                storeyb.<>f__ref$890 = storeya;
                storeyb.<>f__this = this;
                enumerator2 = quests.quests.GetEnumerator();
            Label_0074:
                try
                {
                    goto Label_00A7;
                Label_0079:
                    storeyb.quest = &enumerator2.Current;
                    if (Enumerable.Any<QuestParam>(paramArray, new Func<QuestParam, bool>(storeyb.<>m__3BD)) == null)
                    {
                        goto Label_00A7;
                    }
                    flag = 1;
                    goto Label_00B3;
                Label_00A7:
                    if (&enumerator2.MoveNext() != null)
                    {
                        goto Label_0079;
                    }
                Label_00B3:
                    goto Label_00C5;
                }
                finally
                {
                Label_00B8:
                    ((List<QuestParam>.Enumerator) enumerator2).Dispose();
                }
            Label_00C5:
                obj2.GetComponent<BookmarkUnit>().Overlay.SetActive(flag == 0);
            Label_00DE:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0011;
                }
                goto Label_00FB;
            }
            finally
            {
            Label_00EF:
                ((List<GameObject>.Enumerator) enumerator).Dispose();
            }
        Label_00FB:
            return;
        }

        private void ToggleSectionButton(int index)
        {
            int num;
            BookmarkToggleButton button;
            num = 0;
            goto Label_002F;
        Label_0007:
            button = this.ButtonSections[num].GetComponent<BookmarkToggleButton>();
            if ((button != null) == null)
            {
                goto Label_002B;
            }
            button.Activate(num == index);
        Label_002B:
            num += 1;
        Label_002F:
            if (num < ((int) this.ButtonSections.Length))
            {
                goto Label_0007;
            }
            return;
        }

        [CompilerGenerated]
        private sealed class <CreateUnitPanels>c__AnonStorey37C
        {
            internal QuestBookmarkWindow.ItemAndQuests itemQuests;

            public <CreateUnitPanels>c__AnonStorey37C()
            {
                base..ctor();
                return;
            }
        }

        [CompilerGenerated]
        private sealed class <CreateUnitPanels>c__AnonStorey37D
        {
            internal string sectionName;

            public <CreateUnitPanels>c__AnonStorey37D()
            {
                base..ctor();
                return;
            }
        }

        [CompilerGenerated]
        private sealed class <CreateUnitPanels>c__AnonStorey37E
        {
            internal ItemParam itemParam;
            internal QuestBookmarkWindow.<CreateUnitPanels>c__AnonStorey37D <>f__ref$893;
            internal QuestBookmarkWindow.<CreateUnitPanels>c__AnonStorey37C <>f__ref$892;

            public <CreateUnitPanels>c__AnonStorey37E()
            {
                base..ctor();
                return;
            }

            internal bool <>m__3BF(QuestBookmarkWindow.ItemAndQuests p)
            {
                return (p.itemName == this.<>f__ref$892.itemQuests.itemName);
            }

            internal bool <>m__3C0(QuestParam q)
            {
                return (q.world == this.<>f__ref$893.sectionName);
            }

            internal bool <>m__3C1(UnitParam unit)
            {
                return (unit.piece == this.itemParam.iname);
            }
        }

        [CompilerGenerated]
        private sealed class <DeleteBookmark>c__AnonStorey381
        {
            internal QuestBookmarkWindow.ItemAndQuests item;

            public <DeleteBookmark>c__AnonStorey381()
            {
                base..ctor();
                return;
            }

            internal bool <>m__3C6(QuestBookmarkWindow.ItemAndQuests p)
            {
                return (p.itemName == this.item.itemName);
            }
        }

        [CompilerGenerated]
        private sealed class <OnUnitSelect>c__AnonStorey37F
        {
            internal long currentTime;
            internal QuestBookmarkWindow <>f__this;

            public <OnUnitSelect>c__AnonStorey37F()
            {
                base..ctor();
                return;
            }

            internal bool <>m__3C2(QuestParam q)
            {
                return Enumerable.Contains<string>(this.<>f__this.mAvailableSections, q.world);
            }

            internal bool <>m__3C3(QuestParam q)
            {
                return (q.world == this.<>f__this.mLastSectionName);
            }

            internal bool <>m__3C4(QuestParam q)
            {
                return this.<>f__this.IsAvailableQuest(q, this.currentTime);
            }
        }

        [CompilerGenerated]
        private sealed class <OnUnitSelectBookmark>c__AnonStorey382
        {
            internal QuestBookmarkWindow.ItemAndQuests target;

            public <OnUnitSelectBookmark>c__AnonStorey382()
            {
                base..ctor();
                return;
            }

            internal bool <>m__3C7(QuestBookmarkWindow.ItemAndQuests p)
            {
                return (p.itemName == this.target.itemName);
            }
        }

        [CompilerGenerated]
        private sealed class <SetActivateWithoutBookmarkedUnit>c__AnonStorey380
        {
            internal ItemParam param;

            public <SetActivateWithoutBookmarkedUnit>c__AnonStorey380()
            {
                base..ctor();
                return;
            }

            internal bool <>m__3C5(QuestBookmarkWindow.ItemAndQuests b)
            {
                return (b.itemName == this.param.iname);
            }
        }

        [CompilerGenerated]
        private sealed class <SetDeactivateNotAvailableUnit>c__AnonStorey37A
        {
            internal long currentTime;
            internal QuestBookmarkWindow <>f__this;

            public <SetDeactivateNotAvailableUnit>c__AnonStorey37A()
            {
                base..ctor();
                return;
            }
        }

        [CompilerGenerated]
        private sealed class <SetDeactivateNotAvailableUnit>c__AnonStorey37B
        {
            internal QuestParam quest;
            internal QuestBookmarkWindow.<SetDeactivateNotAvailableUnit>c__AnonStorey37A <>f__ref$890;
            internal QuestBookmarkWindow <>f__this;

            public <SetDeactivateNotAvailableUnit>c__AnonStorey37B()
            {
                base..ctor();
                return;
            }

            internal bool <>m__3BD(QuestParam q)
            {
                return ((this.<>f__this.IsAvailableQuest(q, this.<>f__ref$890.currentTime) == null) ? 0 : (this.quest.iname == q.iname));
            }
        }

        private class ItemAndQuests
        {
            public string itemName;
            public List<QuestParam> quests;

            public ItemAndQuests()
            {
                base..ctor();
                return;
            }
        }

        public class JSON_Body
        {
            public QuestBookmarkWindow.JSON_Item[] result;

            public JSON_Body()
            {
                base..ctor();
                return;
            }
        }

        public class JSON_Item
        {
            public string iname;

            public JSON_Item()
            {
                base..ctor();
                return;
            }
        }
    }
}

