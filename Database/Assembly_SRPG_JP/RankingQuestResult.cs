// Decompiled with JetBrains decompiler
// Type: SRPG.RankingQuestResult
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class RankingQuestResult : MonoBehaviour
  {
    private const int HIGHER_RANK_TEXT_INDEX = 4;
    private const int MIDDLE_RANK_TEXT_INDEX = 10;
    [SerializeField]
    private Text m_HigherRankText;
    [SerializeField]
    private Text m_MiddleRankText;
    [SerializeField]
    private Text m_LowerRankText;
    [SerializeField]
    private GameObject m_RankDecoration;
    [SerializeField]
    private GameObject m_RankDecorationEffect;
    [SerializeField]
    private Text m_MainScoreText;
    [SerializeField]
    private Text m_MainScoreValue;
    [SerializeField]
    private Text m_SubScoreValue;

    public RankingQuestResult()
    {
      base.\u002Ector();
    }

    private void Start()
    {
      RankingQuestParam rankingQuestParam;
      if (Object.op_Inequality((Object) SceneBattle.Instance, (Object) null))
      {
        rankingQuestParam = SceneBattle.Instance.Battle.GetRankingQuestParam();
        DataSource.Bind<UnitData>(((Component) this).get_gameObject(), SceneBattle.Instance.Battle.Leader.UnitData);
      }
      else
        rankingQuestParam = GlobalVars.SelectedRankingQuestParam;
      if (rankingQuestParam == null)
        return;
      ((Component) this.m_HigherRankText).get_gameObject().SetActive(false);
      ((Component) this.m_MiddleRankText).get_gameObject().SetActive(false);
      ((Component) this.m_LowerRankText).get_gameObject().SetActive(false);
      this.m_RankDecoration.SetActive(false);
      this.m_RankDecorationEffect.SetActive(false);
      Text text = this.m_LowerRankText;
      if (SceneBattle.Instance.RankingQuestNewRank <= 4)
      {
        text = this.m_HigherRankText;
        this.m_RankDecoration.SetActive(true);
        this.m_RankDecorationEffect.SetActive(true);
      }
      else if (SceneBattle.Instance.RankingQuestNewRank <= 10)
      {
        text = this.m_MiddleRankText;
        this.m_RankDecoration.SetActive(true);
        this.m_RankDecorationEffect.SetActive(true);
      }
      ((Component) text).get_gameObject().SetActive(true);
      text.set_text(SceneBattle.Instance.RankingQuestNewRank.ToString());
      if (rankingQuestParam.type == RankingQuestType.ActionCount)
      {
        if (Object.op_Inequality((Object) this.m_MainScoreText, (Object) null))
          this.m_MainScoreText.set_text(LocalizedText.Get("sys.RANKING_QUEST_WND_TYPE_ACTION"));
        if (Object.op_Inequality((Object) this.m_MainScoreValue, (Object) null))
          this.m_MainScoreValue.set_text(SceneBattle.Instance.Battle.ActionCount.ToString());
      }
      if (!Object.op_Inequality((Object) this.m_SubScoreValue, (Object) null))
        return;
      this.m_SubScoreValue.set_text(SceneBattle.Instance.Battle.CalcPlayerUnitsTotalParameter().ToString());
    }
  }
}
