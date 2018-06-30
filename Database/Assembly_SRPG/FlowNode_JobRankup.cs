namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;

    [Pin(2, "Unlock", 1, 2), Pin(0, "Request", 0, 0), Pin(1, "Success", 1, 1), Pin(3, "ClassChange", 1, 3), NodeType("System/JobRankup", 0x7fe5)]
    public class FlowNode_JobRankup : FlowNode_Network
    {
        private int mSuccessPinID;

        public FlowNode_JobRankup()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            List<AbilityData> list;
            long num;
            this.mSuccessPinID = 1;
            if (pinID != null)
            {
                goto Label_0099;
            }
            list = GlobalVars.LearningAbilities;
            if (list == null)
            {
                goto Label_0034;
            }
            if (list.Count <= 0)
            {
                goto Label_0034;
            }
            FlowNode_Variable.Set("LEARNING_ABILITY", "1");
        Label_0034:
            if (GlobalVars.ReturnItems == null)
            {
                goto Label_005D;
            }
            if (GlobalVars.ReturnItems.Count <= 0)
            {
                goto Label_005D;
            }
            FlowNode_Variable.Set("RETURN_ITEMS", "1");
        Label_005D:
            if (Network.Mode != 1)
            {
                goto Label_006F;
            }
            this.Success();
            return;
        Label_006F:
            num = GlobalVars.SelectedJobUniqueID;
            base.ExecRequest(new ReqJobRankup(num, new Network.ResponseCallback(this.ResponseCallback)));
            base.set_enabled(1);
        Label_0099:
            return;
        }

        public override unsafe void OnSuccess(WWWResult www)
        {
            WebAPI.JSON_BodyResponse<Json_PlayerDataAll> response;
            Exception exception;
            long num;
            UnitData data;
            int num2;
            Network.EErrCode code;
            if (Network.IsError == null)
            {
                goto Label_0043;
            }
            code = Network.ErrCode;
            if (code == 0xa8c)
            {
                goto Label_002E;
            }
            if (code == 0xa8d)
            {
                goto Label_0035;
            }
            goto Label_003C;
        Label_002E:
            this.OnFailed();
            return;
        Label_0035:
            this.OnBack();
            return;
        Label_003C:
            this.OnRetry();
            return;
        Label_0043:
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_PlayerDataAll>>(&www.text);
            DebugUtility.Assert((response == null) == 0, "res == null");
        Label_0061:
            try
            {
                if (response.body != null)
                {
                    goto Label_0072;
                }
                throw new InvalidJSONException();
            Label_0072:
                MonoSingleton<GameManager>.Instance.Deserialize(response.body.player);
                MonoSingleton<GameManager>.Instance.Deserialize(response.body.units);
                MonoSingleton<GameManager>.Instance.Deserialize(response.body.items);
                goto Label_00CD;
            }
            catch (Exception exception1)
            {
            Label_00B6:
                exception = exception1;
                DebugUtility.LogException(exception);
                this.OnRetry();
                goto Label_013B;
            }
        Label_00CD:
            Network.RemoveAPI();
            num = GlobalVars.SelectedUnitUniqueID;
            data = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID(num);
            num2 = GlobalVars.SelectedUnitJobIndex;
            data.SetJobIndex(num2);
            if (GlobalVars.JobRankUpType != 1)
            {
                goto Label_011E;
            }
            this.mSuccessPinID = 2;
            goto Label_0135;
        Label_011E:
            if (GlobalVars.JobRankUpType != 2)
            {
                goto Label_0135;
            }
            this.mSuccessPinID = 3;
        Label_0135:
            this.Success();
        Label_013B:
            return;
        }

        private void Success()
        {
            base.set_enabled(0);
            base.ActivateOutputLinks(this.mSuccessPinID);
            return;
        }
    }
}

