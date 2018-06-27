// Decompiled with JetBrains decompiler
// Type: GR.Singleton`1
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;

namespace GR
{
  public abstract class Singleton<T> where T : class, new()
  {
    private static T instance_;

    public static T Instance
    {
      get
      {
        if ((object) Singleton<T>.instance_ == null)
          Singleton<T>.instance_ = Activator.CreateInstance<T>();
        return Singleton<T>.instance_;
      }
    }
  }
}
