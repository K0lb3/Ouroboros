namespace SRPG
{
    using GR;
    using System;
    using UnityEngine;

    public class ArenaHistory : SRPG_ListBase
    {
        public ListItemEvents ListItem_Normal;
        public ListItemEvents ListItem_Self;
        public GameObject DetailWindow;

        public ArenaHistory()
        {
            base..ctor();
            return;
        }

        private void OnItemDetail(GameObject go)
        {
            ArenaPlayerHistory history;
            GameObject obj2;
            if ((this.DetailWindow == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            history = DataSource.FindDataOfClass<ArenaPlayerHistory>(go, null);
            if (history != null)
            {
                goto Label_0021;
            }
            return;
        Label_0021:
            obj2 = Object.Instantiate<GameObject>(this.DetailWindow);
            DataSource.Bind<ArenaPlayer>(obj2, history.enemy);
            obj2.GetComponent<ArenaPlayerInfo>().UpdateValue();
            return;
        }

        private void OnItemSelect(GameObject go)
        {
        }

        private void Refresh()
        {
            Transform transform;
            ArenaPlayerHistory[] historyArray;
            GameManager manager;
            int num;
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
            historyArray = MonoSingleton<GameManager>.Instance.GetArenaHistory();
            num = 0;
            goto Label_00B7;
        Label_0033:
            events = null;
            events2 = Object.Instantiate<ListItemEvents>(this.ListItem_Normal);
            DataSource.Bind<ArenaPlayerHistory>(events2.get_gameObject(), historyArray[num]);
            DataSource.Bind<ArenaPlayer>(events2.get_gameObject(), historyArray[num].enemy);
            events2.OnSelect = new ListItemEvents.ListItemEvent(this.OnItemSelect);
            events2.OnOpenDetail = new ListItemEvents.ListItemEvent(this.OnItemDetail);
            base.AddItem(events2);
            events2.get_transform().SetParent(transform, 0);
            events2.get_gameObject().SetActive(1);
            num += 1;
        Label_00B7:
            if (num < ((int) historyArray.Length))
            {
                goto Label_0033;
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

