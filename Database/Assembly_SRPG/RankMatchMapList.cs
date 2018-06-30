namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    public class RankMatchMapList : SRPG_ListBase
    {
        public ListItemEvents ListItem_Normal;

        public RankMatchMapList()
        {
            base..ctor();
            return;
        }

        private void Refresh()
        {
            GameManager manager;
            List<VersusEnableTimeScheduleParam> list;
            int num;
            int num2;
            bool flag;
            int num3;
            ListItemEvents events;
            base.ClearItems();
            if ((this.ListItem_Normal == null) == null)
            {
                goto Label_0018;
            }
            return;
        Label_0018:
            manager = MonoSingleton<GameManager>.Instance;
            list = manager.GetVersusRankMapSchedule(manager.RankMatchScheduleId);
            num = SRPG_Extensions.ToYMD(TimeManager.ServerTime);
            num2 = 0;
            goto Label_0107;
        Label_003D:
            if (list[num2].AddDateList == null)
            {
                goto Label_00BB;
            }
            if (list[num2].AddDateList.Count <= 0)
            {
                goto Label_00BB;
            }
            flag = 0;
            num3 = 0;
            goto Label_0097;
        Label_0070:
            if (SRPG_Extensions.ToYMD(list[num2].AddDateList[num3]) != num)
            {
                goto Label_0091;
            }
            flag = 1;
        Label_0091:
            num3 += 1;
        Label_0097:
            if (num3 < list[num2].AddDateList.Count)
            {
                goto Label_0070;
            }
            if (flag != null)
            {
                goto Label_00BB;
            }
            goto Label_0103;
        Label_00BB:
            events = Object.Instantiate<ListItemEvents>(this.ListItem_Normal);
            DataSource.Bind<VersusEnableTimeScheduleParam>(events.get_gameObject(), list[num2]);
            base.AddItem(events);
            events.get_transform().SetParent(base.get_transform(), 0);
            events.get_gameObject().SetActive(1);
        Label_0103:
            num2 += 1;
        Label_0107:
            if (num2 < list.Count)
            {
                goto Label_003D;
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

