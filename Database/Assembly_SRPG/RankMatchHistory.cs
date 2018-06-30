namespace SRPG
{
    using GR;
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    public class RankMatchHistory : SRPG_ListBase
    {
        [SerializeField]
        private GameObject PlayerGO;
        [SerializeField]
        private ListItemEvents ListItem;
        [SerializeField, Space(10f)]
        private Text LastBattleDate;

        public RankMatchHistory()
        {
            base..ctor();
            return;
        }

        private unsafe void ResponseCallback(WWWResult www)
        {
            WebAPI.JSON_BodyResponse<ReqRankMatchHistory.Response> response;
            long num;
            int num2;
            ReqRankMatchHistory.ResponceHistoryList list;
            ListItemEvents events;
            FriendData data;
            Network.EErrCode code;
            DateTime time;
            if (FlowNode_Network.HasCommonError(www) == null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            if (Network.IsError == null)
            {
                goto Label_009E;
            }
            code = Network.ErrCode;
            switch ((code - 0xca))
            {
                case 0:
                    goto Label_008B;

                case 1:
                    goto Label_008B;

                case 2:
                    goto Label_003E;

                case 3:
                    goto Label_008B;

                case 4:
                    goto Label_008B;
            }
        Label_003E:
            if (code == 0xce5)
            {
                goto Label_0067;
            }
            if (code == 0xe78)
            {
                goto Label_0079;
            }
            if (code == 0x2718)
            {
                goto Label_0079;
            }
            goto Label_0098;
        Label_0067:
            Network.RemoveAPI();
            Network.ResetError();
            base.set_enabled(0);
            return;
        Label_0079:
            Network.RemoveAPI();
            Network.ResetError();
            base.set_enabled(0);
            return;
        Label_008B:
            Network.RemoveAPI();
            base.set_enabled(0);
            return;
        Label_0098:
            FlowNode_Network.Retry();
            return;
        Label_009E:
            response = JsonUtility.FromJson<WebAPI.JSON_BodyResponse<ReqRankMatchHistory.Response>>(&www.text);
            DebugUtility.Assert((response == null) == 0, "res == null");
            if (response.body != null)
            {
                goto Label_00CD;
            }
            Network.RemoveAPI();
            return;
        Label_00CD:
            if (response.body.histories == null)
            {
                goto Label_00F2;
            }
            if (response.body.histories.list != null)
            {
                goto Label_00F3;
            }
        Label_00F2:
            return;
        Label_00F3:
            num = 0L;
            num2 = 0;
            goto Label_019E;
        Label_00FD:
            list = response.body.histories.list[num2];
            events = Object.Instantiate<ListItemEvents>(this.ListItem);
            DataSource.Bind<ReqRankMatchHistory.ResponceHistoryList>(events.get_gameObject(), list);
            data = new FriendData();
            data.Deserialize(list.enemy);
            DataSource.Bind<FriendData>(events.get_gameObject(), data);
            DataSource.Bind<UnitData>(events.get_gameObject(), data.Unit);
            base.AddItem(events);
            events.get_transform().SetParent(base.get_transform(), 0);
            events.get_gameObject().SetActive(1);
            if (num >= list.time_end)
            {
                goto Label_019A;
            }
            num = list.time_end;
        Label_019A:
            num2 += 1;
        Label_019E:
            if (num2 < ((int) response.body.histories.list.Length))
            {
                goto Label_00FD;
            }
            if ((this.LastBattleDate != null) == null)
            {
                goto Label_01EE;
            }
            if (num <= 0L)
            {
                goto Label_01EE;
            }
            this.LastBattleDate.set_text(&TimeManager.FromUnixTime(num).ToString("MM/dd HH:mm"));
        Label_01EE:
            Network.RemoveAPI();
            return;
        }

        protected override void Start()
        {
            PlayerData data;
            base.Start();
            if ((this.PlayerGO != null) == null)
            {
                goto Label_002E;
            }
            data = MonoSingleton<GameManager>.Instance.Player;
            DataSource.Bind<PlayerData>(this.PlayerGO, data);
        Label_002E:
            if ((this.ListItem == null) == null)
            {
                goto Label_0040;
            }
            return;
        Label_0040:
            base.ClearItems();
            this.ListItem.get_gameObject().SetActive(0);
            Network.RequestAPI(new ReqRankMatchHistory(new SRPG.Network.ResponseCallback(this.ResponseCallback)), 0);
            return;
        }
    }
}

