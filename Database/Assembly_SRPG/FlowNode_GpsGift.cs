namespace SRPG
{
    using GR;
    using System;

    [Pin(20, "GPSギフト受け取った", 1, 20), Pin(0x16, "GPSギフト受け取れなかった", 1, 0x16), Pin(0x15, "GPSギフト受け取り済み", 1, 0x15), Pin(100, "Start", 0, 0), NodeType("System/WebApi/GpsGift", 0x7fe5)]
    public class FlowNode_GpsGift : FlowNode_Network
    {
        private RecieveStatus m_RecieveStatus;

        public FlowNode_GpsGift()
        {
            this.m_RecieveStatus = 2;
            base..ctor();
            return;
        }

        private void _Failed()
        {
            RecieveStatus status;
            status = this.m_RecieveStatus;
            if (status == 1)
            {
                goto Label_001A;
            }
            if (status == 2)
            {
                goto Label_0028;
            }
            goto Label_0036;
        Label_001A:
            base.ActivateOutputLinks(0x15);
            goto Label_0036;
        Label_0028:
            base.ActivateOutputLinks(0x16);
        Label_0036:
            Network.RemoveAPI();
            Network.ResetError();
            base.set_enabled(0);
            return;
        }

        private void _Success()
        {
            base.ActivateOutputLinks(20);
            base.set_enabled(0);
            return;
        }

        public override void OnActivate(int pinID)
        {
            if (pinID != 100)
            {
                goto Label_0047;
            }
            if (Network.Mode != null)
            {
                goto Label_003A;
            }
            base.ExecRequest(new GpsGift(GlobalVars.Location, new Network.ResponseCallback(this.ResponseCallback)));
            base.set_enabled(1);
            goto Label_0047;
        Label_003A:
            this.m_RecieveStatus = 2;
            this._Failed();
        Label_0047:
            return;
        }

        public override void OnSuccess(WWWResult www)
        {
            Network.EErrCode code;
            if (Network.IsError == null)
            {
                goto Label_0060;
            }
            code = Network.ErrCode;
            if (code == 0x2198)
            {
                goto Label_0044;
            }
            if (code == 0x2199)
            {
                goto Label_0036;
            }
            if (code == 0x191)
            {
                goto Label_0052;
            }
            goto Label_0059;
        Label_0036:
            this.m_RecieveStatus = 1;
            this._Failed();
            return;
        Label_0044:
            this.m_RecieveStatus = 2;
            this._Failed();
            return;
        Label_0052:
            this.OnBack();
            return;
        Label_0059:
            this.OnRetry();
            return;
        Label_0060:
            Network.RemoveAPI();
            MonoSingleton<GameManager>.Instance.Player.UnreadMailPeriod = 1;
            MonoSingleton<GameManager>.Instance.RequestUpdateBadges(0x100);
            this.m_RecieveStatus = 0;
            this._Success();
            return;
        }

        private enum RecieveStatus
        {
            SUCCESS_RECEIVE,
            FAILED_RECEIVED,
            FAILED_NOTRECEIVE
        }
    }
}

