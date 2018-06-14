// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.BasicApi.Nearby.NearbyConnectionConfiguration
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GooglePlayGames.OurUtils;
using System;

namespace GooglePlayGames.BasicApi.Nearby
{
  public struct NearbyConnectionConfiguration
  {
    public const int MaxUnreliableMessagePayloadLength = 1168;
    public const int MaxReliableMessagePayloadLength = 4096;
    private readonly Action<InitializationStatus> mInitializationCallback;
    private readonly long mLocalClientId;

    public NearbyConnectionConfiguration(Action<InitializationStatus> callback, long localClientId)
    {
      this.mInitializationCallback = Misc.CheckNotNull<Action<InitializationStatus>>(callback);
      this.mLocalClientId = localClientId;
    }

    public long LocalClientId
    {
      get
      {
        return this.mLocalClientId;
      }
    }

    public Action<InitializationStatus> InitializationCallback
    {
      get
      {
        return this.mInitializationCallback;
      }
    }
  }
}
