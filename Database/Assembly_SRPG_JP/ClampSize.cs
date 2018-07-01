// Decompiled with JetBrains decompiler
// Type: SRPG.ClampSize
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.EventSystems;

namespace SRPG
{
  [ExecuteInEditMode]
  public class ClampSize : UIBehaviour
  {
    private RectTransform mTransform;
    public RectTransform Target;
    public bool ClampX;
    public float XSize;
    public float XMargin;
    public float XPadding;
    public bool ClampY;
    public float YSize;
    public float YMargin;
    public float YPadding;

    public ClampSize()
    {
      base.\u002Ector();
    }

    protected virtual void Awake()
    {
      base.Awake();
    }

    protected virtual void OnRectTransformDimensionsChange()
    {
      base.OnRectTransformDimensionsChange();
      this.Resize();
    }

    public void Resize()
    {
      if (Object.op_Equality((Object) this.mTransform, (Object) null))
        this.mTransform = (RectTransform) ((Component) this).get_transform();
      if (Object.op_Equality((Object) this.Target, (Object) null))
        return;
      if (!((Transform) this.Target).IsChildOf((Transform) this.mTransform))
      {
        Debug.LogError((object) (((Object) this.Target).get_name() + " is not child of " + ((Object) this).get_name()));
      }
      else
      {
        Rect rect = this.mTransform.get_rect();
        // ISSUE: explicit reference operation
        Vector2 size = ((Rect) @rect).get_size();
        if (this.ClampX)
        {
          float num = Mathf.Floor(((float) size.x - this.XMargin - this.XPadding) / this.XSize) * this.XSize + this.XMargin;
          size.x = (__Null) (double) num;
        }
        if (this.ClampY)
        {
          float num = Mathf.Floor(((float) size.y - this.YMargin - this.YPadding) / this.YSize) * this.YSize + this.YMargin;
          size.y = (__Null) (double) num;
        }
        this.Target.set_sizeDelta(size);
      }
    }
  }
}
