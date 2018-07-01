// Decompiled with JetBrains decompiler
// Type: SRPG.GlobalEventTrigger
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

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
