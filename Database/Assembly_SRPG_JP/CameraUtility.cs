// Decompiled with JetBrains decompiler
// Type: SRPG.CameraUtility
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [AddComponentMenu("Scripts/SRPG/Camera/Utility")]
  public class CameraUtility : MonoBehaviour
  {
    private float mFixedWidth;
    private float mFixedHeight;

    public CameraUtility()
    {
      base.\u002Ector();
    }

    private void Awake()
    {
      this.Reset();
      Object.Destroy((Object) this);
    }

    public void Reset()
    {
      Camera component = (Camera) ((Component) this).GetComponent<Camera>();
      if (!Object.op_Inequality((Object) component, (Object) null))
        return;
      component.set_rect(this.CalcScreenAspect());
    }

    public float CalcAspectRatio(float w, float h)
    {
      return 1f * w / h;
    }

    private Rect CalcScreenAspect()
    {
      float num1 = this.CalcAspectRatio((float) Screen.get_width(), (float) Screen.get_height()) / this.CalcAspectRatio(this.mFixedWidth, this.mFixedHeight);
      Rect rect;
      // ISSUE: explicit reference operation
      ((Rect) @rect).\u002Ector(0.0f, 0.0f, 1f, 1f);
      if (1.0 > (double) num1)
      {
        // ISSUE: explicit reference operation
        ((Rect) @rect).set_x(0.0f);
        // ISSUE: explicit reference operation
        ((Rect) @rect).set_y((float) ((1.0 - (double) num1) / 2.0));
        // ISSUE: explicit reference operation
        ((Rect) @rect).set_width(1f);
        // ISSUE: explicit reference operation
        ((Rect) @rect).set_height(num1);
      }
      else
      {
        float num2 = 1f / num1;
        // ISSUE: explicit reference operation
        ((Rect) @rect).set_x((float) ((1.0 - (double) num2) / 2.0));
        // ISSUE: explicit reference operation
        ((Rect) @rect).set_y(0.0f);
        // ISSUE: explicit reference operation
        ((Rect) @rect).set_width(num2);
        // ISSUE: explicit reference operation
        ((Rect) @rect).set_height(1f);
      }
      return rect;
    }
  }
}
