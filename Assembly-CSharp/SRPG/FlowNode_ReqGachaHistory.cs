// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqGachaHistory
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(0, "Request", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Success", FlowNode.PinTypes.Output, 1)]
  [FlowNode.NodeType("Network/GachaHistory", 32741)]
  public class FlowNode_ReqGachaHistory : FlowNode_Network
  {
    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      this.ExecRequest((WebAPI) new ReqGachaHistory(new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
      ((Behaviour) this).set_enabled(true);
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
        Network.EErrCode errCode = Network.ErrCode;
      }
      else
      {
        WebAPI.JSON_BodyResponse<Json_GachaHistory> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_GachaHistory>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        Network.RemoveAPI();
        GachaHistoryWindow component = (GachaHistoryWindow) ((Component) this).GetComponent<GachaHistoryWindow>();
        if (Object.op_Inequality((Object) component, (Object) null))
          component.SetGachaHistoryData(jsonObject.body.log);
        this.Success();
      }
    }
  }
}
