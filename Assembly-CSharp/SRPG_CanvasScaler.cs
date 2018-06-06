// Decompiled with JetBrains decompiler
// Type: SRPG_CanvasScaler
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[ExecuteInEditMode]
[AddComponentMenu("Layout/Canvas Scaler (SRPG)")]
public class SRPG_CanvasScaler : CanvasScaler
{
  public const float MinScreenWidth = 1200f;
  public const float MinScreenHeight = 750f;
  public static bool UseKuroObi;

  public SRPG_CanvasScaler()
  {
    base.\u002Ector();
  }

  protected virtual void Awake()
  {
    ((UIBehaviour) this).Awake();
    this.set_uiScaleMode((CanvasScaler.ScaleMode) 1);
    if (SRPG_CanvasScaler.UseKuroObi)
    {
      this.set_screenMatchMode((CanvasScaler.ScreenMatchMode) 1);
    }
    else
    {
      this.set_screenMatchMode((CanvasScaler.ScreenMatchMode) 0);
      this.set_matchWidthOrHeight(1f);
    }
    this.set_referenceResolution(new Vector2(1200f, 750f));
  }
}
