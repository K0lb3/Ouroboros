namespace SRPG
{
    using GR;
    using System;
    using System.Runtime.CompilerServices;
    using UnityEngine;

    [Pin(1, "Success", 1, 10), NodeType("System/ReqTowerRecover", 0x7fe5), Pin(0, "Request", 0, 0)]
    public class FlowNode_ReqTowerRecover : FlowNode_Network
    {
        private GameObject mFlowRoot;
        private int usedCoin;

        public FlowNode_ReqTowerRecover()
        {
            base..ctor();
            return;
        }

        [CompilerGenerated]
        private void <Success>m__1C2(GameObject go)
        {
            base.ActivateOutputLinks(1);
            return;
        }

        public override void OnActivate(int pinID)
        {
            TowerRecoverData data;
            TowerResuponse resuponse;
            byte num;
            if (pinID != null)
            {
                goto Label_0083;
            }
            data = DataSource.FindDataOfClass<TowerRecoverData>(base.get_gameObject(), null);
            resuponse = MonoSingleton<GameManager>.Instance.TowerResuponse;
            if (Network.Mode != null)
            {
                goto Label_0083;
            }
            if (data == null)
            {
                goto Label_007C;
            }
            num = resuponse.GetCurrentFloor().floor;
            this.usedCoin = data.useCoin;
            base.ExecRequest(new ReqTowerRecover(data.towerID, data.useCoin, resuponse.round, num, new Network.ResponseCallback(this.ResponseCallback)));
            base.set_enabled(1);
            goto Label_0083;
        Label_007C:
            base.set_enabled(0);
        Label_0083:
            return;
        }

        public override unsafe void OnSuccess(WWWResult www)
        {
            WebAPI.JSON_BodyResponse<JSON_ReqTowerRecoverResponse> response;
            GameManager manager;
            Exception exception;
            if (TowerErrorHandle.Error(this) == null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<JSON_ReqTowerRecoverResponse>>(&www.text);
            DebugUtility.Assert((response == null) == 0, "res == null");
            Network.RemoveAPI();
            manager = MonoSingleton<GameManager>.Instance;
        Label_0035:
            try
            {
                manager.Deserialize(response.body.player);
                manager.TowerResuponse.Deserialize(response.body.pdeck);
                manager.TowerResuponse.rtime = (long) response.body.rtime;
                manager.TowerResuponse.recover_num = response.body.rcv_num;
                goto Label_009F;
            }
            catch (Exception exception1)
            {
            Label_008E:
                exception = exception1;
                DebugUtility.LogException(exception);
                goto Label_00A5;
            }
        Label_009F:
            this.Success();
        Label_00A5:
            return;
        }

        private unsafe void Success()
        {
            object[] objArray1;
            string str;
            string str2;
            base.set_enabled(0);
            str = LocalizedText.Get("sys.CAPTION_TOWER_RECOVERED");
            objArray1 = new object[] { &this.usedCoin.ToString() };
            str2 = LocalizedText.Get("sys.MSG_TOWER_RECOVERED", objArray1);
            UIUtility.SystemMessage(str, str2, new UIUtility.DialogResultEvent(this.<Success>m__1C2), null, 0, -1);
            return;
        }

        private class JSON_ReqTowerRecoverResponse
        {
            public Json_PlayerData player;
            public int rtime;
            public int rcv_num;
            public JSON_ReqTowerResuponse.Json_TowerPlayerUnit[] pdeck;

            public JSON_ReqTowerRecoverResponse()
            {
                base..ctor();
                return;
            }
        }
    }
}

