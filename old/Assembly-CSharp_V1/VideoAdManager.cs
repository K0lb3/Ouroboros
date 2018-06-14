// Decompiled with JetBrains decompiler
// Type: VideoAdManager
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.Advertisements;

public class VideoAdManager : MonoBehaviour
{
  public VideoAdManager()
  {
    base.\u002Ector();
  }

  public static void Init()
  {
    VideoAdManager.InitialiseAds();
  }

  private static void InitialiseAds()
  {
    string str = "1476714";
    bool flag = false;
    if (!Advertisement.get_isInitialized())
    {
      Advertisement.Initialize(str, flag);
      DebugUtility.Log("Initialising Unity Ads: " + Advertisement.get_version() + "-- AdID: " + str + " -- IsTestAds: " + (object) flag);
    }
    else
      DebugUtility.Log("Ads somehow already initialised: " + Advertisement.get_version() + "-- AdID: " + str + " -- IsTestAds: " + (object) flag);
  }
}
