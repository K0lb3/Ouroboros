// Decompiled with JetBrains decompiler
// Type: SRPG.MissionDetail
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class MissionDetail : MonoBehaviour
  {
    [SerializeField]
    private QuestMissionItem ItemTemplate;
    [SerializeField]
    private QuestMissionItem UnitTemplate;
    [SerializeField]
    private QuestMissionItem ArtifactTemplate;
    [SerializeField]
    private QuestMissionItem ConceptCardTemplate;
    [SerializeField]
    private QuestMissionItem NothingRewardTemplate;
    [SerializeField]
    private GameObject ContentsParent;
    [SerializeField]
    private GameObject Window;
    [SerializeField]
    private ScrollRect ScrollRect;
    [SerializeField]
    private GameObject Scrollbar;

    public MissionDetail()
    {
      base.\u002Ector();
    }

    private void Awake()
    {
      QuestParam questParam = DataSource.FindDataOfClass<QuestParam>(((Component) this).get_gameObject(), (QuestParam) null);
      if (questParam == null && Object.op_Inequality((Object) SceneBattle.Instance, (Object) null))
        questParam = MonoSingleton<GameManager>.Instance.FindQuest(SceneBattle.Instance.Battle.QuestID);
      if (questParam == null || questParam.bonusObjective == null)
        return;
      if (questParam.bonusObjective.Length > 3)
      {
        if (Object.op_Inequality((Object) this.Scrollbar, (Object) null))
          this.Scrollbar.SetActive(true);
        if (Object.op_Inequality((Object) this.ScrollRect, (Object) null))
        {
          this.ScrollRect.set_horizontal(false);
          this.ScrollRect.set_vertical(true);
        }
        if (Object.op_Equality((Object) this.Window, (Object) null))
          return;
        RectTransform transform = this.Window.get_transform() as RectTransform;
        if (Object.op_Inequality((Object) transform, (Object) null))
          transform.set_sizeDelta(new Vector2((float) transform.get_sizeDelta().x, (float) (transform.get_sizeDelta().y + 120.0)));
      }
      else
      {
        if (Object.op_Inequality((Object) this.Scrollbar, (Object) null))
          this.Scrollbar.SetActive(false);
        if (Object.op_Inequality((Object) this.ScrollRect, (Object) null))
        {
          this.ScrollRect.set_horizontal(false);
          this.ScrollRect.set_vertical(false);
        }
      }
      this.RefreshQuestMissionReward(questParam);
    }

    private void RefreshQuestMissionReward(QuestParam questParam)
    {
      if (questParam.bonusObjective == null)
        return;
      for (int index = 0; index < questParam.bonusObjective.Length; ++index)
      {
        QuestBonusObjective questBonusObjective = questParam.bonusObjective[index];
        QuestMissionItem questMissionItem;
        if (questBonusObjective.itemType == RewardType.Artifact)
          questMissionItem = (QuestMissionItem) ((GameObject) Object.Instantiate<GameObject>((M0) ((Component) this.ArtifactTemplate).get_gameObject())).GetComponent<QuestMissionItem>();
        else if (questBonusObjective.itemType == RewardType.ConceptCard)
        {
          questMissionItem = (QuestMissionItem) ((GameObject) Object.Instantiate<GameObject>((M0) ((Component) this.ConceptCardTemplate).get_gameObject())).GetComponent<QuestMissionItem>();
          ConceptCardIcon component = (ConceptCardIcon) ((Component) questMissionItem).get_gameObject().GetComponent<ConceptCardIcon>();
          if (Object.op_Inequality((Object) component, (Object) null))
          {
            ConceptCardData cardDataForDisplay = ConceptCardData.CreateConceptCardDataForDisplay(questBonusObjective.item);
            component.Setup(cardDataForDisplay);
          }
        }
        else if (questBonusObjective.itemType == RewardType.Nothing)
        {
          questMissionItem = (QuestMissionItem) ((GameObject) Object.Instantiate<GameObject>((M0) ((Component) this.NothingRewardTemplate).get_gameObject())).GetComponent<QuestMissionItem>();
        }
        else
        {
          ItemParam itemParam = MonoSingleton<GameManager>.Instance.GetItemParam(questBonusObjective.item);
          if (itemParam != null)
            questMissionItem = itemParam.type != EItemType.Unit ? (QuestMissionItem) ((GameObject) Object.Instantiate<GameObject>((M0) ((Component) this.ItemTemplate).get_gameObject())).GetComponent<QuestMissionItem>() : (QuestMissionItem) ((GameObject) Object.Instantiate<GameObject>((M0) ((Component) this.UnitTemplate).get_gameObject())).GetComponent<QuestMissionItem>();
          else
            continue;
        }
        if (!Object.op_Equality((Object) questMissionItem, (Object) null))
        {
          if (Object.op_Inequality((Object) questMissionItem.Star, (Object) null))
            questMissionItem.Star.Index = index;
          if (Object.op_Inequality((Object) questMissionItem.FrameParam, (Object) null))
            questMissionItem.FrameParam.Index = index;
          if (Object.op_Inequality((Object) questMissionItem.IconParam, (Object) null))
            questMissionItem.IconParam.Index = index;
          if (Object.op_Inequality((Object) questMissionItem.NameParam, (Object) null))
            questMissionItem.NameParam.Index = index;
          if (Object.op_Inequality((Object) questMissionItem.AmountParam, (Object) null))
            questMissionItem.AmountParam.Index = index;
          if (Object.op_Inequality((Object) questMissionItem.ObjectigveParam, (Object) null))
            questMissionItem.ObjectigveParam.Index = index;
          ((Component) questMissionItem).get_gameObject().SetActive(true);
          ((Component) questMissionItem).get_transform().SetParent(this.ContentsParent.get_transform(), false);
          GameParameter.UpdateAll(((Component) questMissionItem).get_gameObject());
        }
      }
    }
  }
}
