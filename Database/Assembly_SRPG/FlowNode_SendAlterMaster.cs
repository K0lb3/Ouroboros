namespace SRPG
{
    using GR;
    using System;

    [Pin(0, "Request", 0, 0), NodeType("System/SendAlterCheck", 0x7fe5), Pin(10, "Success", 1, 10), Pin(11, "Failed", 1, 11)]
    public class FlowNode_SendAlterMaster : FlowNode_Network
    {
        public FlowNode_SendAlterMaster()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            string str;
            string str2;
            if (pinID != null)
            {
                goto Label_0058;
            }
            str = MonoSingleton<GameManager>.GetInstanceDirect().DigestHash;
            str2 = MonoSingleton<GameManager>.GetInstanceDirect().AlterCheckHash;
            if (string.IsNullOrEmpty(str) != null)
            {
                goto Label_0051;
            }
            if (string.IsNullOrEmpty(str2) != null)
            {
                goto Label_0051;
            }
            base.ExecRequest(new ReqSendAlterData(new Network.ResponseCallback(this.ResponseCallback)));
            base.set_enabled(1);
            return;
        Label_0051:
            this.Success();
            return;
        Label_0058:
            return;
        }

        public override void OnSuccess(WWWResult www)
        {
            Network.EErrCode code;
            if (Network.IsError == null)
            {
                goto Label_0015;
            }
            code = Network.ErrCode;
        Label_0015:
            Network.RemoveAPI();
            PlayerPrefsUtility.SetString(PlayerPrefsUtility.ALTER_PREV_CHECK_HASH, MonoSingleton<GameManager>.Instance.AlterCheckHash, 0);
            this.Success();
            return;
        }

        private void Success()
        {
            MonoSingleton<GameManager>.GetInstanceDirect().AlterCheckHash = null;
            MonoSingleton<GameManager>.GetInstanceDirect().DigestHash = null;
            MonoSingleton<GameManager>.GetInstanceDirect().PrevCheckHash = null;
            base.set_enabled(0);
            base.ActivateOutputLinks(10);
            return;
        }
    }
}

