namespace SRPG
{
    using GR;
    using System;
    using UnityEngine;

    [Pin(0x2712, "通信エラー", 1, 0x2712), Pin(0x2714, "バージョン不一致", 1, 0x2714), Pin(0x2715, "セッションID無効", 1, 0x2715), Pin(0x2716, "API呼び出しパラメータ不正", 1, 0x2716), Pin(0x2711, "タイムアウト", 1, 0x2711), Pin(0x2717, "リトライ", 1, 0x2717), Pin(0x2718, "API呼び出し前の状態に戻る", 1, 0x2718), Pin(0x2713, "メンテナンス中", 1, 0x2713)]
    public abstract class FlowNode_Network : FlowNode
    {
        public const string RetryWindowPrefabPath = "e/UI/NetworkRetryWindow";
        private StateMachine<FlowNode_Network> mStateMachine;

        protected FlowNode_Network()
        {
            base..ctor();
            return;
        }

        public static void Back()
        {
            string str;
            Network.RequestResult = 3;
            if (Network.IsImmediateMode == null)
            {
                goto Label_0011;
            }
            return;
        Label_0011:
            Network.RemoveAPI();
            GlobalEvent.Invoke(((PredefinedGlobalEvents) 4).ToString(), null);
            Network.ResetError();
            return;
        }

        public static void CloseWebView()
        {
            GlobalEvent.Invoke("WEBVIEW_DELETE", (int) 1);
            return;
        }

        public static void ErrorAppQuit()
        {
            string str;
            Network.RequestResult = 7;
            Network.RemoveAPI();
            Network.ResetError();
            GlobalEvent.Invoke(((PredefinedGlobalEvents) 7).ToString(), null);
            return;
        }

        public void ExecRequest(WebAPI api)
        {
            Network.RequestAPI(api, 0);
            this.mStateMachine = new StateMachine<FlowNode_Network>(this);
            this.mStateMachine.GotoState<State_WaitForConnect>();
            return;
        }

        public static void Failed()
        {
            string str;
            Network.RequestResult = 4;
            if (Network.IsImmediateMode == null)
            {
                goto Label_0011;
            }
            return;
        Label_0011:
            Network.RemoveAPI();
            Network.ResetError();
            GlobalEvent.Invoke(((PredefinedGlobalEvents) 3).ToString(), null);
            return;
        }

        public static unsafe bool HasCommonError(WWWResult www)
        {
            object[] objArray1;
            Network.EErrCode code;
            if (Network.IsError != null)
            {
                goto Label_000C;
            }
            return 0;
        Label_000C:
            if (Network.ErrCode == -1)
            {
                goto Label_003E;
            }
            if (Network.ErrCode == -2)
            {
                goto Label_003E;
            }
            DebugUtility.LogError("NetworkError: " + &www.text);
            goto Label_0073;
        Label_003E:
            objArray1 = new object[] { "NetworkError: ", (Network.EErrCode) Network.ErrCode, " : ", Network.ErrMsg };
            DebugUtility.LogError(string.Concat(objArray1));
        Label_0073:
            SRPG_InputField.ResetInput();
            code = Network.ErrCode;
            switch ((code + 2))
            {
                case 0:
                    goto Label_00E6;

                case 1:
                    goto Label_00F0;

                case 2:
                    goto Label_00A2;

                case 3:
                    goto Label_00FA;

                case 4:
                    goto Label_010E;

                case 5:
                    goto Label_0118;

                case 6:
                    goto Label_0140;
            }
        Label_00A2:
            if (code == 0x138a)
            {
                goto Label_0122;
            }
            if (code == 0x138b)
            {
                goto Label_012C;
            }
            if (code == 100)
            {
                goto Label_0122;
            }
            if (code == 200)
            {
                goto Label_0104;
            }
            if (code == 300)
            {
                goto Label_0136;
            }
            if (code == 0x44c)
            {
                goto Label_0140;
            }
            goto Label_0150;
        Label_00E6:
            Retry();
            goto Label_0152;
        Label_00F0:
            Retry();
            goto Label_0152;
        Label_00FA:
            Failed();
            goto Label_0152;
        Label_0104:
            Maintenance();
            goto Label_0152;
        Label_010E:
            Version();
            goto Label_0152;
        Label_0118:
            Failed();
            goto Label_0152;
        Label_0122:
            SessionID();
            goto Label_0152;
        Label_012C:
            Relogin();
            goto Label_0152;
        Label_0136:
            Retry();
            goto Label_0152;
        Label_0140:
            Network.IsNoVersion = 1;
            Version();
            goto Label_0152;
        Label_0150:
            return 0;
        Label_0152:
            return 1;
        }

        public static void IllegalParam()
        {
            Network.RequestResult = 8;
            Network.RemoveAPI();
            Network.ResetError();
            return;
        }

        public static void Maintenance()
        {
            string str;
            Network.RequestResult = 5;
            if (Network.IsImmediateMode == null)
            {
                goto Label_0011;
            }
            return;
        Label_0011:
            Network.RemoveAPI();
            Network.ResetError();
            GlobalEvent.Invoke(((PredefinedGlobalEvents) 5).ToString(), null);
            return;
        }

