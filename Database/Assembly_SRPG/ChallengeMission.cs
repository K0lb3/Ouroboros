namespace SRPG
{
    using GR;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.UI;

    [Pin(10, "次のページ", 0, 10), Pin(0x63, "メッセージ非表示", 0, 0x63), Pin(100, "詳細", 1, 100), Pin(0x65, "報酬受け取り", 1, 0x65), Pin(0x66, "コンプリート報酬受け取り", 1, 0x66), Pin(0x67, "終了", 1, 0x67), Pin(0x68, "コンプリート報酬受け取りとレビュー表示", 1, 0x68), Pin(0, "更新", 0, 0), Pin(1, "現在表示中のカテゴリを維持しつつ更新", 0, 0), Pin(11, "前のページ", 0, 11)]
    public class ChallengeMission : MonoBehaviour, IFlowInterface
    {
        private const int PIN_REFRESH = 0;
        private const int PIN_REFRESH_KEEP_CATEGORY = 1;
        private const int PIN_NEXT_PAGE = 10;
        private const int PIN_BACK_PAGE = 11;
        private const int PIN_MESSAGE_CLOSE = 0x63;
        private const int PIN_DETAIL = 100;
        private const int PIN_REWARD = 0x65;
        private const int PIN_COMPLETE = 0x66;
        private const int PIN_END = 0x67;
        private const int PIN_COMPLETE_REVIEW = 0x68;
        public Image ImageReward;
        public List<ChallengeMissionItem> Items;
        public GameObject PageDotsHolder;
        public GameObject PageDotTemplate;
        public ChallengeMissionDetail DetailWindow;
        public GameObject CharMessageWindow;
        public Text MessageText;
        public Image Shadow;
        public Text ShadowText;
        public Text PageNumText;
        public Text PageMaxNumText;
        public Button NextPageButton;
        public Button BackPageButton;
        public Image CompleteBadge;
        private int mRootCount;
        public List<GameObject> mDotsList;
        private int mCurrentCategoryIndex;
        private ChallengeCategoryParam[] mCategories;
        public Transform CategoryButtonContainer;
        public GameObject CategoryButtonTemplate;
        public bool UseCharMessage;
        public float CharMessageDelay;
        private List<ChallengeMissionCategoryButton> mCategoryButtons;
        private int mCurrentPage;
        private Coroutine mMessageCloseCoroutine;
        [CompilerGenerated]
        private static Func<ChallengeCategoryParam, int> <>f__am$cache19;
        [CompilerGenerated]
        private static Func<ChallengeCategoryParam, string> <>f__am$cache1A;
        [CompilerGenerated]
        private static Func<ChallengeCategoryParam, int> <>f__am$cache1B;

        public ChallengeMission()
        {
            this.mDotsList = new List<GameObject>();
            this.UseCharMessage = 1;
            this.CharMessageDelay = 3f;
            this.mCategoryButtons = new List<ChallengeMissionCategoryButton>();
            base..ctor();
            return;
        }

        [CompilerGenerated]
        private static int <GetOpeningCategory>m__2A5(ChallengeCategoryParam cat)
        {
            return cat.prio;
        }

        [CompilerGenerated]
        private static string <GetTrophiesSortedByPriority>m__2A9(ChallengeCategoryParam c)
        {
            return c.iname;
        }

        [CompilerGenerated]
        private static int <GetTrophiesSortedByPriority>m__2AA(ChallengeCategoryParam c)
        {
            return c.prio;
        }

        public void Activated(int pinID)
        {
            int num;
            num = pinID;
            if (num == null)
            {
                goto Label_002C;
            }
            if (num == 1)
            {
                goto Label_003A;
            }
            if (num == 10)
            {
                goto Label_0048;
            }
            if (num == 11)
            {
                goto Label_0075;
            }
            if (num == 0x63)
            {
                goto Label_009D;
            }
            goto Label_00C5;
        Label_002C:
            this.Refresh(1, 1, 0);
            goto Label_00C5;
        Label_003A:
            this.Refresh(1, 0, 0);
            goto Label_00C5;
        Label_0048:
            if (this.mCurrentPage >= this.mRootCount)
            {
                goto Label_00C5;
            }
            this.mCurrentPage += 1;
            this.Refresh(0, 0, 0);
            goto Label_00C5;
        Label_0075:
            if (this.mCurrentPage <= 0)
            {
                goto Label_00C5;
            }
            this.mCurrentPage -= 1;
            this.Refresh(0, 0, 0);
            goto Label_00C5;
        Label_009D:
            if ((this.CharMessageWindow != null) == null)
            {
                goto Label_00C5;
            }
            this.ResetMessageCloseCoroutine();
            this.CharMessageWindow.SetActive(0);
        Label_00C5:
            return;
        }

        private void Awake()
        {
            int num;
            ChallengeCategoryParam param;
            GameObject obj2;
            ChallengeMissionCategoryButton button;
            <Awake>c__AnonStorey30A storeya;
            if ((this.DetailWindow != null) == null)
            {
                goto Label_0022;
            }
            this.DetailWindow.get_gameObject().SetActive(0);
        Label_0022:
            if ((this.CharMessageWindow != null) == null)
            {
                goto Label_003F;
            }
            this.CharMessageWindow.SetActive(0);
        Label_003F:
            this.mCategories = GetOpeningCategory();
            if ((this.CategoryButtonTemplate != null) == null)
            {
                goto Label_0108;
            }
            this.CategoryButtonTemplate.SetActive(0);
            num = 0;
            goto Label_00FA;
        Label_006E:
            storeya = new <Awake>c__AnonStorey30A();
            storeya.<>f__this = this;
            param = this.mCategories[num];
            obj2 = Object.Instantiate<GameObject>(this.CategoryButtonTemplate);
            button = obj2.GetComponent<ChallengeMissionCategoryButton>();
            storeya.index = num;
            button.Button.get_onClick().AddListener(new UnityAction(storeya, this.<>m__2A4));
            button.SetChallengeCategory(param);
            this.mCategoryButtons.Add(button);
            DataSource.Bind<ChallengeCategoryParam>(button.get_gameObject(), param);
            obj2.get_transform().SetParent(this.CategoryButtonContainer, 0);
            obj2.SetActive(1);
            num += 1;
        Label_00FA:
            if (num < ((int) this.mCategories.Length))
            {
                goto Label_006E;
            }
        Label_0108:
            return;
        }

        private void ChangeRewardImage(TrophyParam trophy)
        {
            DataSource.Bind<TrophyParam>(this.ImageReward.get_gameObject(), trophy);
            GameParameter.UpdateAll(this.ImageReward.get_gameObject());
            return;
        }

        [DebuggerHidden]
        private IEnumerator CloseMessageWindow()
        {
            <CloseMessageWindow>c__IteratorEA rea;
            rea = new <CloseMessageWindow>c__IteratorEA();
            rea.<>f__this = this;
            return rea;
        }

        public static TrophyParam[] GetChildeTrophies(TrophyParam root)
        {
            List<TrophyParam> list;
            TrophyParam[] paramArray;
            TrophyParam param;
            TrophyParam[] paramArray2;
            int num;
            list = new List<TrophyParam>();
            paramArray2 = GetTropies();
            num = 0;
            goto Label_005F;
        Label_0016:
            param = paramArray2[num];
            if (param.IsChallengeMission == null)
            {
                goto Label_0059;
            }
            if ((param.Category == root.Category) == null)
            {
                goto Label_0059;
            }
            if ((root.iname == param.ParentTrophy) == null)
            {
                goto Label_0059;
            }
            list.Add(param);
        Label_0059:
            num += 1;
        Label_005F:
            if (num < ((int) paramArray2.Length))
            {
                goto Label_0016;
            }
            return list.ToArray();
        }

        private static int GetCurrentActiveTrophyIndex(TrophyParam[] trophies)
        {
            int num;
            TrophyParam param;
            TrophyParam[] paramArray;
            int num2;
            TrophyState state;
            if (trophies == null)
            {
                goto Label_000E;
            }
            if (((int) trophies.Length) != null)
            {
                goto Label_0010;
            }
        Label_000E:
            return -1;
        Label_0010:
            num = 0;
            paramArray = trophies;
            num2 = 0;
            goto Label_003B;
        Label_001B:
            param = paramArray[num2];
            if (GetTrophyCounter(param).IsEnded == null)
            {
                goto Label_0037;
            }
            num += 1;
        Label_0037:
            num2 += 1;
        Label_003B:
            if (num2 < ((int) paramArray.Length))
            {
                goto Label_001B;
            }
            if (num < ((int) trophies.Length))
            {
                goto Label_0053;
            }
            return (((int) trophies.Length) - 1);
        Label_0053:
            return num;
        }

        public static TrophyParam GetCurrentRootTrophy(string category)
        {
            TrophyParam[] paramArray;
            TrophyParam param;
            TrophyParam[] paramArray2;
            int num;
            TrophyState state;
            paramArray = GetRootTropies(category);
            if (paramArray == null)
            {
                goto Label_0015;
            }
            if (((int) paramArray.Length) != null)
            {
                goto Label_0017;
            }
        Label_0015:
            return null;
        Label_0017:
            paramArray2 = paramArray;
            num = 0;
            goto Label_003E;
        Label_0020:
            param = paramArray2[num];
            if (GetTrophyCounter(param).IsEnded != null)
            {
                goto Label_003A;
            }
            return param;
        Label_003A:
            num += 1;
        Label_003E:
            if (num < ((int) paramArray2.Length))
            {
                goto Label_0020;
            }
            return null;
        }

        private static ChallengeCategoryParam[] GetOpeningCategory()
        {
            ChallengeCategoryParam[] paramArray;
            List<ChallengeCategoryParam> list;
            ChallengeCategoryParam param;
            ChallengeCategoryParam[] paramArray2;
            int num;
            if (<>f__am$cache19 != null)
            {
                goto Label_0027;
            }
            <>f__am$cache19 = new Func<ChallengeCategoryParam, int>(ChallengeMission.<GetOpeningCategory>m__2A5);
        Label_0027:
            paramArray = Enumerable.ToArray<ChallengeCategoryParam>(Enumerable.OrderByDescending<ChallengeCategoryParam, int>(MonoSingleton<GameManager>.Instance.MasterParam.ChallengeCategories, <>f__am$cache19));
            list = new List<ChallengeCategoryParam>();
            paramArray2 = paramArray;
            num = 0;
            goto Label_0064;
        Label_0047:
            param = paramArray2[num];
            if (IsCategoryOpening(param) == null)
            {
                goto Label_005E;
            }
            list.Add(param);
        Label_005E:
            num += 1;
        Label_0064:
            if (num < ((int) paramArray2.Length))
            {
                goto Label_0047;
            }
            return list.ToArray();
        }

        public static TrophyParam[] GetRootTrophiesSortedByPriority()
        {
            return GetTrophiesSortedByPriority(GetRootTropies());
        }

        public static TrophyParam[] GetRootTropies()
        {
            ChallengeCategoryParam[] paramArray;
            List<TrophyParam> list;
            TrophyParam[] paramArray2;
            TrophyParam[] paramArray3;
            int num;
            ChallengeCategoryParam param;
            DateTime time;
            <GetRootTropies>c__AnonStorey30D storeyd;
            paramArray = MonoSingleton<GameManager>.Instance.MasterParam.ChallengeCategories;
            list = new List<TrophyParam>();
            paramArray2 = GetTropies();
            storeyd = new <GetRootTropies>c__AnonStorey30D();
            paramArray3 = paramArray2;
            num = 0;
            goto Label_00D9;
        Label_002D:
            storeyd.trophy = paramArray3[num];
            if (storeyd.trophy.IsChallengeMissionRoot == null)
            {
                goto Label_00D3;
            }
            param = Enumerable.FirstOrDefault<ChallengeCategoryParam>(paramArray, new Func<ChallengeCategoryParam, bool>(storeyd.<>m__2A8));
            if (param == null)
            {
                goto Label_00D3;
            }
            if (param.begin_at == null)
            {
                goto Label_007D;
            }
            if (param.end_at != null)
            {
                goto Label_008F;
            }
        Label_007D:
            list.Add(storeyd.trophy);
            goto Label_00D3;
        Label_008F:
            time = TimeManager.ServerTime;
            if ((time >= param.begin_at.DateTimes) == null)
            {
                goto Label_00D3;
            }
            if ((time <= param.end_at.DateTimes) == null)
            {
                goto Label_00D3;
            }
            list.Add(storeyd.trophy);
        Label_00D3:
            num += 1;
        Label_00D9:
            if (num < ((int) paramArray3.Length))
            {
                goto Label_002D;
            }
            return list.ToArray();
        }

        public static TrophyParam[] GetRootTropies(string category)
        {
            List<TrophyParam> list;
            TrophyParam[] paramArray;
            TrophyParam param;
            TrophyParam[] paramArray2;
            int num;
            list = new List<TrophyParam>();
            paramArray2 = GetTropies();
            num = 0;
            goto Label_0044;
        Label_0016:
            param = paramArray2[num];
            if (param.IsChallengeMissionRoot == null)
            {
                goto Label_003E;
            }
            if ((param.Category == category) == null)
            {
                goto Label_003E;
            }
            list.Add(param);
        Label_003E:
            num += 1;
        Label_0044:
            if (num < ((int) paramArray2.Length))
            {
                goto Label_0016;
            }
            return list.ToArray();
        }

        private static string GetTopMostPriorityCategory(ChallengeCategoryParam[] categories)
        {
            TrophyParam param;
            param = GetTopMostPriorityTrophy(categories);
            if (param != null)
            {
                goto Label_000F;
            }
            return null;
        Label_000F:
            return param.Category;
        }

        public static TrophyParam GetTopMostPriorityTrophy()
        {
            ChallengeCategoryParam[] paramArray;
            return GetTopMostPriorityTrophy(GetOpeningCategory());
        }

        public static TrophyParam GetTopMostPriorityTrophy(ChallengeCategoryParam[] categories)
        {
            TrophyParam param;
            TrophyParam param2;
            TrophyParam param3;
            ChallengeCategoryParam param4;
            ChallengeCategoryParam[] paramArray;
            int num;
            bool flag;
            TrophyParam param5;
            TrophyParam[] paramArray2;
            TrophyParam param6;
            TrophyParam[] paramArray3;
            int num2;
            TrophyState state;
            TrophyState state2;
            param = null;
            param2 = null;
            param3 = null;
            paramArray = categories;
            num = 0;
            goto Label_00EE;
        Label_0011:
            param4 = paramArray[num];
            flag = 1;
            param5 = GetCurrentRootTrophy(param4.iname);
            if (param5 != null)
            {
                goto Label_0033;
            }
            goto Label_00E8;
        Label_0033:
            paramArray3 = GetChildeTrophies(param5);
            num2 = 0;
            goto Label_0090;
        Label_0048:
            param6 = paramArray3[num2];
            state = GetTrophyCounter(param6);
            if (state.IsEnded != null)
            {
                goto Label_008A;
            }
            flag = 0;
            if (state.IsCompleted == null)
            {
                goto Label_0081;
            }
            if (param2 != null)
            {
                goto Label_008A;
            }
            param2 = param6;
            goto Label_008A;
        Label_0081:
            if (param != null)
            {
                goto Label_008A;
            }
            param = param6;
        Label_008A:
            num2 += 1;
        Label_0090:
            if (num2 < ((int) paramArray3.Length))
            {
                goto Label_0048;
            }
            state2 = GetTrophyCounter(param5);
            if (state2.IsEnded != null)
            {
                goto Label_00E8;
            }
            if (flag == null)
            {
                goto Label_00C5;
            }
            if (param3 != null)
            {
                goto Label_00E8;
            }
            param3 = param5;
            goto Label_00E8;
        Label_00C5:
            if (state2.IsCompleted == null)
            {
                goto Label_00DF;
            }
            if (param2 != null)
            {
                goto Label_00E8;
            }
            param2 = param5;
            goto Label_00E8;
        Label_00DF:
            if (param != null)
            {
                goto Label_00E8;
            }
            param = param5;
        Label_00E8:
            num += 1;
        Label_00EE:
            if (num < ((int) paramArray.Length))
            {
                goto Label_0011;
            }
            if (param3 == null)
            {
                goto Label_0101;
            }
            return param3;
        Label_0101:
            if (param2 == null)
            {
                goto Label_0109;
            }
            return param2;
        Label_0109:
            return param;
        }

        public static TrophyParam[] GetTrophiesSortedByPriority(TrophyParam[] trophies)
        {
            ChallengeCategoryParam[] paramArray;
            TrophyParam[] paramArray2;
            <GetTrophiesSortedByPriority>c__AnonStorey30E storeye;
            storeye = new <GetTrophiesSortedByPriority>c__AnonStorey30E();
            paramArray = MonoSingleton<GameManager>.Instance.MasterParam.ChallengeCategories;
            if (paramArray == null)
            {
                goto Label_0025;
            }
            if (((int) paramArray.Length) >= 1)
            {
                goto Label_0027;
            }
        Label_0025:
            return null;
        Label_0027:
            if (<>f__am$cache1A != null)
            {
                goto Label_0041;
            }
            <>f__am$cache1A = new Func<ChallengeCategoryParam, string>(ChallengeMission.<GetTrophiesSortedByPriority>m__2A9);
        Label_0041:
            if (<>f__am$cache1B != null)
            {
                goto Label_005E;
            }
            <>f__am$cache1B = new Func<ChallengeCategoryParam, int>(ChallengeMission.<GetTrophiesSortedByPriority>m__2AA);
        Label_005E:
            storeye.priorities = Enumerable.ToDictionary<ChallengeCategoryParam, string, int>(paramArray, <>f__am$cache1A, <>f__am$cache1B);
            if (trophies == null)
            {
                goto Label_007C;
            }
            if (((int) trophies.Length) >= 1)
            {
                goto Label_007E;
            }
        Label_007C:
            return null;
        Label_007E:
            return Enumerable.ToArray<TrophyParam>(Enumerable.OrderByDescending<TrophyParam, int>(trophies, new Func<TrophyParam, int>(storeye.<>m__2AB)));
        }

        public static TrophyParam GetTrophy(string key)
        {
            return MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.GetTrophy(key);
        }

        public static TrophyState GetTrophyCounter(TrophyParam trophy)
        {
            return MonoSingleton<GameManager>.GetInstanceDirect().Player.GetTrophyCounter(trophy, 0);
        }

        public static TrophyParam[] GetTropies()
        {
            return MonoSingleton<GameManager>.GetInstanceDirect().Trophies;
        }

        private string HankakuToZenkakuNumber(int num)
        {
            int num2;
            num2 = num;
            switch (num2)
            {
                case 0:
                    goto Label_0035;

                case 1:
                    goto Label_003B;

                case 2:
                    goto Label_0041;

                case 3:
                    goto Label_0047;

                case 4:
                    goto Label_004D;

                case 5:
                    goto Label_0053;

                case 6:
                    goto Label_0059;

                case 7:
                    goto Label_005F;

                case 8:
                    goto Label_0065;

                case 9:
                    goto Label_006B;
            }
            goto Label_0071;
        Label_0035:
            return "１";
        Label_003B:
            return "１";
        Label_0041:
            return "２";
        Label_0047:
            return "３";
        Label_004D:
            return "４";
        Label_0053:
            return "５";
        Label_0059:
            return "６";
        Label_005F:
            return "７";
        Label_0065:
            return "８";
        Label_006B:
            return "９";
        Label_0071:
            return "０";
        }

        private static bool IsCategoryOpening(ChallengeCategoryParam category)
        {
            return category.IsAvailablePeriod(TimeManager.ServerTime);
        }

        private void OnClickCategoryButton(int index)
        {
            if (index != this.mCurrentCategoryIndex)
            {
                goto Label_000D;
            }
            return;
        Label_000D:
            this.mCurrentCategoryIndex = index;
            this.Refresh(0, 0, 1);
            return;
        }

        private void OnSelectItem(TrophyParam selectTrophy)
        {
            TrophyState state;
            if (GetTrophyCounter(selectTrophy).IsCompleted == null)
            {
                goto Label_003A;
            }
            GlobalVars.SelectedChallengeMissionTrophy = selectTrophy.iname;
            GlobalVars.SelectedTrophy.Set(selectTrophy.iname);
            FlowNode_GameObject.ActivateOutputLinks(this, 0x65);
            goto Label_0053;
        Label_003A:
            DataSource.Bind<TrophyParam>(this.DetailWindow.get_gameObject(), selectTrophy);
            FlowNode_GameObject.ActivateOutputLinks(this, 100);
        Label_0053:
            return;
        }

        private unsafe void OpenNewPage(TrophyParam rootTrophy, bool doInitialize)
        {
            object[] objArray3;
            object[] objArray2;
            object[] objArray1;
            TrophyParam[] paramArray;
            bool flag;
            bool flag2;
            int num;
            TrophyState state;
            ItemParam param;
            TrophyState state2;
            string str;
            string str2;
            ItemParam param2;
            UnitParam param3;
            <OpenNewPage>c__AnonStorey30C storeyc;
            if (rootTrophy != null)
            {
                goto Label_000F;
            }
            FlowNode_GameObject.ActivateOutputLinks(this, 0x67);
            return;
        Label_000F:
            paramArray = GetChildeTrophies(rootTrophy);
            if (((int) paramArray.Length) == this.Items.Count)
            {
                goto Label_0032;
            }
            FlowNode_GameObject.ActivateOutputLinks(this, 0x67);
            return;
        Label_0032:
            this.Shadow.get_gameObject().SetActive(0);
            flag = 0;
            flag2 = 1;
            num = 0;
            goto Label_0172;
        Label_004E:
            storeyc = new <OpenNewPage>c__AnonStorey30C();
            storeyc.<>f__this = this;
            storeyc.trophy = paramArray[num];
            state = GetTrophyCounter(storeyc.trophy);
            if (state.IsEnded != null)
            {
                goto Label_0091;
            }
            flag2 = 0;
            if (state.IsCompleted == null)
            {
                goto Label_0091;
            }
            flag = 1;
        Label_0091:
            this.Items[num].OnClick = new UnityAction(storeyc, this.<>m__2A7);
            DataSource.Bind<TrophyParam>(this.Items[num].get_gameObject(), storeyc.trophy);
            param = null;
            if (storeyc.trophy.Coin == null)
            {
                goto Label_00F6;
            }
            param = MonoSingleton<GameManager>.Instance.GetItemParam("$COIN");
            goto Label_013E;
        Label_00F6:
            if (storeyc.trophy.Items == null)
            {
                goto Label_013E;
            }
            if (((int) storeyc.trophy.Items.Length) <= 0)
            {
                goto Label_013E;
            }
            param = MonoSingleton<GameManager>.Instance.GetItemParam(&(storeyc.trophy.Items[0]).iname);
        Label_013E:
            if (param == null)
            {
                goto Label_015D;
            }
            DataSource.Bind<ItemParam>(this.Items[num].get_gameObject(), param);
        Label_015D:
            this.Items[num].Refresh();
            num += 1;
        Label_0172:
            if (num < this.Items.Count)
            {
                goto Label_004E;
            }
            this.CompleteBadge.get_gameObject().SetActive(flag2);
            if (flag2 != null)
            {
                goto Label_01A1;
            }
            if (doInitialize != null)
            {
                goto Label_01A1;
            }
            return;
        Label_01A1:
            state2 = GetTrophyCounter(rootTrophy);
            if (this.UseCharMessage == null)
            {
                goto Label_03DF;
            }
            if ((this.MessageText != null) == null)
            {
                goto Label_03DF;
            }
            if (state2.IsEnded != null)
            {
                goto Label_03DF;
            }
            str = null;
            if (flag == null)
            {
                goto Label_01EB;
            }
            str = LocalizedText.Get("sys.CHALLENGE_MSG_CLEAR");
            goto Label_03A2;
        Label_01EB:
            if (PlayerPrefsUtility.GetInt(PlayerPrefsUtility.CHALLENGEMISSION_HAS_SHOW_MESSAGE, 0) != null)
            {
                goto Label_03A2;
            }
            str2 = string.Empty;
            if (rootTrophy.Gold == null)
            {
                goto Label_022E;
            }
            str2 = string.Format(LocalizedText.Get("sys.CHALLENGE_REWARD_GOLD"), (int) rootTrophy.Gold);
            goto Label_037E;
        Label_022E:
            if (rootTrophy.Exp == null)
            {
                goto Label_025A;
            }
            str2 = string.Format(LocalizedText.Get("sys.CHALLENGE_REWARD_EXP"), (int) rootTrophy.Exp);
            goto Label_037E;
        Label_025A:
            if (rootTrophy.Coin == null)
            {
                goto Label_0286;
            }
            str2 = string.Format(LocalizedText.Get("sys.CHALLENGE_REWARD_COIN"), (int) rootTrophy.Coin);
            goto Label_037E;
        Label_0286:
            if (rootTrophy.Stamina == null)
            {
                goto Label_02B2;
            }
            str2 = string.Format(LocalizedText.Get("sys.CHALLENGE_REWARD_STAMINA"), (int) rootTrophy.Stamina);
            goto Label_037E;
        Label_02B2:
            if (rootTrophy.Items == null)
            {
                goto Label_037E;
            }
            if (((int) rootTrophy.Items.Length) <= 0)
            {
                goto Label_037E;
            }
            param2 = MonoSingleton<GameManager>.Instance.GetItemParam(&(rootTrophy.Items[0]).iname);
            if (param2 == null)
            {
                goto Label_037E;
            }
            if (param2.type != 0x10)
            {
                goto Label_0349;
            }
            param3 = MonoSingleton<GameManager>.Instance.GetUnitParam(param2.iname);
            if (param3 == null)
            {
                goto Label_037E;
            }
            objArray1 = new object[] { (int) (param3.rare + 1), param3.name };
            str2 = LocalizedText.Get("sys.CHALLENGE_DETAIL_REWARD_UNIT", objArray1);
            goto Label_037E;
        Label_0349:
            objArray2 = new object[] { param2.name, (int) &(rootTrophy.Items[0]).Num };
            str2 = LocalizedText.Get("sys.CHALLENGE_REWARD_ITEM", objArray2);
        Label_037E:
            objArray3 = new object[] { str2 };
            str = LocalizedText.Get("sys.CHALLENGE_MSG_INFO", objArray3);
            PlayerPrefsUtility.SetInt(PlayerPrefsUtility.CHALLENGEMISSION_HAS_SHOW_MESSAGE, 1, 0);
        Label_03A2:
            if (str == null)
            {
                goto Label_03DF;
            }
            this.MessageText.set_text(str);
            if ((this.CharMessageWindow != null) == null)
            {
                goto Label_03DF;
            }
            this.ResetMessageCloseCoroutine();
            this.CharMessageWindow.SetActive(1);
            this.StartMessageCloseCoroutine();
        Label_03DF:
            if (flag2 == null)
            {
                goto Label_0458;
            }
            if (state2.IsEnded != null)
            {
                goto Label_0458;
            }
            MonoSingleton<GameManager>.GetInstanceDirect().Player.OnChallengeMissionComplete(rootTrophy.iname);
            GlobalVars.SelectedChallengeMissionTrophy = rootTrophy.iname;
            GlobalVars.SelectedTrophy.Set(rootTrophy.iname);
            PlayerPrefsUtility.SetInt(PlayerPrefsUtility.CHALLENGEMISSION_HAS_SHOW_MESSAGE, 0, 0);
            if ((rootTrophy.iname == "CHALLENGE_02") == null)
            {
                goto Label_0450;
            }
            FlowNode_GameObject.ActivateOutputLinks(this, 0x68);
            goto Label_0458;
        Label_0450:
            FlowNode_GameObject.ActivateOutputLinks(this, 0x66);
        Label_0458:
            return;
        }

        private unsafe void OpenNonAchievedPage(int newPage)
        {
            ChallengeMissionItem item;
            List<ChallengeMissionItem>.Enumerator enumerator;
            DataSource source;
            this.Shadow.get_gameObject().SetActive(1);
            this.ShadowText.set_text(this.HankakuToZenkakuNumber(newPage));
            this.CompleteBadge.get_gameObject().SetActive(0);
            enumerator = this.Items.GetEnumerator();
        Label_0040:
            try
            {
                goto Label_0073;
            Label_0045:
                item = &enumerator.Current;
                item.OnClick = null;
                source = item.GetComponent<DataSource>();
                if ((source != null) == null)
                {
                    goto Label_006D;
                }
                Object.DestroyImmediate(source);
            Label_006D:
                item.Refresh();
            Label_0073:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0045;
                }
                goto Label_0090;
            }
            finally
            {
            Label_0084:
                ((List<ChallengeMissionItem>.Enumerator) enumerator).Dispose();
            }
        Label_0090:
            return;
        }

        private unsafe void Refresh(bool doInitialize, bool autoCategorySelection, bool changeCategory)
        {
            int num;
            int num2;
            ChallengeMissionCategoryButton button;
            int num3;
            bool flag;
            bool flag2;
            bool flag3;
            TrophyParam param;
            TrophyParam param2;
            TrophyParam[] paramArray;
            int num4;
            TrophyState state;
            TrophyState state2;
            ChallengeMissionCategoryButton button2;
            TrophyParam[] paramArray2;
            int num5;
            TrophyParam param3;
            GameObject obj2;
            List<GameObject>.Enumerator enumerator;
            int num6;
            GameObject obj3;
            <Refresh>c__AnonStorey30B storeyb;
            int num7;
            storeyb = new <Refresh>c__AnonStorey30B();
            if (autoCategorySelection == null)
            {
                goto Label_0051;
            }
            storeyb.category = GetTopMostPriorityCategory(this.mCategories);
            num = Array.FindIndex<ChallengeCategoryParam>(this.mCategories, new Predicate<ChallengeCategoryParam>(storeyb.<>m__2A6));
            this.mCurrentCategoryIndex = (num >= 0) ? num : 0;
            goto Label_006A;
        Label_0051:
            storeyb.category = this.mCategories[this.mCurrentCategoryIndex].iname;
        Label_006A:
            num2 = 0;
            goto Label_009B;
        Label_0071:
            button = this.mCategoryButtons[num2];
            button.SelectionFrame.get_gameObject().SetActive(num2 == this.mCurrentCategoryIndex);
            num2 += 1;
        Label_009B:
            if (num2 < this.mCategoryButtons.Count)
            {
                goto Label_0071;
            }
            if (doInitialize == null)
            {
                goto Label_01A4;
            }
            num3 = 0;
            goto Label_0185;
        Label_00B9:
            flag = 0;
            flag2 = 1;
            param = GetCurrentRootTrophy(this.mCategories[num3].iname);
            if (param == null)
            {
                goto Label_015D;
            }
            paramArray = GetChildeTrophies(param);
            num4 = 0;
            goto Label_0124;
        Label_00EB:
            param2 = paramArray[num4];
            state = GetTrophyCounter(param2);
            if (state.IsEnded != null)
            {
                goto Label_011E;
            }
            flag2 = 0;
            if (state.IsCompleted == null)
            {
                goto Label_011E;
            }
            flag = 1;
            goto Label_012F;
        Label_011E:
            num4 += 1;
        Label_0124:
            if (num4 < ((int) paramArray.Length))
            {
                goto Label_00EB;
            }
        Label_012F:
            state2 = GetTrophyCounter(param);
            flag3 = (flag != null) ? 1 : ((flag2 == null) ? 0 : (state2.IsEnded == 0));
            goto Label_0160;
        Label_015D:
            flag3 = 0;
        Label_0160:
            button2 = this.mCategoryButtons[num3];
            button2.Badge.get_gameObject().SetActive(flag3);
            num3 += 1;
        Label_0185:
            if (num3 >= ((int) this.mCategories.Length))
            {
                goto Label_01A4;
            }
            if (num3 < this.mCategoryButtons.Count)
            {
                goto Label_00B9;
            }
        Label_01A4:
            paramArray2 = GetRootTropies(storeyb.category);
            num5 = GetCurrentActiveTrophyIndex(paramArray2);
            if (doInitialize != null)
            {
                goto Label_01C7;
            }
            if (changeCategory == null)
            {
                goto Label_01CF;
            }
        Label_01C7:
            this.mCurrentPage = num5;
        Label_01CF:
            param3 = paramArray2[this.mCurrentPage];
            this.mRootCount = (int) paramArray2.Length;
            num7 = this.mCurrentPage + 1;
            this.PageNumText.set_text(&num7.ToString());
            this.PageMaxNumText.set_text(&this.mRootCount.ToString());
            this.ChangeRewardImage(param3);
            this.PageDotTemplate.SetActive(0);
            enumerator = this.mDotsList.GetEnumerator();
        Label_0237:
            try
            {
                goto Label_024C;
            Label_023C:
                obj2 = &enumerator.Current;
                Object.Destroy(obj2);
            Label_024C:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_023C;
                }
                goto Label_026A;
            }
            finally
            {
            Label_025D:
                ((List<GameObject>.Enumerator) enumerator).Dispose();
            }
        Label_026A:
            this.mDotsList.Clear();
            num6 = 0;
            goto Label_02D3;
        Label_027D:
            obj3 = Object.Instantiate<GameObject>(this.PageDotTemplate);
            this.mDotsList.Add(obj3);
            obj3.SetActive(1);
            obj3.GetComponent<Toggle>().set_isOn(num6 == this.mCurrentPage);
            obj3.get_transform().SetParent(this.PageDotsHolder.get_transform(), 0);
            num6 += 1;
        Label_02D3:
            if (num6 < this.mRootCount)
            {
                goto Label_027D;
            }
            this.BackPageButton.set_interactable(1);
            this.NextPageButton.set_interactable(1);
            if (this.mCurrentPage > 0)
            {
                goto Label_0310;
            }
            this.BackPageButton.set_interactable(0);
        Label_0310:
            if (this.mCurrentPage < (this.mRootCount - 1))
            {
                goto Label_032F;
            }
            this.NextPageButton.set_interactable(0);
        Label_032F:
            if (this.mCurrentPage <= num5)
            {
                goto Label_034D;
            }
            this.OpenNonAchievedPage(this.mCurrentPage);
            goto Label_0356;
        Label_034D:
            this.OpenNewPage(param3, doInitialize);
        Label_0356:
            return;
        }

        private void ResetMessageCloseCoroutine()
        {
            if (this.mMessageCloseCoroutine == null)
            {
                goto Label_001E;
            }
            base.StopCoroutine(this.mMessageCloseCoroutine);
            this.mMessageCloseCoroutine = null;
        Label_001E:
            return;
        }

        private void StartMessageCloseCoroutine()
        {
            this.mMessageCloseCoroutine = base.StartCoroutine(this.CloseMessageWindow());
            return;
        }

        [CompilerGenerated]
        private sealed class <Awake>c__AnonStorey30A
        {
            internal int index;
            internal ChallengeMission <>f__this;

            public <Awake>c__AnonStorey30A()
            {
                base..ctor();
                return;
            }

            internal void <>m__2A4()
            {
                this.<>f__this.OnClickCategoryButton(this.index);
                return;
            }
        }

        [CompilerGenerated]
        private sealed class <CloseMessageWindow>c__IteratorEA : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal int $PC;
            internal object $current;
            internal ChallengeMission <>f__this;

            public <CloseMessageWindow>c__IteratorEA()
            {
                base..ctor();
                return;
            }

            [DebuggerHidden]
            public void Dispose()
            {
                this.$PC = -1;
                return;
            }

            public bool MoveNext()
            {
                uint num;
                bool flag;
                num = this.$PC;
                this.$PC = -1;
                switch (num)
                {
                    case 0:
                        goto Label_0025;

                    case 1:
                        goto Label_004D;

                    case 2:
                        goto Label_0074;
                }
                goto Label_008C;
            Label_0025:
                if (this.<>f__this.CharMessageDelay >= 0f)
                {
                    goto Label_0052;
                }
            Label_003A:
                this.$current = null;
                this.$PC = 1;
                goto Label_008E;
            Label_004D:
                goto Label_003A;
            Label_0052:
                this.$current = new WaitForSeconds(this.<>f__this.CharMessageDelay);
                this.$PC = 2;
                goto Label_008E;
            Label_0074:
                this.<>f__this.CharMessageWindow.SetActive(0);
                this.$PC = -1;
            Label_008C:
                return 0;
            Label_008E:
                return 1;
                return flag;
            }

            [DebuggerHidden]
            public void Reset()
            {
                throw new NotSupportedException();
            }

            object IEnumerator<object>.Current
            {
                [DebuggerHidden]
                get
                {
                    return this.$current;
                }
            }

            object IEnumerator.Current
            {
                [DebuggerHidden]
                get
                {
                    return this.$current;
                }
            }
        }

        [CompilerGenerated]
        private sealed class <GetRootTropies>c__AnonStorey30D
        {
            internal TrophyParam trophy;

            public <GetRootTropies>c__AnonStorey30D()
            {
                base..ctor();
                return;
            }

            internal bool <>m__2A8(ChallengeCategoryParam cat)
            {
                return (cat.iname == this.trophy.Category);
            }
        }

        [CompilerGenerated]
        private sealed class <GetTrophiesSortedByPriority>c__AnonStorey30E
        {
            internal Dictionary<string, int> priorities;

            public <GetTrophiesSortedByPriority>c__AnonStorey30E()
            {
                base..ctor();
                return;
            }

            internal unsafe int <>m__2AB(TrophyParam trophy)
            {
                int num;
                if (this.priorities.TryGetValue(trophy.Category, &num) == null)
                {
                    goto Label_001A;
                }
                return num;
            Label_001A:
                return 0;
            }
        }

        [CompilerGenerated]
        private sealed class <OpenNewPage>c__AnonStorey30C
        {
            internal TrophyParam trophy;
            internal ChallengeMission <>f__this;

            public <OpenNewPage>c__AnonStorey30C()
            {
                base..ctor();
                return;
            }

            internal void <>m__2A7()
            {
                this.<>f__this.OnSelectItem(this.trophy);
                return;
            }
        }

        [CompilerGenerated]
        private sealed class <Refresh>c__AnonStorey30B
        {
            internal string category;

            public <Refresh>c__AnonStorey30B()
            {
                base..ctor();
                return;
            }

            internal bool <>m__2A6(ChallengeCategoryParam cat)
            {
                return (cat.iname == this.category);
            }
        }
    }
}

