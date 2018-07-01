// Decompiled with JetBrains decompiler
// Type: SRPG.MultiInvitationWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  [FlowNode.Pin(130, "送信準備", FlowNode.PinTypes.Input, 130)]
  [FlowNode.Pin(190, "送信準備完了", FlowNode.PinTypes.Output, 190)]
  [FlowNode.Pin(191, "送信ウィンド閉じた", FlowNode.PinTypes.Output, 191)]
  [FlowNode.NodeType("UI/MultiInvitationWindow", 32741)]
  [FlowNode.Pin(210, "受け取りウィンド閉じる", FlowNode.PinTypes.Input, 210)]
  [FlowNode.Pin(220, "アクティブタブ", FlowNode.PinTypes.Input, 220)]
  [FlowNode.Pin(230, "履歴タブ", FlowNode.PinTypes.Input, 230)]
  [FlowNode.Pin(240, "プレイ", FlowNode.PinTypes.Input, 240)]
  [FlowNode.Pin(250, "アクティブリスト更新", FlowNode.PinTypes.Input, 250)]
  [FlowNode.Pin(260, "履歴リスト更新", FlowNode.PinTypes.Input, 260)]
  [FlowNode.Pin(290, "受け取りウィンド閉じた", FlowNode.PinTypes.Output, 290)]
  [FlowNode.Pin(300, "タブ切り替え完了", FlowNode.PinTypes.Output, 300)]
  [FlowNode.Pin(110, "送信ウィンド閉じる", FlowNode.PinTypes.Input, 110)]
  [FlowNode.Pin(100, "送信ウィンド開く", FlowNode.PinTypes.Input, 100)]
  [FlowNode.Pin(200, "受け取りウィンド開く", FlowNode.PinTypes.Input, 200)]
  [FlowNode.Pin(120, "送信フレンド選択", FlowNode.PinTypes.Input, 120)]
  public class MultiInvitationWindow : FlowNodePersistent
  {
    private FlowWindowController m_WindowController = new FlowWindowController();
    public const int INPUT_SENDWINDOW_OPEN = 100;
    public const int INPUT_SENDWINDOW_CLOSE = 110;
    public const int INPUT_SENDWINDOW_SELECTFRIEND = 120;
    public const int INPUT_SENDWINDOW_SEND = 130;
    public const int OUTPUT_SENDWINDOW_SENDOK = 190;
    public const int OUTPUT_SENDWINDOW_CLOSED = 191;
    public const int INPUT_RECEIVEWINDOW_OPEN = 200;
    public const int INPUT_RECEIVEWINDOW_CLOSE = 210;
    public const int INPUT_RECEIVEWINDOW_TAB_ACTIVE = 220;
    public const int INPUT_RECEIVEWINDOW_TAB_LOG = 230;
    public const int INPUT_RECEIVEWINDOW_PLAY = 240;
    public const int INPUT_RECEIVEWINDOW_REFRESH_ACTIVELIST = 250;
    public const int INPUT_RECEIVEWINDOW_REFRESH_LOGLIST = 260;
    public const int OUTPUT_RECEIVEWINDOW_CLOSED = 290;
    public const int OUTPUT_RECEIVEWINDOW_TBCHANGED = 300;
    public MultiInvitationSendWindow.SerializeParam m_SendWindowParam;
    public MultiInvitationReceiveWindow.SerializeParam m_ReceiveWindowParam;

    protected override void Awake()
    {
      base.Awake();
      this.m_WindowController.Initialize((FlowNode) this);
      this.m_WindowController.Add((FlowWindowBase.SerializeParamBase) this.m_SendWindowParam);
      this.m_WindowController.Add((FlowWindowBase.SerializeParamBase) this.m_ReceiveWindowParam);
    }

    private void Start()
    {
    }

    protected override void OnDestroy()
    {
      this.m_WindowController.Release();
      base.OnDestroy();
    }

    private void Update()
    {
      this.m_WindowController.Update();
    }

    public override void OnActivate(int pinID)
    {
      this.m_WindowController.OnActivate(pinID);
    }
  }
}
