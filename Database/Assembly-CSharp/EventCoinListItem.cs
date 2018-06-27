// Decompiled with JetBrains decompiler
// Type: EventCoinListItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
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
