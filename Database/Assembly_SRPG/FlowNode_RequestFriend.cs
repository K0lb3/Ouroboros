namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Text;
    using UnityEngine;

    [Pin(3, "相手のフレンドリスト上限", 1, 3), NodeType("System/RequestFriend", 0x7fe5), Pin(0, "Request", 0, 0), Pin(1, "Success", 1, 1), Pin(2, "自分のフレンドリスト上限", 1, 2), Pin(4, "存在しないプレイヤーを指定した", 1, 4), Pin(5, "一括申請Request", 0, 5), Pin(6, "すでにフレンド", 1, 6), Pin(7, "すでに申請を出している", 1, 7), Pin(10, "Request無し", 1, 10), Pin(11, "Success(ブロックリスト申請のみ)", 1, 11), Pin(12, "Success(フレンド申請&ブロックリスト申請)", 1, 12), Pin(13, "Failed(フレンドorブロック)", 1, 13)]
    public class FlowNode_RequestFriend : FlowNode_Network
    {
        private const int PIN_IN_REQUEST = 0;
        private const int PIN_OT_SUCCESS_FRIEND_ADD = 1;
        private const int PIN_OT_ERR_REQ_FRIEND_REQUEST_MAX = 2;
        private const int PIN_OT_ERR_REQ_FRIEND_IS_FULL = 3;
        private const int PIN_OT_ERR_NOT_PLAYER = 4;
        private const int PIN_IN_REQUEST_ALL = 5;
        private const int PIN_OT_ERR_REQ_FRIEND_REGISTERED = 6;
        private const int PIN_OT_ERR_REQ_FRIEND_REQUESTING = 7;
        private const int PIN_OT_SUCCESS_NOT_REQUEST = 10;
        private const int PIN_OT_SUCCESS_BLOCKLIST_ADD = 11;
        private const int PIN_OT_SUCCESS_FRIEND_BLOCK_ADD = 12;
        private const int PIN_OT_FAILED_FRIEND_BLOCK_ADD = 13;
        private int apiType;

        public FlowNode_RequestFriend()
        {
            base..ctor();
            return;
        }

        [CompilerGenerated]
        private void <OnSuccess>m__1C9(GameObject go)
        {
            this.Failure();
            return;
        }

        private void Failure()
        {
            base.set_enabled(0);
            base.ActivateOutputLinks(13);
            return;
        }

        public override void OnActivate(int pinID)
        {
            List<string> list;
            List<FriendData> list2;
            FriendWindowItem[] itemArray;
            int num;
            FriendWindowItem item;
            FriendData data;
            string str;
            <OnActivate>c__AnonStorey27A storeya;
            <OnActivate>c__AnonStorey27B storeyb;
            storeya = new <OnActivate>c__AnonStorey27A();
            storeya.<>f__this = this;
            if (base.get_enabled() == null)
            {
                goto Label_001B;
            }
            return;
        Label_001B:
            storeya.friendApplyList = new List<string>();
            list = new List<string>();
            list2 = MonoSingleton<GameManager>.Instance.Player.Friends;
            if (pinID != null)
            {
                goto Label_00A1;
            }
            if (string.IsNullOrEmpty(GlobalVars.SelectedFriendID) != null)
            {
                goto Label_0068;
            }
            storeya.friendApplyList.Add(GlobalVars.SelectedFriendID);
            goto Label_009C;
        Label_0068:
            if ((GlobalVars.FoundFriend == null) || (string.IsNullOrEmpty(GlobalVars.FoundFriend.FUID) != null))
            {
                goto Label_0151;
            }
            storeya.friendApplyList.Add(GlobalVars.FoundFriend.FUID);
        Label_009C:
            goto Label_0151;
        Label_00A1:
            itemArray = base.GetComponentsInChildren<FriendWindowItem>();
            if ((itemArray == null) || (((int) itemArray.Length) <= 0))
            {
                goto Label_0151;
            }
            num = 0;
            goto Label_0148;
        Label_00BE:
            item = itemArray[num];
            if (item.IsOn == null)
            {
                goto Label_0109;
            }
            storeya.friendApplyList.Add((item.Support == null) ? item.PlayerParam.FUID : item.Support.FUID);
            goto Label_0144;
        Label_0109:
            if (item.IsBlockOn == null)
            {
                goto Label_0144;
            }
            list.Add((item.Support == null) ? item.PlayerParam.FUID : item.Support.FUID);
        Label_0144:
            num += 1;
        Label_0148:
            if (num < ((int) itemArray.Length))
            {
                goto Label_00BE;
            }
        Label_0151:
            if (storeya.friendApplyList.Count > 0)
            {
                goto Label_016F;
            }
            if (list.Count <= 0)
            {
                goto Label_026E;
            }
        Label_016F:
            storeyb = new <OnActivate>c__AnonStorey27B();
            storeyb.<>f__ref$634 = storeya;
            storeyb.<>f__this = this;
            storeyb.i = 0;
            goto Label_0201;
        Label_0194:
            data = list2.Find(new Predicate<FriendData>(storeyb.<>m__1C7));
            if (data == null)
            {
                goto Label_01E3;
            }
            if (data.State != 1)
            {
                goto Label_01E3;
            }
            str = LocalizedText.Get("sys.FRIEND_ALREADY_FRIEND");
            UIUtility.SystemMessage(null, str, new UIUtility.DialogResultEvent(storeyb.<>m__1C8), null, 0, -1);
            return;
        Label_01E3:
            this.apiType |= 2;
            storeyb.i += 1;
        Label_0201:
            if (storeyb.i < storeya.friendApplyList.Count)
            {
                goto Label_0194;
            }
            if (list == null)
            {
                goto Label_0239;
            }
            if (list.Count <= 0)
            {
                goto Label_0239;
            }
            this.apiType |= 4;
        Label_0239:
            base.ExecRequest(new ReqFriendBlockApply(storeya.friendApplyList.ToArray(), list.ToArray(), new Network.ResponseCallback(this.ResponseCallback)));
            base.set_enabled(1);
            goto Label_027E;
        Label_026E:
            base.set_enabled(0);
            base.ActivateOutputLinks(10);
        Label_027E:
            return;
        }

        public override unsafe void OnSuccess(WWWResult www)
        {
            WebAPI.JSON_BodyResponse<Json_RequestFriendResponse> response;
            Exception exception;
            StringBuilder builder;
            Network.EErrCode code;
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_RequestFriendResponse>>(&www.text);
            DebugUtility.Assert((response == null) == 0, "res == null");
            if (Network.IsError == null)
            {
                goto Label_0061;
            }
            switch ((Network.ErrCode - 0x640))
            {
                case 0:
                    goto Label_0053;

                case 1:
                    goto Label_0053;

                case 2:
                    goto Label_005A;

                case 3:
                    goto Label_0053;

                case 4:
                    goto Label_0053;
            }
            goto Label_005A;
        Label_0053:
            this.OnBack();
            return;
        Label_005A:
            this.OnRetry();
            return;
        Label_0061:
            if (response.body != null)
            {
                goto Label_0073;
            }
            this.OnRetry();
            return;
        Label_0073:
            try
            {
                MonoSingleton<GameManager>.Instance.Deserialize(response.body.player);
                goto Label_009F;
            }
            catch (Exception exception1)
            {
            Label_008D:
                exception = exception1;
                base.OnRetry(exception);
                goto Label_0164;
            }
        Label_009F:
            MonoSingleton<GameManager>.GetInstanceDirect().RequestUpdateBadges(0x10);
            Network.RemoveAPI();
            if (response.body.errors == null)
            {
                goto Label_015E;
            }
            builder = GameUtility.GetStringBuilder();
            if (response.body.errors.friends == null)
            {
                goto Label_0104;
            }
            if (((int) response.body.errors.friends.Length) <= 0)
            {
                goto Label_0104;
            }
            builder.Append(LocalizedText.Get("sys.FRIEND_REQ_ERROR_MESSAGE"));
        Label_0104:
            if (response.body.errors.blocks == null)
            {
                goto Label_0142;
            }
            if (((int) response.body.errors.blocks.Length) <= 0)
            {
                goto Label_0142;
            }
            builder.Append(LocalizedText.Get("sys.BLOCK_REQ_ERROR_MESSAGE"));
        Label_0142:
            UIUtility.SystemMessage(builder.ToString(), new UIUtility.DialogResultEvent(this.<OnSuccess>m__1C9), null, 0, -1);
            return;
        Label_015E:
            this.Success();
        Label_0164:
            return;
        }

        private void Success()
        {
            int num;
            base.set_enabled(0);
            num = 1;
            if ((this.apiType & 4) == null)
            {
                goto Label_002E;
            }
            if ((this.apiType & 2) == null)
            {
                goto Label_002B;
            }
            num = 12;
            goto Label_002E;
        Label_002B:
            num = 11;
        Label_002E:
            base.ActivateOutputLinks(num);
            return;
        }

        [CompilerGenerated]
        private sealed class <OnActivate>c__AnonStorey27A
        {
            internal List<string> friendApplyList;
            internal FlowNode_RequestFriend <>f__this;

            public <OnActivate>c__AnonStorey27A()
            {
                base..ctor();
                return;
            }
        }

        [CompilerGenerated]
        private sealed class <OnActivate>c__AnonStorey27B
        {
            internal int i;
            internal FlowNode_RequestFriend.<OnActivate>c__AnonStorey27A <>f__ref$634;
            internal FlowNode_RequestFriend <>f__this;

            public <OnActivate>c__AnonStorey27B()
            {
                base..ctor();
                return;
            }

            internal bool <>m__1C7(FriendData f)
            {
                return (f.FUID == this.<>f__ref$634.friendApplyList[this.i]);
            }

            internal void <>m__1C8(GameObject go)
            {
                this.<>f__this.ActivateOutputLinks(6);
                return;
            }
        }

        private enum APIType
        {
            None,
            Friend,
            Block
        }
    }
}

