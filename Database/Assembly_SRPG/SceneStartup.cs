// Decompiled with JetBrains decompiler
// Type: SRPG.SceneStartup
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System;
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
        SceneStartup.\u003C\u003Ef__am\u0024cache2 = new Application.AdvertisingIdentifierCallback((object) null, __methodptr(\u003CAwake\u003Em__27D));
      }
      // ISSUE: reference to a compiler-generated field
      if (!Application.RequestAdvertisingIdentifierAsync(SceneStartup.\u003C\u003Ef__am\u0024cache2))
      {
        Debug.Log((object) "advertisingId not supported");
        string device_id = Guid.NewGuid().ToString();
        Debug.Log((object) device_id);
        GameManager.SetDeviceID(device_id);
      }
      base.Awake();
      MonoSingleton<UrlScheme>.Instance.Ensure();
      MonoSingleton<PaymentManager>.Instance.Ensure();
      MonoSingleton<NetworkError>.Instance.Ensure();
      UnityEngine.Object.DontDestroyOnLoad(UnityEngine.Object.Instantiate(Resources.Load("fillalpha"), Vector3.get_zero(), Quaternion.get_identity()));
      TextAsset textAsset = (TextAsset) Resources.Load<TextAsset>("appserveraddr");
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) textAsset, (UnityEngine.Object) null))
        return;
      Network.SetDefaultHostConfigured(textAsset.get_text());
    }

    [DebuggerHidden]
    private IEnumerator Start()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new SceneStartup.\u003CStart\u003Ec__Iterator96() { \u003C\u003Ef__this = this };
    }
  }
}
