// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqAddChatBlackList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(10, "CanNotAddBlackList", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(1, "Success", FlowNode.PinTypes.Output, 1)]
  [FlowNode.NodeType("System/ReqAddChatBlackList", 32741)]
  [FlowNode.Pin(0, "Request", FlowNode.PinTypes.Input, 0)]
  public class FlowNode_ReqAddChatBlackList : FlowNode_Network
  {
    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      this.ExecRequest((WebAPI) new ReqAddChatBlackList(FlowNode_Variable.Get("SelectUserID"), new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
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
        if (Network.ErrCode != Network.EErrCode.CanNotAddBlackList)
          return;
        this.OnBack();
      }
      else
      {
        WebAPI.JSON_BodyResponse<JSON_ChatBlackListRes> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<JSON_ChatBlackListRes>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        Network.RemoveAPI();
        if (jsonObject.body.is_success != (byte) 1)
          return;
        this.Success();
      }
    }
  }
}
