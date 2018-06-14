// Decompiled with JetBrains decompiler
// Type: Foliage
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

[AddComponentMenu("Rendering/Batch/Foliage")]
public class Foliage : BatchChunk
{
  public Color32 TopColor;
  public Color32 BottomColor;

  public override void FillTriangles(int baseVertex, Vector3[] vertices, Vector3[] normals, Vector2[] uv, Color32[] colors, Vector3[] centers, int baseIndex, int[] indices)
  {
    Vector3 position = ((Component) this).get_transform().get_position();
    Matrix4x4 localToWorldMatrix = ((Component) this).get_transform().get_localToWorldMatrix();
    float num1 = float.MaxValue;
    float num2 = float.MinValue;
    for (int index = 0; index < this.Mesh.get_vertexCount(); ++index)
    {
      // ISSUE: explicit reference operation
      vertices[baseVertex + index] = ((Matrix4x4) @localToWorldMatrix).MultiplyPoint(this.Mesh.get_vertices()[index]);
      num1 = Mathf.Min((float) vertices[baseVertex + index].y, num1);
      num2 = Mathf.Max((float) vertices[baseVertex + index].y, num2);
    }
    if (normals != null)
    {
      for (int index = 0; index < this.Mesh.get_vertexCount(); ++index)
      {
        // ISSUE: explicit reference operation
        normals[baseVertex + index] = ((Matrix4x4) @localToWorldMatrix).MultiplyVector(this.Mesh.get_normals()[index]);
      }
    }
    if (uv != null)
    {
      for (int index = 0; index < this.Mesh.get_vertexCount(); ++index)
        uv[baseVertex + index] = this.Mesh.get_uv()[index];
    }
    if (centers != null)
    {
      for (int index = 0; index < this.Mesh.get_vertexCount(); ++index)
        centers[baseVertex + index] = position;
    }
    float num3 = num2 - num1;
    if (colors != null)
    {
      if (this.Mesh.get_colors32().Length == this.Mesh.get_vertexCount())
      {
        for (int index = 0; index < this.Mesh.get_vertexCount(); ++index)
        {
          Color32 color32_1 = this.Mesh.get_colors32()[index];
          Color32 color32_2 = Color32.Lerp(this.BottomColor, this.TopColor, ((float) vertices[baseVertex + index].y - num1) / num3);
          color32_1.r = (__Null) (int) (byte) (color32_1.r * color32_2.r / (int) byte.MaxValue);
          color32_1.g = (__Null) (int) (byte) (color32_1.g * color32_2.g / (int) byte.MaxValue);
          color32_1.b = (__Null) (int) (byte) (color32_1.b * color32_2.b / (int) byte.MaxValue);
          color32_1.a = (__Null) (int) (byte) (color32_1.a * color32_2.a / (int) byte.MaxValue);
          colors[baseVertex + index] = color32_1;
        }
      }
      else
      {
        for (int index = 0; index < this.Mesh.get_vertexCount(); ++index)
          colors[baseVertex + index] = Color32.Lerp(this.BottomColor, this.TopColor, ((float) vertices[baseVertex + index].y - num1) / num3);
      }
    }
    for (int index = 0; index < this.Mesh.get_triangles().Length; ++index)
      indices[baseIndex + index] = this.Mesh.get_triangles()[index] + baseVertex;
  }
}
