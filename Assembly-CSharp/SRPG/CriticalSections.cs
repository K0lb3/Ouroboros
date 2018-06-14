// Decompiled with JetBrains decompiler
// Type: SRPG.CriticalSections
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;

namespace SRPG
{
  [Flags]
  public enum CriticalSections
  {
    Default = 1,
    Network = 2,
    SceneChange = 4,
    ExDownload = 8,
  }
}
