// Decompiled with JetBrains decompiler
// Type: SRPG.WorldMapUI
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.EventSystems;

namespace SRPG
{
  public class WorldMapUI : MonoBehaviour
  {
    public Camera TargetCamera;
    private bool mDragging;
    public float ScrollSpeed;

    public WorldMapUI()
    {
      base.\u002Ector();
    }

    private void Start()
    {
      UIEventListener uiEventListener = (UIEventListener) ((Component) this).get_gameObject().AddComponent<UIEventListener>();
      uiEventListener.onBeginDrag = new UIEventListener.PointerEvent(this.OnBeginDrag);
      uiEventListener.onEndDrag = new UIEventListener.PointerEvent(this.OnEndDrag);
      uiEventListener.onDrag = new UIEventListener.PointerEvent(this.OnDrag);
    }

    private void OnBeginDrag(PointerEventData eventData)
    {
      this.mDragging = true;
    }

    private void OnEndDrag(PointerEventData eventData)
    {
      this.mDragging = false;
    }

    private void OnDrag(PointerEventData eventData)
    {
      if (!this.mDragging || !Object.op_Inequality((Object) this.TargetCamera, (Object) null))
        return;
      Transform transform = ((Component) this.TargetCamera).get_transform();
      transform.set_position(Vector3.op_Subtraction(transform.get_position(), new Vector3((float) eventData.get_delta().x * this.ScrollSpeed, 0.0f, (float) eventData.get_delta().y * this.ScrollSpeed)));
    }
  }
}
