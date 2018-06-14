// Decompiled with JetBrains decompiler
// Type: SRPG.RankingQuestActionCount
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System.Collections;
using System.Diagnostics;
using UnityEngine;

namespace SRPG
{
  public class RankingQuestActionCount : MonoBehaviour
  {
    public GameObject GoWhiteFont;
    public GameObject GoYellowFont;
    public GameObject GoRedFont;
    private RankingQuestActionCount.AnmCtrl mAnmCtrl;
    private uint mActionCount;
    private uint mOldActionCount;
    private bool mIsInitialized;

    public RankingQuestActionCount()
    {
      base.\u002Ector();
    }

    public uint ActionCount
    {
      get
      {
        return this.mActionCount;
      }
      set
      {
        this.mActionCount = value;
        if ((int) this.mOldActionCount == (int) this.mActionCount)
          return;
        this.dispActionCount((int) this.mActionCount);
        this.mOldActionCount = this.mActionCount;
      }
    }

    private void dispActionCount(int count)
    {
      if (!this.mIsInitialized)
        return;
      if (count < 0)
        count = 0;
      this.GoWhiteFont.SetActive(false);
      this.GoYellowFont.SetActive(false);
      this.GoRedFont.SetActive(false);
      GameObject goYellowFont = this.GoYellowFont;
      goYellowFont.SetActive(true);
      BitmapText componentInChildren = (BitmapText) goYellowFont.GetComponentInChildren<BitmapText>(true);
      if (!Object.op_Implicit((Object) componentInChildren))
        return;
      componentInChildren.text = count.ToString();
    }

    public void PlayEffect()
    {
      this.StartCoroutine(this.playEffect());
    }

    [DebuggerHidden]
    private IEnumerator playEffect()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new RankingQuestActionCount.\u003CplayEffect\u003Ec__Iterator72() { \u003C\u003Ef__this = this };
    }

    private void Start()
    {
      this.mIsInitialized = false;
      if (Object.op_Implicit((Object) this.GoWhiteFont) && Object.op_Implicit((Object) this.GoYellowFont) && Object.op_Implicit((Object) this.GoRedFont))
        this.mIsInitialized = true;
      this.ActionCount = 0U;
    }

    private enum eAnmState
    {
      IDLE,
      INIT,
      WAIT_FRAME,
      PLAY_DROP,
      WAIT_DROP,
      PLAY_BEAT,
      WAIT_BEAT,
    }

    private class AnmCtrl
    {
      public uint mPlayBeatCtr = 1;
      public RankingQuestActionCount.eAnmState mAnmState;
      public uint mWaitFrameCtr;
      public GameObject mGoSelf;
      public Animator mAnmSelf;
      public GameObject mGoEffect;
      public Animator mAnmEffect;
    }
  }
}
