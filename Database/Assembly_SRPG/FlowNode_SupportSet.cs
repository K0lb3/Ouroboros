// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_SupportSet
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(0, "Request", FlowNode.PinTypes.Input, 0)]
  [FlowNode.NodeType("System/SupportSet", 32741)]
  [FlowNode.Pin(1, "Success", FlowNode.PinTypes.Output, 1)]
  public class FlowNode_SupportSet : FlowNode_Network
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
        DataSource.Bind<UnitData>(this.UnitParent, MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID((long) GlobalVars.SelectedSupportUnitUniqueID));
        GameParameter.UpdateAll(((Component) this).get_gameObject());
        Network.RemoveAPI();
        this.ActivateOutputLinks(1);
      }
    }

    public override void OnActivate(int pinID)
    {
      this.ExecRequest((WebAPI) new ReqSetSupport(MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID((long) GlobalVars.SelectedSupportUnitUniqueID).UniqueID, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
    }

    private void OnUnitSelect(long uniqueID)
    {
    }
  }
}
