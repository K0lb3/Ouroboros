namespace SRPG
{
    using System;

    [Pin(300, "FAILURE", 1, 300), NodeType("VS/CheckEnableTime", 0x7fe5), Pin(100, "Check", 0, 100), Pin(200, "SUCCESS", 1, 200)]
    public class FlowNode_VersusEnableTime : FlowNode_Network
    {
        private readonly int PIN_CHECK;
        private readonly int PIN_SUCCESS;
        private readonly int PIN_FAILURE;

        public FlowNode_VersusEnableTime()
        {
            this.PIN_CHECK = 100;
            this.PIN_SUCCESS = 200;
            this.PIN_FAILURE = 300;
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            if (pinID != this.PIN_CHECK)
            {
                goto Label_0023;
            }
            base.ExecRequest(new ReqVersusFreematchStatus(new Network.ResponseCallback(this.ResponseCallback)));
        Label_0023:
            return;
        }

        public override void OnSuccess(WWWResult www)
        {
            MyPhoton photon;
            Network.EErrCode code;
            if (Network.IsError == null)
            {
                goto Label_006F;
            }
            if (Network.ErrCode == 0x2722)
            {
                goto Label_0020;
            }
            goto Label_006F;
        Label_0020:
            photon = PunMonoSingleton<MyPhoton>.Instance;
            Network.RemoveAPI();
            base.set_enabled(0);
            if (photon.IsConnectedInRoom() == null)
            {
                goto Label_0061;
            }
            if (photon.GetRoomPlayerList().Count <= 1)
            {
                goto Label_0061;
            }
            Network.ResetError();
            base.ActivateOutputLinks(this.PIN_SUCCESS);
            return;
        Label_0061:
            base.ActivateOutputLinks(this.PIN_FAILURE);
            return;
        Label_006F:
            GlobalVars.VersusFreeMatchTime = TimeManager.FromDateTime(TimeManager.ServerTime);
            this.Success();
            return;
        }

        private void Success()
        {
            Network.RemoveAPI();
            base.set_enabled(0);
            base.ActivateOutputLinks(this.PIN_SUCCESS);
            return;
        }
    }
}

