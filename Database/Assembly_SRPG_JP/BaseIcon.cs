// Decompiled with JetBrains decompiler
// Type: SRPG.BaseIcon
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SRPG
{
  public class BaseIcon : MonoBehaviour, IPointerDownHandler, IEventSystemHandler, IGameParameter, IHoldGesture
  {
    public BaseIcon()
    {
      base.\u002Ector();
    }

    public virtual bool HasTooltip
    {
      get
      {
        return true;
      }
    }

    protected virtual void ShowTooltip(Vector2 screen)
    {
    }

    public void OnPointerDown(PointerEventData eventData)
    {
      HoldGestureObserver.StartHoldGesture((IHoldGesture) this);
    }

    public void OnPointerHoldStart()
    {
      if (!this.HasTooltip)
        return;
      RectTransform transform = (RectTransform) ((Component) this).get_transform();
      Vector2 screen = Vector2.op_Implicit(((Transform) transform).TransformPoint(Vector2.op_Implicit(Vector2.get_zero())));
      CanvasScaler componentInParent = (CanvasScaler) ((Component) transform).GetComponentInParent<CanvasScaler>();
      if (Object.op_Inequality((Object) componentInParent, (Object) null))
      {
        Vector3 localScale = ((Component) componentInParent).get_transform().get_localScale();
        // ISSUE: explicit reference operation
        // ISSUE: variable of a reference type
        Vector2& local1 = @screen;
        // ISSUE: explicit reference operation
        // ISSUE: explicit reference operation
        (^local1).x = (^local1).x / localScale.x;
        // ISSUE: explicit reference operation
        // ISSUE: variable of a reference type
        Vector2& local2 = @screen;
        // ISSUE: explicit reference operation
        // ISSUE: explicit reference operation
        (^local2).y = (^local2).y / localScale.y;
      }
      this.ShowTooltip(screen);
    }

    public void OnPointerHoldEnd()
    {
    }

    public virtual void UpdateValue()
    {
    }
  }
}
