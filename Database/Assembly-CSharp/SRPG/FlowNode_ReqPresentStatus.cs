// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqPresentStatus
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System;
using UnityEngine;

namespace SRPG
{
  [FlowNode.NodeType("System/WebApi/PresentStatus", 32741)]
  [FlowNode.Pin(100, "送付ステータス取得", FlowNode.PinTypes.Input, 100)]
  [FlowNode.Pin(110, "送付ステータス取得完了", FlowNode.PinTypes.Output, 110)]
  [FlowNode.Pin(120, "送付ステータス取得失敗", FlowNode.PinTypes.Output, 120)]
  public class FlowNode_ReqPresentStatus : FlowNode_Network
  {
    private FlowNode_ReqPresentStatus.ApiBase m_Api;

    public override void OnActivate(int pinID)
    {
      if (this.m_Api != null)
      {
        DebugUtility.LogError("同時に複数の通信が入ると駄目！");
      }
      else
      {
        if (pinID == 100)
          this.m_Api = (FlowNode_ReqPresentStatus.ApiBase) new FlowNode_ReqPresentStatus.Api_PresentListStatus(this);
        if (this.m_Api == null)
          return;
        this.m_Api.Start();
        ((Behaviour) this).set_enabled(true);
      }
    }

    public override void OnSuccess(WWWResult www)
    {
      if (this.m_Api == null)
        return;
      this.m_Api.Complete(www);
      this.m_Api = (FlowNode_ReqPresentStatus.ApiBase) null;
    }

    public class ApiBase
    {
      protected FlowNode_ReqPresentStatus m_Node;
      protected RequestAPI m_Request;

      public ApiBase(FlowNode_ReqPresentStatus node)
      {
        this.m_Node = node;
      }

      public virtual string url
      {
        get
        {
          return string.Empty;
        }
      }

      public virtual string req
      {
        get
        {
          return (string) null;
        }
      }

      public virtual void Success()
      {
      }

      public virtual void Failed()
      {
      }

      public virtual void Complete(WWWResult www)
      {
      }

      public virtual void Start()
      {
        if (Network.Mode == Network.EConnectMode.Online)
        {
          if (MonoSingleton<GameManager>.Instance.MasterParam.IsFriendPresentItemParamValid())
            this.m_Node.ExecRequest((WebAPI) new RequestAPI(this.url, new Network.ResponseCallback(((FlowNode_Network) this.m_Node).ResponseCallback), this.req));
          else
            this.Success();
        }
        else
          this.Failed();
      }
    }

    public class Api_PresentListStatus : FlowNode_ReqPresentStatus.ApiBase
    {
      public Api_PresentListStatus(FlowNode_ReqPresentStatus node)
        : base(node)
      {
      }

      public override string url
      {
        get
        {
          return "present";
        }
      }

      public override void Success()
      {
        this.m_Node.ActivateOutputLinks(110);
        ((Behaviour) this.m_Node).set_enabled(false);
      }

      public override void Failed()
      {
        this.m_Node.ActivateOutputLinks(120);
        Network.RemoveAPI();
        Network.ResetError();
        ((Behaviour) this.m_Node).set_enabled(false);
      }

      public override void Complete(WWWResult www)
      {
        if (Network.IsError)
        {
          this.m_Node.OnFailed();
        }
        else
        {
          DebugMenu.Log("API", this.url + ":" + www.text);
          WebAPI.JSON_BodyResponse<FlowNode_ReqPresentStatus.Api_PresentListStatus.Json> jsonBodyResponse = (WebAPI.JSON_BodyResponse<FlowNode_ReqPresentStatus.Api_PresentListStatus.Json>) JsonUtility.FromJson<WebAPI.JSON_BodyResponse<FlowNode_ReqPresentStatus.Api_PresentListStatus.Json>>(www.text);
          DebugUtility.Assert(jsonBodyResponse != null, "res == null");
          if (jsonBodyResponse.body != null)
          {
            if (string.IsNullOrEmpty(jsonBodyResponse.body.result))
              FriendPresentRootWindow.SetSendStatus(FriendPresentRootWindow.SendStatus.UNSENT);
            else if (jsonBodyResponse.body.result == "0")
              FriendPresentRootWindow.SetSendStatus(FriendPresentRootWindow.SendStatus.SENDING);
            else if (jsonBodyResponse.body.result == "1")
              FriendPresentRootWindow.SetSendStatus(FriendPresentRootWindow.SendStatus.SENDED);
            else if (jsonBodyResponse.body.result == "9")
              FriendPresentRootWindow.SetSendStatus(FriendPresentRootWindow.SendStatus.SENTFAILED);
            else
              FriendPresentRootWindow.SetSendStatus(FriendPresentRootWindow.SendStatus.NONE);
          }
          else
            FriendPresentRootWindow.SetSendStatus(FriendPresentRootWindow.SendStatus.NONE);
          Network.RemoveAPI();
          this.Success();
        }
      }

      [Serializable]
      public class Json
      {
        public string result;
      }
    }
  }
}
