// Decompiled with JetBrains decompiler
// Type: FPSDisplay
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
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

  private void Start()
  {
    Object.Destroy((Object) ((Component) this).get_gameObject());
  }

  private void Update()
  {
    this.mDeltaTime += (float) (((double) Time.get_unscaledDeltaTime() - (double) this.mDeltaTime) * 0.100000001490116);
    string empty = string.Empty;
    float num1 = this.mDeltaTime * 1000f;
    string str1 = "00ff00";
    if ((double) num1 >= 50.0)
      str1 = "ff0000";
    else if ((double) num1 >= 33.0)
      str1 = "ffff00";
    string str2 = string.Format("<color=#{0}>{1:0.00} ms</color>", (object) str1, (object) num1) + string.Format("\nAssets:{0}/{1}", (object) AssetManager.GetLoadedAssetNames().Length, (object) AssetManager.GetOpenedAssetBundleNames().Length);
    float num2 = 1048576f;
    uint monoUsedSize = Profiler.GetMonoUsedSize();
    uint monoHeapSize = Profiler.GetMonoHeapSize();
    uint totalAllocatedMemory = Profiler.GetTotalAllocatedMemory();
    uint totalReservedMemory = Profiler.GetTotalReservedMemory();
    string str3 = monoHeapSize + totalReservedMemory <= 314572800U ? (monoHeapSize + totalReservedMemory <= 262144000U ? "00ff00" : "ffff00") : "ff0000";
    string str4 = str2 + string.Format("\n<color=#{2}>Mono:{0}/{1}MB</color>", (object) ((float) monoUsedSize / num2).ToString("F2"), (object) ((float) monoHeapSize / num2).ToString("F2"), (object) str3) + string.Format("\n<color=#{2}>Unity:{0}/{1}MB</color>", (object) ((float) totalAllocatedMemory / num2).ToString("F2"), (object) ((float) totalReservedMemory / num2).ToString("F2"), (object) str3);
    if (!AssetDownloader.isDone)
      str4 += string.Format("\n<color=#{2}>Current Download:{0}/{1}MB</color>", (object) AssetDownloader.CurrentDownloadSize.ToString("F2"), (object) AssetDownloader.TotalDownloadSize.ToString("F2"), (object) str3);
    this.FPS.set_text(str4);
  }
}
