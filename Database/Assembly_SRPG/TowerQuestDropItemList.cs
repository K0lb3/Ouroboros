// Decompiled with JetBrains decompiler
// Type: SRPG.TowerQuestDropItemList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
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
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.ItemTemplate, (UnityEngine.Object) null))
        return;
      for (int index = this.mItems.Count - 1; index >= 0; --index)
        UnityEngine.Object.Destroy((UnityEngine.Object) this.mItems[index]);
      QuestParam dataOfClass = DataSource.FindDataOfClass<QuestParam>(((Component) this).get_gameObject(), (QuestParam) null);
      if (dataOfClass == null)
        return;
      Transform transform = ((Component) this).get_transform();
      List<TowerRewardItem> towerRewardItem = MonoSingleton<GameManager>.Instance.FindTowerReward(MonoSingleton<GameManager>.Instance.FindTowerFloor(dataOfClass.iname).reward_id).GetTowerRewardItem();
      for (int index = 0; index < towerRewardItem.Count; ++index)
      {
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: variable of a compiler-generated type
        TowerQuestDropItemList.\u003CRefresh\u003Ec__AnonStorey37F refreshCAnonStorey37F = new TowerQuestDropItemList.\u003CRefresh\u003Ec__AnonStorey37F();
        // ISSUE: reference to a compiler-generated field
        refreshCAnonStorey37F.item = towerRewardItem[index];
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        if (refreshCAnonStorey37F.item.visible && refreshCAnonStorey37F.item.type != TowerRewardItem.RewardType.Gold)
        {
          GameObject gameObject = (GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) this.ItemTemplate);
          gameObject.get_transform().SetParent(transform, false);
          gameObject.get_transform().set_localScale(this.ItemTemplate.get_transform().get_localScale());
          // ISSUE: reference to a compiler-generated field
          DataSource.Bind<TowerRewardItem>(gameObject, refreshCAnonStorey37F.item);
          gameObject.SetActive(true);
          foreach (GameParameter componentsInChild in (GameParameter[]) gameObject.GetComponentsInChildren<GameParameter>())
            componentsInChild.Index = index;
          TowerRewardUI componentInChildren1 = (TowerRewardUI) gameObject.GetComponentInChildren<TowerRewardUI>();
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) componentInChildren1, (UnityEngine.Object) null))
            componentInChildren1.Refresh();
          // ISSUE: reference to a compiler-generated field
          if (refreshCAnonStorey37F.item.type == TowerRewardItem.RewardType.Artifact)
          {
            // ISSUE: reference to a compiler-generated field
            ArtifactParam artifactParam = MonoSingleton<GameManager>.Instance.MasterParam.GetArtifactParam(refreshCAnonStorey37F.item.iname);
            DataSource.Bind<ArtifactParam>(gameObject, artifactParam);
            ArtifactIcon componentInChildren2 = (ArtifactIcon) gameObject.GetComponentInChildren<ArtifactIcon>();
            if (UnityEngine.Object.op_Equality((UnityEngine.Object) componentInChildren2, (UnityEngine.Object) null))
              break;
            ((Behaviour) componentInChildren2).set_enabled(true);
            componentInChildren2.UpdateValue();
            // ISSUE: reference to a compiler-generated method
            if (MonoSingleton<GameManager>.Instance.Player.Artifacts.Find(new Predicate<ArtifactData>(refreshCAnonStorey37F.\u003C\u003Em__424)) != null)
              break;
            // ISSUE: reference to a compiler-generated field
            refreshCAnonStorey37F.item.is_new = true;
            break;
          }
        }
      }
    }
  }
}
