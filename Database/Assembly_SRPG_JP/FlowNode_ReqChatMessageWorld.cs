// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqChatMessageWorld
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  [FlowNode.NodeType("System/ReqChatLogWorld", 32741)]
  public class FlowNode_ReqChatMessageWorld : FlowNode_ReqChatMessage
  {
    private int mChannel;
    private long mStartID;
    private int mLimit;
    private long mExcludeID;

    private void ResetParam()
    {
      this.mChannel = 0;
      this.mStartID = 0L;
      this.mLimit = 0;
      this.mExcludeID = 0L;
      this.mSetup = false;
    }

    public override void SetChatMessageInfo(int channel, long start_id, int limit, long exclude_id)
    {
      this.ResetParam();
      this.mChannel = channel;
      this.mStartID = start_id;
      this.mLimit = limit;
      this.mExcludeID = exclude_id;
      this.mSetup = true;
    }

    public override void OnActivate(int pinID)
    {
      ((Behaviour) this).set_enabled(true);
      if (!this.mSetup || pinID != 0)
        return;
      Network.IsIndicator = false;
      bool isMultiPush = false;
      if (Object.op_Inequality((Object) MonoSingleton<GameManager>.Instance, (Object) null) && MonoSingleton<GameManager>.Instance.Player != null)
        isMultiPush = MonoSingleton<GameManager>.Instance.Player.MultiInvitaionFlag;
      this.ExecRequest((WebAPI) new ReqChatMessage(this.mStartID, this.mChannel, this.mLimit, this.mExcludeID, isMultiPush, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
    }

    protected override void Success(ChatLog log)
    {
      ChatWindow component = (ChatWindow) ((Component) this).get_gameObject().GetComponent<ChatWindow>();
      if (Object.op_Inequality((Object) component, (Object) null))
        component.SetChatLog(log, ChatWindow.eChatType.World);
      base.Success(log);
    }
  }
}
