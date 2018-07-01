// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqChatBlackList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(2, "Maintenance", FlowNode.PinTypes.Output, 2)]
  [FlowNode.NodeType("Request/ReqBlackList(ブロックリスト取得)", 32741)]
  [FlowNode.Pin(0, "Request", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Success", FlowNode.PinTypes.Output, 1)]
  public class FlowNode_ReqChatBlackList : FlowNode_Network
  {
    public int GetLimit = 10;
    public bool IsGetOnly;

    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      string s = FlowNode_Variable.Get("BLACKLIST_OFFSET");
      this.ExecRequest((WebAPI) new ReqChatBlackList(!string.IsNullOrEmpty(s) ? int.Parse(s) : 1, this.GetLimit, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
      if (Object.op_Equality((Object) this, (Object) null))
        return;
      ((Behaviour) this).set_enabled(false);
    }

    private void Success()
    {
      if (Object.op_Equality((Object) this, (Object) null))
        return;
      ((Behaviour) this).set_enabled(false);
      this.ActivateOutputLinks(1);
    }

    private void ChatMaintenance()
    {
      if (Object.op_Equality((Object) this, (Object) null))
        return;
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
        GlobalVars.BlockList.Clear();
        if (this.IsGetOnly)
        {
          foreach (ChatBlackListParam list in chatBlackList.lists)
            GlobalVars.BlockList.Add(list.uid);
        }
        else
        {
          BlackList component = (BlackList) ((Component) this).get_gameObject().GetComponent<BlackList>();
          if (Object.op_Inequality((Object) component, (Object) null))
            component.BList = chatBlackList;
        }
        this.Success();
      }
    }
  }
}
