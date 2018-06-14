// Decompiled with JetBrains decompiler
// Type: Gsc.Auth.GAuth.GAuth.API.Request.AddDeviceWithEmailAddressAndPassword
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using Gsc.Auth.GAuth.GAuth.API.Generic;
using Gsc.Network;
using Gsc.Network.Support.MiniJsonHelper;
using System;
using System.Collections.Generic;

namespace Gsc.Auth.GAuth.GAuth.API.Request
{
  public class AddDeviceWithEmailAddressAndPassword : GAuthRequest<AddDeviceWithEmailAddressAndPassword, Gsc.Auth.GAuth.GAuth.API.Response.AddDeviceWithEmailAddressAndPassword>
  {
    private const string ___path = "/auth/email/device";

    public AddDeviceWithEmailAddressAndPassword(string emailAddress, string password)
    {
      this.EmailAddress = emailAddress;
      this.Password = password;
    }

    public string EmailAddress { get; set; }

    public string Password { get; set; }

    public string Idfv { get; set; }

    public override string GetPath()
    {
      return "/auth/email/device";
    }

    public override string GetMethod()
    {
      return "POST";
    }

    protected override Dictionary<string, object> GetParameters()
    {
      Dictionary<string, object> dictionary = new Dictionary<string, object>();
      dictionary["email"] = Serializer.Instance.Add<string>(new Func<string, object>(Serializer.From<string>)).Serialize<string>(this.EmailAddress);
      dictionary["password"] = Serializer.Instance.Add<string>(new Func<string, object>(Serializer.From<string>)).Serialize<string>(this.Password);
      dictionary["idfv"] = Serializer.Instance.Add<string>(new Func<string, object>(Serializer.From<string>)).Serialize<string>(this.Idfv);
      return dictionary;
    }

    public override Type GetErrorResponseType()
    {
      return typeof (Gsc.Auth.GAuth.GAuth.API.Response.ErrorResponse);
    }

    public override WebTaskResult InquireResult(WebTaskResult result, WebInternalResponse response)
    {
      if (response.StatusCode == 400 && response.Payload != null && (response.Payload.Length > 0 && response.ContentType == ContentType.ApplicationJson))
        return WebTaskResult.MustErrorHandle;
      return result;
    }

    protected override bool IsParameterUseParam()
    {
      return false;
    }
  }
}
