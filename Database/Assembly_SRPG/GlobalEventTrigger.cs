// Decompiled with JetBrains decompiler
// Type: SRPG.GlobalEventTrigger
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
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
