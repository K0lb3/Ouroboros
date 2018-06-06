// Decompiled with JetBrains decompiler
// Type: FPSDisplay
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

public class FPSDisplay : MonoBehaviour
{
  private float mDeltaTime;
  public Text FPS;

  public FPSDisplay()
  {
    base.\u002Ector();
  }

  private void Update()
  {
    this.mDeltaTime += (float) (((double) Time.get_unscaledDeltaTime() - (double) this.mDeltaTime) * 0.100000001490116);
    string str = string.Empty;
    if (GameUtility.IsDebugBuild)
      str = string.Format("{0:0.00} ms\nAssets:{1}/{2}", (object) (this.mDeltaTime * 1000f), (object) AssetManager.GetLoadedAssetNames().Length, (object) AssetManager.GetOpenedAssetBundleNames().Length);
    this.FPS.set_text(str);
  }
}
