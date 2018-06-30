namespace SRPG
{
    using GR;
    using System;

    [Pin(300, "贈ってくれた人ウィンド開く", 0, 300), NodeType("UI/FriendPresentWindow", 0x7fe5), Pin(100, "ルートウィンド開く", 0, 100), Pin(110, "ルートウィンド閉じる", 0, 110), Pin(160, "ウィッシュリスト更新", 0, 160), Pin(170, "受け取りリスト更新", 0, 170), Pin(310, "贈ってくれた人ウィンド閉じる", 0, 310), Pin(0xab, "受け取りリストクリア", 0, 0xab), Pin(180, "贈るリスト更新", 0, 180), Pin(200, "欲しいものウィンド開く", 0, 200), Pin(210, "欲しいものウィンド閉じる", 0, 210), Pin(220, "欲しいもの選択", 0, 220), Pin(120, "受け取りタブ", 0, 120), Pin(130, "贈るタブ", 0, 130), Pin(140, "一括受け取り", 0, 140), Pin(390, "贈ってくれた人ウィンド閉じた", 1, 0x3fc), Pin(290, "欲しいものウィンド閉じた", 1, 0x3f2), Pin(190, "ルートウィンド閉じた", 1, 0x3e9), Pin(0xbf, "ルートウィンド開いた", 1, 0x3e8), Pin(150, "一括送信", 0, 150)]
    public class FriendPresentWindow : FlowNodePersistent
    {
        public const int INPUT_ROOTWINDOW_OPEN = 100;
        public const int INPUT_ROOTWINDOW_CLOSE = 110;
        public const int INPUT_ROOTWINDOW_TAB_RECEIVE = 120;
        public const int INPUT_ROOTWINDOW_TAB_SEND = 130;
        public const int INPUT_ROOTWINDOW_RECEIVEALL = 140;
        public const int INPUT_ROOTWINDOW_SENDALL = 150;
        public const int INPUT_ROOTWINDOW_REFRESH_WISHLIST = 160;
        public const int INPUT_ROOTWINDOW_REFRESH_RECEIVELIST = 170;
        public const int INPUT_ROOTWINDOW_CLEAR_RECEIVELIST = 0xab;
        public const int INPUT_ROOTWINDOW_REFRESH_SENDLIST = 180;
        public const int OUTPUT_ROOTWINDOW_CLOSED = 190;
        public const int OUTPUT_ROOTWINDOW_OPEND = 0xbf;
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
        private FlowWindowController m_WindowController;

        public FriendPresentWindow()
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
            this.m_WindowController.Add(this.m_WantWindowParam);
            this.m_WindowController.Add(this.m_GaveWindowParam);
            MonoSingleton<GameManager>.Instance.Player.FriendPresentReceiveList.Clear();
            return;
        }

        public override void OnActivate(int pinID)
        {
            this.m_WindowController.OnActivate(pinID);
            return;
        }

        protected override void OnDestroy()
        {
            this.m_WindowController.Release();
            base.OnDestroy();
            return;
        }

        private void Start()
        {
        }

        private void Update()
        {
            this.m_WindowController.Update();
            return;
        }
    }
}

