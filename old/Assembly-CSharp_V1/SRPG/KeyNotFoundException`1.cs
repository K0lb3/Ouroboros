// Decompiled with JetBrains decompiler
// Type: SRPG.KeyNotFoundException`1
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
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
