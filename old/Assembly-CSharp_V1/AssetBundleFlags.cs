// Decompiled with JetBrains decompiler
// Type: AssetBundleFlags
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;

[Flags]
public enum AssetBundleFlags
{
  Compressed = 1,
  RawData = 2,
  Required = 4,
  Scene = 8,
  Tutorial = 16, // 0x00000010
  Multiplay = 32, // 0x00000020
  StreamingAsset = 64, // 0x00000040
  TutorialMovie = 128, // 0x00000080
  Persistent = 256, // 0x00000100
  IsLanguage = 512, // 0x00000200
  IsCombined = 1024, // 0x00000400
  IsFolder = 2048, // 0x00000800
}
