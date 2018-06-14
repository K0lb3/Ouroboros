// Decompiled with JetBrains decompiler
// Type: ExitGames.Client.Photon.Chat.AuthenticationValues
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;

namespace ExitGames.Client.Photon.Chat
{
  public class AuthenticationValues
  {
    private CustomAuthenticationType authType = CustomAuthenticationType.None;

    public AuthenticationValues()
    {
    }

    public AuthenticationValues(string userId)
    {
      this.UserId = userId;
    }

    public CustomAuthenticationType AuthType
    {
      get
      {
        return this.authType;
      }
      set
      {
        this.authType = value;
      }
    }

    public string AuthGetParameters { get; set; }

    public object AuthPostData { get; private set; }

    public string Token { get; set; }

    public string UserId { get; set; }

    public virtual void SetAuthPostData(string stringData)
    {
      this.AuthPostData = !string.IsNullOrEmpty(stringData) ? (object) stringData : (object) (string) null;
    }

    public virtual void SetAuthPostData(byte[] byteData)
    {
      this.AuthPostData = (object) byteData;
    }

    public virtual void AddAuthParameter(string key, string value)
    {
      this.AuthGetParameters = string.Format("{0}{1}{2}={3}", new object[4]
      {
        (object) this.AuthGetParameters,
        (object) (!string.IsNullOrEmpty(this.AuthGetParameters) ? "&" : string.Empty),
        (object) Uri.EscapeDataString(key),
        (object) Uri.EscapeDataString(value)
      });
    }

    public override string ToString()
    {
      return string.Format("AuthenticationValues UserId: {0}, GetParameters: {1} Token available: {2}", (object) this.UserId, (object) this.AuthGetParameters, (object) (this.Token != null));
    }
  }
}
