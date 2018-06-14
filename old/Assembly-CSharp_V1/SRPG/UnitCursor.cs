// Decompiled with JetBrains decompiler
// Type: SRPG.UnitCursor
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  public class UnitCursor : MonoBehaviour
  {
    public float StartScale;
    public float LoopScale;
    public float EndScale;
    public float Opacity;
    public float FadeTime;
    private float mCurrentScale;
    private float mTime;
    private float mDuration;
    private float mDesiredScale;
    private float mStartScale;
    private bool mDiscard;
    private Color mColor;
    private float mCurrentOpacity;
    private float mStartOpacity;
    private float mDesiredOpacity;

    public UnitCursor()
    {
      base.\u002Ector();
    }

    private void Start()
    {
      this.mCurrentScale = this.StartScale;
      this.AnimateScale(this.LoopScale, this.Opacity, this.FadeTime, false);
      this.Update();
    }

    public Color Color
    {
      set
      {
        this.mColor = value;
      }
    }

    private void Update()
    {
      if ((double) this.mTime <= (double) this.mDuration)
      {
        this.mTime += Time.get_deltaTime();
        float num = Mathf.Sin((float) ((double) Mathf.Clamp01(this.mTime / this.mDuration) * 3.14159274101257 * 0.5));
        this.mCurrentScale = Mathf.Lerp(this.mStartScale, this.mDesiredScale, num);
        this.mCurrentOpacity = Mathf.Lerp(this.mStartOpacity, this.mDesiredOpacity, num);
        Color mColor = this.mColor;
        // ISSUE: explicit reference operation
        // ISSUE: variable of a reference type
        Color& local = @mColor;
        // ISSUE: explicit reference operation
        // ISSUE: explicit reference operation
        (^local).a = (__Null) ((^local).a * (double) this.mCurrentOpacity);
        ((Component) this).get_transform().set_localScale(Vector3.op_Multiply(Vector3.get_one(), this.mCurrentScale));
        ((Renderer) ((Component) this).GetComponent<Renderer>()).get_material().SetColor("_color", mColor);
      }
      else
      {
        if (!this.mDiscard)
          return;
        Object.DestroyImmediate((Object) ((Component) this).get_gameObject());
      }
    }

    public void FadeOut()
    {
      this.AnimateScale(this.EndScale, 0.0f, this.FadeTime, true);
    }

    private void AnimateScale(float endScale, float opacity, float time, bool destroyLater)
    {
      this.mTime = 0.0f;
      this.mDuration = time;
      this.mStartScale = this.mCurrentScale;
      this.mDesiredScale = endScale;
      this.mDiscard = destroyLater;
      this.mStartOpacity = this.mCurrentOpacity;
      this.mDesiredOpacity = opacity;
    }
  }
}
