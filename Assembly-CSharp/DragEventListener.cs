// Decompiled with JetBrains decompiler
// Type: DragEventListener
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.EventSystems;

public class DragEventListener : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IEventSystemHandler
{
  public DragEventListener.BeginDragDelegate BeginDrag;
  public DragEventListener.BeginDragDelegate Drag;
  public DragEventListener.BeginDragDelegate EndDrag;

  public DragEventListener()
  {
    base.\u002Ector();
  }

  public void OnBeginDrag(PointerEventData eventData)
  {
    if (this.BeginDrag == null)
      return;
    this.BeginDrag(((Component) this).get_gameObject(), eventData);
  }

  public void OnDrag(PointerEventData eventData)
  {
    if (this.Drag == null)
      return;
    this.Drag(((Component) this).get_gameObject(), eventData);
  }

  public void OnEndDrag(PointerEventData eventData)
  {
    if (this.EndDrag == null)
      return;
    this.EndDrag(((Component) this).get_gameObject(), eventData);
  }

  public delegate void BeginDragDelegate(GameObject sender, PointerEventData eventData);

  public delegate void DragDelegate(GameObject sender, PointerEventData eventData);

  public delegate void EndDragDelegate(GameObject sender, PointerEventData eventData);
}
