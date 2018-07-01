// Decompiled with JetBrains decompiler
// Type: SRPG.SGShowServerTime
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class SGShowServerTime : MonoBehaviour
  {
    [SerializeField]
    private Text dateText;
    [SerializeField]
    private Text timeText;
    private DateTime currentTime;

    public SGShowServerTime()
    {
      base.\u002Ector();
    }

    private void Start()
    {
      this.UpdateDateTimeText();
    }

    private void UpdateDateTimeText()
    {
      this.currentTime = TimeManager.ServerTime;
      this.dateText.set_text(this.currentTime.ToString(GameUtility.CultureSetting.DateTimeFormat.MonthDayPattern.Replace("MMMM", "MMM"), (IFormatProvider) GameUtility.CultureSetting));
      this.timeText.set_text(this.currentTime.ToString("HH:mm"));
      this.StartCoroutine(this.Tick());
    }

    [DebuggerHidden]
    private IEnumerator Tick()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new SGShowServerTime.\u003CTick\u003Ec__Iterator4F() { \u003C\u003Ef__this = this };
    }
  }
}
