// Decompiled with JetBrains decompiler
// Type: SRPG.RankMatchMissionItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class RankMatchMissionItem : ListItemEvents
  {
    [SerializeField]
    private GameObject Completed;
    [SerializeField]
    private Button GetRewardButton;
    [SerializeField]
    private GameObject Count;
    [SerializeField]
    private GameObject RewardUnit;
    [SerializeField]
    private GameObject RewardItem;
    [SerializeField]
    private GameObject RewardCard;
    [SerializeField]
    private GameObject RewardArtifact;
    [SerializeField]
    private GameObject RewardAward;
    [SerializeField]
    private GameObject RewardGold;
    [SerializeField]
    private GameObject RewardCoin;
    [SerializeField]
    private Transform RewardList;
    private RankMatchMissionWindow mWindow;

    public void Initialize(RankMatchMissionWindow window)
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.RewardUnit, (UnityEngine.Object) null))
        this.RewardUnit.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.RewardItem, (UnityEngine.Object) null))
        this.RewardItem.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.RewardCard, (UnityEngine.Object) null))
        this.RewardCard.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.RewardArtifact, (UnityEngine.Object) null))
        this.RewardArtifact.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.RewardAward, (UnityEngine.Object) null))
        this.RewardAward.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.RewardGold, (UnityEngine.Object) null))
        this.RewardGold.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.RewardCoin, (UnityEngine.Object) null))
        this.RewardCoin.SetActive(false);
      this.mWindow = window;
      VersusRankMissionParam dataOfClass1 = DataSource.FindDataOfClass<VersusRankMissionParam>(((Component) this).get_gameObject(), (VersusRankMissionParam) null);
      int num = 0;
      ReqRankMatchMission.MissionProgress dataOfClass2 = DataSource.FindDataOfClass<ReqRankMatchMission.MissionProgress>(((Component) this).get_gameObject(), (ReqRankMatchMission.MissionProgress) null);
      if (dataOfClass2 != null)
        num = dataOfClass2.prog;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Completed, (UnityEngine.Object) null))
        this.Completed.SetActive(dataOfClass1.IVal <= num);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.GetRewardButton, (UnityEngine.Object) null))
        ((Selectable) this.GetRewardButton).set_interactable(dataOfClass1.IVal <= num && string.IsNullOrEmpty(dataOfClass2.rewarded_at));
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Count, (UnityEngine.Object) null))
        this.Count.SetActive(dataOfClass1.IVal > num);
      GameManager gm = MonoSingleton<GameManager>.Instance;
      gm.GetVersusRankClassRewardList(dataOfClass1.RewardId).ForEach((Action<VersusRankReward>) (reward =>
      {
        bool flag = false;
        GameObject gameObject;
        switch (reward.Type)
        {
          case RewardType.Item:
            ItemParam itemParam = gm.GetItemParam(reward.IName);
            if (itemParam == null)
              return;
            gameObject = (GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) this.RewardItem);
            DataSource.Bind<ItemParam>(gameObject, itemParam);
            flag = true;
            break;
          case RewardType.Gold:
            gameObject = (GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) this.RewardGold);
            flag = true;
            break;
          case RewardType.Coin:
            gameObject = (GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) this.RewardCoin);
            flag = true;
            break;
          case RewardType.Artifact:
            ArtifactParam artifactParam = gm.MasterParam.GetArtifactParam(reward.IName);
            if (artifactParam == null)
              return;
            gameObject = (GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) this.RewardArtifact);
            DataSource.Bind<ArtifactParam>(gameObject, artifactParam);
            break;
          case RewardType.Unit:
            UnitParam unitParam = gm.GetUnitParam(reward.IName);
            if (unitParam == null)
              return;
            gameObject = (GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) this.RewardUnit);
            DataSource.Bind<UnitParam>(gameObject, unitParam);
            break;
          case RewardType.Award:
            AwardParam awardParam = gm.GetAwardParam(reward.IName);
            if (awardParam == null)
              return;
            gameObject = (GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) this.RewardAward);
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
        gameObject.get_transform().SetParent(this.RewardList, false);
        gameObject.SetActive(true);
      }));
    }

    public void MissionComplate()
    {
      VersusRankMissionParam dataOfClass = DataSource.FindDataOfClass<VersusRankMissionParam>(((Component) this).get_gameObject(), (VersusRankMissionParam) null);
      if (dataOfClass == null)
        return;
      this.mWindow.ReceiveReward(dataOfClass);
    }
  }
}
