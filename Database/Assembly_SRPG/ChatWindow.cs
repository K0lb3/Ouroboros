namespace SRPG
{
    using GR;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.UI;

    [Pin(0x2d, "前回の発言から10秒以内にメッセージ送信時", 0, 0x2d), Pin(10, "Chat Enter", 0, 10), Pin(11, "Chat Leave", 0, 11), Pin(20, "チャットウィンドウを開く", 0, 20), Pin(0x17, "チャットウィンドウを閉じる", 0, 0x17), Pin(0x15, "ウィンドウオープン終了", 1, 0x15), Pin(0x16, "ウィンドウクローズ終了", 1, 0x16), Pin(30, "ユーザーのアイコンをタップ", 1, 30), Pin(40, "ワールドチャットログ取得Request", 1, 40), Pin(0x2a, "ワールドチャットログリセット", 0, 0x2a), Pin(0x2b, "ワールドチャットログ取得", 0, 0x2b), Pin(0x2e, "ルームチャットログ取得Request", 1, 0x2e), Pin(50, "チャットログリスト更新", 0, 50), Pin(0x33, "ウィンドウスライド", 0, 0x33), Pin(0x34, "ウィンドウスライドリセット", 0, 0x34), Pin(70, "チャットログ更新失敗", 0, 70), Pin(100, "スタンプ送信セット", 0, 100), Pin(0x65, "スタンプ送信", 1, 0x65)]
    public class ChatWindow : MonoSingleton<ChatWindow>, IFlowInterface
    {
        private const int PINID_IN_CHAT_ENTER = 10;
        private const int PINID_IN_CHAT_LEAVE = 11;
        private const int PINID_IN_CAHTWINDOW_OPEN = 20;
        private const int PINID_OU_OPEN_OUTPUT = 0x15;
        private const int PINID_OU_CLOSE_OUTPUT = 0x16;
        private const int PINID_IN_CAHTWINDOW_CLOSE = 0x17;
        private const int PINID_OU_UNITICON_TAP = 30;
        private const int PINID_OU_REQUEST_CHATLOG_WORLD = 40;
        private const int PINID_IN_UPDATE_CHATLOG_RESET = 0x2a;
        private const int PINID_IN_UPDATE_CHATLOG = 0x2b;
        private const int PINID_IN_SEND_CHAT_INTERVAL = 0x2d;
        private const int PINID_OU_REQUEST_CHATLOG_ROOM = 0x2e;
        private const int PINID_IN_REFRESH_CHATLOGLIST = 50;
        private const int PINID_IN_SLIDE_WINDOW = 0x33;
        private const int PINID_IN_SLIDERESET_WINDOW = 0x34;
        private const int PINID_IN_UPDATE_CHATLOG_FAILURE = 70;
        private const int PINID_IN_SEND_STAMP = 100;
        private const int PINID_OU_REQUEST_SEND_STAMP = 0x65;
        private static readonly float SPAN_UPDATE_WORLD_MESSAGE_UIOPEN;
        private static readonly float SPAN_UPDATE_WORLD_MESSAGE_UICLOSE;
        private static readonly float SPAN_UPDATE_ROOM_MESSAGE_UIOPEN;
        private static readonly float SPAN_UPDATE_ROOM_MESSAGE_UICLOSE;
        private static readonly float SPAN_UPDATE_ROOM_MEMBER;
        private float elapsed_time_for_photon_room_member;
        private StateMachine<ChatWindow> mState;
        private bool mOpened;
        private bool Maintenance;
        private string MaintenanceMsg;
        [SerializeField]
        private GameObject MessageRoot;
        [SerializeField]
        private GameObject MessageTemplate;
        [SerializeField]
        private Scrollbar ChatScrollBar;
        [SerializeField]
        private InputFieldCensorship InputFieldMessage;
        [SerializeField]
        private SRPG_Button SendMessageButton;
        [SerializeField]
        private GameObject ChannelPanel;
        private Dictionary<eChatType, SRPG_ToggleButton> mTabButtons;
        [SerializeField]
        private SRPG_ToggleButton Tab_World;
        [SerializeField]
        private SRPG_ToggleButton Tab_Room;
        [SerializeField]
        private GameObject MaintenancePanel;
        [SerializeField]
        private GameObject MaintenanceText;
        [SerializeField]
        private GameObject NoUsedChatText;
        [SerializeField]
        private GameObject ClosedShowMessage;
        [SerializeField]
        private GameObject UpdateMessageBadge;
        [SerializeField]
        private GameObject WordlChatBadge;
        [SerializeField]
        private GameObject RoomChatBadge;
        [SerializeField]
        private ScrollRect ScrollView;
        [SerializeField]
        private Text Caution;
        [SerializeField]
        private Animator CautionAnimator;
        [SerializeField]
        private Text MaintenanceMsgText;
        [SerializeField]
        private Button UsefulButton;
        [SerializeField]
        private GameObject UsefulRootObject;
        [SerializeField]
        private Text InputPlaceholderText;
        [SerializeField]
        private Text ClosedShowMessageText;
        public static readonly byte MAX_CHAT_LOG_ITEM;
        public int CharacterLimit;
        private GameManager gm;
        private float mRestTime_Opend_UpdateWorldChat;
        private float mRestTime_Opend_UpdateRoomChat;
        private float mRestTime_Closed_UpdateWorldChat;
        private float mRestTime_Closed_UpdateRoomChat;
        private int mLastChannelID;
        private static long system_message_local_id;
        private Text mMaintenance;
        private Text mNoUsedChat;
        private bool is_need_reset_world;
        private bool is_need_reset_room;
        private ChatLog mWorldChatLog;
        private ChatLog mRoomChatLog;
        private ChatLog mOfficalChatLog;
        private eChatType mCurrentChatType;
        private string mInputPlaceholderDefaultText_World;
        private string mInputPlaceholderDefaultText_Room;
        private static ChatLog CacheRoomChatLog;
        private static ChatUtility.RoomInfo room_info;
        public static ChatUtility.RoomMemberManager room_member_manager;
        private List<ChatLogItem> mItems;
        private bool mRequesting;
        private bool mInitialized;
        [SerializeField]
        private GameObject RootWindow;
        [SerializeField]
        private float SlidePositionX;
        private bool mChatPermit;
        private List<ChatUtility.ChatInspectionMaster> mChatInspectionMaster;
        private FlowNode_SendChatMessage mFNode_Sendmessage;

        static ChatWindow()
        {
            SPAN_UPDATE_WORLD_MESSAGE_UIOPEN = 10f;
            SPAN_UPDATE_WORLD_MESSAGE_UICLOSE = 20f;
            SPAN_UPDATE_ROOM_MESSAGE_UIOPEN = 7f;
            SPAN_UPDATE_ROOM_MESSAGE_UICLOSE = 20f;
            SPAN_UPDATE_ROOM_MEMBER = 1f;
            MAX_CHAT_LOG_ITEM = 30;
            return;
        }

        public ChatWindow()
        {
            this.MaintenanceMsg = string.Empty;
            this.CharacterLimit = 140;
            this.mLastChannelID = -1;
            this.mItems = new List<ChatLogItem>();
            this.mChatInspectionMaster = new List<ChatUtility.ChatInspectionMaster>();
            base..ctor();
            return;
        }

        [CompilerGenerated]
        private void <Awake>m__2C1(string)
        {
            this.OnSetSendMessage();
            return;
        }

        public unsafe void Activated(int pinID)
        {
            string str;
            int num;
            Vector3 vector;
            Vector3 vector2;
            num = pinID;
            switch ((num - 0x2a))
            {
                case 0:
                    goto Label_011B;

                case 1:
                    goto Label_00EE;

                case 2:
                    goto Label_0037;

                case 3:
                    goto Label_0110;

                case 4:
                    goto Label_0037;

                case 5:
                    goto Label_0037;

                case 6:
                    goto Label_0037;

                case 7:
                    goto Label_0037;

                case 8:
                    goto Label_012C;

                case 9:
                    goto Label_017A;

                case 10:
                    goto Label_01C8;
            }
        Label_0037:
            switch ((num - 20))
            {
                case 0:
                    goto Label_00D5;

                case 1:
                    goto Label_0050;

                case 2:
                    goto Label_0050;

                case 3:
                    goto Label_00D5;
            }
        Label_0050:
            if (num == 10)
            {
                goto Label_0075;
            }
            if (num == 11)
            {
                goto Label_0097;
            }
            if (num == 70)
            {
                goto Label_0145;
            }
            if (num == 100)
            {
                goto Label_0168;
            }
            goto Label_0215;
        Label_0075:
            this.UpdateRootWindowState(1);
            this.mChatPermit = 1;
            FlowNode_Variable.Set("IS_EXTERNAL_API_PERMIT", string.Empty);
            goto Label_021A;
        Label_0097:
            if (string.IsNullOrEmpty(FlowNode_Variable.Get("CHAT_SCENE_STATE")) == null)
            {
                goto Label_00C1;
            }
            this.ExitRoomSelf();
            this.UpdateRootWindowState(0);
            this.mChatPermit = 0;
        Label_00C1:
            FlowNode_Variable.Set("CHAT_SCENE_STATE", string.Empty);
            goto Label_021A;
        Label_00D5:
            this.UpdateWindowState(pinID);
            this.UpdateMessageBadgeState();
            this.RefreshChatLogView(0);
            goto Label_021A;
        Label_00EE:
            this.InputFieldMessage.set_text(string.Empty);
            this.RequestChatLog(this.mCurrentChatType, 0);
            goto Label_021A;
        Label_0110:
            this.RefreshCaution();
            goto Label_021A;
        Label_011B:
            this.ResetChatLog(this.mCurrentChatType);
            goto Label_021A;
        Label_012C:
            this.UpdateMessageBadgeState();
            this.RefreshChatLogView(0);
            this.Maintenance = 0;
            goto Label_021A;
        Label_0145:
            this.RefreshChatLogView(0);
            this.Maintenance = 1;
            this.MaintenanceMsg = Network.ErrMsg;
            Network.ResetError();
            goto Label_021A;
        Label_0168:
            this.SetSendStamp();
            this.SetActiveUsefulWindowObject(0);
            goto Label_021A;
        Label_017A:
            if ((this.RootWindow != null) == null)
            {
                goto Label_021A;
            }
            this.RootWindow.get_transform().set_position(new Vector2(this.SlidePositionX, &this.RootWindow.get_transform().get_position().y));
            goto Label_021A;
        Label_01C8:
            if ((this.RootWindow != null) == null)
            {
                goto Label_021A;
            }
            this.RootWindow.get_transform().set_position(new Vector2(0f, &this.RootWindow.get_transform().get_position().y));
            goto Label_021A;
        Label_0215:;
        Label_021A:
            return;
        }

        private unsafe void Awake()
        {
            int num;
            GameObject obj2;
            eChatType type;
            Dictionary<eChatType, SRPG_ToggleButton>.KeyCollection.Enumerator enumerator;
            eChatType type2;
            Dictionary<eChatType, SRPG_ToggleButton> dictionary;
            if (room_info != null)
            {
                goto Label_0014;
            }
            room_info = new ChatUtility.RoomInfo();
        Label_0014:
            if (room_member_manager != null)
            {
                goto Label_0028;
            }
            room_member_manager = new ChatUtility.RoomMemberManager();
        Label_0028:
            if (CacheRoomChatLog == null)
            {
                goto Label_0043;
            }
            this.CopyChatLog(CacheRoomChatLog, &this.mRoomChatLog);
        Label_0043:
            if ((this.MessageTemplate != null) == null)
            {
                goto Label_0060;
            }
            this.MessageTemplate.SetActive(0);
        Label_0060:
            if ((this.InputFieldMessage != null) == null)
            {
                goto Label_008D;
            }
            this.InputFieldMessage.get_onEndEdit().AddListener(new UnityAction<string>(this, this.<Awake>m__2C1));
        Label_008D:
            if ((this.MaintenancePanel != null) == null)
            {
                goto Label_00AA;
            }
            this.MaintenancePanel.SetActive(0);
        Label_00AA:
            if ((this.UpdateMessageBadge != null) == null)
            {
                goto Label_00C7;
            }
            this.UpdateMessageBadge.SetActive(0);
        Label_00C7:
            this.mItems = new List<ChatLogItem>(MAX_CHAT_LOG_ITEM);
            num = 0;
            goto Label_0116;
        Label_00DE:
            obj2 = Object.Instantiate<GameObject>(this.MessageTemplate);
            obj2.get_transform().SetParent(this.MessageRoot.get_transform(), 0);
            this.mItems.Add(obj2.GetComponent<ChatLogItem>());
            num += 1;
        Label_0116:
            if (num < MAX_CHAT_LOG_ITEM)
            {
                goto Label_00DE;
            }
            dictionary = new Dictionary<eChatType, SRPG_ToggleButton>();
            dictionary.Add(1, this.Tab_World);
            dictionary.Add(2, this.Tab_Room);
            this.mTabButtons = dictionary;
            enumerator = this.mTabButtons.Keys.GetEnumerator();
        Label_015D:
            try
            {
                goto Label_01A3;
            Label_0162:
                type = &enumerator.Current;
                if ((this.mTabButtons[type] == null) == null)
                {
                    goto Label_0186;
                }
                goto Label_01A3;
            Label_0186:
                this.mTabButtons[type].AddListener(new SRPG_Button.ButtonClickEvent(this.OnTabChange));
            Label_01A3:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0162;
                }
                goto Label_01C0;
            }
            finally
            {
            Label_01B4:
                ((Dictionary<eChatType, SRPG_ToggleButton>.KeyCollection.Enumerator) enumerator).Dispose();
            }
        Label_01C0:
            type2 = (room_info.IsActive == null) ? 1 : 2;
            this.ChangeChatTypeTab(type2);
            if ((this.UsefulButton != null) == null)
            {
                goto Label_020D;
            }
            this.UsefulButton.get_onClick().AddListener(new UnityAction(this, this.OnUsefulButton));
        Label_020D:
            room_info.Init(this);
            return;
        }

        public unsafe void ChangeChatTypeTab(eChatType _chat_type)
        {
            bool flag;
            eChatType type;
            Dictionary<eChatType, SRPG_ToggleButton>.KeyCollection.Enumerator enumerator;
            ChatLog log;
            long num;
            flag = this.mCurrentChatType == _chat_type;
            this.mCurrentChatType = _chat_type;
            this.SetMessageDataToFlowNode(this.InputFieldMessage.get_text(), 0);
            if (flag == null)
            {
                goto Label_002A;
            }
            return;
        Label_002A:
            enumerator = this.mTabButtons.Keys.GetEnumerator();
        Label_003B:
            try
            {
                goto Label_005A;
            Label_0040:
                type = &enumerator.Current;
                this.mTabButtons[type].IsOn = 0;
            Label_005A:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0040;
                }
                goto Label_0077;
            }
            finally
            {
            Label_006B:
                ((Dictionary<eChatType, SRPG_ToggleButton>.KeyCollection.Enumerator) enumerator).Dispose();
            }
        Label_0077:
            this.mTabButtons[_chat_type].IsOn = 1;
            this.SetActiveUsefulWindowObject(0);
            if (this.is_need_reset_world == null)
            {
                goto Label_00BD;
            }
            this.is_need_reset_world = 0;
            this.ClearAllItems();
            this.GetChatLogInstance(1).Reset();
            this.RequestChatLog(1, 0);
            return;
        Label_00BD:
            if (this.is_need_reset_room == null)
            {
                goto Label_00F5;
            }
            this.is_need_reset_room = 0;
            this.ClearAllItems();
            log = this.GetChatLogInstance(2);
            num = log.TopMessageIdServer;
            log.Reset();
            this.RequestRoomChatLog(num, 1);
            return;
        Label_00F5:
            this.UpdateMessageBadgeState();
            this.RefreshChatLogView(1);
            return;
        }

        private void CheckChannelUpdate()
        {
            if (this.mLastChannelID != -1)
            {
                goto Label_001C;
            }
            this.mLastChannelID = GlobalVars.CurrentChatChannel;
        Label_001C:
            if (this.mLastChannelID == GlobalVars.CurrentChatChannel)
            {
                goto Label_0054;
            }
            if ((this.FlowNodeSendChatMessage != null) == null)
            {
                goto Label_0054;
            }
            this.SetMessageDataToFlowNode(this.InputFieldMessage.get_text(), 0);
        Label_0054:
            this.mLastChannelID = GlobalVars.CurrentChatChannel;
            return;
        }

        private void CheckRoomMember()
        {
            ChatLog log;
            List<ChatLogParam> list;
            ChatLogParam param;
            string str;
            int num;
            ChatLogParam param2;
            string str2;
            string str3;
            int num2;
            ChatLogParam param3;
            string str4;
            string str5;
            if (room_info.IsActive != null)
            {
                goto Label_001A;
            }
            room_member_manager.Clear();
            return;
        Label_001A:
            this.elapsed_time_for_photon_room_member -= Time.get_deltaTime();
            if (this.elapsed_time_for_photon_room_member <= 0f)
            {
                goto Label_003D;
            }
            return;
        Label_003D:
            this.elapsed_time_for_photon_room_member = SPAN_UPDATE_ROOM_MEMBER;
            if (PunMonoSingleton<MyPhoton>.Instance.IsConnectedInRoom() == null)
            {
                goto Label_006B;
            }
            room_member_manager.Refresh(PunMonoSingleton<MyPhoton>.Instance.GetRoomPlayerList());
        Label_006B:
            if ((room_member_manager.EntryMembers.Count > 0) || (room_member_manager.LeaveMembers.Count > 0))
            {
                goto Label_0096;
            }
            return;
        Label_0096:
            log = this.GetChatLogInstance(2);
            list = new List<ChatLogParam>();
            param = null;
            str = (room_info.QuestParam.IsMultiTower == null) ? room_info.QuestParam.name : (room_info.QuestParam.title + room_info.QuestParam.name);
            num = 0;
            goto Label_01B2;
        Label_00FA:
            param2 = new ChatLogParam();
            param2.id = this.GenerateSystemMessageId();
            param2.message_type = 3;
            param2.posted_at = TimeManager.FromDateTime(TimeManager.ServerTime);
            if ((MonoSingleton<GameManager>.Instance.DeviceId == room_member_manager.EntryMembers[num].UID) == null)
            {
                goto Label_0174;
            }
            str2 = LocalizedText.Get("sys.CHAT_SYSTEM_MESSAGE_ENTRY_ROOM_SELF");
            param2.message = string.Format(str2, str);
            param = param2;
            goto Label_01A4;
        Label_0174:
            str3 = LocalizedText.Get("sys.CHAT_SYSTEM_MESSAGE_ENTRY_ROOM_OTHER");
            param2.message = string.Format(str3, room_member_manager.EntryMembers[num].Name);
        Label_01A4:
            list.Add(param2);
            num += 1;
        Label_01B2:
            if (num < room_member_manager.EntryMembers.Count)
            {
                goto Label_00FA;
            }
            num2 = 0;
            goto Label_0285;
        Label_01D0:
            param3 = new ChatLogParam();
            param3.id = this.GenerateSystemMessageId();
            param3.message_type = 3;
            param3.posted_at = TimeManager.FromDateTime(TimeManager.ServerTime);
            if ((MonoSingleton<GameManager>.Instance.DeviceId == room_member_manager.LeaveMembers[num2].UID) == null)
            {
                goto Label_0247;
            }
            str4 = LocalizedText.Get("sys.CHAT_SYSTEM_MESSAGE_LEAVE_ROOM_SELF");
            param3.message = string.Format(str4, str);
            goto Label_0277;
        Label_0247:
            str5 = LocalizedText.Get("sys.CHAT_SYSTEM_MESSAGE_LEAVE_ROOM_OTHER");
            param3.message = string.Format(str5, room_member_manager.LeaveMembers[num2].Name);
        Label_0277:
            list.Add(param3);
            num2 += 1;
        Label_0285:
            if (num2 < room_member_manager.LeaveMembers.Count)
            {
                goto Label_01D0;
            }
            if (param == null)
            {
                goto Label_02AE;
            }
            list.Clear();
            list.Add(param);
        Label_02AE:
            log.AddMessage(list);
            this.UpdateMessageBadgeState();
            this.RefreshChatLogView(0);
            room_member_manager.EntryMembers.Clear();
            room_member_manager.LeaveMembers.Clear();
            return;
        }

        private void ClearAllItems()
        {
            int num;
            num = 0;
            goto Label_001C;
        Label_0007:
            this.mItems[num].Clear();
            num += 1;
        Label_001C:
            if (num < this.mItems.Count)
            {
                goto Label_0007;
            }
            return;
        }

        private void CopyChatLog(ChatLog _base, ref ChatLog _target)
        {
            ChatLog log;
            int num;
            log = new ChatLog();
            num = 0;
            goto Label_0023;
        Label_000D:
            log.AddMessage(_base.messages[num]);
            num += 1;
        Label_0023:
            if (num < _base.messages.Count)
            {
                goto Label_000D;
            }
            *(_target) = log;
            return;
        }

        public void ExitRoomSelf()
        {
            ChatLog log;
            ChatLogParam param;
            string str;
            string str2;
            if (room_info.IsActive != null)
            {
                goto Label_0010;
            }
            return;
        Label_0010:
            room_info.ExitRoom();
            room_member_manager.Clear();
            if (room_info.QuestParam == null)
            {
                goto Label_00CF;
            }
            log = this.GetChatLogInstance(2);
            param = new ChatLogParam();
            param.id = this.GenerateSystemMessageId();
            param.message_type = 3;
            param.posted_at = TimeManager.FromDateTime(TimeManager.ServerTime);
            str = LocalizedText.Get("sys.CHAT_SYSTEM_MESSAGE_LEAVE_ROOM_SELF");
            str2 = (room_info.QuestParam.IsMultiTower == null) ? room_info.QuestParam.name : (room_info.QuestParam.title + room_info.QuestParam.name);
            param.message = string.Format(str, str2);
            log.AddMessage(param);
        Label_00CF:
            this.SaveRoomChatLogCache();
            return;
        }

        private long GenerateSystemMessageId()
        {
            system_message_local_id -= 1L;
            return system_message_local_id;
        }

        private ChatLog GetChatLogInstance(eChatType _chat_type)
        {
            eChatType type;
            type = _chat_type;
            if (type == 1)
            {
                goto Label_0015;
            }
            if (type == 2)
            {
                goto Label_0032;
            }
            goto Label_004F;
        Label_0015:
            if (this.mWorldChatLog != null)
            {
                goto Label_002B;
            }
            this.mWorldChatLog = new ChatLog();
        Label_002B:
            return this.mWorldChatLog;
        Label_0032:
            if (this.mRoomChatLog != null)
            {
                goto Label_0048;
            }
            this.mRoomChatLog = new ChatLog();
        Label_0048:
            return this.mRoomChatLog;
        Label_004F:
            return null;
        }

        private string GetOneLineMessageText(ChatLogParam _param)
        {
            object[] objArray2;
            object[] objArray1;
            string str;
            str = string.Empty;
            if (_param == null)
            {
                goto Label_009D;
            }
            if (_param.message_type != 1)
            {
                goto Label_0050;
            }
            objArray1 = new object[] { _param.name, ChatUtility.ReplaceNGWord(_param.message, this.mChatInspectionMaster, "*") };
            str = LocalizedText.Get("sys.TEXT_CLOSED_SHOW_MESSAGE", objArray1);
            goto Label_009D;
        Label_0050:
            if (_param.message_type != 2)
            {
                goto Label_007B;
            }
            objArray2 = new object[] { _param.name };
            str = LocalizedText.Get("sys.TEXT_CLOSED_SHOW_MESSAGE_STAMP", objArray2);
            goto Label_009D;
        Label_007B:
            if (_param.message_type != 3)
            {
                goto Label_009D;
            }
            str = _param.message.Replace("\n", string.Empty);
        Label_009D:
            return str;
        }

        public void LoadTemplateMessage()
        {
            ChatTemplateMessage message;
            message = base.GetComponentInChildren<ChatTemplateMessage>(1);
            if ((message == null) == null)
            {
                goto Label_0015;
            }
            return;
        Label_0015:
            message.LoadTemplateMessage();
            return;
        }

        private void OnSetSendMessage()
        {
            this.SetMessageDataToFlowNode(this.InputFieldMessage.get_text(), 0);
            return;
        }

        private void OnTabChange(SRPG_Button button)
        {
            if (this.TabChange(button) != null)
            {
                goto Label_000D;
            }
            return;
        Label_000D:
            return;
        }

        private void OnTapUnitIcon(SRPG_Button button)
        {
            ChatLogItem item;
            if ((button == null) == null)
            {
                goto Label_000D;
            }
            return;
        Label_000D:
            item = button.GetComponentInParent<ChatLogItem>();
            if ((item == null) == null)
            {
                goto Label_0021;
            }
            return;
        Label_0021:
            if (item.ChatLogParam != null)
            {
                goto Label_002D;
            }
            return;
        Label_002D:
            if (string.IsNullOrEmpty(item.ChatLogParam.uid) == null)
            {
                goto Label_0043;
            }
            return;
        Label_0043:
            FlowNode_Variable.Set("SelectUserID", item.ChatLogParam.uid);
            FlowNode_Variable.Set("IsBlackList", "0");
            FlowNode_GameObject.ActivateOutputLinks(this, 30);
            return;
        }

        private void OnUsefulButton()
        {
            bool flag;
            if ((this.UsefulRootObject == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            flag = this.UsefulRootObject.get_activeSelf();
            this.SetActiveUsefulWindowObject(flag == 0);
            return;
        }

        private void RefreshCaution()
        {
            string str;
            str = FlowNode_Variable.Get("MESSAGE_CAUTION_SEND_MESSAGE");
            if (string.IsNullOrEmpty(str) == null)
            {
                goto Label_0038;
            }
            if ((this.CautionAnimator != null) == null)
            {
                goto Label_0037;
            }
            this.CautionAnimator.ResetTrigger("onShowCaution");
        Label_0037:
            return;
        Label_0038:
            this.Caution.set_text(str);
            if ((this.CautionAnimator != null) == null)
            {
                goto Label_0065;
            }
            this.CautionAnimator.SetTrigger("onShowCaution");
        Label_0065:
            return;
        }

        private void RefreshChatLogView(bool _force_refresh)
        {
            ChatLog log;
            log = this.GetChatLogInstance(this.mCurrentChatType);
            if (log.is_dirty != null)
            {
                goto Label_001E;
            }
            if (_force_refresh == null)
            {
                goto Label_0038;
            }
        Label_001E:
            log.is_dirty = 0;
            base.StartCoroutine(this.RefreshChatMessage(log));
            goto Label_003F;
        Label_0038:
            this.mRequesting = 0;
        Label_003F:
            return;
        }

        [DebuggerHidden]
        private IEnumerator RefreshChatMessage(ChatLog _chat_log)
        {
            float num;
            <RefreshChatMessage>c__IteratorF2 rf;
            rf = new <RefreshChatMessage>c__IteratorF2();
            rf._chat_log = _chat_log;
            rf.<$>_chat_log = _chat_log;
            rf.<>f__this = this;
            return rf;
        }

        private void RefreshMainBadge(bool is_show_world_badge, bool is_show_room_badge)
        {
            if ((this.UpdateMessageBadge == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            if (this.mOpened == null)
            {
                goto Label_002A;
            }
            this.UpdateMessageBadge.SetActive(0);
            return;
        Label_002A:
            if (is_show_world_badge != null)
            {
                goto Label_0036;
            }
            if (is_show_room_badge == null)
            {
                goto Label_0043;
            }
        Label_0036:
            this.UpdateMessageBadge.SetActive(1);
            return;
        Label_0043:
            return;
        }

        private bool RefreshRoomChatBadge()
        {
            ChatLog log;
            bool flag;
            if ((this.RoomChatBadge == null) == null)
            {
                goto Label_0013;
            }
            return 0;
        Label_0013:
            log = this.GetChatLogInstance(2);
            flag = (log.is_dirty == null) ? 0 : (log.LastMessagePostedAt > TimeManager.FromDateTime(MonoSingleton<GameManager>.Instance.Player.LoginDate));
            if (this.mCurrentChatType != 2)
            {
                goto Label_0063;
            }
            this.RoomChatBadge.SetActive(0);
            goto Label_006F;
        Label_0063:
            this.RoomChatBadge.SetActive(flag);
        Label_006F:
            return flag;
        }

        private bool RefreshWorldChatBadge()
        {
            ChatLog log;
            bool flag;
            if ((this.WordlChatBadge == null) == null)
            {
                goto Label_0013;
            }
            return 0;
        Label_0013:
            log = this.GetChatLogInstance(1);
            flag = (log.is_dirty == null) ? 0 : (log.LastMessagePostedAt > TimeManager.FromDateTime(MonoSingleton<GameManager>.Instance.Player.LoginDate));
            if (this.mCurrentChatType != 1)
            {
                goto Label_0063;
            }
            this.WordlChatBadge.SetActive(0);
            goto Label_006F;
        Label_0063:
            this.WordlChatBadge.SetActive(flag);
        Label_006F:
            return flag;
        }

        private void RequestChatLog(eChatType select, bool force_request)
        {
            ChatLog log;
            eChatType type;
            type = select;
            if (type == 1)
            {
                goto Label_0015;
            }
            if (type == 2)
            {
                goto Label_0020;
            }
            goto Label_003A;
        Label_0015:
            this.RequestWorldChatLog();
            goto Label_003A;
        Label_0020:
            log = this.GetChatLogInstance(2);
            this.RequestRoomChatLog(log.LastMessageIdServer, force_request);
        Label_003A:
            return;
        }

        private void RequestRoomChatLog(long exclude_id, bool force_request)
        {
            FlowNode_ReqChatMessageRoom room;
            string str;
            bool flag;
            if ((room_info.IsActive != null) || (exclude_id > 0L))
            {
                goto Label_0018;
            }
            return;
        Label_0018:
            if ((force_request == null) && (room_info.IsActive == null))
            {
                goto Label_007D;
            }
            room = base.get_gameObject().GetComponent<FlowNode_ReqChatMessageRoom>();
            if ((room != null) == null)
            {
                goto Label_007D;
            }
            str = (room_info.IsActive == null) ? string.Empty : GlobalVars.SelectedMultiPlayRoomName;
            flag = force_request;
            room.SetChatMessageInfo(str, 0L, MAX_CHAT_LOG_ITEM, exclude_id, flag);
            this.mRequesting = 1;
        Label_007D:
            if (this.mRequesting == null)
            {
                goto Label_0090;
            }
            FlowNode_GameObject.ActivateOutputLinks(this, 0x2e);
        Label_0090:
            return;
        }

        private void RequestWorldChatLog()
        {
            ChatLog log;
            FlowNode_ReqChatMessageWorld world;
            log = this.GetChatLogInstance(1);
            world = base.get_gameObject().GetComponent<FlowNode_ReqChatMessageWorld>();
            if ((world != null) == null)
            {
                goto Label_0044;
            }
            world.SetChatMessageInfo(GlobalVars.CurrentChatChannel, 0L, MAX_CHAT_LOG_ITEM, log.LastMessageIdServer);
            this.mRequesting = 1;
        Label_0044:
            if (this.mRequesting == null)
            {
                goto Label_0057;
            }
            FlowNode_GameObject.ActivateOutputLinks(this, 40);
        Label_0057:
            return;
        }

        private void ResetChatLog(eChatType _chat_type)
        {
            ChatLog log;
            long num;
            ChatLog log2;
            if (_chat_type != 2)
            {
                goto Label_0048;
            }
            this.ClearAllItems();
            this.CheckChannelUpdate();
            this.UpdateChannelPanel();
            this.SaveRoomChatLogCache();
            log = this.GetChatLogInstance(2);
            num = log.TopMessageIdServer;
            log.Reset();
            this.RequestRoomChatLog(num, 1);
            this.is_need_reset_world = 1;
            goto Label_0077;
        Label_0048:
            this.ClearAllItems();
            this.CheckChannelUpdate();
            this.UpdateChannelPanel();
            this.GetChatLogInstance(1).Reset();
            this.RequestChatLog(1, 0);
            this.is_need_reset_room = 1;
        Label_0077:
            return;
        }

        private void ResetChatOff()
        {
            this.ResetCloseShowMessage();
            this.UpdateMessageBadge.SetActive(0);
            return;
        }

        private void ResetCloseShowMessage()
        {
            if ((this.ClosedShowMessageText != null) == null)
            {
                goto Label_0021;
            }
            this.ClosedShowMessageText.set_text(string.Empty);
        Label_0021:
            return;
        }

        private unsafe void SaveRoomChatLogCache()
        {
            ChatLog log;
            log = this.GetChatLogInstance(2);
            this.CopyChatLog(log, &CacheRoomChatLog);
            return;
        }

        public void SetActiveUsefulWindowObject(bool _active)
        {
            if ((this.UsefulRootObject == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            this.UsefulRootObject.SetActive(_active);
            return;
        }

        public void SetChatLog(ChatLog _chat_log, eChatType _chat_type)
        {
            ChatLog log;
            this.GetChatLogInstance(_chat_type).AddMessage(_chat_log.messages);
            this.SaveRoomChatLogCache();
            return;
        }

        public void SetChatLogAndSystemMessageMerge(ChatLog _server_chat_log, long _exclude_id)
        {
            ChatLog log;
            List<ChatLogParam> list;
            ChatLogParam param;
            int num;
            int num2;
            <SetChatLogAndSystemMessageMerge>c__AnonStorey319 storey;
            <SetChatLogAndSystemMessageMerge>c__AnonStorey31A storeya;
            <SetChatLogAndSystemMessageMerge>c__AnonStorey31B storeyb;
            storey = new <SetChatLogAndSystemMessageMerge>c__AnonStorey319();
            storey._exclude_id = _exclude_id;
            storey._server_chat_log = _server_chat_log;
            log = new ChatLog();
            list = new List<ChatLogParam>();
            param = CacheRoomChatLog.messages.Find(new Predicate<ChatLogParam>(storey.<>m__2BE));
            storeya = new <SetChatLogAndSystemMessageMerge>c__AnonStorey31A();
            storeya.i = 0;
            goto Label_010B;
        Label_0054:
            if (CacheRoomChatLog.messages[storeya.i].message_type == 3)
            {
                goto Label_00BE;
            }
            if (CacheRoomChatLog.messages[storeya.i].id == param.id)
            {
                goto Label_00BE;
            }
            if (storey._server_chat_log.messages.Find(new Predicate<ChatLogParam>(storeya.<>m__2BF)) == null)
            {
                goto Label_00DF;
            }
        Label_00BE:
            log.AddMessage(CacheRoomChatLog.messages[storeya.i]);
            goto Label_00FB;
        Label_00DF:
            list.Add(CacheRoomChatLog.messages[storeya.i]);
        Label_00FB:
            storeya.i += 1;
        Label_010B:
            if (storeya.i < CacheRoomChatLog.messages.Count)
            {
                goto Label_0054;
            }
            storeyb = new <SetChatLogAndSystemMessageMerge>c__AnonStorey31B();
            storeyb.<>f__ref$793 = storey;
            storeyb.i = 0;
            goto Label_0193;
        Label_0143:
            if (log.messages.Find(new Predicate<ChatLogParam>(storeyb.<>m__2C0)) == null)
            {
                goto Label_0165;
            }
            goto Label_0183;
        Label_0165:
            log.AddMessage(storey._server_chat_log.messages[storeyb.i]);
        Label_0183:
            storeyb.i += 1;
        Label_0193:
            if (storeyb.i < storey._server_chat_log.messages.Count)
            {
                goto Label_0143;
            }
            num = log.messages.IndexOf(param);
            if (num < 0)
            {
                goto Label_020D;
            }
            num2 = 0;
            goto Label_0200;
        Label_01CC:
            if ((list[num2].fuid != param.fuid) == null)
            {
                goto Label_01EE;
            }
            goto Label_01FA;
        Label_01EE:
            log.RemoveByIndex(num);
            goto Label_020D;
        Label_01FA:
            num2 += 1;
        Label_0200:
            if (num2 < list.Count)
            {
                goto Label_01CC;
            }
        Label_020D:
            this.mRoomChatLog = log;
            this.SaveRoomChatLogCache();
            return;
        }

        public void SetMessageDataToFlowNode(string input_text, bool is_force_send)
        {
            string str;
            eChatType type;
            if ((this.FlowNodeSendChatMessage == null) == null)
            {
                goto Label_001C;
            }
            DebugUtility.LogError("CHatWindow.cs -> OnSetSendMessage():FlowNode_SendChatMessage is Null References!");
            return;
        Label_001C:
            if (string.IsNullOrEmpty(input_text) == null)
            {
                goto Label_0033;
            }
            this.FlowNodeSendChatMessage.ResetParam();
            return;
        Label_0033:
            str = input_text;
            if (str.Length <= this.CharacterLimit)
            {
                goto Label_0054;
            }
            str = str.Substring(0, this.CharacterLimit);
        Label_0054:
            type = this.mCurrentChatType;
            if (type == 1)
            {
                goto Label_006E;
            }
            if (type == 2)
            {
                goto Label_0089;
            }
            goto Label_009F;
        Label_006E:
            this.FlowNodeSendChatMessage.SetMessageData(GlobalVars.CurrentChatChannel, str);
            goto Label_009F;
        Label_0089:
            this.FlowNodeSendChatMessage.SetMessageData(GlobalVars.SelectedMultiPlayRoomName, str);
        Label_009F:
            if (is_force_send == null)
            {
                goto Label_00B0;
            }
            this.FlowNodeSendChatMessage.ReqestSendMessage();
        Label_00B0:
            return;
        }

        private unsafe void setPosX(RectTransform rt, float px)
        {
            Vector2 vector;
            if (rt != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            vector = rt.get_anchoredPosition();
            &vector.x = px;
            rt.set_anchoredPosition(vector);
            return;
        }

        private void SetSendStamp()
        {
            string str;
            int num;
            eChatType type;
            if ((this.FlowNodeSendChatMessage == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            num = int.Parse(FlowNode_Variable.Get("SELECT_STAMP_ID"));
            type = this.mCurrentChatType;
            if (type == 1)
            {
                goto Label_003E;
            }
            if (type == 2)
            {
                goto Label_0059;
            }
            goto Label_006F;
        Label_003E:
            this.FlowNodeSendChatMessage.SetStampData(GlobalVars.CurrentChatChannel, num);
            goto Label_006F;
        Label_0059:
            this.FlowNodeSendChatMessage.SetStampData(GlobalVars.SelectedMultiPlayRoomName, num);
        Label_006F:
            FlowNode_GameObject.ActivateOutputLinks(this, 0x65);
            return;
        }

        private unsafe void Start()
        {
            bool flag;
            this.gm = MonoSingleton<GameManager>.Instance;
            if (this.mChatInspectionMaster == null)
            {
                goto Label_0027;
            }
            if (this.mChatInspectionMaster.Count > 0)
            {
                goto Label_0047;
            }
        Label_0027:
            flag = 0;
            this.mChatInspectionMaster = ChatUtility.LoadInspectionMaster(&flag);
            if (flag != null)
            {
                goto Label_0047;
            }
            DebugUtility.LogError("ChatWindow Error:Failed Load InspectionMaster!");
            return;
        Label_0047:
            if (ChatUtility.SetupChatChannelMaster() != null)
            {
                goto Label_005C;
            }
            DebugUtility.LogError("ChatWindow Error:Failed Load ChatChannelMaster!");
            return;
        Label_005C:
            if ((this.InputPlaceholderText != null) == null)
            {
                goto Label_008E;
            }
            this.mInputPlaceholderDefaultText_World = this.InputPlaceholderText.get_text();
            this.mInputPlaceholderDefaultText_Room = LocalizedText.Get("sys.CHAT_DISABLE_INPUT_FIELD_ROOM");
        Label_008E:
            if ((this.InputPlaceholderText == null) == null)
            {
                goto Label_00A9;
            }
            DebugUtility.LogError("InputPlaceholderText is NULL");
        Label_00A9:
            this.mState = new StateMachine<ChatWindow>(this);
            this.mState.GotoState<State_Init>();
            return;
        }

        private unsafe bool TabChange(SRPG_Button button)
        {
            eChatType type;
            Dictionary<eChatType, SRPG_ToggleButton>.KeyCollection.Enumerator enumerator;
            if (button.IsInteractable() != null)
            {
                goto Label_000D;
            }
            return 0;
        Label_000D:
            enumerator = this.mTabButtons.Keys.GetEnumerator();
        Label_001E:
            try
            {
                goto Label_0058;
            Label_0023:
                type = &enumerator.Current;
                if ((this.mTabButtons[type].get_name() == button.get_name()) == null)
                {
                    goto Label_0058;
                }
                this.ChangeChatTypeTab(type);
                goto Label_0064;
            Label_0058:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0023;
                }
            Label_0064:
                goto Label_0075;
            }
            finally
            {
            Label_0069:
                ((Dictionary<eChatType, SRPG_ToggleButton>.KeyCollection.Enumerator) enumerator).Dispose();
            }
        Label_0075:
            return 1;
        }

        private void Update()
        {
            if (this.UpdateMaintenancePanel() == null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            if (ChatUtility.IsMultiQuestNow() != null)
            {
                goto Label_0022;
            }
            if (this.IsPermit != null)
            {
                goto Label_0022;
            }
            return;
        Label_0022:
            if (this.gm.IsExternalPermit() != null)
            {
                goto Label_0033;
            }
            return;
        Label_0033:
            if (this.mState != null)
            {
                goto Label_003F;
            }
            return;
        Label_003F:
            this.UpdateSendMessageButtonInteractable();
            this.UpdateInputPlaceholderText();
            room_info.Run();
            this.CheckRoomMember();
            this.mState.Update();
            return;
        }

        private unsafe void UpdateChannelPanel()
        {
            int num;
            int num2;
            int num3;
            int num4;
            string str;
            Transform transform;
            int num5;
            int num6;
            float num7;
            if ((this.ChannelPanel == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            num = this.ChannelPanel.get_transform().get_childCount() - 1;
            num2 = GlobalVars.CurrentChatChannel.ToString().Length;
            num3 = GlobalVars.CurrentChatChannel;
            num4 = num;
            goto Label_00E4;
        Label_0047:
            str = "value_" + &Mathf.Pow(10f, (float) (num4 - 1)).ToString();
            transform = this.ChannelPanel.get_transform().FindChild(str);
            if ((transform == null) == null)
            {
                goto Label_0090;
            }
            goto Label_00E0;
        Label_0090:
            if (num2 >= num4)
            {
                goto Label_00A9;
            }
            transform.get_gameObject().SetActive(0);
            goto Label_00E0;
        Label_00A9:
            num5 = (int) Mathf.Pow(10f, (float) (num4 - 1));
            num6 = num3 / num5;
            transform.get_gameObject().SetActive(1);
            transform.GetComponent<ImageArray>().ImageIndex = num6;
            num3 = num3 % num5;
        Label_00E0:
            num4 -= 1;
        Label_00E4:
            if (num4 > 0)
            {
                goto Label_0047;
            }
            return;
        }

        private void UpdateClosingMessage(bool _is_world_dirty, bool _is_room_dirty)
        {
            ChatLog log;
            ChatLog log2;
            long num;
            long num2;
            ChatLogParam param;
            ChatLogParam param2;
            string str;
            if (this.mOpened == null)
            {
                goto Label_0028;
            }
            this.ClosedShowMessageText.set_text(string.Empty);
            this.ClosedShowMessage.SetActive(0);
            return;
        Label_0028:
            this.ClosedShowMessage.SetActive(1);
            log = this.GetChatLogInstance(1);
            log2 = this.GetChatLogInstance(2);
            num = 0L;
            num2 = 0L;
            param = ((log != null) && (log.messages.Count != null)) ? log.messages[log.messages.Count - 1] : null;
        Label_0096:
            param2 = ((log2 != null) && (log2.messages.Count != null)) ? log2.messages[log2.messages.Count - 1] : null;
            str = this.ClosedShowMessageText.get_text();
            if (_is_world_dirty != null)
            {
                goto Label_00D0;
            }
            if (_is_room_dirty != null)
            {
                goto Label_00D0;
            }
            return;
        Label_00D0:
            if (_is_world_dirty == null)
            {
                goto Label_00E5;
            }
            if (param == null)
            {
                goto Label_00E5;
            }
            num = param.posted_at;
        Label_00E5:
            if (_is_room_dirty == null)
            {
                goto Label_00FA;
            }
            if (param2 == null)
            {
                goto Label_00FA;
            }
            num2 = param2.posted_at;
        Label_00FA:
            if (num != null)
            {
                goto Label_0106;
            }
            if (num2 == null)
            {
                goto Label_012D;
            }
        Label_0106:
            if (num <= num2)
            {
                goto Label_011C;
            }
            str = this.GetOneLineMessageText(param);
            goto Label_012D;
        Label_011C:
            if (num2 < num)
            {
                goto Label_012D;
            }
            str = this.GetOneLineMessageText(param2);
        Label_012D:
            this.ClosedShowMessageText.set_text(str);
            return;
        }

        private void UpdateInputPlaceholderText()
        {
            if ((this.InputFieldMessage == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            if ((this.InputPlaceholderText == null) == null)
            {
                goto Label_0024;
            }
            return;
        Label_0024:
            if (this.mCurrentChatType != 2)
            {
                goto Label_0051;
            }
            if (room_info.IsActive != null)
            {
                goto Label_0051;
            }
            this.InputPlaceholderText.set_text(this.mInputPlaceholderDefaultText_Room);
            return;
        Label_0051:
            this.InputPlaceholderText.set_text(this.mInputPlaceholderDefaultText_World);
            return;
        }

        private bool UpdateMaintenancePanel()
        {
            if (GameUtility.Config_ChatState.Value != null)
            {
                goto Label_003B;
            }
            this.MaintenancePanel.SetActive(1);
            this.NoUsedChatText.SetActive(1);
            this.MaintenanceText.SetActive(0);
            this.ResetChatOff();
            return 1;
        Label_003B:
            if (this.Maintenance == null)
            {
                goto Label_0093;
            }
            this.MaintenancePanel.SetActive(1);
            this.MaintenanceText.SetActive(1);
            this.NoUsedChatText.SetActive(0);
            this.ResetChatOff();
            if (string.IsNullOrEmpty(this.MaintenanceMsg) != null)
            {
                goto Label_0091;
            }
            this.MaintenanceMsgText.set_text(this.MaintenanceMsg);
        Label_0091:
            return 1;
        Label_0093:
            this.MaintenancePanel.SetActive(0);
            this.NoUsedChatText.SetActive(0);
            this.MaintenanceText.SetActive(0);
            return 0;
        }

        private void UpdateMessageBadgeState()
        {
            bool flag;
            bool flag2;
            flag = this.RefreshWorldChatBadge();
            flag2 = this.RefreshRoomChatBadge();
            this.RefreshMainBadge(flag, flag2);
            this.UpdateClosingMessage(flag, flag2);
            return;
        }

        private void UpdateRootWindowState(bool state)
        {
            if ((this.RootWindow != null) == null)
            {
                goto Label_001D;
            }
            this.RootWindow.SetActive(state);
        Label_001D:
            if ((this.MessageRoot != null) == null)
            {
                goto Label_003A;
            }
            this.MessageRoot.SetActive(state);
        Label_003A:
            return;
        }

        private void UpdateSendMessageButtonInteractable()
        {
            bool flag;
            if ((this.InputFieldMessage == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            if ((this.SendMessageButton == null) == null)
            {
                goto Label_0024;
            }
            return;
        Label_0024:
            if ((this.UsefulButton == null) == null)
            {
                goto Label_0036;
            }
            return;
        Label_0036:
            if (this.mCurrentChatType != 2)
            {
                goto Label_007A;
            }
            if (room_info.IsActive != null)
            {
                goto Label_007A;
            }
            flag = 0;
            this.InputFieldMessage.set_interactable(flag);
            flag = flag;
            this.UsefulButton.set_interactable(flag);
            this.SendMessageButton.set_interactable(flag);
            return;
        Label_007A:
            flag = 1;
            this.InputFieldMessage.set_interactable(flag);
            flag = flag;
            this.UsefulButton.set_interactable(flag);
            this.SendMessageButton.set_interactable(flag);
            return;
        }

        private void UpdateWindowState(int inputPinID)
        {
            int num;
            num = 0x15;
            if (inputPinID != 0x17)
            {
                goto Label_0026;
            }
            this.UsefulRootObject.SetActive(0);
            num = 0x16;
            this.mOpened = 0;
            goto Label_002D;
        Label_0026:
            this.mOpened = 1;
        Label_002D:
            this.ClosedShowMessageText.set_text(string.Empty);
            this.ClosedShowMessage.SetActive(this.mOpened == 0);
            FlowNode_GameObject.ActivateOutputLinks(this, num);
            return;
        }

        public bool IsOpened
        {
            get
            {
                return this.mOpened;
            }
        }

        public bool IsRequesting
        {
            get
            {
                return this.mRequesting;
            }
        }

        public bool IsInitialized
        {
            get
            {
                return this.mInitialized;
            }
            set
            {
                this.mInitialized = value;
                return;
            }
        }

        public bool IsPermit
        {
            get
            {
                return this.mChatPermit;
            }
        }

        private FlowNode_SendChatMessage FlowNodeSendChatMessage
        {
            get
            {
                if ((this.mFNode_Sendmessage == null) == null)
                {
                    goto Label_0022;
                }
                this.mFNode_Sendmessage = base.get_gameObject().GetComponent<FlowNode_SendChatMessage>();
            Label_0022:
                return this.mFNode_Sendmessage;
            }
        }

        [CompilerGenerated]
        private sealed class <RefreshChatMessage>c__IteratorF2 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal int <i>__0;
            internal ChatLog _chat_log;
            internal RectTransform <rt>__1;
            internal GameObject <go_scroll_marker>__2;
            internal ListExtras <listex>__3;
            internal int $PC;
            internal object $current;
            internal ChatLog <$>_chat_log;
            internal ChatWindow <>f__this;

            public <RefreshChatMessage>c__IteratorF2()
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
                bool flag;
                num = this.$PC;
                this.$PC = -1;
                switch (num)
                {
                    case 0:
                        goto Label_0029;

                    case 1:
                        goto Label_020C;

                    case 2:
                        goto Label_026B;

                    case 3:
                        goto Label_02C1;
                }
                goto Label_02C8;
            Label_0029:
                this.<>f__this.ClearAllItems();
                this.<i>__0 = 0;
                goto Label_00FC;
            Label_0040:
                if (this._chat_log.messages[this.<i>__0].message_type == 3)
                {
                    goto Label_00AC;
                }
                this._chat_log.messages[this.<i>__0].message = ChatUtility.ReplaceNGWord(this._chat_log.messages[this.<i>__0].message, this.<>f__this.mChatInspectionMaster, "*");
            Label_00AC:
                this.<>f__this.mItems[this.<i>__0].SetParam(this._chat_log.messages[this.<i>__0], new SRPG_Button.ButtonClickEvent(this.<>f__this.OnTapUnitIcon));
                this.<i>__0 += 1;
            Label_00FC:
                if (this.<i>__0 < this._chat_log.messages.Count)
                {
                    goto Label_0040;
                }
                if ((this.<>f__this.ScrollView != null) == null)
                {
                    goto Label_0297;
                }
                this.<rt>__1 = null;
                if (this.<>f__this.MessageRoot == null)
                {
                    goto Label_015F;
                }
                this.<rt>__1 = this.<>f__this.MessageRoot.GetComponent<RectTransform>();
            Label_015F:
                if (this.<rt>__1 == null)
                {
                    goto Label_0185;
                }
                this.<>f__this.setPosX(this.<rt>__1, -2048f);
            Label_0185:
                this.<go_scroll_marker>__2 = null;
                if (this.<>f__this.ChatScrollBar == null)
                {
                    goto Label_01DD;
                }
                if (this.<>f__this.ChatScrollBar.get_transform().get_childCount() <= 0)
                {
                    goto Label_01DD;
                }
                this.<go_scroll_marker>__2 = this.<>f__this.ChatScrollBar.get_transform().GetChild(0).get_gameObject();
            Label_01DD:
                if (this.<go_scroll_marker>__2 == null)
                {
                    goto Label_01F9;
                }
                this.<go_scroll_marker>__2.SetActive(0);
            Label_01F9:
                this.$current = null;
                this.$PC = 1;
                goto Label_02CA;
            Label_020C:
                this.<listex>__3 = this.<>f__this.ScrollView.GetComponent<ListExtras>();
                this.<listex>__3.SetScrollPos(1f);
                if (this.<rt>__1 == null)
                {
                    goto Label_0258;
                }
                this.<>f__this.setPosX(this.<rt>__1, -2048f);
            Label_0258:
                this.$current = null;
                this.$PC = 2;
                goto Label_02CA;
            Label_026B:
                this.<listex>__3.SetScrollPos(0f);
                if (this.<go_scroll_marker>__2 == null)
                {
                    goto Label_0297;
                }
                this.<go_scroll_marker>__2.SetActive(1);
            Label_0297:
                this.<>f__this.SaveRoomChatLogCache();
                this.<>f__this.mRequesting = 0;
                this.$current = null;
                this.$PC = 3;
                goto Label_02CA;
            Label_02C1:
                this.$PC = -1;
            Label_02C8:
                return 0;
            Label_02CA:
                return 1;
                return flag;
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

        [CompilerGenerated]
        private sealed class <SetChatLogAndSystemMessageMerge>c__AnonStorey319
        {
            internal long _exclude_id;
            internal ChatLog _server_chat_log;

            public <SetChatLogAndSystemMessageMerge>c__AnonStorey319()
            {
                base..ctor();
                return;
            }

            internal bool <>m__2BE(ChatLogParam log)
            {
                return (log.id == this._exclude_id);
            }
        }

        [CompilerGenerated]
        private sealed class <SetChatLogAndSystemMessageMerge>c__AnonStorey31A
        {
            internal int i;

            public <SetChatLogAndSystemMessageMerge>c__AnonStorey31A()
            {
                base..ctor();
                return;
            }

            internal bool <>m__2BF(ChatLogParam log)
            {
                return (log.id == ChatWindow.CacheRoomChatLog.messages[this.i].id);
            }
        }

        [CompilerGenerated]
        private sealed class <SetChatLogAndSystemMessageMerge>c__AnonStorey31B
        {
            internal int i;
            internal ChatWindow.<SetChatLogAndSystemMessageMerge>c__AnonStorey319 <>f__ref$793;

            public <SetChatLogAndSystemMessageMerge>c__AnonStorey31B()
            {
                base..ctor();
                return;
            }

            internal bool <>m__2C0(ChatLogParam log)
            {
                return (log.id == this.<>f__ref$793._server_chat_log.messages[this.i].id);
            }
        }

        public enum eChatType : byte
        {
            None = 0,
            World = 1,
            Room = 2
        }

        public enum MessageTemplateType : byte
        {
            None = 0,
            OtherUser = 1,
            User = 2,
            System = 3
        }

        private class State_Init : State<ChatWindow>
        {
            public State_Init()
            {
                base..ctor();
                return;
            }

            public override void Update(ChatWindow self)
            {
                if (GlobalVars.CurrentChatChannel <= 0)
                {
                    goto Label_0027;
                }
                self.CheckChannelUpdate();
                self.UpdateChannelPanel();
                self.mState.GotoState<ChatWindow.State_WaitClosed>();
            Label_0027:
                return;
            }
        }

        private class State_NoUsed : State<ChatWindow>
        {
            public State_NoUsed()
            {
                base..ctor();
                return;
            }

            public override void Begin(ChatWindow self)
            {
            }

            public override void Update(ChatWindow self)
            {
            }
        }

        private class State_WaitClosed : State<ChatWindow>
        {
            public State_WaitClosed()
            {
                base..ctor();
                return;
            }

            public override void Begin(ChatWindow self)
            {
                self.mRestTime_Closed_UpdateWorldChat = ChatWindow.SPAN_UPDATE_WORLD_MESSAGE_UICLOSE;
                self.mRestTime_Closed_UpdateRoomChat = ChatWindow.SPAN_UPDATE_ROOM_MESSAGE_UICLOSE;
                return;
            }

            public override void Update(ChatWindow self)
            {
                if (self.IsOpened == null)
                {
                    goto Label_0017;
                }
                self.mState.GotoState<ChatWindow.State_WaitOpened>();
                return;
            Label_0017:
                if (self.IsRequesting == null)
                {
                    goto Label_0023;
                }
                return;
            Label_0023:
                if (self.mCurrentChatType != 1)
                {
                    goto Label_0058;
                }
                if (self.mRestTime_Closed_UpdateWorldChat > 0f)
                {
                    goto Label_0058;
                }
                self.mRestTime_Closed_UpdateWorldChat = ChatWindow.SPAN_UPDATE_WORLD_MESSAGE_UICLOSE;
                self.RequestChatLog(self.mCurrentChatType, 0);
                return;
            Label_0058:
                if (self.mCurrentChatType != 2)
                {
                    goto Label_008D;
                }
                if (self.mRestTime_Closed_UpdateRoomChat > 0f)
                {
                    goto Label_008D;
                }
                self.mRestTime_Closed_UpdateRoomChat = ChatWindow.SPAN_UPDATE_ROOM_MESSAGE_UICLOSE;
                self.RequestChatLog(self.mCurrentChatType, 0);
                return;
            Label_008D:
                self.mRestTime_Closed_UpdateWorldChat = Mathf.Max(0f, self.mRestTime_Closed_UpdateWorldChat - Time.get_deltaTime());
                self.mRestTime_Closed_UpdateRoomChat = Mathf.Max(0f, self.mRestTime_Closed_UpdateRoomChat - Time.get_deltaTime());
                return;
            }
        }

        private class State_WaitOpened : State<ChatWindow>
        {
            public State_WaitOpened()
            {
                base..ctor();
                return;
            }

            public override void Begin(ChatWindow self)
            {
                if (self.IsInitialized != null)
                {
                    goto Label_0021;
                }
                self.mRestTime_Opend_UpdateWorldChat = 0f;
                self.mRestTime_Opend_UpdateRoomChat = 0f;
            Label_0021:
                self.IsInitialized = 1;
                return;
            }

            public override void Update(ChatWindow self)
            {
                if (self.IsOpened != null)
                {
                    goto Label_0017;
                }
                self.mState.GotoState<ChatWindow.State_WaitClosed>();
                return;
            Label_0017:
                if (self.IsRequesting == null)
                {
                    goto Label_0023;
                }
                return;
            Label_0023:
                if (self.mCurrentChatType != 1)
                {
                    goto Label_0058;
                }
                if (self.mRestTime_Opend_UpdateWorldChat > 0f)
                {
                    goto Label_0058;
                }
                self.mRestTime_Opend_UpdateWorldChat = ChatWindow.SPAN_UPDATE_WORLD_MESSAGE_UIOPEN;
                self.RequestChatLog(self.mCurrentChatType, 0);
                return;
            Label_0058:
                if (self.mCurrentChatType != 2)
                {
                    goto Label_008D;
                }
                if (self.mRestTime_Opend_UpdateRoomChat > 0f)
                {
                    goto Label_008D;
                }
                self.mRestTime_Opend_UpdateRoomChat = ChatWindow.SPAN_UPDATE_ROOM_MESSAGE_UIOPEN;
                self.RequestChatLog(self.mCurrentChatType, 0);
                return;
            Label_008D:
                self.mRestTime_Opend_UpdateWorldChat = Mathf.Max(0f, self.mRestTime_Opend_UpdateWorldChat - Time.get_deltaTime());
                self.mRestTime_Opend_UpdateRoomChat = Mathf.Max(0f, self.mRestTime_Opend_UpdateRoomChat - Time.get_deltaTime());
                return;
            }
        }
    }
}

