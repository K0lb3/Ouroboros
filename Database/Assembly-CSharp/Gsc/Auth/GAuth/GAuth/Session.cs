// Decompiled with JetBrains decompiler
// Type: Gsc.Auth.GAuth.GAuth.Session
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using Gsc.Device;
using Gsc.Network;
using Gsc.Tasks;
using MiniJSON;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace Gsc.Auth.GAuth.GAuth
{
  public class Session : ISession
  {
    private static readonly string userAgentCache = Json.Serialize((object) new Dictionary<string, object>() { { "device_model", (object) DeviceInfo.DeviceModel }, { "device_vendor", (object) DeviceInfo.DeviceVendor }, { "os_info", (object) DeviceInfo.OperatingSystem }, { "cpu_info", (object) DeviceInfo.ProcessorType }, { "memory_size", (object) (((double) (DeviceInfo.SystemMemorySize >> 10) / 1000.0).ToString() + "GB") } });
    public readonly string EnvName;
    private readonly IAccountManager accountManager;

    public Session(string envName, IAccountManager accountManager)
    {
      this.EnvName = envName;
      this.accountManager = accountManager;
    }

    public string AccessToken { get; protected set; }

    public virtual string DeviceID
    {
      get
      {
        return this.accountManager.GetDeviceId(this.EnvName);
      }
    }

    public virtual string SecretKey
    {
      get
      {
        return this.accountManager.GetSecretKey(this.EnvName);
      }
    }

    public virtual string UserAgent
    {
      get
      {
        return Session.userAgentCache;
      }
    }

    public void DeleteAuthKeys()
    {
      this.accountManager.Remove(this.EnvName);
    }

    public virtual bool CanRefreshToken(Type requestType)
    {
      return !requestType.Equals(typeof (Gsc.Auth.GAuth.GAuth.API.Request.AccessToken));
    }

    public virtual IRefreshTokenTask GetRefreshTokenTask()
    {
      return (IRefreshTokenTask) new Session.RefreshTokenTask(this);
    }

    public virtual IWebTask RegisterEmailAddressAndPassword(string email, string password, bool disableValicationEmail, Action<RegisterEmailAddressAndPasswordResult> callback)
    {
      return (IWebTask) new Gsc.Auth.GAuth.GAuth.API.Request.RegisterEmailAddressAndPassword(this.DeviceID, this.SecretKey, email, password) { DisableValidationEmail = disableValicationEmail }.Send().OnResponse((VoidCallbackWithError<Gsc.Auth.GAuth.GAuth.API.Response.RegisterEmailAddressAndPassword>) ((response, error) => callback(Session.GetRegisterEmailAddressWithPasswordResult(response, (Gsc.Auth.GAuth.GAuth.API.Response.ErrorResponse) error))));
    }

    private static RegisterEmailAddressAndPasswordResult GetRegisterEmailAddressWithPasswordResult(Gsc.Auth.GAuth.GAuth.API.Response.RegisterEmailAddressAndPassword response, Gsc.Auth.GAuth.GAuth.API.Response.ErrorResponse error)
    {
      if (error == null)
        return new RegisterEmailAddressAndPasswordResult(RegisterEmailAddressAndPasswordResultCode.Success);
      RegisterEmailAddressAndPasswordResultCode resultCode = RegisterEmailAddressAndPasswordResultCode.UnknownError;
      string errorCode = error.ErrorCode;
      if (errorCode != null)
      {
        // ISSUE: reference to a compiler-generated field
        if (Session.\u003C\u003Ef__switch\u0024map1 == null)
        {
          // ISSUE: reference to a compiler-generated field
          Session.\u003C\u003Ef__switch\u0024map1 = new Dictionary<string, int>(3)
          {
            {
              "000",
              0
            },
            {
              "001",
              1
            },
            {
              "002",
              2
            }
          };
        }
        int num;
        // ISSUE: reference to a compiler-generated field
        if (Session.\u003C\u003Ef__switch\u0024map1.TryGetValue(errorCode, out num))
        {
          switch (num)
          {
            case 0:
              resultCode = RegisterEmailAddressAndPasswordResultCode.InvalidEmailAddress;
              goto label_14;
            case 1:
              resultCode = RegisterEmailAddressAndPasswordResultCode.InvalidPassword;
              goto label_14;
            case 2:
              resultCode = RegisterEmailAddressAndPasswordResultCode.DuplicatedEmailAddress;
              goto label_14;
          }
        }
      }
      if (error.data.Root.GetValueByPointer("/reason/email", (string) null) != null)
        resultCode = RegisterEmailAddressAndPasswordResultCode.InvalidEmailAddress;
      else if (error.data.Root.GetValueByPointer("/reason/password", (string) null) != null)
        resultCode = RegisterEmailAddressAndPasswordResultCode.InvalidPassword;
label_14:
      return new RegisterEmailAddressAndPasswordResult(resultCode);
    }

    public virtual IWebTask AddDeviceWithEmailAddressAndPassword(string email, string password, Action<AddDeviceWithEmailAddressAndPasswordResult> callback)
    {
      return (IWebTask) new Gsc.Auth.GAuth.GAuth.API.Request.AddDeviceWithEmailAddressAndPassword(email, password) { Idfv = Gsc.Auth.GAuth.GAuth.Device.Instance.ID }.Send().OnResponse((VoidCallbackWithError<Gsc.Auth.GAuth.GAuth.API.Response.AddDeviceWithEmailAddressAndPassword>) ((response, error) =>
      {
        AddDeviceWithEmailAddressAndPasswordResult addressAndPassword = Session.GetAddDeviceWithEmailAddressAndPassword((Gsc.Auth.GAuth.GAuth.API.Response.ErrorResponse) error);
        if (addressAndPassword == AddDeviceWithEmailAddressAndPasswordResultCode.Success)
          this.accountManager.SetKeyPair(this.EnvName, response.SecretKey, response.DeviceId);
        callback(addressAndPassword);
      }));
    }

    private static AddDeviceWithEmailAddressAndPasswordResult GetAddDeviceWithEmailAddressAndPassword(Gsc.Auth.GAuth.GAuth.API.Response.ErrorResponse error)
    {
      if (error == null)
        return new AddDeviceWithEmailAddressAndPasswordResult(AddDeviceWithEmailAddressAndPasswordResultCode.Success);
      AddDeviceWithEmailAddressAndPasswordResultCode resultCode = AddDeviceWithEmailAddressAndPasswordResultCode.UnknownError;
      string errorCode = error.ErrorCode;
      if (errorCode != null)
      {
        // ISSUE: reference to a compiler-generated field
        if (Session.\u003C\u003Ef__switch\u0024map2 == null)
        {
          // ISSUE: reference to a compiler-generated field
          Session.\u003C\u003Ef__switch\u0024map2 = new Dictionary<string, int>(3)
          {
            {
              "000",
              0
            },
            {
              "001",
              1
            },
            {
              "002",
              2
            }
          };
        }
        int num;
        // ISSUE: reference to a compiler-generated field
        if (Session.\u003C\u003Ef__switch\u0024map2.TryGetValue(errorCode, out num))
        {
          switch (num)
          {
            case 0:
              resultCode = AddDeviceWithEmailAddressAndPasswordResultCode.MissingDeviceId;
              goto label_14;
            case 1:
              resultCode = AddDeviceWithEmailAddressAndPasswordResultCode.MissingEmailOrPassword;
              goto label_14;
            case 2:
              resultCode = AddDeviceWithEmailAddressAndPasswordResultCode.Locked;
              goto label_14;
          }
        }
      }
      if (error.data.Root.GetValueByPointer("/reason/email", (string) null) != null)
        resultCode = AddDeviceWithEmailAddressAndPasswordResultCode.MissingEmailOrPassword;
      else if (error.data.Root.GetValueByPointer("/reason/password", (string) null) != null)
        resultCode = AddDeviceWithEmailAddressAndPasswordResultCode.MissingEmailOrPassword;
label_14:
      if (resultCode == AddDeviceWithEmailAddressAndPasswordResultCode.Locked)
        return new AddDeviceWithEmailAddressAndPasswordResult(resultCode, error.data.Root["expires_in"].ToInt(), error.data.Root["trial_counter"].ToInt());
      return new AddDeviceWithEmailAddressAndPasswordResult(resultCode);
    }

    public class AccessTokenChecker : MonoBehaviour
    {
      private const float FAILED_POLLING_INTERVAL = 30f;
      private const WebTaskAttribute TASK_ATTRIBUTES = WebTaskAttribute.Reliable | WebTaskAttribute.Silent | WebTaskAttribute.Parallel;
      private const WebTaskResult ACCEPT_RESULTS = WebTaskResult.kLocalResult | WebTaskResult.kGrobalResult | WebTaskResult.kCreticalError | WebTaskResult.Maintenance | WebTaskResult.UpdateApplication;
      private bool isRunning;

      public AccessTokenChecker()
      {
        base.\u002Ector();
      }

      private void OnApplicationPause(bool pauseState)
      {
        if (this.isRunning || pauseState || WebQueue.defaultQueue.isPause)
          return;
        this.StartCoroutine(this.CheckToken());
      }

      [DebuggerHidden]
      private IEnumerator CheckToken()
      {
        // ISSUE: object of a compiler-generated type is created
        return (IEnumerator) new Session.AccessTokenChecker.\u003CCheckToken\u003Ec__Iterator6() { \u003C\u003Ef__this = this };
      }
    }

    public class RefreshTokenTask : IRefreshTokenTask, ITask
    {
      private readonly Session session;

      public RefreshTokenTask(Session session)
      {
        this.session = session;
      }

      public WebTaskResult Result { get; protected set; }

      public bool isDone { get; protected set; }

      public void OnStart()
      {
      }

      public void OnFinish()
      {
      }

      [DebuggerHidden]
      public IEnumerator Run()
      {
        // ISSUE: object of a compiler-generated type is created
        return (IEnumerator) new Session.RefreshTokenTask.\u003CRun\u003Ec__Iterator7() { \u003C\u003Ef__this = this };
      }
    }
  }
}
