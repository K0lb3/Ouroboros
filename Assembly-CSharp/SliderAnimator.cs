// Decompiled with JetBrains decompiler
// Type: SliderAnimator
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof (Slider))]
public class SliderAnimator : MonoBehaviour
{
  private Slider mSlider;
  private float mStartValue;
  private float mTargetValue;
  private float mTime;
  private float mTransitionTime;

  public SliderAnimator()
  {
    base.\u002Ector();
  }

  private void Awake()
  {
    this.mSlider = (Slider) ((Component) this).GetComponent<Slider>();
    ((Behaviour) this).set_enabled(false);
  }

  public void AnimateValue(float value, float time)
  {
    if ((double) time <= 0.0)
    {
      this.mSlider.set_value(value);
    }
    else
    {
      this.mStartValue = this.mSlider.get_value();
      this.mTargetValue = value;
      this.mTime = 0.0f;
      this.mTransitionTime = time;
      ((Behaviour) this).set_enabled(true);
    }
  }

  private void Update()
  {
    this.mTime += Time.get_deltaTime();
    float num = Mathf.Clamp01(this.mTime / this.mTransitionTime);
    this.mSlider.set_value(Mathf.Lerp(this.mStartValue, this.mTargetValue, num));
    if ((double) num < 1.0)
      return;
    ((Behaviour) this).set_enabled(false);
  }

  public bool IsAnimating
  {
    get
    {
      return ((Behaviour) this).get_enabled();
    }
  }
}
