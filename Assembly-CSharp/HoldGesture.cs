// Decompiled with JetBrains decompiler
// Type: HoldGesture
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[AddComponentMenu("Event/Hold Gesture")]
public class HoldGesture : MonoBehaviour, IPointerDownHandler, IEventSystemHandler, IHoldGesture
{
  public UnityEvent OnHoldStart;
  public UnityEvent OnHoldEnd;

  public HoldGesture()
  {
    base.\u002Ector();
  }

  public void OnPointerHoldStart()
  {
    if (this.OnHoldStart == null)
      return;
    this.OnHoldStart.Invoke();
  }

  public void OnPointerHoldEnd()
  {
    if (this.OnHoldEnd == null)
      return;
    this.OnHoldEnd.Invoke();
  }

  public void OnPointerDown(PointerEventData eventData)
  {
    HoldGestureObserver.StartHoldGesture((IHoldGesture) this);
  }
}
