// Decompiled with JetBrains decompiler
// Type: SRPG.SceneStartup
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

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
      // ISSUE: reference to a compiler-generated field
      if (SceneStartup.\u003C\u003Ef__am\u0024cache2 == null)
      {
        // ISSUE: reference to a compiler-generated field
        // ISSUE: method pointer
        SceneStartup.\u003C\u003Ef__am\u0024cache2 = new Application.AdvertisingIdentifierCallback((object) null, __methodptr(\u003CAwake\u003Em__1DA));
      }
      // ISSUE: reference to a compiler-generated field
      Application.RequestAdvertisingIdentifierAsync(SceneStartup.\u003C\u003Ef__am\u0024cache2);
      base.Awake();
      MonoSingleton<UrlScheme>.Instance.Ensure();
      MonoSingleton<PaymentManager>.Instance.Ensure();
      MonoSingleton<NetworkError>.Instance.Ensure();
      MonoSingleton<WatchManager>.Instance.Ensure();
      Object.DontDestroyOnLoad(Object.Instantiate(Resources.Load("fillalpha"), Vector3.get_zero(), Quaternion.get_identity()));
      TextAsset textAsset = (TextAsset) Resources.Load<TextAsset>("appserveraddr");
      if (!Object.op_Inequality((Object) textAsset, (Object) null))
        return;
      Network.SetDefaultHostConfigured(textAsset.get_text());
    }

    [DebuggerHidden]
    private IEnumerator Start()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new SceneStartup.\u003CStart\u003Ec__Iterator5D() { \u003C\u003Ef__this = this };
    }
  }
}
