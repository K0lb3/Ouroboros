// Decompiled with JetBrains decompiler
// Type: SRPG.GlobalEvent
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;

namespace SRPG
{
  public class GlobalEvent
  {
    private static Dictionary<string, GlobalEvent.Delegate> mListeners = new Dictionary<string, GlobalEvent.Delegate>();

    public static void AddListener(string eventName, GlobalEvent.Delegate callback)
    {
      if (GlobalEvent.mListeners.ContainsKey(eventName))
        GlobalEvent.mListeners[eventName] += callback;
      else
        GlobalEvent.mListeners[eventName] = callback;
    }

    public static void RemoveListener(string eventName, GlobalEvent.Delegate callback)
    {
      if (!GlobalEvent.mListeners.ContainsKey(eventName))
        return;
      GlobalEvent.mListeners[eventName] -= callback;
      if (GlobalEvent.mListeners[eventName] != null)
        return;
      GlobalEvent.mListeners.Remove(eventName);
    }

    public static void Invoke(string eventName, object param)
    {
      if (!GlobalEvent.mListeners.ContainsKey(eventName))
        return;
      GlobalEvent.mListeners[eventName](param);
    }

    public delegate void Delegate(object caller);
  }
}
