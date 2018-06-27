// Decompiled with JetBrains decompiler
// Type: StaticLightVolume
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class StaticLightVolume : MonoBehaviour
{
  public static List<StaticLightVolume> Volumes = new List<StaticLightVolume>();
  private const float GizmoColorR = 0.2f;
  private const float GizmoColorG = 0.6f;
  private const float GizmoColorB = 0.6f;
  [NonSerialized]
  public Bounds Bounds;
  [Range(0.0f, 1f)]
  public float AmbientLitToDirectLit;
  [Range(0.0f, 1f)]
  public float AmbientLitToIndirectLit;
  [Range(0.0f, 1f)]
  public float PointLitToDirectLit;
  [Range(0.0f, 1f)]
  public float PointLitToIndirectLit;
  [Range(0.1f, 4f)]
  public float DirectLightingScale;
  [Range(0.1f, 4f)]
  public float IndirectLightingScale;
  [HideInInspector]
  [SerializeField]
  private StaticLightVolume.LightProbe[] mVoxel;
  [Range(1f, 16f)]
  public int XSize;
  [Range(1f, 16f)]
  public int YSize;
  [Range(1f, 16f)]
  public int ZSize;

  public StaticLightVolume()
  {
    base.\u002Ector();
  }

  public static StaticLightVolume FindVolume(Vector3 pos)
  {
    for (int index = StaticLightVolume.Volumes.Count - 1; index >= 0; --index)
    {
      // ISSUE: explicit reference operation
      if (((Behaviour) StaticLightVolume.Volumes[index]).get_isActiveAndEnabled() && ((Bounds) @StaticLightVolume.Volumes[index].Bounds).Contains(pos))
        return StaticLightVolume.Volumes[index];
    }
    return (StaticLightVolume) null;
  }

  private Bounds CalcBounds()
  {
    Transform transform = ((Component) this).get_transform();
    return new Bounds(transform.get_position(), transform.get_localScale());
  }

  private void OnEnable()
  {
    StaticLightVolume.Volumes.Add(this);
    this.Bounds = this.CalcBounds();
  }

  private void OnDisable()
  {
    StaticLightVolume.Volumes.Remove(this);
  }

  public void CalcLightColor(Vector3 position, out Color directLit, out Color indirectLit)
  {
    // ISSUE: explicit reference operation
    position = Vector3.Max(position, ((Bounds) @this.Bounds).get_min());
    // ISSUE: explicit reference operation
    position = Vector3.Min(position, ((Bounds) @this.Bounds).get_max());
    // ISSUE: explicit reference operation
    float num1 = (float) ((Bounds) @this.Bounds).get_size().x / (float) this.XSize;
    // ISSUE: explicit reference operation
    float num2 = (float) ((Bounds) @this.Bounds).get_size().y / (float) this.YSize;
    // ISSUE: explicit reference operation
    float num3 = (float) ((Bounds) @this.Bounds).get_size().z / (float) this.ZSize;
    // ISSUE: explicit reference operation
    Vector3 vector3 = Vector3.op_Subtraction(Vector3.op_Subtraction(position, ((Bounds) @this.Bounds).get_min()), Vector3.op_Multiply(Vector3.get_one(), 0.5f));
    int num4 = Mathf.Clamp(Mathf.FloorToInt((float) vector3.x / num1), 0, this.XSize - 1);
    int num5 = Mathf.Clamp(Mathf.FloorToInt((float) vector3.y / num2), 0, this.YSize - 1);
    int num6 = Mathf.Clamp(Mathf.FloorToInt((float) vector3.z / num3), 0, this.ZSize - 1);
    int num7 = Mathf.Min(num4 + 1, this.XSize - 1);
    int num8 = Mathf.Min(num5 + 1, this.YSize - 1);
    int num9 = Mathf.Min(num6 + 1, this.ZSize - 1);
    float num10 = (float) vector3.x % num1 / num1;
    float num11 = (float) vector3.y % num2 / num2;
    float num12 = (float) vector3.z % num3 / num3;
    int num13 = this.XSize * this.YSize;
    int index1 = num4 + this.XSize * num5 + num13 * num6;
    int index2 = num7 + this.XSize * num8 + num13 * num6;
    int index3 = num4 + this.XSize * num5 + num13 * num6;
    int index4 = num7 + this.XSize * num8 + num13 * num6;
    int index5 = num4 + this.XSize * num5 + num13 * num9;
    int index6 = num7 + this.XSize * num8 + num13 * num9;
    int index7 = num4 + this.XSize * num5 + num13 * num9;
    int index8 = num7 + this.XSize * num8 + num13 * num9;
    directLit = Color.Lerp(Color.Lerp(Color.Lerp(Color32.op_Implicit(this.mVoxel[index1].DirectLightColor), Color32.op_Implicit(this.mVoxel[index2].DirectLightColor), num10), Color.Lerp(Color32.op_Implicit(this.mVoxel[index3].DirectLightColor), Color32.op_Implicit(this.mVoxel[index4].DirectLightColor), num10), num11), Color.Lerp(Color.Lerp(Color32.op_Implicit(this.mVoxel[index5].DirectLightColor), Color32.op_Implicit(this.mVoxel[index6].DirectLightColor), num10), Color.Lerp(Color32.op_Implicit(this.mVoxel[index7].DirectLightColor), Color32.op_Implicit(this.mVoxel[index8].DirectLightColor), num10), num11), num12);
    indirectLit = Color.Lerp(Color.Lerp(Color.Lerp(Color32.op_Implicit(this.mVoxel[index1].IndirectLightColor), Color32.op_Implicit(this.mVoxel[index2].IndirectLightColor), num10), Color.Lerp(Color32.op_Implicit(this.mVoxel[index3].IndirectLightColor), Color32.op_Implicit(this.mVoxel[index4].IndirectLightColor), num10), num11), Color.Lerp(Color.Lerp(Color32.op_Implicit(this.mVoxel[index5].IndirectLightColor), Color32.op_Implicit(this.mVoxel[index6].IndirectLightColor), num10), Color.Lerp(Color32.op_Implicit(this.mVoxel[index7].IndirectLightColor), Color32.op_Implicit(this.mVoxel[index8].IndirectLightColor), num10), num11), num12);
  }

  private void OnValidate()
  {
    int length = this.XSize * this.YSize * this.ZSize;
    if (this.mVoxel.Length == length)
      return;
    this.mVoxel = new StaticLightVolume.LightProbe[length];
    for (int index1 = 0; index1 < this.XSize; ++index1)
    {
      for (int index2 = 0; index2 < this.YSize; ++index2)
      {
        for (int index3 = 0; index3 < this.ZSize; ++index3)
        {
          int index4 = index1 + index2 * this.XSize + index3 * this.XSize * this.YSize;
          this.mVoxel[index4] = new StaticLightVolume.LightProbe();
          this.mVoxel[index4].DirectLightColor = Color32.op_Implicit(Color.get_black());
          this.mVoxel[index4].IndirectLightColor = Color32.op_Implicit(Color.get_black());
        }
      }
    }
  }

  private Vector3 CalcCenter(int x, int y, int z)
  {
    // ISSUE: explicit reference operation
    // ISSUE: explicit reference operation
    // ISSUE: explicit reference operation
    // ISSUE: explicit reference operation
    // ISSUE: explicit reference operation
    // ISSUE: explicit reference operation
    return new Vector3(Mathf.Lerp((float) ((Bounds) @this.Bounds).get_min().x, (float) ((Bounds) @this.Bounds).get_max().x, ((float) x + 0.5f) / (float) this.XSize), Mathf.Lerp((float) ((Bounds) @this.Bounds).get_min().y, (float) ((Bounds) @this.Bounds).get_max().y, ((float) y + 0.5f) / (float) this.YSize), Mathf.Lerp((float) ((Bounds) @this.Bounds).get_min().z, (float) ((Bounds) @this.Bounds).get_max().z, ((float) z + 0.5f) / (float) this.ZSize));
  }

  public void Bake()
  {
    Light[] objectsOfType = (Light[]) Object.FindObjectsOfType<Light>();
    Light[] all1 = Array.FindAll<Light>(objectsOfType, (Predicate<Light>) (lit => lit.get_type() == 1));
    Light[] all2 = Array.FindAll<Light>(objectsOfType, (Predicate<Light>) (lit => lit.get_type() == 2));
    Color color1 = Color.get_black();
    foreach (Light light in all1)
      color1 = Color.op_Addition(color1, Color.op_Multiply(light.get_color(), light.get_intensity()));
    Color color2 = Color.op_Multiply(color1, this.DirectLightingScale);
    color2.a = (__Null) 1.0;
    using (List<ColorBlendVolume>.Enumerator enumerator = ColorBlendVolume.Volumes.GetEnumerator())
    {
      while (enumerator.MoveNext())
        enumerator.Current.UpdateBounds();
    }
    for (int x = 0; x < this.XSize; ++x)
    {
      for (int y = 0; y < this.YSize; ++y)
      {
        for (int z = 0; z < this.ZSize; ++z)
        {
          Vector3 vector3_1 = this.CalcCenter(x, y, z);
          Color color3;
          // ISSUE: explicit reference operation
          ((Color) @color3).\u002Ector(0.0f, 0.0f, 0.0f);
          Color color4;
          // ISSUE: explicit reference operation
          ((Color) @color4).\u002Ector(0.0f, 0.0f, 0.0f);
          int index1 = x + y * this.XSize + z * this.XSize * this.YSize;
          AmbientLightSettings volume1 = AmbientLightSettings.FindVolume(vector3_1);
          color3 = Color.op_Addition(color2, Color.op_Multiply(volume1.AmbientLightColor, this.AmbientLitToDirectLit));
          for (int index2 = 0; index2 < all2.Length; ++index2)
          {
            Vector3 vector3_2 = Vector3.op_Subtraction(vector3_1, ((Component) all2[index2]).get_transform().get_position());
            // ISSUE: explicit reference operation
            float num = Mathf.Clamp01((float) (1.0 - (double) ((Vector3) @vector3_2).get_magnitude() / (double) all2[index2].get_range())) * all2[index2].get_intensity();
            // ISSUE: explicit reference operation
            // ISSUE: variable of a reference type
            Color& local1 = @color3;
            // ISSUE: explicit reference operation
            // ISSUE: explicit reference operation
            (^local1).r = (__Null) ((^local1).r + all2[index2].get_color().r * (double) num * (double) this.PointLitToDirectLit);
            // ISSUE: explicit reference operation
            // ISSUE: variable of a reference type
            Color& local2 = @color3;
            // ISSUE: explicit reference operation
            // ISSUE: explicit reference operation
            (^local2).g = (__Null) ((^local2).g + all2[index2].get_color().g * (double) num * (double) this.PointLitToDirectLit);
            // ISSUE: explicit reference operation
            // ISSUE: variable of a reference type
            Color& local3 = @color3;
            // ISSUE: explicit reference operation
            // ISSUE: explicit reference operation
            (^local3).b = (__Null) ((^local3).b + all2[index2].get_color().b * (double) num * (double) this.PointLitToDirectLit);
            // ISSUE: explicit reference operation
            // ISSUE: variable of a reference type
            Color& local4 = @color4;
            // ISSUE: explicit reference operation
            // ISSUE: explicit reference operation
            (^local4).r = (__Null) ((^local4).r + all2[index2].get_color().r * (double) num * (double) this.PointLitToIndirectLit);
            // ISSUE: explicit reference operation
            // ISSUE: variable of a reference type
            Color& local5 = @color4;
            // ISSUE: explicit reference operation
            // ISSUE: explicit reference operation
            (^local5).g = (__Null) ((^local5).g + all2[index2].get_color().g * (double) num * (double) this.PointLitToIndirectLit);
            // ISSUE: explicit reference operation
            // ISSUE: variable of a reference type
            Color& local6 = @color4;
            // ISSUE: explicit reference operation
            // ISSUE: explicit reference operation
            (^local6).b = (__Null) ((^local6).b + all2[index2].get_color().b * (double) num * (double) this.PointLitToIndirectLit);
          }
          color4 = Color.op_Multiply(color4, this.IndirectLightingScale);
          color4 = Color.op_Addition(color4, Color.op_Multiply(volume1.AmbientLightColor, this.AmbientLitToIndirectLit));
          ColorBlendVolume volume2 = ColorBlendVolume.FindVolume(vector3_1);
          if (Object.op_Inequality((Object) volume2, (Object) null))
          {
            color3 = Color.op_Multiply(color3, Color32.op_Implicit(volume2.Color));
            color4 = Color.op_Multiply(color4, Color32.op_Implicit(volume2.Color));
          }
          color3.a = (__Null) 1.0;
          color4.a = (__Null) 1.0;
          this.mVoxel[index1].DirectLightColor = Color32.op_Implicit(color3);
          this.mVoxel[index1].IndirectLightColor = Color32.op_Implicit(color4);
        }
      }
    }
  }

  [Serializable]
  public struct LightProbe
  {
    public Color32 DirectLightColor;
    public Color32 IndirectLightColor;
  }
}
