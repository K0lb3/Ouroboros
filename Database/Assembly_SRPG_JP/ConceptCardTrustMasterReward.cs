// Decompiled with JetBrains decompiler
// Type: SRPG.ConceptCardTrustMasterReward
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class ConceptCardTrustMasterReward : MonoBehaviour
  {
    [SerializeField]
    private Text mItemName;
    [SerializeField]
    private Text mItemAmount;
    [SerializeField]
    private ItemIcon mItemIcon;
    [SerializeField]
    private ArtifactIcon mArtifactIcon;
    [SerializeField]
    private ConceptCardIcon mCardIcon;
    [SerializeField]
    private Sprite CoinFrame;
    [SerializeField]
    private Sprite GoldFrame;

    public ConceptCardTrustMasterReward()
    {
      base.\u002Ector();
    }

    public void SetData(ConceptCardData data)
    {
      ConceptCardTrustRewardItemParam reward = data.GetReward();
      if (reward == null)
        return;
      switch (reward.reward_type)
      {
        case eRewardType.Item:
          this.SetItem(reward);
          break;
        case eRewardType.Artifact:
          this.SetArtifact(reward);
          break;
        case eRewardType.ConceptCard:
          this.SetConceptCard(reward);
          break;
      }
      this.mItemAmount.set_text(reward.reward_num.ToString());
    }

    public void SetItem(ConceptCardTrustRewardItemParam reward_item)
    {
      if (string.IsNullOrEmpty(reward_item.iname))
        return;
      ItemParam itemParam = MonoSingleton<GameManager>.Instance.GetItemParam(reward_item.iname);
      this.mItemName.set_text(itemParam.name);
      DataSource.Bind<ItemParam>(((Component) this).get_gameObject(), itemParam);
      if (!Object.op_Inequality((Object) this.mItemIcon, (Object) null))
        return;
      ((Component) this.mItemIcon).get_gameObject().SetActive(true);
      this.mItemIcon.UpdateValue();
    }

    public void SetArtifact(ConceptCardTrustRewardItemParam reward_item)
    {
      if (string.IsNullOrEmpty(reward_item.iname))
        return;
      ArtifactParam artifactParam = MonoSingleton<GameManager>.Instance.MasterParam.GetArtifactParam(reward_item.iname);
      this.mItemName.set_text(artifactParam.name);
      DataSource.Bind<ArtifactParam>(((Component) this).get_gameObject(), artifactParam);
      if (!Object.op_Inequality((Object) this.mArtifactIcon, (Object) null))
        return;
      ((Component) this.mArtifactIcon).get_gameObject().SetActive(true);
      this.mArtifactIcon.UpdateValue();
    }

    public void SetConceptCard(ConceptCardTrustRewardItemParam reward_item)
    {
      if (string.IsNullOrEmpty(reward_item.iname))
        return;
      ConceptCardParam conceptCardParam = MonoSingleton<GameManager>.Instance.MasterParam.GetConceptCardParam(reward_item.iname);
      this.mItemName.set_text(conceptCardParam.name);
      DataSource.Bind<ConceptCardParam>(((Component) this).get_gameObject(), conceptCardParam);
      if (!Object.op_Inequality((Object) this.mCardIcon, (Object) null))
        return;
      ((Component) this.mCardIcon).get_gameObject().SetActive(true);
      this.mCardIcon.Setup(ConceptCardData.CreateConceptCardDataForDisplay(conceptCardParam.iname));
    }
  }
}
