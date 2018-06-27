// Decompiled with JetBrains decompiler
// Type: Gsc.Configuration
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using Gsc.Device;
using Gsc.Network;
using System.Collections.Generic;

namespace Gsc
{
  public struct Configuration
  {
    public readonly string AppName;
    public readonly string EnvName;
    public readonly Configuration.IEnvironment Env;
    public readonly IAccountManager accountManager;
    public readonly IWebQueueObserver webQueueObserver;

    public Configuration(Configuration.IBuilder builder, string envName, Configuration.IEnvironment env)
    {
      this.AppName = builder.name;
      this.EnvName = envName;
      this.Env = env;
      this.accountManager = builder.accountManager;
      this.webQueueObserver = builder.webQueueObserver;
    }

    public T GetEnv<T>() where T : struct, Configuration.IEnvironment
    {
      return (T) this.Env;
    }

    public interface IEnvironment
    {
      string ServerUrl { get; }

      string NativeBaseUrl { get; }

      string LogCollectionUrl { get; }

      string ClientErrorApi { get; }

      string AuthApiPrefix { get; }

      string PurchaseApiPrefix { get; }

      string BundlePurchaseApiPrefix { get; }

      void SetValue(string key, string value);
    }

    public interface IBuilder
    {
      string name { get; }

      IAccountManager accountManager { get; }

      IWebQueueObserver webQueueObserver { get; }
    }

    public class Builder<T> : Configuration.IBuilder where T : struct, Configuration.IEnvironment
    {
      public string envUrl;
      public Dictionary<string, Configuration.IEnvironment> envCollection;

      public string name { get; private set; }

      public string version { get; private set; }

      public IAccountManager accountManager { get; private set; }

      public IWebQueueObserver webQueueObserver { get; private set; }

      public Configuration.Builder<T> SetApplicationName(string name)
      {
        this.name = name;
        return this;
      }

      public Configuration.Builder<T> SetApplicationVersion(string version)
      {
        this.version = version;
        return this;
      }

      public Configuration.Builder<T> SetEnvironment(string url)
      {
        this.envUrl = url;
        return this;
      }

      public Configuration.Builder<T> AddEnvironment(string label, T env)
      {
        if (this.envCollection == null)
          this.envCollection = new Dictionary<string, Configuration.IEnvironment>();
        this.envCollection.Add(label, (Configuration.IEnvironment) env);
        return this;
      }

      public Configuration.Builder<T> SetWebQueueObserver(IWebQueueObserver observer)
      {
        this.webQueueObserver = observer;
        return this;
      }

      public Configuration.Builder<T> SetAccountManager(IAccountManager accountManager)
      {
        this.accountManager = accountManager;
        return this;
      }
    }
  }
}
