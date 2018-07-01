// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqChatMessage
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(0, "Request", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(10, "Success", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(100, "ChatFailure", FlowNode.PinTypes.Output, 100)]
  [FlowNode.NodeType("System/ReqChatLog", 32741)]
  public class FlowNode_ReqChatMessage : FlowNode_Network
  {
    protected bool mSetup;

    public virtual void SetChatMessageInfo(int channel, long start_id, int limit, long exclude_id)
    {
    }

    public virtual void SetChatMessageInfo(string room_token, long start_id, int limit, long exclude_id, bool is_sys_msg_merge)
    {
    }

    public override void OnActivate(int pinID)
    {
    }

    protected virtual void Success(ChatLog log)
    {
      ((Behaviour) this).set_enabled(false);
      this.mSetup = false;
      this.ActivateOutputLinks(10);
    }

    public override void OnSuccess(WWWResult www)
    {
      if (Object.op_Equality((Object) this, (Object) null))
      {
        Network.RemoveAPI();
        Network.IsIndicator = true;
      }
      else if (Network.IsError)
      {
        Network.EErrCode errCode = Network.ErrCode;
        Network.RemoveAPI();
        Network.IsIndicator = true;
        ((Behaviour) this).set_enabled(false);
        this.mSetup = false;
        this.ActivateOutputLinks(100);
      }
      else
      {
        DebugMenu.Log("API", "chat:message:{" + www.text + "}");
        WebAPI.JSON_BodyResponse<JSON_ChatLog> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<JSON_ChatLog>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        Network.RemoveAPI();
        Network.IsIndicator = true;
        ChatLog log = new ChatLog();
        if (jsonObject.body != null)
        {
          log.Deserialize(jsonObject.body);
          MultiInvitationReceiveWindow.SetBadge(jsonObject.body.player != null && jsonObject.body.player.multi_inv != 0);
        }
        else
          MultiInvitationReceiveWindow.SetBadge(false);
        this.Success(log);
      }
    }
  }
}
