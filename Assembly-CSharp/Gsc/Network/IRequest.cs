// Decompiled with JetBrains decompiler
// Type: Gsc.Network.IRequest
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;

namespace Gsc.Network
{
  public interface IRequest
  {
    CustomHeaders CustomHeaders { get; }

    bool isDone { get; }

    WebTaskResult GetResult();

    string GetRequestID();

    string GetHost();

    string GetUrl();

    string GetPath();

    string GetMethod();

    IWebTask Cast();

    IWebTask Send();

    void Retry();

    byte[] GetPayload();

    Type GetErrorResponseType();

    WebTaskResult InquireResult(WebTaskResult result, WebInternalResponse response);
  }
}
