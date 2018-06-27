// Decompiled with JetBrains decompiler
// Type: SRPG.FlowCommonWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Diagnostics;

namespace SRPG
{
  [FlowNode.Pin(400, "選択", FlowNode.PinTypes.Input, 400)]
  [FlowNode.NodeType("UI/FlowCommonWindow", 32741)]
  [FlowNode.Pin(50, "開始", FlowNode.PinTypes.Output, 50)]
  [FlowNode.Pin(100, "ウィンド開く", FlowNode.PinTypes.Input, 100)]
  [FlowNode.Pin(190, "開く", FlowNode.PinTypes.Output, 190)]
  [FlowNode.Pin(191, "開いた", FlowNode.PinTypes.Output, 191)]
  [FlowNode.Pin(200, "ウィンド閉じる", FlowNode.PinTypes.Input, 200)]
  [FlowNode.Pin(290, "閉じる", FlowNode.PinTypes.Output, 290)]
  [FlowNode.Pin(291, "閉じた", FlowNode.PinTypes.Output, 291)]
  [FlowNode.Pin(300, "更新", FlowNode.PinTypes.Input, 300)]
  [FlowNode.Pin(390, "更新した", FlowNode.PinTypes.Output, 390)]
  [FlowNode.Pin(490, "選択した", FlowNode.PinTypes.Output, 490)]
  [FlowNode.Pin(500, "長押し", FlowNode.PinTypes.Input, 500)]
  [FlowNode.Pin(590, "長押しした", FlowNode.PinTypes.Output, 590)]
  public class FlowCommonWindow : FlowNodePersistent
  {
    private FlowWindowController m_WindowController = new FlowWindowController();
    public const int OUTPUT_WINDOW_START = 50;
    public const int INPUT_WINDOW_OPEN = 100;
    public const int OUTPUT_WINDOW_OPEN = 190;
    public const int OUTPUT_WINDOW_OPENED = 191;
    public const int INPUT_WINDOW_CLOSE = 200;
    public const int OUTPUT_WINDOW_CLOSE = 290;
    public const int OUTPUT_WINDOW_CLOSED = 291;
    public const int INPUT_WINDOW_REFRESH = 300;
    public const int OUTPUT_WINDOW_REFRESH = 390;
    public const int INPUT_WINDOW_SELECT = 400;
    public const int OUTPUT_WINDOW_SELECT = 490;
    public const int INPUT_WINDOW_HOLD = 500;
    public const int OUTPUT_WINDOW_HOLD = 590;
    public FlowCommonWindow.RootWindow.SerializeParam m_RootWindowParam;
    private FlowCommonWindow.RootWindow m_RootWindow;

    public FlowCommonWindow.RootWindow rootWindow
    {
      get
      {
        return this.m_RootWindow;
      }
    }

    protected override void Awake()
    {
      base.Awake();
      this.m_WindowController.Initialize((FlowNode) this);
      this.m_WindowController.Add((FlowWindowBase.SerializeParamBase) this.m_RootWindowParam);
      this.m_RootWindow = this.m_WindowController.GetWindow<FlowCommonWindow.RootWindow>();
    }

    [DebuggerHidden]
    private IEnumerator Start()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new FlowCommonWindow.\u003CStart\u003Ec__IteratorEF() { \u003C\u003Ef__this = this };
    }

    protected override void OnDestroy()
    {
      this.m_RootWindow = (FlowCommonWindow.RootWindow) null;
      this.m_WindowController.Release();
      base.OnDestroy();
    }

    private void Update()
    {
      this.m_WindowController.Update();
    }

    private void LateUpdate()
    {
      this.m_WindowController.LateUpdate();
    }

    public override void OnActivate(int pinID)
    {
      this.m_WindowController.OnActivate(pinID);
    }

    public class RootWindow : FlowWindowBase
    {
      private EventCall m_CustomEvent;
      private SerializeValueList m_ValueList;
      private bool m_Destroy;

      public override string name
      {
        get
        {
          return "FlowCommonWindow.RootWindow";
        }
      }

      public override void Initialize(FlowWindowBase.SerializeParamBase param)
      {
        base.Initialize(param);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) param.window, (UnityEngine.Object) null))
        {
          this.m_CustomEvent = this.GetChildComponent<EventCall>("root");
          SerializeValueBehaviour childComponent = this.GetChildComponent<SerializeValueBehaviour>("root");
          this.m_ValueList = !UnityEngine.Object.op_Inequality((UnityEngine.Object) childComponent, (UnityEngine.Object) null) ? new SerializeValueList() : childComponent.list;
        }
        this.m_Destroy = false;
        this.Close(true);
      }

      public override void Release()
      {
        base.Release();
      }

      public override void Start()
      {
        this.OnEvent("START");
        this.StartUp();
      }

      public override int Update()
      {
        base.Update();
        if (this.isClosed)
          this.SetActiveChild(false);
        return -1;
      }

      private void OnEvent(string key)
      {
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_CustomEvent, (UnityEngine.Object) null))
          return;
        this.m_CustomEvent.Invoke(key, string.Empty, (object) this.m_ValueList);
      }

      public override int OnActivate(int pinId)
      {
        switch (pinId)
        {
          case 100:
            this.OnEvent("OPEN");
            this.m_Destroy = false;
            this.Open();
            return 190;
          case 200:
            this.OnEvent("CLOSE");
            this.m_Destroy = true;
            this.Close(false);
            return 290;
          case 300:
            this.OnEvent("REFRESH");
            return 390;
          case 400:
            this.OnEvent("SELECT");
            return 490;
          case 500:
            this.OnEvent("HOLD");
            return 590;
          default:
            return -1;
        }
      }

      protected override int OnOpened()
      {
        this.OnEvent("OPENED");
        return 191;
      }

      protected override int OnClosed()
      {
        if (!this.m_Destroy)
          return -1;
        this.OnEvent("CLOSED");
        return 291;
      }

      [Serializable]
      public class SerializeParam : FlowWindowBase.SerializeParamBase
      {
        public override System.Type type
        {
          get
          {
            return typeof (FlowCommonWindow.RootWindow);
          }
        }
      }
    }
  }
}
