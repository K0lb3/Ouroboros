// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.Native.PInvoke.RealtimeRoomConfig
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GooglePlayGames.Native.Cwrapper;
using System;
using System.Runtime.InteropServices;

namespace GooglePlayGames.Native.PInvoke
{
  internal class RealtimeRoomConfig : BaseReferenceHolder
  {
    internal RealtimeRoomConfig(IntPtr selfPointer)
      : base(selfPointer)
    {
    }

    protected override void CallDispose(HandleRef selfPointer)
    {
      RealTimeRoomConfig.RealTimeRoomConfig_Dispose(selfPointer);
    }

    internal static RealtimeRoomConfig FromPointer(IntPtr selfPointer)
    {
      if (selfPointer.Equals((object) IntPtr.Zero))
        return (RealtimeRoomConfig) null;
      return new RealtimeRoomConfig(selfPointer);
    }
  }
}
