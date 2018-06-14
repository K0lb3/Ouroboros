// Decompiled with JetBrains decompiler
// Type: SRPG.FriendPresentWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;

namespace SRPG
{
  [FlowNode.Pin(190, "ルートウィンド閉じた", FlowNode.PinTypes.Output, 1001)]
  [FlowNode.Pin(140, "一括受け取り", FlowNode.PinTypes.Input, 140)]
  [FlowNode.Pin(130, "贈るタブ", FlowNode.PinTypes.Input, 130)]
  [FlowNode.Pin(120, "受け取りタブ", FlowNode.PinTypes.Input, 120)]
  [FlowNode.Pin(110, "ルートウィンド閉じる", FlowNode.PinTypes.Input, 110)]
  [FlowNode.Pin(100, "ルートウィンド開く", FlowNode.PinTypes.Input, 100)]
  [FlowNode.Pin(150, "一括送信", FlowNode.PinTypes.Input, 150)]
  [FlowNode.NodeType("UI/FriendPresentWindow", 32741)]
  [FlowNode.Pin(390, "贈ってくれた人ウィンド閉じた", FlowNode.PinTypes.Output, 1020)]
  [FlowNode.Pin(170, "受け取りリスト更新", FlowNode.PinTypes.Input, 170)]
  [FlowNode.Pin(171, "受け取りリストクリア", FlowNode.PinTypes.Input, 171)]
  [FlowNode.Pin(180, "贈るリスト更新", FlowNode.PinTypes.Input, 180)]
  [FlowNode.Pin(200, "欲しいものウィンド開く", FlowNode.PinTypes.Input, 200)]
  [FlowNode.Pin(210, "欲しいものウィンド閉じる", FlowNode.PinTypes.Input, 210)]
  [FlowNode.Pin(220, "欲しいもの選択", FlowNode.PinTypes.Input, 220)]
  [FlowNode.Pin(300, "贈ってくれた人ウィンド開く", FlowNode.PinTypes.Input, 300)]
  [FlowNode.Pin(310, "贈ってくれた人ウィンド閉じる", FlowNode.PinTypes.Input, 310)]
  [FlowNode.Pin(191, "ルートウィンド開いた", FlowNode.PinTypes.Output, 1000)]
  [FlowNode.Pin(290, "欲しいものウィンド閉じた", FlowNode.PinTypes.Output, 1010)]
  [FlowNode.Pin(160, "ウィッシュリスト更新", FlowNode.PinTypes.Input, 160)]
  public class FriendPresentWindow : FlowNodePersistent
  {
    private FlowWindowController m_WindowController = new FlowWindowController();
    public const int INPUT_ROOTWINDOW_OPEN = 100;
    public const int INPUT_ROOTWINDOW_CLOSE = 110;
    public const int INPUT_ROOTWINDOW_TAB_RECEIVE = 120;
    public const int INPUT_ROOTWINDOW_TAB_SEND = 130;
    public const int INPUT_ROOTWINDOW_RECEIVEALL = 140;
    public const int INPUT_ROOTWINDOW_SENDALL = 150;
    public const int INPUT_ROOTWINDOW_REFRESH_WISHLIST = 160;
    public const int INPUT_ROOTWINDOW_REFRESH_RECEIVELIST = 170;
    public const int INPUT_ROOTWINDOW_CLEAR_RECEIVELIST = 171;
    public const int INPUT_ROOTWINDOW_REFRESH_SENDLIST = 180;
    public const int OUTPUT_ROOTWINDOW_CLOSED = 190;
    public const int OUTPUT_ROOTWINDOW_OPEND = 191;
    public const int INPUT_WANTWINDOW_OPEN = 200;
    public const int INPUT_WANTWINDOW_CLOSE = 210;
    public const int INPUT_WANTWINDOW_ITEMSELECT = 220;
    public const int OUTPUT_WANTWINDOW_CLOSED = 290;
    public const int INPUT_GAVEWINDOW_OPEN = 300;
    public const int INPUT_GAVEWINDOW_CLOSE = 310;
    public const int OUTPUT_GAVEWINDOW_CLOSED = 390;
    public FriendPresentRootWindow.SerializeParam m_RootWindowParam;
    public FriendPresentWantWindow.SerializeParam m_WantWindowParam;
    public FriendPresentGaveWindow.SerializeParam m_GaveWindowParam;

    protected override void Awake()
    {
      base.Awake();
      this.m_WindowController.Initialize((FlowNode) this);
      this.m_WindowController.Add((FlowWindowBase.SerializeParamBase) this.m_RootWindowParam);
      this.m_WindowController.Add((FlowWindowBase.SerializeParamBase) this.m_WantWindowParam);
      this.m_WindowController.Add((FlowWindowBase.SerializeParamBase) this.m_GaveWindowParam);
      MonoSingleton<GameManager>.Instance.Player.FriendPresentReceiveList.Clear();
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
