// Decompiled with JetBrains decompiler
// Type: SRPG.GuerrillaShopTimeLimit
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class GuerrillaShopTimeLimit : MonoBehaviour, IGameParameter
  {
    public Text Hour;
    public Text Minute;
    public Text Second;
    private long mEndTime;
    private float mRefreshInterval;

    public GuerrillaShopTimeLimit()
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
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Hour, (UnityEngine.Object) null))
          this.Hour.set_text("00");
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Minute, (UnityEngine.Object) null))
          this.Minute.set_text("00");
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Second, (UnityEngine.Object) null))
          return;
        this.Second.set_text("00");
      }
      else
      {
        TimeSpan timeSpan = TimeManager.FromUnixTime(this.mEndTime) - TimeManager.ServerTime;
        int totalHours = (int) timeSpan.TotalHours;
        int totalMinutes = (int) timeSpan.TotalMinutes;
        int totalSeconds = (int) timeSpan.TotalSeconds;
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Hour, (UnityEngine.Object) null))
          this.Hour.set_text(string.Format("{0:D2}", (object) totalHours));
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Minute, (UnityEngine.Object) null))
        {
          if (totalHours > 0)
            this.Minute.set_text(string.Format("{0:D2}", (object) (totalMinutes % (totalHours * 60))));
          else
            this.Minute.set_text(string.Format("{0:D2}", (object) totalMinutes));
        }
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Second, (UnityEngine.Object) null))
        {
          if (totalMinutes > 0)
            this.Second.set_text(string.Format("{0:D2}", (object) (totalSeconds % (totalMinutes * 60))));
          else
            this.Second.set_text(string.Format("{0:D2}", (object) totalSeconds));
        }
        if (!(timeSpan <= TimeSpan.Zero))
          return;
        GlobalEvent.Invoke("FINISH_GUERRILLA_SHOP_SHOW", (object) null);
      }
    }

    public void UpdateValue()
    {
      this.mEndTime = 0L;
      long guerrillaShopEnd = MonoSingleton<GameManager>.Instance.Player.GuerrillaShopEnd;
      if (guerrillaShopEnd <= 0L)
        return;
      this.mEndTime = guerrillaShopEnd;
      this.Refresh();
    }
  }
}
