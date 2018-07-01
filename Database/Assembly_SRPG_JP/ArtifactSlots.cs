// Decompiled with JetBrains decompiler
// Type: SRPG.ArtifactSlots
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(0, "Job Change(True)", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Job Change(False)", FlowNode.PinTypes.Input, 1)]
  public class ArtifactSlots : MonoBehaviour, IFlowInterface
  {
    private static readonly string ArtiSelectPath = "UI/ArtifactEquip";
    public GenericSlot ArtifactSlot;
    public GenericSlot ArtifactSlot2;
    public GenericSlot ArtifactSlot3;
    public GameObject RootObject;
    private UnitData mCurrentUnit;

    public ArtifactSlots()
    {
      base.\u002Ector();
    }

    public void Activated(int pinID)
    {
      if (pinID != 0 && pinID != 1)
        return;
      this.Refresh(pinID == 0);
    }

    private void Start()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ArtifactSlot, (UnityEngine.Object) null))
        this.ArtifactSlot.OnSelect = new GenericSlot.SelectEvent(this.OnClick);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ArtifactSlot2, (UnityEngine.Object) null))
        this.ArtifactSlot2.OnSelect = new GenericSlot.SelectEvent(this.OnClick);
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ArtifactSlot3, (UnityEngine.Object) null))
        return;
      this.ArtifactSlot3.OnSelect = new GenericSlot.SelectEvent(this.OnClick);
    }

    public void Refresh(bool enable)
    {
      UnitData dataOfClass = DataSource.FindDataOfClass<UnitData>(((Component) ((Component) this).get_transform().get_parent()).get_gameObject(), (UnitData) null);
      if (dataOfClass == null)
        return;
      this.mCurrentUnit = dataOfClass;
      ArtifactData[] artifactDataArray = new ArtifactData[3];
      ArtifactData[] artifactDatas;
      if (enable)
      {
        int jobIndex = dataOfClass.JobIndex;
        UnitData unitDataByUniqueId = MonoSingleton<GameManager>.GetInstanceDirect().Player.FindUnitDataByUniqueID(this.mCurrentUnit.UniqueID);
        if (unitDataByUniqueId == null)
          return;
        artifactDatas = unitDataByUniqueId.Jobs[jobIndex].ArtifactDatas;
      }
      else
        artifactDatas = dataOfClass.CurrentJob.ArtifactDatas;
      List<ArtifactData> artifactDataList = new List<ArtifactData>((IEnumerable<ArtifactData>) artifactDatas);
      Dictionary<int, List<ArtifactData>> dictionary = new Dictionary<int, List<ArtifactData>>();
      for (int index = 0; index < artifactDataList.Count; ++index)
      {
        if (artifactDataList[index] != null)
        {
          int type = (int) artifactDataList[index].ArtifactParam.type;
          if (!dictionary.ContainsKey(type))
            dictionary[type] = new List<ArtifactData>();
          dictionary[type].Add(artifactDataList[index]);
        }
      }
      artifactDataList.Clear();
      List<int> intList = new List<int>((IEnumerable<int>) dictionary.Keys);
      intList.Sort((Comparison<int>) ((x, y) => x - y));
      for (int index = 0; index < intList.Count; ++index)
        artifactDataList.AddRange((IEnumerable<ArtifactData>) dictionary[intList[index]]);
      ArtifactData[] array = artifactDataList.ToArray();
      this.RefreshSlots(this.ArtifactSlot, array, 0, dataOfClass.CurrentJob.Rank, this.CheckArtifactSlot(0, dataOfClass.CurrentJob, dataOfClass), enable);
      this.RefreshSlots(this.ArtifactSlot2, array, 1, dataOfClass.CurrentJob.Rank, this.CheckArtifactSlot(1, dataOfClass.CurrentJob, dataOfClass), enable);
      this.RefreshSlots(this.ArtifactSlot3, array, 2, dataOfClass.CurrentJob.Rank, this.CheckArtifactSlot(2, dataOfClass.CurrentJob, dataOfClass), enable);
    }

    private void RefreshSlots(GenericSlot slot, ArtifactData[] list, int index, int job_rank, bool is_locked = false, bool enable = true)
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) slot, (UnityEngine.Object) null))
        return;
      ArtifactData data = (ArtifactData) null;
      if (job_rank > 0 && list.Length > index)
        data = list[index];
      slot.SetLocked(!is_locked);
      slot.SetSlotData<ArtifactData>(data);
      SRPG_Button componentInChildren = (SRPG_Button) ((Component) slot).get_gameObject().GetComponentInChildren<SRPG_Button>();
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) componentInChildren, (UnityEngine.Object) null))
        return;
      ((Behaviour) componentInChildren).set_enabled(enable);
    }

    private bool CheckArtifactSlot(int slot, JobData job, UnitData unit)
    {
      if (job == null || unit == null)
        return false;
      int awakeLv = unit.AwakeLv;
      FixParam fixParam = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.FixParam;
      if (fixParam == null || fixParam.EquipArtifactSlotUnlock == null || (fixParam.EquipArtifactSlotUnlock.Length <= 0 || fixParam.EquipArtifactSlotUnlock.Length < slot))
        return false;
      return (int) fixParam.EquipArtifactSlotUnlock[slot] <= awakeLv;
    }

    private string[] SetEquipArtifactFilters(ArtifactData data, ArtifactTypes type = ArtifactTypes.None)
    {
      List<string> stringList = new List<string>();
      int jobIndex = this.mCurrentUnit.JobIndex;
      UnitData unitDataByUniqueId = MonoSingleton<GameManager>.GetInstanceDirect().Player.FindUnitDataByUniqueID(this.mCurrentUnit.UniqueID);
      if (unitDataByUniqueId == null)
        return stringList.ToArray();
      JobData job = unitDataByUniqueId.Jobs[jobIndex];
      ArtifactData[] artifactDatas = job.ArtifactDatas;
      for (int index = 0; index < RarityParam.MAX_RARITY; ++index)
        stringList.Add("RARE:" + (object) index);
      if (artifactDatas != null)
      {
        for (int index = 0; index < artifactDatas.Length; ++index)
        {
          if (artifactDatas[index] != null && (data == null || (long) artifactDatas[index].UniqueID != (long) data.UniqueID))
            stringList.Add("SAME:" + artifactDatas[index].ArtifactParam.iname);
        }
      }
      for (int index = 0; index < job.ArtifactDatas.Length; ++index)
      {
        ArtifactData artifactData = job.ArtifactDatas[index];
        if (artifactData == null || artifactData.ArtifactParam.type == ArtifactTypes.Accessory)
          stringList.Add("TYPE:" + (object) (index + 1));
        else if (artifactData.ArtifactParam.type == type)
          stringList.Add("TYPE:" + (object) (index + 1));
      }
      return stringList.ToArray();
    }

    private void OnClick(GenericSlot slot, bool interactable)
    {
      if (!interactable)
        return;
      GameObject gameObject1 = AssetManager.Load<GameObject>(ArtifactSlots.ArtiSelectPath);
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) gameObject1, (UnityEngine.Object) null))
        return;
      GameObject gameObject2 = (GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) gameObject1);
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) gameObject2, (UnityEngine.Object) null))
        return;
      ArtifactWindow component = (ArtifactWindow) gameObject2.GetComponent<ArtifactWindow>();
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) component, (UnityEngine.Object) null))
        return;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) slot, (UnityEngine.Object) null))
      {
        if (UnityEngine.Object.op_Equality((UnityEngine.Object) slot, (UnityEngine.Object) this.ArtifactSlot))
          component.SelectArtifactSlot = ArtifactTypes.Arms;
        else if (UnityEngine.Object.op_Equality((UnityEngine.Object) slot, (UnityEngine.Object) this.ArtifactSlot2))
          component.SelectArtifactSlot = ArtifactTypes.Armor;
        else if (UnityEngine.Object.op_Equality((UnityEngine.Object) slot, (UnityEngine.Object) this.ArtifactSlot3))
          component.SelectArtifactSlot = ArtifactTypes.Accessory;
      }
      component.OnEquip = new ArtifactWindow.EquipEvent(this.OnSelect);
      component.SetOwnerUnit(this.mCurrentUnit, this.mCurrentUnit.JobIndex);
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) component.ArtifactList, (UnityEngine.Object) null))
        return;
      if (component.ArtifactList.TestOwner != this.mCurrentUnit)
        component.ArtifactList.TestOwner = this.mCurrentUnit;
      ArtifactData dataOfClass = DataSource.FindDataOfClass<ArtifactData>(((Component) slot).get_gameObject(), (ArtifactData) null);
      long iid = 0;
      if (this.mCurrentUnit.CurrentJob.Artifacts != null)
      {
        long num = (long) (dataOfClass == null ? (OLong) 0L : dataOfClass.UniqueID);
        for (int index = 0; index < this.mCurrentUnit.CurrentJob.Artifacts.Length; ++index)
        {
          if (this.mCurrentUnit.CurrentJob.Artifacts[index] != 0L & this.mCurrentUnit.CurrentJob.Artifacts[index] == num)
          {
            iid = this.mCurrentUnit.CurrentJob.Artifacts[index];
            break;
          }
        }
      }
      ArtifactData artifactData = (ArtifactData) null;
      if (iid != 0L)
        artifactData = MonoSingleton<GameManager>.Instance.Player.FindArtifactByUniqueID(iid);
      if (artifactData != null)
        component.ArtifactList.SetSelection((object[]) new ArtifactData[1]
        {
          artifactData
        }, true, true);
      else
        component.ArtifactList.SetSelection((object[]) new ArtifactData[0], true, true);
      ArtifactTypes type = dataOfClass == null ? ArtifactTypes.None : dataOfClass.ArtifactParam.type;
      component.ArtifactList.FiltersPriority = this.SetEquipArtifactFilters(dataOfClass, type);
    }

    private void OnSelect(ArtifactData artifact, ArtifactTypes type = ArtifactTypes.None)
    {
      if (artifact != null)
      {
        PlayerData player = MonoSingleton<GameManager>.GetInstanceDirect().Player;
        UnitData unit = (UnitData) null;
        JobData job1 = (JobData) null;
        if (player.FindOwner(artifact, out unit, out job1))
        {
          for (int slot = 0; slot < job1.Artifacts.Length; ++slot)
          {
            if (job1.Artifacts[slot] == (long) artifact.UniqueID)
            {
              job1.SetEquipArtifact(slot, (ArtifactData) null);
              break;
            }
          }
          if (this.mCurrentUnit.UniqueID == unit.UniqueID)
          {
            JobData jobData = (JobData) null;
            foreach (JobData job2 in this.mCurrentUnit.Jobs)
            {
              if (job2.UniqueID == job1.UniqueID)
                jobData = job2;
            }
            if (jobData != null)
            {
              for (int slot = 0; slot < jobData.Artifacts.Length; ++slot)
              {
                if (jobData.Artifacts[slot] == (long) artifact.UniqueID)
                {
                  jobData.SetEquipArtifact(slot, (ArtifactData) null);
                  break;
                }
              }
            }
          }
        }
      }
      ArtifactData[] view_artifact_datas = new List<ArtifactData>()
      {
        DataSource.FindDataOfClass<ArtifactData>(((Component) this.ArtifactSlot).get_gameObject(), (ArtifactData) null),
        DataSource.FindDataOfClass<ArtifactData>(((Component) this.ArtifactSlot2).get_gameObject(), (ArtifactData) null),
        DataSource.FindDataOfClass<ArtifactData>(((Component) this.ArtifactSlot3).get_gameObject(), (ArtifactData) null)
      }.ToArray();
      int artifactSlotIndex = JobData.GetArtifactSlotIndex(type);
      List<ArtifactData> artifactDataList1 = new List<ArtifactData>((IEnumerable<ArtifactData>) this.mCurrentUnit.CurrentJob.ArtifactDatas);
      if (artifactDataList1.Count != view_artifact_datas.Length)
        return;
      for (int i = 0; i < view_artifact_datas.Length; ++i)
      {
        if (view_artifact_datas[i] != null && artifactDataList1.Find((Predicate<ArtifactData>) (x =>
        {
          if (x != null)
            return (long) x.UniqueID == (long) view_artifact_datas[i].UniqueID;
          return false;
        })) == null)
          return;
      }
      view_artifact_datas[artifactSlotIndex] = artifact;
      List<ArtifactData> artifactDataList2 = new List<ArtifactData>();
      for (int slot = 0; slot < this.mCurrentUnit.CurrentJob.ArtifactDatas.Length; ++slot)
        this.mCurrentUnit.CurrentJob.SetEquipArtifact(slot, (ArtifactData) null);
      for (int index = 0; index < view_artifact_datas.Length; ++index)
      {
        if (view_artifact_datas[index] != null)
        {
          if (view_artifact_datas[index].ArtifactParam.type == ArtifactTypes.Accessory)
            artifactDataList2.Add(view_artifact_datas[index]);
          else
            this.mCurrentUnit.CurrentJob.SetEquipArtifact(JobData.GetArtifactSlotIndex(view_artifact_datas[index].ArtifactParam.type), view_artifact_datas[index]);
        }
      }
      for (int index = 0; index < artifactDataList2.Count; ++index)
      {
        for (int slot = 0; slot < this.mCurrentUnit.CurrentJob.ArtifactDatas.Length; ++slot)
        {
          if (this.mCurrentUnit.CurrentJob.ArtifactDatas[slot] == null)
          {
            this.mCurrentUnit.CurrentJob.SetEquipArtifact(slot, artifactDataList2[index]);
            break;
          }
        }
      }
      this.mCurrentUnit.UpdateArtifact(this.mCurrentUnit.JobIndex, true, true);
      Network.RequestAPI((WebAPI) new ReqArtifactSet(this.mCurrentUnit.UniqueID, this.mCurrentUnit.CurrentJob.UniqueID, this.mCurrentUnit.CurrentJob.Artifacts, new Network.ResponseCallback(this.OnArtifactSetResult)), false);
    }

    private void OnArtifactSetResult(WWWResult www)
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
        DebugUtility.Assert(jsonObject != null, "res == null");
        if (jsonObject.body == null)
        {
          FlowNode_Network.Retry();
        }
        else
        {
          try
          {
            MonoSingleton<GameManager>.GetInstanceDirect().Deserialize(jsonObject.body.player);
            MonoSingleton<GameManager>.GetInstanceDirect().Deserialize(jsonObject.body.units);
            MonoSingleton<GameManager>.GetInstanceDirect().Deserialize(jsonObject.body.artifacts, false);
            MonoSingleton<GameManager>.GetInstanceDirect().Player.UpdateArtifactOwner();
          }
          catch (Exception ex)
          {
            DebugUtility.LogException(ex);
            FlowNode_Network.Retry();
            return;
          }
          Network.RemoveAPI();
          this.UpdateRootObject();
          this.Refresh(true);
        }
      }
    }

    private void UpdateRootObject()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.RootObject, (UnityEngine.Object) null))
        return;
      GameParameter.UpdateAll(this.RootObject);
    }
  }
}
