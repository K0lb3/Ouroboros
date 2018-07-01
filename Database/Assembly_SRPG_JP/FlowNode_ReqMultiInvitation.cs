// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqMultiInvitation
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Text;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(210, "マルチ招待完了", FlowNode.PinTypes.Output, 210)]
  [FlowNode.Pin(300, "マルチ招待通知取得", FlowNode.PinTypes.Input, 300)]
  [FlowNode.Pin(310, "マルチ招待通知完了", FlowNode.PinTypes.Output, 310)]
  [FlowNode.Pin(320, "マルチ招待通知失敗", FlowNode.PinTypes.Output, 320)]
  [FlowNode.NodeType("System/WebApi/MultiInvitation", 32741)]
  [FlowNode.Pin(100, "マルチ招待一覧", FlowNode.PinTypes.Input, 100)]
  [FlowNode.Pin(110, "マルチ招待一覧取得完了", FlowNode.PinTypes.Output, 110)]
  [FlowNode.Pin(120, "マルチ招待一覧取得失敗", FlowNode.PinTypes.Output, 120)]
  [FlowNode.Pin(200, "マルチ招待", FlowNode.PinTypes.Input, 200)]
  [FlowNode.Pin(220, "マルチ招待失敗", FlowNode.PinTypes.Output, 220)]
  public class FlowNode_ReqMultiInvitation : FlowNode_Network
  {
    public const int INPUT_ROOMINVITATION = 100;
    public const int OUTPUT_ROOMINVITATION_SUCCESS = 110;
    public const int OUTPUT_ROOMINVITATION_FAILED = 120;
    public const int INPUT_ROOMINVITATIONSEND = 200;
    public const int OUTPUT_ROOMINVITATIONSEND_SUCCESS = 210;
    public const int OUTPUT_ROOMINVITATIONSEND_FAILED = 220;
    public const int INPUT_NOTIFYINVITATION = 300;
    public const int OUTPUT_NOTIFYINVITATION_SUCCESS = 310;
    public const int OUTPUT_NOTIFYINVITATION_FAILED = 320;
    private FlowNode_ReqMultiInvitation.ApiBase m_Api;

    public override void OnActivate(int pinID)
    {
      if (this.m_Api != null)
      {
        DebugUtility.LogError("同時に複数の通信が入ると駄目！");
      }
      else
      {
        switch (pinID)
        {
          case 100:
            this.m_Api = (FlowNode_ReqMultiInvitation.ApiBase) new FlowNode_ReqMultiInvitation.Api_RoomInvitation(this);
            break;
          case 200:
            this.m_Api = (FlowNode_ReqMultiInvitation.ApiBase) new FlowNode_ReqMultiInvitation.Api_RoomInvitationSend(this);
            break;
          case 300:
            this.m_Api = (FlowNode_ReqMultiInvitation.ApiBase) new FlowNode_ReqMultiInvitation.Api_NotifyInvitation(this);
            break;
        }
        if (this.m_Api == null)
          return;
        if (this.m_Api.Start())
          ((Behaviour) this).set_enabled(true);
        else
          this.m_Api = (FlowNode_ReqMultiInvitation.ApiBase) null;
      }
    }

    public override void OnSuccess(WWWResult www)
    {
      if (this.m_Api == null)
        return;
      this.m_Api.Complete(www);
      this.m_Api = (FlowNode_ReqMultiInvitation.ApiBase) null;
    }

    public class ApiBase
    {
      protected FlowNode_ReqMultiInvitation m_Node;
      protected RequestAPI m_Request;

      public ApiBase(FlowNode_ReqMultiInvitation node)
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

    public class Api_NotifyInvitation : FlowNode_ReqMultiInvitation.ApiBase
    {
      public Api_NotifyInvitation(FlowNode_ReqMultiInvitation node)
        : base(node)
      {
      }

      public override string url
      {
        get
        {
          return "btl/multi/invitation";
        }
      }

      public override string req
      {
        get
        {
          StringBuilder stringBuilder = new StringBuilder(64);
          stringBuilder.Append("\"is_multi_push\":1");
          return stringBuilder.ToString();
        }
      }

      public override bool Start()
      {
        bool flag = false;
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) MonoSingleton<GameManager>.Instance, (UnityEngine.Object) null) && MonoSingleton<GameManager>.Instance.Player != null && !MonoSingleton<GameManager>.Instance.Player.MultiInvitaionFlag)
          flag = true;
        if (!flag)
          return base.Start();
        MultiInvitationBadge.isValid = false;
        this.Success();
        return false;
      }

      public override void Success()
      {
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_Node, (UnityEngine.Object) null))
          return;
        this.m_Node.ActivateOutputLinks(310);
        ((Behaviour) this.m_Node).set_enabled(false);
      }

      public override void Failed()
      {
        Network.RemoveAPI();
        Network.ResetError();
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_Node, (UnityEngine.Object) null))
          return;
        this.m_Node.ActivateOutputLinks(320);
        ((Behaviour) this.m_Node).set_enabled(false);
      }

      public override void Complete(WWWResult www)
      {
        if (Network.IsError)
        {
          MultiInvitationBadge.isValid = false;
          this.Failed();
        }
        else
        {
          DebugMenu.Log("API", this.url + ":" + www.text);
          WebAPI.JSON_BodyResponse<FlowNode_ReqMultiInvitation.Api_NotifyInvitation.Json> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<FlowNode_ReqMultiInvitation.Api_NotifyInvitation.Json>>(www.text);
          DebugUtility.Assert(jsonObject != null, "res == null");
          if (jsonObject.body != null)
            MultiInvitationReceiveWindow.SetBadge(jsonObject.body.player != null && jsonObject.body.player.multi_inv != 0);
          else
            MultiInvitationReceiveWindow.SetBadge(false);
          Network.RemoveAPI();
          this.Success();
        }
      }

      [Serializable]
      public class Player
      {
        public int multi_inv;
      }

      [Serializable]
      public class Json
      {
        public FlowNode_ReqMultiInvitation.Api_NotifyInvitation.Player player;
      }
    }

    public class Api_RoomInvitation : FlowNode_ReqMultiInvitation.ApiBase
    {
      public Api_RoomInvitation(FlowNode_ReqMultiInvitation node)
        : base(node)
      {
      }

      public override string url
      {
        get
        {
          return "btl/room/invitation";
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
          WebAPI.JSON_BodyResponse<FlowNode_ReqMultiInvitation.Api_RoomInvitation.Json> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<FlowNode_ReqMultiInvitation.Api_RoomInvitation.Json>>(www.text);
          DebugUtility.Assert(jsonObject != null, "res == null");
          if (jsonObject.body != null)
          {
            MultiInvitationReceiveWindow instance = MultiInvitationReceiveWindow.instance;
            if (instance != null)
              instance.DeserializeActiveList(jsonObject.body);
          }
          Network.RemoveAPI();
          this.Success();
        }
      }

      [Serializable]
      public class JsonRoomOwner
      {
        public string name;
        public string fuid;
        public Json_Unit[] units;
      }

      [Serializable]
      public class JsonRoomQuest
      {
        public string iname;
      }

      [Serializable]
      public class JsonRoom
      {
        public int roomid;
        public string comment;
        public int num;
        public FlowNode_ReqMultiInvitation.Api_RoomInvitation.JsonRoomOwner owner;
        public FlowNode_ReqMultiInvitation.Api_RoomInvitation.JsonRoomQuest quest;
        public string pwd_hash;
        public int unitlv;
        public int clear;
        public int limit;
        public string btype;
      }

      [Serializable]
      public class Json
      {
        public FlowNode_ReqMultiInvitation.Api_RoomInvitation.JsonRoom[] rooms;
      }
    }

    public class Api_RoomInvitationSend : FlowNode_ReqMultiInvitation.ApiBase
    {
      private int m_RoomType;
      private int m_RoomId;
      private string[] m_Sends;

      public Api_RoomInvitationSend(FlowNode_ReqMultiInvitation node)
        : base(node)
      {
        this.m_RoomType = GlobalVars.SelectedMultiPlayRoomType != JSON_MyPhotonRoomParam.EType.TOWER ? 0 : 1;
        this.m_RoomId = GlobalVars.SelectedMultiPlayRoomID;
        MultiInvitationSendWindow instance = MultiInvitationSendWindow.instance;
        if (instance == null)
          return;
        this.m_Sends = instance.GetSendList();
      }

      public override string url
      {
        get
        {
          return "btl/room/invitation/send";
        }
      }

      public override string req
      {
        get
        {
          StringBuilder stringBuilder = new StringBuilder(128);
          stringBuilder.Append("\"roomid\":" + (object) this.m_RoomId);
          stringBuilder.Append(",\"btype\":" + (this.m_RoomType != 0 ? "\"multi_tower\"" : "\"multi\""));
          stringBuilder.Append(",\"send_uids\":[");
          if (this.m_Sends != null)
          {
            for (int index = 0; index < this.m_Sends.Length; ++index)
              stringBuilder.Append((index <= 0 ? string.Empty : ",") + "\"" + this.m_Sends[index] + "\"");
          }
          stringBuilder.Append("]");
          return stringBuilder.ToString();
        }
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
          this.m_Node.OnFailed();
        }
        else
        {
          DebugMenu.Log("API", this.url + ":" + www.text);
          DebugUtility.Assert((WebAPI.JSON_BodyResponse<FlowNode_ReqMultiInvitation.Api_RoomInvitationSend.Json>) JsonUtility.FromJson<WebAPI.JSON_BodyResponse<FlowNode_ReqMultiInvitation.Api_RoomInvitationSend.Json>>(www.text) != null, "res == null");
          if (this.m_Sends != null)
          {
            for (int index = 0; index < this.m_Sends.Length; ++index)
              MultiInvitationSendWindow.AddInvited(this.m_Sends[index]);
          }
          Network.RemoveAPI();
          this.Success();
        }
      }

      [Serializable]
      public class Json
      {
        public bool result;
      }
    }
  }
}
