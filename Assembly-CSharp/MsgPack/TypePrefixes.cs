// Decompiled with JetBrains decompiler
// Type: MsgPack.TypePrefixes
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

namespace MsgPack
{
  public enum TypePrefixes : byte
  {
    PositiveFixNum = 0,
    FixMap = 128, // 0x80
    FixArray = 144, // 0x90
    FixRaw = 160, // 0xA0
    Nil = 192, // 0xC0
    False = 194, // 0xC2
    True = 195, // 0xC3
    Float = 202, // 0xCA
    Double = 203, // 0xCB
    UInt8 = 204, // 0xCC
    UInt16 = 205, // 0xCD
    UInt32 = 206, // 0xCE
    UInt64 = 207, // 0xCF
    Int8 = 208, // 0xD0
    Int16 = 209, // 0xD1
    Int32 = 210, // 0xD2
    Int64 = 211, // 0xD3
    Raw16 = 218, // 0xDA
    Raw32 = 219, // 0xDB
    Array16 = 220, // 0xDC
    Array32 = 221, // 0xDD
    Map16 = 222, // 0xDE
    Map32 = 223, // 0xDF
    NegativeFixNum = 224, // 0xE0
  }
}
