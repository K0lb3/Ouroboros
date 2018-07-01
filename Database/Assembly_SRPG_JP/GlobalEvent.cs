// Decompiled with JetBrains decompiler
// Type: SRPG.GlobalEvent
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

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
