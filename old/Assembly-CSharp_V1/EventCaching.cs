// Decompiled with JetBrains decompiler
// Type: EventCaching
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;

public enum EventCaching : byte
{
  DoNotCache = 0,
  [Obsolete] MergeCache = 1,
  [Obsolete] ReplaceCache = 2,
  [Obsolete] RemoveCache = 3,
  AddToRoomCache = 4,
  AddToRoomCacheGlobal = 5,
  RemoveFromRoomCache = 6,
  RemoveFromRoomCacheForActorsLeft = 7,
  SliceIncreaseIndex = 10, // 0x0A
  SliceSetIndex = 11, // 0x0B
  SlicePurgeIndex = 12, // 0x0C
  SlicePurgeUpToIndex = 13, // 0x0D
}
