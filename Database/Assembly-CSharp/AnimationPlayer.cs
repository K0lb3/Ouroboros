// Decompiled with JetBrains decompiler
// Type: AnimationPlayer
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class AnimationPlayer : MonoBehaviour
{
  private static List<AnimationPlayer> mInstances = new List<AnimationPlayer>(16);
  public static long MaxUpdateTime = 10000000;
  private List<AnimationPlayer.AnimationStateSource> mAnimationStates;
  private Animation mAnimation;
  private AnimationPlayer.RootMotionModes mRootMotionMode;
  [NonSerialized]
  public float RootMotionScale;
  [NonSerialized]
  public string RootMotionBoneName;
  private bool mLoadError;
  [NonSerialized]
  public Vector3 RootMotionInverse;
  public string DefaultAnimId;
  public AnimDef DefaultAnim;
  public bool DefaultAnimLoop;
  public AnimationPlayer.AnimationUpdateEvent OnAnimationUpdate;
  private bool mDetectedInifiteLoop;
  private float mResampleTimer;
  public bool AlwaysUpdate;
  private static bool mAllAnimationsUpdated;
  private bool mUpdated;
  private List<AnimationPlayer.AnimLoadRequest> mAnimLoadRequests;
  private Dictionary<string, AnimDef> mLoadedAnimations;

  public AnimationPlayer()
  {
    base.\u002Ector();
  }

  public AnimationPlayer.RootMotionModes RootMotionMode
  {
    get
    {
      return this.mRootMotionMode;
    }
    set
    {
      if (value == AnimationPlayer.RootMotionModes.Translate && value != this.mRootMotionMode)
      {
        Transform childRecursively = GameUtility.findChildRecursively(((Component) this).get_transform(), this.RootMotionBoneName);
        if (Object.op_Inequality((Object) childRecursively, (Object) null))
        {
          Matrix4x4 localToWorldMatrix1 = childRecursively.get_parent().get_localToWorldMatrix();
          Vector3 vector3_1 = ((Matrix4x4) @localToWorldMatrix1).MultiplyPoint(childRecursively.get_localPosition());
          Matrix4x4 localToWorldMatrix2 = childRecursively.get_parent().get_localToWorldMatrix();
          Vector3 vector3_2 = ((Matrix4x4) @localToWorldMatrix2).MultiplyPoint(Vector3.get_zero());
          Vector3 vector3_3 = Vector3.op_Subtraction(vector3_1, vector3_2);
          Transform transform = ((Component) this).get_transform();
          transform.set_position(Vector3.op_Addition(transform.get_position(), vector3_3));
          childRecursively.set_localPosition(Vector3.get_zero());
        }
      }
      this.mRootMotionMode = value;
    }
  }

  protected void SetLoadError()
  {
    this.mLoadError = true;
  }

  public void ClearLoadError()
  {
    this.mLoadError = false;
  }

  public bool HasLoadError
  {
    get
    {
      return this.mLoadError;
    }
  }

  public Animation AnimationComponent
  {
    get
    {
      return this.mAnimation;
    }
  }

  protected virtual void Start()
  {
    if (Object.op_Equality((Object) this.mAnimation, (Object) null))
    {
      Animation component = (Animation) ((Component) this).GetComponent<Animation>();
      if (Object.op_Inequality((Object) component, (Object) null))
        this.SetAnimationComponent(component);
    }
    if (!Object.op_Inequality((Object) this.DefaultAnim, (Object) null) || string.IsNullOrEmpty(this.DefaultAnimId))
      return;
    this.AddAnimation(this.DefaultAnimId, this.DefaultAnim);
    this.PlayAnimation(this.DefaultAnimId, this.DefaultAnimLoop);
  }

  protected virtual void OnEnable()
  {
    if (Object.op_Inequality((Object) this.mAnimation, (Object) null))
    {
      IEnumerator enumerator = this.mAnimation.GetEnumerator();
      try
      {
        while (enumerator.MoveNext())
          ((AnimationState) enumerator.Current).set_enabled(true);
      }
      finally
      {
        IDisposable disposable = enumerator as IDisposable;
        if (disposable != null)
          disposable.Dispose();
      }
    }
    AnimationPlayer.mInstances.Add(this);
  }

  protected virtual void OnDisable()
  {
    AnimationPlayer.mInstances.Remove(this);
  }

  protected virtual void OnDestroy()
  {
  }

  private float EvaluateCurveValue(AnimationCurve curve, float startTime, float endTime, float length)
  {
    startTime %= length;
    endTime %= length;
    if ((double) startTime > (double) endTime)
      return curve.Evaluate(length) - curve.Evaluate(startTime) + (curve.Evaluate(endTime) - curve.Evaluate(0.0f));
    return curve.Evaluate(endTime) - curve.Evaluate(startTime);
  }

  protected virtual void OnEventStart(AnimEvent e, float weight)
  {
  }

  protected virtual void OnEvent(AnimEvent e, float time, float weight)
  {
  }

  protected virtual void OnEventEnd(AnimEvent e, float weight)
  {
  }

  protected virtual bool IsEventAllowed(AnimEvent e)
  {
    return true;
  }

  protected void UpdateAnimationStates(float dt, bool forceUpdate)
  {
    if (Object.op_Equality((Object) this.mAnimation, (Object) null) || this.mDetectedInifiteLoop)
      return;
    if ((double) dt > 0.0)
      this.mResampleTimer += dt;
    Vector3 vector3 = Vector3.get_zero();
    Transform transform1 = ((Component) this).get_transform();
    Vector3 zero = Vector3.get_zero();
    int num1 = 0;
    bool flag;
    do
    {
      flag = false;
      ++num1;
      if (num1 > 100)
      {
        Debug.LogError((object) (((Object) this).get_name() + " >>> INFINITE LOOP DETECTED!!!"));
        this.mDetectedInifiteLoop = true;
        IEnumerator enumerator = this.mAnimation.GetEnumerator();
        try
        {
          while (enumerator.MoveNext())
          {
            AnimationState current = (AnimationState) enumerator.Current;
            Debug.LogError((object) string.Format("CLIP name:{0} clip:{1} clipname:{2} clipInstance:{3}", new object[4]
            {
              (object) current.get_name(),
              (object) current.get_clip(),
              (object) ((Object) current.get_clip()).get_name(),
              (object) ((Object) current.get_clip()).GetInstanceID()
            }));
          }
          return;
        }
        finally
        {
          IDisposable disposable = enumerator as IDisposable;
          if (disposable != null)
            disposable.Dispose();
        }
      }
      else
      {
        IEnumerator enumerator = this.mAnimation.GetEnumerator();
        try
        {
          if (enumerator.MoveNext())
          {
            AnimationState current = (AnimationState) enumerator.Current;
            flag = true;
            this.mAnimation.RemoveClip(current.get_clip());
          }
        }
        finally
        {
          IDisposable disposable = enumerator as IDisposable;
          if (disposable != null)
            disposable.Dispose();
        }
      }
    }
    while (flag);
    float mResampleTimer = this.mResampleTimer;
    this.mResampleTimer = 0.0f;
    float num2 = 0.0f;
    for (int index = 0; index < this.mAnimationStates.Count; ++index)
    {
      AnimationPlayer.AnimationStateSource mAnimationState = this.mAnimationStates[index];
      mAnimationState.Weight = Mathf.MoveTowards(mAnimationState.Weight, mAnimationState.DesiredWeight, mAnimationState.BlendRate * mResampleTimer);
      num2 += mAnimationState.Weight;
    }
    for (int index = 0; index < this.mAnimationStates.Count; ++index)
    {
      AnimationPlayer.AnimationStateSource mAnimationState = this.mAnimationStates[index];
      AnimDef clip = mAnimationState.Clip;
      float length = clip.Length;
      if ((double) mAnimationState.Weight <= 0.0 && (double) mAnimationState.DesiredWeight <= 0.0)
      {
        this.mAnimationStates.RemoveAt(index);
        --index;
      }
      else
      {
        float time = mAnimationState.Time;
        mAnimationState.TimeOld = time;
        mAnimationState.Time += mResampleTimer * mAnimationState.Speed;
        float num3 = mAnimationState.Weight / num2;
        if (mAnimationState.WrapMode == 2)
          mAnimationState.Time %= length;
        else if ((double) mAnimationState.Time > (double) length)
          mAnimationState.Time = length;
        if (clip.UseRootMotion && this.mRootMotionMode == AnimationPlayer.RootMotionModes.Velocity && this.RootMotionBoneName == clip.rootBoneName)
        {
          if (clip.rootTranslationX != null)
          {
            // ISSUE: explicit reference operation
            // ISSUE: variable of a reference type
            Vector3& local1 = @vector3;
            // ISSUE: explicit reference operation
            // ISSUE: explicit reference operation
            (^local1).x = (__Null) ((^local1).x + (double) this.EvaluateCurveValue(clip.rootTranslationX, time, mAnimationState.Time, length) * (double) num3);
            // ISSUE: explicit reference operation
            // ISSUE: variable of a reference type
            Vector3& local2 = @zero;
            // ISSUE: explicit reference operation
            // ISSUE: explicit reference operation
            (^local2).x = (__Null) ((^local2).x - (double) clip.rootTranslationX.Evaluate(mAnimationState.Time) * (double) num3);
          }
          if (clip.rootTranslationY != null)
          {
            // ISSUE: explicit reference operation
            // ISSUE: variable of a reference type
            Vector3& local1 = @vector3;
            // ISSUE: explicit reference operation
            // ISSUE: explicit reference operation
            (^local1).y = (__Null) ((^local1).y + (double) this.EvaluateCurveValue(clip.rootTranslationY, time, mAnimationState.Time, length) * (double) num3);
            // ISSUE: explicit reference operation
            // ISSUE: variable of a reference type
            Vector3& local2 = @zero;
            // ISSUE: explicit reference operation
            // ISSUE: explicit reference operation
            (^local2).y = (__Null) ((^local2).y - (double) clip.rootTranslationY.Evaluate(mAnimationState.Time) * (double) num3);
          }
          if (clip.rootTranslationZ != null)
          {
            // ISSUE: explicit reference operation
            // ISSUE: variable of a reference type
            Vector3& local1 = @vector3;
            // ISSUE: explicit reference operation
            // ISSUE: explicit reference operation
            (^local1).z = (__Null) ((^local1).z + (double) this.EvaluateCurveValue(clip.rootTranslationZ, time, mAnimationState.Time, length) * (double) num3);
            // ISSUE: explicit reference operation
            // ISSUE: variable of a reference type
            Vector3& local2 = @zero;
            // ISSUE: explicit reference operation
            // ISSUE: explicit reference operation
            (^local2).z = (__Null) ((^local2).z - (double) clip.rootTranslationZ.Evaluate(mAnimationState.Time) * (double) num3);
          }
        }
        string name = ((Object) mAnimationState.Clip.animation).get_name();
        ((Object) mAnimationState.Clip.animation).set_name(mAnimationState.Name);
        this.mAnimation.AddClip(mAnimationState.Clip.animation, mAnimationState.Name);
        AnimationState animationState = this.mAnimation.get_Item(mAnimationState.Name);
        animationState.set_name(animationState.get_name());
        animationState.set_time(mAnimationState.Time);
        animationState.set_weight(mAnimationState.Weight);
        animationState.set_enabled(true);
        mAnimationState.CopiedStateRef = animationState;
        ((Object) mAnimationState.Clip.animation).set_name(name);
      }
    }
    if (this.mAnimationStates.Count > 0)
    {
      this.mAnimation.Sample();
      for (int index = 0; index < this.mAnimationStates.Count; ++index)
        this.mAnimationStates[index].CopiedStateRef.set_enabled(false);
      if (this.OnAnimationUpdate != null)
        this.OnAnimationUpdate(((Component) this).get_gameObject());
    }
    this.RootMotionInverse = Vector3.get_zero();
    if (this.mRootMotionMode == AnimationPlayer.RootMotionModes.Velocity)
    {
      // ISSUE: explicit reference operation
      if ((double) ((Vector3) @vector3).get_sqrMagnitude() > 0.0)
      {
        vector3 = Vector3.op_Multiply(vector3, this.RootMotionScale);
        if ((double) Mathf.Sign((float) transform1.get_lossyScale().x) < 0.0)
        {
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          Vector3& local = @vector3;
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          (^local).x = (__Null) ((^local).x * -1.0);
        }
        if ((double) Mathf.Sign((float) transform1.get_lossyScale().z) < 0.0)
        {
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          Vector3& local = @vector3;
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          (^local).z = (__Null) ((^local).z * -1.0);
        }
        vector3.y = (__Null) 0.0;
        Transform transform2 = transform1;
        transform2.set_position(Vector3.op_Addition(transform2.get_position(), Quaternion.op_Multiply(transform1.get_rotation(), vector3)));
      }
      Transform childRecursively = GameUtility.findChildRecursively(transform1, this.RootMotionBoneName);
      if (Object.op_Inequality((Object) childRecursively, (Object) null))
      {
        Transform transform2 = childRecursively;
        transform2.set_localPosition(Vector3.op_Addition(transform2.get_localPosition(), zero));
        this.RootMotionInverse = Quaternion.op_Multiply(childRecursively.get_parent().get_rotation(), zero);
      }
    }
    else if (this.mRootMotionMode == AnimationPlayer.RootMotionModes.Discard)
    {
      Transform childRecursively = GameUtility.findChildRecursively(transform1, this.RootMotionBoneName);
      if (Object.op_Inequality((Object) childRecursively, (Object) null))
        childRecursively.set_localPosition(new Vector3(0.0f, (float) childRecursively.get_localPosition().y, 0.0f));
    }
    this.ProcessAnimationEvents();
  }

  public void SkipToAnimationEnd()
  {
    Vector3 zero1 = Vector3.get_zero();
    Transform transform1 = ((Component) this).get_transform();
    Vector3 zero2 = Vector3.get_zero();
    for (int index = 0; index < this.mAnimationStates.Count; ++index)
    {
      AnimationPlayer.AnimationStateSource mAnimationState = this.mAnimationStates[index];
      AnimDef clip = mAnimationState.Clip;
      float length = clip.Length;
      if ((double) mAnimationState.Weight <= 0.0 && (double) mAnimationState.DesiredWeight <= 0.0)
      {
        this.mAnimationStates.RemoveAt(index);
        --index;
      }
      else
      {
        if (mAnimationState.WrapMode == 2)
          mAnimationState.Time %= length;
        else if ((double) mAnimationState.Time > (double) length)
          mAnimationState.Time = length;
        if (clip.UseRootMotion && this.mRootMotionMode == AnimationPlayer.RootMotionModes.Velocity && this.RootMotionBoneName == clip.rootBoneName)
        {
          if (clip.rootTranslationX != null)
          {
            // ISSUE: explicit reference operation
            // ISSUE: variable of a reference type
            Vector3& local1 = @zero1;
            // ISSUE: explicit reference operation
            // ISSUE: explicit reference operation
            (^local1).x = (__Null) ((^local1).x + ((double) clip.rootTranslationX.Evaluate(length) - (double) clip.rootTranslationX.Evaluate(0.0f)));
            // ISSUE: explicit reference operation
            // ISSUE: variable of a reference type
            Vector3& local2 = @zero2;
            // ISSUE: explicit reference operation
            // ISSUE: explicit reference operation
            (^local2).x = (__Null) ((^local2).x - (double) clip.rootTranslationX.Evaluate(length));
          }
          if (clip.rootTranslationY != null)
          {
            // ISSUE: explicit reference operation
            // ISSUE: variable of a reference type
            Vector3& local1 = @zero1;
            // ISSUE: explicit reference operation
            // ISSUE: explicit reference operation
            (^local1).y = (__Null) ((^local1).y + ((double) clip.rootTranslationY.Evaluate(length) - (double) clip.rootTranslationY.Evaluate(0.0f)));
            // ISSUE: explicit reference operation
            // ISSUE: variable of a reference type
            Vector3& local2 = @zero2;
            // ISSUE: explicit reference operation
            // ISSUE: explicit reference operation
            (^local2).y = (__Null) ((^local2).y - (double) clip.rootTranslationY.Evaluate(length));
          }
          if (clip.rootTranslationZ != null)
          {
            // ISSUE: explicit reference operation
            // ISSUE: variable of a reference type
            Vector3& local1 = @zero1;
            // ISSUE: explicit reference operation
            // ISSUE: explicit reference operation
            (^local1).z = (__Null) ((^local1).z + ((double) clip.rootTranslationZ.Evaluate(length) - (double) clip.rootTranslationZ.Evaluate(0.0f)));
            // ISSUE: explicit reference operation
            // ISSUE: variable of a reference type
            Vector3& local2 = @zero2;
            // ISSUE: explicit reference operation
            // ISSUE: explicit reference operation
            (^local2).z = (__Null) ((^local2).z - (double) clip.rootTranslationZ.Evaluate(length));
          }
        }
      }
    }
    if (this.mRootMotionMode != AnimationPlayer.RootMotionModes.Velocity)
      return;
    // ISSUE: explicit reference operation
    if ((double) ((Vector3) @zero1).get_sqrMagnitude() > 0.0)
    {
      Vector3 vector3 = Vector3.op_Multiply(zero1, this.RootMotionScale);
      if ((double) Mathf.Sign((float) transform1.get_lossyScale().x) < 0.0)
      {
        // ISSUE: explicit reference operation
        // ISSUE: variable of a reference type
        Vector3& local = @vector3;
        // ISSUE: explicit reference operation
        // ISSUE: explicit reference operation
        (^local).x = (__Null) ((^local).x * -1.0);
      }
      if ((double) Mathf.Sign((float) transform1.get_lossyScale().z) < 0.0)
      {
        // ISSUE: explicit reference operation
        // ISSUE: variable of a reference type
        Vector3& local = @vector3;
        // ISSUE: explicit reference operation
        // ISSUE: explicit reference operation
        (^local).z = (__Null) ((^local).z * -1.0);
      }
      vector3.y = (__Null) 0.0;
      Transform transform2 = transform1;
      transform2.set_position(Vector3.op_Addition(transform2.get_position(), Quaternion.op_Multiply(transform1.get_rotation(), vector3)));
    }
    Transform childRecursively = GameUtility.findChildRecursively(transform1, this.RootMotionBoneName);
    if (!Object.op_Inequality((Object) childRecursively, (Object) null))
      return;
    Transform transform3 = childRecursively;
    transform3.set_localPosition(Vector3.op_Addition(transform3.get_localPosition(), zero2));
    this.RootMotionInverse = Quaternion.op_Multiply(childRecursively.get_parent().get_rotation(), zero2);
  }

  private void ProcessAnimationEvents()
  {
    for (int index1 = 0; index1 < this.mAnimationStates.Count; ++index1)
    {
      AnimationPlayer.AnimationStateSource mAnimationState = this.mAnimationStates[index1];
      AnimDef clip = mAnimationState.Clip;
      float length = clip.Length;
      if ((double) mAnimationState.Weight > 0.0 && clip.events != null)
      {
        for (int index2 = 0; index2 < clip.events.Length; ++index2)
        {
          AnimEvent e = clip.events[index2];
          if (!Object.op_Equality((Object) e, (Object) null) && this.IsEventAllowed(e))
          {
            float num1 = Mathf.Min(e.Start, length);
            float num2 = Mathf.Min(e.End, length);
            float num3 = mAnimationState.WrapMode != 2 || (double) mAnimationState.Time >= (double) mAnimationState.TimeOld ? mAnimationState.TimeOld : mAnimationState.TimeOld - length;
            if ((double) num1 < (double) length)
            {
              if ((double) num3 <= (double) num1 && (double) num1 < (double) mAnimationState.Time)
              {
                this.OnEventStart(e, mAnimationState.Weight);
                e.OnStart(((Component) this).get_gameObject());
              }
            }
            else if ((double) num3 < (double) num1 && (double) num1 <= (double) mAnimationState.Time)
            {
              this.OnEventStart(e, mAnimationState.Weight);
              e.OnStart(((Component) this).get_gameObject());
            }
            if ((double) num1 <= (double) mAnimationState.Time && (double) mAnimationState.Time < (double) num2)
            {
              float num4 = e.End - e.Start;
              this.OnEvent(e, mAnimationState.Time, mAnimationState.Weight);
              e.OnTick(((Component) this).get_gameObject(), (double) num4 <= 0.0 ? 0.0f : (mAnimationState.Time - e.Start) / num4);
            }
            if ((double) num2 < (double) length)
            {
              if ((double) num3 <= (double) num2 && (double) num2 < (double) mAnimationState.Time)
              {
                this.OnEventEnd(e, mAnimationState.Weight);
                e.OnEnd(((Component) this).get_gameObject());
              }
            }
            else if ((double) num3 < (double) num2 && (double) num2 <= (double) mAnimationState.Time)
            {
              this.OnEventEnd(e, mAnimationState.Weight);
              e.OnEnd(((Component) this).get_gameObject());
            }
          }
        }
      }
    }
  }

  public virtual float LoadProgress
  {
    get
    {
      int count1 = this.mLoadedAnimations.Count;
      int count2 = this.mAnimLoadRequests.Count;
      int num = count1 + count2;
      if (count1 + count2 <= 0)
        return 0.0f;
      return (float) (1.0 - (double) count2 / (double) num);
    }
  }

  public virtual bool IsLoading
  {
    get
    {
      return this.mAnimLoadRequests.Count > 0;
    }
  }

  public int LoadingAnimationCount
  {
    get
    {
      return this.mAnimLoadRequests.Count;
    }
  }

  protected virtual void Update()
  {
    if (!AnimationPlayer.mAllAnimationsUpdated)
    {
      AnimationPlayer.mAllAnimationsUpdated = true;
      long num = 0;
      Stopwatch stopwatch = new Stopwatch();
      float deltaTime = Time.get_deltaTime();
      for (int index = AnimationPlayer.mInstances.Count - 1; index >= 0; --index)
        AnimationPlayer.mInstances[index].mResampleTimer += deltaTime;
      for (int count = AnimationPlayer.mInstances.Count; count > 0 && num < AnimationPlayer.MaxUpdateTime; --count)
      {
        stopwatch.Reset();
        stopwatch.Start();
        AnimationPlayer.mInstances[0].UpdateAnimationStates(0.0f, false);
        AnimationPlayer.mInstances[0].mUpdated = true;
        AnimationPlayer.mInstances.Add(AnimationPlayer.mInstances[0]);
        AnimationPlayer.mInstances.RemoveAt(0);
        stopwatch.Stop();
        num += stopwatch.ElapsedTicks;
      }
    }
    if (this.mUpdated || !this.AlwaysUpdate)
      return;
    this.UpdateAnimationStates(0.0f, false);
    this.mUpdated = true;
  }

  protected virtual void LateUpdate()
  {
    AnimationPlayer.mAllAnimationsUpdated = false;
    this.mUpdated = false;
  }

  public void StopAll()
  {
    this.mAnimationStates.Clear();
  }

  public void StopAnimation(string id)
  {
    AnimationPlayer.AnimationStateSource state = this.FindState(id);
    if (state == null)
      return;
    this.mAnimationStates.Remove(state);
  }

  private AnimationPlayer.AnimationStateSource FindState(string id)
  {
    for (int index = this.mAnimationStates.Count - 1; index >= 0; --index)
    {
      if (this.mAnimationStates[index].Name == id)
        return this.mAnimationStates[index];
    }
    return (AnimationPlayer.AnimationStateSource) null;
  }

  public bool IsAnimationPlaying(string id)
  {
    return this.FindState(id) != null;
  }

  public float GetRemainingTime(string id)
  {
    AnimationPlayer.AnimationStateSource state = this.FindState(id);
    if (state == null || state.WrapMode == 2)
      return 0.0f;
    return state.Clip.Length - state.Time;
  }

  public void SetSpeed(string id, float speed)
  {
    AnimationPlayer.AnimationStateSource state = this.FindState(id);
    if (state == null)
      DebugUtility.LogError("Animation ID " + id + " not found");
    else
      state.Speed = speed;
  }

  public void SetAnimationComponent(Animation animComponent)
  {
    this.mAnimation = animComponent;
    if (!Object.op_Inequality((Object) this.mAnimation, (Object) null))
      return;
    this.mAnimation.set_cullingType((AnimationCullingType) 0);
  }

  public float GetNormalizedTime(string id)
  {
    AnimationPlayer.AnimationStateSource state = this.FindState(id);
    if (state == null)
      return 0.0f;
    return state.Time / state.Clip.Length;
  }

  public float GetTargetWeight(string id)
  {
    AnimationPlayer.AnimationStateSource state = this.FindState(id);
    if (state == null)
      return 0.0f;
    return state.DesiredWeight;
  }

  public void PlayAnimation(string id, bool loop, float interpTime, float startTime = 0.0f)
  {
    if ((double) interpTime <= 0.0)
      this.StopAll();
    if (!this.mLoadedAnimations.ContainsKey(id))
    {
      DebugUtility.LogError("Unknown animation ID: " + id);
    }
    else
    {
      AnimDef mLoadedAnimation = this.mLoadedAnimations[id];
      if (Object.op_Equality((Object) mLoadedAnimation, (Object) null) || Object.op_Equality((Object) mLoadedAnimation.animation, (Object) null))
      {
        DebugUtility.LogError("Animation not loaded: " + id);
      }
      else
      {
        AnimationPlayer.AnimationStateSource animationStateSource = (AnimationPlayer.AnimationStateSource) null;
        for (int index = 0; index < this.mAnimationStates.Count; ++index)
        {
          if (this.mAnimationStates[index].Name == id)
          {
            animationStateSource = this.mAnimationStates[index];
            break;
          }
        }
        if (animationStateSource == null)
        {
          animationStateSource = new AnimationPlayer.AnimationStateSource();
          animationStateSource.Clip = mLoadedAnimation;
          this.mAnimationStates.Add(animationStateSource);
        }
        animationStateSource.Time = startTime;
        animationStateSource.Name = id;
        animationStateSource.WrapMode = !loop ? (WrapMode) 0 : (WrapMode) 2;
        if ((double) interpTime > 0.0)
        {
          animationStateSource.Weight = 0.0f;
          animationStateSource.DesiredWeight = 1f;
          animationStateSource.BlendRate = 1f / interpTime;
          for (int index = 0; index < this.mAnimationStates.Count; ++index)
          {
            if (this.mAnimationStates[index] != animationStateSource)
            {
              this.mAnimationStates[index].DesiredWeight = 0.0f;
              this.mAnimationStates[index].BlendRate = animationStateSource.BlendRate;
            }
          }
        }
        else
        {
          animationStateSource.Weight = 1f;
          animationStateSource.DesiredWeight = 1f;
        }
      }
    }
  }

  protected AnimDef FindAnimation(string id)
  {
    if (!this.mLoadedAnimations.ContainsKey(id))
      return (AnimDef) null;
    AnimDef mLoadedAnimation = this.mLoadedAnimations[id];
    if (Object.op_Equality((Object) mLoadedAnimation, (Object) null) || Object.op_Equality((Object) mLoadedAnimation.animation, (Object) null))
      return (AnimDef) null;
    return mLoadedAnimation;
  }

  public float GetLength(string id)
  {
    AnimDef animation = this.FindAnimation(id);
    if (Object.op_Equality((Object) animation, (Object) null))
      return 0.0f;
    return animation.Length;
  }

  private static void HACK_Animation_AddClip(Animation animation, AnimationClip clip, string newName)
  {
    string name = ((Object) clip).get_name();
    ((Object) clip).set_name(newName);
    animation.AddClip(clip, newName);
    AnimationState animationState = animation.get_Item(newName);
    animationState.set_name(animationState.get_name());
    ((Object) clip).set_name(name);
  }

  private static void Animation_RemoveState(Animation animation, string stateName)
  {
    AnimationState animationState = animation.get_Item(stateName);
    if (TrackedReference.op_Equality((TrackedReference) animationState, (TrackedReference) null))
      return;
    AnimationState[] animationStateArray = new AnimationState[animation.GetClipCount()];
    int index = 0;
    IEnumerator enumerator = animation.GetEnumerator();
    try
    {
      while (enumerator.MoveNext())
      {
        AnimationState current = (AnimationState) enumerator.Current;
        if (Object.op_Equality((Object) current.get_clip(), (Object) animationState.get_clip()))
        {
          animationStateArray[index] = current;
          ++index;
        }
      }
    }
    finally
    {
      IDisposable disposable = enumerator as IDisposable;
      if (disposable != null)
        disposable.Dispose();
    }
  }

  public void PlayAnimation(string id, bool loop)
  {
    if (Object.op_Equality((Object) this.mAnimation, (Object) null))
      return;
    this.StopAll();
    this.PlayAnimation(id, loop, 0.0f, 0.0f);
  }

  public AnimDef GetActiveAnimation(out float position)
  {
    if (Object.op_Inequality((Object) this.mAnimation, (Object) null))
    {
      IEnumerator enumerator = this.mAnimation.GetEnumerator();
      try
      {
        while (enumerator.MoveNext())
        {
          AnimationState current = (AnimationState) enumerator.Current;
          AnimDef animation = this.FindAnimation(current.get_name());
          if (!Object.op_Equality((Object) animation, (Object) null))
          {
            position = current.get_wrapMode() != 2 ? current.get_time() : current.get_time() % current.get_length();
            return animation;
          }
        }
      }
      finally
      {
        IDisposable disposable = enumerator as IDisposable;
        if (disposable != null)
          disposable.Dispose();
      }
    }
    position = 0.0f;
    return (AnimDef) null;
  }

  public AnimDef GetAnimation(string id)
  {
    if (this.mLoadedAnimations.ContainsKey(id))
      return this.mLoadedAnimations[id];
    return (AnimDef) null;
  }

  public void LoadAnimationAsync(string id, string path)
  {
    this.mAnimLoadRequests.Add(new AnimationPlayer.AnimLoadRequest()
    {
      id = id,
      path = path,
      request = AssetManager.LoadAsync(path, typeof (AnimDef))
    });
    if (this.mAnimLoadRequests.Count != 1)
      return;
    this.StartCoroutine(this.AsyncLoadAnimation());
  }

  public void AddAnimation(string id, AnimDef anim)
  {
    this.mLoadedAnimations[id] = anim;
  }

  public void UnloadAnimation(string id)
  {
    AnimationPlayer.Animation_RemoveState(this.mAnimation, id);
    this.mLoadedAnimations.Remove(id);
  }

  [DebuggerHidden]
  private IEnumerator AsyncLoadAnimation()
  {
    // ISSUE: object of a compiler-generated type is created
    return (IEnumerator) new AnimationPlayer.\u003CAsyncLoadAnimation\u003Ec__Iterator2C()
    {
      \u003C\u003Ef__this = this
    };
  }

  private class AnimationStateSource
  {
    public float Speed = 1f;
    public float BlendRate = 1f;
    public string Name;
    public AnimDef Clip;
    public float Time;
    public float TimeOld;
    public float Weight;
    public WrapMode WrapMode;
    public AnimationState CopiedStateRef;
    public float DesiredWeight;
  }

  public enum RootMotionModes
  {
    Translate,
    Velocity,
    Discard,
  }

  private class AnimLoadRequest
  {
    public string id;
    public string path;
    public LoadRequest request;
  }

  public delegate void AnimationUpdateEvent(GameObject go);
}
