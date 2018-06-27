// Decompiled with JetBrains decompiler
// Type: Gsc.Auth.GAuth.DMMGamesStore.Session
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using Gsc.Auth.GAuth.DMMGamesStore.API.Request;
using Gsc.Core;
using Gsc.Device;
using Gsc.Network;
using Gsc.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace Gsc.Auth.GAuth.DMMGamesStore
{
  public class Session : Gsc.Auth.GAuth.GAuth.Session
  {
    public Session(string envName, IAccountManager accountManager)
      : base(envName, accountManager)
    {
      RootObject.Instance.StartCoroutine(Session.UpdateAccessToken(this));
    }

    [DebuggerHidden]
    private static IEnumerator UpdateAccessToken(Session session)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      Session.\u003CUpdateAccessToken\u003Ec__Iterator8 accessTokenCIterator8 = new Session.\u003CUpdateAccessToken\u003Ec__Iterator8();
      return (IEnumerator) accessTokenCIterator8;
    }

    public override string DeviceID
    {
      get
      {
        return Gsc.Auth.GAuth.DMMGamesStore.Device.Instance.ViewerId.ToString();
      }
    }

    public override bool CanRefreshToken(Type requestType)
    {
      if (!requestType.Equals(typeof (Gsc.Auth.GAuth.DMMGamesStore.API.Request.AccessToken)))
        return !requestType.Equals(typeof (UpdateOnetimeToken));
      return false;
    }

    public override IRefreshTokenTask GetRefreshTokenTask()
    {
      return (IRefreshTokenTask) new Session.RefreshTokenTask(this);
    }

    public override IWebTask RegisterEmailAddressAndPassword(string email, string password, bool disableValicationEmail, Action<RegisterEmailAddressAndPasswordResult> callback)
    {
      return (IWebTask) new Gsc.Auth.GAuth.DMMGamesStore.API.Request.RegisterEmailAddressAndPassword(Gsc.Auth.GAuth.DMMGamesStore.Device.Instance.ViewerId, Gsc.Auth.GAuth.DMMGamesStore.Device.Instance.OnetimeToken, email, password) { DisableValidationEmail = disableValicationEmail }.Send().OnResponse((VoidCallbackWithError<Gsc.Auth.GAuth.DMMGamesStore.API.Request.RegisterEmailAddressAndPassword.Response>) ((response, error) => callback(Session.GetRegisterEmailAddressWithPasswordResult(response, (Gsc.Auth.GAuth.GAuth.API.Response.ErrorResponse) error))));
    }

    private static RegisterEmailAddressAndPasswordResult GetRegisterEmailAddressWithPasswordResult(Gsc.Auth.GAuth.DMMGamesStore.API.Request.RegisterEmailAddressAndPassword.Response response, Gsc.Auth.GAuth.GAuth.API.Response.ErrorResponse error)
    {
      if (error == null)
        return new RegisterEmailAddressAndPasswordResult(RegisterEmailAddressAndPasswordResultCode.Success);
      RegisterEmailAddressAndPasswordResultCode resultCode = RegisterEmailAddressAndPasswordResultCode.UnknownError;
      string errorCode = error.ErrorCode;
      if (errorCode != null)
      {
        // ISSUE: reference to a compiler-generated field
        if (Session.\u003C\u003Ef__switch\u0024map3 == null)
        {
          // ISSUE: reference to a compiler-generated field
          Session.\u003C\u003Ef__switch\u0024map3 = new Dictionary<string, int>(3)
          {
            {
              "invalied_email",
              0
            },
            {
              "invalied_password",
              1
            },
            {
              "duplicated_email",
              2
            }
          };
        }
        int num;
        // ISSUE: reference to a compiler-generated field
        if (Session.\u003C\u003Ef__switch\u0024map3.TryGetValue(errorCode, out num))
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

    public override IWebTask AddDeviceWithEmailAddressAndPassword(string email, string password, Action<AddDeviceWithEmailAddressAndPasswordResult> callback)
    {
      return (IWebTask) new Gsc.Auth.GAuth.DMMGamesStore.API.Request.AddDeviceWithEmailAddressAndPassword(Gsc.Auth.GAuth.DMMGamesStore.Device.Instance.ViewerId, Gsc.Auth.GAuth.DMMGamesStore.Device.Instance.OnetimeToken, email, password).Send().OnResponse((VoidCallbackWithError<Gsc.Auth.GAuth.DMMGamesStore.API.Request.AddDeviceWithEmailAddressAndPassword.Response>) ((response, error) => callback(Session.GetAddDeviceWithEmailAddressAndPassword((Gsc.Auth.GAuth.GAuth.API.Response.ErrorResponse) error))));
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
        if (Session.\u003C\u003Ef__switch\u0024map4 == null)
        {
          // ISSUE: reference to a compiler-generated field
          Session.\u003C\u003Ef__switch\u0024map4 = new Dictionary<string, int>(3)
          {
            {
              "missing_device_id",
              0
            },
            {
              "missing_email_or_password",
              1
            },
            {
              "locked",
              2
            }
          };
        }
        int num;
        // ISSUE: reference to a compiler-generated field
        if (Session.\u003C\u003Ef__switch\u0024map4.TryGetValue(errorCode, out num))
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
        return (IEnumerator) new Session.RefreshTokenTask.\u003CRun\u003Ec__Iterator9() { \u003C\u003Ef__this = this };
      }
    }
  }
}
