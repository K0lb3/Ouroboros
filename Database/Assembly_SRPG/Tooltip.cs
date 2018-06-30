namespace SRPG
{
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    public class Tooltip : MonoBehaviour
    {
        public static Vector2 TooltipPosition;
        public RectTransform Body;
        public RectTransform SizeBody;
        public Text TooltipText;
        public Text TextName;
        public Text TextDesc;
        public string CloseTrigger;
        public float DestroyDelay;
        private Animator mAnimator;
        private bool mDestroying;
        public bool CloseOnPress;

        public Tooltip()
        {
            this.DestroyDelay = 1f;
            base..ctor();
            return;
        }

        public void Close()
        {
            this.mDestroying = 1;
            if ((this.mAnimator != null) == null)
            {
                goto Label_0039;
            }
            if (string.IsNullOrEmpty(this.CloseTrigger) != null)
            {
                goto Label_0039;
            }
            this.mAnimator.SetTrigger(this.CloseTrigger);
        Label_0039:
            if (Time.get_timeScale() == 0f)
            {
                goto Label_005E;
            }
            Object.Destroy(base.get_gameObject(), this.DestroyDelay);
            goto Label_0069;
        Label_005E:
            Object.Destroy(base.get_gameObject());
        Label_0069:
            return;
        }

        public void ResetPosition()
        {
            if ((this.Body != null) == null)
            {
                goto Label_0041;
            }
            this.Body.set_anchorMin(Vector2.get_zero());
            this.Body.set_anchorMax(Vector2.get_zero());
            this.Body.set_anchoredPosition(TooltipPosition);
        Label_0041:
            return;
        }

        public static unsafe void SetTooltipPosition(RectTransform rect, Vector2 localPos)
        {
            Vector2 vector;
            CanvasScaler scaler;
            Vector3 vector2;
            vector = rect.TransformPoint(localPos);
            scaler = rect.GetComponentInParent<CanvasScaler>();
            if ((scaler != null) == null)
            {
                goto Label_005B;
            }
            vector2 = scaler.get_transform().get_localScale();
            &vector.x /= &vector2.x;
            &vector.y /= &vector2.y;
        Label_005B:
            TooltipPosition = vector;
            return;
        }

        private void Start()
        {
            this.mAnimator = base.GetComponent<Animator>();
            this.ResetPosition();
            return;
        }

        private void Update()
        {
            if (this.mDestroying != null)
            {
                goto Label_0039;
            }
            if (this.CloseOnPress == null)
            {
                goto Label_0027;
            }
            if (Input.GetMouseButton(0) != null)
            {
                goto Label_0033;
            }
            return;
            goto Label_0033;
        Label_0027:
            if (Input.GetMouseButton(0) == null)
            {
                goto Label_0033;
            }
            return;
        Label_0033:
            this.Close();
        Label_0039:
            return;
        }

        public Vector2 BodySize
        {
            get
            {
                if (this.SizeBody == null)
                {
                    goto Label_001C;
                }
                return this.SizeBody.get_sizeDelta();
            Label_001C:
                return Vector2.get_zero();
            }
        }

        public bool EnableDisp
        {
            set
            {
                CanvasGroup group;
                if (this.SizeBody == null)
                {
                    goto Label_0042;
                }
                group = this.SizeBody.GetComponent<CanvasGroup>();
                if (group == null)
                {
                    goto Label_0042;
                }
                group.set_alpha((value == null) ? 0f : 1f);
            Label_0042:
                return;
            }
        }
    }
}

