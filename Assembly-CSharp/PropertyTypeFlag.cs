// Decompiled with JetBrains decompiler
// Type: PropertyTypeFlag
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;

[Flags]
public enum PropertyTypeFlag : byte
{
  None = 0,
  Game = 1,
  Actor = 2,
  GameAndActor = Actor | Game, // 0x03
}
