// Decompiled with JetBrains decompiler
// Type: SRPG.KeyNotFoundException`1
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;

namespace SRPG
{
  public class KeyNotFoundException<T> : Exception
  {
    public KeyNotFoundException(string key)
      : base(typeof (T).ToString() + " '" + key + "' doesn't exist.")
    {
    }
  }
}
