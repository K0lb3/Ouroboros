// Decompiled with JetBrains decompiler
// Type: SRPG.MultiInvitationReceiveWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class MultiInvitationReceiveWindow : FlowWindowBase
  {
    private MultiInvitationReceiveWindow.SerializeParam m_Param;
    private MultiInvitationReceiveWindow.Tab m_Tab;
    private bool m_Destroy;
    private MultiInvitationReceiveWindow.ActiveContent.ItemSource m_ActiveSource;
    private ContentController m_ActiveController;
    private MultiInvitationReceiveWindow.LogContent.ItemSource m_LogSource;
    private ContentController m_LogController;
    private SerializeValueBehaviour m_ValueList;
    private Toggle m_ActiveToggle;
    private Toggle m_LogToggle;
    private MultiInvitationReceiveWindow.ActiveData m_ActiveData;
    private MultiInvitationReceiveWindow.LogData m_LogData;
    private static MultiInvitationReceiveWindow m_Instance;

    public override string name
    {
      get
      {
        return nameof (MultiInvitationReceiveWindow);
      }
    }

    public static MultiInvitationReceiveWindow instance
    {
      get
      {
        return MultiInvitationReceiveWindow.m_Instance;
      }
    }

    public static void SetBadge(bool value)
    {
      if (MultiInvitationReceiveWindow.m_Instance == null && value)
      {
        if (MultiInvitationBadge.isValid)
          return;
        MultiInvitationBadge.isValid = value;
        NotifyList.PushMultiInvitation();
      }
      else
        MultiInvitationBadge.isValid = false;
    }

    public override void Initialize(FlowWindowBase.SerializeParamBase param)
    {
      MultiInvitationReceiveWindow.m_Instance = this;
      base.Initialize(param);
      this.m_Param = param as MultiInvitationReceiveWindow.SerializeParam;
      if (this.m_Param == null)
        throw new Exception(this.ToString() + " > Failed serializeParam null.");
      this.m_ValueList = (SerializeValueBehaviour) this.m_Param.window.GetComponent<SerializeValueBehaviour>();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_ValueList, (UnityEngine.Object) null))
      {
        this.m_ActiveToggle = this.m_ValueList.list.GetUIToggle("tgl_receive");
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_ActiveToggle, (UnityEngine.Object) null))
          ((Selectable) this.m_ActiveToggle).set_interactable(false);
        this.m_LogToggle = this.m_ValueList.list.GetUIToggle("tgl_send");
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_LogToggle, (UnityEngine.Object) null))
          ((Selectable) this.m_LogToggle).set_interactable(false);
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_Param.activeList, (UnityEngine.Object) null))
      {
        this.m_ActiveController = (ContentController) this.m_Param.activeList.GetComponentInChildren<ContentController>();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_ActiveController, (UnityEngine.Object) null))
          this.m_ActiveController.SetWork((object) this);
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_Param.logList, (UnityEngine.Object) null))
      {
        this.m_LogController = (ContentController) this.m_Param.logList.GetComponentInChildren<ContentController>();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_LogController, (UnityEngine.Object) null))
          this.m_LogController.SetWork((object) this);
      }
      this.Close(true);
    }

    public override void Release()
    {
      this.ReleaseActiveList();
      this.ReleaseLogList();
      base.Release();
      MultiInvitationReceiveWindow.m_Instance = (MultiInvitationReceiveWindow) null;
    }

    public override int Update()
    {
      base.Update();
      if (this.m_Destroy && this.isClosed)
      {
        this.SetActiveChild(false);
        return 290;
      }
      int active = (int) CriticalSection.GetActive();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_ActiveToggle, (UnityEngine.Object) null))
        ((Selectable) this.m_ActiveToggle).set_interactable(active == 0);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_LogToggle, (UnityEngine.Object) null))
        ((Selectable) this.m_LogToggle).set_interactable(active == 0);
      return -1;
    }

    private bool SetTab(MultiInvitationReceiveWindow.Tab tab)
    {
      bool flag = false;
      if (this.m_Tab != tab)
      {
        flag = true;
        switch (tab)
        {
          case MultiInvitationReceiveWindow.Tab.NONE:
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_Param.tabActive, (UnityEngine.Object) null))
              this.m_Param.tabActive.SetActive(false);
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_Param.tabLog, (UnityEngine.Object) null))
            {
              this.m_Param.tabLog.SetActive(false);
              break;
            }
            break;
          case MultiInvitationReceiveWindow.Tab.ACTIVE:
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_Param.tabActive, (UnityEngine.Object) null))
              this.m_Param.tabActive.SetActive(true);
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_Param.tabLog, (UnityEngine.Object) null))
            {
              this.m_Param.tabLog.SetActive(false);
              break;
            }
            break;
          case MultiInvitationReceiveWindow.Tab.LOG:
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_Param.tabActive, (UnityEngine.Object) null))
              this.m_Param.tabActive.SetActive(false);
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_Param.tabLog, (UnityEngine.Object) null))
            {
              this.m_Param.tabLog.SetActive(true);
              break;
            }
            break;
        }
        this.m_Tab = tab;
      }
      return flag;
    }

    public void InitializeActiveList()
    {
      this.ReleaseActiveList();
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_ActiveController, (UnityEngine.Object) null))
        return;
      this.m_ActiveSource = new MultiInvitationReceiveWindow.ActiveContent.ItemSource();
      if (this.m_ActiveData != null && this.m_ActiveData.rooms != null)
      {
        for (int index = 0; index < this.m_ActiveData.rooms.Length; ++index)
        {
          MultiInvitationReceiveWindow.ActiveData.RoomData room = this.m_ActiveData.rooms[index];
          if (room != null && room.isValid)
          {
            MultiInvitationReceiveWindow.ActiveContent.ItemSource.ItemParam itemParam = new MultiInvitationReceiveWindow.ActiveContent.ItemSource.ItemParam(room);
            if (itemParam != null && itemParam.IsValid())
              this.m_ActiveSource.Add(itemParam);
          }
        }
      }
      this.m_ActiveController.Initialize((ContentSource) this.m_ActiveSource, Vector2.get_zero());
    }

    public void ReleaseActiveList()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_ActiveController, (UnityEngine.Object) null))
        this.m_ActiveController.Release();
      this.m_ActiveSource = (MultiInvitationReceiveWindow.ActiveContent.ItemSource) null;
    }

    public void InitializeLogList()
    {
      this.ReleaseLogList();
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_LogController, (UnityEngine.Object) null))
        return;
      this.m_LogSource = new MultiInvitationReceiveWindow.LogContent.ItemSource();
      if (this.m_LogData != null && this.m_LogData.rooms != null)
      {
        for (int index = 0; index < this.m_LogData.rooms.Length; ++index)
        {
          MultiInvitationReceiveWindow.LogData.RoomData room = this.m_LogData.rooms[index];
          if (room != null && room.isValid && this.GetActiveRoomData(room.roomid) == null)
          {
            MultiInvitationReceiveWindow.LogContent.ItemSource.ItemParam itemParam = new MultiInvitationReceiveWindow.LogContent.ItemSource.ItemParam(room);
            if (itemParam != null && itemParam.IsValid())
              this.m_LogSource.Add(itemParam);
          }
        }
      }
      this.m_LogController.Initialize((ContentSource) this.m_LogSource, Vector2.get_zero());
    }

    public void ReleaseLogList()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_LogController, (UnityEngine.Object) null))
        this.m_LogController.Release();
      this.m_LogSource = (MultiInvitationReceiveWindow.LogContent.ItemSource) null;
    }

    private MultiInvitationReceiveWindow.ActiveData.RoomData GetActiveRoomData(int roomId)
    {
      if (this.m_ActiveData != null && this.m_ActiveData.rooms != null)
      {
        for (int index = 0; index < this.m_ActiveData.rooms.Length; ++index)
        {
          MultiInvitationReceiveWindow.ActiveData.RoomData room = this.m_ActiveData.rooms[index];
          if (room != null && room.roomid == roomId)
            return room;
        }
      }
      return (MultiInvitationReceiveWindow.ActiveData.RoomData) null;
    }

    public int GetLogPage()
    {
      if (this.m_LogData != null)
        return this.m_LogData.page;
      return -1;
    }

    public override int OnActivate(int pinId)
    {
      switch (pinId)
      {
        case 200:
          if (this.SetTab(MultiInvitationReceiveWindow.Tab.ACTIVE))
          {
            this.InitializeActiveList();
            this.InitializeLogList();
          }
          MultiInvitationBadge.isValid = false;
          this.Open();
          break;
        case 210:
          this.m_Destroy = true;
          this.Close(false);
          break;
        case 220:
          if (this.SetTab(MultiInvitationReceiveWindow.Tab.ACTIVE))
            this.InitializeActiveList();
          return 300;
        case 230:
          if (this.SetTab(MultiInvitationReceiveWindow.Tab.LOG))
            this.InitializeLogList();
          return 300;
        case 240:
          SerializeValueList currentValue = FlowNode_ButtonEvent.currentValue as SerializeValueList;
          if (currentValue != null)
          {
            MultiInvitationReceiveWindow.ActiveData.RoomData dataSource = currentValue.GetDataSource<MultiInvitationReceiveWindow.ActiveData.RoomData>("item");
            if (dataSource != null)
            {
              GlobalVars.SelectedMultiPlayRoomID = dataSource.roomid;
              GlobalVars.MultiInvitation = (int) dataSource.multiType;
              GlobalVars.MultiInvitationRoomOwner = dataSource.owner.fuid;
              GlobalVars.MultiInvitationRoomLocked = dataSource.locked;
              string eventName = "MENU_MULTI";
              if (dataSource.multiType == MultiInvitationReceiveWindow.MultiType.TOWER)
              {
                eventName = "MENU_MULTITOWER";
                GameManager instance = MonoSingleton<GameManager>.Instance;
                GlobalVars.SelectedMultiTowerID = dataSource.quest.param.iname;
              }
              GlobalEvent.Invoke(eventName, (object) null);
              this.m_Destroy = true;
              this.Close(false);
              break;
            }
            break;
          }
          break;
        case 250:
          this.InitializeActiveList();
          break;
        case 260:
          this.InitializeLogList();
          break;
      }
      return -1;
    }

    public bool DeserializeActiveList(FlowNode_ReqMultiInvitation.Api_RoomInvitation.Json json)
    {
      this.m_ActiveData = new MultiInvitationReceiveWindow.ActiveData();
      return this.m_ActiveData.Decerialize(json);
    }

    public bool DeserializeLogList(int page, FlowNode_ReqMultiInvitationHistory.Api_MultiInvitationHistory.Json json)
    {
      this.m_LogData = new MultiInvitationReceiveWindow.LogData();
      this.m_LogData.Decerialize(page, json);
      return true;
    }

    public enum Tab
    {
      NONE,
      ACTIVE,
      LOG,
    }

    public enum MultiType
    {
      NONE,
      NORMAL,
      TOWER,
    }

    public static class ActiveContent
    {
      public static MultiInvitationReceiveWindow.ActiveContent.ItemAccessor clickItem;

      public class ItemAccessor
      {
        private ContentNode m_Node;
        private MultiInvitationReceiveWindow.ActiveData.RoomData m_Param;
        private DataSource m_DataSource;
        private SerializeValueBehaviour m_Value;

        public ContentNode node
        {
          get
          {
            return this.m_Node;
          }
        }

        public MultiInvitationReceiveWindow.ActiveData.RoomData param
        {
          get
          {
            return this.m_Param;
          }
        }

        public bool isValid
        {
          get
          {
            if (this.m_Param != null)
              return this.m_Param.isValid;
            return false;
          }
        }

        public void Setup(MultiInvitationReceiveWindow.ActiveData.RoomData param)
        {
          this.m_Param = param;
        }

        public void Bind(ContentNode node)
        {
          this.m_Node = node;
          this.m_DataSource = DataSource.Create(((Component) node).get_gameObject());
          this.m_DataSource.Add(typeof (FriendData), (object) this.m_Param.owner.friend);
          if (this.m_Param.owner.unit != null)
            this.m_DataSource.Add(typeof (UnitData), (object) this.m_Param.owner.unit);
          this.m_DataSource.Add(typeof (MultiInvitationReceiveWindow.ActiveData.RoomData), (object) this.m_Param);
          this.m_Value = (SerializeValueBehaviour) ((Component) this.m_Node).GetComponent<SerializeValueBehaviour>();
          if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_Value, (UnityEngine.Object) null))
            return;
          this.m_Value.list.SetField("quest_name", this.m_Param.quest.param.name);
          this.m_Value.list.SetField("comment", this.m_Param.comment);
        }

        public void Clear()
        {
          if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_DataSource, (UnityEngine.Object) null))
            return;
          this.m_DataSource.Clear();
          this.m_DataSource = (DataSource) null;
        }

        public void ForceUpdate()
        {
        }
      }

      public class ItemSource : ContentSource
      {
        private List<MultiInvitationReceiveWindow.ActiveContent.ItemSource.ItemParam> m_Params = new List<MultiInvitationReceiveWindow.ActiveContent.ItemSource.ItemParam>();

        public override void Initialize(ContentController controller)
        {
          base.Initialize(controller);
          this.Setup();
        }

        public override void Release()
        {
          base.Release();
        }

        public void Add(MultiInvitationReceiveWindow.ActiveContent.ItemSource.ItemParam param)
        {
          if (!param.IsValid())
            return;
          this.m_Params.Add(param);
        }

        public void Setup()
        {
          Func<MultiInvitationReceiveWindow.ActiveContent.ItemSource.ItemParam, bool> predicate = (Func<MultiInvitationReceiveWindow.ActiveContent.ItemSource.ItemParam, bool>) (prop => true);
          this.Clear();
          if (predicate != null)
            this.SetTable((ContentSource.Param[]) this.m_Params.Where<MultiInvitationReceiveWindow.ActiveContent.ItemSource.ItemParam>(predicate).ToArray<MultiInvitationReceiveWindow.ActiveContent.ItemSource.ItemParam>());
          else
            this.SetTable((ContentSource.Param[]) this.m_Params.ToArray());
          this.contentController.Resize(0);
          bool flag = false;
          Vector2 anchoredPosition = this.contentController.anchoredPosition;
          Vector2 lastPageAnchorePos = this.contentController.GetLastPageAnchorePos();
          if (anchoredPosition.x < lastPageAnchorePos.x)
          {
            flag = true;
            anchoredPosition.x = lastPageAnchorePos.x;
          }
          if (anchoredPosition.y < lastPageAnchorePos.y)
          {
            flag = true;
            anchoredPosition.y = lastPageAnchorePos.y;
          }
          if (flag)
            this.contentController.anchoredPosition = anchoredPosition;
          if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.contentController.scroller, (UnityEngine.Object) null))
            return;
          this.contentController.scroller.StopMovement();
        }

        public class ItemParam : ContentSource.Param
        {
          private MultiInvitationReceiveWindow.ActiveContent.ItemAccessor m_Accessor = new MultiInvitationReceiveWindow.ActiveContent.ItemAccessor();

          public ItemParam(MultiInvitationReceiveWindow.ActiveData.RoomData param)
          {
            this.m_Accessor.Setup(param);
          }

          public override bool IsValid()
          {
            return this.m_Accessor.isValid;
          }

          public MultiInvitationReceiveWindow.ActiveContent.ItemAccessor accerror
          {
            get
            {
              return this.m_Accessor;
            }
          }

          public MultiInvitationReceiveWindow.ActiveData.RoomData param
          {
            get
            {
              return this.m_Accessor.param;
            }
          }

          public override void OnEnable(ContentNode node)
          {
            this.m_Accessor.Bind(node);
            this.m_Accessor.ForceUpdate();
          }

          public override void OnDisable(ContentNode node)
          {
            this.m_Accessor.Clear();
          }

          public override void OnClick(ContentNode node)
          {
          }
        }
      }
    }

    public static class LogContent
    {
      public static MultiInvitationReceiveWindow.LogContent.ItemAccessor clickItem;

      public class ItemAccessor
      {
        private ContentNode m_Node;
        private MultiInvitationReceiveWindow.LogData.RoomData m_Param;
        private DataSource m_DataSource;
        private SerializeValueBehaviour m_Value;

        public ContentNode node
        {
          get
          {
            return this.m_Node;
          }
        }

        public MultiInvitationReceiveWindow.LogData.RoomData param
        {
          get
          {
            return this.m_Param;
          }
        }

        public bool isValid
        {
          get
          {
            if (this.m_Param != null)
              return this.m_Param.isValid;
            return false;
          }
        }

        public void Setup(MultiInvitationReceiveWindow.LogData.RoomData param)
        {
          this.m_Param = param;
        }

        public void Bind(ContentNode node)
        {
          this.m_Node = node;
          this.m_DataSource = DataSource.Create(((Component) node).get_gameObject());
          if (this.m_Param.owner.unit != null)
            this.m_DataSource.Add(typeof (UnitData), (object) this.m_Param.owner.unit);
          this.m_DataSource.Add(typeof (MultiInvitationReceiveWindow.ActiveData.RoomData), (object) this.m_Param);
          this.m_Value = (SerializeValueBehaviour) ((Component) this.m_Node).GetComponent<SerializeValueBehaviour>();
          if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_Value, (UnityEngine.Object) null))
            return;
          this.m_Value.list.SetField("quest_name", this.m_Param.quest.param.name);
          this.m_Value.list.SetField("name", this.m_Param.owner.name);
          this.m_Value.list.SetField("lv", this.m_Param.owner.lv);
          TimeSpan timeSpan = DateTime.Now - GameUtility.UnixtimeToLocalTime(this.m_Param.created_at);
          int days = timeSpan.Days;
          if (days > 0)
            this.m_Value.list.SetField("time", LocalizedText.Get("sys.MULTIINVITATION_RECEIVE_TIMEDAY", new object[1]
            {
              (object) days.ToString()
            }));
          else if (timeSpan.Hours > 0)
            this.m_Value.list.SetField("time", LocalizedText.Get("sys.MULTIINVITATION_RECEIVE_TIMHOUR", new object[1]
            {
              (object) timeSpan.Hours.ToString()
            }));
          else
            this.m_Value.list.SetField("time", LocalizedText.Get("sys.MULTIINVITATION_RECEIVE_TIMEMINUTE", new object[1]
            {
              (object) timeSpan.Minutes.ToString()
            }));
        }

        public void Clear()
        {
          if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_DataSource, (UnityEngine.Object) null))
            return;
          this.m_DataSource.Clear();
          this.m_DataSource = (DataSource) null;
        }

        public void ForceUpdate()
        {
        }
      }

      public class ItemSource : ContentSource
      {
        private List<MultiInvitationReceiveWindow.LogContent.ItemSource.ItemParam> m_Params = new List<MultiInvitationReceiveWindow.LogContent.ItemSource.ItemParam>();

        public override void Initialize(ContentController controller)
        {
          base.Initialize(controller);
          this.Setup();
        }

        public override void Release()
        {
          base.Release();
        }

        public void Add(MultiInvitationReceiveWindow.LogContent.ItemSource.ItemParam param)
        {
          if (!param.IsValid())
            return;
          this.m_Params.Add(param);
        }

        public void Setup()
        {
          Func<MultiInvitationReceiveWindow.LogContent.ItemSource.ItemParam, bool> predicate = (Func<MultiInvitationReceiveWindow.LogContent.ItemSource.ItemParam, bool>) (prop => true);
          this.Clear();
          if (predicate != null)
            this.SetTable((ContentSource.Param[]) this.m_Params.Where<MultiInvitationReceiveWindow.LogContent.ItemSource.ItemParam>(predicate).ToArray<MultiInvitationReceiveWindow.LogContent.ItemSource.ItemParam>());
          else
            this.SetTable((ContentSource.Param[]) this.m_Params.ToArray());
          this.contentController.Resize(0);
          bool flag = false;
          Vector2 anchoredPosition = this.contentController.anchoredPosition;
          Vector2 lastPageAnchorePos = this.contentController.GetLastPageAnchorePos();
          if (anchoredPosition.x < lastPageAnchorePos.x)
          {
            flag = true;
            anchoredPosition.x = lastPageAnchorePos.x;
          }
          if (anchoredPosition.y < lastPageAnchorePos.y)
          {
            flag = true;
            anchoredPosition.y = lastPageAnchorePos.y;
          }
          if (flag)
            this.contentController.anchoredPosition = anchoredPosition;
          if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.contentController.scroller, (UnityEngine.Object) null))
            return;
          this.contentController.scroller.StopMovement();
        }

        public class ItemParam : ContentSource.Param
        {
          private MultiInvitationReceiveWindow.LogContent.ItemAccessor m_Accessor = new MultiInvitationReceiveWindow.LogContent.ItemAccessor();

          public ItemParam(MultiInvitationReceiveWindow.LogData.RoomData param)
          {
            this.m_Accessor.Setup(param);
          }

          public override bool IsValid()
          {
            return this.m_Accessor.isValid;
          }

          public MultiInvitationReceiveWindow.LogContent.ItemAccessor accerror
          {
            get
            {
              return this.m_Accessor;
            }
          }

          public MultiInvitationReceiveWindow.LogData.RoomData param
          {
            get
            {
              return this.m_Accessor.param;
            }
          }

          public override void OnEnable(ContentNode node)
          {
            this.m_Accessor.Bind(node);
            this.m_Accessor.ForceUpdate();
          }

          public override void OnDisable(ContentNode node)
          {
            this.m_Accessor.Clear();
          }

          public override void OnClick(ContentNode node)
          {
          }
        }
      }
    }

    [Serializable]
    public class SerializeParam : FlowWindowBase.SerializeParamBase
    {
      public GameObject tabActive;
      public GameObject tabLog;
      public GameObject activeList;
      public GameObject logList;

      public override System.Type type
      {
        get
        {
          return typeof (MultiInvitationReceiveWindow);
        }
      }
    }

    public class ActiveData
    {
      public MultiInvitationReceiveWindow.ActiveData.RoomData[] rooms;

      public bool Decerialize(FlowNode_ReqMultiInvitation.Api_RoomInvitation.Json json)
      {
        if (json == null || json.rooms == null)
          return false;
        this.rooms = new MultiInvitationReceiveWindow.ActiveData.RoomData[json.rooms.Length];
        for (int index = 0; index < json.rooms.Length; ++index)
        {
          MultiInvitationReceiveWindow.ActiveData.RoomData roomData = new MultiInvitationReceiveWindow.ActiveData.RoomData();
          roomData.roomid = json.rooms[index].roomid;
          roomData.comment = json.rooms[index].comment;
          roomData.num = json.rooms[index].num;
          roomData.multiType = !(json.rooms[index].btype == "multi") ? MultiInvitationReceiveWindow.MultiType.TOWER : MultiInvitationReceiveWindow.MultiType.NORMAL;
          roomData.locked = json.rooms[index].pwd_hash == "1";
          roomData.owner = new MultiInvitationReceiveWindow.ActiveData.OwnerData(json.rooms[index].owner);
          roomData.quest = new MultiInvitationReceiveWindow.ActiveData.QuestData(json.rooms[index].quest);
          if (string.IsNullOrEmpty(roomData.comment))
            roomData.comment = LocalizedText.Get("sys.MULTI_INVTATION_COMMNET");
          this.rooms[index] = roomData;
        }
        return true;
      }

      public class OwnerData
      {
        public FriendData friend;
        public UnitData unit;

        public OwnerData(FlowNode_ReqMultiInvitation.Api_RoomInvitation.JsonRoomOwner json)
        {
          if (json == null)
            return;
          this.friend = MonoSingleton<GameManager>.Instance.Player.Friends.Find((Predicate<FriendData>) (prop => prop.FUID == json.fuid));
          if (json.units == null || json.units.Length <= 0)
            return;
          this.unit = new UnitData();
          this.unit.Deserialize(json.units[0]);
        }

        public bool isValid
        {
          get
          {
            return this.friend != null;
          }
        }

        public string fuid
        {
          get
          {
            return this.friend.FUID;
          }
        }
      }

      public class QuestData
      {
        public QuestParam param;

        public QuestData(FlowNode_ReqMultiInvitation.Api_RoomInvitation.JsonRoomQuest json)
        {
          if (json == null)
            return;
          this.param = MonoSingleton<GameManager>.Instance.FindQuest(json.iname);
        }

        public bool isValid
        {
          get
          {
            return this.param != null;
          }
        }
      }

      public class RoomData
      {
        public int roomid;
        public string comment;
        public int num;
        public MultiInvitationReceiveWindow.MultiType multiType;
        public MultiInvitationReceiveWindow.ActiveData.OwnerData owner;
        public MultiInvitationReceiveWindow.ActiveData.QuestData quest;
        public bool locked;

        public bool isValid
        {
          get
          {
            if (this.multiType == MultiInvitationReceiveWindow.MultiType.NONE || this.owner == null || this.quest == null || !this.owner.isValid)
              return false;
            return this.quest.isValid;
          }
        }
      }
    }

    public class LogData
    {
      public int page = -1;
      public MultiInvitationReceiveWindow.LogData.RoomData[] rooms;

      public bool Decerialize(int _page, FlowNode_ReqMultiInvitationHistory.Api_MultiInvitationHistory.Json json)
      {
        if (json == null || json.list == null)
          return false;
        this.page = _page;
        this.rooms = new MultiInvitationReceiveWindow.LogData.RoomData[json.list.Length];
        for (int index = 0; index < json.list.Length; ++index)
        {
          MultiInvitationReceiveWindow.LogData.RoomData roomData = new MultiInvitationReceiveWindow.LogData.RoomData();
          roomData.id = json.list[index].id;
          roomData.roomid = json.list[index].roomid;
          roomData.multiType = !(json.list[index].btype == "multi") ? MultiInvitationReceiveWindow.MultiType.TOWER : MultiInvitationReceiveWindow.MultiType.NORMAL;
          roomData.owner = new MultiInvitationReceiveWindow.LogData.OwnerData(json.list[index].player);
          roomData.quest = new MultiInvitationReceiveWindow.LogData.QuestData(json.list[index].iname);
          if (!string.IsNullOrEmpty(json.list[index].created_at))
          {
            DateTime targetTime = DateTime.Parse(json.list[index].created_at);
            roomData.created_at = TimeManager.GetUnixSec(targetTime);
          }
          this.rooms[index] = roomData;
        }
        return true;
      }

      public class OwnerData
      {
        public string uid;
        public string fuid;
        public string name;
        public int lv;
        public UnitData unit;

        public OwnerData(FlowNode_ReqMultiInvitationHistory.Api_MultiInvitationHistory.JsonPlayer json)
        {
          if (json == null)
            return;
          this.uid = json.uid;
          this.fuid = json.fuid;
          this.name = json.name;
          this.lv = json.lv;
          if (json.unit == null)
            return;
          this.unit = new UnitData();
          this.unit.Deserialize(json.unit);
        }

        public bool isValid
        {
          get
          {
            if (!string.IsNullOrEmpty(this.uid))
              return this.unit != null;
            return false;
          }
        }
      }

      public class QuestData
      {
        public QuestParam param;

        public QuestData(string iname)
        {
          this.param = MonoSingleton<GameManager>.Instance.FindQuest(iname);
        }

        public bool isValid
        {
          get
          {
            return this.param != null;
          }
        }
      }

      public class RoomData
      {
        public int id;
        public int roomid;
        public MultiInvitationReceiveWindow.MultiType multiType;
        public long created_at;
        public MultiInvitationReceiveWindow.LogData.OwnerData owner;
        public MultiInvitationReceiveWindow.LogData.QuestData quest;

        public bool isValid
        {
          get
          {
            if (this.multiType == MultiInvitationReceiveWindow.MultiType.NONE || this.owner == null || this.quest == null || !this.owner.isValid)
              return false;
            return this.quest.isValid;
          }
        }
      }
    }
  }
}
