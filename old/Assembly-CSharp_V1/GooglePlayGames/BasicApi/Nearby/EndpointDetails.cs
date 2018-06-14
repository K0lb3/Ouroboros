// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.BasicApi.Nearby.EndpointDetails
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GooglePlayGames.OurUtils;

namespace GooglePlayGames.BasicApi.Nearby
{
  public struct EndpointDetails
  {
    private readonly string mEndpointId;
    private readonly string mDeviceId;
    private readonly string mName;
    private readonly string mServiceId;

    public EndpointDetails(string endpointId, string deviceId, string name, string serviceId)
    {
      this.mEndpointId = Misc.CheckNotNull<string>(endpointId);
      this.mDeviceId = Misc.CheckNotNull<string>(deviceId);
      this.mName = Misc.CheckNotNull<string>(name);
      this.mServiceId = Misc.CheckNotNull<string>(serviceId);
    }

    public string EndpointId
    {
      get
      {
        return this.mEndpointId;
      }
    }

    public string DeviceId
    {
      get
      {
        return this.mDeviceId;
      }
    }

    public string Name
    {
      get
      {
        return this.mName;
      }
    }

    public string ServiceId
    {
      get
      {
        return this.mServiceId;
      }
    }
  }
}
