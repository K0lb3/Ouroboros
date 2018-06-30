namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;

    [Pin(20, "ひとり拒否成功", 1, 20), Pin(0x15, "すべて拒否成功", 1, 0x15), NodeType("System/RejectFriend", 0x7fe5), Pin(10, "ひとり拒否", 0, 10), Pin(11, "すべて拒否", 0, 11)]
    public class FlowNode_RejectFriend : FlowNode_Network
    {
        private bool mIsRemoveAll;
        private string remove_fuid;

        public FlowNode_RejectFriend()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            string[] textArray1;
            FriendData data;
            string str;
            List<FriendData> list;
            List<FriendData> list2;
            int num;
            string[] strArray;
            int num2;
            int num3;
            if (base.get_enabled() == null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            this.mIsRemoveAll = 0;
            this.remove_fuid = null;
            num3 = pinID;
            if (num3 == 10)
            {
                goto Label_0034;
            }
            if (num3 == 11)
            {
                goto Label_0091;
            }
            goto Label_0171;
        Label_0034:
            if (Network.Mode != 1)
            {
                goto Label_0050;
            }
            base.ActivateOutputLinks(20);
            base.set_enabled(0);
            return;
        Label_0050:
            str = GlobalVars.SelectedFriend.FUID;
            textArray1 = new string[] { str };
            base.ExecRequest(new ReqFriendReject(textArray1, new Network.ResponseCallback(this.ResponseCallback)));
            this.remove_fuid = str;
            base.set_enabled(1);
            goto Label_017D;
        Label_0091:
            this.mIsRemoveAll = 1;
            if (Network.Mode != 1)
            {
                goto Label_00B4;
            }
            base.ActivateOutputLinks(0x15);
            base.set_enabled(0);
            return;
        Label_00B4:
            list = MonoSingleton<GameManager>.Instance.Player.FriendsFollower;
            list2 = new List<FriendData>();
            num = 0;
            goto Label_00F8;
        Label_00D2:
            if (list[num] != null)
            {
                goto Label_00E4;
            }
            goto Label_00F2;
        Label_00E4:
            list2.Add(list[num]);
        Label_00F2:
            num += 1;
        Label_00F8:
            if (num < list.Count)
            {
                goto Label_00D2;
            }
            if (list2.Count >= 1)
            {
                goto Label_0112;
            }
            return;
        Label_0112:
            strArray = new string[list2.Count];
            num2 = 0;
            goto Label_013F;
        Label_0127:
            strArray[num2] = list2[num2].FUID;
            num2 += 1;
        Label_013F:
            if (num2 < list2.Count)
            {
                goto Label_0127;
            }
            base.ExecRequest(new ReqFriendReject(strArray, new Network.ResponseCallback(this.ResponseCallback)));
            base.set_enabled(1);
            goto Label_017D;
        Label_0171:
            base.set_enabled(0);
        Label_017D:
            return;
        }

        public override unsafe void OnSuccess(WWWResult www)
        {
            WebAPI.JSON_BodyResponse<Json_PlayerData> response;
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
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_PlayerData>>(&www.text);
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
                goto Label_007A;
            }
            catch (Exception exception1)
            {
            Label_0063:
                exception = exception1;
                DebugUtility.LogException(exception);
                this.OnRetry();
                goto Label_00E2;
            }
        Label_007A:
            MonoSingleton<GameManager>.GetInstanceDirect().RequestUpdateBadges(0x10);
            Network.RemoveAPI();
            if (this.mIsRemoveAll == null)
            {
                goto Label_00B3;
            }
            MonoSingleton<GameManager>.Instance.Player.RemoveFriendFollowerAll();
            base.ActivateOutputLinks(0x15);
            goto Label_00D1;
        Label_00B3:
            MonoSingleton<GameManager>.Instance.Player.RemoveFriendFollower(this.remove_fuid);
            base.ActivateOutputLinks(20);
        Label_00D1:
            GameParameter.UpdateValuesOfType(0xcf);
            base.set_enabled(0);
        Label_00E2:
            return;
        }
    }
}

