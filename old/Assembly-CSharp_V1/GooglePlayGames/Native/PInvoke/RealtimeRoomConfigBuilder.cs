// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.Native.PInvoke.RealtimeRoomConfigBuilder
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GooglePlayGames.Native.Cwrapper;
using System;
using System.Runtime.InteropServices;

namespace GooglePlayGames.Native.PInvoke
{
  internal class RealtimeRoomConfigBuilder : BaseReferenceHolder
  {
    internal RealtimeRoomConfigBuilder(IntPtr selfPointer)
      : base(selfPointer)
    {
    }

    internal RealtimeRoomConfigBuilder PopulateFromUIResponse(PlayerSelectUIResponse response)
    {
      RealTimeRoomConfigBuilder.RealTimeRoomConfig_Builder_PopulateFromPlayerSelectUIResponse(this.SelfPtr(), response.AsPointer());
      return this;
    }

    internal RealtimeRoomConfigBuilder SetVariant(uint variantValue)
    {
      RealTimeRoomConfigBuilder.RealTimeRoomConfig_Builder_SetVariant(this.SelfPtr(), (int) variantValue != 0 ? variantValue : uint.MaxValue);
      return this;
    }

    internal RealtimeRoomConfigBuilder AddInvitedPlayer(string playerId)
    {
      RealTimeRoomConfigBuilder.RealTimeRoomConfig_Builder_AddPlayerToInvite(this.SelfPtr(), playerId);
      return this;
    }

    internal RealtimeRoomConfigBuilder SetExclusiveBitMask(ulong bitmask)
    {
      RealTimeRoomConfigBuilder.RealTimeRoomConfig_Builder_SetExclusiveBitMask(this.SelfPtr(), bitmask);
      return this;
    }

    internal RealtimeRoomConfigBuilder SetMinimumAutomatchingPlayers(uint minimum)
    {
      RealTimeRoomConfigBuilder.RealTimeRoomConfig_Builder_SetMinimumAutomatchingPlayers(this.SelfPtr(), minimum);
      return this;
    }

    internal RealtimeRoomConfigBuilder SetMaximumAutomatchingPlayers(uint maximum)
    {
      RealTimeRoomConfigBuilder.RealTimeRoomConfig_Builder_SetMaximumAutomatchingPlayers(this.SelfPtr(), maximum);
      return this;
    }

    internal RealtimeRoomConfig Build()
    {
      return new RealtimeRoomConfig(RealTimeRoomConfigBuilder.RealTimeRoomConfig_Builder_Create(this.SelfPtr()));
    }

    protected override void CallDispose(HandleRef selfPointer)
    {
      RealTimeRoomConfigBuilder.RealTimeRoomConfig_Builder_Dispose(selfPointer);
    }

    internal static RealtimeRoomConfigBuilder Create()
    {
      return new RealtimeRoomConfigBuilder(RealTimeRoomConfigBuilder.RealTimeRoomConfig_Builder_Construct());
    }
  }
}
