// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_MultiPlayEnterLobby
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(3, "EnterLobby(autoJoinlobby)", FlowNode.PinTypes.Input, 3)]
  [FlowNode.Pin(2, "Failure", FlowNode.PinTypes.Output, 2)]
  [FlowNode.Pin(1, "Success", FlowNode.PinTypes.Output, 1)]
  [FlowNode.Pin(0, "EnterLobby", FlowNode.PinTypes.Input, 0)]
  [FlowNode.NodeType("Multi/MultiPlayEnterLobby", 32741)]
  public class FlowNode_MultiPlayEnterLobby : FlowNode
  {
    public float TimeOutSec = 10f;
    public bool FlushRoomMsg = true;
    public bool DisconnectIfSendFailed = true;
    public bool SortRoomMsg = true;
    private StateMachine<FlowNode_MultiPlayEnterLobby> mStateMachine;

    private bool IsEqual(string s0, string s1)
    {
      if (string.IsNullOrEmpty(s0))
        return string.IsNullOrEmpty(s1);
      return s0.Equals(s1);
    }

    public override void OnActivate(int pinID)
    {
      if (pinID != 0 && pinID != 3)
        return;
      MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
      instance.TimeOutSec = this.TimeOutSec;
      instance.SendRoomMessageFlush = this.FlushRoomMsg;
      instance.DisconnectIfSendRoomMessageFailed = this.DisconnectIfSendFailed;
      instance.SortRoomMessage = this.SortRoomMsg;
      if (instance.CurrentState == MyPhoton.MyState.LOBBY)
      {
        DebugUtility.Log("already enter lobby");
        this.Success();
      }
      else
      {
        ((Behaviour) this).set_enabled(true);
        if (instance.CurrentState != MyPhoton.MyState.NOP)
          instance.Disconnect();
        this.mStateMachine = new StateMachine<FlowNode_MultiPlayEnterLobby>(this);
        if (pinID == 0)
          this.mStateMachine.GotoState<FlowNode_MultiPlayEnterLobby.State_ConnectLobby>();
        else
          this.mStateMachine.GotoState<FlowNode_MultiPlayEnterLobby.State_ConnectLobbyAuto>();
      }
    }

    private void Update()
    {
      if (this.mStateMachine == null)
        return;
      this.mStateMachine.Update();
    }

    private void Success()
    {
      DebugUtility.Log("Enter Lobby.");
      ((Behaviour) this).set_enabled(false);
      this.ActivateOutputLinks(1);
    }

    private void Failure()
    {
      DebugUtility.Log("Enter Lobby Failure.");
      ((Behaviour) this).set_enabled(false);
      MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
      if (instance.CurrentState != MyPhoton.MyState.NOP)
        instance.Disconnect();
      this.ActivateOutputLinks(2);
    }

    public void GotoState<StateType>() where StateType : State<FlowNode_MultiPlayEnterLobby>, new()
    {
      if (this.mStateMachine == null)
        return;
      this.mStateMachine.GotoState<StateType>();
    }

    private class State_ConnectLobby : State<FlowNode_MultiPlayEnterLobby>
    {
      protected readonly int MAX_RETRY_CNT = 1;
      protected int ReqCnt;

      public override void Begin(FlowNode_MultiPlayEnterLobby self)
      {
        if (this.ReqConnect(self, false))
          return;
        self.Failure();
      }

      public override void Update(FlowNode_MultiPlayEnterLobby self)
      {
        MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
        if (!((Behaviour) self).get_enabled())
          return;
        switch (instance.CurrentState)
        {
          case MyPhoton.MyState.CONNECTING:
            break;
          case MyPhoton.MyState.LOBBY:
            self.Success();
            break;
          default:
            if (!instance.IsDisconnected() || this.ReqConnect(self, false))
              break;
            self.Failure();
            break;
        }
      }

      public override void End(FlowNode_MultiPlayEnterLobby self)
      {
      }

      public bool ReqConnect(FlowNode_MultiPlayEnterLobby self, bool autoJoin = false)
      {
        if (this.ReqCnt++ > this.MAX_RETRY_CNT)
          return false;
        MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
        DebugUtility.Log("start connect:" + GlobalVars.SelectedMultiPlayPhotonAppID);
        instance.ResetLastError();
        return !instance.IsDisconnected() || instance.StartConnect(GlobalVars.SelectedMultiPlayPhotonAppID, autoJoin, "1.0");
      }
    }

    private class State_ConnectLobbyAuto : FlowNode_MultiPlayEnterLobby.State_ConnectLobby
    {
      public override void Begin(FlowNode_MultiPlayEnterLobby self)
      {
        if (this.ReqConnect(self, true))
          return;
        self.Failure();
      }

      public override void Update(FlowNode_MultiPlayEnterLobby self)
      {
        MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
        if (!((Behaviour) self).get_enabled())
          return;
        switch (instance.CurrentState)
        {
          case MyPhoton.MyState.CONNECTING:
            break;
          case MyPhoton.MyState.LOBBY:
            if (!instance.IsRoomListUpdated)
              break;
            self.Success();
            break;
          default:
            if (!instance.IsDisconnected() || this.ReqConnect(self, true))
              break;
            self.Failure();
            break;
        }
      }
    }
  }
}
