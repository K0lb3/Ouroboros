// Decompiled with JetBrains decompiler
// Type: SRPG.SceneStartup
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System.Collections;
using System.Diagnostics;
using UnityEngine;

namespace SRPG
{
  [AddComponentMenu("Scripts/SRPG/Scene/Startup")]
  public class SceneStartup : Scene
  {
    public bool AutoStart = true;
    private const string Key_ClearCache = "CLEARCACHE";
    private static bool mResolutionChanged;

    private new void Awake()
    {
      base.Awake();
      MonoSingleton<UrlScheme>.Instance.Ensure();
      MonoSingleton<PaymentManager>.Instance.Ensure();
      MonoSingleton<NetworkError>.Instance.Ensure();
      MonoSingleton<WatchManager>.Instance.Ensure();
      TextAsset textAsset = (TextAsset) Resources.Load<TextAsset>("appserveraddr");
      if (!Object.op_Inequality((Object) textAsset, (Object) null))
        return;
      Network.SetDefaultHostConfigured(textAsset.get_text());
    }

    [DebuggerHidden]
    private IEnumerator Start()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new SceneStartup.\u003CStart\u003Ec__Iterator84()
      {
        \u003C\u003Ef__this = this
      };
    }
  }
}
