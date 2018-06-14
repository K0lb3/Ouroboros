// Decompiled with JetBrains decompiler
// Type: Gsc.App.BootLoader
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using Gsc.App.NetworkHelper;
using System.Collections;
using System.Diagnostics;
using UnityEngine;

namespace Gsc.App
{
  public static class BootLoader
  {
    private static AccountManager mAccountManager = new AccountManager();

    public static BootLoader.BootState BootStates { get; set; }

    [RuntimeInitializeOnLoadMethod]
    private static void OnBoot()
    {
      BootLoader.BootStates = BootLoader.BootState.AWAKE;
    }

    public static void GscInit()
    {
      if (BootLoader.BootStates != BootLoader.BootState.AWAKE)
        return;
      SDK.BootLoader.Run(BootLoader.InitializeApplication());
    }

    public static void Reboot()
    {
      SDK.Reset();
      GsccBridge.Reset();
    }

    public static AccountManager GetAccountManager()
    {
      return BootLoader.mAccountManager;
    }

    [DebuggerHidden]
    private static IEnumerator InitializeApplication()
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      BootLoader.\u003CInitializeApplication\u003Ec__IteratorD8 applicationCIteratorD8 = new BootLoader.\u003CInitializeApplication\u003Ec__IteratorD8();
      return (IEnumerator) applicationCIteratorD8;
    }

    public enum BootState
    {
      AWAKE,
      SUCCESS,
      FAILED,
    }
  }
}
