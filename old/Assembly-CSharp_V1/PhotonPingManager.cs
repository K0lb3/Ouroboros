// Decompiled with JetBrains decompiler
// Type: PhotonPingManager
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using UnityEngine;

public class PhotonPingManager
{
  public static int Attempts = 5;
  public static bool IgnoreInitialAttempt = true;
  public static int MaxMilliseconsPerPing = 800;
  public bool UseNative;
  private int PingsRunning;

  public Region BestRegion
  {
    get
    {
      Region region = (Region) null;
      int num = int.MaxValue;
      using (List<Region>.Enumerator enumerator = PhotonNetwork.networkingPeer.AvailableRegions.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          Region current = enumerator.Current;
          Debug.Log((object) ("BestRegion checks region: " + (object) current));
          if (current.Ping != 0 && current.Ping < num)
          {
            num = current.Ping;
            region = current;
          }
        }
      }
      return region;
    }
  }

  public bool Done
  {
    get
    {
      return this.PingsRunning == 0;
    }
  }

  [DebuggerHidden]
  public IEnumerator PingSocket(Region region)
  {
    // ISSUE: object of a compiler-generated type is created
    return (IEnumerator) new PhotonPingManager.\u003CPingSocket\u003Ec__Iterator9() { region = region, \u003C\u0024\u003Eregion = region, \u003C\u003Ef__this = this };
  }

  public static string ResolveHost(string hostName)
  {
    string empty = string.Empty;
    try
    {
      IPAddress[] hostAddresses = Dns.GetHostAddresses(hostName);
      if (hostAddresses.Length == 1)
        return hostAddresses[0].ToString();
      for (int index = 0; index < hostAddresses.Length; ++index)
      {
        IPAddress ipAddress = hostAddresses[index];
        if (ipAddress != null)
        {
          if (ipAddress.ToString().Contains(":"))
            return ipAddress.ToString();
          if (string.IsNullOrEmpty(empty))
            empty = hostAddresses.ToString();
        }
      }
    }
    catch (Exception ex)
    {
      Debug.Log((object) ("Exception caught! " + ex.Source + " Message: " + ex.Message));
    }
    return empty;
  }
}
