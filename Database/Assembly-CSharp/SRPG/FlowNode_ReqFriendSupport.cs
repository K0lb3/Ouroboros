// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqFriendSupport
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using System.Text;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(110, "傭兵取得成功", FlowNode.PinTypes.Output, 110)]
  [FlowNode.NodeType("System/ReqFriendSupport", 32741)]
  [FlowNode.Pin(100, "傭兵取得", FlowNode.PinTypes.Input, 100)]
  [FlowNode.Pin(120, "傭兵取得失敗", FlowNode.PinTypes.Output, 120)]
  public class FlowNode_ReqFriendSupport : FlowNode_Network
  {
    public const int INPUT_FRIEND_SUPPORT = 100;
    public const int OUTPUT_FRIEND_SUPPORT_SUCCESS = 110;
    public const int OUTPUT_FRIEND_SUPPORT_FAILED = 120;
    [SerializeField]
    private SerializeValueBehaviour m_SerializeValue;
    private FlowNode_ReqFriendSupport.ApiBase m_Api;

    public override void OnActivate(int pinID)
    {
      if (this.m_Api != null)
      {
        DebugUtility.LogError("同時に複数の通信が入ると駄目！");
      }
      else
      {
        this.m_Api = (FlowNode_ReqFriendSupport.ApiBase) new FlowNode_ReqFriendSupport.Api_FriendSupport(this, !UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_SerializeValue, (UnityEngine.Object) null) ? new SerializeValueList() : this.m_SerializeValue.list);
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
      this.m_Api = (FlowNode_ReqFriendSupport.ApiBase) null;
    }

    public class ApiBase
    {
      protected FlowNode_ReqFriendSupport m_Node;
      protected RequestAPI m_Request;

      public ApiBase(FlowNode_ReqFriendSupport node)
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

    public class Api_FriendSupport : FlowNode_ReqFriendSupport.ApiBase
    {
      private SerializeValueList m_ValueList;

      public Api_FriendSupport(FlowNode_ReqFriendSupport node, SerializeValueList valueList)
        : base(node)
      {
        this.m_ValueList = valueList;
      }

      public override string url
      {
        get
        {
          return "friend/support";
        }
      }

      public override string req
      {
        get
        {
          StringBuilder stringBuilder = new StringBuilder(128);
          stringBuilder.Append("\"fuid\":\"");
          stringBuilder.Append(this.m_ValueList.GetString("fuid"));
          stringBuilder.Append("\"");
          return stringBuilder.ToString();
        }
      }

      public override void Success()
      {
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_Node, (UnityEngine.Object) null))
          return;
        this.m_Node.ActivateOutputLinks(110);
        ((Behaviour) this.m_Node).set_enabled(false);
      }

      public override void Failed()
      {
        Network.RemoveAPI();
        Network.ResetError();
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_Node, (UnityEngine.Object) null))
          return;
        this.m_Node.ActivateOutputLinks(120);
        ((Behaviour) this.m_Node).set_enabled(false);
      }

      public override void Complete(WWWResult www)
      {
        if (Network.IsError)
        {
          this.m_Node.OnBack();
        }
        else
        {
          DebugMenu.Log("API", this.url + ":" + www.text);
          WebAPI.JSON_BodyResponse<FlowNode_ReqFriendSupport.Api_FriendSupport.Json> jsonBodyResponse = (WebAPI.JSON_BodyResponse<FlowNode_ReqFriendSupport.Api_FriendSupport.Json>) JsonUtility.FromJson<WebAPI.JSON_BodyResponse<FlowNode_ReqFriendSupport.Api_FriendSupport.Json>>(www.text);
          DebugUtility.Assert(jsonBodyResponse != null, "res == null");
          if (jsonBodyResponse.body != null && jsonBodyResponse.body.unit != null)
          {
            UnitData[] unitDataArray = new UnitData[Enum.GetValues(typeof (EElement)).Length];
            if (jsonBodyResponse.body.units != null)
            {
              for (int index = 0; index < jsonBodyResponse.body.units.Length; ++index)
              {
                Json_Unit unit = jsonBodyResponse.body.units[index];
                int elem = unit.elem;
                if (elem >= unitDataArray.Length || elem < 0)
                {
                  DebugUtility.LogError(string.Format("不正なインデックスが属性値として指定されています。 elem = {0}", (object) elem));
                }
                else
                {
                  unitDataArray[elem] = new UnitData();
                  unitDataArray[elem].Deserialize(unit);
                }
              }
            }
            if (jsonBodyResponse.body.unit != null)
            {
              UnitData unitData = new UnitData();
              unitData.Deserialize(jsonBodyResponse.body.unit);
              unitDataArray[0] = unitData;
            }
            this.m_ValueList.SetObject("data_units", (object) unitDataArray);
          }
          Network.RemoveAPI();
          this.Success();
        }
      }

      [Serializable]
      public class Json
      {
        public Json_Unit unit;
        public Json_Unit[] units;
      }
    }
  }
}
