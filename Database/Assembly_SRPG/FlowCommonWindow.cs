namespace SRPG
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    [Pin(400, "選択", 0, 400), NodeType("UI/FlowCommonWindow", 0x7fe5), Pin(50, "開始", 1, 50), Pin(100, "ウィンド開く", 0, 100), Pin(190, "開く", 1, 190), Pin(0xbf, "開いた", 1, 0xbf), Pin(200, "ウィンド閉じる", 0, 200), Pin(290, "閉じる", 1, 290), Pin(0x123, "閉じた", 1, 0x123), Pin(300, "更新", 0, 300), Pin(390, "更新した", 1, 390), Pin(490, "選択した", 1, 490), Pin(500, "長押し", 0, 500), Pin(590, "長押しした", 1, 590)]
    public class FlowCommonWindow : FlowNodePersistent
    {
        public const int OUTPUT_WINDOW_START = 50;
        public const int INPUT_WINDOW_OPEN = 100;
        public const int OUTPUT_WINDOW_OPEN = 190;
        public const int OUTPUT_WINDOW_OPENED = 0xbf;
        public const int INPUT_WINDOW_CLOSE = 200;
        public const int OUTPUT_WINDOW_CLOSE = 290;
        public const int OUTPUT_WINDOW_CLOSED = 0x123;
        public const int INPUT_WINDOW_REFRESH = 300;
        public const int OUTPUT_WINDOW_REFRESH = 390;
        public const int INPUT_WINDOW_SELECT = 400;
        public const int OUTPUT_WINDOW_SELECT = 490;
        public const int INPUT_WINDOW_HOLD = 500;
        public const int OUTPUT_WINDOW_HOLD = 590;
        public RootWindow.SerializeParam m_RootWindowParam;
        private FlowWindowController m_WindowController;
        private RootWindow m_RootWindow;

        public FlowCommonWindow()
        {
            this.m_WindowController = new FlowWindowController();
            base..ctor();
            return;
        }

        protected override void Awake()
        {
            base.Awake();
            this.m_WindowController.Initialize(this);
            this.m_WindowController.Add(this.m_RootWindowParam);
            this.m_RootWindow = this.m_WindowController.GetWindow<RootWindow>();
            return;
        }

        private void LateUpdate()
        {
            this.m_WindowController.LateUpdate();
            return;
        }

        public override void OnActivate(int pinID)
        {
            this.m_WindowController.OnActivate(pinID);
            return;
        }

        protected override void OnDestroy()
        {
            this.m_RootWindow = null;
            this.m_WindowController.Release();
            base.OnDestroy();
            return;
        }

        [DebuggerHidden]
        private IEnumerator Start()
        {
            <Start>c__Iterator10B iteratorb;
            iteratorb = new <Start>c__Iterator10B();
            iteratorb.<>f__this = this;
            return iteratorb;
        }

        private void Update()
        {
            this.m_WindowController.Update();
            return;
        }

        public RootWindow rootWindow
        {
            get
            {
                return this.m_RootWindow;
            }
        }

        [CompilerGenerated]
        private sealed class <Start>c__Iterator10B : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal int $PC;
            internal object $current;
            internal FlowCommonWindow <>f__this;

            public <Start>c__Iterator10B()
            {
                base..ctor();
                return;
            }

            [DebuggerHidden]
            public void Dispose()
            {
                this.$PC = -1;
                return;
            }

            public bool MoveNext()
            {
                uint num;
                num = this.$PC;
                this.$PC = -1;
                switch (num)
                {
                    case 0:
                        goto Label_0021;

                    case 1:
                        goto Label_0049;
                }
                goto Label_0078;
            Label_0021:
                this.<>f__this.m_WindowController.Start();
                goto Label_0049;
            Label_0036:
                this.$current = null;
                this.$PC = 1;
                goto Label_007A;
            Label_0049:
                if (this.<>f__this.m_WindowController.IsStartUp() == null)
                {
                    goto Label_0036;
                }
                this.<>f__this.ActivateOutputLinks(50);
                goto Label_0078;
            Label_0078:
                return 0;
            Label_007A:
                return 1;
            }

            [DebuggerHidden]
            public void Reset()
            {
                throw new NotSupportedException();
            }

            object IEnumerator<object>.Current
            {
                [DebuggerHidden]
                get
                {
                    return this.$current;
                }
            }

            object IEnumerator.Current
            {
                [DebuggerHidden]
                get
                {
                    return this.$current;
                }
            }
        }

        public class RootWindow : FlowWindowBase
        {
            private EventCall m_CustomEvent;
            private SerializeValueList m_ValueList;
            private bool m_Destroy;

            public RootWindow()
            {
                base..ctor();
                return;
            }

            public override void Initialize(FlowWindowBase.SerializeParamBase param)
            {
                SerializeValueBehaviour behaviour;
                base.Initialize(param);
                if ((param.window != null) == null)
                {
                    goto Label_005D;
                }
                this.m_CustomEvent = base.GetChildComponent<EventCall>("root");
                behaviour = base.GetChildComponent<SerializeValueBehaviour>("root");
                if ((behaviour != null) == null)
                {
                    goto Label_0052;
                }
                this.m_ValueList = behaviour.list;
                goto Label_005D;
            Label_0052:
                this.m_ValueList = new SerializeValueList();
            Label_005D:
                this.m_Destroy = 0;
                base.Close(1);
                return;
            }

            public override int OnActivate(int pinId)
            {
                if (pinId != 100)
                {
                    goto Label_0026;
                }
                this.OnEvent("OPEN");
                this.m_Destroy = 0;
                base.Open();
                return 190;
            Label_0026:
                if (pinId != 200)
                {
                    goto Label_0050;
                }
                this.OnEvent("CLOSE");
                this.m_Destroy = 1;
                base.Close(0);
                return 290;
            Label_0050:
                if (pinId != 300)
                {
                    goto Label_006C;
                }
                this.OnEvent("REFRESH");
                return 390;
            Label_006C:
                if (pinId != 400)
                {
                    goto Label_0088;
                }
                this.OnEvent("SELECT");
                return 490;
            Label_0088:
                if (pinId != 500)
                {
                    goto Label_00A4;
                }
                this.OnEvent("HOLD");
                return 590;
            Label_00A4:
                return -1;
            }

            protected override int OnClosed()
            {
                if (this.m_Destroy == null)
                {
                    goto Label_001C;
                }
                this.OnEvent("CLOSED");
                return 0x123;
            Label_001C:
                return -1;
            }

            private void OnEvent(string key)
            {
                if ((this.m_CustomEvent != null) == null)
                {
                    goto Label_0028;
                }
                this.m_CustomEvent.Invoke(key, string.Empty, this.m_ValueList);
            Label_0028:
                return;
            }

            protected override int OnOpened()
            {
                this.OnEvent("OPENED");
                return 0xbf;
            }

            public override void Release()
            {
                base.Release();
                return;
            }

            public override void Start()
            {
                this.OnEvent("START");
                base.StartUp();
                return;
            }

            public override int Update()
            {
                base.Update();
                if (base.isClosed == null)
                {
                    goto Label_0019;
                }
                base.SetActiveChild(0);
            Label_0019:
                return -1;
            }

            public override string name
            {
                get
                {
                    return "FlowCommonWindow.RootWindow";
                }
            }

            [Serializable]
            public class SerializeParam : FlowWindowBase.SerializeParamBase
            {
                public SerializeParam()
                {
                    base..ctor();
                    return;
                }

                public override Type type
                {
                    get
                    {
                        return typeof(FlowCommonWindow.RootWindow);
                    }
                }
            }
        }
    }
}

