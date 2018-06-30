namespace SRPG
{
    using GR;
    using System;

    [Pin(0, "Request", 0, 0), Pin(1, "Success", 1, 1), NodeType("System/SetEquip", 0x7fe5)]
    public class FlowNode_SetEquip : FlowNode_Network
    {
        public FlowNode_SetEquip()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            long num;
            int num2;
            UnitData data;
            JobData data2;
            long num3;
            long num4;
            if (pinID != null)
            {
                goto Label_0082;
            }
            if (Network.Mode != null)
            {
                goto Label_007C;
            }
            num = GlobalVars.SelectedUnitUniqueID;
            num2 = GlobalVars.SelectedUnitJobIndex;
            data2 = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID(num).Jobs[num2];
            num3 = data2.UniqueID;
            num4 = (long) GlobalVars.SelectedEquipmentSlot;
            base.ExecRequest(new ReqJobEquip(num3, num4, new Network.ResponseCallback(this.ResponseCallback)));
            base.set_enabled(1);
            goto Label_0082;
        Label_007C:
            this.Success();
        Label_0082:
            return;
        }

        public override unsafe void OnSuccess(WWWResult www)
        {
            WebAPI.JSON_BodyResponse<Json_PlayerDataAll> response;
            Exception exception;
            long num;
            int num2;
            UnitData data;
            Network.EErrCode code;
            if (Network.IsError == null)
            {
                goto Label_0044;
            }
            switch ((Network.ErrCode - 0x9c4))
            {
                case 0:
                    goto Label_002F;

                case 1:
                    goto Label_0036;

                case 2:
                    goto Label_0036;
            }
            goto Label_003D;
        Label_002F:
            this.OnFailed();
            return;
        Label_0036:
            this.OnBack();
            return;
        Label_003D:
            this.OnRetry();
            return;
        Label_0044:
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_PlayerDataAll>>(&www.text);
            DebugUtility.Assert((response == null) == 0, "res == null");
        Label_0062:
            try
            {
                if (response.body != null)
                {
                    goto Label_0073;
                }
                throw new InvalidJSONException();
            Label_0073:
                MonoSingleton<GameManager>.Instance.Deserialize(response.body.player);
                MonoSingleton<GameManager>.Instance.Deserialize(response.body.units);
                MonoSingleton<GameManager>.Instance.Deserialize(response.body.items);
                goto Label_00C9;
            }
            catch (Exception exception1)
            {
            Label_00B7:
                exception = exception1;
                base.OnRetry(exception);
                goto Label_0104;
            }
        Label_00C9:
            Network.RemoveAPI();
            num = GlobalVars.SelectedUnitUniqueID;
            num2 = GlobalVars.SelectedUnitJobIndex;
            MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID(num).SetJobIndex(num2);
            this.Success();
        Label_0104:
            return;
        }

        private void Success()
        {
            base.set_enabled(0);
            base.ActivateOutputLinks(1);
            return;
        }
    }
}

