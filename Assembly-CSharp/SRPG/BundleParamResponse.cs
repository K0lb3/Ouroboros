// Decompiled with JetBrains decompiler
// Type: SRPG.BundleParamResponse
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System.Collections.Generic;

namespace SRPG
{
  public class BundleParamResponse
  {
    public List<BundleParam> bundles = new List<BundleParam>();

    public bool Deserialize(JSON_BundleParamResponse json)
    {
      if (json == null || json.bundles == null)
        return true;
      this.bundles.Clear();
      for (int index = 0; index < json.bundles.Length; ++index)
      {
        BundleParam bundleParam = new BundleParam();
        if (!bundleParam.Deserialize(json.bundles[index]))
          return false;
        this.bundles.Add(bundleParam);
      }
      MonoSingleton<GameManager>.Instance.Player.SetBundleParam(this.bundles);
      return true;
    }
  }
}
