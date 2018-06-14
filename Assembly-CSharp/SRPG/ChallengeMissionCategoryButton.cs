// Decompiled with JetBrains decompiler
// Type: SRPG.ChallengeMissionCategoryButton
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class ChallengeMissionCategoryButton : MonoBehaviour
  {
    public Button Button;
    public Image Badge;
    public Image SelectionFrame;
    public GameObject TimerBase;
    public Text Timer;
    private float mRefreshInterval;
    private DateTime mEndTime;
    private ChallengeCategoryParam mCategoryParam;

    public ChallengeMissionCategoryButton()
    {
      base.\u002Ector();
    }

    private void Update()
    {
      if (this.mCategoryParam == null)
        return;
      this.mRefreshInterval -= Time.get_unscaledDeltaTime();
      if ((double) this.mRefreshInterval > 0.0)
        return;
      this.Refresh();
      this.mRefreshInterval = 1f;
    }

    private void Refresh()
    {
      TimeSpan timeSpan = this.mEndTime - TimeManager.ServerTime;
      string str;
      if (timeSpan.TotalDays >= 1.0)
        str = LocalizedText.Get("sys.QUEST_TIMELIMIT_D", new object[1]
        {
          (object) timeSpan.Days
        });
      else if (timeSpan.TotalHours >= 1.0)
        str = LocalizedText.Get("sys.QUEST_TIMELIMIT_H", new object[1]
        {
          (object) timeSpan.Hours
        });
      else
        str = LocalizedText.Get("sys.QUEST_TIMELIMIT_M", new object[1]
        {
          (object) Mathf.Max(timeSpan.Minutes, 0)
        });
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Timer, (UnityEngine.Object) null) || !(this.Timer.get_text() != str))
        return;
      this.Timer.set_text(str);
    }

    public void SetChallengeCategory(ChallengeCategoryParam category)
    {
      if (category.end_at != null)
      {
        this.TimerBase.SetActive(true);
        this.mCategoryParam = category;
        this.mEndTime = this.mCategoryParam.end_at.DateTimes;
        this.Refresh();
      }
      else
        this.TimerBase.SetActive(false);
    }
  }
}
