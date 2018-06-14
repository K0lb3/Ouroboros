// Decompiled with JetBrains decompiler
// Type: FaceAnimation
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using UnityEngine;

[ExecuteInEditMode]
public class FaceAnimation : MonoBehaviour
{
  public int Columns;
  public int Rows;
  public int NumTiles;
  [NonSerialized]
  public int Face0;
  [NonSerialized]
  public int Face1;
  private Material mMaterial;
  public bool PlayAnimation;
  public FaceAnimation.FaceAnimationStruct Animation0;
  public FaceAnimation.FaceAnimationStruct Animation1;

  public FaceAnimation()
  {
    base.\u002Ector();
  }

  public void SetAnimation(CurveAsset asset)
  {
    this.Animation0.Curve = asset.FindCurve("FAC0");
    this.Animation0.Time = 0.0f;
    this.Animation0.Speed = 1f;
    this.Animation1.Curve = asset.FindCurve("FAC1");
    this.Animation1.Time = 0.0f;
    this.Animation1.Speed = 1f;
    this.PlayAnimation = true;
  }

  private void LateUpdate()
  {
    if (!this.PlayAnimation)
      return;
    if (this.Animation0.Curve == null && this.Animation1.Curve == null)
    {
      this.PlayAnimation = false;
    }
    else
    {
      this.UpdateAnimation(ref this.Animation0);
      this.UpdateAnimation(ref this.Animation1);
      if (this.Animation0.Curve != null)
        this.Face0 = Mathf.FloorToInt(this.Animation0.Curve.Evaluate(this.Animation0.Time));
      if (this.Animation1.Curve == null)
        return;
      this.Face1 = Mathf.FloorToInt(this.Animation1.Curve.Evaluate(this.Animation1.Time));
    }
  }

  private void UpdateAnimation(ref FaceAnimation.FaceAnimationStruct slot)
  {
    if (slot.Curve == null || slot.Curve.get_keys().Length < 2)
      return;
    // ISSUE: explicit reference operation
    // ISSUE: variable of a reference type
    FaceAnimation.FaceAnimationStruct& local = @slot;
    double num1 = (double) slot.Time + (double) slot.Speed * (double) Time.get_deltaTime();
    Keyframe keyframe = slot.Curve.get_Item(slot.Curve.get_length() - 1);
    // ISSUE: explicit reference operation
    double time = (double) ((Keyframe) @keyframe).get_time();
    double num2 = num1 % time;
    // ISSUE: explicit reference operation
    (^local).Time = (float) num2;
  }

  private void OnWillRenderObject()
  {
    Vector4 zero = Vector4.get_zero();
    this.mMaterial = ((Renderer) ((Component) this).GetComponent<Renderer>()).get_material();
    this.Face0 = Mathf.Clamp(this.Face0, 0, this.NumTiles - 1);
    this.Face1 = Mathf.Clamp(this.Face1, 0, this.NumTiles - 1);
    float num1 = 1f / (float) this.Columns;
    float num2 = 1f / (float) this.Rows;
    zero.x = (__Null) ((double) (this.Face0 % this.Columns) * (double) num1);
    zero.y = (__Null) (-(double) num2 * (double) (this.Face0 / this.Columns));
    this.mMaterial.SetVector("_texParam1", zero);
    zero.x = (__Null) ((double) (this.Face1 % this.Columns) * (double) num1);
    zero.y = (__Null) (-(double) num2 * (double) (this.Face1 / this.Columns));
    this.mMaterial.SetVector("_texParam2", zero);
  }

  public struct FaceAnimationStruct
  {
    public AnimationCurve Curve;
    public float Time;
    public float Speed;
  }
}
