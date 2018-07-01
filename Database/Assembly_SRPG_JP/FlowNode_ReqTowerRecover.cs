// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqTowerRecover
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(1, "Success", FlowNode.PinTypes.Output, 10)]
  [FlowNode.NodeType("System/ReqTowerRecover", 32741)]
  [FlowNode.Pin(0, "Request", FlowNode.PinTypes.Input, 0)]
  public class FlowNode_ReqTowerRecover : FlowNode_Network
  {
    private GameObject mFlowRoot;
    private int usedCoin;

    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      TowerRecoverData dataOfClass = DataSource.FindDataOfClass<TowerRecoverData>(((Component) this).get_gameObject(), (TowerRecoverData) null);
      TowerResuponse towerResuponse = MonoSingleton<GameManager>.Instance.TowerResuponse;
      if (Network.Mode != Network.EConnectMode.Online)
        return;
      if (dataOfClass != null)
      {
        byte floor = towerResuponse.GetCurrentFloor().floor;
        this.usedCoin = dataOfClass.useCoin;
        this.ExecRequest((WebAPI) new ReqTowerRecover(dataOfClass.towerID, dataOfClass.useCoin, (int) towerResuponse.round, floor, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
        ((Behaviour) this).set_enabled(true);
      }
      else
        ((Behaviour) this).set_enabled(false);
    }

    private void Success()
    {
      ((Behaviour) this).set_enabled(false);
      UIUtility.SystemMessage(LocalizedText.Get("sys.CAPTION_TOWER_RECOVERED"), LocalizedText.Get("sys.MSG_TOWER_RECOVERED", new object[1]
      {
        (object) this.usedCoin.ToString()
      }), (UIUtility.DialogResultEvent) (go => this.ActivateOutputLinks(1)), (GameObject) null, false, -1);
    }

    public override void OnSuccess(WWWResult www)
    {
      if (TowerErrorHandle.Error((FlowNode_Network) this))
        return;
      WebAPI.JSON_BodyResponse<FlowNode_ReqTowerRecover.JSON_ReqTowerRecoverResponse> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<FlowNode_ReqTowerRecover.JSON_ReqTowerRecoverResponse>>(www.text);
      DebugUtility.Assert(jsonObject != null, "res == null");
      Network.RemoveAPI();
      GameManager instance = MonoSingleton<GameManager>.Instance;
      try
      {
        instance.Deserialize(jsonObject.body.player);
        instance.TowerResuponse.Deserialize(jsonObject.body.pdeck);
        instance.TowerResuponse.rtime = (long) jsonObject.body.rtime;
        instance.TowerResuponse.recover_num = jsonObject.body.rcv_num;
      }
      catch (Exception ex)
      {
        DebugUtility.LogException(ex);
        return;
      }
      this.Success();
    }

    private class JSON_ReqTowerRecoverResponse
    {
      public Json_PlayerData player;
      public int rtime;
      public int rcv_num;
      public JSON_ReqTowerResuponse.Json_TowerPlayerUnit[] pdeck;
    }
  }
}
