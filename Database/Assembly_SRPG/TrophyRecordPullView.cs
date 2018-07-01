// Decompiled with JetBrains decompiler
// Type: SRPG.TrophyRecordPullView
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class TrophyRecordPullView : SRPG_ListBase
  {
    private float FRAME_MERGIN = 25f;
    [SerializeField]
    private int CREATE_CHILD_COUNT = 20;
    private TrophyRecordPullView.eState state = TrophyRecordPullView.eState.CLOSED;
    [SerializeField]
    private float CLOSE_SECOND = 0.15f;
    private float DEFAULT_OPEN_SPEED_AREA = 500f;
    private float OPEN_SPEED = 2000f;
    private float HI_OPEN_SPEED = 20000f;
    private float HI_CLOSE_SPEED = 9000f;
    [SerializeField]
    private GameObject[] original_objects;
    [SerializeField]
    private GameObject badge;
    [SerializeField]
    private VerticalLayoutGroup vertical_layout_group;
    [SerializeField]
    private Transform contents_parent;
    [SerializeField]
    private LayoutElement root_layout_element;
    [SerializeField]
    private RectTransform view_port_rect;
    [SerializeField]
    private RectTransform grid_rect;
    [SerializeField]
    private RectTransform contents_transform;
    [SerializeField]
    private RectTransform button_open_rect;
    [SerializeField]
    private RectTransform button_close_rect;
    [SerializeField]
    private BitmapText comp_trophy_count_text;
    [SerializeField]
    private BitmapText total_trophy_count_text;
    private TrophyCategoryData category_data;
    private TrophyList trophy_list;
    private int comp_trophy_count;
    private int index;
    private float item_distance;
    private float view_mergin;
    private float start_button_open_size;
    private Vector2 start_pos;
    private Vector2 target_pos;
    private float default_min_height;
    private float target_view_port_size;
    private float anim_speed;
    private float move_value;

    private TrophyRecordPullView.eState State
    {
      get
      {
        return this.state;
      }
    }

    public int HashCode
    {
      get
      {
        return this.category_data.Param.hash_code;
      }
    }

    public bool IsStateOpen
    {
      get
      {
        if (this.state != TrophyRecordPullView.eState.OPEN)
          return this.state == TrophyRecordPullView.eState.OPENED;
        return true;
      }
    }

    public bool IsStateOpened
    {
      get
      {
        return this.state == TrophyRecordPullView.eState.OPENED;
      }
    }

    public bool IsStateClose
    {
      get
      {
        if (this.state != TrophyRecordPullView.eState.CLOSE && this.state != TrophyRecordPullView.eState.CLOSED)
          return this.state == TrophyRecordPullView.eState.CLOSE_IMMEDIATE;
        return true;
      }
    }

    public bool IsStateClosed
    {
      get
      {
        return this.state == TrophyRecordPullView.eState.CLOSED;
      }
    }

    public float RootLayoutElementMinHeightDef
    {
      get
      {
        return this.root_layout_element.get_minHeight() - this.default_min_height;
      }
    }

    public int Index
    {
      get
      {
        return this.index;
      }
    }

    public float ItemDistance
    {
      get
      {
        return this.item_distance;
      }
    }

    public float TargetViewPortSize
    {
      get
      {
        return this.target_view_port_size;
      }
    }

    public float VerticalLayoutSpacing
    {
      get
      {
        return ((HorizontalOrVerticalLayoutGroup) this.vertical_layout_group).get_spacing();
      }
    }

    protected override RectTransform GetRectTransform()
    {
      return ((Component) this.trophy_list).get_transform() as RectTransform;
    }

    private void Awake()
    {
      if (!Object.op_Inequality((Object) this.badge, (Object) null))
        return;
      ((Component) this.button_open_rect).get_gameObject().SetActive(false);
      ((Component) this.button_close_rect).get_gameObject().SetActive(true);
      this.badge.SetActive(false);
    }

    private void Update()
    {
      switch (this.state)
      {
        case TrophyRecordPullView.eState.OPEN:
          this.UpdateOpen();
          break;
        case TrophyRecordPullView.eState.CLOSE:
          this.UpdateClose();
          break;
        case TrophyRecordPullView.eState.CLOSE_IMMEDIATE:
          this.UpdateCloseImmediate();
          break;
      }
    }

    public void Init(string _title_str)
    {
      ((Component) this).get_gameObject().SetActive(true);
      Text componentInChildren = (Text) ((Component) this).GetComponentInChildren<Text>();
      if (Object.op_Inequality((Object) componentInChildren, (Object) null))
        componentInChildren.set_text(_title_str);
      if (this.original_objects.Length > 0)
      {
        for (int index = 0; index < this.original_objects.Length; ++index)
          this.original_objects[index].SetActive(false);
      }
      if (!Object.op_Inequality((Object) this.root_layout_element, (Object) null))
        return;
      this.default_min_height = this.root_layout_element.get_minHeight();
    }

    public void Setup(int _index, TrophyList _trophy_list)
    {
      this.index = _index;
      this.item_distance = Mathf.Abs((float) (this.contents_transform.get_anchoredPosition().y + this.grid_rect.get_anchoredPosition().y));
      this.trophy_list = _trophy_list;
      this.view_mergin = this.item_distance * (float) (this.index + 1);
    }

    public void SetCategoryData(TrophyCategoryData _category_data)
    {
      this.category_data = _category_data;
    }

    public void Refresh(RectTransform _scroll_trans_rect)
    {
      if (this.State != TrophyRecordPullView.eState.OPENED || !((Component) this).get_gameObject().get_activeInHierarchy() || Object.op_Equality((Object) _scroll_trans_rect, (Object) null))
        return;
      this.ClearItems();
      this.Setup(this.index, this.trophy_list);
      this.RefreshDisplayParam();
      this.CreateContents();
      this.view_port_rect.set_sizeDelta(new Vector2((float) this.view_port_rect.get_sizeDelta().x, this.target_view_port_size));
      this.root_layout_element.set_minHeight(this.default_min_height + this.target_view_port_size);
      this.button_open_rect.set_sizeDelta(new Vector2(0.0f, (float) (this.view_port_rect.get_sizeDelta().y - this.contents_transform.get_anchoredPosition().y)));
      this.target_pos = new Vector2((float) _scroll_trans_rect.get_anchoredPosition().x, (float) this.index * this.item_distance);
      _scroll_trans_rect.set_anchoredPosition(new Vector2(0.0f, (float) this.target_pos.y));
    }

    public void RefreshDisplayParam()
    {
      this.comp_trophy_count = 0;
      for (int index = 0; index < this.category_data.Trophies.Count; ++index)
      {
        if (this.category_data.Trophies[index].IsCompleted)
          ++this.comp_trophy_count;
      }
      if (Object.op_Inequality((Object) this.comp_trophy_count_text, (Object) null) && Object.op_Inequality((Object) this.total_trophy_count_text, (Object) null))
      {
        int num1 = Mathf.Min(this.CREATE_CHILD_COUNT, this.comp_trophy_count);
        int num2 = Mathf.Min(this.CREATE_CHILD_COUNT, this.category_data.Trophies.Count);
        this.comp_trophy_count_text.text = num1.ToString();
        this.total_trophy_count_text.text = num2.ToString();
      }
      if (!Object.op_Inequality((Object) this.badge, (Object) null))
        return;
      this.badge.SetActive(this.comp_trophy_count > 0);
    }

    public void StartOpen()
    {
      this.move_value = 0.0f;
      this.anim_speed = this.OPEN_SPEED;
      ((Component) this.button_open_rect).get_gameObject().SetActive(true);
      ((Component) this.button_close_rect).get_gameObject().SetActive(false);
      this.start_button_open_size = (float) this.button_open_rect.get_sizeDelta().y;
      this.ChangeState(TrophyRecordPullView.eState.OPEN);
    }

    private void EndOpen()
    {
      this.view_port_rect.set_sizeDelta(new Vector2((float) this.view_port_rect.get_sizeDelta().x, this.target_view_port_size));
      this.root_layout_element.set_minHeight(this.default_min_height + this.target_view_port_size);
      this.button_open_rect.set_sizeDelta(new Vector2(0.0f, (float) (this.view_port_rect.get_sizeDelta().y - this.contents_transform.get_anchoredPosition().y)));
    }

    private void UpdateOpen()
    {
      float num1 = this.anim_speed * Time.get_deltaTime();
      this.move_value += num1;
      if ((double) this.move_value >= (double) this.DEFAULT_OPEN_SPEED_AREA)
        this.anim_speed = this.HI_OPEN_SPEED;
      float num2 = Mathf.Min((float) this.view_port_rect.get_sizeDelta().y + num1, this.target_view_port_size);
      this.view_port_rect.set_sizeDelta(new Vector2((float) this.view_port_rect.get_sizeDelta().x, num2));
      this.root_layout_element.set_minHeight(Mathf.Min(this.root_layout_element.get_minHeight() + num1, this.default_min_height + this.target_view_port_size));
      this.button_open_rect.set_sizeDelta(new Vector2(0.0f, Mathf.Max((float) (this.view_port_rect.get_sizeDelta().y - this.contents_transform.get_anchoredPosition().y), this.start_button_open_size)));
      if ((double) num2 < (double) this.target_view_port_size)
        return;
      this.ChangeState(TrophyRecordPullView.eState.OPENED);
    }

    public void StartClose()
    {
      if (Object.op_Equality((Object) this.contents_parent, (Object) null))
        return;
      this.anim_speed = Mathf.Min(this.target_view_port_size / this.CLOSE_SECOND, this.HI_CLOSE_SPEED);
      this.target_view_port_size = 0.0f;
      this.ChangeState(TrophyRecordPullView.eState.CLOSE);
    }

    private void EndClose()
    {
      this.view_port_rect.set_sizeDelta(new Vector2((float) this.view_port_rect.get_sizeDelta().x, this.target_view_port_size));
      this.root_layout_element.set_minHeight(this.default_min_height + this.target_view_port_size);
      this.button_open_rect.set_sizeDelta(new Vector2(0.0f, this.start_button_open_size));
    }

    private void UpdateClose()
    {
      float num1 = this.anim_speed * Time.get_deltaTime();
      float num2 = Mathf.Max((float) this.view_port_rect.get_sizeDelta().y - num1, this.target_view_port_size);
      this.view_port_rect.set_sizeDelta(new Vector2((float) this.view_port_rect.get_sizeDelta().x, num2));
      this.root_layout_element.set_minHeight(Mathf.Max(this.root_layout_element.get_minHeight() - num1, this.default_min_height + this.target_view_port_size));
      this.button_open_rect.set_sizeDelta(new Vector2(0.0f, Mathf.Max((float) (this.view_port_rect.get_sizeDelta().y - this.contents_transform.get_anchoredPosition().y), this.start_button_open_size)));
      if ((double) num2 > (double) this.target_view_port_size)
        return;
      this.ChangeState(TrophyRecordPullView.eState.CLOSED);
    }

    private void StartClosed()
    {
      this.ClearItems();
      ((Component) this.button_open_rect).get_gameObject().SetActive(false);
      ((Component) this.button_close_rect).get_gameObject().SetActive(true);
    }

    protected void StartCloseImmediate()
    {
      if (Object.op_Equality((Object) this.contents_parent, (Object) null))
        return;
      this.target_view_port_size = 0.0f;
    }

    protected void EndCloseImmediate()
    {
      this.view_port_rect.set_sizeDelta(new Vector2((float) this.view_port_rect.get_sizeDelta().x, this.target_view_port_size));
      this.root_layout_element.set_minHeight(this.default_min_height + this.target_view_port_size);
      this.button_open_rect.set_sizeDelta(new Vector2(0.0f, this.start_button_open_size));
    }

    protected void UpdateCloseImmediate()
    {
      this.ChangeState(TrophyRecordPullView.eState.CLOSED);
    }

    public void CreateContents()
    {
      if (this.original_objects.Length <= 0 || Object.op_Equality((Object) this.contents_parent, (Object) null))
        return;
      float num1 = 0.0f;
      List<GameObject> instances = this.CreateInstances();
      if (instances == null)
        return;
      for (int index = 0; index < instances.Count; ++index)
      {
        instances[index].get_transform().SetParent(this.contents_parent, false);
        RectTransform component = (RectTransform) instances[index].GetComponent<RectTransform>();
        if (Object.op_Inequality((Object) component, (Object) null))
        {
          double num2 = (double) num1;
          Rect rect = component.get_rect();
          // ISSUE: explicit reference operation
          double num3 = (double) ((Rect) @rect).get_height() * ((Transform) this.grid_rect).get_localScale().y;
          num1 = (float) (num2 + num3);
        }
      }
      if (Object.op_Inequality((Object) this.vertical_layout_group, (Object) null))
        num1 += ((HorizontalOrVerticalLayoutGroup) this.vertical_layout_group).get_spacing() * (float) (instances.Count - 1);
      this.target_view_port_size = num1 - (float) ((Component) this.grid_rect).get_transform().get_localPosition().y;
      this.target_view_port_size += this.FRAME_MERGIN;
    }

    private List<GameObject> CreateInstances()
    {
      if (this.category_data == null || this.category_data.Trophies.Count <= 0)
        return (List<GameObject>) null;
      List<GameObject> gameObjectList = new List<GameObject>();
      int createChildCount = this.CREATE_CHILD_COUNT;
      for (int index = 0; index < this.category_data.Trophies.Count && createChildCount != 0; ++index)
      {
        ListItemEvents listItemEvents = this.trophy_list.MakeTrophyPlate(this.category_data.Trophies[index], this.category_data.Trophies[index].IsCompleted);
        if (Object.op_Inequality((Object) listItemEvents, (Object) null))
        {
          --createChildCount;
          this.AddItem(listItemEvents);
          gameObjectList.Add(((Component) listItemEvents).get_gameObject());
          listItemEvents.DisplayRectMergin = new Vector2(0.0f, this.view_mergin);
          listItemEvents.ParentScale = Vector2.op_Implicit(((Transform) this.grid_rect).get_localScale());
          if (MonoSingleton<GameManager>.Instance.IsTutorial() && MonoSingleton<GameManager>.Instance.GetNextTutorialStep() == "ShowMissionFilter" && this.category_data.Trophies[index].Param.iname == "LOGIN_GLTUTOTIAL_01")
          {
            MonoSingleton<GameManager>.Instance.CompleteTutorialStep();
            SGHighlightObject.Instance().highlightedObject = ((Component) listItemEvents).get_gameObject();
            SGHighlightObject.Instance().Highlight(string.Empty, "sg_tut_1.004", (SGHighlightObject.OnActivateCallback) null, EventDialogBubble.Anchors.BottomLeft, true, false, false);
          }
        }
      }
      return gameObjectList;
    }

    public void ChangeState(TrophyRecordPullView.eState _new_state)
    {
      if (this.state == _new_state)
        return;
      switch (this.state)
      {
        case TrophyRecordPullView.eState.OPEN:
          this.EndOpen();
          break;
        case TrophyRecordPullView.eState.CLOSE:
          this.EndClose();
          break;
        case TrophyRecordPullView.eState.CLOSE_IMMEDIATE:
          this.EndCloseImmediate();
          break;
      }
      this.state = _new_state;
      switch (this.state)
      {
        case TrophyRecordPullView.eState.CLOSE_IMMEDIATE:
          this.StartCloseImmediate();
          break;
        case TrophyRecordPullView.eState.CLOSED:
          this.StartClosed();
          break;
      }
    }

    public void OnClickEvent()
    {
      this.trophy_list.SetClickTarget(this);
    }

    public enum eState
    {
      OPEN,
      OPENED,
      CLOSE,
      CLOSE_IMMEDIATE,
      CLOSED,
    }
  }
}
