// Decompiled with JetBrains decompiler
// Type: VideoAdManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
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
    if (Advertisement.get_isInitialized())
      return;
    Advertisement.Initialize(str, flag);
  }
}
