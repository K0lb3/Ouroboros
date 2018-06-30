namespace SRPG
{
    using System;
    using System.Runtime.CompilerServices;
    using UnityEngine;
    using UnityEngine.EventSystems;

    public class VirtualStick : MonoBehaviour
    {
        public static VirtualStick Instance;
        public RectTransform VirtualStickBG;
        public RectTransform VirtualStickFG;
        public RectTransform TouchArea;
        private bool mTouched;
        private Vector3 mTouchStart;
        private Vector3 mTouchPos;
        private Vector3 mVelocity;
        public string OpenFlagName;

        public VirtualStick()
        {
            this.mVelocity = Vector3.get_zero();
            this.OpenFlagName = "open";
            base..ctor();
            return;
        }

        [CompilerGenerated]
        private unsafe void <Start>m__49A(PointerEventData eventData)
        {
            Animator animator;
            GameObject obj2;
            Vector3 vector;
            RectTransform transform;
            RaycastResult result;
            this.VirtualStickBG.GetComponent<Animator>().SetBool(this.OpenFlagName, 1);
            vector = &eventData.get_pointerCurrentRaycast().get_gameObject().get_transform().InverseTransformPoint(eventData.get_position());
            transform = (RectTransform) this.VirtualStickBG.get_transform();
            transform.set_anchoredPosition(new Vector2(&vector.x, &vector.y));
            this.mTouchStart = vector;
            this.mTouchPos = vector;
            this.mTouched = 1;
            this.mVelocity = Vector3.get_zero();
            return;
        }

        [CompilerGenerated]
        private void <Start>m__49B(PointerEventData eventData)
        {
            Animator animator;
            this.VirtualStickBG.GetComponent<Animator>().SetBool(this.OpenFlagName, 0);
            this.mTouched = 0;
            this.mVelocity = Vector3.get_zero();
            return;
        }

        [CompilerGenerated]
        private void <Start>m__49C(PointerEventData eventData)
        {
            GameObject obj2;
            Vector3 vector;
            vector = eventData.get_pointerPress().get_transform().InverseTransformPoint(eventData.get_position());
            this.mTouchPos = vector;
            return;
        }

        public unsafe Vector2 GetVelocity(Transform cameraTransform)
        {
            Vector3 vector;
            Vector3 vector2;
            if ((cameraTransform != null) == null)
            {
                goto Label_0094;
            }
            vector = cameraTransform.get_forward();
            vector2 = cameraTransform.get_right();
            &vector.y = 0f;
            &vector.Normalize();
            &vector2.y = 0f;
            &vector2.Normalize();
            return new Vector2((&vector2.x * &this.mVelocity.x) + (&vector.x * &this.mVelocity.y), (&vector2.z * &this.mVelocity.x) + (&vector.z * &this.mVelocity.y));
        Label_0094:
            return this.mVelocity;
        }

        private void OnDisable()
        {
            if ((Instance == this) == null)
            {
                goto Label_0016;
            }
            Instance = null;
        Label_0016:
            return;
        }

        private void OnEnable()
        {
            if ((Instance == null) == null)
            {
                goto Label_0016;
            }
            Instance = this;
        Label_0016:
            return;
        }

        private void Start()
        {
            UIEventListener listener;
            listener = UIEventListener.Get(this.TouchArea);
            listener.onPointerDown = new UIEventListener.PointerEvent(this.<Start>m__49A);
            listener.onPointerUp = new UIEventListener.PointerEvent(this.<Start>m__49B);
            listener.onDrag = new UIEventListener.PointerEvent(this.<Start>m__49C);
            return;
        }

        private unsafe void Update()
        {
            Vector3 vector;
            RectTransform transform;
            RectTransform transform2;
            float num;
            Vector2 vector2;
            Vector2 vector3;
            if (this.mTouched == null)
            {
                goto Label_009F;
            }
            vector = this.mTouchPos - this.mTouchStart;
            transform = (RectTransform) this.VirtualStickFG.get_transform();
            transform2 = (RectTransform) this.VirtualStickBG.get_transform();
            num = (&transform2.get_sizeDelta().x - &transform.get_sizeDelta().x) * 0.5f;
            if (&vector.get_magnitude() < num)
            {
                goto Label_0080;
            }
            vector = &vector.get_normalized() * num;
        Label_0080:
            transform.set_anchoredPosition(vector);
            this.mVelocity = vector * (1f / num);
        Label_009F:
            return;
        }
    }
}

