// Decompiled with JetBrains decompiler
// Type: SRPG.TowerQuestDropItemList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

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
        TowerRewardItem item = towerRewardItem[index];
        if (item.visible && item.type != TowerRewardItem.RewardType.Gold)
        {
          GameObject gameObject = (GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) this.ItemTemplate);
          gameObject.get_transform().SetParent(transform, false);
          gameObject.get_transform().set_localScale(this.ItemTemplate.get_transform().get_localScale());
          DataSource.Bind<TowerRewardItem>(gameObject, item);
          gameObject.SetActive(true);
          foreach (GameParameter componentsInChild in (GameParameter[]) gameObject.GetComponentsInChildren<GameParameter>())
            componentsInChild.Index = index;
          TowerRewardUI componentInChildren1 = (TowerRewardUI) gameObject.GetComponentInChildren<TowerRewardUI>();
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) componentInChildren1, (UnityEngine.Object) null))
            componentInChildren1.Refresh();
          if (item.type == TowerRewardItem.RewardType.Artifact)
          {
            ArtifactParam artifactParam = MonoSingleton<GameManager>.Instance.MasterParam.GetArtifactParam(item.iname);
            DataSource.Bind<ArtifactParam>(gameObject, artifactParam);
            ArtifactIcon componentInChildren2 = (ArtifactIcon) gameObject.GetComponentInChildren<ArtifactIcon>();
            if (UnityEngine.Object.op_Equality((UnityEngine.Object) componentInChildren2, (UnityEngine.Object) null))
              break;
            ((Behaviour) componentInChildren2).set_enabled(true);
            componentInChildren2.UpdateValue();
            if (MonoSingleton<GameManager>.Instance.Player.Artifacts.Find((Predicate<ArtifactData>) (x => x.ArtifactParam.iname == item.iname)) != null)
              break;
            item.is_new = true;
            break;
          }
        }
      }
    }
  }
}
