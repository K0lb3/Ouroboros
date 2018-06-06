// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqChatMessage
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(0, "Request", FlowNode.PinTypes.Input, 0)]
  [FlowNode.NodeType("System/ReqChatLog", 32741)]
  [FlowNode.Pin(100, "ChatFailure", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(10, "Success", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(1, "Request(Admin)", FlowNode.PinTypes.Input, 1)]
  public class FlowNode_ReqChatMessage : FlowNode_Network
  {
    private bool mSetup;
    private int mPinID;
    private int mChannel;
    private int mStartID;
    private int mLimit;
    private int mExcludeID;

    public void SetChatMessageinfo(int channel, int start_id, int limit, int exclude_id)
    {
      this.mChannel = channel;
      this.mStartID = start_id;
      this.mLimit = limit;
      this.mExcludeID = exclude_id;
      this.mSetup = true;
    }

    public override void OnActivate(int pinID)
    {
      switch (pinID)
      {
        case 0:
          if (this.mSetup)
          {
            int mStartId = this.mStartID;
            int mChannel = this.mChannel;
            int mLimit = this.mLimit;
            int mExcludeId = this.mExcludeID;
            Network.IsIndicator = false;
            this.ExecRequest((WebAPI) new ReqChatMessage(mStartId, mChannel, mLimit, mExcludeId, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
          }
          ((Behaviour) this).set_enabled(true);
          break;
        case 1:
          Network.IsIndicator = false;
          this.ExecRequest((WebAPI) new ReqChatMessageOffical(new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
          ((Behaviour) this).set_enabled(true);
          break;
      }
      this.mPinID = pinID;
    }

    private void Success()
    {
      ((Behaviour) this).set_enabled(false);
      this.mSetup = false;
      this.ActivateOutputLinks(10);
    }

    public override void OnSuccess(WWWResult www)
    {
      if (Network.IsError)
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
        WebAPI.JSON_BodyResponse<JSON_ChatLog> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<JSON_ChatLog>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        Network.RemoveAPI();
        Network.IsIndicator = true;
        ChatLog chatLog = new ChatLog();
        chatLog.Deserialize(jsonObject.body);
        if (chatLog == null || !Object.op_Inequality((Object) this, (Object) null))
          return;
        ChatWindow component = (ChatWindow) ((Component) this).get_gameObject().GetComponent<ChatWindow>();
        if (Object.op_Inequality((Object) component, (Object) null))
        {
          if (this.mPinID == 0)
            component.ChatLog = chatLog;
          else if (this.mPinID == 1)
            component.ChatLogOffical = chatLog;
        }
        this.Success();
      }
    }
  }
}
