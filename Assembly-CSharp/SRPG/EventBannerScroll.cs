// Decompiled with JetBrains decompiler
// Type: SRPG.EventBannerScroll
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(48, "Refreshed", FlowNode.PinTypes.Output, 1)]
  [FlowNode.Pin(49, "ItemEmpty", FlowNode.PinTypes.Output, 2)]
  [FlowNode.Pin(10, "PageNext", FlowNode.PinTypes.Input, 3)]
  [FlowNode.Pin(11, "PagePrev", FlowNode.PinTypes.Input, 4)]
  [FlowNode.Pin(53, "ToGacha", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(52, "ToShop", FlowNode.PinTypes.Output, 9)]
  [FlowNode.Pin(54, "ToURL", FlowNode.PinTypes.Output, 11)]
  [FlowNode.Pin(55, "ToMulti", FlowNode.PinTypes.Output, 8)]
  [FlowNode.Pin(51, "ToEvent", FlowNode.PinTypes.Output, 7)]
  [FlowNode.Pin(50, "ToStory", FlowNode.PinTypes.Output, 6)]
  [FlowNode.Pin(1, "Refresh", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(12, "Select", FlowNode.PinTypes.Input, 5)]
  public class EventBannerScroll : ScrollRect, IFlowInterface
  {
    private const int PIN_REFRESH = 1;
    private const int PIN_PAGE_NEXT = 10;
    private const int PIN_PAGE_PREV = 11;
    private const int PIN_SELECT = 12;
    private const int PIN_REFRESHED = 48;
    private const int PIN_EMPTY = 49;
    private const int PIN_TO_STORY = 50;
    private const int PIN_TO_EVENT = 51;
    private const int PIN_TO_SHOP = 52;
    private const int PIN_TO_GACHA = 53;
    private const int PIN_TO_URL = 54;
    private const int PIN_TO_MULTI = 55;
    private const string BannerPathOfNormals = "Banners/";
    private const string BannerPathOfShop = "LimitedShopBanner/";
    public ToggleGroup PageToggleGroup;
    public GameObject TemplateBannerNormal;
    public GameObject TemplateBannerShop;
    public GameObject TemplatePageIcon;
    public float Interval;
    private bool mDragging;
    private int mPage;
    private float mElapsed;
    private IEnumerator mMove;

    public EventBannerScroll()
    {
      base.\u002Ector();
    }

    void IFlowInterface.Activated(int pinID)
    {
      switch (pinID)
      {
        case 1:
          if (this.Refresh())
          {
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 48);
            break;
          }
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 49);
          break;
        case 10:
          if (this.mMove != null)
            break;
          this.mMove = this.movePage(this.mPage, this.mPage + 1);
          break;
        case 11:
          if (this.mMove != null)
            break;
          this.mMove = this.movePage(this.mPage, this.mPage - 1);
          break;
        case 12:
          BannerParam currentPageBannerParam = this.getCurrentPageBannerParam();
          if (currentPageBannerParam == null)
            break;
          switch (currentPageBannerParam.type)
          {
            case BannerType.storyQuest:
              if (!this.setQuestVariables(currentPageBannerParam.sval, true))
                return;
              GlobalVars.SelectedQuestID = (string) null;
              FlowNode_GameObject.ActivateOutputLinks((Component) this, 50);
              return;
            case BannerType.eventQuest:
              this.setQuestVariables(currentPageBannerParam.sval, false);
              FlowNode_GameObject.ActivateOutputLinks((Component) this, 51);
              return;
            case BannerType.multiQuest:
              FlowNode_GameObject.ActivateOutputLinks((Component) this, 55);
              return;
            case BannerType.gacha:
              GlobalVars.SelectedGachaTableId = currentPageBannerParam.sval;
              FlowNode_GameObject.ActivateOutputLinks((Component) this, 53);
              return;
            case BannerType.shop:
              FlowNode_GameObject.ActivateOutputLinks((Component) this, 52);
              return;
            case BannerType.url:
              if (!string.IsNullOrEmpty(currentPageBannerParam.sval))
                Application.OpenURL(currentPageBannerParam.sval);
              FlowNode_GameObject.ActivateOutputLinks((Component) this, 54);
              return;
            default:
              return;
          }
      }
    }

    private int PageCount
    {
      get
      {
        return ((Transform) this.get_content()).get_childCount() - 1;
      }
    }

    private int PageNow
    {
      get
      {
        return this.mPage;
      }
    }

    protected virtual void Start()
    {
      ((UIBehaviour) this).Start();
      this.set_inertia(false);
      this.set_movementType((ScrollRect.MovementType) 0);
      this.set_horizontal(true);
      this.set_vertical(false);
      // ISSUE: method pointer
      ((UnityEvent<Vector2>) this.get_onValueChanged()).AddListener(new UnityAction<Vector2>((object) this, __methodptr(OnValueChanged)));
    }

    public virtual void OnBeginDrag(PointerEventData eventData)
    {
      base.OnBeginDrag(eventData);
      this.mDragging = true;
      this.mElapsed = 0.0f;
    }

    public virtual void OnEndDrag(PointerEventData eventData)
    {
      this.mDragging = false;
      base.OnEndDrag(eventData);
    }

    private void Update()
    {
      if (this.mDragging || this.mMove != null || ((Transform) this.get_content()).get_childCount() <= 1)
        return;
      this.mElapsed += Time.get_deltaTime();
      if ((double) this.mElapsed < (double) this.Interval)
        return;
      this.mMove = this.movePage(this.mPage, this.mPage + 1);
    }

    protected virtual void LateUpdate()
    {
      if (this.PageCount > 0)
      {
        if (this.mMove != null)
        {
          if (!this.mMove.MoveNext())
            this.mMove = (IEnumerator) null;
        }
        else if (!this.mDragging)
        {
          RectTransform[] rectTransformArray = new RectTransform[((Transform) this.get_content()).get_childCount()];
          for (int index = 0; index < ((Transform) this.get_content()).get_childCount(); ++index)
            rectTransformArray[index] = ((Transform) this.get_content()).GetChild(index) as RectTransform;
          float num = 0.0f;
          Vector2 anchoredPosition = this.get_content().get_anchoredPosition();
          for (int index = 0; index < rectTransformArray.Length; ++index)
          {
            Rect rect = rectTransformArray[index].get_rect();
            // ISSUE: explicit reference operation
            float width = ((Rect) @rect).get_width();
            if ((double) num + (double) width / 2.0 > -anchoredPosition.x)
            {
              anchoredPosition.x = (__Null) (double) Mathf.Lerp((float) anchoredPosition.x, -num, 0.1f);
              this.SetContentAnchoredPosition(anchoredPosition);
              int page = index % this.PageCount;
              if (this.mPage != page)
              {
                this.onPageChanged(page);
                break;
              }
              break;
            }
            num += width;
          }
        }
      }
      base.LateUpdate();
    }

    private bool Refresh()
    {
      while (((Transform) this.get_content()).get_childCount() != 0)
        Object.Destroy((Object) ((Transform) this.get_content()).GetChild(0));
      while (((Component) this.PageToggleGroup).get_transform().get_childCount() != 0)
        Object.Destroy((Object) ((Component) this.PageToggleGroup).get_transform().GetChild(0));
      BannerParam[] bannerParamArray = this.makeValidBannerParams();
      if (bannerParamArray.Length != 0)
      {
        for (int index1 = 0; index1 < bannerParamArray.Length + 1; ++index1)
        {
          int index2 = index1 % bannerParamArray.Length;
          GameObject gameObject1 = bannerParamArray[index2].type != BannerType.shop ? (GameObject) Object.Instantiate<GameObject>((M0) this.TemplateBannerNormal) : (GameObject) Object.Instantiate<GameObject>((M0) this.TemplateBannerShop);
          Vector3 localScale1 = gameObject1.get_transform().get_localScale();
          gameObject1.get_transform().SetParent((Transform) this.get_content());
          gameObject1.get_transform().set_localScale(localScale1);
          gameObject1.SetActive(true);
          DataSource.Bind<BannerParam>(gameObject1, bannerParamArray[index2]);
          if (index1 < bannerParamArray.Length)
          {
            GameObject gameObject2 = (GameObject) Object.Instantiate<GameObject>((M0) this.TemplatePageIcon);
            Vector3 localScale2 = gameObject1.get_transform().get_localScale();
            gameObject2.get_transform().SetParent(((Component) this.PageToggleGroup).get_transform());
            gameObject2.get_transform().set_localScale(localScale2);
            gameObject2.SetActive(true);
            if (index1 == 0)
              ((Toggle) gameObject2.GetComponent<Toggle>()).set_isOn(true);
          }
        }
      }
      this.mPage = 0;
      this.mElapsed = 0.0f;
      ((Component) this.PageToggleGroup).get_gameObject().SetActive(bannerParamArray.Length > 1);
      return bannerParamArray.Length != 0;
    }

    private BannerParam[] makeValidBannerParams()
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      BannerParam[] banners = instance.MasterParam.Banners;
      if (banners == null)
        return new BannerParam[0];
      List<BannerParam> bannerParamList = new List<BannerParam>();
      GachaParam[] gachas = instance.Gachas;
      QuestParam[] availableQuests = instance.Player.AvailableQuests;
      QuestParam lastStoryQuest = instance.Player.FindLastStoryQuest();
      long serverTime = Network.GetServerTime();
      DateTime now = TimeManager.FromUnixTime(serverTime);
      for (int index = 0; index < banners.Length; ++index)
      {
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: variable of a compiler-generated type
        EventBannerScroll.\u003CmakeValidBannerParams\u003Ec__AnonStorey243 paramsCAnonStorey243 = new EventBannerScroll.\u003CmakeValidBannerParams\u003Ec__AnonStorey243();
        // ISSUE: reference to a compiler-generated field
        paramsCAnonStorey243.banner = banners[index];
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated method
        if (!string.IsNullOrEmpty(paramsCAnonStorey243.banner.banner) && bannerParamList.FindIndex(new Predicate<BannerParam>(paramsCAnonStorey243.\u003C\u003Em__270)) == -1)
        {
          // ISSUE: reference to a compiler-generated field
          if (paramsCAnonStorey243.banner.type == BannerType.shop)
          {
            // ISSUE: reference to a compiler-generated field
            if (!string.IsNullOrEmpty(paramsCAnonStorey243.banner.sval))
            {
              // ISSUE: reference to a compiler-generated method
              Array.Find<JSON_ShopListArray.Shops>(instance.LimitedShopList, new Predicate<JSON_ShopListArray.Shops>(paramsCAnonStorey243.\u003C\u003Em__271));
            }
            // ISSUE: reference to a compiler-generated field
            if (!paramsCAnonStorey243.banner.IsAvailablePeriod(now))
              continue;
          }
          else
          {
            // ISSUE: reference to a compiler-generated field
            if (paramsCAnonStorey243.banner.type == BannerType.storyQuest)
            {
              if (lastStoryQuest != null)
              {
                QuestParam questParam;
                // ISSUE: reference to a compiler-generated field
                if (string.IsNullOrEmpty(paramsCAnonStorey243.banner.sval))
                {
                  questParam = lastStoryQuest;
                }
                else
                {
                  // ISSUE: reference to a compiler-generated method
                  questParam = Array.Find<QuestParam>(availableQuests, new Predicate<QuestParam>(paramsCAnonStorey243.\u003C\u003Em__272));
                  if (questParam == null || questParam.iname != lastStoryQuest.iname && questParam.state == QuestStates.New)
                    questParam = lastStoryQuest;
                }
                if (!questParam.IsDateUnlock(serverTime))
                  continue;
              }
              else
                continue;
            }
            else
            {
              // ISSUE: reference to a compiler-generated field
              // ISSUE: reference to a compiler-generated field
              if (paramsCAnonStorey243.banner.type == BannerType.eventQuest || paramsCAnonStorey243.banner.type == BannerType.multiQuest)
              {
                // ISSUE: reference to a compiler-generated field
                if (!string.IsNullOrEmpty(paramsCAnonStorey243.banner.sval))
                {
                  // ISSUE: reference to a compiler-generated method
                  QuestParam questParam = Array.Find<QuestParam>(availableQuests, new Predicate<QuestParam>(paramsCAnonStorey243.\u003C\u003Em__273));
                  if (questParam == null || !questParam.IsDateUnlock(serverTime))
                    continue;
                }
              }
              else
              {
                // ISSUE: reference to a compiler-generated field
                if (paramsCAnonStorey243.banner.type == BannerType.gacha)
                {
                  // ISSUE: reference to a compiler-generated field
                  if (!string.IsNullOrEmpty(paramsCAnonStorey243.banner.sval))
                  {
                    // ISSUE: reference to a compiler-generated method
                    GachaParam gachaParam = Array.Find<GachaParam>(gachas, new Predicate<GachaParam>(paramsCAnonStorey243.\u003C\u003Em__274));
                    if (gachaParam != null)
                    {
                      // ISSUE: reference to a compiler-generated field
                      paramsCAnonStorey243.banner.begin_at = TimeManager.FromUnixTime(gachaParam.startat).ToString();
                      // ISSUE: reference to a compiler-generated field
                      paramsCAnonStorey243.banner.end_at = TimeManager.FromUnixTime(gachaParam.endat).ToString();
                      // ISSUE: reference to a compiler-generated field
                      if (!paramsCAnonStorey243.banner.IsAvailablePeriod(now))
                        continue;
                    }
                    else
                      continue;
                  }
                }
                else
                {
                  // ISSUE: reference to a compiler-generated field
                  // ISSUE: reference to a compiler-generated field
                  // ISSUE: reference to a compiler-generated field
                  if (paramsCAnonStorey243.banner.type == BannerType.url && (string.IsNullOrEmpty(paramsCAnonStorey243.banner.sval) || !paramsCAnonStorey243.banner.IsAvailablePeriod(now)))
                    continue;
                }
              }
            }
          }
          // ISSUE: reference to a compiler-generated field
          bannerParamList.Add(paramsCAnonStorey243.banner);
        }
      }
      for (int index1 = 0; index1 < bannerParamList.Count - 1; ++index1)
      {
        for (int index2 = index1 + 1; index2 < bannerParamList.Count; ++index2)
        {
          if (bannerParamList[index1].priority > bannerParamList[index2].priority)
          {
            BannerParam bannerParam = bannerParamList[index1];
            bannerParamList[index1] = bannerParamList[index2];
            bannerParamList[index2] = bannerParam;
          }
        }
      }
      return bannerParamList.ToArray();
    }

    [DebuggerHidden]
    private IEnumerator movePage(int from, int to)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new EventBannerScroll.\u003CmovePage\u003Ec__IteratorAB() { to = to, from = from, \u003C\u0024\u003Eto = to, \u003C\u0024\u003Efrom = from, \u003C\u003Ef__this = this };
    }

    private void OnValueChanged(Vector2 value)
    {
      Rect rect1 = this.get_content().get_rect();
      // ISSUE: explicit reference operation
      double width1 = (double) ((Rect) @rect1).get_width();
      Rect rect2 = this.get_viewport().get_rect();
      // ISSUE: explicit reference operation
      double width2 = (double) ((Rect) @rect2).get_width();
      float num1 = (float) (width1 - width2);
      float num2 = (float) -this.get_content().get_anchoredPosition().x;
      if ((double) num2 > (double) num1)
      {
        this.SetContentAnchoredPosition(new Vector2(-(num2 % num1), 0.0f));
      }
      else
      {
        if ((double) num2 >= 0.0)
          return;
        this.SetContentAnchoredPosition(new Vector2((float) -((double) num1 + (double) num2), 0.0f));
      }
    }

    private void onPageChanged(int page)
    {
      this.mPage = page;
      int num = 0;
      foreach (Toggle componentsInChild in (Toggle[]) ((Component) this.PageToggleGroup).GetComponentsInChildren<Toggle>())
      {
        componentsInChild.set_isOn(num == this.mPage);
        ++num;
      }
    }

    private float getPageOffset(int page)
    {
      RectTransform[] rectTransformArray = new RectTransform[((Transform) this.get_content()).get_childCount()];
      for (int index = 0; index < ((Transform) this.get_content()).get_childCount(); ++index)
        rectTransformArray[index] = ((Transform) this.get_content()).GetChild(index) as RectTransform;
      float num1 = 0.0f;
      for (int index = 0; index < rectTransformArray.Length; ++index)
      {
        if (index == page)
          return num1;
        double num2 = (double) num1;
        Rect rect = rectTransformArray[index].get_rect();
        // ISSUE: explicit reference operation
        double width = (double) ((Rect) @rect).get_width();
        num1 = (float) (num2 + width);
      }
      return 0.0f;
    }

    private BannerParam getCurrentPageBannerParam()
    {
      if (this.mPage >= ((Transform) this.get_content()).get_childCount())
        return (BannerParam) null;
      DataSource component = (DataSource) ((Component) ((Transform) this.get_content()).GetChild(this.mPage)).GetComponent<DataSource>();
      if (Object.op_Equality((Object) component, (Object) null))
        return (BannerParam) null;
      return component.FindDataOfClass<BannerParam>((BannerParam) null);
    }

    private float decelerate(float value)
    {
      return (float) (1.0 - (1.0 - (double) value) * (1.0 - (double) value) * (1.0 - (double) value));
    }

    private bool setQuestVariables(string questId, bool story)
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      QuestParam[] availableQuests = instance.Player.AvailableQuests;
      for (int index = 0; index < availableQuests.Length; ++index)
      {
        if (availableQuests[index].iname == questId)
        {
          GlobalVars.SelectedSection.Set(availableQuests[index].Chapter.section);
          GlobalVars.SelectedChapter.Set(availableQuests[index].ChapterID);
          return true;
        }
      }
      if (story)
      {
        QuestParam lastStoryQuest = instance.Player.FindLastStoryQuest();
        if (lastStoryQuest != null && lastStoryQuest.IsDateUnlock(Network.GetServerTime()))
        {
          GlobalVars.SelectedSection.Set(lastStoryQuest.Chapter.section);
          GlobalVars.SelectedChapter.Set(lastStoryQuest.ChapterID);
          return true;
        }
      }
      return false;
    }
  }
}
