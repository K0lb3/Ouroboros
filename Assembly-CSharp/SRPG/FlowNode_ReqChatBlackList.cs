// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqChatBlackList
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(2, "Maintenance", FlowNode.PinTypes.Output, 2)]
  [FlowNode.NodeType("System/ReqChatBlackList", 32741)]
  [FlowNode.Pin(0, "Request", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Success", FlowNode.PinTypes.Output, 1)]
  public class FlowNode_ReqChatBlackList : FlowNode_Network
  {
    public int GetLimit = 10;

    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      string s = FlowNode_Variable.Get("BLACKLIST_OFFSET");
      this.ExecRequest((WebAPI) new ReqChatBlackList(!string.IsNullOrEmpty(s) ? int.Parse(s) : 1, this.GetLimit, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
      ((Behaviour) this).set_enabled(false);
    }

    private void Success()
    {
      ((Behaviour) this).set_enabled(false);
      this.ActivateOutputLinks(1);
    }

    private void ChatMaintenance()
    {
      ((Behaviour) this).set_enabled(false);
      BlackList component = (BlackList) ((Component) this).get_gameObject().GetComponent<BlackList>();
      if (Object.op_Inequality((Object) component, (Object) null))
        component.RefreshMaintenanceMessage(Network.ErrMsg);
      Network.RemoveAPI();
      Network.ResetError();
      this.ActivateOutputLinks(2);
    }

    public override void OnSuccess(WWWResult www)
    {
      if (Network.IsError)
      {
        if (Network.ErrCode != Network.EErrCode.ChatMaintenance)
          return;
        this.ChatMaintenance();
      }
      else
      {
        WebAPI.JSON_BodyResponse<JSON_ChatBlackList> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<JSON_ChatBlackList>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        Network.RemoveAPI();
        ChatBlackList chatBlackList = new ChatBlackList();
        chatBlackList.Deserialize(jsonObject.body);
        if (chatBlackList == null)
          return;
        BlackList componentInChildren = (BlackList) ((Component) this).GetComponentInChildren<BlackList>();
        if (Object.op_Inequality((Object) componentInChildren, (Object) null))
          componentInChildren.BList = chatBlackList;
        this.Success();
      }
    }
  }
}
