// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqSupport
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(100, "傭兵取得", FlowNode.PinTypes.Input, 100)]
  [FlowNode.NodeType("System/ReqSupport", 32741)]
  [FlowNode.Pin(120, "傭兵取得失敗", FlowNode.PinTypes.Output, 120)]
  [FlowNode.Pin(110, "傭兵取得成功", FlowNode.PinTypes.Output, 110)]
  public class FlowNode_ReqSupport : FlowNode_Network
  {
    public const int INPUT_SUPPORT = 100;
    public const int OUTPUT_SUPPORT_SUCCESS = 110;
    public const int OUTPUT_SUPPORT_FAILED = 120;
    [SerializeField]
    private SupportElementListRootWindow m_TargetWindow;
    private FlowNode_ReqSupport.ApiBase m_Api;

    public override void OnActivate(int pinID)
    {
      if (this.m_Api != null)
      {
        DebugUtility.LogError("同時に複数の通信が入ると駄目！");
      }
      else
      {
        this.m_Api = (FlowNode_ReqSupport.ApiBase) new FlowNode_ReqSupport.Api_Support(this);
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
      this.m_Api = (FlowNode_ReqSupport.ApiBase) null;
    }

    public class ApiBase
    {
      protected FlowNode_ReqSupport m_Node;
      protected RequestAPI m_Request;

      public ApiBase(FlowNode_ReqSupport node)
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

    public class Api_Support : FlowNode_ReqSupport.ApiBase
    {
      public Api_Support(FlowNode_ReqSupport node)
        : base(node)
      {
      }

      public override string url
      {
        get
        {
          return "support";
        }
      }

      public override string req
      {
        get
        {
          return (string) null;
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
          WebAPI.JSON_BodyResponse<FlowNode_ReqSupport.Api_Support.Json> jsonBodyResponse = (WebAPI.JSON_BodyResponse<FlowNode_ReqSupport.Api_Support.Json>) JsonUtility.FromJson<WebAPI.JSON_BodyResponse<FlowNode_ReqSupport.Api_Support.Json>>(www.text);
          DebugUtility.Assert(jsonBodyResponse != null, "res == null");
          if (jsonBodyResponse.body != null && jsonBodyResponse.body.unit != null)
          {
            long[] iids = new long[Enum.GetValues(typeof (EElement)).Length];
            if (jsonBodyResponse.body.units != null)
            {
              for (int index = 0; index < jsonBodyResponse.body.units.Length; ++index)
              {
                if (jsonBodyResponse.body.units[index] != null)
                {
                  int elem = jsonBodyResponse.body.units[index].elem;
                  iids[elem] = jsonBodyResponse.body.units[index].iid;
                }
              }
            }
            iids[0] = jsonBodyResponse.body.unit.iid;
            this.m_Node.m_TargetWindow.SetSupportUnitData(iids);
          }
          Network.RemoveAPI();
          this.Success();
        }
      }

      [Serializable]
      public class Json_OwnSupportData
      {
        public long iid;
        public int elem;
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
