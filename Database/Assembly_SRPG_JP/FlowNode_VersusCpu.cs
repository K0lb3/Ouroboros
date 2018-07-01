// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_VersusCpu
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(1, "Success", FlowNode.PinTypes.Output, 1)]
  [FlowNode.Pin(0, "Request", FlowNode.PinTypes.Input, 0)]
  [FlowNode.NodeType("VS/ReqCom", 32741)]
  public class FlowNode_VersusCpu : FlowNode_Network
  {
    public override void OnActivate(int pinID)
    {
      if (pinID != 0 || ((Behaviour) this).get_enabled())
        return;
      if (Network.Mode == Network.EConnectMode.Offline)
      {
        this.Success();
      }
      else
      {
        GameManager instance = MonoSingleton<GameManager>.Instance;
        VersusStatusData versusStatusData = new VersusStatusData();
        int num = 0;
        PartyData party = instance.Player.Partys[7];
        if (party != null)
        {
          for (int index = 0; index < party.MAX_UNIT; ++index)
          {
            long unitUniqueId = party.GetUnitUniqueID(index);
            if (party.GetUnitUniqueID(index) != 0L)
            {
              UnitData unitDataByUniqueId = instance.Player.FindUnitDataByUniqueID(unitUniqueId);
              if (unitDataByUniqueId != null)
              {
                versusStatusData.Add(unitDataByUniqueId.Status.param, unitDataByUniqueId.GetCombination());
                ++num;
              }
            }
          }
        }
        this.ExecRequest((WebAPI) new ReqVersusCpuList(versusStatusData, num, GlobalVars.SelectedQuestID, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
        ((Behaviour) this).set_enabled(true);
      }
    }

    private void Success()
    {
      this.ActivateOutputLinks(1);
    }

    public override void OnSuccess(WWWResult www)
    {
      if (Network.IsError)
      {
        Network.EErrCode errCode = Network.ErrCode;
        this.OnRetry();
      }
      else
      {
        Debug.Log((object) www.text);
        WebAPI.JSON_BodyResponse<Json_VersusCpu> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_VersusCpu>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        if (jsonObject.body == null)
        {
          this.OnRetry();
        }
        else
        {
          ((Behaviour) this).set_enabled(false);
          if (!MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body))
          {
            this.OnFailed();
          }
          else
          {
            Network.RemoveAPI();
            this.Success();
          }
        }
      }
    }
  }
}
