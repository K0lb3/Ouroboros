// Decompiled with JetBrains decompiler
// Type: Gsc.Network.WebTaskAttribute
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;

namespace Gsc.Network
{
  [Flags]
  public enum WebTaskAttribute : uint
  {
    None = 0,
    Reliable = 1,
    Interrupt = 2,
    Silent = 4,
    Parallel = 8,
  }
}
