// Decompiled with JetBrains decompiler
// Type: SRPG.FriendPresentRootWindow
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
  public class FriendPresentRootWindow : FlowWindowBase
  {
    private static FriendPresentRootWindow.SendStatus m_SendStatus = FriendPresentRootWindow.SendStatus.UNSENT;
    private FriendPresentRootWindow.SerializeParam m_Param;
    private FriendPresentRootWindow.Tab m_Tab;
    private bool m_Destroy;
    private FriendPresentRootWindow.WantContent.ItemSource m_WantSource;
    private ContentController m_WantController;
    private FriendPresentRootWindow.ReceiveContent.ItemSource m_ReceiveSource;
    private ContentController m_ReceiveController;
    private FriendPresentRootWindow.SendContent.ItemSource m_SendSource;
    private ContentController m_SendController;
    private SerializeValueBehaviour m_ValueList;
    private Toggle m_ReceiveToggle;
    private Toggle m_SendToggle;
    private static FriendPresentRootWindow m_Instance;

    public override string name
    {
      get
      {
        return nameof (FriendPresentRootWindow);
      }
    }

    public static FriendPresentRootWindow instance
    {
      get
      {
        return FriendPresentRootWindow.m_Instance;
      }
    }

    public static void SetSendStatus(FriendPresentRootWindow.SendStatus status)
    {
      FriendPresentRootWindow.m_SendStatus = status;
    }

    public override void Initialize(FlowWindowBase.SerializeParamBase param)
    {
      FriendPresentRootWindow.m_Instance = this;
      base.Initialize(param);
      this.m_Param = param as FriendPresentRootWindow.SerializeParam;
      if (this.m_Param == null)
        throw new Exception(this.ToString() + " > Failed serializeParam null.");
      this.m_ValueList = (SerializeValueBehaviour) this.m_Param.window.GetComponent<SerializeValueBehaviour>();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_ValueList, (UnityEngine.Object) null))
      {
        this.m_ReceiveToggle = this.m_ValueList.list.GetUIToggle("tgl_receive");
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_ReceiveToggle, (UnityEngine.Object) null))
          ((Selectable) this.m_ReceiveToggle).set_interactable(false);
        this.m_SendToggle = this.m_ValueList.list.GetUIToggle("tgl_send");
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_SendToggle, (UnityEngine.Object) null))
          ((Selectable) this.m_SendToggle).set_interactable(false);
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_Param.wantList, (UnityEngine.Object) null))
      {
        this.m_WantController = (ContentController) this.m_Param.wantList.GetComponentInChildren<ContentController>();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_WantController, (UnityEngine.Object) null))
          this.m_WantController.SetWork((object) this);
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_Param.receiveList, (UnityEngine.Object) null))
      {
        this.m_ReceiveController = (ContentController) this.m_Param.receiveList.GetComponentInChildren<ContentController>();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_ReceiveController, (UnityEngine.Object) null))
          this.m_ReceiveController.SetWork((object) this);
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_Param.sendList, (UnityEngine.Object) null))
      {
        this.m_SendController = (ContentController) this.m_Param.sendList.GetComponentInChildren<ContentController>();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_SendController, (UnityEngine.Object) null))
          this.m_SendController.SetWork((object) this);
      }
      this.Close(true);
    }

    public override void Release()
    {
      this.ReleaseWantList();
      this.ReleaseReceiveList();
      this.ReleaseSendList();
      base.Release();
      FriendPresentRootWindow.m_Instance = (FriendPresentRootWindow) null;
    }

    public override int Update()
    {
      base.Update();
      if (this.m_Destroy && this.isClosed)
      {
        this.SetActiveChild(false);
        return 190;
      }
      int active = (int) CriticalSection.GetActive();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_ReceiveToggle, (UnityEngine.Object) null))
        ((Selectable) this.m_ReceiveToggle).set_interactable(active == 0);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_SendToggle, (UnityEngine.Object) null))
        ((Selectable) this.m_SendToggle).set_interactable(active == 0);
      return -1;
    }

    private bool SetTab(FriendPresentRootWindow.Tab tab)
    {
      bool flag = false;
      if (this.m_Tab != tab)
      {
        flag = true;
        switch (tab)
        {
          case FriendPresentRootWindow.Tab.NONE:
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_Param.tabReceive, (UnityEngine.Object) null))
              this.m_Param.tabReceive.SetActive(false);
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_Param.tabSend, (UnityEngine.Object) null))
            {
              this.m_Param.tabSend.SetActive(false);
              break;
            }
            break;
          case FriendPresentRootWindow.Tab.RECEIVE:
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_Param.tabReceive, (UnityEngine.Object) null))
              this.m_Param.tabReceive.SetActive(true);
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_Param.tabSend, (UnityEngine.Object) null))
            {
              this.m_Param.tabSend.SetActive(false);
              break;
            }
            break;
          case FriendPresentRootWindow.Tab.SEND:
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_Param.tabReceive, (UnityEngine.Object) null))
              this.m_Param.tabReceive.SetActive(false);
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_Param.tabSend, (UnityEngine.Object) null))
            {
              this.m_Param.tabSend.SetActive(true);
              break;
            }
            break;
        }
        this.m_Tab = tab;
      }
      return flag;
    }

    public void InitializeWantList()
    {
      this.ReleaseWantList();
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_WantController, (UnityEngine.Object) null))
        return;
      this.m_WantSource = new FriendPresentRootWindow.WantContent.ItemSource();
      for (int index = 0; index < 3; ++index)
      {
        FriendPresentItemParam friendPresentWish = MonoSingleton<GameManager>.Instance.Player.FriendPresentWishList[index];
        if (friendPresentWish != null)
          this.m_WantSource.Add(new FriendPresentRootWindow.WantContent.ItemSource.ItemParam(friendPresentWish));
        else
          this.m_WantSource.Add(new FriendPresentRootWindow.WantContent.ItemSource.ItemParam((FriendPresentItemParam) null));
      }
      this.m_WantController.Initialize((ContentSource) this.m_WantSource, Vector2.get_zero());
    }

    public void ReleaseWantList()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_WantController, (UnityEngine.Object) null))
        this.m_WantController.Release();
      this.m_WantSource = (FriendPresentRootWindow.WantContent.ItemSource) null;
    }

    public void InitializeReceiveList()
    {
      this.ReleaseReceiveList();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_ReceiveController, (UnityEngine.Object) null))
      {
        this.m_ReceiveSource = new FriendPresentRootWindow.ReceiveContent.ItemSource();
        List<FriendPresentReceiveList.Param> list = MonoSingleton<GameManager>.Instance.Player.FriendPresentReceiveList.list;
        for (int index = 0; index < list.Count; ++index)
        {
          FriendPresentRootWindow.ReceiveContent.ItemSource.ItemParam itemParam = new FriendPresentRootWindow.ReceiveContent.ItemSource.ItemParam(list[index]);
          if (itemParam.IsValid())
            this.m_ReceiveSource.Add(itemParam);
        }
        this.m_ReceiveController.Initialize((ContentSource) this.m_ReceiveSource, Vector2.get_zero());
      }
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_ValueList, (UnityEngine.Object) null))
        return;
      this.m_ValueList.list.SetInteractable("btn_receive", this.m_ReceiveSource.GetCount() != 0);
    }

    public void ReleaseReceiveList()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_ReceiveController, (UnityEngine.Object) null))
        this.m_ReceiveController.Release();
      this.m_ReceiveSource = (FriendPresentRootWindow.ReceiveContent.ItemSource) null;
    }

    public void InitializeSendList()
    {
      this.ReleaseSendList();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_SendController, (UnityEngine.Object) null))
      {
        this.m_SendSource = new FriendPresentRootWindow.SendContent.ItemSource();
        List<FriendData> friends = MonoSingleton<GameManager>.Instance.Player.Friends;
        for (int index = 0; index < friends.Count; ++index)
        {
          FriendData friend = friends[index];
          if (friend != null && friend.WishStatus != "1")
          {
            FriendPresentRootWindow.SendContent.ItemSource.ItemParam itemParam = new FriendPresentRootWindow.SendContent.ItemSource.ItemParam(friend);
            if (itemParam.IsValid())
              this.m_SendSource.Add(itemParam);
          }
        }
        this.m_SendController.Initialize((ContentSource) this.m_SendSource, Vector2.get_zero());
      }
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_ValueList, (UnityEngine.Object) null))
        return;
      bool flag = false;
      switch (FriendPresentRootWindow.m_SendStatus)
      {
        case FriendPresentRootWindow.SendStatus.NONE:
          this.m_ValueList.list.SetField("status", string.Empty);
          flag = false;
          break;
        case FriendPresentRootWindow.SendStatus.UNSENT:
          this.m_ValueList.list.SetField("status", "sys.FRIENDPRESENT_STATUS_UNSENT");
          flag = true;
          break;
        case FriendPresentRootWindow.SendStatus.SENDING:
          this.m_ValueList.list.SetField("status", "sys.FRIENDPRESENT_STATUS_SENDING");
          flag = false;
          break;
        case FriendPresentRootWindow.SendStatus.SENTFAILED:
          this.m_ValueList.list.SetField("status", "sys.FRIENDPRESENT_STATUS_SENTFAILED");
          flag = true;
          break;
        case FriendPresentRootWindow.SendStatus.SENDED:
          this.m_ValueList.list.SetField("status", "sys.FRIENDPRESENT_STATUS_SENDED");
          flag = true;
          break;
      }
      this.m_ValueList.list.SetInteractable("btn_send", this.m_SendSource.GetCount() != 0 && flag);
    }

    public void ReleaseSendList()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_SendController, (UnityEngine.Object) null))
        this.m_SendController.Release();
      this.m_SendSource = (FriendPresentRootWindow.SendContent.ItemSource) null;
    }

    public override int OnActivate(int pinId)
    {
      switch (pinId)
      {
        case 100:
          if (this.SetTab(FriendPresentRootWindow.Tab.RECEIVE))
          {
            this.InitializeWantList();
            this.InitializeReceiveList();
            this.InitializeSendList();
          }
          this.Open();
          return 191;
        case 110:
          this.m_Destroy = true;
          this.Close(false);
          break;
        case 120:
          if (this.SetTab(FriendPresentRootWindow.Tab.RECEIVE))
          {
            this.InitializeWantList();
            this.InitializeReceiveList();
            break;
          }
          break;
        case 130:
          if (this.SetTab(FriendPresentRootWindow.Tab.SEND))
          {
            this.InitializeSendList();
            break;
          }
          break;
        case 160:
          this.InitializeWantList();
          break;
        case 170:
          this.InitializeReceiveList();
          break;
        case 171:
          MonoSingleton<GameManager>.Instance.Player.FriendPresentReceiveList.Clear();
          this.InitializeReceiveList();
          break;
        case 180:
          this.InitializeSendList();
          break;
      }
      return -1;
    }

    public enum Tab
    {
      NONE,
      RECEIVE,
      SEND,
    }

    public enum SendStatus
    {
      NONE,
      UNSENT,
      SENDING,
      SENTFAILED,
      SENDED,
    }

    public static class WantContent
    {
      public static FriendPresentRootWindow.WantContent.ItemAccessor clickItem;

      public class ItemAccessor
      {
        private ContentNode m_Node;
        private FriendPresentItemParam m_Present;
        private FriendPresentItemIcon m_Icon;

        public ContentNode node
        {
          get
          {
            return this.m_Node;
          }
        }

        public FriendPresentItemParam present
        {
          get
          {
            return this.m_Present;
          }
        }

        public FriendPresentItemIcon icon
        {
          get
          {
            return this.m_Icon;
          }
        }

        public int priority
        {
          get
          {
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_Node, (UnityEngine.Object) null))
              return this.m_Node.index;
            return 0;
          }
        }

        public bool isValid
        {
          get
          {
            return true;
          }
        }

        public void Setup(FriendPresentItemParam present)
        {
          this.m_Present = present;
        }

        public void Bind(ContentNode node)
        {
          this.m_Node = node;
          this.m_Icon = (FriendPresentItemIcon) ((Component) this.m_Node).GetComponent<FriendPresentItemIcon>();
          if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_Icon, (UnityEngine.Object) null))
            return;
          if (this.present != null)
          {
            this.m_Icon.Bind(this.present, true);
          }
          else
          {
            this.m_Icon.Clear();
            this.m_Icon.Refresh();
          }
        }

        public void Clear()
        {
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_Icon, (UnityEngine.Object) null))
          {
            this.m_Icon.Clear();
            this.m_Icon = (FriendPresentItemIcon) null;
          }
          this.m_Node = (ContentNode) null;
        }

        public void ForceUpdate()
        {
          if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_Icon, (UnityEngine.Object) null))
            return;
          this.m_Icon.Refresh();
        }
      }

      public class ItemSource : ContentSource
      {
        private List<FriendPresentRootWindow.WantContent.ItemSource.ItemParam> m_Params = new List<FriendPresentRootWindow.WantContent.ItemSource.ItemParam>();

        public override void Initialize(ContentController controller)
        {
          base.Initialize(controller);
          this.Setup();
        }

        public override void Release()
        {
          base.Release();
        }

        public void Add(FriendPresentRootWindow.WantContent.ItemSource.ItemParam param)
        {
          if (!param.IsValid())
            return;
          this.m_Params.Add(param);
        }

        public void Setup()
        {
          Func<FriendPresentRootWindow.WantContent.ItemSource.ItemParam, bool> predicate = (Func<FriendPresentRootWindow.WantContent.ItemSource.ItemParam, bool>) (prop => true);
          this.Clear();
          if (predicate != null)
            this.SetTable((ContentSource.Param[]) this.m_Params.Where<FriendPresentRootWindow.WantContent.ItemSource.ItemParam>(predicate).ToArray<FriendPresentRootWindow.WantContent.ItemSource.ItemParam>());
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
          private FriendPresentRootWindow.WantContent.ItemAccessor m_Accessor = new FriendPresentRootWindow.WantContent.ItemAccessor();

          public ItemParam(FriendPresentItemParam present)
          {
            this.m_Accessor.Setup(present);
          }

          public override bool IsValid()
          {
            return this.m_Accessor.isValid;
          }

          public FriendPresentRootWindow.WantContent.ItemAccessor accerror
          {
            get
            {
              return this.m_Accessor;
            }
          }

          public FriendPresentItemParam present
          {
            get
            {
              return this.m_Accessor.present;
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
            FriendPresentRootWindow.WantContent.clickItem = this.m_Accessor;
            ButtonEvent.Invoke("FRIENDPRESENT_WANTLIST_OPEN", (object) node);
          }
        }
      }
    }

    public static class ReceiveContent
    {
      public static FriendPresentRootWindow.ReceiveContent.ItemAccessor clickItem;

      public class ItemAccessor
      {
        private ContentNode m_Node;
        private FriendPresentReceiveList.Param m_Param;
        private FriendPresentItemIcon m_Icon;
        private SerializeValueBehaviour m_Value;

        public ContentNode node
        {
          get
          {
            return this.m_Node;
          }
        }

        public FriendPresentReceiveList.Param param
        {
          get
          {
            return this.m_Param;
          }
        }

        public FriendPresentItemParam present
        {
          get
          {
            return this.m_Param.present;
          }
        }

        public FriendPresentItemIcon icon
        {
          get
          {
            return this.m_Icon;
          }
        }

        public bool isValid
        {
          get
          {
            return this.m_Param != null;
          }
        }

        public void Setup(FriendPresentReceiveList.Param param)
        {
          this.m_Param = param;
        }

        public void Bind(ContentNode node)
        {
          this.m_Node = node;
          this.m_Icon = (FriendPresentItemIcon) ((Component) this.m_Node).GetComponent<FriendPresentItemIcon>();
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_Icon, (UnityEngine.Object) null))
            this.m_Icon.Bind(this.present, false);
          this.m_Value = (SerializeValueBehaviour) ((Component) this.m_Node).GetComponent<SerializeValueBehaviour>();
          if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_Value, (UnityEngine.Object) null))
            return;
          this.m_Value.list.SetField("num", this.m_Param.num);
        }

        public void Clear()
        {
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_Icon, (UnityEngine.Object) null))
          {
            this.m_Icon.Clear();
            this.m_Icon = (FriendPresentItemIcon) null;
          }
          this.m_Value = (SerializeValueBehaviour) null;
          this.m_Node = (ContentNode) null;
        }

        public void ForceUpdate()
        {
          if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_Icon, (UnityEngine.Object) null))
            return;
          this.m_Icon.Refresh();
        }
      }

      public class ItemSource : ContentSource
      {
        private List<FriendPresentRootWindow.ReceiveContent.ItemSource.ItemParam> m_Params = new List<FriendPresentRootWindow.ReceiveContent.ItemSource.ItemParam>();

        public override void Initialize(ContentController controller)
        {
          base.Initialize(controller);
          this.Setup();
        }

        public override void Release()
        {
          base.Release();
        }

        public void Add(FriendPresentRootWindow.ReceiveContent.ItemSource.ItemParam param)
        {
          if (!param.IsValid())
            return;
          this.m_Params.Add(param);
        }

        public void Setup()
        {
          Func<FriendPresentRootWindow.ReceiveContent.ItemSource.ItemParam, bool> predicate = (Func<FriendPresentRootWindow.ReceiveContent.ItemSource.ItemParam, bool>) (prop => true);
          this.Clear();
          if (predicate != null)
            this.SetTable((ContentSource.Param[]) this.m_Params.Where<FriendPresentRootWindow.ReceiveContent.ItemSource.ItemParam>(predicate).ToArray<FriendPresentRootWindow.ReceiveContent.ItemSource.ItemParam>());
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
          private FriendPresentRootWindow.ReceiveContent.ItemAccessor m_Accessor = new FriendPresentRootWindow.ReceiveContent.ItemAccessor();

          public ItemParam(FriendPresentReceiveList.Param param)
          {
            this.m_Accessor.Setup(param);
          }

          public override bool IsValid()
          {
            return this.m_Accessor.isValid;
          }

          public FriendPresentRootWindow.ReceiveContent.ItemAccessor accerror
          {
            get
            {
              return this.m_Accessor;
            }
          }

          public FriendPresentReceiveList.Param param
          {
            get
            {
              return this.m_Accessor.param;
            }
          }

          public FriendPresentItemParam present
          {
            get
            {
              return this.m_Accessor.present;
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

    public static class SendContent
    {
      public static FriendPresentRootWindow.SendContent.ItemAccessor clickItem;

      public class ItemAccessor
      {
        private ContentNode m_Node;
        private FriendData m_Friend;
        private FriendPresentItemParam m_Present;
        private FriendPresentItemIcon m_Icon;
        private DataSource m_DataSource;

        public ContentNode node
        {
          get
          {
            return this.m_Node;
          }
        }

        public FriendData friend
        {
          get
          {
            return this.m_Friend;
          }
        }

        public FriendPresentItemParam present
        {
          get
          {
            return this.m_Present;
          }
        }

        public FriendPresentItemIcon icon
        {
          get
          {
            return this.m_Icon;
          }
        }

        public bool isValid
        {
          get
          {
            return this.m_Friend != null && this.m_Present != null;
          }
        }

        public void Setup(FriendData friend)
        {
          this.m_Friend = friend;
          if (string.IsNullOrEmpty(friend.Wish))
            this.m_Present = FriendPresentItemParam.DefaultParam;
          else
            this.m_Present = MonoSingleton<GameManager>.Instance.MasterParam.GetFriendPresentItemParam(friend.Wish);
        }

        public void Bind(ContentNode node)
        {
          this.m_Node = node;
          this.m_DataSource = DataSource.Create(((Component) node).get_gameObject());
          this.m_DataSource.Add(typeof (FriendData), (object) this.m_Friend);
          this.m_DataSource.Add(typeof (UnitData), (object) this.m_Friend.Unit);
          this.m_Icon = (FriendPresentItemIcon) ((Component) this.m_Node).GetComponent<FriendPresentItemIcon>();
          if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_Icon, (UnityEngine.Object) null))
            return;
          this.m_Icon.Bind(this.present, false);
        }

        public void Clear()
        {
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_DataSource, (UnityEngine.Object) null))
          {
            this.m_DataSource.Clear();
            this.m_DataSource = (DataSource) null;
          }
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_Icon, (UnityEngine.Object) null))
          {
            this.m_Icon.Clear();
            this.m_Icon = (FriendPresentItemIcon) null;
          }
          this.m_Node = (ContentNode) null;
        }

        public void ForceUpdate()
        {
          if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_Icon, (UnityEngine.Object) null))
            return;
          this.m_Icon.Refresh();
        }
      }

      public class ItemSource : ContentSource
      {
        private List<FriendPresentRootWindow.SendContent.ItemSource.ItemParam> m_Params = new List<FriendPresentRootWindow.SendContent.ItemSource.ItemParam>();

        public override void Initialize(ContentController controller)
        {
          base.Initialize(controller);
          this.Setup();
        }

        public override void Release()
        {
          base.Release();
        }

        public void Add(FriendPresentRootWindow.SendContent.ItemSource.ItemParam param)
        {
          if (!param.IsValid())
            return;
          this.m_Params.Add(param);
        }

        public void Setup()
        {
          Func<FriendPresentRootWindow.SendContent.ItemSource.ItemParam, bool> predicate = (Func<FriendPresentRootWindow.SendContent.ItemSource.ItemParam, bool>) (prop => true);
          this.Clear();
          if (predicate != null)
            this.SetTable((ContentSource.Param[]) this.m_Params.Where<FriendPresentRootWindow.SendContent.ItemSource.ItemParam>(predicate).ToArray<FriendPresentRootWindow.SendContent.ItemSource.ItemParam>());
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
          private FriendPresentRootWindow.SendContent.ItemAccessor m_Accessor = new FriendPresentRootWindow.SendContent.ItemAccessor();

          public ItemParam(FriendData friend)
          {
            this.m_Accessor.Setup(friend);
          }

          public override bool IsValid()
          {
            return this.m_Accessor.isValid;
          }

          public FriendPresentRootWindow.SendContent.ItemAccessor accerror
          {
            get
            {
              return this.m_Accessor;
            }
          }

          public FriendData friend
          {
            get
            {
              return this.m_Accessor.friend;
            }
          }

          public FriendPresentItemParam present
          {
            get
            {
              return this.m_Accessor.present;
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
      public GameObject tabReceive;
      public GameObject tabSend;
      public GameObject wantList;
      public GameObject receiveList;
      public GameObject sendList;

      public override System.Type type
      {
        get
        {
          return typeof (FriendPresentRootWindow);
        }
      }
    }
  }
}
