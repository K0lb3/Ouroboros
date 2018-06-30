namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    [Pin(110, "プレゼント一覧取得完了", 1, 110), NodeType("System/WebApi/PresentList", 0x7fe5), Pin(100, "一覧取得", 0, 100), Pin(420, "プレゼント贈ってくれた人失敗", 1, 420), Pin(410, "プレゼント贈ってくれた人完了", 1, 410), Pin(400, "贈ってくれた人", 0, 400), Pin(320, "プレゼント一括送付失敗", 1, 320), Pin(310, "プレゼント一括送付完了", 1, 310), Pin(300, "一括送付", 0, 300), Pin(220, "プレゼント一括受け取り失敗", 1, 220), Pin(210, "プレゼント一括受け取り完了", 1, 210), Pin(200, "一括受け取り", 0, 200), Pin(120, "プレゼント一覧取得失敗", 1, 120)]
    public class FlowNode_ReqPresentList : FlowNode_Network
    {
        private ApiBase m_Api;

        public FlowNode_ReqPresentList()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            SerializeValueList list;
            Api_PresentListGave gave;
            if (this.m_Api == null)
            {
                goto Label_0016;
            }
            DebugUtility.LogError("同時に複数の通信が入ると駄目！");
            return;
        Label_0016:
            if (pinID != 100)
            {
                goto Label_002F;
            }
            this.m_Api = new Api_PresentList(this);
            goto Label_009B;
        Label_002F:
            if (pinID != 200)
            {
                goto Label_004B;
            }
            this.m_Api = new Api_PresentListExec(this);
            goto Label_009B;
        Label_004B:
            if (pinID != 300)
            {
                goto Label_0067;
            }
            this.m_Api = new Api_PresentListSend(this);
            goto Label_009B;
        Label_0067:
            if (pinID != 400)
            {
                goto Label_009B;
            }
            list = FlowNode_ButtonEvent.currentValue as SerializeValueList;
            if (list == null)
            {
                goto Label_009B;
            }
            gave = new Api_PresentListGave(this, list.GetGameObject("item"));
            gave.Start();
        Label_009B:
            if (this.m_Api == null)
            {
                goto Label_00B8;
            }
            this.m_Api.Start();
            base.set_enabled(1);
        Label_00B8:
            return;
        }

        public override void OnSuccess(WWWResult www)
        {
            if (this.m_Api == null)
            {
                goto Label_001E;
            }
            this.m_Api.Complete(www);
            this.m_Api = null;
        Label_001E:
            return;
        }

        public class Api_PresentList : FlowNode_ReqPresentList.ApiBase
        {
            public Api_PresentList(FlowNode_ReqPresentList node)
            {
                base..ctor(node);
                return;
            }

            public override unsafe void Complete(WWWResult www)
            {
                WebAPI.JSON_BodyResponse<FriendPresentReceiveList.Json[]> response;
                if (Network.IsError == null)
                {
                    goto Label_0016;
                }
                base.m_Node.OnFailed();
                return;
            Label_0016:
                DebugMenu.Log("API", this.url + ":" + &www.text);
                response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<FriendPresentReceiveList.Json[]>>(&www.text);
                DebugUtility.Assert((response == null) == 0, "res == null");
                if (response.body == null)
                {
                    goto Label_0070;
                }
                MonoSingleton<GameManager>.Instance.Deserialize(response.body);
            Label_0070:
                Network.RemoveAPI();
                this.Success();
                return;
            }

            public override void Failed()
            {
                base.m_Node.ActivateOutputLinks(120);
                Network.RemoveAPI();
                Network.ResetError();
                base.m_Node.set_enabled(0);
                return;
            }

            public override void Success()
            {
                base.m_Node.ActivateOutputLinks(110);
                base.m_Node.set_enabled(0);
                return;
            }

            public override string url
            {
                get
                {
                    return "presentlist";
                }
            }
        }

        public class Api_PresentListExec : FlowNode_ReqPresentList.ApiBase
        {
            public Api_PresentListExec(FlowNode_ReqPresentList node)
            {
                base..ctor(node);
                return;
            }

            public override unsafe void Complete(WWWResult www)
            {
                WebAPI.JSON_BodyResponse<Json> response;
                bool flag;
                RewardData data;
                int num;
                JsonItem item;
                RewardData data2;
                GiftRecieveItemData data3;
                Dictionary<string, GiftRecieveItemData>.ValueCollection.Enumerator enumerator;
                if (Network.IsError == null)
                {
                    goto Label_0016;
                }
                base.m_Node.OnFailed();
                return;
            Label_0016:
                DebugMenu.Log("API", this.url + ":" + &www.text);
                response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json>>(&www.text);
                DebugUtility.Assert((response == null) == 0, "res == null");
                flag = 0;
                if (response.body == null)
                {
                    goto Label_0212;
                }
                if (response.body.player == null)
                {
                    goto Label_0087;
                }
                MonoSingleton<GameManager>.Instance.Deserialize(response.body.player);
            Label_0087:
                if (response.body.items == null)
                {
                    goto Label_00AC;
                }
                MonoSingleton<GameManager>.Instance.Deserialize(response.body.items);
            Label_00AC:
                if (response.body.presents == null)
                {
                    goto Label_0212;
                }
                data = new RewardData();
                num = 0;
                goto Label_01C9;
            Label_00C9:
                item = response.body.presents[num];
                if (item == null)
                {
                    goto Label_01C5;
                }
                data2 = this.ReceiveDataToRewardData(item);
                if (data2 == null)
                {
                    goto Label_01C5;
                }
                data.Exp += data2.Exp;
                data.Stamina += data2.Stamina;
                data.Coin += data2.Coin;
                data.Gold += data2.Gold;
                data.ArenaMedal += data2.ArenaMedal;
                data.MultiCoin += data2.MultiCoin;
                data.KakeraCoin += data2.KakeraCoin;
                enumerator = data2.GiftRecieveItemDataDic.Values.GetEnumerator();
            Label_018F:
                try
                {
                    goto Label_01A5;
                Label_0194:
                    data3 = &enumerator.Current;
                    data.AddReward(data3);
                Label_01A5:
                    if (&enumerator.MoveNext() != null)
                    {
                        goto Label_0194;
                    }
                    goto Label_01C3;
                }
                finally
                {
                Label_01B6:
                    ((Dictionary<string, GiftRecieveItemData>.ValueCollection.Enumerator) enumerator).Dispose();
                }
            Label_01C3:
                flag = 1;
            Label_01C5:
                num += 1;
            Label_01C9:
                if (num < ((int) response.body.presents.Length))
                {
                    goto Label_00C9;
                }
                GlobalVars.LastReward.Set(data);
                if (data == null)
                {
                    goto Label_0202;
                }
                MonoSingleton<GameManager>.Instance.Player.OnGoldChange(data.Gold);
            Label_0202:
                MonoSingleton<GameManager>.Instance.Player.ValidFriendPresent = 0;
            Label_0212:
                Network.RemoveAPI();
                if (flag == null)
                {
                    goto Label_0228;
                }
                this.Success();
                goto Label_022E;
            Label_0228:
                this.Failed();
            Label_022E:
                return;
            }

            public override void Failed()
            {
                base.m_Node.ActivateOutputLinks(220);
                Network.RemoveAPI();
                Network.ResetError();
                base.m_Node.set_enabled(0);
                return;
            }

            private RewardData ReceiveDataToRewardData(JsonItem receiveData)
            {
                MasterParam param;
                FriendPresentItemParam param2;
                RewardData data;
                param2 = MonoSingleton<GameManager>.Instance.MasterParam.GetFriendPresentItemParam(receiveData.iname);
                if (param2 != null)
                {
                    goto Label_0020;
                }
                return null;
            Label_0020:
                data = new RewardData();
                data.Exp = 0;
                data.Coin = 0;
                data.Gold = 0;
                data.Stamina = 0;
                data.MultiCoin = 0;
                data.KakeraCoin = 0;
                if (param2.IsItem() == null)
                {
                    goto Label_0079;
                }
                data.AddReward(param2.item, param2.num * receiveData.num);
                goto Label_008C;
            Label_0079:
                data.Gold = param2.zeny * receiveData.num;
            Label_008C:
                return data;
            }

            public override void Success()
            {
                base.m_Node.ActivateOutputLinks(210);
                base.m_Node.set_enabled(0);
                return;
            }

            public override string url
            {
                get
                {
                    return "presentlist/exec";
                }
            }

            [Serializable]
            public class Json
            {
                public Json_PlayerData player;
                public Json_Item[] items;
                public FlowNode_ReqPresentList.Api_PresentListExec.JsonItem[] presents;

                public Json()
                {
                    base..ctor();
                    return;
                }
            }

            [Serializable]
            public class JsonItem
            {
                public string iname;
                public int num;

                public JsonItem()
                {
                    base..ctor();
                    return;
                }
            }
        }

        public class Api_PresentListGave : FlowNode_ReqPresentList.ApiBase
        {
            private FriendPresentItemParam m_Param;

            public Api_PresentListGave(FlowNode_ReqPresentList node, GameObject gobj)
            {
                ContentNode node2;
                FriendPresentRootWindow.ReceiveContent.ItemSource.ItemParam param;
                base..ctor(node);
                if ((gobj != null) == null)
                {
                    goto Label_003F;
                }
                node2 = gobj.GetComponent<ContentNode>();
                if ((node2 != null) == null)
                {
                    goto Label_003F;
                }
                param = node2.GetParam<FriendPresentRootWindow.ReceiveContent.ItemSource.ItemParam>();
                if (param == null)
                {
                    goto Label_003F;
                }
                this.m_Param = param.present;
            Label_003F:
                return;
            }

            public void Complete()
            {
                FriendPresentGaveWindow window;
                FriendPresentReceiveList.Param param;
                int num;
                window = FriendPresentGaveWindow.instance;
                if (window == null)
                {
                    goto Label_0087;
                }
                if (this.m_Param == null)
                {
                    goto Label_0087;
                }
                window.ClearFuids();
                param = MonoSingleton<GameManager>.Instance.Player.FriendPresentReceiveList.GetParam(this.m_Param.iname);
                if (param == null)
                {
                    goto Label_007C;
                }
                num = 0;
                goto Label_0060;
            Label_004A:
                window.AddUid(param.uids[num]);
                num += 1;
            Label_0060:
                if (num < param.uids.Count)
                {
                    goto Label_004A;
                }
                this.Success();
                goto Label_0082;
            Label_007C:
                this.Failed();
            Label_0082:
                goto Label_008D;
            Label_0087:
                this.Failed();
            Label_008D:
                return;
            }

            public override void Failed()
            {
                base.m_Node.ActivateOutputLinks(320);
                base.m_Node.set_enabled(0);
                return;
            }

            public override void Start()
            {
                this.Complete();
                return;
            }

            public override void Success()
            {
                base.m_Node.ActivateOutputLinks(410);
                base.m_Node.set_enabled(0);
                return;
            }

            public override string url
            {
                get
                {
                    return "presentlist/exec";
                }
            }
        }

        public class Api_PresentListSend : FlowNode_ReqPresentList.ApiBase
        {
            public Api_PresentListSend(FlowNode_ReqPresentList node)
            {
                base..ctor(node);
                return;
            }

            public override unsafe void Complete(WWWResult www)
            {
                WebAPI.JSON_BodyResponse<Json> response;
                FriendPresentRootWindow window;
                if (Network.IsError == null)
                {
                    goto Label_0016;
                }
                base.m_Node.OnFailed();
                return;
            Label_0016:
                DebugMenu.Log("API", this.url + ":" + &www.text);
                response = JsonUtility.FromJson<WebAPI.JSON_BodyResponse<Json>>(&www.text);
                DebugUtility.Assert((response == null) == 0, "res == null");
                if (response.body == null)
                {
                    goto Label_0082;
                }
                if (response.body.result == null)
                {
                    goto Label_0082;
                }
                if (FriendPresentRootWindow.instance == null)
                {
                    goto Label_0082;
                }
                FriendPresentRootWindow.SetSendStatus(2);
            Label_0082:
                Network.RemoveAPI();
                MonoSingleton<GameManager>.Instance.ServerSyncTrophyExecEnd(www);
                this.Success();
                return;
            }

            public override void Failed()
            {
                base.m_Node.ActivateOutputLinks(320);
                Network.RemoveAPI();
                Network.ResetError();
                base.m_Node.set_enabled(0);
                return;
            }

            public override unsafe void Start()
            {
                string str;
                string str2;
                if (Network.Mode != null)
                {
                    goto Label_005B;
                }
                MonoSingleton<GameManager>.Instance.Player.UpdateSendFriendPresentTrophy();
                MonoSingleton<GameManager>.Instance.ServerSyncTrophyExecStart(&str, &str2);
                base.m_Node.ExecRequest(new ReqFriendPresentSend(this.url, new Network.ResponseCallback(base.m_Node.ResponseCallback), this.req, str, str2));
                goto Label_0061;
            Label_005B:
                this.Failed();
            Label_0061:
                return;
            }

            public override void Success()
            {
                base.m_Node.ActivateOutputLinks(310);
                base.m_Node.set_enabled(0);
                return;
            }

            public override string url
            {
                get
                {
                    return "present/send";
                }
            }

            [Serializable]
            public class Json
            {
                public bool result;

                public Json()
                {
                    base..ctor();
                    return;
                }
            }
        }

        public class ApiBase
        {
            protected FlowNode_ReqPresentList m_Node;
            protected RequestAPI m_Request;

            public ApiBase(FlowNode_ReqPresentList node)
            {
                base..ctor();
                this.m_Node = node;
                return;
            }

            public virtual void Complete(WWWResult www)
            {
            }

            public virtual void Failed()
            {
            }

            public virtual void Start()
            {
                if (Network.Mode != null)
                {
                    goto Label_003C;
                }
                this.m_Node.ExecRequest(new RequestAPI(this.url, new Network.ResponseCallback(this.m_Node.ResponseCallback), this.req));
                goto Label_0042;
            Label_003C:
                this.Failed();
            Label_0042:
                return;
            }

            public virtual void Success()
            {
            }

            public virtual string url
            {
                get
                {
                    return string.Empty;
                }
            }

            public virtual string req
            {
                get
                {
                    return null;
                }
            }
        }
    }
}

