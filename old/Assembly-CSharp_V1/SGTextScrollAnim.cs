// Decompiled with JetBrains decompiler
// Type: SGTextScrollAnim
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

public class SGTextScrollAnim : MonoBehaviour
{
  public float MoveSpeed;
  public float ResetSpeed;
  public float ResetWaitTime;
  public float XPositionToReset;
  public float XPositionToAppear;
  private float mStopTime;
  private Vector3 mPrePosition;
  private Vector3 mBasePosition;
  private RectTransform rectTrans;
  private string mPreText;
  private SGTextScrollAnim.State mState;

  public SGTextScrollAnim()
  {
    base.\u002Ector();
  }

  private void Start()
  {
    try
    {
      this.rectTrans = (RectTransform) ((Component) this).GetComponent<RectTransform>();
      RectTransform component = (RectTransform) ((Component) ((Component) this).get_transform().get_parent()).get_gameObject().GetComponent<RectTransform>();
      this.mBasePosition = this.rectTrans.get_anchoredPosition3D();
      this.mState = SGTextScrollAnim.State.INANIM_WAIT;
    }
    catch
    {
      this.mState = SGTextScrollAnim.State.NONE;
    }
  }

  private void Update()
  {
    switch (this.mState)
    {
      case SGTextScrollAnim.State.NONE:
      case SGTextScrollAnim.State.MOVE_ANIM:
      case SGTextScrollAnim.State.RESET_ANIM:
      case SGTextScrollAnim.State.WAIT_ANIM:
        if (this.IsTextChangeCheck())
        {
          this.rectTrans.set_anchoredPosition3D(new Vector3((float) this.mBasePosition.x, (float) this.rectTrans.get_anchoredPosition3D().y, (float) this.rectTrans.get_anchoredPosition3D().z));
          this.mStopTime = this.ResetWaitTime;
          this.mState = SGTextScrollAnim.State.INANIM_WAIT;
          break;
        }
        break;
    }
    switch (this.mState)
    {
      case SGTextScrollAnim.State.INANIM_WAIT:
        this.WaitInAnim();
        break;
      case SGTextScrollAnim.State.MOVE_ANIM:
        this.MoveAnim();
        break;
      case SGTextScrollAnim.State.RESET_ANIM:
        this.ResetAnim();
        break;
      case SGTextScrollAnim.State.WAIT_ANIM:
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
    this.mStopTime -= Time.get_deltaTime();
    if (0.0 <= (double) this.mStopTime)
      return;
    this.mState = SGTextScrollAnim.State.MOVE_ANIM;
  }

  private void MoveAnim()
  {
    ((Transform) this.rectTrans).Translate(-(Time.get_deltaTime() * this.MoveSpeed), 0.0f, 0.0f, (Space) 1);
    if (this.rectTrans.get_anchoredPosition3D().x >= (double) this.XPositionToReset)
      return;
    this.rectTrans.set_anchoredPosition3D(new Vector3(this.XPositionToAppear, (float) this.rectTrans.get_anchoredPosition3D().y, (float) this.rectTrans.get_anchoredPosition3D().z));
    this.mState = SGTextScrollAnim.State.RESET_ANIM;
  }

  private void ResetAnim()
  {
    ((Transform) this.rectTrans).Translate(-(Time.get_deltaTime() * this.MoveSpeed * this.ResetSpeed), 0.0f, 0.0f, (Space) 1);
    if (this.mBasePosition.x <= this.rectTrans.get_anchoredPosition3D().x)
      return;
    this.rectTrans.set_anchoredPosition3D(new Vector3((float) this.mBasePosition.x, (float) this.rectTrans.get_anchoredPosition3D().y, (float) this.rectTrans.get_anchoredPosition3D().z));
    this.mStopTime = this.ResetWaitTime;
    this.mState = SGTextScrollAnim.State.WAIT_ANIM;
  }

  private void WaitAnim()
  {
    this.mStopTime -= Time.get_deltaTime();
    if (0.0 <= (double) this.mStopTime)
      return;
    this.mState = SGTextScrollAnim.State.MOVE_ANIM;
  }

  private enum State
  {
    NONE,
    INANIM_WAIT,
    MOVE_ANIM,
    RESET_ANIM,
    WAIT_ANIM,
  }
}
