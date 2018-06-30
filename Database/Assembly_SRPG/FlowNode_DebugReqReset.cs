namespace SRPG
{
    using GR;
    using System;

    [Pin(2, "Error", 1, 3), NodeType("Debug/サーバのデータを削除", 0x7fe5), Pin(0, "Reset", 0, 0), Pin(1, "Success_Offline", 1, 1), Pin(2, "Success_Online", 1, 2)]
    public class FlowNode_DebugReqReset : FlowNode
    {
        private StateMachine<FlowNode_DebugReqReset> mStateMachine;

        public FlowNode_DebugReqReset()
        {
            base..ctor();
            return;
        }

        private void Failure()
        {
            base.set_enabled(0);
            base.ActivateOutputLinks(3);
            return;
        }

        public override void OnActivate(int pinID)
        {
            if (pinID != null)
            {
                goto Label_0059;
            }
            this.mStateMachine = new StateMachine<FlowNode_DebugReqReset>(this);
            if (Network.Mode != null)
            {
                goto Label_004A;
            }
            Network.RequestAPI(new ReqDebugDataReset(new Network.ResponseCallback(this.ResDebugDataReset)), 0);
            this.mStateMachine.GotoState<State_WaitForConnect>();
            base.set_enabled(1);
            goto Label_0059;
        Label_004A:
            base.set_enabled(0);
            base.ActivateOutputLinks(1);
        Label_0059:
            return;
        }

        public unsafe void ResDebugDataReset(WWWResult www)
        {
            bool flag;
            if (Network.IsError == null)
            {
                goto Label_0011;
            }
            Network.IsRetry = 1;
            return;
        Label_0011:
            flag = GameUtility.Config_UseAssetBundles.Value;
            GameUtility.ClearPreferences();
            GameUtility.Config_UseAssetBundles.Value = flag;
            DebugUtility.Assert((&www.text == null) == 0, "res == null");
            MonoSingleton<GameManager>.Instance.ResetAuth();
            GameUtility.Config_NewGame.Value = 0;
            MetapsAnalyticsScript.ReInitialize();
            Network.RemoveAPI();
            this.Success();
            return;
        }

        private void Success()
        {
            base.set_enabled(0);
            base.ActivateOutputLinks(2);
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

        private class State_WaitForConnect : State<FlowNode_DebugReqReset>
        {
            public State_WaitForConnect()
            {
                base..ctor();
                return;
            }

            public override void Update(FlowNode_DebugReqReset self)
            {
                if (Network.IsConnecting == null)
                {
                    goto Label_000B;
                }
                return;
            Label_000B:
                return;
            }
        }
    }
}

