// Decompiled with JetBrains decompiler
// Type: ObjectAnimator
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

public class ObjectAnimator : MonoBehaviour
{
  private ObjectAnimator.CurveType mCurveType;
  private AnimationCurve mCurve;
  private Vector3 mStartPos;
  private Vector3 mEndPos;
  private Quaternion mStartRot;
  private Quaternion mEndRot;
  private Vector3 mStartScale;
  private Vector3 mEndScale;
  private float mTime;
  private float mDuration;
  private bool mPositionSet;
  private bool mRotationSet;
  private bool mScaleSet;

  public ObjectAnimator()
  {
    base.\u002Ector();
  }

  public bool isMoving
  {
    get
    {
      return ((Behaviour) this).get_enabled();
    }
  }

  public float NormalizedTime
  {
    get
    {
      if ((double) this.mDuration > 0.0)
        return Mathf.Clamp01(this.mTime / this.mDuration);
      return 0.0f;
    }
  }

  private void Update()
  {
    if ((double) this.mTime < (double) this.mDuration)
    {
      this.mTime = Mathf.Min(this.mTime + Time.get_deltaTime(), this.mDuration);
      float t = this.mTime / this.mDuration;
      float num = this.mCurve == null ? this.mCurveType.Evaluate(t) : this.mCurve.Evaluate(t);
      Transform transform = ((Component) this).get_transform();
      if (this.mPositionSet)
        transform.set_position(Vector3.Lerp(this.mStartPos, this.mEndPos, num));
      if (this.mRotationSet)
        transform.set_rotation(Quaternion.Slerp(this.mStartRot, this.mEndRot, num));
      if (!this.mScaleSet)
        return;
      transform.set_localScale(Vector3.Lerp(this.mStartScale, this.mEndScale, num));
    }
    else
      ((Behaviour) this).set_enabled(false);
  }

  public void ScaleTo(Vector3 scale, float duration, ObjectAnimator.CurveType curveType)
  {
    this.mPositionSet = false;
    this.mRotationSet = false;
    this.mScaleSet = true;
    this.mTime = 0.0f;
    if ((double) duration > 0.0)
    {
      this.mStartScale = ((Component) this).get_transform().get_localScale();
      this.mEndScale = scale;
      this.mCurve = (AnimationCurve) null;
      this.mCurveType = curveType;
      this.mDuration = duration;
    }
    else
    {
      ((Component) this).get_transform().set_localScale(scale);
      this.mDuration = 0.0f;
    }
    ((Behaviour) this).set_enabled(true);
  }

  public void AnimateTo(Vector3 position, Quaternion rotation, float duration, AnimationCurve curve)
  {
    this.AnimateTo(position, rotation, duration, ObjectAnimator.CurveType.Linear);
    this.mCurve = curve;
  }

  public void AnimateTo(Vector3 position, Quaternion rotation, float duration, ObjectAnimator.CurveType curveType)
  {
    this.mPositionSet = true;
    this.mRotationSet = true;
    this.mScaleSet = false;
    this.mTime = 0.0f;
    if ((double) duration > 0.0)
    {
      this.mStartPos = ((Component) this).get_transform().get_position();
      this.mStartRot = ((Component) this).get_transform().get_rotation();
      this.mEndPos = position;
      this.mEndRot = rotation;
      this.mCurve = (AnimationCurve) null;
      this.mCurveType = curveType;
      this.mDuration = duration;
    }
    else
    {
      ((Component) this).get_transform().set_position(position);
      ((Component) this).get_transform().set_rotation(rotation);
      this.mDuration = 0.0f;
    }
    ((Behaviour) this).set_enabled(true);
  }

  public static ObjectAnimator Get(Component component)
  {
    return ObjectAnimator.Get(component.get_gameObject());
  }

  public static ObjectAnimator Get(GameObject obj)
  {
    ObjectAnimator objectAnimator = (ObjectAnimator) obj.GetComponent<ObjectAnimator>();
    if (Object.op_Equality((Object) objectAnimator, (Object) null))
      objectAnimator = (ObjectAnimator) obj.AddComponent<ObjectAnimator>();
    return objectAnimator;
  }

  public enum CurveType
  {
    Linear,
    EaseIn,
    EaseOut,
    EaseInOut,
  }
}
