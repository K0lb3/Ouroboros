// Decompiled with JetBrains decompiler
// Type: PropertyTypeFlag
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
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
