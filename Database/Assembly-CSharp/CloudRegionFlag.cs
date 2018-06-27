// Decompiled with JetBrains decompiler
// Type: CloudRegionFlag
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;

[Flags]
public enum CloudRegionFlag
{
  eu = 1,
  us = 2,
  asia = 4,
  jp = 8,
  au = 16, // 0x00000010
  usw = 32, // 0x00000020
  sa = 64, // 0x00000040
  cae = 128, // 0x00000080
  kr = 256, // 0x00000100
  @in = 512, // 0x00000200
}
