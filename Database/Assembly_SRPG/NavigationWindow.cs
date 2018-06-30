namespace SRPG
{
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    public class NavigationWindow : MonoBehaviour
    {
        private const int CanvasPriority = 0x1388;
        private static NavigationWindow mCurrent;
        private static int mNumNavigations;
        private static GameObject mNavigationCanvas;
        public UnityEngine.UI.Text Text;
        public float DestroyDelay;
        public string HideTrigger;
        public Vector2 Margin;
        private Animator mAnimator;
        private RectTransform mRect;

        public NavigationWindow()
        {
            this.DestroyDelay = 1f;
            this.HideTrigger = string.Empty;
            this.Margin = new Vector2(20f, 20f);
            base..ctor();
            return;
        }

        private void Awake()
        {
            this.mRect = base.GetComponent<RectTransform>();
            this.mAnimator = base.GetComponent<Animator>();
            return;
        }

        public static void DiscardCurrent()
        {
            if ((mCurrent != null) == null)
            {
                goto Label_0071;
            }
            Object.Destroy(mCurrent.get_gameObject(), mCurrent.DestroyDelay);
            if ((mCurrent.mAnimator != null) == null)
            {
                goto Label_006B;
            }
            if (string.IsNullOrEmpty(mCurrent.HideTrigger) != null)
            {
                goto Label_006B;
            }
            mCurrent.mAnimator.SetTrigger(mCurrent.HideTrigger);
        Label_006B:
            mCurrent = null;
        Label_0071:
            return;
        }

        private void OnDestroy()
        {
            mNumNavigations -= 1;
            if (mNumNavigations != null)
            {
                goto Label_002B;
            }
            Object.Destroy(mNavigationCanvas.get_gameObject());
            mNavigationCanvas = null;
        Label_002B:
            return;
        }

        private unsafe void SetAlignment(Alignment alignment)
        {
            Vector2 vector;
            Vector2 vector2;
            Vector2 vector3;
            int num;
            int num2;
            Vector2 vector4;
            vector = Vector2.get_zero();
            vector2 = Vector2.get_zero();
            vector3 = Vector2.get_zero();
            num = alignment & 3;
            num2 = (alignment >> 2) & 3;
            if (num != null)
            {
                goto Label_0052;
            }
            &vector.x = 0f;
            &vector2.x = 0f;
            &vector3.x = &this.Margin.x;
            goto Label_00A8;
        Label_0052:
            if (num != 1)
            {
                goto Label_0076;
            }
            &vector.x = 0.5f;
            &vector2.x = 0.5f;
            goto Label_00A8;
        Label_0076:
            if (num != 2)
            {
                goto Label_00A8;
            }
            &vector.x = 1f;
            &vector2.x = 1f;
            &vector3.x = -&this.Margin.x;
        Label_00A8:
            if (num2 != null)
            {
                goto Label_00DE;
            }
            &vector.y = 0f;
            &vector2.y = 0f;
            &vector3.y = &this.Margin.y;
            goto Label_0136;
        Label_00DE:
            if (num2 != 1)
            {
                goto Label_0103;
            }
            &vector.y = 0.5f;
            &vector2.y = 0.5f;
            goto Label_0136;
        Label_0103:
            if (num2 != 2)
            {
                goto Label_0136;
            }
            &vector.y = 1f;
            &vector2.y = 1f;
            &vector3.y = -&this.Margin.y;
        Label_0136:
            vector4 = vector;
            this.mRect.set_anchorMax(vector4);
            this.mRect.set_anchorMin(vector4);
            this.mRect.set_pivot(vector2);
            this.mRect.set_anchoredPosition(vector3);
            return;
        }

        private void SetText(string text)
        {
            this.Text.set_text(text);
            return;
        }

        public static void Show(NavigationWindow template, string text, Alignment align)
        {
            Type[] typeArray1;
            Canvas canvas;
            NavigationWindow window;
            if ((template == null) == null)
            {
                goto Label_000D;
            }
            return;
        Label_000D:
            if (mNumNavigations != null)
            {
                goto Label_006D;
            }
            typeArray1 = new Type[] { typeof(Canvas), typeof(SRPG_CanvasScaler) };
            mNavigationCanvas = new GameObject("NavigationCanvas", typeArray1);
            canvas = mNavigationCanvas.GetComponent<Canvas>();
            canvas.set_renderMode(0);
            canvas.set_sortingOrder(0x1388);
            Object.DontDestroyOnLoad(mNavigationCanvas);
        Label_006D:
            window = Object.Instantiate<NavigationWindow>(template);
            window.SetAlignment(align);
            window.SetText(text);
            window.get_transform().SetParent(mNavigationCanvas.get_transform(), 0);
            mNumNavigations += 1;
            return;
        }

        private void Start()
        {
            DiscardCurrent();
            mCurrent = this;
            return;
        }

        public enum Alignment
        {
            Top = 9,
            TopLeft = 8,
            TopRight = 10,
            Middle = 5,
            MiddleLeft = 4,
            MiddleRight = 6,
            Bottom = 1,
            BottomLeft = 0,
            BottomRight = 2
        }
    }
}

