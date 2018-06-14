// Decompiled with JetBrains decompiler
// Type: Gsc.Network.WebInternalResponse
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Gsc.Network
{
  public class WebInternalResponse
  {
    private readonly WeakReference request;
    public readonly byte[] Payload;
    public readonly int StatusCode;
    public readonly ContentType ContentType;

    public WebInternalResponse(WWW request)
    {
      this.request = new WeakReference((object) request);
      this.StatusCode = WebInternalResponse.GetStatusCode(request);
      this.Payload = WebInternalResponse.GetResponsePayload(request);
      this.ContentType = WebInternalResponse.GetContentType(this);
    }

    public WebInternalResponse(int statusCode)
    {
      this.request = (WeakReference) null;
      this.StatusCode = statusCode;
      this.Payload = (byte[]) null;
      this.ContentType = ContentType.None;
    }

    public string GetResponseHeader(string name)
    {
      string str = (string) null;
      if (this.request != null && this.request.IsAlive)
        ((WWW) this.request.Target).get_responseHeaders().TryGetValue(name, out str);
      return str;
    }

    private static int GetStatusCode(WWW webRequest)
    {
      if (webRequest.get_responseHeaders() != null)
      {
        string s;
        if (webRequest.get_responseHeaders().TryGetValue("X-GUMI-STATUS-CODE", out s))
          return int.Parse(s);
        if (webRequest.get_responseHeaders().TryGetValue("STATUS", out s) || webRequest.get_responseHeaders().TryGetValue("NULL", out s))
        {
          if (s.ToLower().Contains("connection established"))
            return 503;
          string[] array = ((IEnumerable<string>) s.Split(' ')).Select<string, string>((Func<string, string>) (x => x.Trim())).Where<string>((Func<string, bool>) (x => !string.IsNullOrEmpty(x))).ToArray<string>();
          if (array.Length >= 3)
            return int.Parse(array[1]);
        }
      }
      return 0;
    }

    private static byte[] GetResponsePayload(WWW webRequest)
    {
      return webRequest.get_bytes();
    }

    private static ContentType GetContentType(WebInternalResponse response)
    {
      string responseHeader = response.GetResponseHeader("CONTENT-TYPE");
      if (responseHeader != null)
      {
        if (responseHeader.StartsWith("application/json"))
          return ContentType.ApplicationJson;
        if (responseHeader.StartsWith("application/octet-stream"))
          return ContentType.ApplicationOctetStream;
      }
      return ContentType.TextPlain;
    }
  }
}
