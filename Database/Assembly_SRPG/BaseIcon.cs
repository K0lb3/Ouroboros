namespace SRPG
{
    using System;
    using UnityEngine;
    using UnityEngine.EventSystems;
    using UnityEngine.UI;

    public class BaseIcon : MonoBehaviour, IPointerDownHandler, IEventSystemHandler, IGameParameter, IHoldGesture
    {
        public BaseIcon()
        {
            base..ctor();
            return;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            HoldGestureObserver.StartHoldGesture(this);
            return;
        }

        public void OnPointerHoldEnd()
        {
        }

        public unsafe void OnPointerHoldStart()
        {
            RectTransform transform;
            Vector2 vector;
            CanvasScaler scaler;
            Vector3 vector2;
            if (this.HasTooltip == null)
            {
                goto Label_007D;
            }
            transform = (RectTransform) base.get_transform();
            vector = transform.TransformPoint(Vector2.get_zero());
            scaler = transform.GetComponentInParent<CanvasScaler>();
            if ((scaler != null) == null)
            {
                goto Label_0076;
            }
            vector2 = scaler.get_transform().get_localScale();
            &vector.x /= &vector2.x;
            &vector.y /= &vector2.y;
        Label_0076:
            this.ShowTooltip(vector);
        Label_007D:
            return;
        }

        protected virtual void ShowTooltip(Vector2 screen)
        {
        }

        public virtual void UpdateValue()
        {
        }

        public virtual bool HasTooltip
        {
            get
            {
                return 1;
            }
        }
    }
}

