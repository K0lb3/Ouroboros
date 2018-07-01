// Decompiled with JetBrains decompiler
// Type: SRPG.FriendPresentWantWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SRPG
{
  public class FriendPresentWantWindow : FlowWindowBase
  {
    private FriendPresentWantWindow.Content.ItemSource m_ContentSource;
    private ContentController m_ContentController;

    public override string name
    {
      get
      {
        return nameof (FriendPresentWantWindow);
      }
    }

    public override void Initialize(FlowWindowBase.SerializeParamBase param)
    {
      base.Initialize(param);
      FriendPresentWantWindow.SerializeParam serializeParam = param as FriendPresentWantWindow.SerializeParam;
      if (serializeParam == null)
        throw new Exception(this.ToString() + " > Failed serializeParam null.");
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) serializeParam.list, (UnityEngine.Object) null))
      {
        this.m_ContentController = (ContentController) serializeParam.list.GetComponentInChildren<ContentController>();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_ContentController, (UnityEngine.Object) null))
          this.m_ContentController.SetWork((object) this);
      }
      this.Close(true);
    }

    public override void Release()
    {
      this.ReleaseContentList();
      base.Release();
    }

    public override int Update()
    {
      base.Update();
      if (!this.isClosed)
        return -1;
      this.SetActiveChild(false);
      return 290;
    }

    public void InitializeContentList()
    {
      this.ReleaseContentList();
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_ContentController, (UnityEngine.Object) null))
        return;
      this.m_ContentSource = new FriendPresentWantWindow.Content.ItemSource();
      FriendPresentItemParam[] presentItemParams = MonoSingleton<GameManager>.Instance.MasterParam.GetFriendPresentItemParams();
      List<FriendPresentItemParam> list = new List<FriendPresentItemParam>();
      for (int index = 0; index < presentItemParams.Length; ++index)
      {
        if (!presentItemParams[index].IsDefault() && presentItemParams[index].IsValid(Network.GetServerTime()))
          list.Add(presentItemParams[index]);
      }
      long serverTime = Network.GetServerTime();
      SortUtility.StableSort<FriendPresentItemParam>(list, (Comparison<FriendPresentItemParam>) ((p1, p2) => (!p1.HasTimeLimit() ? long.MaxValue : p1.GetRestTime(serverTime)).CompareTo(!p2.HasTimeLimit() ? long.MaxValue : p2.GetRestTime(serverTime))));
      for (int index = 0; index < list.Count; ++index)
      {
        FriendPresentWantWindow.Content.ItemSource.ItemParam itemParam = new FriendPresentWantWindow.Content.ItemSource.ItemParam(list[index]);
        if (itemParam.IsValid())
          this.m_ContentSource.Add(itemParam);
      }
      this.m_ContentController.Initialize((ContentSource) this.m_ContentSource, Vector2.get_zero());
    }

    public void ReleaseContentList()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_ContentController, (UnityEngine.Object) null))
        this.m_ContentController.Release();
      this.m_ContentSource = (FriendPresentWantWindow.Content.ItemSource) null;
    }

    public override int OnActivate(int pinId)
    {
      switch (pinId)
      {
        case 200:
          this.InitializeContentList();
          this.Open();
          break;
        case 210:
          this.Close(false);
          break;
      }
      return -1;
    }

    public static class Content
    {
      public static FriendPresentWantWindow.Content.ItemAccessor clickItem;

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

        public string presentId
        {
          get
          {
            if (this.m_Present != null)
              return this.m_Present.iname;
            return "FP_DEFAULT";
          }
        }

        public bool isValid
        {
          get
          {
            return this.m_Present != null;
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
          this.m_Icon.Bind(this.present, false);
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
        private List<FriendPresentWantWindow.Content.ItemSource.ItemParam> m_Params = new List<FriendPresentWantWindow.Content.ItemSource.ItemParam>();

        public override void Initialize(ContentController controller)
        {
          base.Initialize(controller);
          this.Setup();
        }

        public override void Release()
        {
          base.Release();
        }

        public void Add(FriendPresentWantWindow.Content.ItemSource.ItemParam param)
        {
          if (!param.IsValid())
            return;
          this.m_Params.Add(param);
        }

        public void Setup()
        {
          Func<FriendPresentWantWindow.Content.ItemSource.ItemParam, bool> predicate = (Func<FriendPresentWantWindow.Content.ItemSource.ItemParam, bool>) (prop => true);
          this.Clear();
          if (predicate != null)
            this.SetTable((ContentSource.Param[]) this.m_Params.Where<FriendPresentWantWindow.Content.ItemSource.ItemParam>(predicate).ToArray<FriendPresentWantWindow.Content.ItemSource.ItemParam>());
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
          private FriendPresentWantWindow.Content.ItemAccessor m_Accessor = new FriendPresentWantWindow.Content.ItemAccessor();

          public ItemParam(FriendPresentItemParam present)
          {
            this.m_Accessor.Setup(present);
          }

          public override bool IsValid()
          {
            return this.m_Accessor.isValid;
          }

          public FriendPresentWantWindow.Content.ItemAccessor accerror
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
            FriendPresentWantWindow.Content.clickItem = this.m_Accessor;
            ButtonEvent.Invoke("FRIENDPRESENT_WANTLIST_SELECT", (object) node);
          }
        }
      }
    }

    [Serializable]
    public class SerializeParam : FlowWindowBase.SerializeParamBase
    {
      public GameObject list;

      public override System.Type type
      {
        get
        {
          return typeof (FriendPresentWantWindow);
        }
      }
    }
  }
}
