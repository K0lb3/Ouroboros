namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;

    [Pin(0, "Request", 0, 0), NodeType("System/SetAbility", 0x7fe5), Pin(1, "Success", 1, 1)]
    public class FlowNode_SetAbility : FlowNode_Network
    {
        private long mUnitUniqueID;
        private Queue<long> mJobs;
        private Queue<long> mAbilities;

        public FlowNode_SetAbility()
        {
            this.mJobs = new Queue<long>();
            this.mAbilities = new Queue<long>();
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            UnitData data;
            int num;
            int num2;
            if (pinID != null)
            {
                goto Label_00B9;
            }
            if (Network.Mode != null)
            {
                goto Label_00B3;
            }
            this.mUnitUniqueID = GlobalVars.SelectedUnitUniqueID;
            data = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID(this.mUnitUniqueID);
            num = 0;
            goto Label_0093;
        Label_003D:
            this.mJobs.Enqueue(data.Jobs[num].UniqueID);
            num2 = 0;
            goto Label_007A;
        Label_005C:
            this.mAbilities.Enqueue(data.Jobs[num].AbilitySlots[num2]);
            num2 += 1;
        Label_007A:
            if (num2 < ((int) data.Jobs[num].AbilitySlots.Length))
            {
                goto Label_005C;
            }
            num += 1;
        Label_0093:
            if (num < ((int) data.Jobs.Length))
            {
                goto Label_003D;
            }
            this.UpdateAbilities();
            base.set_enabled(1);
            goto Label_00B9;
        Label_00B3:
            this.Success();
        Label_00B9:
            return;
        }

        public override unsafe void OnSuccess(WWWResult www)
        {
            WebAPI.JSON_BodyResponse<Json_PlayerDataAll> response;
            Exception exception;
            Network.EErrCode code;
            if (Network.IsError == null)
            {
                goto Label_003B;
            }
            switch ((Network.ErrCode - 0x960))
            {
                case 0:
                    goto Label_002D;

                case 1:
                    goto Label_002D;

                case 2:
                    goto Label_002D;
            }
            goto Label_0034;
        Label_002D:
            this.OnFailed();
            return;
        Label_0034:
            this.OnRetry();
            return;
        Label_003B:
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_PlayerDataAll>>(&www.text);
            DebugUtility.Assert((response == null) == 0, "res == null");
        Label_0059:
            try
            {
                if (response.body != null)
                {
                    goto Label_006A;
                }
                throw new InvalidJSONException();
            Label_006A:
                MonoSingleton<GameManager>.Instance.Deserialize(response.body.player);
                MonoSingleton<GameManager>.Instance.Deserialize(response.body.units);
                goto Label_00AB;
            }
            catch (Exception exception1)
            {
            Label_0099:
                exception = exception1;
                base.OnRetry(exception);
                goto Label_00D2;
            }
        Label_00AB:
            Network.RemoveAPI();
            if (this.mJobs.Count >= 1)
            {
                goto Label_00CC;
            }
            this.Success();
            goto Label_00D2;
        Label_00CC:
            this.UpdateAbilities();
        Label_00D2:
            return;
        }

        private void Success()
        {
            base.set_enabled(0);
            base.ActivateOutputLinks(1);
            return;
        }

        private void UpdateAbilities()
        {
            long num;
            long[] numArray;
            int num2;
            num = this.mJobs.Dequeue();
            numArray = new long[5];
            num2 = 0;
            goto Label_002C;
        Label_001A:
            numArray[num2] = this.mAbilities.Dequeue();
            num2 += 1;
        Label_002C:
            if (num2 < 5)
            {
                goto Label_001A;
            }
            base.ExecRequest(new ReqJobAbility(num, numArray, new Network.ResponseCallback(this.ResponseCallback)));
            return;
        }
    }
}

