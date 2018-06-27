// Decompiled with JetBrains decompiler
// Type: Gsc.Support.SupportExtentions
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;

namespace Gsc.Support
{
  public static class SupportExtentions
  {
    public static T Get<T>(this WeakReference self)
    {
      if (self != null && self.IsAlive)
        return (T) self.Target;
      return default (T);
    }

    public static bool TryGet<T>(this WeakReference self, out T value)
    {
      if (self != null && self.IsAlive)
      {
        value = (T) self.Target;
        return true;
      }
      value = default (T);
      return false;
    }

    public static WeakReference Set<T>(this WeakReference self, T obj)
    {
      if (self == null)
        self = new WeakReference((object) obj);
      else
        self.Target = (object) obj;
      return self;
    }
  }
}