        public virtual void OnBack()
        {
            Back();
            base.set_enabled(0);
            base.ActivateOutputLinks(this.OnBackPinIndex);
            return;
        }

        public virtual void OnErrorAppQuit()
        {
            SessionID();
            base.set_enabled(0);
            base.ActivateOutputLinks(this.OnSessionIDPinIndex);
            return;
        }

        public virtual void OnFailed()
        {
            Failed();
            base.set_enabled(0);
            base.ActivateOutputLinks(this.OnFailedPinIndex);
            return;
        }

        public virtual void OnIllegalParam()
        {
            base.set_enabled(0);
            base.ActivateOutputLinks(this.OnIllegalParamPinIndex);
            return;
        }

        public virtual void OnMaintenance()
        {
            Maintenance();
            base.set_enabled(0);
            base.ActivateOutputLinks(this.OnMaintenancePinIndex);
            return;
        }

        public virtual void OnRetry()
        {
            Retry();
            base.set_enabled(0);
            base.ActivateOutputLinks(this.OnRetryPinIndex);
            return;
        }

        protected void OnRetry(Exception reason)
        {
            DebugUtility.LogException(reason);
            this.OnRetry();
            return;
        }

        public virtual void OnSessionID()
        {
            SessionID();
            base.set_enabled(0);
            base.ActivateOutputLinks(this.OnSessionIDPinIndex);
            return;
        }

        public abstract void OnSuccess(WWWResult www);
        public virtual void OnTimeOut()
        {
            TimeOut();
            base.set_enabled(0);
            base.ActivateOutputLinks(this.OnTimeOutPinIndex);
            return;
        }

        public virtual void OnVersion()
        {
            Version();
            base.set_enabled(0);
            base.ActivateOutputLinks(this.OnVersionPinIndex);
            return;
        }

        public static void Relogin()
        {
            string str;
            MonoSingleton<GameManager>.Instance.IsRelogin = 1;
            Network.RequestResult = 7;
            Network.RemoveAPI();
            Network.ResetError();
            GlobalEvent.Invoke(((PredefinedGlobalEvents) 3).ToString(), null);
            return;
        }

        public void ResponseCallback(WWWResult www)
        {
            if (HasCommonError(www) != null)
            {
                goto Label_0012;
            }
            this.OnSuccess(www);
        Label_0012:
            return;
        }

        public static void Retry()
        {
            NetworkRetryWindow window;
            Network.RequestResult = 2;
            if (Network.IsImmediateMode == null)
            {
                goto Label_0011;
            }
            return;
        Label_0011:
            CloseWebView();
            window = Object.Instantiate<NetworkRetryWindow>(Resources.Load<NetworkRetryWindow>("e/UI/NetworkRetryWindow"));
            window.Delegate = new NetworkRetryWindow.RetryWindowEvent(FlowNode_Network.RetryEvent);
            window.Body = Network.ErrMsg;
            return;
        }

        private static void RetryEvent(bool retry)
        {
            if (retry == null)
            {
                goto Label_0015;
            }
            Network.ResetError();
            Network.SetRetry();
            goto Label_0024;
        Label_0015:
            Network.RemoveAPI();
            Network.ResetError();
            FlowNode_LoadScene.LoadBootScene();
        Label_0024:
            return;
        }

        public static void SessionID()
        {
            string str;
            Network.RequestResult = 7;
            Network.RemoveAPI();
            Network.ResetError();
            GlobalEvent.Invoke(((PredefinedGlobalEvents) 3).ToString(), null);
            return;
        }

        public static void TimeOut()
        {
            Network.RequestResult = 4;
            if (Network.IsImmediateMode == null)
            {
                goto Label_0011;
            }
            return;
        Label_0011:
            Network.RemoveAPI();
            Network.ResetError();
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

        public static void Version()
        {
            string str;
            Network.RequestResult = 6;
            if (Network.IsImmediateMode == null)
            {
                goto Label_0011;
            }
            return;
        Label_0011:
            Network.RemoveAPI();
            Network.ResetError();
            GlobalEvent.Invoke(((PredefinedGlobalEvents) 8).ToString(), null);
            return;
        }

        public int OnTimeOutPinIndex
        {
            get
            {
                return 0x2711;
            }
        }

        public int OnFailedPinIndex
        {
            get
            {
                return 0x2712;
            }
        }

        public int OnMaintenancePinIndex
        {
            get
            {
                return 0x2713;
            }
        }

        public int OnVersionPinIndex
        {
            get
            {
                return 0x2714;
            }
        }

        public int OnSessionIDPinIndex
        {
            get
            {
                return 0x2715;
            }
        }

        public int OnIllegalParamPinIndex
        {
            get
            {
                return 0x2716;
            }
        }

        public int OnRetryPinIndex
        {
            get
            {
                return 0x2717;
            }
        }

        public int OnBackPinIndex
        {
            get
            {
                return 0x2718;
            }
        }

        private class State_WaitForConnect : State<FlowNode_Network>
        {
            public State_WaitForConnect()
            {
                base..ctor();
                return;
            }

            public override void Update(FlowNode_Network self)
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

