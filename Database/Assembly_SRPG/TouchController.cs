namespace SRPG
{
    using System;
    using System.Runtime.CompilerServices;
    using UnityEngine;
    using UnityEngine.EventSystems;

    [AddComponentMenu("Event/TouchController")]
    public class TouchController : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler, IEventSystemHandler
    {
        private const float DragStartThreshold = 0.1f;
        public ClickEvent OnClick;
        public DragEvent OnDragDelegate;
        public DragEvent OnDragEndDelegate;
        [NonSerialized]
        public Vector2 Velocity;
        [NonSerialized]
        public Vector2 AngularVelocity;
        private Vector2 mClickPos;
        private bool mMultiTouched;
        private bool mIsTouching;
        private float mPointerDownTime;
        [NonSerialized]
        public Vector2 DragDelta;
        private float mClickRadiusThreshold;
        private RectTransform mRectTransform;
        private Vector2 mDragStart;

        public TouchController()
        {
            this.mClickRadiusThreshold = 5f;
            base..ctor();
            return;
        }

        private void Awake()
        {
            this.mRectTransform = base.GetComponent<RectTransform>();
            return;
        }

        public RectTransform GetRectTransform()
        {
            return this.mRectTransform;
        }

        public void IgnoreCurrentTouch()
        {
            this.mIsTouching = 0;
            this.Velocity = Vector2.get_zero();
            this.AngularVelocity = Vector2.get_zero();
            return;
        }

        public void OnDrag(PointerEventData eventData)
        {
            Vector2 vector;
            if (this.mIsTouching == null)
            {
                goto Label_0098;
            }
            if (Time.get_time() >= (this.mPointerDownTime + 0.1f))
            {
                goto Label_0022;
            }
            return;
        Label_0022:
            if (SRPG_TouchInputModule.IsMultiTouching == null)
            {
                goto Label_0048;
            }
            this.AngularVelocity = eventData.get_delta();
            this.DragDelta = Vector2.get_zero();
            goto Label_0098;
        Label_0048:
            vector = base.get_transform().InverseTransformPoint(eventData.get_position());
            this.Velocity = eventData.get_delta();
            this.DragDelta = vector - this.mDragStart;
            if (this.OnDragDelegate == null)
            {
                goto Label_0098;
            }
            this.OnDragDelegate();
        Label_0098:
            return;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            Vector2 vector;
            this.mClickPos = eventData.get_position();
            this.mMultiTouched = 0;
            this.mIsTouching = 1;
            this.DragDelta = Vector2.get_zero();
            this.mPointerDownTime = Time.get_time();
            this.AngularVelocity = Vector2.get_zero();
            this.Velocity = Vector2.get_zero();
            vector = base.get_transform().InverseTransformPoint(eventData.get_position());
            this.mDragStart = vector;
            return;
        }

        public unsafe void OnPointerUp(PointerEventData eventData)
        {
            Vector2 vector;
            this.mIsTouching = 0;
            if ((eventData.get_pointerPress() == base.get_gameObject()) == null)
            {
                goto Label_0068;
            }
            vector = this.mClickPos - eventData.get_position();
            if (&vector.get_magnitude() > this.mClickRadiusThreshold)
            {
                goto Label_0068;
            }
            if (this.mMultiTouched != null)
            {
                goto Label_0068;
            }
            if (this.OnClick == null)
            {
                goto Label_0068;
            }
            this.OnClick(eventData.get_position());
        Label_0068:
            if (this.OnDragEndDelegate == null)
            {
                goto Label_007E;
            }
            this.OnDragEndDelegate();
        Label_007E:
            return;
        }

        private void Start()
        {
            this.mClickRadiusThreshold = (float) (Screen.get_height() / 0x12);
            return;
        }

        private void Update()
        {
            if (SRPG_TouchInputModule.IsMultiTouching == null)
            {
                goto Label_0011;
            }
            this.mMultiTouched = 1;
        Label_0011:
            return;
        }

        public bool IsTouching
        {
            get
            {
                return this.mIsTouching;
            }
        }

        public Vector2 DragStart
        {
            get
            {
                return this.mDragStart;
            }
        }

        public Vector2 WorldSpaceVelocity
        {
            get
            {
                Camera camera;
                Transform transform;
                Vector3 vector;
                Vector3 vector2;
                camera = Camera.get_main();
                if ((camera != null) == null)
                {
                    goto Label_00A1;
                }
                transform = camera.get_transform();
                vector = transform.get_forward();
                vector2 = transform.get_right();
                &vector.y = 0f;
                &vector.Normalize();
                &vector2.y = 0f;
                &vector2.Normalize();
                return new Vector2((&vector2.x * &this.Velocity.x) + (&vector.x * &this.Velocity.y), (&vector2.z * &this.Velocity.x) + (&vector.z * &this.Velocity.y));
            Label_00A1:
                return Vector2.get_zero();
            }
        }

        public delegate void ClickEvent(Vector2 screenPos);

        public delegate void DragEvent();
    }
}

