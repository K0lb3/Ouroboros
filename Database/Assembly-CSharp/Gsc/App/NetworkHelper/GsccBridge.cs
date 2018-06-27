// Decompiled with JetBrains decompiler
// Type: Gsc.App.NetworkHelper.GsccBridge
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using Gsc.Auth;
using Gsc.Network;
using SRPG;
using System;
using System.Text;

namespace Gsc.App.NetworkHelper
{
  public static class GsccBridge
  {
    private static WebTaskBundle unhandledTasks;

    public static void Send(WebAPI api, bool silent)
    {
      WebTaskAttribute attributes = WebTaskAttribute.Reliable;
      if (silent)
        attributes |= WebTaskAttribute.Silent;
      bool flag = !string.IsNullOrEmpty(api.body);
      string method = !flag ? "GET" : "POST";
      byte[] payload = !flag ? (byte[]) null : Encoding.UTF8.GetBytes(api.body);
      WebRequest webRequest = new WebRequest(method, api.name, payload);
      GsccBridge.SetCustomHeaders(webRequest.CustomHeaders);
      GsccBridge.Send(webRequest.ToWebTask(attributes), api.callback);
    }

    public static void SendImmediate(WebAPI api)
    {
      bool flag = !string.IsNullOrEmpty(api.body);
      string method = !flag ? "GET" : "POST";
      byte[] payload = !flag ? (byte[]) null : Encoding.UTF8.GetBytes(api.body);
      WebRequest webRequest = new WebRequest(method, api.name, payload);
      GsccBridge.SetCustomHeaders(webRequest.CustomHeaders);
      BlockRequest<WebRequest, WebResponse> blockRequest = BlockRequest.Create<WebRequest, WebResponse>((IRequest<WebRequest, WebResponse>) webRequest);
      string result;
      if (blockRequest.GetResult() != WebTaskResult.Success)
      {
        SRPG.Network.SetServerMetaDataAsError();
        result = string.Empty;
      }
      else
      {
        WebResponse response = blockRequest.GetResponse();
        result = response == null || response.payload == null ? string.Empty : Encoding.UTF8.GetString(response.payload);
      }
      api.callback(new WWWResult(result));
    }

    private static void SetCustomHeaders(CustomHeaders headers)
    {
      if (!string.IsNullOrEmpty(SRPG.Network.Version))
        headers.SetCustomHeader("x-app-ver", SRPG.Network.Version);
      if (string.IsNullOrEmpty(SRPG.Network.AssetVersion))
        return;
      headers.SetCustomHeader("x-asset-ver", SRPG.Network.AssetVersion);
    }

    public static void SetBaseCustomHeaders(Action<string, string> setter, string requestId)
    {
      if (SDK.Initialized)
      {
        CustomHeaders headers = new CustomHeaders(requestId);
        GsccBridge.SetCustomHeaders(headers);
        headers.Dispatch(setter);
      }
      else
      {
        setter("Content-Type", "application/json; charset=utf-8");
        if (!string.IsNullOrEmpty(SRPG.Network.Version))
          setter("x-app-ver", SRPG.Network.Version);
        if (!string.IsNullOrEmpty(SRPG.Network.AssetVersion))
          setter("x-asset-ver", SRPG.Network.AssetVersion);
        setter("X-GUMI-DEVICE-OS", "android");
        setter("X-GUMI-TRANSACTION", requestId);
        setter("X-GUMI-REQUEST-ID", requestId);
        if (Session.DefaultSession == null || Session.DefaultSession.AccessToken == null)
          return;
        setter("Authorization", "gauth " + Session.DefaultSession.AccessToken);
        setter("X-Gumi-User-Agent", Session.DefaultSession.UserAgent);
      }
    }

    public static void SetWebViewHeaders(Action<string, string> setter)
    {
      if (!SDK.Initialized)
        return;
      if (Session.DefaultSession != null && Session.DefaultSession.AccessToken != null)
        setter("Authorization", "gauth " + Session.DefaultSession.AccessToken);
      setter("X-GUMI-DEVICE-OS", "android");
      setter("X-GUMI-DEVICE-PLATFORM", "android");
    }

    private static void Send(WebTask<WebRequest, WebResponse> task, SRPG.Network.ResponseCallback callback)
    {
      task.OnResponse((VoidCallback<WebResponse>) (r => SRPG.Network.ConnectingResponse(r, callback)));
    }

    public static bool HasUnhandledTasks
    {
      get
      {
        return GsccBridge.unhandledTasks != null;
      }
    }

    public static void OnReceiveUnhandledTasks(WebTaskBundle taskBundle)
    {
      if (GsccBridge.unhandledTasks == null)
      {
        GsccBridge.unhandledTasks = taskBundle;
      }
      else
      {
        foreach (IWebTask task in taskBundle)
          GsccBridge.unhandledTasks.Add<IWebTask>(task);
      }
    }

    public static void Retry()
    {
      if (GsccBridge.unhandledTasks == null)
        return;
      GsccBridge.unhandledTasks.Retry();
      GsccBridge.unhandledTasks = (WebTaskBundle) null;
    }

    public static void Reset()
    {
      GsccBridge.unhandledTasks = (WebTaskBundle) null;
    }
  }
}
