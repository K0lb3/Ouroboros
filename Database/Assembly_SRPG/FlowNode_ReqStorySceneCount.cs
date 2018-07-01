// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqStorySceneCount
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(10, "Success", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(0, "Requesst", FlowNode.PinTypes.Input, 0)]
  [FlowNode.NodeType("System/ReqStorySceneCount", 32741)]
  public class FlowNode_ReqStorySceneCount : FlowNode_Network
  {
    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      ((Behaviour) this).set_enabled(true);
      this.ExecRequest((WebAPI) new ReqStorySceneCount((string) GlobalVars.ReplaySelectedQuestID, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
    }

    private void Success()
    {
      ((Behaviour) this).set_enabled(false);
      this.ActivateOutputLinks(10);
    }

    public override void OnSuccess(WWWResult www)
    {
      if (Network.IsError)
      {
        this.OnRetry();
      }
      else
      {
        Network.RemoveAPI();
        this.Success();
      }
    }
  }
}
