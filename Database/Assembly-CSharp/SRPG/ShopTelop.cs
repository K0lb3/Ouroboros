// Decompiled with JetBrains decompiler
// Type: SRPG.ShopTelop
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class ShopTelop : MonoBehaviour
  {
    public Text BodyText;
    public float FadeInSec;
    public float FadeInInterval;
    public float FadeOutSec;
    public float FadeOutInterval;
    public GameObject WindowAnimator;
    public string WindowOpenProperty;
    public string WindowOpeningState;
    public string WindowCloseProperty;
    public string WindowClosingState;
    public string WindowClosedState;
    public string WindowLoopState;
    public CanvasGroup WindowAlphaCanvasGroup;
    private List<ShopTelop.TextChar> chList;
    private float mPastSec;
    private ShopTelop.EState mState;
    private string mNextText;
    private bool mNextTextUpdated;
    private bool mFadeOut;

    public ShopTelop()
    {
      base.\u002Ector();
    }

    public void SetText(string text)
    {
      this.mNextText = text;
      this.mNextTextUpdated = true;
      if (string.IsNullOrEmpty(this.mNextText))
        return;
      ((Component) this).get_gameObject().SetActive(true);
    }

    private void Awake()
    {
      ((Component) this).get_gameObject().SetActive(false);
    }

    private void StartTextAnim()
    {
      this.chList.Clear();
      if (this.mNextText != null)
      {
        int num = 0;
        foreach (char ch in this.mNextText)
          this.chList.Add(new ShopTelop.TextChar()
          {
            index = num++,
            ch = ch,
            alpha = 0.0f
          });
      }
      this.mPastSec = 0.0f;
      this.mFadeOut = false;
      this.mNextText = (string) null;
      this.mNextTextUpdated = false;
    }

    private void StartWindowAnim(string property)
    {
      Animator animator = !Object.op_Equality((Object) this.WindowAnimator, (Object) null) ? (Animator) this.WindowAnimator.GetComponent<Animator>() : (Animator) null;
      if (Object.op_Equality((Object) animator, (Object) null))
        return;
      string[] strArray = property.Split(':');
      bool flag = true;
      if (strArray.Length > 1)
        flag = strArray[1].Equals("true");
      animator.SetBool(strArray[0], flag);
    }

    private bool IsWindowState(string state)
    {
      if (string.IsNullOrEmpty(state))
        return false;
      Animator animator = !Object.op_Equality((Object) this.WindowAnimator, (Object) null) ? (Animator) this.WindowAnimator.GetComponent<Animator>() : (Animator) null;
      if (Object.op_Equality((Object) animator, (Object) null))
        return true;
      AnimatorStateInfo animatorStateInfo = animator.GetCurrentAnimatorStateInfo(0);
      // ISSUE: explicit reference operation
      return ((AnimatorStateInfo) @animatorStateInfo).IsName(state);
    }

    private bool IsCanvasGroupAlphaZero()
    {
      if (Object.op_Equality((Object) this.WindowAlphaCanvasGroup, (Object) null))
        return false;
      return (double) this.WindowAlphaCanvasGroup.get_alpha() <= 0.0;
    }

    private bool UpdateWindowState()
    {
      if (string.IsNullOrEmpty(this.mNextText))
      {
        if (this.IsWindowState(this.WindowClosedState) || this.IsCanvasGroupAlphaZero())
          ((Component) this).get_gameObject().SetActive(false);
        else if (this.IsWindowState(this.WindowOpeningState) || this.IsWindowState(this.WindowLoopState))
          this.StartWindowAnim(this.WindowCloseProperty);
        return false;
      }
      if (this.IsWindowState(this.WindowClosingState) || this.IsWindowState(this.WindowClosedState))
        this.StartWindowAnim(this.WindowOpenProperty);
      return this.IsWindowState(this.WindowLoopState);
    }

    private void Update()
    {
      if (Object.op_Equality((Object) this.BodyText, (Object) null))
        return;
      if (this.mState == ShopTelop.EState.NOP)
      {
        this.BodyText.set_text(string.Empty);
        if (!this.UpdateWindowState())
          return;
        this.StartTextAnim();
        this.mState = ShopTelop.EState.ACTIVE;
      }
      if (!this.mNextTextUpdated)
      {
        this.UpdateTextAnim();
      }
      else
      {
        if (!this.UpdateTextOut())
          return;
        if (!string.IsNullOrEmpty(this.mNextText))
        {
          this.StartTextAnim();
        }
        else
        {
          this.StartWindowAnim(this.WindowCloseProperty);
          this.mState = ShopTelop.EState.NOP;
        }
      }
    }

    private void UpdateTextAnim()
    {
      this.mPastSec += Time.get_deltaTime();
      using (List<ShopTelop.TextChar>.Enumerator enumerator = this.chList.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          ShopTelop.TextChar current = enumerator.Current;
          float num1 = (float) current.index * this.FadeInInterval;
          float num2 = num1 + this.FadeInSec;
          current.alpha = (double) this.mPastSec > (double) num1 ? ((double) num2 <= (double) this.mPastSec || (double) this.FadeInSec <= 0.0 ? 1f : (this.mPastSec - num1) / this.FadeInSec) : 0.0f;
        }
      }
      this.UpdateTextString();
    }

    private void UpdateTextString()
    {
      if (Object.op_Equality((Object) this.BodyText, (Object) null))
        return;
      Color color = ((Graphic) this.BodyText).get_color();
      string str1 = string.Empty;
      using (List<ShopTelop.TextChar>.Enumerator enumerator = this.chList.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          ShopTelop.TextChar current = enumerator.Current;
          char ch = current.ch;
          byte num = (byte) ((double) byte.MaxValue * (double) current.alpha);
          string str2 = string.Format("<color=#{0:X2}{1:X2}{2:X2}{3:X2}>", new object[4]{ (object) (byte) ((double) byte.MaxValue * color.r), (object) (byte) ((double) byte.MaxValue * color.g), (object) (byte) ((double) byte.MaxValue * color.b), (object) num });
          str1 = str1 + str2 + ch.ToString() + "</color>";
        }
      }
      this.BodyText.set_text(str1);
    }

    private bool UpdateTextOut()
    {
      if (!this.mFadeOut)
      {
        this.mFadeOut = true;
        this.mPastSec = 0.0f;
      }
      this.mPastSec += Time.get_deltaTime();
      bool flag = true;
      using (List<ShopTelop.TextChar>.Enumerator enumerator = this.chList.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          ShopTelop.TextChar current = enumerator.Current;
          float num1 = (float) current.index * this.FadeOutInterval;
          float num2 = num1 + this.FadeOutSec;
          float num3 = (double) this.mPastSec > (double) num1 ? ((double) num2 <= (double) this.mPastSec || (double) this.FadeOutSec <= 0.0 ? 0.0f : (num2 - this.mPastSec) / this.FadeOutSec) : 1f;
          current.alpha *= num3;
          flag &= (double) num3 <= 0.0;
        }
      }
      this.UpdateTextString();
      return flag;
    }

    private class TextChar
    {
      public int index;
      public char ch;
      public float alpha;
    }

    private enum EState
    {
      NOP,
      ACTIVE,
    }
  }
}
