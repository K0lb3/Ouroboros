// Decompiled with JetBrains decompiler
// Type: SRPG.BattleResultMissionDetail
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class BattleResultMissionDetail : MonoBehaviour
  {
    private static readonly float WaitTime = 0.2f;
    [SerializeField]
    private QuestMissionItem ItemTemplate;
    [SerializeField]
    private QuestMissionItem UnitTemplate;
    [SerializeField]
    private QuestMissionItem ArtifactTemplate;
    [SerializeField]
    private GameObject ContentsParent;
    [SerializeField]
    private ScrollRect ScrollRect;
    [SerializeField]
    private VerticalLayoutGroup VerticalLayout;
    private List<GameObject> allStarObjects;
    private Coroutine lastCoroutine;

    public BattleResultMissionDetail()
    {
      base.\u002Ector();
    }

    private void Awake()
    {
      QuestParam questParam = DataSource.FindDataOfClass<QuestParam>(((Component) this).get_gameObject(), (QuestParam) null);
      if (questParam == null && Object.op_Inequality((Object) SceneBattle.Instance, (Object) null))
        questParam = MonoSingleton<GameManager>.Instance.FindQuest(SceneBattle.Instance.Battle.QuestID);
      if (questParam == null)
        return;
      this.RefreshQuestMissionReward(questParam);
    }

    private void RefreshQuestMissionReward(QuestParam questParam)
    {
      if (questParam.bonusObjective == null)
        return;
      for (int index = 0; index < questParam.bonusObjective.Length; ++index)
      {
        QuestMissionItem questMissionItem;
        if (questParam.bonusObjective[index].itemType == RewardType.Artifact)
        {
          questMissionItem = (QuestMissionItem) ((GameObject) Object.Instantiate<GameObject>((M0) ((Component) this.ArtifactTemplate).get_gameObject())).GetComponent<QuestMissionItem>();
        }
        else
        {
          ItemParam itemParam = MonoSingleton<GameManager>.Instance.GetItemParam(questParam.bonusObjective[index].item);
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
          this.allStarObjects.Add(((Component) questMissionItem.Star).get_gameObject());
          GameParameter.UpdateAll(((Component) questMissionItem).get_gameObject());
        }
      }
      if (!Object.op_Inequality((Object) this.ScrollRect, (Object) null))
        return;
      this.ScrollRect.set_verticalNormalizedPosition(1f);
      this.ScrollRect.set_horizontalNormalizedPosition(1f);
    }

    public List<GameObject> GetObjectiveStars()
    {
      return this.allStarObjects;
    }

    public float MoveTo(int index)
    {
      if (Object.op_Equality((Object) this.ScrollRect, (Object) null))
        return 0.0f;
      if (this.lastCoroutine != null)
        this.StopCoroutine(this.lastCoroutine);
      this.lastCoroutine = this.StartCoroutine(this.MoveScrollCoroutine(index));
      return BattleResultMissionDetail.WaitTime;
    }

    [DebuggerHidden]
    private IEnumerator MoveScrollCoroutine(int index)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new BattleResultMissionDetail.\u003CMoveScrollCoroutine\u003Ec__IteratorE3() { index = index, \u003C\u0024\u003Eindex = index, \u003C\u003Ef__this = this };
    }
  }
}
