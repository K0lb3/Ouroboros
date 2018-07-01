// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqShopLineup
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(0, "Request", FlowNode.PinTypes.Input, 0)]
  [FlowNode.NodeType("System/ReqShopLineup", 32741)]
  [FlowNode.Pin(1, "Success", FlowNode.PinTypes.Output, 1)]
  public class FlowNode_ReqShopLineup : FlowNode_Network
  {
    private EShopType mShopType;
    [SerializeField]
    private GameObject Target;

    public override void OnActivate(int pinID)
    {
      if (pinID != 0 || ((Behaviour) this).get_enabled())
        return;
      string gname;
      switch (GlobalVars.ShopType)
      {
        case EShopType.Event:
          gname = GlobalVars.EventShopItem.shops.gname;
          break;
        case EShopType.Limited:
          gname = GlobalVars.LimitedShopItem.shops.gname;
          break;
        default:
          this.mShopType = GlobalVars.ShopType;
          gname = this.mShopType.ToString();
          break;
      }
      this.ExecRequest((WebAPI) new ReqShopLineup(gname, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
      ((Behaviour) this).set_enabled(true);
    }

    private void Success()
    {
      ((Behaviour) this).set_enabled(false);
      this.ActivateOutputLinks(1);
    }

    public override void OnSuccess(WWWResult www)
    {
      if (Network.IsError)
      {
        Network.EErrCode errCode = Network.ErrCode;
        this.OnRetry();
      }
      else
      {
        WebAPI.JSON_BodyResponse<Json_ShopLineup> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_ShopLineup>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        if (jsonObject.body == null)
        {
          this.OnRetry();
        }
        else
        {
          Network.RemoveAPI();
          if (Object.op_Equality((Object) this.Target, (Object) null))
            return;
          ShopLineupWindow component = (ShopLineupWindow) this.Target.GetComponent<ShopLineupWindow>();
          if (Object.op_Equality((Object) component, (Object) null))
            return;
          component.SetItemInames(jsonObject.body.shopitems);
          this.Success();
        }
      }
    }
  }
}
