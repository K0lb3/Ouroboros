namespace SRPG
{
    using GR;
    using System;
    using UnityEngine;

    [Pin(1, "Success", 1, 1), Pin(0, "Request", 0, 0), NodeType("VS/ReqCom", 0x7fe5)]
    public class FlowNode_VersusCpu : FlowNode_Network
    {
        public FlowNode_VersusCpu()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            GameManager manager;
            VersusStatusData data;
            int num;
            PartyData data2;
            int num2;
            long num3;
            UnitData data3;
            if (pinID != null)
            {
                goto Label_00D9;
            }
            if (base.get_enabled() == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            if (Network.Mode != 1)
            {
                goto Label_0024;
            }
            this.Success();
            return;
        Label_0024:
            manager = MonoSingleton<GameManager>.Instance;
            data = new VersusStatusData();
            num = 0;
            data2 = manager.Player.Partys[7];
            if (data2 == null)
            {
                goto Label_00B4;
            }
            num2 = 0;
            goto Label_00A7;
        Label_0052:
            num3 = data2.GetUnitUniqueID(num2);
            if (data2.GetUnitUniqueID(num2) != null)
            {
                goto Label_006E;
            }
            goto Label_00A1;
        Label_006E:
            data3 = manager.Player.FindUnitDataByUniqueID(num3);
            if (data3 == null)
            {
                goto Label_00A1;
            }
            data.Add(data3.Status.param, data3.GetCombination());
            num += 1;
        Label_00A1:
            num2 += 1;
        Label_00A7:
            if (num2 < data2.MAX_UNIT)
            {
                goto Label_0052;
            }
        Label_00B4:
            base.ExecRequest(new ReqVersusCpuList(data, num, GlobalVars.SelectedQuestID, new Network.ResponseCallback(this.ResponseCallback)));
            base.set_enabled(1);
        Label_00D9:
            return;
        }

        public override unsafe void OnSuccess(WWWResult www)
        {
            WebAPI.JSON_BodyResponse<Json_VersusCpu> response;
            GameManager manager;
            Network.EErrCode code;
            if (Network.IsError == null)
            {
                goto Label_0017;
            }
            code = Network.ErrCode;
            this.OnRetry();
            return;
        Label_0017:
            Debug.Log(&www.text);
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_VersusCpu>>(&www.text);
            DebugUtility.Assert((response == null) == 0, "res == null");
            if (response.body != null)
            {
                goto Label_0053;
            }
            this.OnRetry();
            return;
        Label_0053:
            base.set_enabled(0);
            if (MonoSingleton<GameManager>.Instance.Deserialize(response.body) != null)
            {
                goto Label_0078;
            }
            this.OnFailed();
            return;
        Label_0078:
            Network.RemoveAPI();
            this.Success();
            return;
        }

        private void Success()
        {
            base.ActivateOutputLinks(1);
            return;
        }
    }
}

