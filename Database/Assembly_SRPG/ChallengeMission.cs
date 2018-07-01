// Decompiled with JetBrains decompiler
// Type: SRPG.ChallengeMission
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(1, "現在表示中のカテゴリを維持しつつ更新", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(103, "終了", FlowNode.PinTypes.Output, 103)]
  [FlowNode.Pin(104, "コンプリート報酬受け取りとレビュー表示", FlowNode.PinTypes.Output, 104)]
  [FlowNode.Pin(0, "更新", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(102, "コンプリート報酬受け取り", FlowNode.PinTypes.Output, 102)]
  [FlowNode.Pin(101, "報酬受け取り", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(100, "詳細", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(99, "メッセージ非表示", FlowNode.PinTypes.Input, 99)]
  [FlowNode.Pin(11, "前のページ", FlowNode.PinTypes.Input, 11)]
  [FlowNode.Pin(10, "次のページ", FlowNode.PinTypes.Input, 10)]
  public class ChallengeMission : MonoBehaviour, IFlowInterface
  {
    private const int PIN_REFRESH = 0;
    private const int PIN_REFRESH_KEEP_CATEGORY = 1;
    private const int PIN_NEXT_PAGE = 10;
    private const int PIN_BACK_PAGE = 11;
    private const int PIN_MESSAGE_CLOSE = 99;
    private const int PIN_DETAIL = 100;
    private const int PIN_REWARD = 101;
    private const int PIN_COMPLETE = 102;
    private const int PIN_END = 103;
    private const int PIN_COMPLETE_REVIEW = 104;
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

    public ChallengeMission()
    {
      base.\u002Ector();
    }

    public static TrophyParam[] GetTropies()
    {
      return MonoSingleton<GameManager>.GetInstanceDirect().Trophies;
    }

    public static TrophyParam GetTrophy(string key)
    {
      return MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.GetTrophy(key);
    }

    public static TrophyState GetTrophyCounter(TrophyParam trophy)
    {
      return MonoSingleton<GameManager>.GetInstanceDirect().Player.GetTrophyCounter(trophy, false);
    }

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 0:
          this.Refresh(true, true, false);
          break;
        case 1:
          this.Refresh(true, false, false);
          break;
        case 10:
          if (this.mCurrentPage >= this.mRootCount)
            break;
          ++this.mCurrentPage;
          this.Refresh(false, false, false);
          break;
        case 11:
          if (this.mCurrentPage <= 0)
            break;
          --this.mCurrentPage;
          this.Refresh(false, false, false);
          break;
        case 99:
          if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.CharMessageWindow, (UnityEngine.Object) null))
            break;
          this.ResetMessageCloseCoroutine();
          this.CharMessageWindow.SetActive(false);
          break;
      }
    }

    private void Awake()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.DetailWindow, (UnityEngine.Object) null))
        ((Component) this.DetailWindow).get_gameObject().SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.CharMessageWindow, (UnityEngine.Object) null))
        this.CharMessageWindow.SetActive(false);
      this.mCategories = ChallengeMission.GetOpeningCategory();
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.CategoryButtonTemplate, (UnityEngine.Object) null))
        return;
      this.CategoryButtonTemplate.SetActive(false);
      for (int index = 0; index < this.mCategories.Length; ++index)
      {
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: variable of a compiler-generated type
        ChallengeMission.\u003CAwake\u003Ec__AnonStorey30B awakeCAnonStorey30B = new ChallengeMission.\u003CAwake\u003Ec__AnonStorey30B();
        // ISSUE: reference to a compiler-generated field
        awakeCAnonStorey30B.\u003C\u003Ef__this = this;
        ChallengeCategoryParam mCategory = this.mCategories[index];
        GameObject gameObject = (GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) this.CategoryButtonTemplate);
        ChallengeMissionCategoryButton component = (ChallengeMissionCategoryButton) gameObject.GetComponent<ChallengeMissionCategoryButton>();
        // ISSUE: reference to a compiler-generated field
        awakeCAnonStorey30B.index = index;
        // ISSUE: method pointer
        ((UnityEvent) component.Button.get_onClick()).AddListener(new UnityAction((object) awakeCAnonStorey30B, __methodptr(\u003C\u003Em__31A)));
        component.SetChallengeCategory(mCategory);
        this.mCategoryButtons.Add(component);
        DataSource.Bind<ChallengeCategoryParam>(((Component) component).get_gameObject(), mCategory);
        gameObject.get_transform().SetParent(this.CategoryButtonContainer, false);
        gameObject.SetActive(true);
      }
    }

    private void StartMessageCloseCoroutine()
    {
      this.mMessageCloseCoroutine = this.StartCoroutine(this.CloseMessageWindow());
    }

    private void ResetMessageCloseCoroutine()
    {
      if (this.mMessageCloseCoroutine == null)
        return;
      this.StopCoroutine(this.mMessageCloseCoroutine);
      this.mMessageCloseCoroutine = (Coroutine) null;
    }

    private static ChallengeCategoryParam[] GetOpeningCategory()
    {
      ChallengeCategoryParam[] array = ((IEnumerable<ChallengeCategoryParam>) MonoSingleton<GameManager>.Instance.MasterParam.ChallengeCategories).OrderByDescending<ChallengeCategoryParam, int>((Func<ChallengeCategoryParam, int>) (cat => cat.prio)).ToArray<ChallengeCategoryParam>();
      List<ChallengeCategoryParam> challengeCategoryParamList = new List<ChallengeCategoryParam>();
      foreach (ChallengeCategoryParam category in array)
      {
        if (ChallengeMission.IsCategoryOpening(category))
          challengeCategoryParamList.Add(category);
      }
      return challengeCategoryParamList.ToArray();
    }

    private static bool IsCategoryOpening(ChallengeCategoryParam category)
    {
      if (category.begin_at == null || category.end_at == null)
        return true;
      DateTime serverTime = TimeManager.ServerTime;
      return serverTime >= category.begin_at.DateTimes && serverTime <= category.end_at.DateTimes;
    }

    private void OnClickCategoryButton(int index)
    {
      if (index == this.mCurrentCategoryIndex)
        return;
      this.mCurrentCategoryIndex = index;
      this.Refresh(false, false, true);
    }

    private void Refresh(bool doInitialize, bool autoCategorySelection, bool changeCategory = false)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      ChallengeMission.\u003CRefresh\u003Ec__AnonStorey30C refreshCAnonStorey30C = new ChallengeMission.\u003CRefresh\u003Ec__AnonStorey30C();
      if (autoCategorySelection)
      {
        // ISSUE: reference to a compiler-generated field
        refreshCAnonStorey30C.category = ChallengeMission.GetTopMostPriorityCategory(this.mCategories);
        // ISSUE: reference to a compiler-generated method
        int index = Array.FindIndex<ChallengeCategoryParam>(this.mCategories, new Predicate<ChallengeCategoryParam>(refreshCAnonStorey30C.\u003C\u003Em__31C));
        this.mCurrentCategoryIndex = index >= 0 ? index : 0;
      }
      else
      {
        // ISSUE: reference to a compiler-generated field
        refreshCAnonStorey30C.category = this.mCategories[this.mCurrentCategoryIndex].iname;
      }
      for (int index = 0; index < this.mCategoryButtons.Count; ++index)
        ((Component) this.mCategoryButtons[index].SelectionFrame).get_gameObject().SetActive(index == this.mCurrentCategoryIndex);
      if (doInitialize)
      {
        for (int index = 0; index < this.mCategories.Length && index < this.mCategoryButtons.Count; ++index)
        {
          bool flag1 = false;
          bool flag2 = true;
          TrophyParam currentRootTrophy = ChallengeMission.GetCurrentRootTrophy(this.mCategories[index].iname);
          bool flag3;
          if (currentRootTrophy != null)
          {
            foreach (TrophyParam childeTrophy in ChallengeMission.GetChildeTrophies(currentRootTrophy))
            {
              TrophyState trophyCounter = ChallengeMission.GetTrophyCounter(childeTrophy);
              if (!trophyCounter.IsEnded)
              {
                flag2 = false;
                if (trophyCounter.IsCompleted)
                {
                  flag1 = true;
                  break;
                }
              }
            }
            TrophyState trophyCounter1 = ChallengeMission.GetTrophyCounter(currentRootTrophy);
            flag3 = flag1 || flag2 && !trophyCounter1.IsEnded;
          }
          else
            flag3 = false;
          ((Component) this.mCategoryButtons[index].Badge).get_gameObject().SetActive(flag3);
        }
      }
      // ISSUE: reference to a compiler-generated field
      TrophyParam[] rootTropies = ChallengeMission.GetRootTropies(refreshCAnonStorey30C.category);
      int activeTrophyIndex = ChallengeMission.GetCurrentActiveTrophyIndex(rootTropies);
      if (doInitialize || changeCategory)
        this.mCurrentPage = activeTrophyIndex;
      TrophyParam trophyParam = rootTropies[this.mCurrentPage];
      this.mRootCount = rootTropies.Length;
      this.PageNumText.set_text((this.mCurrentPage + 1).ToString());
      this.PageMaxNumText.set_text(this.mRootCount.ToString());
      this.ChangeRewardImage(trophyParam);
      this.PageDotTemplate.SetActive(false);
      using (List<GameObject>.Enumerator enumerator = this.mDotsList.GetEnumerator())
      {
        while (enumerator.MoveNext())
          UnityEngine.Object.Destroy((UnityEngine.Object) enumerator.Current);
      }
      this.mDotsList.Clear();
      for (int index = 0; index < this.mRootCount; ++index)
      {
        GameObject gameObject = (GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) this.PageDotTemplate);
        this.mDotsList.Add(gameObject);
        gameObject.SetActive(true);
        ((Toggle) gameObject.GetComponent<Toggle>()).set_isOn(index == this.mCurrentPage);
        gameObject.get_transform().SetParent(this.PageDotsHolder.get_transform(), false);
      }
      ((Selectable) this.BackPageButton).set_interactable(true);
      ((Selectable) this.NextPageButton).set_interactable(true);
      if (this.mCurrentPage <= 0)
        ((Selectable) this.BackPageButton).set_interactable(false);
      if (this.mCurrentPage >= this.mRootCount - 1)
        ((Selectable) this.NextPageButton).set_interactable(false);
      if (this.mCurrentPage > activeTrophyIndex)
        this.OpenNonAchievedPage(this.mCurrentPage);
      else
        this.OpenNewPage(trophyParam, doInitialize);
    }

    private void ChangeRewardImage(TrophyParam trophy)
    {
      DataSource.Bind<TrophyParam>(((Component) this.ImageReward).get_gameObject(), trophy);
      GameParameter.UpdateAll(((Component) this.ImageReward).get_gameObject());
    }

    private void OpenNewPage(TrophyParam rootTrophy, bool doInitialize)
    {
      if (rootTrophy == null)
      {
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 103);
      }
      else
      {
        TrophyParam[] childeTrophies = ChallengeMission.GetChildeTrophies(rootTrophy);
        if (childeTrophies.Length != this.Items.Count)
        {
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 103);
        }
        else
        {
          ((Component) this.Shadow).get_gameObject().SetActive(false);
          bool flag1 = false;
          bool flag2 = true;
          for (int index = 0; index < this.Items.Count; ++index)
          {
            // ISSUE: object of a compiler-generated type is created
            // ISSUE: variable of a compiler-generated type
            ChallengeMission.\u003COpenNewPage\u003Ec__AnonStorey30D pageCAnonStorey30D = new ChallengeMission.\u003COpenNewPage\u003Ec__AnonStorey30D();
            // ISSUE: reference to a compiler-generated field
            pageCAnonStorey30D.\u003C\u003Ef__this = this;
            // ISSUE: reference to a compiler-generated field
            pageCAnonStorey30D.trophy = childeTrophies[index];
            // ISSUE: reference to a compiler-generated field
            TrophyState trophyCounter = ChallengeMission.GetTrophyCounter(pageCAnonStorey30D.trophy);
            if (!trophyCounter.IsEnded)
            {
              flag2 = false;
              if (trophyCounter.IsCompleted)
                flag1 = true;
            }
            // ISSUE: method pointer
            this.Items[index].OnClick = new UnityAction((object) pageCAnonStorey30D, __methodptr(\u003C\u003Em__31D));
            // ISSUE: reference to a compiler-generated field
            DataSource.Bind<TrophyParam>(((Component) this.Items[index]).get_gameObject(), pageCAnonStorey30D.trophy);
            ItemParam data = (ItemParam) null;
            // ISSUE: reference to a compiler-generated field
            if (pageCAnonStorey30D.trophy.Coin != 0)
            {
              data = MonoSingleton<GameManager>.Instance.GetItemParam("$COIN");
            }
            else
            {
              // ISSUE: reference to a compiler-generated field
              // ISSUE: reference to a compiler-generated field
              if (pageCAnonStorey30D.trophy.Items != null && pageCAnonStorey30D.trophy.Items.Length > 0)
              {
                // ISSUE: reference to a compiler-generated field
                data = MonoSingleton<GameManager>.Instance.GetItemParam(pageCAnonStorey30D.trophy.Items[0].iname);
              }
            }
            if (data != null)
              DataSource.Bind<ItemParam>(((Component) this.Items[index]).get_gameObject(), data);
            this.Items[index].Refresh();
          }
          ((Component) this.CompleteBadge).get_gameObject().SetActive(flag2);
          if (!flag2 && !doInitialize)
            return;
          TrophyState trophyCounter1 = ChallengeMission.GetTrophyCounter(rootTrophy);
          if (this.UseCharMessage && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.MessageText, (UnityEngine.Object) null) && !trophyCounter1.IsEnded)
          {
            string str1 = (string) null;
            if (flag1)
              str1 = LocalizedText.Get("sys.CHALLENGE_MSG_CLEAR");
            else if (PlayerPrefsUtility.GetInt(PlayerPrefsUtility.CHALLENGEMISSION_HAS_SHOW_MESSAGE, 0) == 0)
            {
              string str2 = string.Empty;
              if (rootTrophy.Gold != 0)
                str2 = string.Format(LocalizedText.Get("sys.CHALLENGE_REWARD_GOLD"), (object) rootTrophy.Gold);
              else if (rootTrophy.Exp != 0)
                str2 = string.Format(LocalizedText.Get("sys.CHALLENGE_REWARD_EXP"), (object) rootTrophy.Exp);
              else if (rootTrophy.Coin != 0)
                str2 = string.Format(LocalizedText.Get("sys.CHALLENGE_REWARD_COIN"), (object) rootTrophy.Coin);
              else if (rootTrophy.Stamina != 0)
                str2 = string.Format(LocalizedText.Get("sys.CHALLENGE_REWARD_STAMINA"), (object) rootTrophy.Stamina);
              else if (rootTrophy.Items != null && rootTrophy.Items.Length > 0)
              {
                ItemParam itemParam = MonoSingleton<GameManager>.Instance.GetItemParam(rootTrophy.Items[0].iname);
                if (itemParam != null)
                {
                  if (itemParam.type == EItemType.Unit)
                  {
                    UnitParam unitParam = MonoSingleton<GameManager>.Instance.GetUnitParam(itemParam.iname);
                    if (unitParam != null)
                      str2 = LocalizedText.Get("sys.CHALLENGE_DETAIL_REWARD_UNIT", (object) ((int) unitParam.rare + 1), (object) unitParam.name);
                  }
                  else
                    str2 = LocalizedText.Get("sys.CHALLENGE_REWARD_ITEM", (object) itemParam.name, (object) rootTrophy.Items[0].Num);
                }
              }
              str1 = LocalizedText.Get("sys.CHALLENGE_MSG_INFO", new object[1]
              {
                (object) str2
              });
              PlayerPrefsUtility.SetInt(PlayerPrefsUtility.CHALLENGEMISSION_HAS_SHOW_MESSAGE, 1, false);
            }
            if (str1 != null && !MonoSingleton<GameManager>.Instance.IsTutorial())
            {
              this.MessageText.set_text(str1);
              if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.CharMessageWindow, (UnityEngine.Object) null))
              {
                this.ResetMessageCloseCoroutine();
                this.CharMessageWindow.SetActive(true);
                this.StartMessageCloseCoroutine();
              }
            }
          }
          if (!flag2 || trophyCounter1.IsEnded)
            return;
          MonoSingleton<GameManager>.GetInstanceDirect().Player.OnChallengeMissionComplete(rootTrophy.iname);
          GlobalVars.SelectedChallengeMissionTrophy = rootTrophy.iname;
          GlobalVars.SelectedTrophy.Set(rootTrophy.iname);
          PlayerPrefsUtility.SetInt(PlayerPrefsUtility.CHALLENGEMISSION_HAS_SHOW_MESSAGE, 0, false);
          if (rootTrophy.iname == "CHALLENGE_01")
          {
            Debug.Log((object) ("<color=yellow>11 " + rootTrophy.iname + "</color>"));
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 104);
          }
          else
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 102);
        }
      }
    }

    private void OpenNonAchievedPage(int newPage)
    {
      ((Component) this.Shadow).get_gameObject().SetActive(true);
      this.ShadowText.set_text(LocalizedText.Get("sys.SG_CHALLENGE_UNLOCK_COND", new object[1]
      {
        (object) newPage
      }));
      ((Component) this.CompleteBadge).get_gameObject().SetActive(false);
      using (List<ChallengeMissionItem>.Enumerator enumerator = this.Items.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          ChallengeMissionItem current = enumerator.Current;
          current.OnClick = (UnityAction) null;
          DataSource component = (DataSource) ((Component) current).GetComponent<DataSource>();
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
            UnityEngine.Object.DestroyImmediate((UnityEngine.Object) component);
          current.Refresh();
        }
      }
    }

    private string HankakuToZenkakuNumber(int num)
    {
      switch (num)
      {
        case 0:
          return "１";
        case 1:
          return "１";
        case 2:
          return "２";
        case 3:
          return "３";
        case 4:
          return "４";
        case 5:
          return "５";
        case 6:
          return "６";
        case 7:
          return "７";
        case 8:
          return "８";
        case 9:
          return "９";
        default:
          return "０";
      }
    }

    [DebuggerHidden]
    private IEnumerator CloseMessageWindow()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new ChallengeMission.\u003CCloseMessageWindow\u003Ec__IteratorE4() { \u003C\u003Ef__this = this };
    }

    private void OnSelectItem(TrophyParam selectTrophy)
    {
      if (ChallengeMission.GetTrophyCounter(selectTrophy).IsCompleted)
      {
        GlobalVars.SelectedChallengeMissionTrophy = selectTrophy.iname;
        GlobalVars.SelectedTrophy.Set(selectTrophy.iname);
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 101);
      }
      else
      {
        if ((MonoSingleton<GameManager>.Instance.Player.TutorialFlags & 1L) == 0L)
          return;
        DataSource.Bind<TrophyParam>(((Component) this.DetailWindow).get_gameObject(), selectTrophy);
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
      }
    }

    public static TrophyParam[] GetRootTropies()
    {
      ChallengeCategoryParam[] challengeCategories = MonoSingleton<GameManager>.Instance.MasterParam.ChallengeCategories;
      List<TrophyParam> trophyParamList = new List<TrophyParam>();
      TrophyParam[] tropies = ChallengeMission.GetTropies();
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      ChallengeMission.\u003CGetRootTropies\u003Ec__AnonStorey30E tropiesCAnonStorey30E = new ChallengeMission.\u003CGetRootTropies\u003Ec__AnonStorey30E();
      foreach (TrophyParam trophyParam in tropies)
      {
        // ISSUE: reference to a compiler-generated field
        tropiesCAnonStorey30E.trophy = trophyParam;
        // ISSUE: reference to a compiler-generated field
        if (tropiesCAnonStorey30E.trophy.IsChallengeMissionRoot)
        {
          // ISSUE: reference to a compiler-generated method
          ChallengeCategoryParam challengeCategoryParam = ((IEnumerable<ChallengeCategoryParam>) challengeCategories).FirstOrDefault<ChallengeCategoryParam>(new Func<ChallengeCategoryParam, bool>(tropiesCAnonStorey30E.\u003C\u003Em__31E));
          if (challengeCategoryParam != null)
          {
            if (challengeCategoryParam.begin_at == null || challengeCategoryParam.end_at == null)
            {
              // ISSUE: reference to a compiler-generated field
              trophyParamList.Add(tropiesCAnonStorey30E.trophy);
            }
            else
            {
              DateTime serverTime = TimeManager.ServerTime;
              if (serverTime >= challengeCategoryParam.begin_at.DateTimes && serverTime <= challengeCategoryParam.end_at.DateTimes)
              {
                // ISSUE: reference to a compiler-generated field
                trophyParamList.Add(tropiesCAnonStorey30E.trophy);
              }
            }
          }
        }
      }
      return trophyParamList.ToArray();
    }

    public static TrophyParam[] GetRootTropies(string category)
    {
      List<TrophyParam> trophyParamList = new List<TrophyParam>();
      foreach (TrophyParam tropy in ChallengeMission.GetTropies())
      {
        if (tropy.IsChallengeMissionRoot && tropy.Category == category)
          trophyParamList.Add(tropy);
      }
      return trophyParamList.ToArray();
    }

    public static TrophyParam GetCurrentRootTrophy(string category)
    {
      TrophyParam[] rootTropies = ChallengeMission.GetRootTropies(category);
      if (rootTropies == null || rootTropies.Length == 0)
        return (TrophyParam) null;
      foreach (TrophyParam trophy in rootTropies)
      {
        if (!ChallengeMission.GetTrophyCounter(trophy).IsEnded)
          return trophy;
      }
      return (TrophyParam) null;
    }

    public static TrophyParam[] GetChildeTrophies(TrophyParam root)
    {
      List<TrophyParam> trophyParamList = new List<TrophyParam>();
      foreach (TrophyParam tropy in ChallengeMission.GetTropies())
      {
        if (tropy.IsChallengeMission && tropy.Category == root.Category && root.iname == tropy.ParentTrophy)
          trophyParamList.Add(tropy);
      }
      return trophyParamList.ToArray();
    }

    private static string GetTopMostPriorityCategory(ChallengeCategoryParam[] categories)
    {
      TrophyParam mostPriorityTrophy = ChallengeMission.GetTopMostPriorityTrophy(categories);
      if (mostPriorityTrophy == null)
        return (string) null;
      return mostPriorityTrophy.Category;
    }

    public static TrophyParam GetTopMostPriorityTrophy()
    {
      return ChallengeMission.GetTopMostPriorityTrophy(ChallengeMission.GetOpeningCategory());
    }

    public static TrophyParam GetTopMostPriorityTrophy(ChallengeCategoryParam[] categories)
    {
      TrophyParam trophyParam1 = (TrophyParam) null;
      TrophyParam trophyParam2 = (TrophyParam) null;
      TrophyParam trophyParam3 = (TrophyParam) null;
      foreach (ChallengeCategoryParam category in categories)
      {
        bool flag = true;
        TrophyParam currentRootTrophy = ChallengeMission.GetCurrentRootTrophy(category.iname);
        if (currentRootTrophy != null)
        {
          foreach (TrophyParam childeTrophy in ChallengeMission.GetChildeTrophies(currentRootTrophy))
          {
            TrophyState trophyCounter = ChallengeMission.GetTrophyCounter(childeTrophy);
            if (!trophyCounter.IsEnded)
            {
              flag = false;
              if (trophyCounter.IsCompleted)
              {
                if (trophyParam2 == null)
                  trophyParam2 = childeTrophy;
              }
              else if (trophyParam1 == null)
                trophyParam1 = childeTrophy;
            }
          }
          TrophyState trophyCounter1 = ChallengeMission.GetTrophyCounter(currentRootTrophy);
          if (!trophyCounter1.IsEnded)
          {
            if (flag)
            {
              if (trophyParam3 == null)
                trophyParam3 = currentRootTrophy;
            }
            else if (trophyCounter1.IsCompleted)
            {
              if (trophyParam2 == null)
                trophyParam2 = currentRootTrophy;
            }
            else if (trophyParam1 == null)
              trophyParam1 = currentRootTrophy;
          }
        }
      }
      return trophyParam3 ?? trophyParam2 ?? trophyParam1;
    }

    public static TrophyParam[] GetTrophiesSortedByPriority(TrophyParam[] trophies)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      ChallengeMission.\u003CGetTrophiesSortedByPriority\u003Ec__AnonStorey30F priorityCAnonStorey30F = new ChallengeMission.\u003CGetTrophiesSortedByPriority\u003Ec__AnonStorey30F();
      ChallengeCategoryParam[] challengeCategories = MonoSingleton<GameManager>.Instance.MasterParam.ChallengeCategories;
      if (challengeCategories == null || challengeCategories.Length < 1)
        return (TrophyParam[]) null;
      // ISSUE: reference to a compiler-generated field
      priorityCAnonStorey30F.priorities = ((IEnumerable<ChallengeCategoryParam>) challengeCategories).ToDictionary<ChallengeCategoryParam, string, int>((Func<ChallengeCategoryParam, string>) (c => c.iname), (Func<ChallengeCategoryParam, int>) (c => c.prio));
      if (trophies == null || trophies.Length < 1)
        return (TrophyParam[]) null;
      // ISSUE: reference to a compiler-generated method
      return ((IEnumerable<TrophyParam>) trophies).OrderByDescending<TrophyParam, int>(new Func<TrophyParam, int>(priorityCAnonStorey30F.\u003C\u003Em__321)).ToArray<TrophyParam>();
    }

    public static TrophyParam[] GetRootTrophiesSortedByPriority()
    {
      return ChallengeMission.GetTrophiesSortedByPriority(ChallengeMission.GetRootTropies());
    }

    private static int GetCurrentActiveTrophyIndex(TrophyParam[] trophies)
    {
      if (trophies == null || trophies.Length == 0)
        return -1;
      int num = 0;
      foreach (TrophyParam trophy in trophies)
      {
        if (ChallengeMission.GetTrophyCounter(trophy).IsEnded)
          ++num;
      }
      if (num >= trophies.Length)
        return trophies.Length - 1;
      return num;
    }
  }
}
