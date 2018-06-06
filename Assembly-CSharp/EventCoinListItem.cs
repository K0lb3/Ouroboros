// Decompiled with JetBrains decompiler
// Type: EventCoinListItem
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

public class EventCoinListItem : MonoBehaviour
{
  [SerializeField]
  private GameObject button;

  public EventCoinListItem()
  {
    base.\u002Ector();
  }

  public ListItemEvents listItemEvents
  {
    get
    {
      return (ListItemEvents) ((Component) this).GetComponent<ListItemEvents>();
    }
  }

  public GameObject Button
  {
    get
    {
      return this.button;
    }
  }

  public void Set(bool isPeriod, bool isRead, long post_at, long read)
  {
    if (isRead)
      this.button.get_gameObject().SetActive(false);
    else
      this.button.get_gameObject().SetActive(true);
  }
}
