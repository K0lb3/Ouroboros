// Decompiled with JetBrains decompiler
// Type: AnimDef
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

public class AnimDef : ScriptableObject
{
  private static AnimDef mDefaultEmptyAnimation;
  public AnimationClip animation;
  public AnimationCurve rootTranslationX;
  public AnimationCurve rootTranslationY;
  public AnimationCurve rootTranslationZ;
  public bool UseRootMotion;
  public string rootBoneName;
  public bool IsParentPosZero;
  public AnimEvent[] events;
  public List<string> CurveNames;
  [HideInInspector]
  public AnimDef.CustomCurve[] CustomCurves;

  public AnimDef()
  {
    base.\u002Ector();
  }

  public static AnimDef DefaultEmptyAnimation
  {
    get
    {
      if (Object.op_Equality((Object) AnimDef.mDefaultEmptyAnimation, (Object) null))
      {
        AnimDef.mDefaultEmptyAnimation = (AnimDef) ScriptableObject.CreateInstance<AnimDef>();
        AnimDef.mDefaultEmptyAnimation.animation = new AnimationClip();
        ((Object) AnimDef.mDefaultEmptyAnimation.animation).set_hideFlags((HideFlags) 52);
        AnimDef.mDefaultEmptyAnimation.animation.set_legacy(true);
      }
      return AnimDef.mDefaultEmptyAnimation;
    }
  }

  public float Length
  {
    get
    {
      if (Object.op_Inequality((Object) this.animation, (Object) null))
        return this.animation.get_length();
      return 0.0f;
    }
  }

  public AnimationCurve FindCustomCurve(string curveName)
  {
    for (int index = 0; index < this.CustomCurves.Length; ++index)
    {
      if (this.CustomCurves[index].Name == curveName)
        return this.CustomCurves[index].CurveData;
    }
    return (AnimationCurve) null;
  }

  public bool IsValid
  {
    get
    {
      return Object.op_Inequality((Object) this.animation, (Object) null);
    }
  }

  public T[] GetAnimationEvents<T>() where T : AnimEvent
  {
    List<AnimEvent> animEventList = new List<AnimEvent>(this.events.Length);
    for (int index = 0; index < this.events.Length; ++index)
    {
      if (this.events[index] is T)
        animEventList.Add(this.events[index]);
    }
    return (T[]) animEventList.ToArray();
  }

  [Serializable]
  public struct CustomCurve
  {
    public string Name;
    public AnimationCurve CurveData;
    public string Source;
  }
}
