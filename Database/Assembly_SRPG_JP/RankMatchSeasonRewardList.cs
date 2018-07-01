// Decompiled with JetBrains decompiler
// Type: SRPG.RankMatchSeasonRewardList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class RankMatchSeasonRewardList : SRPG_ListBase
  {
    [SerializeField]
    private RankMatchClassListItem ListItem;

    protected override void Start()
    {
      base.Start();
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.ListItem, (UnityEngine.Object) null))
        return;
      ((Component) this.ListItem).get_gameObject().SetActive(false);
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.ListItem.RewardUnit, (UnityEngine.Object) null))
        return;
      this.ListItem.RewardUnit.SetActive(false);
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.ListItem.RewardItem, (UnityEngine.Object) null))
        return;
      this.ListItem.RewardItem.SetActive(false);
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.ListItem.RewardCard, (UnityEngine.Object) null))
        return;
      this.ListItem.RewardCard.SetActive(false);
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.ListItem.RewardArtifact, (UnityEngine.Object) null))
        return;
      this.ListItem.RewardArtifact.SetActive(false);
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.ListItem.RewardAward, (UnityEngine.Object) null))
        return;
      this.ListItem.RewardAward.SetActive(false);
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.ListItem.RewardGold, (UnityEngine.Object) null))
        return;
      this.ListItem.RewardGold.SetActive(false);
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.ListItem.RewardCoin, (UnityEngine.Object) null))
        return;
      this.ListItem.RewardCoin.SetActive(false);
      GameManager gm = MonoSingleton<GameManager>.Instance;
      GlobalVars.RankMatchSeasonReward.ForEach((Action<List<VersusRankReward>>) (vrc =>
      {
        RankMatchClassListItem item = (RankMatchClassListItem) UnityEngine.Object.Instantiate<RankMatchClassListItem>((M0) this.ListItem);
        this.AddItem((ListItemEvents) item);
        ((Component) item).get_transform().SetParent(((Component) this).get_transform(), false);
        ((Component) item).get_gameObject().SetActive(true);
        vrc.ForEach((Action<VersusRankReward>) (reward =>
        {
          bool flag = false;
          GameObject gameObject;
          switch (reward.Type)
          {
            case RewardType.Item:
              ItemParam itemParam = gm.GetItemParam(reward.IName);
              if (itemParam == null)
                return;
              gameObject = (GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) item.RewardItem);
              DataSource.Bind<ItemParam>(gameObject, itemParam);
              flag = true;
              break;
            case RewardType.Gold:
              gameObject = (GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) item.RewardGold);
              flag = true;
              break;
            case RewardType.Coin:
              gameObject = (GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) item.RewardCoin);
              flag = true;
              break;
            case RewardType.Artifact:
              ArtifactParam artifactParam = gm.MasterParam.GetArtifactParam(reward.IName);
              if (artifactParam == null)
                return;
              gameObject = (GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) item.RewardArtifact);
              DataSource.Bind<ArtifactParam>(gameObject, artifactParam);
              break;
            case RewardType.Unit:
              UnitParam unitParam = gm.GetUnitParam(reward.IName);
              if (unitParam == null)
                return;
              gameObject = (GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) item.RewardUnit);
              DataSource.Bind<UnitParam>(gameObject, unitParam);
              break;
            case RewardType.Award:
              AwardParam awardParam = gm.GetAwardParam(reward.IName);
              if (awardParam == null)
                return;
              gameObject = (GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) item.RewardAward);
              DataSource.Bind<AwardParam>(gameObject, awardParam);
              break;
            default:
              return;
          }
          if (flag)
          {
            Transform child = gameObject.get_transform().FindChild("amount/Text_amount");
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) child, (UnityEngine.Object) null))
            {
              Text component = (Text) ((Component) child).GetComponent<Text>();
              if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
                component.set_text(reward.Num.ToString());
            }
          }
          gameObject.get_transform().SetParent(item.RewardList, false);
          gameObject.SetActive(true);
        }));
      }));
    }
  }
}
