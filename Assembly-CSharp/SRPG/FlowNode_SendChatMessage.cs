// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_SendChatMessage
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(3, "Interval", FlowNode.PinTypes.Output, 3)]
  [FlowNode.Pin(4, "Request_Stamp", FlowNode.PinTypes.Input, 4)]
  [FlowNode.Pin(1, "Success", FlowNode.PinTypes.Output, 1)]
  [FlowNode.Pin(2, "Failure", FlowNode.PinTypes.Output, 2)]
  [FlowNode.NodeType("System/SendChatMessage", 32741)]
  [FlowNode.Pin(0, "Request", FlowNode.PinTypes.Input, 0)]
  public class FlowNode_SendChatMessage : FlowNode_Network
  {
    private int mStampId = -1;
    private int mChannel;
    private string mMessage;

    public override void OnActivate(int pinID)
    {
      switch (pinID)
      {
        case 0:
          if (string.IsNullOrEmpty(this.mMessage) || this.mChannel < 0)
            break;
          int mChannel1 = this.mChannel;
          string message = WebAPI.EscapeString(this.mMessage);
          Network.IsIndicator = false;
          this.ExecRequest((WebAPI) new ReqSendChatMessage(mChannel1, message, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
          ((Behaviour) this).set_enabled(true);
          break;
        case 4:
          if (this.mStampId < 0)
            break;
          int mChannel2 = this.mChannel;
          int mStampId = this.mStampId;
          Network.IsIndicator = false;
          this.ExecRequest((WebAPI) new ReqSendChatStamp(mChannel2, mStampId, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
          ((Behaviour) this).set_enabled(true);
          break;
      }
    }

    public void SetMessageData(int channle, string message)
    {
      this.mChannel = channle;
      this.mMessage = message;
    }

    public void SetStampData(int channle, int stamp_id)
    {
      this.mChannel = channle;
      this.mStampId = stamp_id;
    }

    private void Success()
    {
      ((Behaviour) this).set_enabled(false);
      this.mChannel = 0;
      this.mMessage = string.Empty;
      this.ActivateOutputLinks(1);
    }

    private void Failure()
    {
      ((Behaviour) this).set_enabled(false);
      this.mChannel = 0;
      this.mMessage = string.Empty;
      this.ActivateOutputLinks(2);
    }

    private void Interval()
    {
      ((Behaviour) this).set_enabled(false);
      this.ActivateOutputLinks(3);
    }

    public override void OnSuccess(WWWResult www)
    {
      if (Network.IsError)
      {
        if (Network.ErrCode == Network.EErrCode.SendChatInterval)
        {
          Network.IsIndicator = true;
          FlowNode_Variable.Set("MESSAGE_CAUTION_SEND_MESSAGE", Network.ErrMsg);
          Network.RemoveAPI();
          Network.ResetError();
          this.Interval();
        }
        else
        {
          Network.IsIndicator = true;
          Network.RemoveAPI();
          Network.ResetError();
        }
      }
      else
      {
        FlowNode_Variable.Set("MESSAGE_CAUTION_SEND_MESSAGE", string.Empty);
        WebAPI.JSON_BodyResponse<JSON_ChatSendRes> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<JSON_ChatSendRes>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        Network.RemoveAPI();
        Network.IsIndicator = true;
        if (jsonObject.body != null)
        {
          ChatSendRes chatSendRes = new ChatSendRes();
          chatSendRes.Deserialize(jsonObject.body);
          if (chatSendRes.IsSuccess)
          {
            this.Success();
            return;
          }
        }
        this.Failure();
      }
    }
  }
}
