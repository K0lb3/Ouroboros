namespace SRPG
{
    using System;
    using UnityEngine;
    using UnityEngine.EventSystems;

    public class WorldMapUI : MonoBehaviour
    {
        public Camera TargetCamera;
        private bool mDragging;
        public float ScrollSpeed;

        public WorldMapUI()
        {
            this.ScrollSpeed = 0.01f;
            base..ctor();
            return;
        }

        private void OnBeginDrag(PointerEventData eventData)
        {
            this.mDragging = 1;
            return;
        }

        private unsafe void OnDrag(PointerEventData eventData)
        {
            Transform transform1;
            Vector2 vector;
            Vector2 vector2;
            if (this.mDragging == null)
            {
                goto Label_006B;
            }
            if ((this.TargetCamera != null) == null)
            {
                goto Label_006B;
            }
            transform1 = this.TargetCamera.get_transform();
            transform1.set_position(transform1.get_position() - new Vector3(&eventData.get_delta().x * this.ScrollSpeed, 0f, &eventData.get_delta().y * this.ScrollSpeed));
        Label_006B:
            return;
        }

        private void OnEndDrag(PointerEventData eventData)
        {
            this.mDragging = 0;
            return;
        }

        private void Start()
        {
            UIEventListener listener;
            listener = base.get_gameObject().AddComponent<UIEventListener>();
            listener.onBeginDrag = new UIEventListener.PointerEvent(this.OnBeginDrag);
            listener.onEndDrag = new UIEventListener.PointerEvent(this.OnEndDrag);
            listener.onDrag = new UIEventListener.PointerEvent(this.OnDrag);
            return;
        }
    }
}

