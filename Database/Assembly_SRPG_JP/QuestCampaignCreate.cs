// Decompiled with JetBrains decompiler
// Type: SRPG.QuestCampaignCreate
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  public class QuestCampaignCreate : MonoBehaviour
  {
    [SerializeField]
    private GameObject QuestCampaignItem;
    private GameObject mGoQuestCampaignItem;

    public QuestCampaignCreate()
    {
      base.\u002Ector();
    }

    public QuestCampaignList GetQuestCampaignList
    {
      get
      {
        if (Object.op_Equality((Object) this.mGoQuestCampaignItem, (Object) null))
          return (QuestCampaignList) null;
        return (QuestCampaignList) this.mGoQuestCampaignItem.GetComponent<QuestCampaignList>();
      }
    }

    private void Start()
    {
      if (Object.op_Equality((Object) this.QuestCampaignItem, (Object) null))
        return;
      this.mGoQuestCampaignItem = (GameObject) Object.Instantiate<GameObject>((M0) this.QuestCampaignItem);
      this.mGoQuestCampaignItem.SetActive(true);
      Vector2 anchoredPosition = ((RectTransform) this.mGoQuestCampaignItem.GetComponent<RectTransform>()).get_anchoredPosition();
      Vector3 localScale = this.mGoQuestCampaignItem.get_transform().get_localScale();
      this.mGoQuestCampaignItem.get_transform().SetParent(((Component) this).get_transform());
      ((RectTransform) this.mGoQuestCampaignItem.GetComponent<RectTransform>()).set_anchoredPosition(anchoredPosition);
      this.mGoQuestCampaignItem.get_transform().set_localScale(localScale);
    }
  }
}
