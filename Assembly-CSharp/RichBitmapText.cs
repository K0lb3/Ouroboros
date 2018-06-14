// Decompiled with JetBrains decompiler
// Type: RichBitmapText
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

public class RichBitmapText : BitmapText
{
  public Color32 TopColor = new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue);
  public Color32 BottomColor = new Color32((byte) 0, (byte) 0, (byte) 0, byte.MaxValue);
  [Range(-1f, 1f)]
  public float Shear;

  private static Color32 Multiply(Color32 x, Color32 y)
  {
    x.r = (__Null) (int) (byte) (x.r * y.r / (int) byte.MaxValue);
    x.g = (__Null) (int) (byte) (x.g * y.g / (int) byte.MaxValue);
    x.b = (__Null) (int) (byte) (x.b * y.b / (int) byte.MaxValue);
    x.a = (__Null) (int) (byte) (x.a * y.a / (int) byte.MaxValue);
    return x;
  }

  protected virtual void OnPopulateMesh(VertexHelper toFill)
  {
    toFill.Clear();
    base.OnPopulateMesh(toFill);
    UIVertex simpleVert = (UIVertex) UIVertex.simpleVert;
    int currentVertCount = toFill.get_currentVertCount();
    int num1;
    for (int index = 0; index < currentVertCount; index = num1 + 1)
    {
      toFill.PopulateUIVertex(ref simpleVert, index);
      simpleVert.color = (__Null) RichBitmapText.Multiply((Color32) simpleVert.color, this.TopColor);
      toFill.SetUIVertex(simpleVert, index);
      int num2 = index + 1;
      toFill.PopulateUIVertex(ref simpleVert, num2);
      simpleVert.color = (__Null) RichBitmapText.Multiply((Color32) simpleVert.color, this.TopColor);
      toFill.SetUIVertex(simpleVert, num2);
      int num3 = num2 + 1;
      toFill.PopulateUIVertex(ref simpleVert, num3);
      simpleVert.color = (__Null) RichBitmapText.Multiply((Color32) simpleVert.color, this.BottomColor);
      toFill.SetUIVertex(simpleVert, num3);
      num1 = num3 + 1;
      toFill.PopulateUIVertex(ref simpleVert, num1);
      simpleVert.color = (__Null) RichBitmapText.Multiply((Color32) simpleVert.color, this.BottomColor);
      toFill.SetUIVertex(simpleVert, num1);
    }
    if ((double) this.Shear == 0.0)
      return;
    float num4 = this.Shear * (float) this.get_fontSize();
    int num5;
    for (int index = 0; index < currentVertCount; index = num5 + 1)
    {
      toFill.PopulateUIVertex(ref simpleVert, index);
      // ISSUE: explicit reference operation
      // ISSUE: variable of a reference type
      __Null& local1 = @simpleVert.position;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      (^(Vector3&) local1).x = (__Null) ((^(Vector3&) local1).x + (double) num4);
      toFill.SetUIVertex(simpleVert, index);
      int num2 = index + 1;
      toFill.PopulateUIVertex(ref simpleVert, num2);
      // ISSUE: explicit reference operation
      // ISSUE: variable of a reference type
      __Null& local2 = @simpleVert.position;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      (^(Vector3&) local2).x = (__Null) ((^(Vector3&) local2).x + (double) num4);
      toFill.SetUIVertex(simpleVert, num2);
      int num3 = num2 + 1;
      toFill.PopulateUIVertex(ref simpleVert, num3);
      // ISSUE: explicit reference operation
      // ISSUE: variable of a reference type
      __Null& local3 = @simpleVert.position;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      (^(Vector3&) local3).x = (__Null) ((^(Vector3&) local3).x - (double) num4);
      toFill.SetUIVertex(simpleVert, num3);
      num5 = num3 + 1;
      toFill.PopulateUIVertex(ref simpleVert, num5);
      // ISSUE: explicit reference operation
      // ISSUE: variable of a reference type
      __Null& local4 = @simpleVert.position;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      (^(Vector3&) local4).x = (__Null) ((^(Vector3&) local4).x - (double) num4);
      toFill.SetUIVertex(simpleVert, num5);
    }
  }
}
