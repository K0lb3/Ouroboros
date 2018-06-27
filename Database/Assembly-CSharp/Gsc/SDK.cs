// Decompiled with JetBrains decompiler
// Type: Gsc.SDK
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using DeviceKit;
using Gsc.Auth;
using Gsc.Core;
using Gsc.Device;
using Gsc.Network;
using Gsc.Purchase;
using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;

namespace Gsc
{
  public static class SDK
  {
    public static bool Initialized { get; private set; }

    public static bool hasError { get; private set; }

    public static Configuration Configuration { get; private set; }

    public static Coroutine Initialize<T>(Configuration.Builder<T> confBuilder, string specifiedEnv = null) where T : struct, Configuration.IEnvironment
    {
      SDK.hasError = true;
      SDK._PreInit(confBuilder.webQueueObserver);
      Gsc.Auth.Device.Initialize();
      LogKit.Logger.SetPlatform(Gsc.Auth.Device.Platform);
      return RootObject.Instance.StartCoroutine(SDK.Initializer<T>.Initialize(confBuilder, specifiedEnv));
    }

    public static void Reset()
    {
      SDK.Initialized = false;
      if (SDK.Configuration.accountManager != null)
        SDK.Configuration.accountManager.Reset();
      if (SDK.Configuration.webQueueObserver != null)
        SDK.Configuration.webQueueObserver.Reset();
      SDK._PreInit(SDK.Configuration.webQueueObserver);
      SDK._PostInit(SDK.Configuration);
    }

    private static void _PreInit(IWebQueueObserver webQueueObserver)
    {
      Gsc.Core.Logger.Init();
      UnityErrorLogSender.Initialize();
      RootObject.Initialize();
      WebQueue.Init(webQueueObserver);
    }

    private static void _PostInit(Configuration conf)
    {
      SDK.Configuration = conf;
      Session.Init(conf.EnvName, AccountManager.Create(conf.accountManager));
      LogKit.Logger.SetServerUrl(conf.Env.LogCollectionUrl);
      LogKit.Logger.SetDeviceIdGetter((Func<string>) (() => Session.DefaultSession.DeviceID));
      LogKit.Logger.Init(conf.AppName, Path.applicationDataPath + "/_logs", ((Component) NativeRootObject.Instance).get_gameObject());
      App.Hardkey.Init(((Component) NativeRootObject.Instance).get_gameObject());
      PurchaseHandler.Initialize();
      SDK.Initialized = true;
    }

    private static class Initializer<T> where T : struct, Configuration.IEnvironment
    {
      [DebuggerHidden]
      public static IEnumerator Initialize(Configuration.Builder<T> confBuilder, string specifiedEnv)
      {
        // ISSUE: object of a compiler-generated type is created
        return (IEnumerator) new SDK.Initializer<T>.\u003CInitialize\u003Ec__Iterator1C() { confBuilder = confBuilder, specifiedEnv = specifiedEnv, \u003C\u0024\u003EconfBuilder = confBuilder, \u003C\u0024\u003EspecifiedEnv = specifiedEnv };
      }

      [DebuggerHidden]
      private static IEnumerator BuildConfigration(Configuration.Builder<T> confBuilder, string specifiedEnv)
      {
        // ISSUE: object of a compiler-generated type is created
        return (IEnumerator) new SDK.Initializer<T>.\u003CBuildConfigration\u003Ec__Iterator1D() { specifiedEnv = specifiedEnv, confBuilder = confBuilder, \u003C\u0024\u003EspecifiedEnv = specifiedEnv, \u003C\u0024\u003EconfBuilder = confBuilder };
      }

      private static void SelectedEnvironment(Configuration.Builder<T> confBuilder, string envName, Configuration.IEnvironment env)
      {
        SDK.hasError = false;
        Application.set_runInBackground(true);
        SDK._PostInit(new Configuration((Configuration.IBuilder) confBuilder, envName, env));
      }
    }

    public class BootLoader : MonoBehaviour
    {
      private static SDK.BootLoader currentLoader;
      private IEnumerator enumerator;

      public BootLoader()
      {
        base.\u002Ector();
      }

      public static void Run(IEnumerator enumerator)
      {
        if (Object.op_Inequality((Object) SDK.BootLoader.currentLoader, (Object) null))
          Object.Destroy((Object) ((Component) SDK.BootLoader.currentLoader).get_gameObject());
        GameObject gameObject = new GameObject("Gsc.SDK.Bootloader");
        ((Object) gameObject).set_hideFlags((HideFlags) 52);
        Object.DontDestroyOnLoad((Object) gameObject);
        SDK.BootLoader.currentLoader = (SDK.BootLoader) gameObject.AddComponent<SDK.BootLoader>();
        SDK.BootLoader.currentLoader.enumerator = enumerator;
      }

      [DebuggerHidden]
      private IEnumerator Start()
      {
        // ISSUE: object of a compiler-generated type is created
        return (IEnumerator) new SDK.BootLoader.\u003CStart\u003Ec__Iterator1E() { \u003C\u003Ef__this = this };
      }
    }
  }
}
