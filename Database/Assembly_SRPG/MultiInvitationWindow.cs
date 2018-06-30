namespace SRPG
{
    using System;

    [Pin(130, "送信準備", 0, 130), Pin(190, "送信準備完了", 1, 190), Pin(0xbf, "送信ウィンド閉じた", 1, 0xbf), NodeType("UI/MultiInvitationWindow", 0x7fe5), Pin(210, "受け取りウィンド閉じる", 0, 210), Pin(220, "アクティブタブ", 0, 220), Pin(230, "履歴タブ", 0, 230), Pin(240, "プレイ", 0, 240), Pin(250, "アクティブリスト更新", 0, 250), Pin(260, "履歴リスト更新", 0, 260), Pin(290, "受け取りウィンド閉じた", 1, 290), Pin(300, "タブ切り替え完了", 1, 300), Pin(110, "送信ウィンド閉じる", 0, 110), Pin(100, "送信ウィンド開く", 0, 100), Pin(200, "受け取りウィンド開く", 0, 200), Pin(120, "送信フレンド選択", 0, 120)]
    public class MultiInvitationWindow : FlowNodePersistent
    {
        public const int INPUT_SENDWINDOW_OPEN = 100;
        public const int INPUT_SENDWINDOW_CLOSE = 110;
        public const int INPUT_SENDWINDOW_SELECTFRIEND = 120;
        public const int INPUT_SENDWINDOW_SEND = 130;
        public const int OUTPUT_SENDWINDOW_SENDOK = 190;
        public const int OUTPUT_SENDWINDOW_CLOSED = 0xbf;
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
        private FlowWindowController m_WindowController;

        public MultiInvitationWindow()
        {
            this.m_WindowController = new FlowWindowController();
            base..ctor();
            return;
        }

        protected override void Awake()
        {
            base.Awake();
            this.m_WindowController.Initialize(this);
            this.m_WindowController.Add(this.m_SendWindowParam);
            this.m_WindowController.Add(this.m_ReceiveWindowParam);
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

