// Decompiled with JetBrains decompiler
// Type: Gsc.Network.WebTask`2
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using Gsc.App;
using Gsc.Auth;
using Gsc.Core;
using Gsc.Tasks;
using System;
using System.Collections;
using System.Reflection;
using System.Text;
using UnityEngine;

namespace Gsc.Network
{
  public class WebTask<TRequest, TResponse> : IEnumerator, IWebTaskBase, IWebTask, ITask, IWebTask<TRequest, TResponse>, IWebTask<TResponse> where TRequest : IRequest<TRequest, TResponse> where TResponse : IResponse<TResponse>
  {
    private WebInternalTask<TRequest, TResponse> internalTask;
    private WebTaskAttribute attributes;

    private WebTask(WebTaskAttribute attributes)
    {
      this.attributes = attributes;
    }

    public IWebCallback<TRequest, TResponse> callback { get; private set; }

    public WebTaskResult acceptResults { get; private set; }

    public TRequest Request { get; private set; }

    public TResponse Response { get; private set; }

    public IErrorResponse ErrorResponse { get; private set; }

    public byte[] error { get; private set; }

    public bool isBreak { get; private set; }

    public bool isDone { get; private set; }

    public WebTaskResult Result { get; private set; }

    public bool handled { get; private set; }

    public static WebTask<TRequest, TResponse> Send(IRequest<TRequest, TResponse> request, WebTaskAttribute attributes)
    {
      WebTask<TRequest, TResponse> webTask = new WebTask<TRequest, TResponse>(attributes);
      webTask.Request = (TRequest) request;
      WebQueue.defaultQueue.Add((IWebTask) webTask);
      return webTask;
    }

    public Type GetRequestType()
    {
      return typeof (TRequest);
    }

    public bool IsAcceptResult(WebTaskResult result)
    {
      return (this.acceptResults & result) > WebTaskResult.None;
    }

    public bool HasAttributes(WebTaskAttribute attributes)
    {
      return (this.attributes & attributes) == attributes;
    }

    public void Retry()
    {
      if (this.internalTask != null)
        return;
      this.Reset();
      this.attributes |= WebTaskAttribute.Interrupt;
      WebQueue.defaultQueue.Add((IWebTask) this);
      WebQueue.defaultQueue.Pause(false);
    }

    public void Break()
    {
      if (this.internalTask == null)
        return;
      this.internalTask.Break();
    }

    public WebTask<TRequest, TResponse> SetAcceptResults(WebTaskResult handleResults)
    {
      this.acceptResults = handleResults;
      return this;
    }

    public WebInternalTask GetInternalTask()
    {
      return (WebInternalTask) this.internalTask;
    }

    public object Current
    {
      get
      {
        return (object) null;
      }
    }

    public void Reset()
    {
      this.Response = default (TResponse);
      this.ErrorResponse = (IErrorResponse) null;
      this.error = (byte[]) null;
      this.isBreak = false;
      this.isDone = false;
      this.Result = WebTaskResult.None;
      this.handled = false;
    }

    public bool MoveNext()
    {
      return !this.isDone;
    }

    public WebTask<TRequest, TResponse> OnResponse(VoidCallback<TResponse> callback)
    {
      this.callback = (IWebCallback<TRequest, TResponse>) WebCallbackBuilder<TRequest, TResponse>.Build(callback);
      return this;
    }

    public WebTask<TRequest, TResponse> OnResponse(VoidCallbackWithError<TResponse> callback)
    {
      this.callback = (IWebCallback<TRequest, TResponse>) WebCallbackBuilder<TRequest, TResponse>.Build(callback);
      return this;
    }

    public WebTask<TRequest, TResponse> OnCoroutineResponse(YieldCallback<TResponse> callback)
    {
      this.callback = (IWebCallback<TRequest, TResponse>) WebCallbackBuilder<TRequest, TResponse>.Build(callback);
      return this;
    }

