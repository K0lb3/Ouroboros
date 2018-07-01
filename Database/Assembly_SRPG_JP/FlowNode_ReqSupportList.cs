// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqSupportList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(820, "傭兵リスト取得失敗", FlowNode.PinTypes.Output, 820)]
  [FlowNode.NodeType("System/ReqSupportList")]
  [FlowNode.Pin(0, "傭兵リスト取得", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(10, "傭兵リスト取得(強制)", FlowNode.PinTypes.Input, 10)]
  [FlowNode.Pin(810, "傭兵リスト取得成功", FlowNode.PinTypes.Output, 810)]
  public class FlowNode_ReqSupportList : FlowNode_Network
  {
    public const int INPUT_GETLIST = 0;
    public const int INPUT_GETLIST_FORCE = 10;
    public const int OUTPUT_SUCCESS = 810;
    public const int OUTPUT_FAILED = 820;
    public UnitListWindow m_Window;
    private FlowNode_ReqSupportList.ApiBase m_Api;

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
          case 0:
            this.m_Api = (FlowNode_ReqSupportList.ApiBase) new FlowNode_ReqSupportList.Api_SupportList(this, this.m_Window.rootWindow.GetElement(), false);
            break;
          case 10:
            this.m_Api = (FlowNode_ReqSupportList.ApiBase) new FlowNode_ReqSupportList.Api_SupportList(this, this.m_Window.rootWindow.GetElement(), true);
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
      this.m_Api = (FlowNode_ReqSupportList.ApiBase) null;
    }

    public class ApiBase
    {
      protected FlowNode_ReqSupportList m_Node;
      protected RequestAPI m_Request;

      public ApiBase(FlowNode_ReqSupportList node)
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

    public class Api_SupportList : FlowNode_ReqSupportList.ApiBase
    {
      private List<SupportData> m_Select = new List<SupportData>();
      private EElement m_Element;
      private bool m_IsForce;

      public Api_SupportList(FlowNode_ReqSupportList node, EElement element, bool isForce)
        : base(node)
      {
        this.m_Element = element;
        this.m_IsForce = isForce;
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) node.m_Window, (UnityEngine.Object) null))
          return;
        QuestParam data1 = node.m_Window.GetData<QuestParam>("data_quest");
        SupportData[] data2 = node.m_Window.GetData<SupportData[]>("data_support");
        if (data1 != null && data1.type == QuestTypes.Ordeal)
        {
          foreach (SupportData supportData in data2)
          {
            if (supportData != null)
              this.m_Select.Add(supportData);
          }
        }
        else
        {
          int data3 = node.m_Window.GetData<int>("data_party_index", -1);
          if (data2[data3] == null)
            return;
          this.m_Select.Add(data2[data3]);
        }
      }

      public Api_SupportList(FlowNode_ReqSupportList node, EElement element)
        : base(node)
      {
        this.m_Element = element;
      }

      public override string url
      {
        get
        {
          return this.m_Element == EElement.None ? "btl/com/supportlist" : "btl/com/support_elem";
        }
      }

      public override string req
      {
        get
        {
          StringBuilder stringBuilder = new StringBuilder(128);
          stringBuilder.Append("\"elem\":" + (object) this.m_Element);
          stringBuilder.Append(",\"is_update\":" + (!this.m_IsForce ? "1" : "0"));
          if (this.m_Select != null && this.m_Select.Count > 0)
          {
            stringBuilder.Append(",\"helps\":[");
            for (int index = 0; index < this.m_Select.Count; ++index)
            {
              if (index > 0)
                stringBuilder.Append(",");
              stringBuilder.Append("{");
              stringBuilder.Append("\"fuid\":\"" + this.m_Select[index].FUID + "\"");
              stringBuilder.Append(",\"elem\":" + (object) this.m_Select[index].Unit.SupportElement);
              stringBuilder.Append(",\"iname\":\"" + this.m_Select[index].Unit.UnitID + "\"");
              stringBuilder.Append("}");
            }
            stringBuilder.Append("]");
          }
          return stringBuilder.ToString();
        }
      }

      public override void Success()
      {
        this.m_Node.ActivateOutputLinks(810);
        ((Behaviour) this.m_Node).set_enabled(false);
      }

      public override void Failed()
      {
        this.m_Node.ActivateOutputLinks(820);
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
          WebAPI.JSON_BodyResponse<FlowNode_ReqSupportList.Api_SupportList.Json> jsonBodyResponse = (WebAPI.JSON_BodyResponse<FlowNode_ReqSupportList.Api_SupportList.Json>) JsonUtility.FromJson<WebAPI.JSON_BodyResponse<FlowNode_ReqSupportList.Api_SupportList.Json>>(www.text);
          DebugUtility.Assert(jsonBodyResponse != null, "res == null");
          if (jsonBodyResponse.body != null)
          {
            FlowNode_ReqSupportList.SupportList supportList = new FlowNode_ReqSupportList.SupportList(this.m_Element);
            supportList.Deserialize(jsonBodyResponse.body.supports);
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_Node.m_Window, (UnityEngine.Object) null) && this.m_Node.m_Window.rootWindow != null)
              this.m_Node.m_Window.rootWindow.AddData("data_supportres", (object) supportList);
          }
          Network.RemoveAPI();
          this.Success();
        }
      }

      [Serializable]
      public class Json
      {
        public Json_Support[] supports;
      }
    }

    public class SupportList
    {
      public EElement m_Element;
      public SupportData[] m_SupportData;

      public SupportList(EElement element)
      {
        this.m_Element = element;
      }

      public void Deserialize(Json_Support[] json)
      {
        this.m_SupportData = new SupportData[json.Length];
        for (int index = 0; index < json.Length; ++index)
        {
          SupportData supportData = new SupportData();
          try
          {
            supportData.Deserialize(json[index]);
            this.m_SupportData[index] = supportData;
          }
          catch (Exception ex)
          {
            DebugUtility.LogException(ex);
          }
        }
      }
    }
  }
}
