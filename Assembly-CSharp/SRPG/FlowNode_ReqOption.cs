// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqOption
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Text;
using UnityEngine;

namespace SRPG
{
  [FlowNode.NodeType("System/WebApi/Option", 32741)]
  [FlowNode.Pin(220, "オプション設定失敗", FlowNode.PinTypes.Output, 220)]
  [FlowNode.Pin(210, "オプション設定完了", FlowNode.PinTypes.Output, 210)]
  [FlowNode.Pin(200, "Set", FlowNode.PinTypes.Input, 200)]
  public class FlowNode_ReqOption : FlowNode_Network
  {
    private FlowNode_ReqOption.ApiBase m_Api;

    public override void OnActivate(int pinID)
    {
      if (this.m_Api != null)
      {
        DebugUtility.LogError("同時に複数の通信が入ると駄目！");
      }
      else
      {
        if (pinID == 200)
          this.m_Api = (FlowNode_ReqOption.ApiBase) new FlowNode_ReqOption.Api_OptionSet(this);
        if (this.m_Api == null)
          return;
        if (this.m_Api.Start())
          ((Behaviour) this).set_enabled(true);
        else
          this.m_Api = (FlowNode_ReqOption.ApiBase) null;
      }
    }

    public override void OnSuccess(WWWResult www)
    {
      if (this.m_Api == null)
        return;
      this.m_Api.Complete(www);
      this.m_Api = (FlowNode_ReqOption.ApiBase) null;
    }

    public class ApiBase
    {
      protected FlowNode_ReqOption m_Node;
      protected RequestAPI m_Request;

      public ApiBase(FlowNode_ReqOption node)
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

      public virtual bool Start()
      {
        if (Network.Mode == Network.EConnectMode.Online)
        {
          this.m_Node.ExecRequest((WebAPI) new RequestAPI(this.url, new Network.ResponseCallback(((FlowNode_Network) this.m_Node).ResponseCallback), this.req));
          return true;
        }
        this.Failed();
        return false;
      }
    }

    public class Api_OptionSet : FlowNode_ReqOption.ApiBase
    {
      private bool m_Flag;
      private string m_Comment;

      public Api_OptionSet(FlowNode_ReqOption node)
        : base(node)
      {
        this.m_Flag = GlobalVars.MultiInvitaionFlag;
        this.m_Comment = GlobalVars.MultiInvitaionComment;
      }

      public override string url
      {
        get
        {
          return "setoption";
        }
      }

      public override string req
      {
        get
        {
          StringBuilder stringBuilder = new StringBuilder(128);
          stringBuilder.Append("\"is_multi_push\":" + (!this.m_Flag ? "0" : "1"));
          stringBuilder.Append(",\"multi_comment\":\"" + (this.m_Comment != null ? this.m_Comment : string.Empty) + "\"");
          return stringBuilder.ToString();
        }
      }

      public override bool Start()
      {
        if (UnityEngine.Object.op_Equality((UnityEngine.Object) MonoSingleton<GameManager>.Instance, (UnityEngine.Object) null) || MonoSingleton<GameManager>.Instance.Player == null)
        {
          this.Failed();
          return false;
        }
        PlayerData player = MonoSingleton<GameManager>.Instance.Player;
        if (this.m_Flag != player.MultiInvitaionFlag || this.m_Comment != player.MultiInvitaionComment)
          return base.Start();
        this.Success();
        return false;
      }

      public override void Success()
      {
        this.m_Node.ActivateOutputLinks(210);
        ((Behaviour) this.m_Node).set_enabled(false);
      }

      public override void Failed()
      {
        this.m_Node.ActivateOutputLinks(220);
        Network.RemoveAPI();
        Network.ResetError();
        ((Behaviour) this.m_Node).set_enabled(false);
      }

      public override void Complete(WWWResult www)
      {
        if (Network.IsError)
        {
          this.Failed();
        }
        else
        {
          DebugMenu.Log("API", this.url + ":" + www.text);
          WebAPI.JSON_BodyResponse<FlowNode_ReqOption.Api_OptionSet.Json> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<FlowNode_ReqOption.Api_OptionSet.Json>>(www.text);
          DebugUtility.Assert(jsonObject != null, "res == null");
          if (jsonObject.body != null)
            MonoSingleton<GameManager>.Instance.Player.Deserialize(jsonObject.body.player);
          Network.RemoveAPI();
          this.Success();
        }
      }

      [Serializable]
      public class Json
      {
        public Json_PlayerData player;
      }
    }
  }
}
