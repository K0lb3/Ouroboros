// Decompiled with JetBrains decompiler
// Type: SRPG.CriticalSections
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

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
