// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_SetAbility
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(0, "Request", FlowNode.PinTypes.Input, 0)]
  [FlowNode.NodeType("System/SetAbility", 32741)]
  [FlowNode.Pin(1, "Success", FlowNode.PinTypes.Output, 1)]
  public class FlowNode_SetAbility : FlowNode_Network
  {
    private Queue<long> mJobs = new Queue<long>();
    private Queue<long> mAbilities = new Queue<long>();
    private long mUnitUniqueID;

    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      if (Network.Mode == Network.EConnectMode.Online)
      {
        this.mUnitUniqueID = (long) GlobalVars.SelectedUnitUniqueID;
        UnitData unitDataByUniqueId = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID(this.mUnitUniqueID);
        for (int index1 = 0; index1 < unitDataByUniqueId.Jobs.Length; ++index1)
        {
          this.mJobs.Enqueue(unitDataByUniqueId.Jobs[index1].UniqueID);
          for (int index2 = 0; index2 < unitDataByUniqueId.Jobs[index1].AbilitySlots.Length; ++index2)
            this.mAbilities.Enqueue(unitDataByUniqueId.Jobs[index1].AbilitySlots[index2]);
        }
        this.UpdateAbilities();
        ((Behaviour) this).set_enabled(true);
      }
      else
        this.Success();
    }

    private void UpdateAbilities()
    {
      long iid_job = this.mJobs.Dequeue();
      long[] iid_abils = new long[5];
      for (int index = 0; index < 5; ++index)
        iid_abils[index] = this.mAbilities.Dequeue();
      this.ExecRequest((WebAPI) new ReqJobAbility(iid_job, iid_abils, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
    }

    private void Success()
    {
      ((Behaviour) this).set_enabled(false);
      this.ActivateOutputLinks(1);
    }

    public override void OnSuccess(WWWResult www)
    {
      if (Network.IsError)
      {
        switch (Network.ErrCode)
        {
          case Network.EErrCode.NoAbilitySetAbility:
          case Network.EErrCode.NoJobSetAbility:
          case Network.EErrCode.UnsetAbility:
            this.OnFailed();
            break;
          default:
            this.OnRetry();
            break;
        }
      }
      else
      {
        WebAPI.JSON_BodyResponse<Json_PlayerDataAll> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_PlayerDataAll>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        try
        {
          if (jsonObject.body == null)
            throw new InvalidJSONException();
          MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.player);
          MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.units);
        }
        catch (Exception ex)
        {
          this.OnRetry(ex);
          return;
        }
        Network.RemoveAPI();
        if (this.mJobs.Count < 1)
          this.Success();
        else
          this.UpdateAbilities();
      }
    }
  }
}
