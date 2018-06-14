// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqChatUserDetail
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(1, "Success", FlowNode.PinTypes.Output, 1)]
  [FlowNode.Pin(0, "Request", FlowNode.PinTypes.Input, 0)]
  [FlowNode.NodeType("System/ReqChatUserDetail", 32741)]
  public class FlowNode_ReqChatUserDetail : FlowNode_Network
  {
    [SerializeField]
    private ChatPlayerWindow window;
    [SerializeField]
    private FriendDetailWindow detail;

    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      this.ExecRequest((WebAPI) new ReqChatUserProfile(FlowNode_Variable.Get("SelectUserID"), new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
      ((Behaviour) this).set_enabled(false);
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
        this.OnBack();
      }
      else
      {
        WebAPI.JSON_BodyResponse<JSON_ChatPlayerData> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<JSON_ChatPlayerData>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        Network.RemoveAPI();
        ChatPlayerData data = new ChatPlayerData();
        data.Deserialize(jsonObject.body);
        if (Object.op_Inequality((Object) this.window, (Object) null))
          this.window.Player = data;
        if (Object.op_Inequality((Object) this.detail, (Object) null))
          this.detail.SetChatPlayerData(data);
        this.Success();
      }
    }
  }
}