    public WebTask<TRequest, TResponse> OnCoroutineResponse(YieldCallbackWithError<TResponse> callback)
    {
      this.callback = (IWebCallback<TRequest, TResponse>) WebCallbackBuilder<TRequest, TResponse>.Build(callback);
      return this;
    }

    public IEnumerator Run()
    {
      return (IEnumerator) this.internalTask;
    }

    public void OnStart()
    {
      this.internalTask = WebInternalTask.Create<TRequest, TResponse>((IRequest<TRequest, TResponse>) this.Request);
      this.internalTask.OnStart();
    }

    public void OnFinish()
    {
      this.internalTask.OnFinish();
      this.Response = this.internalTask.Response;
      this.ErrorResponse = this.internalTask.ErrorResponse;
      this.error = this.internalTask.error;
      this.isBreak = this.internalTask.isBreak;
      this.isDone = this.internalTask.isDone;
      this.Result = this.internalTask.Result;
      this.internalTask = (WebInternalTask<TRequest, TResponse>) null;
      if (this.callback == null)
        return;
      this.handled = this.callback.OnCallback(this);
    }

    private static WebTaskResult GetTaskResult(WebInternalResponse response)
    {
      int statusCode = response.StatusCode;
      byte[] payload = response.Payload;
      if (statusCode == 0)
        return WebTaskResult.ServerError;
      if (200 <= statusCode && statusCode <= 299)
        return WebTaskResult.Success;
      switch (statusCode)
      {
        case 401:
          if (!Session.DefaultSession.CanRefreshToken(typeof (TRequest)))
            return WebTaskResult.InvalidDeviceError;
          return payload != null && payload.Length > 0 && Encoding.UTF8.GetString(payload).ToUpper() == "EXPIRED TOKEN" ? WebTaskResult.InternalExpiredTokenError : WebTaskResult.ExpiredSessionError;
        case 471:
          return WebTaskResult.UpdateApplication;
        case 472:
          return WebTaskResult.UpdateResource;
        case 479:
          return WebTaskResult.Maintenance;
        case 498:
          return WebTaskResult.Interrupt;
        case 499:
          return WebTaskResult.MustErrorHandle;
        default:
          return 500 <= statusCode && statusCode <= 599 ? WebTaskResult.ServerError : WebTaskResult.UnknownError;
      }
    }

    public static WebTaskResult TryGetResponse(TRequest request, WebInternalResponse internalResponse, out TResponse response, out IErrorResponse error)
    {
      error = (IErrorResponse) null;
      response = default (TResponse);
      byte[] payload = internalResponse.Payload;
      WebTaskResult webTaskResult = request.InquireResult(WebTask<TRequest, TResponse>.GetTaskResult(internalResponse), internalResponse);
      try
      {
        if (webTaskResult == WebTaskResult.Success)
        {
          try
          {
            response = AssemblySupport.CreateInstance<TResponse>(new object[1]
            {
              (object) internalResponse
            });
          }
          catch (MissingMethodException ex)
          {
            response = AssemblySupport.CreateInstance<TResponse>(new object[1]
            {
              (object) payload
            });
          }
        }
        if (webTaskResult == WebTaskResult.MustErrorHandle)
          error = AssemblySupport.CreateInstance<IErrorResponse>(request.GetErrorResponseType(), new object[1]
          {
            (object) internalResponse
          });
      }
      catch (TargetInvocationException ex)
      {
        WebQueueListener.ErrorPayload = payload;
        Debug.LogError((object) Encoding.UTF8.GetString(payload));
        Debug.Log((object) ex);
        return WebTaskResult.ParseError;
      }
      return webTaskResult;
    }

    public class ParseError : Exception
    {
      public ParseError(byte[] payload, TargetInvocationException e)
        : base(string.Format("<{0}, {1}>: {2}", (object) typeof (TRequest), (object) typeof (TResponse), (object) Encoding.UTF8.GetString(payload)), (Exception) e)
      {
      }
    }
  }
}
