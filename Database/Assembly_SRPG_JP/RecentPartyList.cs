// Decompiled with JetBrains decompiler
// Type: SRPG.RecentPartyList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(100, "Output", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(1, "Request", FlowNode.PinTypes.Input, 0)]
  public class RecentPartyList : MonoBehaviour, IFlowInterface
  {
    [SerializeField]
    private RecentPartyPanel PartyPanelTemplate;
    [SerializeField]
    private GameObject PartyPanelHolder;
    [SerializeField]
    private Text ErrorText;
    [SerializeField]
    private ScrollRect ScrollRect;
    [SerializeField]
    private SRPG_Button PrevButton;
    [SerializeField]
    private SRPG_Button NextButton;
    [SerializeField]
    private GameObject PageInfo;
    [SerializeField]
    private Text CurrentPage;
    [SerializeField]
    private Text MaxPage;
    [SerializeField]
    private GameObject CheckBox;
    private QuestParam mCurrentQuest;
    private RecentPartyList.JSON_List[] mLastRecords;
    private List<UnitData[]> allParties;
    private List<SupportData> allHelpUnits;
    private List<int[]> allAchieves;
    private List<ItemData[]> allUsedItems;
    private List<RecentPartyPanel> allTeamPanel;
    private bool mOwnedUnitOnly;
    private int mCurrentPage;
    private int mMaxPage;
    private int mUnitId;

    public RecentPartyList()
    {
      base.\u002Ector();
    }

    public void Activated(int pinID)
    {
      this.mCurrentQuest = MonoSingleton<GameManager>.Instance.FindQuest(GlobalVars.SelectedQuestID);
      this.mCurrentPage = 1;
      this.Connect();
    }

    private void Initialize()
    {
      this.allParties.Clear();
      this.allHelpUnits.Clear();
      this.allAchieves.Clear();
      using (List<RecentPartyPanel>.Enumerator enumerator = this.allTeamPanel.GetEnumerator())
      {
        while (enumerator.MoveNext())
          UnityEngine.Object.Destroy((UnityEngine.Object) ((Component) enumerator.Current).get_gameObject());
      }
      this.allTeamPanel.Clear();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.PartyPanelTemplate, (UnityEngine.Object) null))
        ((Component) this.PartyPanelTemplate).get_gameObject().SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ErrorText, (UnityEngine.Object) null))
        ((Component) this.ErrorText).get_gameObject().SetActive(false);
      this.ScrollRect.set_verticalNormalizedPosition(1f);
      this.ScrollRect.set_horizontalNormalizedPosition(1f);
    }

    private void Connect()
    {
      Network.RequestAPI((WebAPI) new ReqBtlComRecord(this.mCurrentQuest.iname, this.mCurrentPage, this.mUnitId, new Network.ResponseCallback(this.ResponseCallback)), false);
    }

    private void ResponseCallback(WWWResult www)
    {
      if (FlowNode_Network.HasCommonError(www))
        return;
      if (Network.IsError)
      {
        switch (Network.ErrCode)
        {
          case Network.EErrCode.BattleRecordMaintenance:
          case Network.EErrCode.RecordLimitUpload:
            Network.RemoveAPI();
            Network.ResetError();
            this.EnableErrorText(LocalizedText.Get("sys.PARTYEDITOR_RECENT_CLEARED_PARTY_ERROR_UPLOAD_LIMIT_2"));
            this.DisableUnnecessaryUIOnError();
            break;
          default:
            FlowNode_Network.Retry();
            break;
        }
      }
      else
      {
        WebAPI.JSON_BodyResponse<RecentPartyList.JSON_Body> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<RecentPartyList.JSON_Body>>(www.text);
        this.mLastRecords = jsonObject.body.list;
        this.mMaxPage = jsonObject.body.option.totalPage;
        Network.RemoveAPI();
        this.Refresh();
      }
    }

    private void Refresh()
    {
      this.Initialize();
      if (this.mLastRecords == null)
      {
        this.EnableErrorText(LocalizedText.Get("sys.PARTYEDITOR_RECENT_CLEARED_PARTY_NOT_FOUND"));
        this.DisableUnnecessaryUIOnError();
      }
      else
      {
        this.LoadAllParties(this.mLastRecords);
        if (this.mOwnedUnitOnly)
        {
          PlayerData player = MonoSingleton<GameManager>.Instance.Player;
          for (int index = 0; index < this.allParties.Count && index < this.allTeamPanel.Count; ++index)
          {
            foreach (UnitData unitData in this.allParties[index])
            {
              if (unitData != null && player.FindUnitDataByUnitID(unitData.UnitParam.iname) == null)
                ((Component) this.allTeamPanel[index]).get_gameObject().SetActive(false);
            }
          }
        }
        if (this.allTeamPanel.All<RecentPartyPanel>((Func<RecentPartyPanel, bool>) (panel => !((Component) panel).get_gameObject().get_activeSelf())))
          this.EnableErrorText(LocalizedText.Get("sys.PARTYEDITOR_RECENT_CLEARED_PARTY_NOT_FOUND_OWNED_UNIT"));
        this.SetActiveUICoponent();
        this.CurrentPage.set_text(this.mCurrentPage.ToString());
        this.MaxPage.set_text(this.mMaxPage.ToString());
      }
    }

    private void DisableUnnecessaryUIOnError()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ScrollRect, (UnityEngine.Object) null))
        ((Component) this.ScrollRect).get_gameObject().SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.PageInfo, (UnityEngine.Object) null))
        this.PageInfo.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.PrevButton, (UnityEngine.Object) null))
        ((Component) this.PrevButton).get_gameObject().SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.NextButton, (UnityEngine.Object) null))
        ((Component) this.NextButton).get_gameObject().SetActive(false);
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.CheckBox, (UnityEngine.Object) null))
        return;
      this.CheckBox.get_gameObject().SetActive(false);
    }

    private void EnableErrorText(string errorMessage)
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ErrorText, (UnityEngine.Object) null))
        return;
      ((Component) this.ErrorText).get_gameObject().SetActive(true);
      this.ErrorText.set_text(errorMessage);
    }

    private void SetActiveUICoponent()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ScrollRect, (UnityEngine.Object) null))
        ((Component) this.ScrollRect).get_gameObject().SetActive(true);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.PageInfo, (UnityEngine.Object) null))
        this.PageInfo.SetActive(true);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.PrevButton, (UnityEngine.Object) null))
      {
        ((Component) this.PrevButton).get_gameObject().SetActive(true);
        ((Selectable) this.PrevButton).set_interactable(this.mCurrentPage > 1);
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.NextButton, (UnityEngine.Object) null))
      {
        ((Component) this.NextButton).get_gameObject().SetActive(true);
        ((Selectable) this.NextButton).set_interactable(this.mCurrentPage < this.mMaxPage);
      }
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.CheckBox, (UnityEngine.Object) null))
        return;
      this.CheckBox.get_gameObject().SetActive(true);
    }

    private RecentPartyPanel CreatePartyPanel(UnitData[] party, SupportData support, RecentPartyList.JSON_List record, SRPG_Button.ButtonClickEvent buttonClickCallback)
    {
      RecentPartyPanel component = (RecentPartyPanel) ((GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) ((Component) this.PartyPanelTemplate).get_gameObject())).GetComponent<RecentPartyPanel>();
      component.SetPartyInfo(party, support, this.mCurrentQuest);
      component.SetUnitIconPressedCallback(buttonClickCallback);
      component.SetUserName(record.detail.my.name);
      component.SetUserRank(record.detail.my.lv.ToString());
      component.SetClearDate(this.GetClearedTime(record.created_at));
      for (int index = 0; index < record.achieved.Length; ++index)
        component.SetConditionStarActive(index, record.achieved[index] != 0);
      if (this.mCurrentQuest.type == QuestTypes.Tower)
        component.SetConditionItemActive(2, false);
      ((Component) component).get_gameObject().SetActive(true);
      return component;
    }

    private string GetClearedTime(string iso8601String)
    {
      CultureInfo cultureInfo = new CultureInfo("ja-JP");
      DateTime result;
      if (DateTime.TryParseExact(iso8601String, TimeManager.ISO_8601_FORMAT, (IFormatProvider) cultureInfo, DateTimeStyles.None, out result))
        return result.ToString("yyyy/M/d");
      return string.Empty;
    }

    private void LoadAllParties(RecentPartyList.JSON_List[] winRecords)
    {
      int num = 0;
      foreach (RecentPartyList.JSON_List winRecord in winRecords)
      {
        int index = num;
        SRPG_Button.ButtonClickEvent buttonClickEvent = (SRPG_Button.ButtonClickEvent) (b => this.OnButtonClick(index));
        if (winRecord.id > this.mUnitId)
          this.mUnitId = winRecord.id;
        UnitData[] party = this.LoadParty((IEnumerable<Json_Unit>) winRecord.detail.my.units);
        SupportData support = this.LoadHelpUnit(winRecord.detail.help);
        ItemData[] itemDataArray = this.LoadUsedItems(winRecord.detail.items);
        this.allParties.Add(((IEnumerable<UnitData>) party).ToArray<UnitData>());
        this.allHelpUnits.Add(support);
        this.allAchieves.Add(winRecord.achieved);
        this.allUsedItems.Add(itemDataArray);
        RecentPartyPanel partyPanel = this.CreatePartyPanel(party, support, winRecord, buttonClickEvent);
        this.allTeamPanel.Add(partyPanel);
        ((Component) partyPanel).get_transform().SetParent(this.PartyPanelHolder.get_gameObject().get_transform(), false);
        SRPG_Button component = (SRPG_Button) ((Component) partyPanel).GetComponent<SRPG_Button>();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
          component.AddListener(buttonClickEvent);
        ++num;
      }
    }

    private UnitData[] LoadParty(IEnumerable<Json_Unit> jsonUnit)
    {
      List<UnitData> unitDataList = new List<UnitData>();
      foreach (Json_Unit json in jsonUnit)
      {
        UnitData unitData = new UnitData();
        if (json == null || string.IsNullOrEmpty(json.iname))
        {
          unitDataList.Add((UnitData) null);
        }
        else
        {
          unitData.Deserialize(json);
          unitDataList.Add(unitData);
        }
      }
      return unitDataList.ToArray();
    }

    private SupportData LoadHelpUnit(Json_Support json)
    {
      if (json == null)
        return (SupportData) null;
      SupportData supportData = new SupportData();
      supportData.Deserialize(json);
      return supportData;
    }

    private ItemData[] LoadUsedItems(RecentPartyList.JSON_Item[] jsonItem)
    {
      if (jsonItem == null)
        return (ItemData[]) null;
      List<ItemData> itemDataList = new List<ItemData>();
      foreach (RecentPartyList.JSON_Item jsonItem1 in jsonItem)
      {
        ItemData itemData = new ItemData();
        itemData.Setup(0L, jsonItem1.iname, jsonItem1.num);
        itemDataList.Add(itemData);
      }
      return itemDataList.ToArray();
    }

    private void OnButtonClick(int index)
    {
      if (index < 0 || index >= this.allParties.Count || index >= this.allAchieves.Count)
        return;
      GlobalVars.UserSelectionPartyDataInfo = new GlobalVars.UserSelectionPartyData()
      {
        unitData = this.allParties[index],
        supportData = this.allHelpUnits[index],
        achievements = this.allAchieves[index],
        usedItems = this.allUsedItems[index]
      };
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
    }

    public void OnToggleEnabled()
    {
      this.mOwnedUnitOnly = true;
      this.Refresh();
    }

    public void OnToggleDisabled()
    {
      this.mOwnedUnitOnly = false;
      this.Refresh();
    }

    public void OnPrevButtonPressed()
    {
      if (this.mCurrentPage - 1 < 1)
        return;
      --this.mCurrentPage;
      this.Connect();
    }

    public void OnNextButtonPressed()
    {
      if (this.mCurrentPage + 1 > this.mMaxPage)
        return;
      ++this.mCurrentPage;
      this.Connect();
    }

    public class JSON_Body
    {
      public RecentPartyList.JSON_List[] list;
      public RecentPartyList.JSON_Option option;
    }

    public class JSON_Option
    {
      public int totalPage;
    }

    public class JSON_List
    {
      public int id;
      public int[] achieved;
      public string created_at;
      public RecentPartyList.JSON_Detail detail;
    }

    public class JSON_Detail
    {
      public RecentPartyList.JSON_My my;
      public Json_Support help;
      public RecentPartyList.JSON_Item[] items;
    }

    public class JSON_My
    {
      public int lv;
      public string name;
      public Json_Unit[] units;
    }

    public class JSON_Item
    {
      public string iname;
      public int num;
    }
  }
}
