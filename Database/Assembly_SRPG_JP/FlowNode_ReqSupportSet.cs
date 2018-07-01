// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqSupportSet
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System;
using System.Text;
using UnityEngine;

namespace SRPG
{
  [FlowNode.NodeType("System/ReqSupportSet", 32741)]
  [FlowNode.Pin(100, "傭兵設定", FlowNode.PinTypes.Input, 100)]
  [FlowNode.Pin(110, "傭兵設定成功", FlowNode.PinTypes.Output, 110)]
  [FlowNode.Pin(120, "傭兵設定失敗", FlowNode.PinTypes.Output, 120)]
  public class FlowNode_ReqSupportSet : FlowNode_Network
  {
    public const int INPUT_SUPPORT_SET = 100;
    public const int OUTPUT_SUPPORT_SET_SUCCESS = 110;
    public const int OUTPUT_SUPPORT_SET_FAILED = 120;
    [SerializeField]
    private SupportElementListRootWindow m_TargetWindow;
    private FlowNode_ReqSupportSet.ApiBase m_Api;

    public override void OnActivate(int pinID)
    {
      if (this.m_Api != null)
      {
        DebugUtility.LogError("同時に複数の通信が入ると駄目！");
      }
      else
      {
        this.m_Api = (FlowNode_ReqSupportSet.ApiBase) new FlowNode_ReqSupportSet.Api_SupportSet(this, this.m_TargetWindow.GetSupportUnitData());
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
      this.m_Api = (FlowNode_ReqSupportSet.ApiBase) null;
    }

    public class ApiBase
    {
      protected FlowNode_ReqSupportSet m_Node;
      protected RequestAPI m_Request;

      public ApiBase(FlowNode_ReqSupportSet node)
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

    public class Api_SupportSet : FlowNode_ReqSupportSet.ApiBase
    {
      private FlowNode_ReqSupportSet.OwnSupportData[] m_SupportData;

      public Api_SupportSet(FlowNode_ReqSupportSet node, FlowNode_ReqSupportSet.OwnSupportData[] ownSupportData)
        : base(node)
      {
        this.m_SupportData = ownSupportData;
      }

      public override string url
      {
        get
        {
          return "support/set";
        }
      }

      public override string req
      {
        get
        {
          StringBuilder stringBuilder = new StringBuilder(128);
          stringBuilder.Append("\"units\":[");
          for (int index = 0; index < this.m_SupportData.Length; ++index)
          {
            if (this.m_SupportData[index] != null)
            {
              if (index != 0)
                stringBuilder.Append(",");
              stringBuilder.Append("{");
              stringBuilder.Append("\"id\":");
              stringBuilder.Append(this.m_SupportData[index].m_UniqueID);
              stringBuilder.Append(",\"elem\":");
              stringBuilder.Append((int) this.m_SupportData[index].m_Element);
              stringBuilder.Append("}");
            }
          }
          stringBuilder.Append("]");
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
          this.Failed();
        }
        else
        {
          for (int index = 0; index < this.m_SupportData.Length; ++index)
          {
            if (this.m_SupportData[index] != null && this.m_SupportData[index].m_Element == EElement.None)
            {
              GlobalVars.SelectedSupportUnitUniqueID.Set(this.m_SupportData[index].m_UniqueID);
              break;
            }
          }
          Network.RemoveAPI();
          this.Success();
        }
      }

      [Serializable]
      public class Json_OwnSupportData
      {
        public long id;
        public int elem;
      }

      [Serializable]
      public class Json
      {
        public FlowNode_ReqSupportSet.Api_SupportSet.Json_OwnSupportData[] units;
      }
    }

    public class OwnSupportData
    {
      public long m_UniqueID;
      public EElement m_Element;
    }
  }
}
