// Decompiled with JetBrains decompiler
// Type: SRPG.FriendList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

namespace SRPG
{
  [AddComponentMenu("UI/リスト/宿屋で表示するフレンドリスト")]
  [FlowNode.Pin(1, "Refresh", FlowNode.PinTypes.Input, 1)]
  public class FriendList : MonoBehaviour, IFlowInterface
  {
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
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ItemTemplate, (UnityEngine.Object) null) && this.ItemTemplate.get_activeInHierarchy())
        this.ItemTemplate.SetActive(false);
      this.mCanvasGroup = (CanvasGroup) ((Component) this).GetComponent<CanvasGroup>();
      if (!UnityEngine.Object.op_Equality((UnityEngine.Object) this.mCanvasGroup, (UnityEngine.Object) null))
        return;
      this.mCanvasGroup = (CanvasGroup) ((Component) this).get_gameObject().AddComponent<CanvasGroup>();
    }

    private void Start()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.ItemTemplate, (UnityEngine.Object) null))
        return;
      this.SortPulldownInit();
      this.Refresh();
    }

    private void Update()
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mCanvasGroup, (UnityEngine.Object) null) || (double) this.mCanvasGroup.get_alpha() >= 1.0)
        return;
      this.mCanvasGroup.set_alpha(Mathf.Clamp01(this.mCanvasGroup.get_alpha() + Time.get_unscaledDeltaTime() * 3.333333f));
    }

    private void SortPulldownInit()
    {
      if (!string.IsNullOrEmpty(PlayerPrefsUtility.PREFS_KEY_FRIEND_SORT) && PlayerPrefsUtility.HasKey(PlayerPrefsUtility.PREFS_KEY_FRIEND_SORT))
      {
        int num = PlayerPrefsUtility.GetInt(PlayerPrefsUtility.PREFS_KEY_FRIEND_SORT, 0);
        if (0 <= num && num < 3)
          this.mSortType = (FriendList.eSortType) num;
      }
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.SortPulldown, (UnityEngine.Object) null))
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
      DateTime created_at1 = DateTime.Now;
      DateTime created_at2 = DateTime.Now;
      string str_datetime_fmt = TimeManager.ISO_8601_FORMAT;
      CultureInfo ci = new CultureInfo("ja-JP");
      lists.Sort((Comparison<FriendData>) ((fr1, fr2) =>
      {
        if (!DateTime.TryParseExact(fr1.CreatedAt, str_datetime_fmt, (IFormatProvider) ci, DateTimeStyles.None, out created_at1))
          Debug.LogWarningFormat("FriendList/SortByEntryDate ParseExact error! FUID={0}, PlayerName={1}, mCreatedAt={2}", new object[3]
          {
            (object) fr1.FUID,
            (object) fr1.PlayerName,
            (object) fr1.CreatedAt
          });
        if (!DateTime.TryParseExact(fr2.CreatedAt, str_datetime_fmt, (IFormatProvider) ci, DateTimeStyles.None, out created_at2))
          Debug.LogWarningFormat("FriendList/SortByEntryDate ParseExact error! FUID={0}, PlayerName={1}, mCreatedAt={2}", new object[3]
          {
            (object) fr2.FUID,
            (object) fr2.PlayerName,
            (object) fr2.CreatedAt
          });
        return (int) (TimeManager.FromDateTime(created_at2) - TimeManager.FromDateTime(created_at1));
      }));
    }

    private void SortByLastLogin(List<FriendData> lists)
    {
      lists.Sort((Comparison<FriendData>) ((fr1, fr2) => (int) (fr2.LastLogin - fr1.LastLogin)));
    }

    private void SortByPlayerLevel(List<FriendData> lists)
    {
      lists.Sort((Comparison<FriendData>) ((fr1, fr2) =>
      {
        int num = fr2.PlayerLevel - fr1.PlayerLevel;
        if (num != 0)
          return num;
        return string.Compare(fr2.CreatedAt, fr1.CreatedAt);
      }));
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
          GameObject gameObject = (GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) this.ItemTemplate);
          if (UnityEngine.Object.op_Implicit((UnityEngine.Object) gameObject))
          {
            gameObject.get_transform().SetParent(transform, false);
            ListItemEvents component = (ListItemEvents) gameObject.GetComponent<ListItemEvents>();
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
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
      if (string.IsNullOrEmpty(PlayerPrefsUtility.PREFS_KEY_FRIEND_SORT))
        return;
      PlayerPrefsUtility.SetInt(PlayerPrefsUtility.PREFS_KEY_FRIEND_SORT, idx, true);
    }

    private void Refresh()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mCanvasGroup, (UnityEngine.Object) null))
        this.mCanvasGroup.set_alpha(0.0f);
      for (int index = 0; index < this.mItems.Count; ++index)
      {
        GameObject mItem = this.mItems[index];
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) mItem, (UnityEngine.Object) null))
          UnityEngine.Object.Destroy((UnityEngine.Object) mItem);
      }
      this.mItems.Clear();
      this.entryItems();
      if (this.mItems.Count > 0 && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ItemEmpty, (UnityEngine.Object) null))
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
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) nodeOnFriendSelect, (UnityEngine.Object) null))
        nodeOnFriendSelect = (FlowNode_OnFriendSelect) UnityEngine.Object.FindObjectOfType<FlowNode_OnFriendSelect>();
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) nodeOnFriendSelect, (UnityEngine.Object) null))
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
