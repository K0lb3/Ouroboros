// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqUnitFavorite
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(200, "お気に入り設定失敗", FlowNode.PinTypes.Output, 200)]
  [FlowNode.Pin(130, "お気に入り設定OFF完了", FlowNode.PinTypes.Output, 130)]
  [FlowNode.Pin(120, "お気に入り設定ON完了", FlowNode.PinTypes.Output, 120)]
  [FlowNode.Pin(110, "お気に入りOFF", FlowNode.PinTypes.Input, 110)]
  [FlowNode.Pin(100, "お気に入りON", FlowNode.PinTypes.Input, 100)]
  [FlowNode.NodeType("System/WebApi/ReqUnitFavorite", 32741)]
  public class FlowNode_ReqUnitFavorite : FlowNode_Network
  {
    private FlowNode_ReqUnitFavorite.ApiBase m_Api;

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
            this.m_Api = (FlowNode_ReqUnitFavorite.ApiBase) new FlowNode_ReqUnitFavorite.Api_SetUnitFavorite(this, true);
            break;
          case 110:
            this.m_Api = (FlowNode_ReqUnitFavorite.ApiBase) new FlowNode_ReqUnitFavorite.Api_SetUnitFavorite(this, false);
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
      this.m_Api = (FlowNode_ReqUnitFavorite.ApiBase) null;
    }

    public class ApiBase
    {
      protected FlowNode_ReqUnitFavorite m_Node;
      protected RequestAPI m_Request;

      public ApiBase(FlowNode_ReqUnitFavorite node)
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

    public class Api_SetUnitFavorite : FlowNode_ReqUnitFavorite.ApiBase
    {
      private List<long> m_OnUniqId = new List<long>();
      private List<long> m_OffUniqId = new List<long>();

      public Api_SetUnitFavorite(FlowNode_ReqUnitFavorite node, bool value)
        : base(node)
      {
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) UnitEnhanceV3.Instance, (UnityEngine.Object) null) || UnitEnhanceV3.Instance.CurrentUnit == null)
          return;
        if (value)
          this.m_OnUniqId.Add(UnitEnhanceV3.Instance.CurrentUnit.UniqueID);
        else
          this.m_OffUniqId.Add(UnitEnhanceV3.Instance.CurrentUnit.UniqueID);
      }

      public override string url
      {
        get
        {
          return "unit/favorite";
        }
      }

      public override string req
      {
        get
        {
          StringBuilder stringBuilder = new StringBuilder(128);
          stringBuilder.Append("\"favids\":[");
          for (int index = 0; index < this.m_OnUniqId.Count; ++index)
            stringBuilder.Append((index <= 0 ? string.Empty : ",") + this.m_OnUniqId[index].ToString());
          stringBuilder.Append("]");
          stringBuilder.Append(",\"unfavids\":[");
          for (int index = 0; index < this.m_OffUniqId.Count; ++index)
            stringBuilder.Append((index <= 0 ? string.Empty : ",") + this.m_OffUniqId[index].ToString());
          stringBuilder.Append("]");
          DebugMenu.Log("API", stringBuilder.ToString());
          return stringBuilder.ToString();
        }
      }

      public override void Success()
      {
        if (this.m_OnUniqId.Count > 0)
          this.m_Node.ActivateOutputLinks(120);
        else if (this.m_OffUniqId.Count > 0)
          this.m_Node.ActivateOutputLinks(130);
        else
          this.m_Node.ActivateOutputLinks(200);
        ((Behaviour) this.m_Node).set_enabled(false);
      }

      public override void Failed()
      {
        this.m_Node.ActivateOutputLinks(200);
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
          WebAPI.JSON_BodyResponse<FlowNode_ReqUnitFavorite.Api_SetUnitFavorite.Json> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<FlowNode_ReqUnitFavorite.Api_SetUnitFavorite.Json>>(www.text);
          DebugUtility.Assert(jsonObject != null, "res == null");
          if (jsonObject.body != null && jsonObject.body.units != null)
          {
            PlayerData player = MonoSingleton<GameManager>.Instance.Player;
            if (player != null)
            {
              for (int index = 0; index < jsonObject.body.units.Length; ++index)
              {
                UnitData unitData = player.GetUnitData(jsonObject.body.units[index].iid);
                if (unitData != null)
                  unitData.IsFavorite = jsonObject.body.units[index].fav == 1;
              }
            }
          }
          Network.RemoveAPI();
          this.Success();
        }
      }

      [Serializable]
      public class Json
      {
        public Json_Unit[] units;
      }
    }
  }
}
