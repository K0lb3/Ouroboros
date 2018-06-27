// Decompiled with JetBrains decompiler
// Type: TapjoyUnity.Internal.TapjoyUnityInit
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace TapjoyUnity.Internal
{
  public sealed class TapjoyUnityInit : MonoBehaviour
  {
    private static bool initialized;

    public TapjoyUnityInit()
    {
      base.\u002Ector();
    }

    private void Awake()
    {
      if (TapjoyUnityInit.initialized)
        return;
      TapjoyUnityInit.initialized = true;
      ApiBindingAndroid.Install();
    }
  }
}
