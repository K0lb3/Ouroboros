namespace SRPG
{
    using GR;
    using System;
    using UnityEngine;

    public class ArenaRankingList : SRPG_ListBase
    {
        public ReqBtlColoRanking.RankingTypes RankingType;
        public ListItemEvents ListItem_Normal;
        public ListItemEvents ListItem_Self;
        public GameObject OwnRankingInfo;
        public GameObject DetailWindow;
        private ArenaPlayer arenaPlayerOwner;

        public ArenaRankingList()
        {
            base..ctor();
            return;
        }

        private void OnItemDetail(GameObject go)
        {
            ArenaPlayer player;
            GameObject obj2;
            if ((this.DetailWindow == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            player = DataSource.FindDataOfClass<ArenaPlayer>(go, null);
            if (player != null)
            {
                goto Label_0021;
            }
            return;
        Label_0021:
            obj2 = Object.Instantiate<GameObject>(this.DetailWindow);
            DataSource.Bind<ArenaPlayer>(obj2, player);
            obj2.GetComponent<ArenaPlayerInfo>().UpdateValue();
            return;
        }

        private void OnItemSelect(GameObject go)
        {
        }

        private void Refresh()
        {
            Transform transform;
            ArenaPlayer[] playerArray;
            GameManager manager;
            PlayerData data;
            PartyData data2;
            int num;
            long num2;
            int num3;
            ListItemEvents events;
            ListItemEvents events2;
            base.ClearItems();
            if ((this.ListItem_Normal == null) == null)
            {
                goto Label_0018;
            }
            return;
        Label_0018:
            transform = base.get_transform();
            manager = MonoSingleton<GameManager>.Instance;
            playerArray = manager.GetArenaRanking(this.RankingType);
            data = MonoSingleton<GameManager>.Instance.Player;
            this.arenaPlayerOwner = new ArenaPlayer();
            this.arenaPlayerOwner.PlayerName = data.Name;
            this.arenaPlayerOwner.PlayerLevel = data.Lv;
            this.arenaPlayerOwner.ArenaRank = data.ArenaRank;
            this.arenaPlayerOwner.battle_at = data.ArenaLastAt;
            this.arenaPlayerOwner.SelectAward = data.SelectedAward;
            data2 = data.FindPartyOfType(3);
            num = 0;
            goto Label_00D5;
        Label_00AE:
            num2 = data2.GetUnitUniqueID(num);
            this.arenaPlayerOwner.Unit[num] = data.FindUnitDataByUniqueID(num2);
            num += 1;
        Label_00D5:
            if (num < 3)
            {
                goto Label_00AE;
            }
            DataSource.Bind<ArenaPlayer>(this.OwnRankingInfo.get_gameObject(), this.arenaPlayerOwner);
            this.OwnRankingInfo.get_gameObject().SetActive(0);
            this.OwnRankingInfo.get_gameObject().SetActive(1);
            num3 = 0;
            goto Label_01C3;
        Label_011D:
            events = null;
            if ((playerArray[num3].FUID == manager.Player.FUID) == null)
            {
                goto Label_0146;
            }
            events = this.ListItem_Self;
        Label_0146:
            if ((events == null) == null)
            {
                goto Label_015B;
            }
            events = this.ListItem_Normal;
        Label_015B:
            events2 = Object.Instantiate<ListItemEvents>(events);
            DataSource.Bind<ArenaPlayer>(events2.get_gameObject(), playerArray[num3]);
            events2.OnSelect = new ListItemEvents.ListItemEvent(this.OnItemSelect);
            events2.OnOpenDetail = new ListItemEvents.ListItemEvent(this.OnItemDetail);
            base.AddItem(events2);
            events2.get_transform().SetParent(transform, 0);
            events2.get_gameObject().SetActive(1);
            num3 += 1;
        Label_01C3:
            if (num3 < ((int) playerArray.Length))
            {
                goto Label_011D;
            }
            return;
        }

        protected override void Start()
        {
            base.Start();
            if ((this.ListItem_Normal != null) == null)
            {
                goto Label_0028;
            }
            this.ListItem_Normal.get_gameObject().SetActive(0);
        Label_0028:
            this.Refresh();
            return;
        }
    }
}

