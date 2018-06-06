// Decompiled with JetBrains decompiler
// Type: SRPG.SyncColor
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [RequireComponent(typeof (Graphic))]
  public class SyncColor : MonoBehaviour
  {
    private Graphic mGraphic;
    private Color mOriginColor;
    public CanvasRenderer Source;
    [BitMask]
    public SyncColor.ColorMasks ColorMask;
    public SyncColor.SyncType Type;

    public SyncColor()
    {
      base.\u002Ector();
    }

    private void Start()
    {
      this.mGraphic = (Graphic) ((Component) this).GetComponent<Graphic>();
      this.mOriginColor = this.mGraphic.get_color();
      this.Sync();
    }

    private void LateUpdate()
    {
      this.Sync();
    }

    private void Sync()
    {
      if (Object.op_Equality((Object) this.Source, (Object) null) || Object.op_Equality((Object) this.mGraphic, (Object) null))
        return;
      Color color1 = this.Source.GetColor();
      Color color2 = (Color) null;
      switch (this.Type)
      {
        case SyncColor.SyncType.Override:
          color2 = this.mGraphic.get_color();
          if ((this.ColorMask & SyncColor.ColorMasks.R) != (SyncColor.ColorMasks) 0)
            color2.r = color1.r;
          if ((this.ColorMask & SyncColor.ColorMasks.G) != (SyncColor.ColorMasks) 0)
            color2.g = color1.g;
          if ((this.ColorMask & SyncColor.ColorMasks.B) != (SyncColor.ColorMasks) 0)
            color2.b = color1.b;
          if ((this.ColorMask & SyncColor.ColorMasks.A) != (SyncColor.ColorMasks) 0)
          {
            color2.a = color1.a;
            break;
          }
          break;
        case SyncColor.SyncType.Multi:
          color2 = this.mOriginColor;
          if ((this.ColorMask & SyncColor.ColorMasks.R) != (SyncColor.ColorMasks) 0)
          {
            // ISSUE: explicit reference operation
            // ISSUE: variable of a reference type
            Color& local = @color2;
            // ISSUE: explicit reference operation
            // ISSUE: explicit reference operation
            (^local).r = (^local).r * color1.r;
          }
          if ((this.ColorMask & SyncColor.ColorMasks.G) != (SyncColor.ColorMasks) 0)
          {
            // ISSUE: explicit reference operation
            // ISSUE: variable of a reference type
            Color& local = @color2;
            // ISSUE: explicit reference operation
            // ISSUE: explicit reference operation
            (^local).g = (^local).g * color1.g;
          }
          if ((this.ColorMask & SyncColor.ColorMasks.B) != (SyncColor.ColorMasks) 0)
          {
            // ISSUE: explicit reference operation
            // ISSUE: variable of a reference type
            Color& local = @color2;
            // ISSUE: explicit reference operation
            // ISSUE: explicit reference operation
            (^local).b = (^local).b * color1.b;
          }
          if ((this.ColorMask & SyncColor.ColorMasks.A) != (SyncColor.ColorMasks) 0)
          {
            // ISSUE: explicit reference operation
            // ISSUE: variable of a reference type
            Color& local = @color2;
            // ISSUE: explicit reference operation
            // ISSUE: explicit reference operation
            (^local).a = (^local).a * color1.a;
            break;
          }
          break;
      }
      this.mGraphic.set_color(color2);
    }

    public enum SyncType
    {
      Override,
      Multi,
    }

    [Flags]
    public enum ColorMasks
    {
      R = 1,
      G = 2,
      B = 4,
      A = 8,
    }
  }
}
