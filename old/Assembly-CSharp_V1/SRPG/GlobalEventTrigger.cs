// Decompiled with JetBrains decompiler
// Type: SRPG.GlobalEventTrigger
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [DisallowMultipleComponent]
  [AddComponentMenu("Event/Global Event Trigger")]
  public class GlobalEventTrigger : MonoBehaviour
  {
    public GlobalEventTrigger()
    {
      base.\u002Ector();
    }

    public void Trigger(string eventName)
    {
      GlobalEvent.Invoke(eventName, (object) ((Component) this).get_gameObject());
    }
  }
}
