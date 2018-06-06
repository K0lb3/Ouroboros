// Decompiled with JetBrains decompiler
// Type: SRPG.GiftTypes
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public enum GiftTypes : long
  {
    Item = 1,
    Gold = 2,
    Coin = 4,
    ArenaCoin = 8,
    MultiCoin = 16, // 0x0000000000000010
    KakeraCoin = 32, // 0x0000000000000020
    Artifact = 64, // 0x0000000000000040
    Unit = 128, // 0x0000000000000080
    SelectUnitItem = 256, // 0x0000000000000100
    SelectItem = 512, // 0x0000000000000200
    SelectArtifactItem = 1024, // 0x0000000000000400
    Award = 2048, // 0x0000000000000800
  }
}
