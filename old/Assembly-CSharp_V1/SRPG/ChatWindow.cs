// Decompiled with JetBrains decompiler
// Type: SRPG.ChatWindow
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(23, "チャットウィンドウを閉じる", FlowNode.PinTypes.Input, 23)]
  [FlowNode.Pin(43, "ワールドチャットログリセット", FlowNode.PinTypes.Input, 43)]
  [FlowNode.Pin(41, "運営ログ取得Request", FlowNode.PinTypes.Output, 41)]
  [FlowNode.Pin(45, "前回の発言から10秒以内にメッセージ送信時", FlowNode.PinTypes.Input, 45)]
  [FlowNode.Pin(50, "チャットログリスト更新", FlowNode.PinTypes.Input, 50)]
  [FlowNode.Pin(70, "チャットログ更新失敗", FlowNode.PinTypes.Input, 70)]
  [FlowNode.Pin(100, "スタンプ送信セット", FlowNode.PinTypes.Input, 100)]
  [FlowNode.Pin(10, "Chat Enter", FlowNode.PinTypes.Input, 10)]
  [FlowNode.Pin(42, "ワールドチャットログ取得", FlowNode.PinTypes.Input, 42)]
  [FlowNode.Pin(11, "Chat Leave", FlowNode.PinTypes.Input, 11)]
  [FlowNode.Pin(20, "チャットウィンドウを開く", FlowNode.PinTypes.Input, 20)]
  [FlowNode.Pin(21, "ウィンドウオープン終了", FlowNode.PinTypes.Output, 21)]
  [FlowNode.Pin(101, "スタンプ送信", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(44, "運営ログ取得", FlowNode.PinTypes.Input, 44)]
  [FlowNode.Pin(22, "ウィンドウクローズ終了", FlowNode.PinTypes.Output, 22)]
  [FlowNode.Pin(30, "ユーザーのアイコンをタップ", FlowNode.PinTypes.Output, 30)]
  [FlowNode.Pin(40, "ワールドチャットログ取得Request", FlowNode.PinTypes.Output, 40)]
  public class ChatWindow : MonoBehaviour, IFlowInterface
  {
    private static readonly float SPAN_UPDATE_MESSAGE_UIOPEN = 10f;
    private static readonly float SPAN_UPDATE_MESSAGE_UICLOSE = 20f;
    public static readonly string CHAT_CHANNEL_MASTER_PATH = "Data/Channel";
    private const int PINID_IN_CHAT_ENTER = 10;
    private const int PINID_IN_CHAT_LEAVE = 11;
    private const int PINID_IN_CAHTWINDOW_OPEN = 20;
    private const int PINID_OU_OPEN_OUTPUT = 21;
    private const int PINID_OU_CLOSE_OUTPUT = 22;
    private const int PINID_IN_CAHTWINDOW_CLOSE = 23;
    private const int PINID_OU_UNITICON_TAP = 30;
    private const int PINID_OU_REQUEST_CHATLOG = 40;
    private const int PINID_OU_REQUEST_CHATLOG_OFFICAL = 41;
    private const int PINID_IN_UPDATE_CHATLOG = 42;
    private const int PINID_IN_UPDATE_CHATLOG_RESET = 43;
    private const int PINID_IN_UPDATE_CHATLOG_OFFICAL = 44;
    private const int PINID_IN_SEND_CHAT_INTERVAL = 45;
    private const int PINID_IN_REFRESH_CHATLOGLIST = 50;
    private const int PINID_IN_UPDATE_CHATLOG_FAILURE = 70;
    private const int PINID_IN_SEND_STAMP = 100;
    private const int PINID_OU_REQUEST_SEND_STAMP = 101;
    private const string CHAT_INSPECTION_MASTER_PATH = "Data/ChatWord";
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
    private GameObject ChannelPanel;
    private SRPG_ToggleButton[] mTabButtons;
    [SerializeField]
    private SRPG_ToggleButton Tab_World;
    [SerializeField]
    private SRPG_ToggleButton Tab_Message;
    [SerializeField]
    private GameObject MaintenancePanel;
    [SerializeField]
    private GameObject MaintenanceText;
    [SerializeField]
    private GameObject NoUsedChatText;
    [SerializeField]
    private GameObject PushOfficalMessage;
    [SerializeField]
    private GameObject ClosedShowMessage;
    [SerializeField]
    private GameObject UpdateMessageBadge;
    [SerializeField]
    private ScrollRect ScrollView;
    [SerializeField]
    private UnityEngine.UI.Text Caution;
    [SerializeField]
    private Animator CautionAnimator;
    [SerializeField]
    private UnityEngine.UI.Text MaintenanceMsgText;
    [SerializeField]
    private Button StampFieldBtn;
    [SerializeField]
    private GameObject StampField;
    public byte MaxChatLogItem;
    public int CharacterLimit;
    private GameManager gm;
    private ChatWindow.SelectChatTab mCurrentChatTab;
    private int mLastMessageID;
    private UnityEngine.UI.Text mMaintenance;
    private UnityEngine.UI.Text mNoUsedChat;
    private ChatLog mChatLog;
    private ChatLog mChatLogOffical;
    private int mLastIDOffical;
    private List<GameObject> mItems;
    private bool mRequesting;
    private bool mInitialized;
    [SerializeField]
    private GameObject RootWindow;
    private bool mChatPermit;
    private List<ChatInspectionMaster> mChatInspectionMaster;

    public ChatWindow()
    {
      base.\u002Ector();
    }

    public bool IsOpened
    {
      get
      {
        return this.mOpened;
      }
    }

    public ChatLog ChatLog
    {
      get
      {
        return this.mChatLog;
      }
      set
      {
        int count = value.messages.Count;
        if (this.mChatLog != null && count < (int) this.MaxChatLogItem)
        {
          if (this.mChatLog.messages != null && this.mChatLog.messages.Count > 0 && this.mChatLog.messages.Count == (int) this.MaxChatLogItem)
          {
            for (int index = 0; index < count; ++index)
              this.mChatLog.messages.RemoveAt(0);
          }
          using (List<ChatLogParam>.Enumerator enumerator = value.messages.GetEnumerator())
          {
            while (enumerator.MoveNext())
              this.mChatLog.messages.Add(enumerator.Current);
          }
        }
        else
          this.mChatLog = value;
      }
    }

    public ChatLog ChatLogOffical
    {
      get
      {
        return this.mChatLogOffical;
      }
      set
      {
        this.mChatLogOffical = value;
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
      }
    }

    public bool IsPermit
    {
      get
      {
        return this.mChatPermit;
      }
    }

    private bool SetupInspectionMaster()
    {
      string src = AssetManager.LoadTextData("Data/ChatWord");
      if (string.IsNullOrEmpty(src))
      {
        DebugUtility.LogError("ChatWindow Error:[Data/ChatWord] is Not Found or Not Data!");
        return false;
      }
      try
      {
        JSON_ChatInspectionMaster[] jsonArray = JSONParser.parseJSONArray<JSON_ChatInspectionMaster>(src);
        if (jsonArray == null)
          throw new InvalidJSONException();
        this.mChatInspectionMaster.Clear();
        foreach (JSON_ChatInspectionMaster json in jsonArray)
        {
          ChatInspectionMaster inspectionMaster = new ChatInspectionMaster();
          if (inspectionMaster.Deserialize(json))
            this.mChatInspectionMaster.Add(inspectionMaster);
        }
      }
      catch (Exception ex)
      {
        DebugUtility.LogWarning("ChatWindow/SetupInspectionMaster parse error! e=" + ex.ToString());
        return false;
      }
      return true;
    }

    private bool SetupChatChannelMaster()
    {
      if (Object.op_Equality((Object) this.gm, (Object) null))
      {
        DebugUtility.LogError("ChatWindow Error:gm is NotInstance!");
        return false;
      }
      if (this.gm.GetChatChannelMaster() != null && this.gm.GetChatChannelMaster().Length > 0)
        return true;
      string src = AssetManager.LoadTextData(ChatWindow.CHAT_CHANNEL_MASTER_PATH);
      if (string.IsNullOrEmpty(src))
      {
        DebugUtility.LogError("ChatWindow Error:[" + ChatWindow.CHAT_CHANNEL_MASTER_PATH + "] is Not Found or Not Data!");
        return false;
      }
      bool flag = false;
      try
      {
        Json_ChatChannelMasterParam[] jsonArray = JSONParser.parseJSONArray<Json_ChatChannelMasterParam>(src);
        if (jsonArray != null)
        {
          if (jsonArray != null)
          {
            if (this.gm.Deserialize(GameUtility.Config_Language, new JSON_ChatChannelMaster() { channels = jsonArray }))
              flag = true;
          }
        }
      }
      catch (Exception ex)
      {
        DebugUtility.LogError(ex.ToString());
        flag = false;
      }
      return flag;
    }

    private void UpdateRootWindowState(bool state)
    {
      if (Object.op_Inequality((Object) this.RootWindow, (Object) null))
        this.RootWindow.SetActive(state);
      if (!Object.op_Inequality((Object) this.MessageRoot, (Object) null))
        return;
      this.MessageRoot.SetActive(state);
    }

    public void Activated(int pinID)
    {
      int num = pinID;
      switch (num)
      {
        case 42:
          this.InputFieldMessage.set_text(string.Empty);
          this.RequestChatLog((int) GlobalVars.CurrentChatChannel, ChatWindow.SelectChatTab.World);
          break;
        case 43:
          this.ClearChatLog();
          this.InputFieldMessage.set_text(string.Empty);
          this.mLastMessageID = 0;
          this.UpdateChannelPanel();
          this.mChatLog.messages.Clear();
          this.RequestChatLog((int) GlobalVars.CurrentChatChannel, ChatWindow.SelectChatTab.World);
          break;
        case 44:
          this.RequestChatLog(0, ChatWindow.SelectChatTab.Offical);
          break;
        case 45:
          this.RefreshCaution();
          break;
        case 50:
          this.UpdateMessageBadgeState();
          this.RefreshChatLog();
          this.Maintenance = false;
          break;
        default:
          switch (num - 20)
          {
            case 0:
              this.ResetCloseShowMessage();
              this.UpdateWindowState(pinID);
              this.UpdateMessageBadgeState();
              return;
            case 3:
              this.UpdateWindowState(pinID);
              this.UpdateMessageBadgeState();
              return;
            default:
              if (num != 10)
              {
                if (num != 11)
                {
                  if (num != 70)
                  {
                    if (num != 100)
                      return;
                    this.StampField.SetActive(false);
                    this.SetSendStamp();
                    return;
                  }
                  this.RefreshChatLog();
                  this.Maintenance = true;
                  this.MaintenanceMsg = Network.ErrMsg;
                  Network.ResetError();
                  return;
                }
                if (string.IsNullOrEmpty(FlowNode_Variable.Get("CHAT_SCENE_STATE")))
                {
                  this.UpdateRootWindowState(false);
                  this.mChatPermit = false;
                }
                FlowNode_Variable.Set("CHAT_SCENE_STATE", string.Empty);
                return;
              }
              this.UpdateRootWindowState(true);
              this.mChatPermit = true;
              FlowNode_Variable.Set("IS_EXTERNAL_API_PERMIT", string.Empty);
              return;
          }
      }
    }

    private void RefreshCaution()
    {
      string str = FlowNode_Variable.Get("MESSAGE_CAUTION_SEND_MESSAGE");
      if (string.IsNullOrEmpty(str))
      {
        if (!Object.op_Inequality((Object) this.CautionAnimator, (Object) null))
          return;
        this.CautionAnimator.ResetTrigger("onShowCaution");
      }
      else
      {
        this.Caution.set_text(str);
        if (!Object.op_Inequality((Object) this.CautionAnimator, (Object) null))
          return;
        this.CautionAnimator.SetTrigger("onShowCaution");
      }
    }

    private void UpdateWindowState(int inputPinID)
    {
      int pinID = 21;
      if (inputPinID == 23)
      {
        this.StampField.SetActive(false);
        pinID = 22;
        this.mOpened = false;
      }
      else
        this.mOpened = true;
      FlowNode_GameObject.ActivateOutputLinks((Component) this, pinID);
    }

    private void Awake()
    {
      if (Object.op_Inequality((Object) this.MessageTemplate, (Object) null))
        this.MessageTemplate.SetActive(false);
      if (Object.op_Inequality((Object) this.InputFieldMessage, (Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent<string>) this.InputFieldMessage.get_onEndEdit()).AddListener(new UnityAction<string>((object) this, __methodptr(\u003CAwake\u003Em__254)));
      }
      if (Object.op_Inequality((Object) this.MaintenancePanel, (Object) null))
        this.MaintenancePanel.SetActive(false);
      if (Object.op_Inequality((Object) this.PushOfficalMessage, (Object) null))
        this.PushOfficalMessage.SetActive(false);
      if (Object.op_Inequality((Object) this.UpdateMessageBadge, (Object) null))
        this.UpdateMessageBadge.SetActive(false);
      this.mTabButtons = new SRPG_ToggleButton[1]
      {
        this.Tab_World
      };
      for (int index = 0; index < this.mTabButtons.Length; ++index)
      {
        if (Object.op_Inequality((Object) this.mTabButtons[index], (Object) null))
          this.mTabButtons[index].AddListener(new SRPG_Button.ButtonClickEvent(this.OnTabChange));
      }
      if (this.mTabButtons.Length > 0 && Object.op_Inequality((Object) this.mTabButtons[0], (Object) null))
        this.mTabButtons[0].IsOn = true;
      if (Object.op_Inequality((Object) this.StampField, (Object) null))
        this.StampField.SetActive(false);
      if (!Object.op_Inequality((Object) this.StampFieldBtn, (Object) null))
        return;
      // ISSUE: method pointer
      ((UnityEvent) this.StampFieldBtn.get_onClick()).AddListener(new UnityAction((object) this, __methodptr(OnStampField)));
    }

    private void OnTabChange(SRPG_Button button)
    {
      if (!this.TabChange(button))
        ;
    }

    private bool TabChange(SRPG_Button button)
    {
      if (!((Selectable) button).IsInteractable())
        return false;
      int num = Array.IndexOf<SRPG_Button>((SRPG_Button[]) this.mTabButtons, button);
      for (int index = 0; index < this.mTabButtons.Length; ++index)
      {
        bool flag = index == num;
        if (Object.op_Inequality((Object) this.mTabButtons[index], (Object) null))
          this.mTabButtons[index].IsOn = flag;
      }
      return true;
    }

    private void OnStampField()
    {
      if (!Object.op_Inequality((Object) this.StampField, (Object) null))
        return;
      this.StampField.SetActive(!this.StampField.GetActive());
    }

    private void Start()
    {
      this.gm = MonoSingleton<GameManager>.Instance;
      if ((this.mChatInspectionMaster == null || this.mChatInspectionMaster.Count <= 0) && !this.SetupInspectionMaster())
        DebugUtility.LogError("ChatWindow Error:Failed Load InspectionMaster!");
      else if (!this.SetupChatChannelMaster())
      {
        DebugUtility.LogError("ChatWindow Error:Failed Load ChatChannelMaster!");
      }
      else
      {
        this.mCurrentChatTab = ChatWindow.SelectChatTab.World;
        this.mItems.Clear();
        this.mItems = new List<GameObject>((int) this.MaxChatLogItem);
        for (int index = 0; index < (int) this.MaxChatLogItem; ++index)
        {
          GameObject gameObject = (GameObject) Object.Instantiate<GameObject>((M0) this.MessageTemplate);
          gameObject.get_transform().SetParent(this.MessageRoot.get_transform(), false);
          this.mItems.Add(gameObject);
        }
        this.mState = new StateMachine<ChatWindow>(this);
        this.mState.GotoState<ChatWindow.State_Init>();
      }
    }

    private void OnSetSendMessage(InputFieldCensorship field)
    {
      if (string.IsNullOrEmpty(this.InputFieldMessage.get_text()))
        return;
      int currentChatChannel = (int) GlobalVars.CurrentChatChannel;
      string message = this.InputFieldMessage.get_text();
      if (message.Length > this.CharacterLimit)
        message = message.Substring(0, this.CharacterLimit);
      FlowNode_SendChatMessage component = (FlowNode_SendChatMessage) ((Component) this).get_gameObject().GetComponent<FlowNode_SendChatMessage>();
      if (!Object.op_Inequality((Object) component, (Object) null))
        return;
      component.SetMessageData(currentChatChannel, message);
    }

    private void SetSendStamp()
    {
      int currentChatChannel = (int) GlobalVars.CurrentChatChannel;
      int stamp_id = int.Parse(FlowNode_Variable.Get("SELECT_STAMP_ID"));
      FlowNode_SendChatMessage component = (FlowNode_SendChatMessage) ((Component) this).get_gameObject().GetComponent<FlowNode_SendChatMessage>();
      if (!Object.op_Inequality((Object) component, (Object) null))
        return;
      component.SetStampData(currentChatChannel, stamp_id);
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 101);
    }

    private void Update()
    {
      if (this.UpdateMaintenancePanel() || !this.IsPermit || (!this.gm.IsExternalPermit() || this.mState == null))
        return;
      this.mState.Update();
    }

    public int UpdateLogItem(ChatLogParam param, int num = 0)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      ChatWindow.\u003CUpdateLogItem\u003Ec__AnonStorey23D itemCAnonStorey23D = new ChatWindow.\u003CUpdateLogItem\u003Ec__AnonStorey23D();
      // ISSUE: reference to a compiler-generated field
      itemCAnonStorey23D.param = param;
      // ISSUE: reference to a compiler-generated field
      itemCAnonStorey23D.\u003C\u003Ef__this = this;
      GameObject gameObject = num >= this.mItems.Count ? (GameObject) null : this.mItems[num];
      // ISSUE: reference to a compiler-generated field
      if (itemCAnonStorey23D.param == null)
      {
        if (Object.op_Inequality((Object) gameObject, (Object) null))
          gameObject.SetActive(false);
        return -1;
      }
      gameObject.SetActive(true);
      ChatLogItem component1 = (ChatLogItem) gameObject.GetComponent<ChatLogItem>();
      if (Object.op_Equality((Object) component1, (Object) null))
        return -1;
      ChatWindow.MessageTemplateType type = ChatWindow.MessageTemplateType.OtherUser;
      // ISSUE: reference to a compiler-generated field
      if (string.IsNullOrEmpty(itemCAnonStorey23D.param.fuid))
      {
        type = ChatWindow.MessageTemplateType.Official;
      }
      else
      {
        // ISSUE: reference to a compiler-generated field
        if (this.gm.Player.FUID == itemCAnonStorey23D.param.fuid)
          type = ChatWindow.MessageTemplateType.User;
      }
      // ISSUE: reference to a compiler-generated field
      component1.Refresh(itemCAnonStorey23D.param, type);
      SRPG_Button component2 = (SRPG_Button) component1.GetIcon.GetComponent<SRPG_Button>();
      // ISSUE: reference to a compiler-generated field
      if (Object.op_Inequality((Object) component2, (Object) null) && itemCAnonStorey23D.param.fuid != this.gm.Player.FUID)
      {
        // ISSUE: method pointer
        ((UnityEvent) component2.get_onClick()).AddListener(new UnityAction((object) itemCAnonStorey23D, __methodptr(\u003C\u003Em__255)));
      }
      else
        ((UnityEventBase) component2.get_onClick()).RemoveAllListeners();
      // ISSUE: reference to a compiler-generated field
      return itemCAnonStorey23D.param.id;
    }

    private void UpdateChannelPanel()
    {
      if (Object.op_Equality((Object) this.ChannelPanel, (Object) null))
        return;
      int num1 = this.ChannelPanel.get_transform().get_childCount() - 1;
      int length = GlobalVars.CurrentChatChannel.ToString().Length;
      int currentChatChannel = (int) GlobalVars.CurrentChatChannel;
      for (int index = num1; index > 0; --index)
      {
        Transform child = this.ChannelPanel.get_transform().FindChild("value_" + Mathf.Pow(10f, (float) (index - 1)).ToString());
        if (!Object.op_Equality((Object) child, (Object) null))
        {
          if (length < index)
          {
            ((Component) child).get_gameObject().SetActive(false);
          }
          else
          {
            int num2 = (int) Mathf.Pow(10f, (float) (index - 1));
            int num3 = currentChatChannel / num2;
            ((Component) child).get_gameObject().SetActive(true);
            ((ImageArray) ((Component) child).GetComponent<ImageArray>()).ImageIndex = num3;
            currentChatChannel %= num2;
          }
        }
      }
    }

    private void RequestChatLog(int channel = 0, ChatWindow.SelectChatTab select = ChatWindow.SelectChatTab.World)
    {
      int channel1 = channel;
      int pinID = 40;
      if (select == ChatWindow.SelectChatTab.Offical)
        pinID = 41;
      switch (select - (byte) 1)
      {
        case ChatWindow.SelectChatTab.Message:
          this.mRequesting = true;
          break;
        default:
          int start_id = 0;
          int limit = 30;
          int exclude_id = this.mLastMessageID >= 0 ? this.mLastMessageID : 0;
          FlowNode_ReqChatMessage component = (FlowNode_ReqChatMessage) ((Component) this).get_gameObject().GetComponent<FlowNode_ReqChatMessage>();
          if (Object.op_Inequality((Object) component, (Object) null))
          {
            component.SetChatMessageinfo(channel1, start_id, limit, exclude_id);
            this.mRequesting = true;
            break;
          }
          break;
      }
      if (!this.mRequesting)
        return;
      FlowNode_GameObject.ActivateOutputLinks((Component) this, pinID);
    }

    private void RefreshChatLog()
    {
      if (this.mChatLog != null && this.mChatLog.messages.Count > 0)
      {
        if (this.mLastMessageID != this.mChatLog.messages[this.mChatLog.messages.Count - 1].id)
          this.StartCoroutine(this.RefreshChatMessage());
        else
          this.mRequesting = false;
      }
      else
        this.mRequesting = false;
    }

    [DebuggerHidden]
    private IEnumerator RefreshChatMessage()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new ChatWindow.\u003CRefreshChatMessage\u003Ec__IteratorA9() { \u003C\u003Ef__this = this };
    }

    private string FilterMessage(string message)
    {
      string[] strArray = this.isIllegalWord(message);
      if (strArray != null && strArray.Length > 0)
      {
        for (int index = 0; index < strArray.Length; ++index)
        {
          string oldValue = strArray[index];
          string newValue = new StringBuilder().Insert(0, "*", oldValue.Length).ToString();
          message = message.Replace(oldValue, newValue);
        }
      }
      if (this.mChatInspectionMaster != null && this.mChatInspectionMaster.Count > 0)
      {
        using (List<ChatInspectionMaster>.Enumerator enumerator = this.mChatInspectionMaster.GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            ChatInspectionMaster current = enumerator.Current;
            StringBuilder stringBuilder;
            if (!string.IsNullOrEmpty(current.word))
            {
              for (; message.ToLower().Contains(current.word.ToLower()); message = stringBuilder.ToString())
              {
                string str = new StringBuilder().Insert(0, "*", current.word.Length).ToString();
                int num = message.ToLower().IndexOf(current.word.ToLower());
                stringBuilder = new StringBuilder(message);
                stringBuilder.Remove(num, current.word.Length);
                stringBuilder.Insert(num, str);
              }
            }
          }
        }
      }
      return message;
    }

    private void setPosX(RectTransform rt, float px)
    {
      if (!Object.op_Implicit((Object) rt))
        return;
      Vector2 anchoredPosition = rt.get_anchoredPosition();
      anchoredPosition.x = (__Null) (double) px;
      rt.set_anchoredPosition(anchoredPosition);
    }

    private string[] isIllegalWord(string text)
    {
      List<string> stringList = new List<string>();
      MatchCollection matchCollection1 = new Regex("\\d{1,4}(-|ー|‐|－)\\d{1,4}(-|ー|‐|－)\\d{4}|\\d{10,11}").Matches(text);
      if (matchCollection1 != null && matchCollection1.Count > 0)
      {
        foreach (Match match in matchCollection1)
          stringList.Add(match.Value);
      }
      MatchCollection matchCollection2 = new Regex("(.)+@(.)+(\\.|どっと|ドット|dot)(.*)").Matches(text);
      if (matchCollection2 != null && matchCollection2.Count > 0)
      {
        foreach (Match match in matchCollection2)
          stringList.Add(match.Value);
      }
      return stringList.ToArray();
    }

    private void ClearChatLog()
    {
      for (int num = 0; num < this.mItems.Count; ++num)
        this.UpdateLogItem((ChatLogParam) null, num);
      this.ResetCloseShowMessage();
    }

    private void RefreshPushLog()
    {
      if (this.mCurrentChatTab == ChatWindow.SelectChatTab.Offical)
        return;
      ChatLogParam message = this.mChatLogOffical.messages[this.mChatLogOffical.messages.Count - 1];
      if (this.mLastIDOffical == message.id)
        return;
      Transform transform = this.PushOfficalMessage.get_transform().Find("item");
      ChatLogItem component = (ChatLogItem) ((Component) transform).GetComponent<ChatLogItem>();
      if (!Object.op_Inequality((Object) component, (Object) null))
        return;
      ((Component) transform).get_gameObject().SetActive(true);
      component.RefreshPushMessage(message, ChatWindow.MessageTemplateType.Official);
    }

    private void OnTapUnitIcon(string uid)
    {
      if (string.IsNullOrEmpty(uid))
        return;
      FlowNode_Variable.Set("SelectUserID", uid);
      FlowNode_Variable.Set("IsBlackList", "0");
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 30);
    }

    private bool UpdateMaintenancePanel()
    {
      if (!GameUtility.Config_ChatState.Value)
      {
        this.MaintenancePanel.SetActive(true);
        this.NoUsedChatText.SetActive(true);
        this.MaintenanceText.SetActive(false);
        return true;
      }
      if (this.Maintenance)
      {
        this.MaintenancePanel.SetActive(true);
        this.MaintenanceText.SetActive(true);
        this.NoUsedChatText.SetActive(false);
        if (!string.IsNullOrEmpty(this.MaintenanceMsg))
          this.MaintenanceMsgText.set_text(this.MaintenanceMsg);
        return true;
      }
      this.MaintenancePanel.SetActive(false);
      this.NoUsedChatText.SetActive(false);
      this.MaintenanceText.SetActive(false);
      return false;
    }

    private void RefreshCloseShowMessage()
    {
      if (!Object.op_Inequality((Object) this.ClosedShowMessage, (Object) null) || !this.ClosedShowMessage.get_activeInHierarchy())
        return;
      Transform child = this.ClosedShowMessage.get_transform().FindChild("text");
      UnityEngine.UI.Text component = (UnityEngine.UI.Text) ((Component) child).GetComponent<UnityEngine.UI.Text>();
      if (Object.op_Equality((Object) child, (Object) null))
        return;
      string str = string.Empty;
      if (this.mChatLogOffical != null && this.mChatLogOffical.messages.Count > 0)
      {
        ChatLogParam message = this.mChatLogOffical.messages[this.mChatLogOffical.messages.Count - 1];
        int id = message.id;
        str = "[" + message.name + "]:" + message.message;
      }
      else if (this.mChatLog != null && this.mChatLog.messages.Count > 0)
      {
        ChatLogParam message = this.mChatLog.messages[this.mChatLog.messages.Count - 1];
        int id = message.id;
        if (message.fuid != this.gm.Player.FUID)
        {
          string source = this.FilterMessage(message.message);
          if (source.All<char>((Func<char, bool>) (x =>
          {
            if ((int) x != 42)
              return (int) x == 32;
            return true;
          })))
            return;
          str = "[" + message.name + "]:" + source;
          if (message.message == string.Empty)
            str = "[" + message.name + "] " + LocalizedText.Get("sys.CHAT_SEND_STAMP");
        }
      }
      component.set_text(str);
    }

    private void UpdateMessageBadgeState()
    {
      if (!Object.op_Inequality((Object) this.UpdateMessageBadge, (Object) null))
        return;
      if (this.mOpened)
        this.UpdateMessageBadge.SetActive(false);
      else if (this.mChatLogOffical != null && this.mChatLogOffical.messages.Count > 0)
      {
        if (this.mChatLogOffical.messages[this.mChatLogOffical.messages.Count - 1].id == this.mLastIDOffical)
          return;
        this.UpdateMessageBadge.SetActive(true);
        this.RefreshCloseShowMessage();
      }
      else
      {
        if (this.mChatLog == null || this.mChatLog.messages.Count <= 0)
          return;
        ChatLogParam message = this.mChatLog.messages[this.mChatLog.messages.Count - 1];
        int id = message.id;
        if (this.mLastMessageID > -1)
        {
          if (id == this.mLastMessageID || !(message.fuid != this.gm.Player.FUID))
            return;
          this.UpdateMessageBadge.SetActive(true);
          this.RefreshCloseShowMessage();
        }
        else
          this.UpdateMessageBadge.SetActive((TimeManager.ServerTime - TimeManager.FromUnixTime(message.posted_at)).TotalSeconds < (double) ChatWindow.SPAN_UPDATE_MESSAGE_UICLOSE);
      }
    }

    private void ResetCloseShowMessage()
    {
      if (!Object.op_Inequality((Object) this.ClosedShowMessage, (Object) null))
        return;
      ((UnityEngine.UI.Text) ((Component) this.ClosedShowMessage.get_transform().Find("text")).GetComponent<UnityEngine.UI.Text>()).set_text(string.Empty);
    }

    private enum SelectChatTab : byte
    {
      None,
      World,
      Message,
      Offical,
    }

    public enum MessageTemplateType : byte
    {
      None,
      OtherUser,
      User,
      Official,
    }

    private class State_NoUsed : State<ChatWindow>
    {
      public override void Begin(ChatWindow self)
      {
      }

      public override void Update(ChatWindow self)
      {
      }
    }

    private class State_Init : State<ChatWindow>
    {
      public override void Update(ChatWindow self)
      {
        if ((int) GlobalVars.CurrentChatChannel <= 0)
          return;
        self.UpdateChannelPanel();
        self.mState.GotoState<ChatWindow.State_WaitClosed>();
      }
    }

    private class State_WaitOpened : State<ChatWindow>
    {
      private float mExpire;

      public override void Begin(ChatWindow self)
      {
        this.mExpire = !self.IsInitialized ? ChatWindow.SPAN_UPDATE_MESSAGE_UIOPEN : 0.0f;
        self.IsInitialized = true;
      }

      public override void Update(ChatWindow self)
      {
        if (!self.IsOpened)
        {
          self.mState.GotoState<ChatWindow.State_WaitClosed>();
        }
        else
        {
          if (self.IsRequesting)
            return;
          if ((double) this.mExpire >= (double) ChatWindow.SPAN_UPDATE_MESSAGE_UIOPEN)
          {
            this.mExpire = 0.0f;
            self.RequestChatLog((int) GlobalVars.CurrentChatChannel, ChatWindow.SelectChatTab.World);
          }
          else
            this.mExpire += Time.get_deltaTime();
        }
      }
    }

    private class State_WaitClosed : State<ChatWindow>
    {
      private float mExpire;

      public override void Begin(ChatWindow self)
      {
        this.mExpire = 0.0f;
      }

      public override void Update(ChatWindow self)
      {
        if (self.IsOpened)
        {
          self.mState.GotoState<ChatWindow.State_WaitOpened>();
        }
        else
        {
          if (self.IsRequesting)
            return;
          if ((double) this.mExpire >= (double) ChatWindow.SPAN_UPDATE_MESSAGE_UICLOSE)
          {
            this.mExpire = 0.0f;
            self.RequestChatLog((int) GlobalVars.CurrentChatChannel, ChatWindow.SelectChatTab.World);
          }
          else
            this.mExpire += Time.get_deltaTime();
        }
      }
    }
  }
}
