// Decompiled with JetBrains decompiler
// Type: EventQuit
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.Events;

public class EventQuit : MonoBehaviour
{
  public EventQuit()
  {
    base.\u002Ector();
  }

  private static EventQuit Instance { get; set; }

  public static EventQuit Find()
  {
    return EventQuit.Instance;
  }

  public UnityAction OnClick { private get; set; }

  public void Quit()
  {
    if (this.OnClick == null)
      return;
    this.OnClick.Invoke();
  }

  private void Awake()
  {
    if (Object.op_Inequality((Object) null, (Object) EventQuit.Instance))
      Object.Destroy((Object) this);
    EventQuit.Instance = this;
  }

  private void OnDestroy()
  {
    if (!Object.op_Equality((Object) this, (Object) EventQuit.Instance))
      return;
    EventQuit.Instance = (EventQuit) null;
  }

  private void Update()
  {
  }
}
