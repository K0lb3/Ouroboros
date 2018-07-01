// Decompiled with JetBrains decompiler
// Type: SRPG.RankMatchMapList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  public class RankMatchMapList : SRPG_ListBase
  {
    public ListItemEvents ListItem_Normal;

    protected override void Start()
    {
      base.Start();
      if (Object.op_Inequality((Object) this.ListItem_Normal, (Object) null))
        ((Component) this.ListItem_Normal).get_gameObject().SetActive(false);
      this.Refresh();
    }

    private void Refresh()
    {
      this.ClearItems();
      if (Object.op_Equality((Object) this.ListItem_Normal, (Object) null))
        return;
      GameManager instance = MonoSingleton<GameManager>.Instance;
      List<VersusEnableTimeScheduleParam> versusRankMapSchedule = instance.GetVersusRankMapSchedule(instance.RankMatchScheduleId);
      int ymd = TimeManager.ServerTime.ToYMD();
      for (int index1 = 0; index1 < versusRankMapSchedule.Count; ++index1)
      {
        if (versusRankMapSchedule[index1].AddDateList != null && versusRankMapSchedule[index1].AddDateList.Count > 0)
        {
          bool flag = false;
          for (int index2 = 0; index2 < versusRankMapSchedule[index1].AddDateList.Count; ++index2)
          {
            if (versusRankMapSchedule[index1].AddDateList[index2].ToYMD() == ymd)
              flag = true;
          }
          if (!flag)
            continue;
        }
        ListItemEvents listItemEvents = (ListItemEvents) Object.Instantiate<ListItemEvents>((M0) this.ListItem_Normal);
        DataSource.Bind<VersusEnableTimeScheduleParam>(((Component) listItemEvents).get_gameObject(), versusRankMapSchedule[index1]);
        this.AddItem(listItemEvents);
        ((Component) listItemEvents).get_transform().SetParent(((Component) this).get_transform(), false);
        ((Component) listItemEvents).get_gameObject().SetActive(true);
      }
    }
  }
}
