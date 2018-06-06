// Decompiled with JetBrains decompiler
// Type: SRPG.ArenaHistory
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  public class ArenaHistory : SRPG_ListBase
  {
    public ListItemEvents ListItem_Normal;
    public ListItemEvents ListItem_Self;
    public GameObject DetailWindow;

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
      Transform transform = ((Component) this).get_transform();
      ArenaPlayerHistory[] arenaHistory = MonoSingleton<GameManager>.Instance.GetArenaHistory();
      for (int index = 0; index < arenaHistory.Length; ++index)
      {
        ListItemEvents listItemEvents = (ListItemEvents) Object.Instantiate<ListItemEvents>((M0) this.ListItem_Normal);
        DataSource.Bind<ArenaPlayerHistory>(((Component) listItemEvents).get_gameObject(), arenaHistory[index]);
        DataSource.Bind<ArenaPlayer>(((Component) listItemEvents).get_gameObject(), arenaHistory[index].enemy);
        listItemEvents.OnSelect = new ListItemEvents.ListItemEvent(this.OnItemSelect);
        listItemEvents.OnOpenDetail = new ListItemEvents.ListItemEvent(this.OnItemDetail);
        this.AddItem(listItemEvents);
        ((Component) listItemEvents).get_transform().SetParent(transform, false);
        ((Component) listItemEvents).get_gameObject().SetActive(true);
      }
    }

    private void OnItemSelect(GameObject go)
    {
    }

    private void OnItemDetail(GameObject go)
    {
      if (Object.op_Equality((Object) this.DetailWindow, (Object) null))
        return;
      ArenaPlayerHistory dataOfClass = DataSource.FindDataOfClass<ArenaPlayerHistory>(go, (ArenaPlayerHistory) null);
      if (dataOfClass == null)
        return;
      GameObject gameObject = (GameObject) Object.Instantiate<GameObject>((M0) this.DetailWindow);
      DataSource.Bind<ArenaPlayer>(gameObject, dataOfClass.enemy);
      ((ArenaPlayerInfo) gameObject.GetComponent<ArenaPlayerInfo>()).UpdateValue();
    }
  }
}
