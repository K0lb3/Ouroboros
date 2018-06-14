// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqSupport
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  [FlowNode.NodeType("System/ReqSupport", 32741)]
  [FlowNode.Pin(0, "Request", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Success", FlowNode.PinTypes.Output, 1)]
  public class FlowNode_ReqSupport : FlowNode_Network
  {
    public GameObject UnitParent;

    public override void OnSuccess(WWWResult www)
    {
      if (Network.IsError)
      {
        Network.EErrCode errCode = Network.ErrCode;
        this.OnFailed();
      }
      else
      {
        WebAPI.JSON_BodyResponse<Json_SupportSet> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_SupportSet>>(www.text);
        UnitData unitData = new UnitData();
        unitData.Deserialize(jsonObject.body.unit);
        GlobalVars.SelectedSupportUnitUniqueID.Set(unitData.UniqueID);
        GameParameter.UpdateAll(((Component) this).get_gameObject());
        Network.RemoveAPI();
        this.ActivateOutputLinks(1);
      }
    }

    public override void OnActivate(int pinID)
    {
      this.ExecRequest((WebAPI) new ReqGetSupport(new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
    }
  }
}
