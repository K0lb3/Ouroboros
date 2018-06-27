// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqChanelList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  [FlowNode.NodeType("System/ReqChatChannelList", 32741)]
  [FlowNode.Pin(10, "Success", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(1, "Request(SelectPageIndex)", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(0, "Request", FlowNode.PinTypes.Input, 0)]
  public class FlowNode_ReqChanelList : FlowNode_Network
  {
    public int ChannelLimit = 20;

    public override void OnActivate(int pinID)
    {
      if (pinID < 0)
        return;
      ChatChannelMasterParam[] chatChannelMaster = MonoSingleton<GameManager>.Instance.GetChatChannelMaster();
      List<int> intList = new List<int>();
      int currentChatChannel = (int) GlobalVars.CurrentChatChannel;
      int num1;
      if (pinID == 0)
      {
        num1 = currentChatChannel % this.ChannelLimit != 0 ? currentChatChannel / this.ChannelLimit : (currentChatChannel - 1) / this.ChannelLimit;
        FlowNode_Variable.Set("SelectChannelPage", num1.ToString());
      }
      else
        num1 = int.Parse(FlowNode_Variable.Get("SelectChannelPage"));
      int num2 = this.ChannelLimit * num1;
      int num3 = num2 <= chatChannelMaster.Length ? num2 : 0;
      int num4 = this.ChannelLimit * (num1 + 1);
      int num5 = num4 <= chatChannelMaster.Length ? num4 : chatChannelMaster.Length;
      for (int index = num3; index < num5; ++index)
      {
        if (index < chatChannelMaster.Length)
          intList.Add(chatChannelMaster[index].id);
      }
      this.ExecRequest((WebAPI) new ReqChatChannelList(intList.ToArray(), new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
      ((Behaviour) this).set_enabled(true);
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
        Network.EErrCode errCode = Network.ErrCode;
      }
      else
      {
        WebAPI.JSON_BodyResponse<JSON_ChatChannel> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<JSON_ChatChannel>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        Network.RemoveAPI();
        ChatChannel chatChannel = new ChatChannel();
        chatChannel.Deserialize(jsonObject.body);
        if (chatChannel == null)
          return;
        ChatChannelWindow component = (ChatChannelWindow) ((Component) this).get_gameObject().GetComponent<ChatChannelWindow>();
        if (Object.op_Inequality((Object) component, (Object) null))
          component.Channel = chatChannel;
        this.Success();
      }
    }
  }
}
