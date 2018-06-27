// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqWishList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Text;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(100, "Get", FlowNode.PinTypes.Input, 100)]
  [FlowNode.Pin(110, "ウィッシュリスト取得完了", FlowNode.PinTypes.Output, 110)]
  [FlowNode.Pin(120, "ウィッシュリスト取得失敗", FlowNode.PinTypes.Output, 120)]
  [FlowNode.Pin(200, "Set", FlowNode.PinTypes.Input, 200)]
  [FlowNode.Pin(210, "ウィッシュリスト設定完了", FlowNode.PinTypes.Output, 210)]
  [FlowNode.Pin(220, "ウィッシュリスト設定失敗", FlowNode.PinTypes.Output, 220)]
  [FlowNode.NodeType("System/WebApi/WishList", 32741)]
  public class FlowNode_ReqWishList : FlowNode_Network
  {
    private FlowNode_ReqWishList.ApiBase m_Api;

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
            this.m_Api = (FlowNode_ReqWishList.ApiBase) new FlowNode_ReqWishList.Api_WishList(this);
            break;
          case 200:
            FriendPresentRootWindow.WantContent.ItemAccessor clickItem1 = FriendPresentRootWindow.WantContent.clickItem;
            FriendPresentWantWindow.Content.ItemAccessor clickItem2 = FriendPresentWantWindow.Content.clickItem;
            if (clickItem1 != null && clickItem2 != null)
            {
              this.m_Api = (FlowNode_ReqWishList.ApiBase) new FlowNode_ReqWishList.Api_WishListSet(this, clickItem2.presentId, clickItem1.priority);
              break;
            }
            break;
        }
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
      this.m_Api = (FlowNode_ReqWishList.ApiBase) null;
    }

    public class ApiBase
    {
      protected FlowNode_ReqWishList m_Node;
      protected RequestAPI m_Request;

      public ApiBase(FlowNode_ReqWishList node)
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
          this.m_Node.ExecRequest((WebAPI) new RequestAPI(this.url, new Network.ResponseCallback(((FlowNode_Network) this.m_Node).ResponseCallback), this.req));
        else
          this.Failed();
      }
    }

    public class Api_WishList : FlowNode_ReqWishList.ApiBase
    {
      public Api_WishList(FlowNode_ReqWishList node)
        : base(node)
      {
      }

      public override string url
      {
        get
        {
          return "wishlist";
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
          WebAPI.JSON_BodyResponse<FriendPresentWishList.Json[]> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<FriendPresentWishList.Json[]>>(www.text);
          DebugUtility.Assert(jsonObject != null, "res == null");
          if (jsonObject.body != null)
            MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body);
          Network.RemoveAPI();
          this.Success();
        }
      }
    }

    public class Api_WishListSet : FlowNode_ReqWishList.ApiBase
    {
      private string m_Id;
      private int m_Priority;

      public Api_WishListSet(FlowNode_ReqWishList node, string iname, int priority)
        : base(node)
      {
        this.m_Id = iname;
        this.m_Priority = priority;
      }

      public override string url
      {
        get
        {
          return "wishlist/set";
        }
      }

      public override string req
      {
        get
        {
          StringBuilder stringBuilder = new StringBuilder(128);
          stringBuilder.Append("\"iname\":\"" + this.m_Id + "\"");
          stringBuilder.Append(",\"priority\":" + (object) (this.m_Priority + 1));
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
          WebAPI.JSON_BodyResponse<FlowNode_ReqWishList.Api_WishListSet.Json> jsonBodyResponse = (WebAPI.JSON_BodyResponse<FlowNode_ReqWishList.Api_WishListSet.Json>) JsonUtility.FromJson<WebAPI.JSON_BodyResponse<FlowNode_ReqWishList.Api_WishListSet.Json>>(www.text);
          DebugUtility.Assert(jsonBodyResponse != null, "res == null");
          if (jsonBodyResponse.body != null && jsonBodyResponse.body.result)
            MonoSingleton<GameManager>.Instance.Player.SetWishList(this.m_Id, this.m_Priority);
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
