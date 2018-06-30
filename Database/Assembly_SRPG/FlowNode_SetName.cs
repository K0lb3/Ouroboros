namespace SRPG
{
    using GR;
    using System;
    using UnityEngine;

    [NodeType("System/SetName", 0x7fe5), Pin(100, "Start", 0, 0), Pin(1, "Success", 1, 10), Pin(2, "Failure", 1, 11), Pin(3, "Rename", 1, 12)]
    public class FlowNode_SetName : FlowNode_Network
    {
        public FlowNode_SetName()
        {
            base..ctor();
            return;
        }

        private void Failure()
        {
            base.set_enabled(0);
            base.ActivateOutputLinks(2);
            return;
        }

        public override void OnActivate(int pinID)
        {
            if (pinID != 100)
            {
                goto Label_00CB;
            }
            if (Network.Mode != null)
            {
                goto Label_00C5;
            }
            if (string.IsNullOrEmpty(GlobalVars.EditPlayerName) == null)
            {
                goto Label_0035;
            }
            GlobalVars.EditPlayerName = MonoSingleton<GameManager>.Instance.Player.Name;
        Label_0035:
            if (string.IsNullOrEmpty(GlobalVars.EditPlayerName) == null)
            {
                goto Label_0069;
            }
            UIUtility.SystemMessage(null, LocalizedText.Get("sys.RENAME_PLAYER_NAME"), null, null, 0, -1);
            base.ActivateOutputLinks(3);
            base.set_enabled(0);
            return;
        Label_0069:
            if (MyMsgInput.isLegal(GlobalVars.EditPlayerName) != null)
            {
                goto Label_009D;
            }
            UIUtility.SystemMessage(null, LocalizedText.Get("sys.RENAME_PLAYER_NAME"), null, null, 0, -1);
            base.ActivateOutputLinks(3);
            base.set_enabled(0);
            return;
        Label_009D:
            base.ExecRequest(new ReqSetName(GlobalVars.EditPlayerName, new Network.ResponseCallback(this.ResponseCallback)));
            base.set_enabled(1);
            goto Label_00CB;
        Label_00C5:
            this.Success();
        Label_00CB:
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
            if (Network.ErrCode == 0x578)
            {
                goto Label_0020;
            }
            goto Label_0027;
        Label_0020:
            this.OnBack();
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
                MonoSingleton<GameManager>.Instance.Deserialize(response.body.units);
                MonoSingleton<GameManager>.Instance.Deserialize(response.body.items);
                MonoSingleton<GameManager>.Instance.Deserialize(response.body.mails);
                goto Label_00CF;
            }
            catch (Exception exception1)
            {
            Label_00B8:
                exception = exception1;
                Debug.LogException(exception);
                this.Failure();
                goto Label_00E0;
            }
        Label_00CF:
            GameParameter.UpdateValuesOfType(0);
            Network.RemoveAPI();
            this.Success();
        Label_00E0:
            return;
        }

        private void Success()
        {
            PlayerData data;
            if (Network.Mode != null)
            {
                goto Label_002B;
            }
            MyMetaps.TrackTutorialPoint("SetName");
            MyGrowthPush.registCustomerId(MonoSingleton<GameManager>.Instance.Player.OkyakusamaCode);
        Label_002B:
            base.set_enabled(0);
            base.ActivateOutputLinks(1);
            return;
        }
    }
}

