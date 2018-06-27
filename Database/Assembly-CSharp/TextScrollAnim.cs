// Decompiled with JetBrains decompiler
// Type: TextScrollAnim
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

public class TextScrollAnim : MonoBehaviour
{
  public float MoveSpeed;
  public float ResetSpeed;
  public float ResetWaitTime;
  private float mStopTime;
  private Vector3 mPrePosition;
  private Vector3 mBasePosition;
  private RectTransform rectTrans;
  private float mParentWidth;
  private float mTextWidth;
  private string mPreText;
  private TextScrollAnim.State mState;

  public TextScrollAnim()
  {
    base.\u002Ector();
  }

  private void Start()
  {
    try
    {
      this.rectTrans = (RectTransform) ((Component) this).GetComponent<RectTransform>();
      this.mParentWidth = (float) ((RectTransform) ((Component) ((Component) this).get_transform().get_parent()).get_gameObject().GetComponent<RectTransform>()).get_sizeDelta().x;
      this.mBasePosition = this.rectTrans.get_anchoredPosition3D();
      this.mState = TextScrollAnim.State.INANIM_WAIT;
    }
    catch
    {
      this.mState = TextScrollAnim.State.NONE;
    }
  }

  private void Update()
  {
    switch (this.mState)
    {
      case TextScrollAnim.State.NONE:
      case TextScrollAnim.State.MOVE_ANIM:
      case TextScrollAnim.State.RESET_ANIM:
      case TextScrollAnim.State.WAIT_ANIM:
        if (this.IsTextChangeCheck())
        {
          this.rectTrans.set_anchoredPosition3D(new Vector3((float) this.mBasePosition.x, (float) this.rectTrans.get_anchoredPosition3D().y, (float) this.rectTrans.get_anchoredPosition3D().z));
          this.mState = TextScrollAnim.State.START_CHECK;
          break;
        }
        break;
    }
    switch (this.mState)
    {
      case TextScrollAnim.State.INANIM_WAIT:
        this.WaitInAnim();
        break;
      case TextScrollAnim.State.START_CHECK:
        this.StartCheck();
        break;
      case TextScrollAnim.State.MOVE_ANIM:
        this.MoveAnim();
        break;
      case TextScrollAnim.State.RESET_ANIM:
        this.ResetAnim();
        break;
      case TextScrollAnim.State.WAIT_ANIM:
        this.WaitAnim();
        break;
    }
  }

  private bool IsTextChangeCheck()
  {
    try
    {
      Text component = (Text) ((Component) this).GetComponent<Text>();
      if (this.mPreText != component.get_text())
      {
        this.mPreText = component.get_text();
        return true;
      }
    }
    catch
    {
    }
    return false;
  }

  private void WaitInAnim()
  {
    // ISSUE: explicit reference operation
    if (!((Vector3) @this.mPrePosition).Equals((object) this.rectTrans.get_anchoredPosition3D()))
    {
      this.mPrePosition = this.rectTrans.get_anchoredPosition3D();
    }
    else
    {
      this.mStopTime -= Time.get_deltaTime();
      if (0.0 <= (double) this.mStopTime)
        return;
      this.mState = TextScrollAnim.State.START_CHECK;
    }
  }

  private void StartCheck()
  {
    try
    {
      this.mTextWidth = ((Text) ((Component) this).GetComponent<Text>()).get_preferredWidth();
    }
    catch
    {
      this.mState = TextScrollAnim.State.NONE;
      return;
    }
    if ((double) this.mTextWidth >= (double) this.mParentWidth)
    {
      this.mStopTime = this.ResetWaitTime;
      this.mState = TextScrollAnim.State.WAIT_ANIM;
    }
    else
      this.mState = TextScrollAnim.State.NONE;
  }

  private void MoveAnim()
  {
    ((Transform) this.rectTrans).Translate(-(Time.get_deltaTime() * this.MoveSpeed), 0.0f, 0.0f, (Space) 1);
    if (0.0 <= (double) this.mTextWidth + this.rectTrans.get_anchoredPosition3D().x)
      return;
    this.rectTrans.set_anchoredPosition3D(new Vector3(this.mParentWidth, (float) this.rectTrans.get_anchoredPosition3D().y, (float) this.rectTrans.get_anchoredPosition3D().z));
    this.mState = TextScrollAnim.State.RESET_ANIM;
  }

  private void ResetAnim()
  {
    ((Transform) this.rectTrans).Translate(-(Time.get_deltaTime() * this.MoveSpeed * this.ResetSpeed), 0.0f, 0.0f, (Space) 1);
    if (this.mBasePosition.x <= this.rectTrans.get_anchoredPosition3D().x)
      return;
    this.rectTrans.set_anchoredPosition3D(new Vector3((float) this.mBasePosition.x, (float) this.rectTrans.get_anchoredPosition3D().y, (float) this.rectTrans.get_anchoredPosition3D().z));
    this.mStopTime = this.ResetWaitTime;
    this.mState = TextScrollAnim.State.WAIT_ANIM;
  }

  private void WaitAnim()
  {
    this.mStopTime -= Time.get_deltaTime();
    if (0.0 <= (double) this.mStopTime)
      return;
    this.mState = TextScrollAnim.State.MOVE_ANIM;
  }

  private enum State
  {
    NONE,
    INANIM_WAIT,
    START_CHECK,
    MOVE_ANIM,
    RESET_ANIM,
    WAIT_ANIM,
  }
}
