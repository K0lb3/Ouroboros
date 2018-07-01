// Decompiled with JetBrains decompiler
// Type: SRPG.MailWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(11, "現在の頁更新", FlowNode.PinTypes.Input, 101)]
  [FlowNode.Pin(303, "武具選択アイテムで所持武具がいっぱい", FlowNode.PinTypes.Output, 203)]
  [FlowNode.Pin(302, "武具選択アイテム", FlowNode.PinTypes.Output, 202)]
  [FlowNode.Pin(301, "アイテム選択アイテム", FlowNode.PinTypes.Output, 201)]
  [FlowNode.Pin(300, "ユニット選択アイテム", FlowNode.PinTypes.Output, 200)]
  [FlowNode.Pin(60, "開封", FlowNode.PinTypes.Output, 106)]
  [FlowNode.Pin(31, "頁内開封", FlowNode.PinTypes.Input, 105)]
  [FlowNode.Pin(30, "一件開封", FlowNode.PinTypes.Input, 104)]
  [FlowNode.Pin(14, "再初期化", FlowNode.PinTypes.Input, 103)]
  [FlowNode.Pin(1, "初期化", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(50, "初期化終了(未読あり)", FlowNode.PinTypes.Output, 2)]
  [FlowNode.Pin(51, "初期化終了(未読なし)", FlowNode.PinTypes.Output, 3)]
  [FlowNode.Pin(2, "期限なし", FlowNode.PinTypes.Input, 52)]
  [FlowNode.Pin(3, "期限あり", FlowNode.PinTypes.Input, 53)]
  [FlowNode.Pin(4, "開封済み", FlowNode.PinTypes.Input, 54)]
  [FlowNode.Pin(12, "前の頁", FlowNode.PinTypes.Input, 55)]
  [FlowNode.Pin(13, "次の頁", FlowNode.PinTypes.Input, 56)]
  [FlowNode.Pin(52, "メールリスト更新", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(200, "ページ取得", FlowNode.PinTypes.Output, 102)]
  public class MailWindow : MonoBehaviour, IFlowInterface
  {
    private const int PIN_ID_INITIALIZE = 1;
    private const int PIN_ID_TAB_NOT_PERIOD = 2;
    private const int PIN_ID_TAB_PERIOD = 3;
    private const int PIN_ID_TAB_READ = 4;
    private const int PIN_ID_CURRENT_PAGE = 11;
    private const int PIN_ID_PREV_PAGE = 12;
    private const int PIN_ID_NEXT_PAGE = 13;
    private const int PIN_ID_REFRESH = 14;
    private const int PIN_ID_READ = 30;
    private const int PIN_ID_READ_PAGE = 31;
    private const int PIN_ID_INITIALIZED = 50;
    private const int PIN_ID_INITIALIZED_EMPTY = 51;
    private const int PIN_ID_SUCCESS = 52;
    private const int PIN_ID_REQUEST_READ = 60;
    private const int PIN_ID_REQUEST_PAGE = 200;
    private const int PIN_ID_UNIT_SELECT = 300;
    private const int PIN_ID_ITEM_SELECT = 301;
    private const int PIN_ID_ARTIFACT_SELECT = 302;
    private const int PIN_ID_ARTIFACT_OVER = 303;
    private MailWindow.TabData notPeriodTab;
    private MailWindow.TabData periodTab;
    private MailWindow.TabData readTab;
    private MailWindow.TabType currentTab;
    [SerializeField]
    private MailWindow.TabType startTab;
    [SerializeField]
    private BitmapText currentPageText;
    [SerializeField]
    private BitmapText maxPageText;
    [SerializeField]
    private GameObject readAllButton;
    [SerializeField]
    private ScrollRect scrollRect;
    [SerializeField]
    private Button prevPageButton;
    [SerializeField]
    private Button nextPageButton;
    [SerializeField]
    private GameObject flowParent;

    public MailWindow()
    {
      base.\u002Ector();
    }

    private void SetMailRequestData(int page, bool isPeriod, bool isRead)
    {
      DataSource.Bind<MailWindow.MailPageRequestData>(this.flowParent, new MailWindow.MailPageRequestData()
      {
        page = page,
        isRead = isRead,
        isPeriod = isPeriod
      });
    }

    private void SetReadRequestData(int page, bool isPeriod, long[] mailIDs)
    {
      DataSource.Bind<MailWindow.MailReadRequestData>(this.flowParent, new MailWindow.MailReadRequestData()
      {
        page = page,
        mailIDs = mailIDs,
        isPeriod = isPeriod
      });
    }

    private void ActivateOutputLinks(int pinID)
    {
      FlowNode_GameObject.ActivateOutputLinks((Component) this, pinID);
    }

    private void LateActivateOutputLinks(int pinID)
    {
      this.StartCoroutine(this.CoroutineActivateOutputLinks(pinID));
    }

    [DebuggerHidden]
    private IEnumerator CoroutineActivateOutputLinks(int pinID)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new MailWindow.\u003CCoroutineActivateOutputLinks\u003Ec__Iterator103() { pinID = pinID, \u003C\u0024\u003EpinID = pinID, \u003C\u003Ef__this = this };
    }

    public void Activated(int pinID)
    {
      int num = pinID;
      switch (num)
      {
        case 1:
          this.currentTab = this.startTab;
          this.Refresh();
          bool flag1 = false;
          bool flag2 = false;
          switch (this.startTab)
          {
            case MailWindow.TabType.NotPeriod:
              flag1 = MonoSingleton<GameManager>.Instance.Player.UnreadMail;
              flag2 = MonoSingleton<GameManager>.Instance.Player.UnreadMailPeriod;
              break;
            case MailWindow.TabType.Period:
              flag1 = MonoSingleton<GameManager>.Instance.Player.UnreadMailPeriod;
              flag2 = MonoSingleton<GameManager>.Instance.Player.UnreadMail;
              break;
            case MailWindow.TabType.Read:
              flag1 = true;
              break;
          }
          if (flag1)
          {
            this.LateActivateOutputLinks(50);
            break;
          }
          if (flag2)
          {
            this.LateActivateOutputLinks(51);
            break;
          }
          this.LateActivateOutputLinks(50);
          break;
        case 2:
          this.TabChange(MailWindow.TabType.NotPeriod);
          if (this.currentTabData.currentPageIsReady)
          {
            this.UpdateUI();
            this.ActivateOutputLinks(52);
            break;
          }
          this.RequestCurrentMails();
          this.ActivateOutputLinks(200);
          break;
        case 3:
          this.TabChange(MailWindow.TabType.Period);
          if (this.currentTabData.currentPageIsReady)
          {
            this.UpdateUI();
            this.ActivateOutputLinks(52);
            break;
          }
          this.RequestCurrentMails();
          this.ActivateOutputLinks(200);
          break;
        case 4:
          this.TabChange(MailWindow.TabType.Read);
          if (this.currentTabData.currentPageIsReady)
          {
            this.UpdateUI();
            this.ActivateOutputLinks(52);
            break;
          }
          this.RequestCurrentMails();
          this.ActivateOutputLinks(200);
          break;
        case 11:
          if (this.currentTabData.SetPage(MonoSingleton<GameManager>.Instance.Player.MailPage))
          {
            this.UpdateUI();
            MonoSingleton<GameManager>.Instance.Player.CurrentMails = this.currentTabData.currentPageData.mails;
          }
          this.ActivateOutputLinks(52);
          break;
        case 12:
          if (!this.currentTabData.HasPrev())
            break;
          if (this.PrevPage())
          {
            this.UpdateUI();
            this.ActivateOutputLinks(52);
            break;
          }
          this.RequestPrevMails();
          this.ActivateOutputLinks(200);
          break;
        case 13:
          if (!this.currentTabData.HasNext())
            break;
          if (this.NexePage())
          {
            this.UpdateUI();
            this.ActivateOutputLinks(52);
            break;
          }
          this.RequestNextMails();
          this.ActivateOutputLinks(200);
          break;
        case 14:
          this.Refresh();
          if (this.currentTabData.SetPage(MonoSingleton<GameManager>.Instance.Player.MailPage))
          {
            this.UpdateUI();
            MonoSingleton<GameManager>.Instance.Player.CurrentMails = this.currentTabData.currentPageData.mails;
          }
          this.ActivateOutputLinks(52);
          break;
        default:
          if (num != 30)
          {
            if (num != 31)
              break;
            this.RequestRead(this.currentTabData.currentPageData.mails.FindAll((Predicate<MailData>) (md => !md.Contains(GiftTypes.Unit | GiftTypes.SelectUnitItem | GiftTypes.SelectItem | GiftTypes.SelectArtifactItem))).ConvertAll<long>((Converter<MailData, long>) (md => md.mid)).ToArray());
            this.ActivateOutputLinks(60);
            break;
          }
          long[] mailIDs = new long[1]{ (long) GlobalVars.SelectedMailUniqueID };
          MailData mailData = this.currentTabData.currentPageData.mails.Find((Predicate<MailData>) (md => md.mid == (long) GlobalVars.SelectedMailUniqueID));
          if (mailData == null)
            break;
          if (mailData.Contains(GiftTypes.SelectUnitItem | GiftTypes.SelectItem | GiftTypes.SelectArtifactItem))
          {
            GlobalVars.SelectedMailPeriod.Set(mailData.period);
            GlobalVars.SelectedMailPage.Set(this.currentTabData.currentPage);
          }
          if (mailData.Contains(GiftTypes.SelectArtifactItem))
          {
            if (MonoSingleton<GameManager>.Instance.Player.ArtifactNum + 1 > (int) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.ArtifactBoxCap)
            {
              this.ActivateOutputLinks(303);
              break;
            }
            this.ActivateOutputLinks(302);
            break;
          }
          if (mailData.Contains(GiftTypes.SelectItem))
          {
            this.ActivateOutputLinks(301);
            break;
          }
          if (mailData.Contains(GiftTypes.SelectUnitItem))
          {
            this.ActivateOutputLinks(300);
            break;
          }
          this.RequestRead(mailIDs);
          this.ActivateOutputLinks(60);
          break;
      }
    }

    private void UpdateUI()
    {
      int num = this.currentTabData.currentPageData.page;
      int pageMax = this.currentTabData.currentPageData.pageMax;
      if (pageMax == 0)
        num = 0;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.scrollRect, (UnityEngine.Object) null))
        this.scrollRect.SetNormalizedPosition(Vector2.get_one(), true);
      this.currentPageText.text = num.ToString();
      this.maxPageText.text = pageMax.ToString();
      ((Selectable) this.nextPageButton).set_interactable(this.currentTabData.HasNext());
      ((Selectable) this.prevPageButton).set_interactable(this.currentTabData.HasPrev());
      if (this.currentTabData.currentPageData.mails.Count < 1 || this.currentTab == MailWindow.TabType.Read)
      {
        this.readAllButton.SetActive(false);
      }
      else
      {
        int count = this.currentTabData.currentPageData.mails.FindAll((Predicate<MailData>) (mailData => mailData.Contains(GiftTypes.Unit | GiftTypes.SelectUnitItem | GiftTypes.SelectItem | GiftTypes.SelectArtifactItem))).Count;
        if (count > 0 && this.currentTabData.currentPageData.mails.Count == count)
          this.readAllButton.SetActive(false);
        else
          this.readAllButton.SetActive(true);
      }
    }

    private void RequestRead(long[] mailIDs)
    {
      switch (this.currentTab)
      {
        case MailWindow.TabType.NotPeriod:
          this.SetReadRequestData(this.currentTabData.currentPage, false, mailIDs);
          break;
        case MailWindow.TabType.Period:
          this.SetReadRequestData(this.currentTabData.currentPage, true, mailIDs);
          break;
      }
    }

    private void RequestCurrentMails()
    {
      MailWindow.TabData currentTabData = this.currentTabData;
      if (currentTabData == null)
        return;
      switch (currentTabData.tabType)
      {
        case MailWindow.TabType.NotPeriod:
          this.SetMailRequestData(currentTabData.currentPage, false, false);
          break;
        case MailWindow.TabType.Period:
          this.SetMailRequestData(currentTabData.currentPage, true, false);
          break;
        case MailWindow.TabType.Read:
          this.SetMailRequestData(currentTabData.currentPage, false, true);
          break;
      }
    }

    private void RequestNextMails()
    {
      MailWindow.TabData currentTabData = this.currentTabData;
      if (currentTabData == null || !currentTabData.HasNext())
        return;
      switch (currentTabData.tabType)
      {
        case MailWindow.TabType.NotPeriod:
          this.SetMailRequestData(currentTabData.currentPage + 1, false, false);
          break;
        case MailWindow.TabType.Period:
          this.SetMailRequestData(currentTabData.currentPage + 1, true, false);
          break;
        case MailWindow.TabType.Read:
          this.SetMailRequestData(currentTabData.currentPage + 1, false, true);
          break;
      }
    }

    private void RequestPrevMails()
    {
      MailWindow.TabData currentTabData = this.currentTabData;
      if (currentTabData == null || !currentTabData.HasPrev())
        return;
      switch (currentTabData.tabType)
      {
        case MailWindow.TabType.NotPeriod:
          this.SetMailRequestData(currentTabData.currentPage - 1, false, false);
          break;
        case MailWindow.TabType.Period:
          this.SetMailRequestData(currentTabData.currentPage - 1, true, false);
          break;
        case MailWindow.TabType.Read:
          this.SetMailRequestData(currentTabData.currentPage - 1, false, true);
          break;
      }
    }

    public void TabChange(MailWindow.TabType tabType)
    {
      MailWindow.TabType currentTab = this.currentTab;
      this.currentTab = tabType;
      if (currentTab != this.currentTab)
        MonoSingleton<GameManager>.Instance.Player.CurrentMails = new List<MailData>(1);
      this.AddPage(0);
    }

    private MailWindow.TabData GetTab(MailWindow.TabType tabType)
    {
      switch (tabType)
      {
        case MailWindow.TabType.NotPeriod:
          return this.notPeriodTab;
        case MailWindow.TabType.Period:
          return this.periodTab;
        case MailWindow.TabType.Read:
          return this.readTab;
        default:
          return (MailWindow.TabData) null;
      }
    }

    private MailWindow.TabData currentTabData
    {
      get
      {
        return this.GetTab(this.currentTab);
      }
    }

    private bool NexePage()
    {
      return this.AddPage(1);
    }

    private bool PrevPage()
    {
      return this.AddPage(-1);
    }

    private bool AddPage(int addValue)
    {
      MailWindow.TabData currentTabData = this.currentTabData;
      if (currentTabData == null)
        return false;
      int page = currentTabData.currentPage + addValue;
      MailPageData pageData = currentTabData.GetPageData(page);
      if (pageData == null)
        return false;
      currentTabData.currentPage = page;
      MonoSingleton<GameManager>.Instance.Player.CurrentMails = pageData.mails;
      return true;
    }

    private void Refresh()
    {
      Func<MailWindow.TabData, MailWindow.TabType, MailWindow.TabData> func = (Func<MailWindow.TabData, MailWindow.TabType, MailWindow.TabData>) ((tabData, tabType) =>
      {
        if (tabData == null)
        {
          tabData = new MailWindow.TabData();
          tabData.tabType = tabType;
        }
        return tabData;
      });
      this.notPeriodTab = func(this.notPeriodTab, MailWindow.TabType.NotPeriod);
      this.periodTab = func(this.periodTab, MailWindow.TabType.Period);
      this.readTab = func(this.readTab, MailWindow.TabType.Read);
      this.notPeriodTab.Clear();
      this.periodTab.Clear();
      this.readTab.Clear();
    }

    public enum TabType : byte
    {
      NotPeriod,
      Period,
      Read,
    }

    internal class TabData
    {
      internal int currentPage = 1;
      internal List<MailPageData> pageDataList;
      internal int pageMax;
      internal int mailCount;
      internal MailWindow.TabType tabType;

      internal bool currentPageIsReady
      {
        get
        {
          return this.currentPageData != null;
        }
      }

      internal MailPageData currentPageData
      {
        get
        {
          return this.GetPageData(this.currentPage);
        }
      }

      internal bool SetPage(MailPageData mailPageData)
      {
        if (mailPageData == null)
          return false;
        if (this.pageDataList.Find((Predicate<MailPageData>) (pd => pd.page == mailPageData.page)) == null)
        {
          this.pageDataList.Add(mailPageData);
          this.currentPage = mailPageData.page;
          this.pageMax = mailPageData.pageMax;
          this.mailCount = mailPageData.mailCount;
        }
        return true;
      }

      internal void Clear()
      {
        this.pageDataList = new List<MailPageData>();
        this.currentPage = 1;
        this.pageMax = 1;
        this.mailCount = 0;
      }

      internal bool HasPrev()
      {
        MailPageData pageData = this.GetPageData(this.currentPage);
        if (pageData != null)
          return pageData.hasPrev;
        return false;
      }

      internal bool HasNext()
      {
        MailPageData pageData = this.GetPageData(this.currentPage);
        if (pageData != null)
          return pageData.hasNext;
        return false;
      }

      internal MailPageData PrevPageData()
      {
        MailPageData pageData = this.GetPageData(this.currentPage - 1);
        if (pageData != null)
          --this.currentPage;
        return pageData;
      }

      internal MailPageData NextPageData()
      {
        MailPageData pageData = this.GetPageData(this.currentPage + 1);
        if (pageData != null)
          ++this.currentPage;
        return pageData;
      }

      internal MailPageData GetPageData(int page)
      {
        return this.pageDataList.Find((Predicate<MailPageData>) (pageData => pageData.page == page));
      }
    }

    public class MailPageRequestData
    {
      public int page;
      public bool isRead;
      public bool isPeriod;
    }

    public class MailReadRequestData
    {
      public long[] mailIDs;
      public int page;
      public bool isPeriod;
    }
  }
}
