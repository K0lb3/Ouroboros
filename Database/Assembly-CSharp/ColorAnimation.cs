// Decompiled with JetBrains decompiler
// Type: ColorAnimation
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

public class ColorAnimation : MonoBehaviour
{
  public float Duration;
  public Color ColorStart;
  public Color ColorEnd;
  public ColorAnimation.AnimationTypes AnimationType;
  public Graphic Graphic;
  public AnimatorUpdateMode UpdateMode;
  private float mAnimPos;

  public ColorAnimation()
  {
    base.\u002Ector();
  }

  private void Update()
  {
    if ((double) this.Duration <= 0.0)
      return;
    float num;
    if (this.AnimationType == ColorAnimation.AnimationTypes.Once)
    {
      this.mAnimPos += this.UpdateMode != 2 ? Time.get_deltaTime() : Time.get_unscaledDeltaTime();
      num = Mathf.Clamp01(this.mAnimPos / this.Duration);
      if ((double) num >= 1.0)
        ((Behaviour) this).set_enabled(false);
    }
    else
    {
      num = (this.UpdateMode != 2 ? Time.get_time() : Time.get_unscaledTime()) % this.Duration / this.Duration;
      if (this.AnimationType == ColorAnimation.AnimationTypes.PingPong)
      {
        num *= 2f;
        if ((double) num >= 1.0)
          num = (float) (1.0 - ((double) num - 1.0));
      }
    }
    if (!Object.op_Inequality((Object) this.Graphic, (Object) null))
      return;
    this.Graphic.set_color(Color.Lerp(this.ColorStart, this.ColorEnd, num));
  }

  public enum AnimationTypes
  {
    Loop,
    PingPong,
    Once,
  }
}
