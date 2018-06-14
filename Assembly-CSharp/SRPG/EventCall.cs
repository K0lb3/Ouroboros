// Decompiled with JetBrains decompiler
// Type: SRPG.EventCall
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using UnityEngine;
using UnityEngine.Events;

namespace SRPG
{
  public class EventCall : MonoBehaviour
  {
    public EventCall.CustomEvent m_Events;
    public static object currentValue;

    public EventCall()
    {
      base.\u002Ector();
    }

    public void Invoke(string key, string value)
    {
      EventCall.currentValue = (object) null;
      this.m_Events.Invoke(key, value);
    }

    public void Invoke(string key, string value, object obj)
    {
      EventCall.currentValue = obj;
      this.m_Events.Invoke(key, value);
    }

    [Serializable]
    public class CustomEvent : UnityEvent<string, string>
    {
      public CustomEvent()
      {
        base.\u002Ector();
      }
    }
  }
}
