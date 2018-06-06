// Decompiled with JetBrains decompiler
// Type: SRPG.FriendList
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(1, "Refresh", FlowNode.PinTypes.Input, 1)]
  [AddComponentMenu("UI/リスト/宿屋で表示するフレンドリスト")]
  public class FriendList : MonoBehaviour, IFlowInterface
  {
    private const string PREFS_KEY_FRIEND_SORT = "FRIENDLIST_SORTTYPE";
    [Description("リストアイテムとして使用するゲームオブジェクト")]
    public GameObject ItemTemplate;
    [Description("リストが空のときに表示するゲームオブジェクト")]
    public GameObject ItemEmpty;
    [Description("表示するフレンドの種類")]
    public FriendStates FriendType;
    [Description("ソート用プルダウン")]
    public Pulldown SortPulldown;
    private string[] mSortString;
    private FriendList.eSortType mSortType;
    private List<GameObject> mItems;
    private CanvasGroup mCanvasGroup;

    public FriendList()
    {
      base.\u002Ector();
    }

    public void Activated(int pinID)
    {
      this.Refresh();
    }

    private void Awake()
    {
      if (Object.op_Inequality((Object) this.ItemTemplate, (Object) null) && this.ItemTemplate.get_activeInHierarchy())
        this.ItemTemplate.SetActive(false);
      this.mCanvasGroup = (CanvasGroup) ((Component) this).GetComponent<CanvasGroup>();
      if (!Object.op_Equality((Object) this.mCanvasGroup, (Object) null))
        return;
      this.mCanvasGroup = (CanvasGroup) ((Component) this).get_gameObject().AddComponent<CanvasGroup>();
    }

    private void Start()
    {
      if (Object.op_Equality((Object) this.ItemTemplate, (Object) null))
        return;
      this.SortPulldownInit();
      this.Refresh();
    }

    private void Update()
    {
      if (!Object.op_Inequality((Object) this.mCanvasGroup, (Object) null) || (double) this.mCanvasGroup.get_alpha() >= 1.0)
        return;
      this.mCanvasGroup.set_alpha(Mathf.Clamp01(this.mCanvasGroup.get_alpha() + Time.get_unscaledDeltaTime() * 3.333333f));
    }

    private void SortPulldownInit()
    {
      if (!string.IsNullOrEmpty("FRIENDLIST_SORTTYPE") && PlayerPrefs.HasKey("FRIENDLIST_SORTTYPE"))
      {
        int num = PlayerPrefs.GetInt("FRIENDLIST_SORTTYPE");
        if (0 <= num && num < 3)
          this.mSortType = (FriendList.eSortType) num;
      }
      if (!Object.op_Inequality((Object) this.SortPulldown, (Object) null))
        return;
      this.SortPulldown.OnSelectionChangeDelegate = new Pulldown.SelectItemEvent(this.OnSortChange);
      this.SortPulldown.ClearItems();
      for (int index = 0; index < this.mSortString.Length; ++index)
        this.SortPulldown.AddItem(LocalizedText.Get(this.mSortString[index]), index);
      this.SortPulldown.Selection = (int) this.mSortType;
      this.SortPulldown.set_interactable(true);
      ((Component) this.SortPulldown).get_gameObject().SetActive(true);
    }

    private void SortByEntryDate(List<FriendData> lists)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: reference to a compiler-generated method
      lists.Sort(new Comparison<FriendData>(new FriendList.\u003CSortByEntryDate\u003Ec__AnonStorey247()
      {
        created_at1 = DateTime.Now,
        created_at2 = DateTime.Now,
        str_datetime_fmt = "yyyy-MM-ddTHH:mm:ss.fffZ",
        ci = new CultureInfo("en-US")
      }.\u003C\u003Em__27F));
    }

    private void SortByLastLogin(List<FriendData> lists)
    {
      lists.Sort((Comparison<FriendData>) ((fr1, fr2) => (int) (fr2.LastLogin - fr1.LastLogin)));
    }

    private void SortByPlayerLevel(List<FriendData> lists)
    {
      lists.Sort((Comparison<FriendData>) ((fr1, fr2) => fr2.PlayerLevel - fr1.PlayerLevel));
    }

    private void entryItems()
    {
      List<FriendData> friendDataList = new List<FriendData>();
      List<FriendData> lists;
      switch (this.FriendType)
      {
        case FriendStates.Friend:
          lists = MonoSingleton<GameManager>.Instance.Player.Friends;
          break;
        case FriendStates.Follow:
          lists = MonoSingleton<GameManager>.Instance.Player.FriendsFollow;
          break;
        case FriendStates.Follwer:
          lists = MonoSingleton<GameManager>.Instance.Player.FriendsFollower;
          break;
        default:
          return;
      }
      if (lists.Count == 0)
        return;
      switch (this.mSortType)
      {
        case FriendList.eSortType.MIN:
          this.SortByEntryDate(lists);
          break;
        case FriendList.eSortType.LAST_LOGIN:
          this.SortByLastLogin(lists);
          break;
        case FriendList.eSortType.PLAYER_LEVEL:
          this.SortByPlayerLevel(lists);
          break;
      }
      Transform transform = ((Component) this).get_transform();
      using (List<FriendData>.Enumerator enumerator = lists.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          FriendData current = enumerator.Current;
          GameObject gameObject = (GameObject) Object.Instantiate<GameObject>((M0) this.ItemTemplate);
          if (Object.op_Implicit((Object) gameObject))
          {
            gameObject.get_transform().SetParent(transform, false);
            ListItemEvents component = (ListItemEvents) gameObject.GetComponent<ListItemEvents>();
            if (Object.op_Inequality((Object) component, (Object) null))
              component.OnSelect = new ListItemEvents.ListItemEvent(this.OnSelectItem);
            DataSource.Bind<FriendData>(gameObject, current);
            DataSource.Bind<UnitData>(gameObject, current.Unit);
            gameObject.SetActive(true);
            this.mItems.Add(gameObject);
          }
        }
      }
    }

    private void OnSortChange(int idx)
    {
      if ((FriendList.eSortType) idx == this.mSortType || 0 > idx || idx >= 3)
        return;
      this.mSortType = (FriendList.eSortType) idx;
      this.Refresh();
      if (string.IsNullOrEmpty("FRIENDLIST_SORTTYPE"))
        return;
      PlayerPrefs.SetInt("FRIENDLIST_SORTTYPE", idx);
      PlayerPrefs.Save();
    }

    private void Refresh()
    {
      if (Object.op_Inequality((Object) this.mCanvasGroup, (Object) null))
        this.mCanvasGroup.set_alpha(0.0f);
      for (int index = 0; index < this.mItems.Count; ++index)
      {
        GameObject mItem = this.mItems[index];
        if (Object.op_Inequality((Object) mItem, (Object) null))
          Object.Destroy((Object) mItem);
      }
      this.mItems.Clear();
      this.entryItems();
      if (this.mItems.Count > 0 && Object.op_Inequality((Object) this.ItemEmpty, (Object) null))
        this.ItemEmpty.SetActive(false);
      else
        this.ItemEmpty.SetActive(true);
    }

    private void OnSelectItem(GameObject go)
    {
      FriendData dataOfClass = DataSource.FindDataOfClass<FriendData>(go, (FriendData) null);
      if (dataOfClass == null)
        return;
      GlobalVars.SelectedFriendID = dataOfClass.FUID;
      GlobalVars.SelectedFriend = dataOfClass;
      FlowNode_OnFriendSelect nodeOnFriendSelect = (FlowNode_OnFriendSelect) ((Component) this).GetComponentInParent<FlowNode_OnFriendSelect>();
      if (Object.op_Equality((Object) nodeOnFriendSelect, (Object) null))
        nodeOnFriendSelect = (FlowNode_OnFriendSelect) Object.FindObjectOfType<FlowNode_OnFriendSelect>();
      if (!Object.op_Inequality((Object) nodeOnFriendSelect, (Object) null))
        return;
      nodeOnFriendSelect.Selected();
    }

    private enum eSortType
    {
      DEFAULT = 0,
      ENTRY_DATE = 0,
      MIN = 0,
      LAST_LOGIN = 1,
      PLAYER_LEVEL = 2,
      MAX = 3,
    }
  }
}
