// Decompiled with JetBrains decompiler
// Type: SRPG.TowerQuestDropItemList
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  public class TowerQuestDropItemList : QuestDropItemList
  {
    protected override void Refresh()
    {
      if (Object.op_Equality((Object) this.ItemTemplate, (Object) null))
        return;
      for (int index = this.mItems.Count - 1; index >= 0; --index)
        Object.Destroy((Object) this.mItems[index]);
      QuestParam dataOfClass = DataSource.FindDataOfClass<QuestParam>(((Component) this).get_gameObject(), (QuestParam) null);
      if (dataOfClass == null)
        return;
      Transform transform = ((Component) this).get_transform();
      List<TowerRewardItem> towerRewardItem = MonoSingleton<GameManager>.Instance.FindTowerReward(MonoSingleton<GameManager>.Instance.FindTowerFloor(dataOfClass.iname).reward_id).GetTowerRewardItem();
      for (int index = 0; index < towerRewardItem.Count; ++index)
      {
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: variable of a compiler-generated type
        TowerQuestDropItemList.\u003CRefresh\u003Ec__AnonStorey274 refreshCAnonStorey274 = new TowerQuestDropItemList.\u003CRefresh\u003Ec__AnonStorey274();
        // ISSUE: reference to a compiler-generated field
        refreshCAnonStorey274.item = towerRewardItem[index];
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        if (refreshCAnonStorey274.item.visible && refreshCAnonStorey274.item.type != TowerRewardItem.RewardType.Gold)
        {
          GameObject gameObject = (GameObject) Object.Instantiate<GameObject>((M0) this.ItemTemplate);
          gameObject.get_transform().SetParent(transform, false);
          gameObject.get_transform().set_localScale(this.ItemTemplate.get_transform().get_localScale());
          // ISSUE: reference to a compiler-generated field
          DataSource.Bind<TowerRewardItem>(gameObject, refreshCAnonStorey274.item);
          gameObject.SetActive(true);
          foreach (GameParameter componentsInChild in (GameParameter[]) gameObject.GetComponentsInChildren<GameParameter>())
            componentsInChild.Index = index;
          TowerRewardUI componentInChildren1 = (TowerRewardUI) gameObject.GetComponentInChildren<TowerRewardUI>();
          if (Object.op_Inequality((Object) componentInChildren1, (Object) null))
            componentInChildren1.Refresh();
          // ISSUE: reference to a compiler-generated field
          if (refreshCAnonStorey274.item.type == TowerRewardItem.RewardType.Artifact)
          {
            // ISSUE: reference to a compiler-generated field
            ArtifactParam artifactParam = MonoSingleton<GameManager>.Instance.MasterParam.GetArtifactParam(refreshCAnonStorey274.item.iname);
            DataSource.Bind<ArtifactParam>(gameObject, artifactParam);
            ArtifactIcon componentInChildren2 = (ArtifactIcon) gameObject.GetComponentInChildren<ArtifactIcon>();
            if (Object.op_Equality((Object) componentInChildren2, (Object) null))
              break;
            ((Behaviour) componentInChildren2).set_enabled(true);
            componentInChildren2.UpdateValue();
            // ISSUE: reference to a compiler-generated method
            if (MonoSingleton<GameManager>.Instance.Player.Artifacts.Find(new Predicate<ArtifactData>(refreshCAnonStorey274.\u003C\u003Em__2F3)) != null)
              break;
            // ISSUE: reference to a compiler-generated field
            refreshCAnonStorey274.item.is_new = true;
            break;
          }
        }
      }
    }
  }
}
