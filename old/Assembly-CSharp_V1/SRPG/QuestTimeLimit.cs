// Decompiled with JetBrains decompiler
// Type: SRPG.QuestTimeLimit
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class QuestTimeLimit : MonoBehaviour, IGameParameter
  {
    public GameObject Body;
    public Text Timer;
    public bool IsTTMMSS;
    private long mEndTime;
    private float mRefreshInterval;

    public QuestTimeLimit()
    {
      base.\u002Ector();
    }

    private void Start()
    {
      this.UpdateValue();
      this.Refresh();
    }

    private void Update()
    {
      this.mRefreshInterval -= Time.get_unscaledDeltaTime();
      if ((double) this.mRefreshInterval > 0.0)
        return;
      this.Refresh();
      this.mRefreshInterval = 1f;
    }

    private void Refresh()
    {
      if (this.mEndTime <= 0L)
      {
        if (!Object.op_Inequality((Object) this.Body, (Object) null))
          return;
        this.Body.SetActive(false);
      }
      else
      {
        if (Object.op_Inequality((Object) this.Body, (Object) null))
          this.Body.SetActive(true);
        TimeSpan timeSpan = TimeManager.FromUnixTime(this.mEndTime) - TimeManager.ServerTime;
        string str1 = (string) null;
        string str2;
        if (this.IsTTMMSS)
        {
          int num1 = Math.Max(Math.Min(timeSpan.Days * 24 + timeSpan.Hours, 99), 0);
          int num2 = Math.Max(Math.Min(timeSpan.Minutes, 59), 0);
          int num3 = Math.Max(Math.Min(timeSpan.Seconds, 59), 0);
          str2 = str1 + string.Format("{0:D2}", (object) num1).ToString() + ":" + string.Format("{0:D2}", (object) num2).ToString() + ":" + string.Format("{0:D2}", (object) num3).ToString();
        }
        else if (timeSpan.TotalDays >= 1.0)
          str2 = LocalizedText.Get("sys.QUEST_TIMELIMIT_D", new object[1]
          {
            (object) timeSpan.Days
          });
        else if (timeSpan.TotalHours >= 1.0)
          str2 = LocalizedText.Get("sys.QUEST_TIMELIMIT_H", new object[1]
          {
            (object) timeSpan.Hours
          });
        else
          str2 = LocalizedText.Get("sys.QUEST_TIMELIMIT_M", new object[1]
          {
            (object) Mathf.Max(timeSpan.Minutes, 0)
          });
        if (!Object.op_Inequality((Object) this.Timer, (Object) null) || !(this.Timer.get_text() != str2))
          return;
        this.Timer.set_text(str2);
      }
    }

    public void UpdateValue()
    {
      this.mEndTime = 0L;
      QuestParam dataOfClass1 = DataSource.FindDataOfClass<QuestParam>(((Component) this).get_gameObject(), (QuestParam) null);
      if (dataOfClass1 != null && dataOfClass1.Chapter != null)
      {
        switch (dataOfClass1.Chapter.GetKeyQuestType())
        {
          case KeyQuestTypes.Timer:
            this.mEndTime = dataOfClass1.Chapter.key_end;
            break;
          case KeyQuestTypes.Count:
            this.mEndTime = 0L;
            break;
          default:
            this.mEndTime = dataOfClass1.Chapter.end;
            break;
        }
        this.Refresh();
      }
      else
      {
        ChapterParam dataOfClass2 = DataSource.FindDataOfClass<ChapterParam>(((Component) this).get_gameObject(), (ChapterParam) null);
        if (dataOfClass2 != null)
        {
          switch (dataOfClass2.GetKeyQuestType())
          {
            case KeyQuestTypes.Timer:
              this.mEndTime = dataOfClass2.key_end;
              break;
            case KeyQuestTypes.Count:
              this.mEndTime = 0L;
              break;
            default:
              this.mEndTime = dataOfClass2.end;
              break;
          }
          this.Refresh();
        }
        else
        {
          if (dataOfClass1 == null || dataOfClass1.type != QuestTypes.Tower)
            return;
          this.mEndTime = dataOfClass1.end;
          this.Refresh();
        }
      }
    }
  }
}
