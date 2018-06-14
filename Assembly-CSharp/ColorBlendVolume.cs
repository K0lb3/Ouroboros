// Decompiled with JetBrains decompiler
// Type: ColorBlendVolume
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ColorBlendVolume : MonoBehaviour
{
  public static List<ColorBlendVolume> Volumes = new List<ColorBlendVolume>();
  public Color32 Color;
  public float Radius;
  [NonSerialized]
  public Bounds Bounds;
  private Bounds mBoundsInner;

  public ColorBlendVolume()
  {
    base.\u002Ector();
  }

  public void UpdateBounds()
  {
    Transform transform = ((Component) this).get_transform();
    Vector3 localScale = transform.get_localScale();
    // ISSUE: explicit reference operation
    // ISSUE: variable of a reference type
    Vector3& local1 = @localScale;
    // ISSUE: explicit reference operation
    // ISSUE: explicit reference operation
    (^local1).x = (__Null) ((^local1).x + (double) this.Radius * 2.0);
    // ISSUE: explicit reference operation
    // ISSUE: variable of a reference type
    Vector3& local2 = @localScale;
    // ISSUE: explicit reference operation
    // ISSUE: explicit reference operation
    (^local2).y = (__Null) ((^local2).y + (double) this.Radius * 2.0);
    // ISSUE: explicit reference operation
    // ISSUE: variable of a reference type
    Vector3& local3 = @localScale;
    // ISSUE: explicit reference operation
    // ISSUE: explicit reference operation
    (^local3).z = (__Null) ((^local3).z + (double) this.Radius * 2.0);
    this.Bounds = new Bounds(transform.get_position(), localScale);
    this.mBoundsInner = new Bounds(transform.get_position(), transform.get_localScale());
  }

  private void OnEnable()
  {
    ColorBlendVolume.Volumes.Add(this);
    this.UpdateBounds();
  }

  private void OnDisable()
  {
    ColorBlendVolume.Volumes.Remove(this);
  }

  public static ColorBlendVolume FindVolume(Vector3 point)
  {
    for (int index = 0; index < ColorBlendVolume.Volumes.Count; ++index)
    {
      // ISSUE: explicit reference operation
      if (((Bounds) @ColorBlendVolume.Volumes[index].Bounds).Contains(point))
        return ColorBlendVolume.Volumes[index];
    }
    return (ColorBlendVolume) null;
  }

  public Color32 CalcBlendColor32(Vector3 point)
  {
    // ISSUE: explicit reference operation
    Vector3 vector3 = Vector3.op_Subtraction(point, ((Bounds) @this.Bounds).get_center());
    // ISSUE: explicit reference operation
    // ISSUE: explicit reference operation
    // ISSUE: explicit reference operation
    float num = 1f - Mathf.Max(Mathf.Clamp01(Mathf.Max(Mathf.Abs(Vector3.Dot(vector3, Vector3.get_right())) - (float) ((Bounds) @this.mBoundsInner).get_extents().x, 0.0f) / this.Radius), Mathf.Max(Mathf.Clamp01(Mathf.Max(Mathf.Abs(Vector3.Dot(vector3, Vector3.get_up())) - (float) ((Bounds) @this.mBoundsInner).get_extents().y, 0.0f) / this.Radius), Mathf.Clamp01(Mathf.Max(Mathf.Abs(Vector3.Dot(vector3, Vector3.get_forward())) - (float) ((Bounds) @this.mBoundsInner).get_extents().z, 0.0f) / this.Radius)));
    Color32 color = this.Color;
    color.a = (__Null) (int) (byte) Mathf.FloorToInt((float) byte.MaxValue * num);
    return color;
  }
}
