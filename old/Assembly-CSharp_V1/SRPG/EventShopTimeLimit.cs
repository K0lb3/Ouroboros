// Decompiled with JetBrains decompiler
// Type: SRPG.EventShopTimeLimit
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class EventShopTimeLimit : MonoBehaviour, IGameParameter
  {
    public GameObject Body;
    public Text Timer;
    private long mEndTime;
    private float mRefreshInterval;

    public EventShopTimeLimit()
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
        if (!Object.op_Inequality((Object) this.Timer, (Object) null) || !(this.Timer.get_text() != str))
          return;
        this.Timer.set_text(str);
      }
    }

    public void UpdateValue()
    {
      this.mEndTime = 0L;
      if (MonoSingleton<GameManager>.Instance.Player.GetEventShopData() == null)
        return;
      this.mEndTime = GlobalVars.EventShopItem.shops.end;
      this.Refresh();
    }
  }
}
