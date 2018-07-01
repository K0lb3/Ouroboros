// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_MultiTowerShowDetail
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(1, "Success", FlowNode.PinTypes.Output, 1)]
  [FlowNode.Pin(0, "Start", FlowNode.PinTypes.Input, 0)]
  [FlowNode.NodeType("Multi/MultiTowerShowDetail", 32741)]
  public class FlowNode_MultiTowerShowDetail : FlowNode
  {
    [SerializeField]
    private GameObject DetailObject;

    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      this.OnClickDetail();
      this.ActivateOutputLinks(1);
    }

    public void OnClickDetail()
    {
      QuestParam dataOfClass = DataSource.FindDataOfClass<QuestParam>(((Component) this).get_gameObject(), (QuestParam) null);
      MultiTowerFloorParam data = DataSource.FindDataOfClass<MultiTowerFloorParam>(((Component) this).get_gameObject(), (MultiTowerFloorParam) null) ?? MonoSingleton<GameManager>.Instance.GetMTFloorParam(GlobalVars.SelectedQuestID);
      if (!Object.op_Inequality((Object) this.DetailObject, (Object) null) || dataOfClass == null)
        return;
      GameObject gameObject = (GameObject) Object.Instantiate<GameObject>((M0) this.DetailObject);
      DataSource.Bind<QuestParam>(gameObject, dataOfClass);
      QuestCampaignData[] questCampaigns = MonoSingleton<GameManager>.Instance.FindQuestCampaigns(dataOfClass);
      DataSource.Bind<QuestCampaignData[]>(gameObject, questCampaigns.Length != 0 ? questCampaigns : (QuestCampaignData[]) null);
      DataSource.Bind<QuestParam>(gameObject, dataOfClass);
      DataSource.Bind<MultiTowerFloorParam>(gameObject, data);
      MultiTowerQuestInfo component = (MultiTowerQuestInfo) gameObject.GetComponent<MultiTowerQuestInfo>();
      if (!Object.op_Inequality((Object) component, (Object) null))
        return;
      component.Refresh();
    }
  }
}
