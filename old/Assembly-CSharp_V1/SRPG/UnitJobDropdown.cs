// Decompiled with JetBrains decompiler
// Type: SRPG.UnitJobDropdown
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(1, "Apply Job", FlowNode.PinTypes.Input, 0)]
  public class UnitJobDropdown : Pulldown, IFlowInterface
  {
    public static UnitJobDropdown.JobChangeEvent OnJobChange = (UnitJobDropdown.JobChangeEvent) (unitUniqueID => {});
    public bool RefreshOnStart = true;
    public RawImage JobIcon;
    public RawImage ItemJobIcon;
    public GameObject GameParamterRoot;
    private bool mRequestSent;
    private UnitData mTargetUnit;
    private string mOriginalJobID;

    public void Activated(int pinID)
    {
      if (pinID != 1 || this.mTargetUnit == null || !(this.mTargetUnit.CurrentJob.JobID != this.mOriginalJobID))
        return;
      this.RequestJobChange(false);
    }

    protected override void Start()
    {
      base.Start();
      UnitJobDropdown unitJobDropdown = this;
      // ISSUE: method pointer
      unitJobDropdown.OnSelectionChange = (UnityAction<int>) System.Delegate.Combine((System.Delegate) unitJobDropdown.OnSelectionChange, (System.Delegate) new UnityAction<int>((object) this, __methodptr(JobChanged)));
      if (!this.RefreshOnStart)
        return;
      this.Refresh();
    }

    private void RequestJobChange(bool immediate)
    {
      if (this.mRequestSent)
        return;
      PlayerPartyTypes dataOfClass = DataSource.FindDataOfClass<PlayerPartyTypes>(((Component) this).get_gameObject(), PlayerPartyTypes.Max);
      this.mRequestSent = true;
      if ((this.mTargetUnit.TempFlags & UnitData.TemporaryFlags.TemporaryUnitData) != (UnitData.TemporaryFlags) 0)
        MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID(this.mTargetUnit.UniqueID).SetJobFor(dataOfClass, this.mTargetUnit.CurrentJob);
      UnitJobDropdown.OnJobChange(this.mTargetUnit.UniqueID);
      ReqUnitJob reqUnitJob = (this.mTargetUnit.TempFlags & UnitData.TemporaryFlags.TemporaryUnitData) != (UnitData.TemporaryFlags) 0 ? new ReqUnitJob(this.mTargetUnit.UniqueID, this.mTargetUnit.CurrentJob.UniqueID, PartyData.GetStringFromPartyType(dataOfClass), new Network.ResponseCallback(this.JobChangeResult)) : new ReqUnitJob(this.mTargetUnit.UniqueID, this.mTargetUnit.CurrentJob.UniqueID, new Network.ResponseCallback(this.JobChangeResult));
      if (immediate)
      {
        int num = (int) Network.RequestAPIImmediate((WebAPI) reqUnitJob, true);
      }
      else
        Network.RequestAPI((WebAPI) reqUnitJob, false);
    }

    private void JobChangeResult(WWWResult www)
    {
      if (Network.IsError)
      {
        switch (Network.ErrCode)
        {
          case Network.EErrCode.NoJobSetJob:
          case Network.EErrCode.CantSelectJob:
          case Network.EErrCode.NoUnitSetJob:
            FlowNode_Network.Failed();
            break;
          default:
            FlowNode_Network.Retry();
            break;
        }
      }
      else
      {
        WebAPI.JSON_BodyResponse<Json_PlayerDataAll> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_PlayerDataAll>>(www.text);
        try
        {
          MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.player);
          MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.units);
          MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.items);
          Network.RemoveAPI();
        }
        catch (Exception ex)
        {
          DebugUtility.LogException(ex);
          FlowNode_Network.Failed();
          return;
        }
        this.mRequestSent = false;
        this.PostJobChange();
      }
    }

    private void PostJobChange()
    {
      this.mRequestSent = false;
      if (this.mTargetUnit == null)
        return;
      MonoSingleton<GameManager>.Instance.Player.OnJobChange(this.mTargetUnit.UnitID);
      this.mOriginalJobID = this.mTargetUnit.CurrentJob.JobID;
    }

    private void OnApplicationPause(bool pausing)
    {
      if (!pausing || this.mTargetUnit == null || !(this.mTargetUnit.CurrentJob.JobID != this.mOriginalJobID))
        return;
      this.RequestJobChange(true);
    }

    private void OnApplicationFocus(bool focus)
    {
      if (focus)
        return;
      this.OnApplicationPause(true);
    }

    private void JobChanged(int value)
    {
      if (this.mTargetUnit == null || value == this.mTargetUnit.JobIndex)
        return;
      this.mTargetUnit.SetJobIndex(value);
      if (!Object.op_Inequality((Object) this.GameParamterRoot, (Object) null))
        return;
      GameParameter.UpdateAll(this.GameParamterRoot);
    }

    public void Refresh()
    {
      this.ClearItems();
      if (this.mTargetUnit == null)
        this.mTargetUnit = DataSource.FindDataOfClass<UnitData>(((Component) this).get_gameObject(), (UnitData) null);
      if (this.mTargetUnit == null)
        return;
      this.mOriginalJobID = this.mTargetUnit.CurrentJob.JobID;
      for (int index = 0; index < this.mTargetUnit.Jobs.Length; ++index)
      {
        if (this.mTargetUnit.Jobs[index].IsActivated)
        {
          UnitJobDropdown.JobPulldownItem jobPulldownItem = this.AddItem(this.mTargetUnit.Jobs[index].Name, index) as UnitJobDropdown.JobPulldownItem;
          if (Object.op_Inequality((Object) jobPulldownItem.JobIcon, (Object) null))
          {
            string name = AssetPath.JobIconSmall(this.mTargetUnit.Jobs[index].Param);
            if (!string.IsNullOrEmpty(name))
              jobPulldownItem.JobIcon.set_texture((Texture) AssetManager.Load<Texture2D>(name));
          }
          if (index == this.mTargetUnit.JobIndex)
            this.Selection = this.ItemCount - 1;
        }
      }
    }

    protected override Pulldown.PulldownItem SetupPulldownItem(GameObject itemObject)
    {
      UnitJobDropdown.JobPulldownItem jobPulldownItem = (UnitJobDropdown.JobPulldownItem) itemObject.AddComponent<UnitJobDropdown.JobPulldownItem>();
      jobPulldownItem.JobIcon = this.ItemJobIcon;
      return (Pulldown.PulldownItem) jobPulldownItem;
    }

    protected override void UpdateSelection()
    {
      if (!Object.op_Inequality((Object) this.JobIcon, (Object) null))
        return;
      UnitJobDropdown.JobPulldownItem itemAt = this.GetItemAt(this.Selection) as UnitJobDropdown.JobPulldownItem;
      if (!Object.op_Inequality((Object) itemAt, (Object) null) || !Object.op_Inequality((Object) itemAt.JobIcon, (Object) null))
        return;
      this.JobIcon.set_texture(itemAt.JobIcon.get_texture());
    }

    public class JobPulldownItem : Pulldown.PulldownItem
    {
      public string JobID;
      public RawImage JobIcon;
    }

    public delegate void JobChangeEvent(long unitUniqueID);
  }
}
