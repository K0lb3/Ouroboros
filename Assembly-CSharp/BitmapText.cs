// Decompiled with JetBrains decompiler
// Type: BitmapText
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BitmapText : Text
{
  public BitmapText.FontScale AutoSize;
  private bool mDelayUpdate;

  public BitmapText()
  {
    base.\u002Ector();
  }

  public virtual Material defaultMaterial
  {
    get
    {
      return Graphic.get_defaultGraphicMaterial();
    }
  }

  private void ResizeFont()
  {
    if (0 > this.AutoSize.CountMin || this.AutoSize.CountMin >= this.AutoSize.CountMax || (this.AutoSize.SizeMin <= 0 || this.AutoSize.SizeMax <= 0))
      return;
    this.set_fontSize((int) Mathf.Lerp((float) this.AutoSize.SizeMin, (float) this.AutoSize.SizeMax, (float) (this.text.Length - this.AutoSize.CountMin) / (float) (this.AutoSize.CountMax - this.AutoSize.CountMin)));
  }

  protected virtual void Start()
  {
    ((UIBehaviour) this).Start();
    this.ResizeFont();
  }

  private void Update()
  {
    if (!this.mDelayUpdate || !this.IsParentCanvasActive)
      return;
    this.mDelayUpdate = false;
    ((Behaviour) this).set_enabled(!((Behaviour) this).get_enabled());
    ((Behaviour) this).set_enabled(!((Behaviour) this).get_enabled());
  }

  protected virtual void OnTransformParentChanged()
  {
    ((MaskableGraphic) this).OnTransformParentChanged();
  }

  public virtual string text
  {
    get
    {
      return base.get_text();
    }
    set
    {
      if (((Behaviour) this).get_enabled())
      {
        if (!this.mDelayUpdate && !this.IsParentCanvasActive)
          this.mDelayUpdate = true;
        if (!this.mDelayUpdate)
        {
          ((Behaviour) this).set_enabled(false);
          ((Behaviour) this).set_enabled(true);
        }
      }
      base.set_text(value);
      this.ResizeFont();
    }
  }

  private bool IsParentCanvasActive
  {
    get
    {
      Transform parent1;
      for (CanvasGroup canvasGroup = (CanvasGroup) ((Component) this).GetComponentInParent<CanvasGroup>(); Object.op_Inequality((Object) canvasGroup, (Object) null); canvasGroup = !Object.op_Inequality((Object) parent1, (Object) null) ? (CanvasGroup) null : (CanvasGroup) ((Component) parent1).GetComponentInParent<CanvasGroup>())
      {
        if ((double) canvasGroup.get_alpha() <= 0.0)
          return false;
        parent1 = ((Component) canvasGroup).get_transform().get_parent();
      }
      Transform parent2;
      for (Canvas canvas = (Canvas) ((Component) this).GetComponentInParent<Canvas>(); Object.op_Inequality((Object) canvas, (Object) null); canvas = !Object.op_Inequality((Object) parent2, (Object) null) ? (Canvas) null : (Canvas) ((Component) parent2).GetComponentInParent<Canvas>())
      {
        if (!((Behaviour) canvas).get_enabled())
          return false;
        parent2 = ((Component) canvas).get_transform().get_parent();
      }
      return true;
    }
  }

  [Serializable]
  public struct FontScale
  {
    public int CountMin;
    public int SizeMin;
    public int CountMax;
    public int SizeMax;
  }
}
