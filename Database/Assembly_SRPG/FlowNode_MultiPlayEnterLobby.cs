namespace SRPG
{
    using System;
    using System.Runtime.InteropServices;

    [Pin(1, "Success", 1, 1), Pin(2, "Failure", 1, 2), Pin(3, "EnterLobby(autoJoinlobby)", 0, 3), NodeType("Multi/MultiPlayEnterLobby", 0x7fe5), Pin(0, "EnterLobby", 0, 0)]
    public class FlowNode_MultiPlayEnterLobby : FlowNode
    {
        private StateMachine<FlowNode_MultiPlayEnterLobby> mStateMachine;
        public float TimeOutSec;
        public bool FlushRoomMsg;
        public bool DisconnectIfSendFailed;
        public bool SortRoomMsg;

        public FlowNode_MultiPlayEnterLobby()
        {
            this.TimeOutSec = 10f;
            this.FlushRoomMsg = 1;
            this.DisconnectIfSendFailed = 1;
            this.SortRoomMsg = 1;
            base..ctor();
            return;
        }

        private void Failure()
        {
            MyPhoton photon;
            DebugUtility.Log("Enter Lobby Failure.");
            base.set_enabled(0);
            photon = PunMonoSingleton<MyPhoton>.Instance;
            if (photon.CurrentState == null)
            {
                goto Label_0028;
            }
            photon.Disconnect();
        Label_0028:
            base.ActivateOutputLinks(2);
            return;
        }

        public void GotoState<StateType>() where StateType: State<FlowNode_MultiPlayEnterLobby>, new()
        {
            if (this.mStateMachine == null)
            {
                goto Label_0016;
            }
            this.mStateMachine.GotoState<StateType>();
        Label_0016:
            return;
        }

        private bool IsEqual(string s0, string s1)
        {
            if (string.IsNullOrEmpty(s0) == null)
            {
                goto Label_0012;
            }
            return string.IsNullOrEmpty(s1);
        Label_0012:
            return s0.Equals(s1);
        }

        public override void OnActivate(int pinID)
        {
            MyPhoton photon;
            if (pinID == null)
            {
                goto Label_000D;
            }
            if (pinID != 3)
            {
                goto Label_00A5;
            }
        Label_000D:
            photon = PunMonoSingleton<MyPhoton>.Instance;
            photon.TimeOutSec = this.TimeOutSec;
            photon.SendRoomMessageFlush = this.FlushRoomMsg;
            photon.DisconnectIfSendRoomMessageFailed = this.DisconnectIfSendFailed;
            photon.SortRoomMessage = this.SortRoomMsg;
            if (photon.CurrentState != 2)
            {
                goto Label_0060;
            }
            DebugUtility.Log("already enter lobby");
            this.Success();
            return;
        Label_0060:
            base.set_enabled(1);
            if (photon.CurrentState == null)
            {
                goto Label_0078;
            }
            photon.Disconnect();
        Label_0078:
            this.mStateMachine = new StateMachine<FlowNode_MultiPlayEnterLobby>(this);
            if (pinID != null)
            {
                goto Label_009A;
            }
            this.mStateMachine.GotoState<State_ConnectLobby>();
            goto Label_00A5;
        Label_009A:
            this.mStateMachine.GotoState<State_ConnectLobbyAuto>();
        Label_00A5:
            return;
        }

        private void Success()
        {
            DebugUtility.Log("Enter Lobby.");
            base.set_enabled(0);
            base.ActivateOutputLinks(1);
            return;
        }

        private void Update()
        {
            if (this.mStateMachine == null)
            {
                goto Label_0016;
            }
            this.mStateMachine.Update();
        Label_0016:
            return;
        }

        private class State_ConnectLobby : State<FlowNode_MultiPlayEnterLobby>
        {
            protected readonly int MAX_RETRY_CNT;
            protected int ReqCnt;

            public State_ConnectLobby()
            {
                this.MAX_RETRY_CNT = 1;
                base..ctor();
                return;
            }

            public override void Begin(FlowNode_MultiPlayEnterLobby self)
            {
                if (this.ReqConnect(self, 0) != null)
                {
                    goto Label_0014;
                }
                self.Failure();
                return;
            Label_0014:
                return;
            }

            public override void End(FlowNode_MultiPlayEnterLobby self)
            {
            }

            public bool ReqConnect(FlowNode_MultiPlayEnterLobby self, bool autoJoin)
            {
                MyPhoton photon;
                int num;
                if (this.ReqCnt++ > this.MAX_RETRY_CNT)
                {
                    goto Label_0061;
                }
                photon = PunMonoSingleton<MyPhoton>.Instance;
                DebugUtility.Log("start connect:" + GlobalVars.SelectedMultiPlayPhotonAppID);
                photon.ResetLastError();
                if (photon.IsDisconnected() == null)
                {
                    goto Label_005F;
                }
                if (photon.StartConnect(GlobalVars.SelectedMultiPlayPhotonAppID, autoJoin, "1.0") != null)
                {
                    goto Label_005F;
                }
                return 0;
            Label_005F:
                return 1;
            Label_0061:
                return 0;
            }

            public override void Update(FlowNode_MultiPlayEnterLobby self)
            {
                MyPhoton photon;
                MyPhoton.MyState state;
                photon = PunMonoSingleton<MyPhoton>.Instance;
                if (self.get_enabled() != null)
                {
                    goto Label_0012;
                }
                return;
            Label_0012:
                state = photon.CurrentState;
                if (state != 1)
                {
                    goto Label_0021;
                }
                return;
            Label_0021:
                if (state == 2)
                {
                    goto Label_0047;
                }
                if (photon.IsDisconnected() == null)
                {
                    goto Label_0046;
                }
                if (this.ReqConnect(self, 0) != null)
                {
                    goto Label_0046;
                }
                self.Failure();
            Label_0046:
                return;
            Label_0047:
                self.Success();
                return;
            }
        }

        private class State_ConnectLobbyAuto : FlowNode_MultiPlayEnterLobby.State_ConnectLobby
        {
            public State_ConnectLobbyAuto()
            {
                base..ctor();
                return;
            }

            public override void Begin(FlowNode_MultiPlayEnterLobby self)
            {
                if (base.ReqConnect(self, 1) != null)
                {
                    goto Label_0014;
                }
                self.Failure();
                return;
            Label_0014:
                return;
            }

            public override void Update(FlowNode_MultiPlayEnterLobby self)
            {
                MyPhoton photon;
                MyPhoton.MyState state;
                photon = PunMonoSingleton<MyPhoton>.Instance;
                if (self.get_enabled() != null)
                {
                    goto Label_0012;
                }
                return;
            Label_0012:
                state = photon.CurrentState;
                if (state != 1)
                {
                    goto Label_0021;
                }
                return;
            Label_0021:
                if (state == 2)
                {
                    goto Label_0047;
                }
                if (photon.IsDisconnected() == null)
                {
                    goto Label_0046;
                }
                if (base.ReqConnect(self, 1) != null)
                {
                    goto Label_0046;
                }
                self.Failure();
            Label_0046:
                return;
            Label_0047:
                if (photon.IsRoomListUpdated != null)
                {
                    goto Label_0053;
                }
                return;
            Label_0053:
                self.Success();
                return;
            }
        }
    }
}

