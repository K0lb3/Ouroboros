// Decompiled with JetBrains decompiler
// Type: SRPG.RankingQuestButton
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(0, "Set ranking quest", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(100, "Success", FlowNode.PinTypes.Output, 100)]
  public class RankingQuestButton : MonoBehaviour, IGameParameter, IFlowInterface
  {
    private const int PERIOD_STATE_ICON_INDEX_OPEN = 0;
    private const int PERIOD_STATE_ICON_INDEX_WAIT = 1;
    private const int PERIOD_STATE_ICON_INDEX_VISIBLE = 2;
    [SerializeField]
    private ImageArray m_PeriodStateIcon;
    [SerializeField]
    private Text m_Time;

    public RankingQuestButton()
    {
      base.\u002Ector();
    }

    private void Start()
    {
      this.UpdateValue();
    }

    public void Activated(int pinID)
    {
      if (pinID != 0)
        return;
      RankingQuestParam dataOfClass = DataSource.FindDataOfClass<RankingQuestParam>(((Component) this).get_gameObject(), (RankingQuestParam) null);
      if (dataOfClass == null)
        return;
      GlobalVars.SelectedQuestID = dataOfClass.iname;
      GlobalVars.SelectedRankingQuestParam = dataOfClass;
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
    }

    public void UpdateValue()
    {
      RankingQuestParam dataOfClass = DataSource.FindDataOfClass<RankingQuestParam>(((Component) this).get_gameObject(), (RankingQuestParam) null);
      if (dataOfClass == null)
      {
        ((Component) this).get_gameObject().SetActive(false);
      }
      else
      {
        DateTime serverTime = TimeManager.ServerTime;
        ((Component) this).get_gameObject().SetActive(true);
        if (dataOfClass.scheduleParam.IsAvailablePeriod(serverTime))
        {
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_PeriodStateIcon, (UnityEngine.Object) null))
            this.m_PeriodStateIcon.ImageIndex = 0;
          TimeSpan timeSpan = dataOfClass.scheduleParam.endAt - serverTime;
          if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_Time, (UnityEngine.Object) null))
            return;
          if (timeSpan.TotalDays >= 1.0)
            this.m_Time.set_text(LocalizedText.Get("sys.RANKING_QUEST_QUEST_BANNER_DAY", new object[1]
            {
              (object) timeSpan.Days
            }));
          else if (timeSpan.TotalHours >= 1.0)
            this.m_Time.set_text(LocalizedText.Get("sys.RANKING_QUEST_QUEST_BANNER_HOUR", new object[1]
            {
              (object) timeSpan.Hours
            }));
          else
            this.m_Time.set_text(LocalizedText.Get("sys.RANKING_QUEST_QUEST_BANNER_MINUTE", new object[1]
            {
              (object) Mathf.Max(timeSpan.Minutes, 0)
            }));
          ((Component) this.m_Time).get_gameObject().SetActive(true);
        }
        else if (dataOfClass.scheduleParam.IsAvailableVisiblePeriod(serverTime))
        {
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_PeriodStateIcon, (UnityEngine.Object) null))
            this.m_PeriodStateIcon.ImageIndex = 2;
          if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_Time, (UnityEngine.Object) null))
            return;
          ((Component) this.m_Time).get_gameObject().SetActive(false);
        }
        else
          ((Component) this).get_gameObject().SetActive(false);
      }
    }
  }
}
