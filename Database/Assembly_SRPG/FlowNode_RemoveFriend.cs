namespace SRPG
{
    using GR;
    using System;

    [Pin(10, "ひとりフレンド解除", 0, 10), NodeType("System/RemoveFriend", 0x7fe5), Pin(20, "ひとりフレンド解除成功", 1, 20)]
    public class FlowNode_RemoveFriend : FlowNode_Network
    {
        public FlowNode_RemoveFriend()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            string[] textArray1;
            FriendData data;
            string str;
            int num;
            if (base.get_enabled() == null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            num = pinID;
            if (num == 10)
            {
                goto Label_001B;
            }
            goto Label_0071;
        Label_001B:
            if (Network.Mode != 1)
            {
                goto Label_0037;
            }
            base.ActivateOutputLinks(20);
            base.set_enabled(0);
            return;
        Label_0037:
            str = GlobalVars.SelectedFriend.FUID;
            textArray1 = new string[] { str };
            base.ExecRequest(new ReqFriendRemove(textArray1, new Network.ResponseCallback(this.ResponseCallback)));
            base.set_enabled(1);
            goto Label_007D;
        Label_0071:
            base.set_enabled(0);
        Label_007D:
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
            if (Network.ErrCode == 0x6a4)
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
                MonoSingleton<GameManager>.Instance.Deserialize(response.body.friends, 1);
                goto Label_00A5;
            }
            catch (Exception exception1)
            {
            Label_008E:
                exception = exception1;
                DebugUtility.LogException(exception);
                this.OnRetry();
                goto Label_00D0;
            }
        Label_00A5:
            MonoSingleton<GameManager>.GetInstanceDirect().RequestUpdateBadges(0x10);
            Network.RemoveAPI();
            base.ActivateOutputLinks(20);
            GameParameter.UpdateValuesOfType(0xcf);
            base.set_enabled(0);
        Label_00D0:
            return;
        }
    }
}

