// Decompiled with JetBrains decompiler
// Type: EventBackGround
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public class EventBackGround : MonoBehaviour
{
  public static List<EventBackGround> Instances = new List<EventBackGround>();
  public bool mClose;

  public EventBackGround()
  {
    base.\u002Ector();
  }

  public static EventBackGround Find()
  {
    using (List<EventBackGround>.Enumerator enumerator = EventBackGround.Instances.GetEnumerator())
    {
      while (enumerator.MoveNext())
      {
        EventBackGround current = enumerator.Current;
        if (Object.op_Inequality((Object) current, (Object) null))
          return current;
      }
    }
    return (EventBackGround) null;
  }

  public static void DiscardAll()
  {
    using (List<EventBackGround>.Enumerator enumerator = EventBackGround.Instances.GetEnumerator())
    {
      while (enumerator.MoveNext())
      {
        EventBackGround current = enumerator.Current;
        if (!((Component) current).get_gameObject().get_activeInHierarchy())
          Object.Destroy((Object) ((Component) current).get_gameObject());
      }
    }
    EventBackGround.Instances.Clear();
  }

  private void Awake()
  {
    EventBackGround.Instances.Add(this);
  }

  private void OnDestroy()
  {
    EventBackGround.Instances.Remove(this);
  }

  public void Open()
  {
    ((Component) this).get_gameObject().SetActive(true);
    this.mClose = false;
  }

  public void Close()
  {
    ((Component) this).get_gameObject().SetActive(false);
    this.mClose = true;
  }
}
