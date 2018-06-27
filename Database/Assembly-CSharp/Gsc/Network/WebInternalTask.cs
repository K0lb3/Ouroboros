// Decompiled with JetBrains decompiler
// Type: Gsc.Network.WebInternalTask
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using Gsc.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Gsc.Network
{
  public abstract class WebInternalTask : IEnumerator, IWebTaskBase, ITask
  {
    private static readonly byte[] wwwZeroBuffer = Encoding.ASCII.GetBytes("{}");
    private float timeout = 30f;
    private int retryCount;
    private bool completed;
    private WWW webRequest;
    private readonly string method;
    private readonly string uri;
    private readonly byte[] payload;
    private readonly CustomHeaders customHeaders;
    private WebInternalTask.WaitTask waitTask;
    private object subroutine;
    private float startTime;

    public WebInternalTask(string method, string uri, byte[] payload, CustomHeaders customHeaders)
    {
      this.method = method;
      this.uri = uri;
      this.payload = payload;
      this.customHeaders = customHeaders;
      if (!GameUtility.IsDebugBuild)
        return;
      this.timeout = 240f;
    }

    public WebTaskResult Result { get; protected set; }

    public bool isBreak { get; private set; }

    public bool isDone
    {
      get
      {
        if (!this.completed)
          return this.isBreak;
        return true;
      }
    }

    public static WebInternalTask<TRequest, TResponse> Create<TRequest, TResponse>(IRequest<TRequest, TResponse> request) where TRequest : IRequest<TRequest, TResponse>, IRequest where TResponse : IResponse<TResponse>
    {
      return new WebInternalTask<TRequest, TResponse>(request);
    }

    private void Update()
    {
      if (this.webRequest == null)
      {
        this.webRequest = WebInternalTask.CreateRequest(this.method, this.uri, this.payload, this.customHeaders);
        this.startTime = Time.get_unscaledTime();
      }
      else
      {
        WebInternalResponse response;
        if (!this.webRequest.get_isDone())
        {
          if ((double) (Time.get_unscaledTime() - this.startTime) < (double) this.timeout)
            return;
          response = new WebInternalResponse(504);
        }
        else
          response = new WebInternalResponse(this.webRequest);
        int statusCode = response.StatusCode;
        try
        {
          if ((statusCode == 0 || 500 <= statusCode && statusCode <= 599) && (statusCode != 503 && statusCode != 504) && ++this.retryCount < 3)
          {
            this.waitTask = new WebInternalTask.WaitTask();
          }
          else
          {
            this.Result = this.ProcessResponse(response);
            this.completed = true;
          }
        }
        finally
        {
          this.InternalDispose();
        }
      }
    }

    protected abstract WebTaskResult ProcessResponse(WebInternalResponse response);

    public void Break()
    {
      this.isBreak = true;
    }

    public void Reset()
    {
      if (this.isBreak)
        return;
      this.retryCount = 0;
      this.completed = false;
    }

    public void OnStart()
    {
      this.Reset();
    }

    public void OnFinish()
    {
    }

    public IEnumerator Run()
    {
      return (IEnumerator) this;
    }

    public object Current
    {
      get
      {
        return this.subroutine;
      }
    }

    public bool MoveNext()
    {
      this.subroutine = (object) null;
      if (this.waitTask != null && this.waitTask.Wait())
        return true;
      this.waitTask = (WebInternalTask.WaitTask) null;
      if (!this.isDone)
        this.Update();
      if (!this.isDone)
        return true;
      this.InternalDispose();
      return false;
    }

    private void InternalDispose()
    {
      if (this.webRequest == null)
        return;
      this.webRequest.Dispose();
      this.webRequest = (WWW) null;
    }

    private static WWW CreateRequest(string method, string uri, byte[] payload, CustomHeaders customHeaders)
    {
      Dictionary<string, string> dictionary = new Dictionary<string, string>();
      customHeaders.Dispatch(new Action<string, string>(dictionary.Add));
      return !(method == "GET") ? new WWW(uri, payload ?? WebInternalTask.wwwZeroBuffer, dictionary) : new WWW(uri, (byte[]) null, dictionary);
    }

    private class WaitTask
    {
      private readonly float time;

      public WaitTask()
      {
        this.time = Time.get_unscaledTime();
      }

      public bool Wait()
      {
        return (double) (Time.get_unscaledTime() - this.time) < 1.0;
      }
    }
  }
}
