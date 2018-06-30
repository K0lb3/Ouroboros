namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;

    [NodeType("System/AddUnitExp", 0x7fe5), Pin(0, "Request", 0, 0), Pin(1, "Success", 1, 1)]
    public class FlowNode_AddUnitExp : FlowNode_Network
    {
        public FlowNode_AddUnitExp()
        {
            base..ctor();
            return;
        }

        public override unsafe void OnActivate(int pinID)
        {
            Dictionary<string, int> dictionary;
            int num;
            KeyValuePair<string, int> pair;
            Dictionary<string, int>.Enumerator enumerator;
            long num2;
            if (Network.Mode != 1)
            {
                goto Label_0012;
            }
            this.Success();
            return;
        Label_0012:
            dictionary = GlobalVars.UsedUnitExpItems;
            if (dictionary.Count >= 1)
            {
                goto Label_002B;
            }
            this.Success();
            return;
        Label_002B:
            num = 0;
            enumerator = dictionary.GetEnumerator();
        Label_0034:
            try
            {
                goto Label_004B;
            Label_0039:
                pair = &enumerator.Current;
                num += &pair.Value;
            Label_004B:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0039;
                }
                goto Label_0068;
            }
            finally
            {
            Label_005C:
                ((Dictionary<string, int>.Enumerator) enumerator).Dispose();
            }
        Label_0068:
            if (num >= 1)
            {
                goto Label_0076;
            }
            this.Success();
            return;
        Label_0076:
            num2 = GlobalVars.SelectedUnitUniqueID;
            base.ExecRequest(new ReqUnitExpAdd(num2, dictionary, new Network.ResponseCallback(this.ResponseCallback)));
            base.set_enabled(1);
            GlobalVars.UsedUnitExpItems.Clear();
            return;
        }

        public override unsafe void OnSuccess(WWWResult www)
        {
            WebAPI.JSON_BodyResponse<Json_PlayerDataAll> response;
            Exception exception;
            Network.EErrCode code;
            if (Network.IsError == null)
            {
                goto Label_002E;
            }
            if (Network.ErrCode == 0x76c)
            {
                goto Label_0020;
            }
            goto Label_0027;
        Label_0020:
            this.OnFailed();
            return;
        Label_0027:
            this.OnRetry();
            return;
        Label_002E:
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_PlayerDataAll>>(&www.text);
            DebugUtility.Assert((response == null) == 0, "res == null");
            if (response.body != null)
            {
                goto Label_005E;
            }
            this.OnRetry();
            return;
        Label_005E:
            try
            {
                MonoSingleton<GameManager>.Instance.Deserialize(response.body.player);
                MonoSingleton<GameManager>.Instance.Deserialize(response.body.items);
                MonoSingleton<GameManager>.Instance.Deserialize(response.body.units);
                goto Label_00B9;
            }
            catch (Exception exception1)
            {
            Label_00A2:
                exception = exception1;
                DebugUtility.LogException(exception);
                this.OnRetry();
                goto Label_00C4;
            }
        Label_00B9:
            Network.RemoveAPI();
            this.Success();
        Label_00C4:
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

