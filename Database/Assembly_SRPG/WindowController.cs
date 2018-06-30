namespace SRPG
{
    using System;
    using System.Runtime.CompilerServices;
    using UnityEngine;
    using UnityEngine.EventSystems;

    [Pin(0x15, "Closed", 1, 11), Pin(10, "Open", 0, 1), Pin(20, "Close", 0, 2), Pin(5, "Lock", 0, 5), Pin(6, "Unlock", 0, 6), Pin(11, "Opened", 1, 10)]
    public class WindowController : UIBehaviour, IFlowInterface
    {
        public WindowStateChangeEvent OnWindowStateChange;
        public Transform Body;
        public string StateInt;
        public string StateBool;
        public bool InvertState;
        public bool StartOpened;
        public bool UpdateCollision;
        public bool AutoSwap;
        public bool ToggleCanvas;
        public string OpenedState;
        public string ClosedState;
        private bool mHasCanvasStack;
        private bool mDesiredState;
        private Animator mAnimator;
        private bool mOpened;
        private bool mPollState;
        private CanvasGroup mCanvasGroup;
        private bool mSwappedOut;
        private bool mJustOpened;
        private bool mJustClosed;

        public WindowController()
        {
            this.StateInt = "st";
            this.StateBool = string.Empty;
            this.OpenedState = "opened";
            this.ClosedState = "closed";
            base..ctor();
            return;
        }

        public void Activated(int pinID)
        {
            if (pinID != 10)
            {
                goto Label_000F;
            }
            this.Open();
            return;
        Label_000F:
            if (pinID != 20)
            {
                goto Label_001E;
            }
            this.Close();
            return;
        Label_001E:
            if (pinID != 5)
            {
                goto Label_002D;
            }
            this.SetCollision(0);
            return;
        Label_002D:
            if (pinID != 6)
            {
                goto Label_003C;
            }
            this.SetCollision(1);
            return;
        Label_003C:
            return;
        }

        protected override void Awake()
        {
            base.Awake();
            this.mAnimator = base.GetComponentInChildren<Animator>();
            this.mCanvasGroup = base.GetComponent<CanvasGroup>();
            this.mHasCanvasStack = base.GetComponent<CanvasStack>() != null;
            this.SetCollision(0);
            this.mDesiredState = this.StartOpened;
            if (this.StartOpened == null)
            {
                goto Label_0053;
            }
            goto Label_005A;
        Label_0053:
            this.SetCanvas(0);
        Label_005A:
            return;
        }

        public void Close()
        {
            if (this.mDesiredState == null)
            {
                goto Label_0016;
            }
            if (this.mOpened != null)
            {
                goto Label_0017;
            }
        Label_0016:
            return;
        Label_0017:
            this.mPollState = this.mDesiredState;
            this.mDesiredState = 0;
            this.SetCollision(0);
            this.UpdateAnimator(0);
            return;
        }

        public static void CloseIfAvailable(Component c)
        {
            WindowController controller;
            controller = c.GetComponent<WindowController>();
            if ((controller != null) == null)
            {
                goto Label_0019;
            }
            controller.Close();
        Label_0019:
            return;
        }

        public void ForceClose()
        {
            if (this.mDesiredState != null)
            {
                goto Label_0017;
            }
            if (this.mOpened != null)
            {
                goto Label_0017;
            }
            return;
        Label_0017:
            if (this.mDesiredState == null)
            {
                goto Label_0034;
            }
            if (this.mOpened == null)
            {
                goto Label_0034;
            }
            this.Close();
            return;
        Label_0034:
            if (this.mDesiredState != null)
            {
                goto Label_0040;
            }
            return;
        Label_0040:
            this.mPollState = 1;
            this.mDesiredState = 0;
            this.SetCollision(0);
            this.UpdateAnimator(0);
            return;
        }

        public void ForceOpen()
        {
            if (this.mDesiredState == null)
            {
                goto Label_0017;
            }
            if (this.mOpened == null)
            {
                goto Label_0017;
            }
            return;
        Label_0017:
            if (this.mDesiredState != null)
            {
                goto Label_0034;
            }
            if (this.mOpened != null)
            {
                goto Label_0034;
            }
            this.Open();
            return;
        Label_0034:
            if (this.mDesiredState == null)
            {
                goto Label_0040;
            }
            return;
        Label_0040:
            this.mPollState = 0;
            this.mDesiredState = 1;
            this.SetCollision(1);
            this.UpdateAnimator(1);
            return;
        }

        public bool IsClosing()
        {
            if (this.mDesiredState != null)
            {
                goto Label_0018;
            }
            if (this.mOpened != null)
            {
                goto Label_0018;
            }
            return 0;
        Label_0018:
            if (this.mDesiredState == null)
            {
                goto Label_0030;
            }
            if (this.mOpened == null)
            {
                goto Label_0030;
            }
            return 0;
        Label_0030:
            if (this.mDesiredState == null)
            {
                goto Label_003D;
            }
            return 0;
        Label_003D:
            return 1;
        }

        public bool IsOpening()
        {
            if (this.mDesiredState != null)
            {
                goto Label_0018;
            }
            if (this.mOpened != null)
            {
                goto Label_0018;
            }
            return 0;
        Label_0018:
            if (this.mDesiredState == null)
            {
                goto Label_0030;
            }
            if (this.mOpened == null)
            {
                goto Label_0030;
            }
            return 0;
        Label_0030:
            if (this.mDesiredState != null)
            {
                goto Label_003D;
            }
            return 0;
        Label_003D:
            return 1;
        }

        public void OnClose()
        {
            if (string.IsNullOrEmpty(this.ClosedState) == null)
            {
                goto Label_0017;
            }
            this.mJustClosed = 1;
        Label_0017:
            return;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            if ((this.Body != null) == null)
            {
                goto Label_0032;
            }
            if (this.mSwappedOut == null)
            {
                goto Label_0032;
            }
            Object.Destroy(this.Body.get_gameObject());
        Label_0032:
            return;
        }

        public void OnOpen()
        {
            if (string.IsNullOrEmpty(this.OpenedState) == null)
            {
                goto Label_0017;
            }
            this.mJustOpened = 1;
        Label_0017:
            return;
        }

        public void Open()
        {
            if (this.mDesiredState != null)
            {
                goto Label_0016;
            }
            if (this.mOpened == null)
            {
                goto Label_0017;
            }
        Label_0016:
            return;
        Label_0017:
            if (this.mSwappedOut == null)
            {
                goto Label_0029;
            }
            this.SwapWindow(1);
        Label_0029:
            this.mPollState = this.mDesiredState == 0;
            this.mDesiredState = 1;
            this.SetCanvas(1);
            this.SetCollision(0);
            this.UpdateAnimator(1);
            return;
        }

        public static void OpenIfAvailable(Component c)
        {
            WindowController controller;
            controller = c.GetComponent<WindowController>();
            if ((controller != null) == null)
            {
                goto Label_0019;
            }
            controller.Open();
        Label_0019:
            return;
        }

        private void SetCanvas(bool visible)
        {
            Canvas canvas;
            if (this.ToggleCanvas == null)
            {
                goto Label_0035;
            }
            canvas = base.GetComponentInParent<Canvas>();
            if ((canvas != null) == null)
            {
                goto Label_0035;
            }
            canvas.set_enabled(visible);
            if (this.mHasCanvasStack == null)
            {
                goto Label_0035;
            }
            CanvasStack.SortCanvases();
        Label_0035:
            return;
        }

        public void SetCollision(bool collide)
        {
            if (this.UpdateCollision == null)
            {
                goto Label_0028;
            }
            if ((this.mCanvasGroup != null) == null)
            {
                goto Label_0028;
            }
            this.mCanvasGroup.set_blocksRaycasts(collide);
        Label_0028:
            return;
        }

        protected override void Start()
        {
            base.Start();
            if (this.StartOpened == null)
            {
                goto Label_0024;
            }
            this.UpdateAnimator(1);
            this.mPollState = 1;
            goto Label_0039;
        Label_0024:
            this.mPollState = 1;
            this.UpdateAnimator(0);
            this.SwapWindow(0);
        Label_0039:
            return;
        }

        private void SwapWindow(bool visible)
        {
            if (this.AutoSwap == null)
            {
                goto Label_0058;
            }
            if ((this.Body != null) == null)
            {
                goto Label_0058;
            }
            if (visible == null)
            {
                goto Label_0040;
            }
            this.Body.SetParent(base.get_transform(), 0);
            this.mSwappedOut = 0;
            goto Label_0058;
        Label_0040:
            this.Body.SetParent(UIUtility.Pool, 0);
            this.mSwappedOut = 1;
        Label_0058:
            return;
        }

        private unsafe void Update()
        {
            AnimatorStateInfo info;
            if ((this.mAnimator == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            if (this.mAnimator.IsInTransition(0) == null)
            {
                goto Label_0024;
            }
            return;
        Label_0024:
            info = this.mAnimator.GetCurrentAnimatorStateInfo(0);
            if (string.IsNullOrEmpty(this.OpenedState) != null)
            {
                goto Label_0054;
            }
            this.mJustOpened = &info.IsName(this.OpenedState);
        Label_0054:
            if (string.IsNullOrEmpty(this.ClosedState) != null)
            {
                goto Label_0077;
            }
            this.mJustClosed = &info.IsName(this.ClosedState);
        Label_0077:
            if (this.mPollState == null)
            {
                goto Label_00DA;
            }
            if (this.mJustOpened == null)
            {
                goto Label_00DA;
            }
            if (this.mDesiredState == null)
            {
                goto Label_00DA;
            }
            this.mPollState = 0;
            this.mOpened = 1;
            this.mJustOpened = 0;
            this.SetCollision(1);
            if (this.OnWindowStateChange == null)
            {
                goto Label_00D1;
            }
            this.OnWindowStateChange(base.get_gameObject(), 1);
        Label_00D1:
            FlowNode_GameObject.ActivateOutputLinks(this, 11);
            return;
        Label_00DA:
            if (this.mPollState == null)
            {
                goto Label_0144;
            }
            if (this.mJustClosed == null)
            {
                goto Label_0144;
            }
            if (this.mDesiredState != null)
            {
                goto Label_0144;
            }
            this.mPollState = 0;
            this.mOpened = 0;
            this.mJustClosed = 0;
            if (this.OnWindowStateChange == null)
            {
                goto Label_012D;
            }
            this.OnWindowStateChange(base.get_gameObject(), 0);
        Label_012D:
            FlowNode_GameObject.ActivateOutputLinks(this, 0x15);
            this.SwapWindow(0);
            this.SetCanvas(0);
            return;
        Label_0144:
            return;
        }

        private void UpdateAnimator(bool visible)
        {
            if (string.IsNullOrEmpty(this.StateInt) != null)
            {
                goto Label_0033;
            }
            this.mAnimator.SetInteger(this.StateInt, (visible == null) ? 0 : 1);
            goto Label_0059;
        Label_0033:
            this.mAnimator.SetBool(this.StateBool, (this.InvertState == null) ? visible : (visible == 0));
        Label_0059:
            return;
        }

        public bool IsOpened
        {
            get
            {
                return this.mOpened;
            }
        }

        public bool IsClosed
        {
            get
            {
                return (this.mOpened == 0);
            }
        }

        public delegate void WindowStateChangeEvent(GameObject go, bool visible);
    }
}

