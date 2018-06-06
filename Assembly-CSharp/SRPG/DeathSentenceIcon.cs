// Decompiled with JetBrains decompiler
// Type: SRPG.DeathSentenceIcon
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class DeathSentenceIcon : MonoBehaviour
  {
    public GameObject DeathIconPrefab;
    private Text mCountDownText;
    private GameObject mDeathIcon;
    private bool mIsDeathSentenceCountDownPlaying;

    public DeathSentenceIcon()
    {
      base.\u002Ector();
    }

    public void Init(Unit parent)
    {
      if (!Object.op_Implicit((Object) this.DeathIconPrefab))
        return;
      this.mDeathIcon = Object.Instantiate((Object) this.DeathIconPrefab, ((Component) this).get_transform().get_position(), ((Component) this).get_transform().get_rotation()) as GameObject;
      if (!Object.op_Inequality((Object) this.mDeathIcon, (Object) null))
        return;
      this.mCountDownText = (Text) this.mDeathIcon.GetComponentInChildren<Text>();
      this.mDeathIcon.get_transform().SetParent(((Component) this).get_transform());
      this.mDeathIcon.SetActive(false);
      DataSource.Bind<Unit>(((Component) this).get_gameObject(), parent);
    }

    public void Open()
    {
      if (!Object.op_Inequality((Object) this.mDeathIcon, (Object) null) || this.mDeathIcon.get_activeSelf())
        return;
      this.mDeathIcon.SetActive(true);
    }

    public void Close()
    {
      if (!Object.op_Inequality((Object) this.mDeathIcon, (Object) null) || !this.mDeathIcon.get_activeSelf())
        return;
      this.mDeathIcon.SetActive(false);
    }

    public void UpdateCountDown(int currentCount)
    {
      if (Object.op_Equality((Object) this.mDeathIcon, (Object) null) || !Object.op_Inequality((Object) this.mCountDownText, (Object) null))
        return;
      this.mCountDownText.set_text(currentCount.ToString());
    }

    public bool IsDeathSentenceCountDownPlaying
    {
      get
      {
        return this.mIsDeathSentenceCountDownPlaying;
      }
      set
      {
      }
    }

    [DebuggerHidden]
    private IEnumerator CountdownInternal(int currentCount, float LifeSeconds)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new DeathSentenceIcon.\u003CCountdownInternal\u003Ec__IteratorAA() { currentCount = currentCount, LifeSeconds = LifeSeconds, \u003C\u0024\u003EcurrentCount = currentCount, \u003C\u0024\u003ELifeSeconds = LifeSeconds, \u003C\u003Ef__this = this };
    }

    public void Countdown(int currentCount, float LifeSeconds = 0.0f)
    {
      if (((Component) this).get_gameObject().get_activeInHierarchy() && (double) LifeSeconds > 0.0)
      {
        this.StartCoroutine(this.CountdownInternal(currentCount, LifeSeconds));
      }
      else
      {
        this.mIsDeathSentenceCountDownPlaying = false;
        this.UpdateCountDown(currentCount);
      }
    }
  }
}
