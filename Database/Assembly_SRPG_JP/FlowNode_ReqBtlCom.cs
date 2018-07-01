// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqBtlCom
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(100, "Start", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(2, "Reset to Title", FlowNode.PinTypes.Output, 11)]
  [FlowNode.NodeType("System/ReqBtlCom", 32741)]
  [FlowNode.Pin(1, "Success", FlowNode.PinTypes.Output, 10)]
  public class FlowNode_ReqBtlCom : FlowNode_Network
  {
    public bool FastRefresh;
    public bool GetTowerProgress;

    public override void OnActivate(int pinID)
    {
      if (pinID != 100)
        return;
      if (Network.Mode == Network.EConnectMode.Online)
      {
        this.ExecRequest((WebAPI) new ReqBtlCom(new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback), this.FastRefresh, this.GetTowerProgress));
        ((Behaviour) this).set_enabled(true);
      }
      else
        this.Success();
    }

    private void Success()
    {
      ((Behaviour) this).set_enabled(false);
      this.ActivateOutputLinks(1);
    }

    private void Failure()
    {
      ((Behaviour) this).set_enabled(false);
      this.ActivateOutputLinks(2);
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
        WebAPI.JSON_BodyResponse<FlowNode_ReqBtlCom.JSON_ReqBtlComResponse> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<FlowNode_ReqBtlCom.JSON_ReqBtlComResponse>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        Network.RemoveAPI();
        GameManager instance = MonoSingleton<GameManager>.Instance;
        instance.Player.SetQuestListDirty();
        instance.ResetJigenQuests();
        if (!instance.Deserialize(jsonObject.body.quests))
        {
          this.Failure();
        }
        else
        {
          if (jsonObject.body.towers != null)
          {
            for (int index = 0; index < jsonObject.body.towers.Length; ++index)
            {
              JSON_ReqTowerResuponse.Json_TowerProg tower1 = jsonObject.body.towers[index];
              TowerParam tower2 = instance.FindTower(tower1.iname);
              if (tower2 != null)
                tower2.is_unlock = tower1.is_open == 1;
            }
          }
          this.Success();
        }
      }
    }

    public class JSON_ReqBtlComResponse
    {
      public JSON_QuestProgress[] quests;
      public JSON_ReqTowerResuponse.Json_TowerProg[] towers;
    }
  }
}
