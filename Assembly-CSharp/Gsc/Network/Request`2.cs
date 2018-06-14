// Decompiled with JetBrains decompiler
// Type: Gsc.Network.Request`2
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using Gsc.DOM;
using Gsc.DOM.Generic;
using Gsc.Network.Parser;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gsc.Network
{
  public abstract class Request<TRequest, TResponse> : ApiObject, IRequest, IRequest<TRequest, TResponse> where TRequest : IRequest<TRequest, TResponse> where TResponse : IResponse<TResponse>
  {
    private readonly string ___request_id;
    protected IWebTask<TRequest, TResponse> ___task;

    public Request()
    {
      this.___request_id = Guid.NewGuid().ToString("N");
      this.CustomHeaders = new CustomHeaders(this.___request_id);
    }

    IWebTask IRequest.Cast()
    {
      return (IWebTask) this.Cast();
    }

    IWebTask IRequest.Send()
    {
      return (IWebTask) this.Send();
    }

    public CustomHeaders CustomHeaders { get; private set; }

    public bool isDone
    {
      get
      {
        if (this.___task != null)
          return this.___task.isDone;
        return false;
      }
    }

    public string GetRequestID()
    {
      return this.___request_id;
    }

    public virtual string GetHost()
    {
      return SDK.Configuration.Env.ServerUrl;
    }

    public virtual string GetUrl()
    {
      return this.GetHost() + this.GetPath();
    }

    public abstract string GetPath();

    public abstract string GetMethod();

    protected virtual Dictionary<string, object> GetParameters()
    {
      return (Dictionary<string, object>) null;
    }

    protected virtual bool IsParameterUseParam()
    {
      return true;
    }

    public virtual byte[] GetPayload()
    {
      Dictionary<string, object> parameters = this.GetParameters();
      if (parameters != null)
        return Encoding.UTF8.GetBytes(MiniJSON.Json.Serialize((object) parameters));
      return Encoding.UTF8.GetBytes(MiniJSON.Json.Serialize(Gsc.DOM.FastJSON.Json.Deserialize((IValue) (Value) ((Gsc.DOM.Generic.Object) this))));
    }

    public virtual Type GetErrorResponseType()
    {
      return typeof (ErrorResponse);
    }

    public virtual WebTaskResult InquireResult(WebTaskResult result, WebInternalResponse response)
    {
      return result;
    }

    public void Retry()
    {
      if (this.___task == null)
        return;
      this.___task.Retry();
    }

    public WebTask<TRequest, TResponse> Cast()
    {
      return this.ToWebTask(WebTaskAttribute.Silent | WebTaskAttribute.Parallel);
    }

    public WebTask<TRequest, TResponse> Send()
    {
      return this.ToWebTask(WebTaskAttribute.Reliable | WebTaskAttribute.Parallel);
    }

    public WebTask<TRequest, TResponse> SerialSend()
    {
      return this.ToWebTask(WebTaskAttribute.Reliable);
    }

    public WebTask<TRequest, TResponse> ToWebTask(WebTaskAttribute attributes)
    {
      WebTask<TRequest, TResponse> webTask = WebTask<TRequest, TResponse>.Send((IRequest<TRequest, TResponse>) this, attributes);
      this.___task = (IWebTask<TRequest, TResponse>) webTask;
      return webTask;
    }

    public TResponse GetResponse()
    {
      if (!this.isDone)
        throw new RequestException("Still processing this request.");
      return this.___task.Response;
    }

    public IErrorResponse GetError()
    {
      if (!this.isDone)
        throw new RequestException("Still processing this request.");
      return this.___task.ErrorResponse;
    }

    public WebTaskResult GetResult()
    {
      if (!this.isDone)
        throw new RequestException("Still processing this request.");
      return this.___task.Result;
    }
  }
}
