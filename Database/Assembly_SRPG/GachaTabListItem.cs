// Decompiled with JetBrains decompiler
// Type: SRPG.GachaTabListItem
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
  public class GachaTabListItem : MonoBehaviour
  {
    public Text Value;
    public Text Fotter;
    private long mEndAt;
    private bool mDisabled;
    private Coroutine mUpdateCoroutine;
    private float mNextUpdateTime;
    private string mFormatkey;
    private long mGachaStartAt;
    private long mGachaEndAt;
    private int mListIndex;

    public GachaTabListItem()
    {
      base.\u002Ector();
    }

    public long EndAt
    {
      get
      {
        return this.mEndAt;
      }
      set
      {
        this.mEndAt = value;
      }
    }

    public bool Disabled
    {
      get
      {
        return this.mDisabled;
      }
      set
      {
        this.mDisabled = value;
      }
    }

    public string FormatKey
    {
      get
      {
        return this.mFormatkey;
      }
      set
      {
        this.mFormatkey = value;
      }
    }

    public long GachaStartAt
    {
      get
      {
        return this.mGachaStartAt;
      }
      set
      {
        this.mGachaStartAt = value;
      }
    }

    public long GachaEndtAt
    {
      get
      {
        return this.mGachaEndAt;
      }
      set
      {
        this.mGachaEndAt = value;
      }
    }

    public int ListIndex
    {
      get
      {
        return this.mListIndex;
      }
      set
      {
        this.mListIndex = value;
      }
    }

    private void Start()
    {
    }

    private void OnEnable()
    {
      if (this.mUpdateCoroutine != null)
      {
        this.StopCoroutine(this.mUpdateCoroutine);
        this.mUpdateCoroutine = (Coroutine) null;
      }
      this.RefreshTimer();
    }

    [DebuggerHidden]
    private IEnumerator UpdateTimer()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new GachaTabListItem.\u003CUpdateTimer\u003Ec__IteratorF3() { \u003C\u003Ef__this = this };
    }

    private void SetUpdateTimer(float interval)
    {
      if (!((Component) this).get_gameObject().get_activeInHierarchy())
        return;
      if ((double) interval <= 0.0)
      {
        if (this.mUpdateCoroutine == null)
          return;
        this.StopCoroutine(this.mUpdateCoroutine);
      }
      else
      {
        this.mNextUpdateTime = Time.get_time() + interval;
        if (this.mUpdateCoroutine != null)
          return;
        this.mUpdateCoroutine = this.StartCoroutine(this.UpdateTimer());
      }
    }

    private void RefreshTimer()
    {
      DateTime serverTime = TimeManager.ServerTime;
      DateTime dateTime = TimeManager.FromUnixTime(this.mEndAt);
      TimeSpan timeSpan = dateTime - serverTime;
      if (this.Disabled && timeSpan.TotalSeconds < 0.0 && this.mGachaEndAt >= Network.GetServerTime())
      {
        this.mEndAt = TimeManager.FromDateTime(dateTime.AddDays(1.0));
        dateTime = TimeManager.FromUnixTime(this.mEndAt);
        timeSpan = dateTime - serverTime;
        SRPG_Button component = (SRPG_Button) ((Component) this).GetComponent<SRPG_Button>();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
        {
          ((Selectable) component).set_interactable(true);
          this.Disabled = false;
        }
      }
      string empty = string.Empty;
      string str;
      if (timeSpan.TotalDays >= 1.0)
        str = LocalizedText.Get(this.FormatKey + "D", new object[1]
        {
          (object) timeSpan.Days
        });
      else if (timeSpan.TotalHours >= 1.0)
        str = LocalizedText.Get(this.FormatKey + "H", new object[1]
        {
          (object) timeSpan.Hours
        });
      else
        str = LocalizedText.Get(this.FormatKey + "M", new object[1]
        {
          (object) Mathf.Max(timeSpan.Minutes, 0)
        });
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Value, (UnityEngine.Object) null) && this.Value.get_text() != str)
        this.Value.set_text(str);
      this.SetUpdateTimer(1f);
    }
  }
}
