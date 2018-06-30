namespace SRPG
{
    using GR;
    using System;
    using UnityEngine;

    public class RankMatchRankingTab : SRPG_ListBase
    {
        [SerializeField]
        private GameObject PlayerGO;
        [SerializeField]
        private GameObject PlayerUnit;
        [SerializeField]
        private ListItemEvents ListItem;

        public RankMatchRankingTab()
        {
            base..ctor();
            return;
        }

        private unsafe void ResponseCallback(WWWResult www)
        {
            WebAPI.JSON_BodyResponse<ReqRankMatchRanking.Response> response;
            int num;
            ReqRankMatchRanking.ResponceRanking ranking;
            ListItemEvents events;
            FriendData data;
            Network.EErrCode code;
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
            response = JsonUtility.FromJson<WebAPI.JSON_BodyResponse<ReqRankMatchRanking.Response>>(&www.text);
            DebugUtility.Assert((response == null) == 0, "res == null");
            if (response.body != null)
            {
                goto Label_00CD;
            }
            Network.RemoveAPI();
            return;
        Label_00CD:
            if (response.body.rankings != null)
            {
                goto Label_00DE;
            }
            return;
        Label_00DE:
            num = 0;
            goto Label_0167;
        Label_00E5:
            ranking = response.body.rankings[num];
            events = Object.Instantiate<ListItemEvents>(this.ListItem);
            DataSource.Bind<ReqRankMatchRanking.ResponceRanking>(events.get_gameObject(), ranking);
            data = new FriendData();
            data.Deserialize(ranking.enemy);
            DataSource.Bind<FriendData>(events.get_gameObject(), data);
            DataSource.Bind<UnitData>(events.get_gameObject(), data.Unit);
            base.AddItem(events);
            events.get_transform().SetParent(base.get_transform(), 0);
            events.get_gameObject().SetActive(1);
            num += 1;
        Label_0167:
            if (num < ((int) response.body.rankings.Length))
            {
                goto Label_00E5;
            }
            Network.RemoveAPI();
            return;
        }

        protected override void Start()
        {
            PlayerData data;
            UnitData data2;
            base.Start();
            if ((this.PlayerUnit != null) == null)
            {
                goto Label_0067;
            }
            if ((this.PlayerGO != null) == null)
            {
                goto Label_0067;
            }
            data = MonoSingleton<GameManager>.Instance.Player;
            DataSource.Bind<PlayerData>(this.PlayerGO, data);
            data2 = data.FindUnitDataByUniqueID(GlobalVars.SelectedSupportUnitUniqueID);
            DataSource.Bind<UnitData>(this.PlayerUnit, data2);
            GameParameter.UpdateAll(this.PlayerUnit);
        Label_0067:
            if ((this.ListItem == null) == null)
            {
                goto Label_0079;
            }
            return;
        Label_0079:
            base.ClearItems();
            this.ListItem.get_gameObject().SetActive(0);
            Network.RequestAPI(new ReqRankMatchRanking(new SRPG.Network.ResponseCallback(this.ResponseCallback)), 0);
            return;
        }
    }
}

