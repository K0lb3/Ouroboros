// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqHealAp
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(1, "Success", FlowNode.PinTypes.Output, 1)]
  [FlowNode.NodeType("System/ReqHealAp", 32741)]
  [FlowNode.Pin(0, "ReqHealAp", FlowNode.PinTypes.Input, 0)]
  public class FlowNode_ReqHealAp : FlowNode_Network
  {
    public override void OnSuccess(WWWResult www)
    {
      WebAPI.JSON_BodyResponse<Json_ArenaPlayerDataAll> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_ArenaPlayerDataAll>>(www.text);
      if (jsonObject.body == null)
      {
        this.OnRetry();
      }
      else
      {
        Network.RemoveAPI();
        MonoSingleton<GameManager>.Instance.Player.Deserialize(jsonObject.body.player);
        MonoSingleton<GameManager>.Instance.Player.Deserialize(jsonObject.body.items);
        ((HealAp) ((Component) this).get_gameObject().GetComponent<HealAp>()).now_ap.set_text(MonoSingleton<GameManager>.Instance.Player.Stamina.ToString());
        MonoSingleton<MySound>.Instance.PlaySEOneShot("SE_0011", 0.0f);
        this.ActivateOutputLinks(1);
      }
    }

    private void Start()
    {
    }

    private void Update()
    {
    }

    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      ItemData dataOfClass = DataSource.FindDataOfClass<ItemData>(((Component) this).get_gameObject(), (ItemData) null);
      HealAp component = (HealAp) ((Component) this).get_gameObject().GetComponent<HealAp>();
      if (dataOfClass == null)
        return;
      this.ExecRequest((WebAPI) new ReqHealAp(dataOfClass.UniqueID, component.bar.UseItemNum, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
    }
  }
}
