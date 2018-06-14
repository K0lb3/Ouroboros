// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqChatMessageRoom
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  [FlowNode.NodeType("System/ReqChatLogRoom", 32741)]
  public class FlowNode_ReqChatMessageRoom : FlowNode_ReqChatMessage
  {
    private string mRoomToken;
    private long mStartID;
    private int mLimit;
    private long mExcludeID;
    private bool mIsSystemMessageMerge;

    private void ResetParam()
    {
      this.mRoomToken = (string) null;
      this.mStartID = 0L;
      this.mLimit = 0;
      this.mExcludeID = 0L;
      this.mIsSystemMessageMerge = false;
      this.mSetup = false;
    }

    public override void SetChatMessageInfo(string room_token, long start_id, int limit, long exclude_id, bool is_sys_msg_merge)
    {
      this.ResetParam();
      this.mRoomToken = room_token;
      this.mStartID = start_id;
      this.mLimit = limit;
      this.mExcludeID = exclude_id;
      this.mIsSystemMessageMerge = is_sys_msg_merge;
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
      this.ExecRequest((WebAPI) new ReqChatMessageRoom(this.mStartID, this.mRoomToken, this.mLimit, this.mExcludeID, isMultiPush, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
    }

    protected override void Success(ChatLog log)
    {
      ChatWindow component = (ChatWindow) ((Component) this).get_gameObject().GetComponent<ChatWindow>();
      if (Object.op_Inequality((Object) component, (Object) null))
      {
        if (this.mIsSystemMessageMerge)
          component.SetChatLogAndSystemMessageMerge(log, this.mExcludeID);
        else
          component.SetChatLog(log, ChatWindow.eChatType.Room);
      }
      base.Success(log);
    }
  }
}
