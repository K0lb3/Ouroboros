// Decompiled with JetBrains decompiler
// Type: SRPG.UnitListRootWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class UnitListRootWindow : FlowWindowBase
  {
    private Dictionary<string, UnitListRootWindow.ListData> m_Dict = new Dictionary<string, UnitListRootWindow.ListData>();
    public const string UNITLIST_KEY = "unitlist";
    public const string PIECELIST_KEY = "piecelist";
    public const string SUPPORTLIST_KEY = "supportlist";
    public const int BTN_GROUP = 1;
    public const int TEXT_GROUP = 2;
    private const float SUPPORT_REFRESH_LOCK_TIME = 10f;
    private UnitListRootWindow.SerializeParam m_Param;
    private SerializeValueList m_ValueList;
    private UnitListWindow m_Root;
    private GameObject m_UnitList;
    private GameObject m_SupportList;
    private GameObject m_PieceList;
    private UnitListWindow.EditType m_EditType;
    private UnitListRootWindow.ContentType m_ContentType;
    private TabMaker m_Tab;
    private bool m_Destroy;
    private UnitListRootWindow.Content.ItemSource m_ContentSource;
    private ContentController m_ContentController;
    private GameObject m_AccessoryRoot;
    private RectTransform[] m_MainSlotLabel;
    private RectTransform[] m_SubSlotLabel;
    private static UnitListRootWindow m_Instance;
    private ContentController m_PieceController;
    private ContentController m_SupportController;
    private SerializeValueList m_SupportValueList;
    private float m_SupportRefreshLock;
    private ContentController m_UnitController;

    public override string name
    {
      get
      {
        return nameof (UnitListRootWindow);
      }
    }

    private GameObject Param_UnitList
    {
      get
      {
        if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.m_UnitList, (UnityEngine.Object) null))
        {
          if (this.m_Param == null || this.m_Param.unitList == null || UnityEngine.Object.op_Equality((UnityEngine.Object) this.m_Param.listParent, (UnityEngine.Object) null))
            return (GameObject) null;
          this.m_UnitList = (GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) AssetManager.Load<GameObject>(this.m_Param.unitList));
          this.m_UnitList.get_transform().SetParent(this.m_Param.listParent.get_transform(), false);
          this.InitializeUnitList();
          ContentNode[] componentsInChildren = (ContentNode[]) this.m_UnitList.GetComponentsInChildren<ContentNode>(true);
          for (int index = 0; index < componentsInChildren.Length; ++index)
            this.m_ValueList.SetField(((UnityEngine.Object) componentsInChildren[index]).get_name().IndexOf("tower", StringComparison.OrdinalIgnoreCase) <= 0 ? "node_unit" : "node_tower", ((Component) componentsInChildren[index]).get_gameObject());
        }
        return this.m_UnitList;
      }
    }

    private GameObject Param_PieceList
    {
      get
      {
        if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.m_PieceList, (UnityEngine.Object) null))
        {
          if (this.m_Param == null || this.m_Param.pieceList == null || UnityEngine.Object.op_Equality((UnityEngine.Object) this.m_Param.listParent, (UnityEngine.Object) null))
            return (GameObject) null;
          this.m_PieceList = (GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) AssetManager.Load<GameObject>(this.m_Param.pieceList));
          this.m_PieceList.get_transform().SetParent(this.m_Param.listParent.get_transform(), false);
          this.InitializePieceList();
          this.m_ValueList.SetField("node_piece", ((Component) this.m_PieceList.GetComponentInChildren<ContentNode>(true)).get_gameObject());
        }
        return this.m_PieceList;
      }
    }

    private GameObject Param_SupportList
    {
      get
      {
        if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.m_SupportList, (UnityEngine.Object) null))
        {
          if (this.m_Param == null || this.m_Param.supportList == null || UnityEngine.Object.op_Equality((UnityEngine.Object) this.m_Param.listParent, (UnityEngine.Object) null))
            return (GameObject) null;
          this.m_SupportList = (GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) AssetManager.Load<GameObject>(this.m_Param.supportList));
          this.m_SupportList.get_transform().SetParent(this.m_Param.listParent.get_transform(), false);
          this.InitializeSupportList();
          this.m_ValueList.SetField("node_support", ((Component) this.m_SupportList.GetComponentInChildren<ContentNode>(true)).get_gameObject());
        }
        return this.m_SupportList;
      }
    }

    public static UnitListRootWindow instance
    {
      get
      {
        return UnitListRootWindow.m_Instance;
      }
    }

    public static bool hasInstance
    {
      get
      {
        if (UnitListRootWindow.m_Instance != null)
        {
          if (UnitListRootWindow.m_Instance.isValid)
            return true;
          UnitListRootWindow.m_Instance = (UnitListRootWindow) null;
        }
        return false;
      }
    }

    public override void Initialize(FlowWindowBase.SerializeParamBase param)
    {
      UnitListRootWindow.m_Instance = this;
      base.Initialize(param);
      this.m_Param = param as UnitListRootWindow.SerializeParam;
      if (this.m_Param == null)
        throw new Exception(this.ToString() + " > Failed serializeParam null.");
      SerializeValueBehaviour childComponent = this.GetChildComponent<SerializeValueBehaviour>("root");
      this.m_ValueList = !UnityEngine.Object.op_Inequality((UnityEngine.Object) childComponent, (UnityEngine.Object) null) ? new SerializeValueList() : childComponent.list;
      this.m_ValueList.SetActive(1, false);
      this.m_ValueList.SetActive(2, false);
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
      if (this.isClosed)
        this.SetActiveChild(false);
      if (this.m_ContentType == UnitListRootWindow.ContentType.SUPPORT)
        this.Update_Support();
      return -1;
    }

    public UnitListRootWindow.ListData GetListData(string key)
    {
      UnitListRootWindow.ListData listData = (UnitListRootWindow.ListData) null;
      this.m_Dict.TryGetValue(key, out listData);
      return listData;
    }

    public UnitListRootWindow.ListData AddListData(string key)
    {
      UnitListRootWindow.ListData listData = new UnitListRootWindow.ListData();
      listData.key = key;
      this.m_Dict.Add(key, listData);
      return listData;
    }

    public void RemoveListDataAll()
    {
      using (Dictionary<string, UnitListRootWindow.ListData>.Enumerator enumerator = this.m_Dict.GetEnumerator())
      {
        while (enumerator.MoveNext())
          enumerator.Current.Value.Delete();
      }
    }

    private string[] GetTabKeys(UnitListRootWindow.Tab[] tabs)
    {
      List<string> stringList = new List<string>();
      for (int index = 0; index < tabs.Length; ++index)
        stringList.Add(tabs[index].ToString());
      return stringList.ToArray();
    }

    private void SetupTab(UnitListRootWindow.Tab[] tabs, UnitListRootWindow.Tab start)
    {
      this.m_Tab = this.m_ValueList.GetComponent<TabMaker>("tab");
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_Tab, (UnityEngine.Object) null))
        return;
      this.m_Tab.Create(this.GetTabKeys(tabs), new Action<GameObject, SerializeValueList>(this.SetupTabNode));
      if (this.m_EditType == UnitListWindow.EditType.SUPPORT)
      {
        int data = this.GetData<int>("data_element", 0);
        if (data != 0)
        {
          start = this.GetTab((EElement) data);
          TabMaker.Info[] infos = this.m_Tab.GetInfos();
          for (int index = 0; index < infos.Length; ++index)
          {
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) infos[index].ev, (UnityEngine.Object) null))
              infos[index].ev.ResetFlag(ButtonEvent.Flag.AUTOLOCK);
            if ((UnitListRootWindow.Tab) infos[index].element.value != start)
              infos[index].SetColor(new Color(0.5f, 0.5f, 0.5f));
            infos[index].interactable = false;
          }
        }
      }
      this.m_Tab.SetOn((Enum) start, true);
    }

    private void SetupTabNode(GameObject gobj, SerializeValueList value)
    {
      TabMaker.Element element = value.GetObject<TabMaker.Element>("element", (TabMaker.Element) null);
      if (element == null)
        return;
      object obj = Enum.Parse(typeof (UnitListRootWindow.Tab), element.key);
      if (obj == null)
        return;
      element.value = (int) obj;
    }

    public UnitListRootWindow.Tab GetCurrentTab()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_Tab, (UnityEngine.Object) null))
      {
        TabMaker.Info onIfno = this.m_Tab.GetOnIfno();
        if (onIfno != null)
          return (UnitListRootWindow.Tab) onIfno.element.value;
      }
      return UnitListRootWindow.Tab.ALL;
    }

    public Vector2 GetCurrentTabAnchore()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_ContentController, (UnityEngine.Object) null))
        return this.m_ContentController.anchoredPosition;
      return Vector2.get_zero();
    }

    public UnitListRootWindow.Tab GetTab(EElement element)
    {
      UnitListRootWindow.Tab tab = UnitListRootWindow.Tab.ALL;
      switch (element)
      {
        case EElement.None:
          tab = UnitListRootWindow.Tab.MAINSUPPORT;
          break;
        case EElement.Fire:
          tab = UnitListRootWindow.Tab.FIRE;
          break;
        case EElement.Water:
          tab = UnitListRootWindow.Tab.WATER;
          break;
        case EElement.Wind:
          tab = UnitListRootWindow.Tab.WIND;
          break;
        case EElement.Thunder:
          tab = UnitListRootWindow.Tab.THUNDER;
          break;
        case EElement.Shine:
          tab = UnitListRootWindow.Tab.LIGHT;
          break;
        case EElement.Dark:
          tab = UnitListRootWindow.Tab.DARK;
          break;
      }
      return tab;
    }

    public EElement GetElement(UnitListRootWindow.Tab tab)
    {
      EElement eelement = EElement.None;
      switch (tab)
      {
        case UnitListRootWindow.Tab.FIRE:
          eelement = EElement.Fire;
          break;
        case UnitListRootWindow.Tab.WATER:
          eelement = EElement.Water;
          break;
        case UnitListRootWindow.Tab.THUNDER:
          eelement = EElement.Thunder;
          break;
        case UnitListRootWindow.Tab.WIND:
          eelement = EElement.Wind;
          break;
        case UnitListRootWindow.Tab.LIGHT:
          eelement = EElement.Shine;
          break;
        case UnitListRootWindow.Tab.DARK:
          eelement = EElement.Dark;
          break;
      }
      return eelement;
    }

    public EElement GetElement()
    {
      UnitListRootWindow.Tab currentTab = this.GetCurrentTab();
      EElement eelement = EElement.None;
      switch (currentTab)
      {
        case UnitListRootWindow.Tab.FIRE:
          eelement = EElement.Fire;
          break;
        case UnitListRootWindow.Tab.WATER:
          eelement = EElement.Water;
          break;
        case UnitListRootWindow.Tab.THUNDER:
          eelement = EElement.Thunder;
          break;
        case UnitListRootWindow.Tab.WIND:
          eelement = EElement.Wind;
          break;
        case UnitListRootWindow.Tab.LIGHT:
          eelement = EElement.Shine;
          break;
        case UnitListRootWindow.Tab.DARK:
          eelement = EElement.Dark;
          break;
      }
      return eelement;
    }

    public void CalcUnit(List<UnitListWindow.Data> list)
    {
      UnitListRootWindow.Tab currentTab = this.GetCurrentTab();
      for (int index = list.Count - 1; index >= 0; --index)
      {
        if ((list[index].tabMask & currentTab) == UnitListRootWindow.Tab.NONE)
          list.RemoveAt(index);
      }
    }

    public void InitializeContentList(UnitListRootWindow.ContentType contentType)
    {
      this.m_Destroy = false;
      this.m_ContentType = contentType;
      switch (this.m_ContentType)
      {
        case UnitListRootWindow.ContentType.UNIT:
          this.m_ContentSource = this.SetupUnitList((UnitListRootWindow.Content.ItemSource) null);
          break;
        case UnitListRootWindow.ContentType.PIECE:
          this.m_ContentSource = this.SetupPieceList((UnitListRootWindow.Content.ItemSource) null);
          break;
        case UnitListRootWindow.ContentType.SUPPORT:
          this.m_ContentSource = this.SetupSupportList((UnitListRootWindow.Content.ItemSource) null);
          break;
      }
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_ContentController, (UnityEngine.Object) null))
        return;
      if (this.m_ContentController.GetNodeCount() == 0)
        this.m_ContentController.Initialize((ContentSource) this.m_ContentSource, Vector2.get_zero());
      else
        this.m_ContentController.SetCurrentSource((ContentSource) this.m_ContentSource);
    }

    public void RefreshContentList()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_ContentController, (UnityEngine.Object) null))
        this.m_ContentController.SetCurrentSource((ContentSource) null);
      switch (this.m_ContentType)
      {
        case UnitListRootWindow.ContentType.UNIT:
          this.SetupUnitList(this.m_ContentSource);
          break;
        case UnitListRootWindow.ContentType.PIECE:
          this.SetupPieceList(this.m_ContentSource);
          break;
        case UnitListRootWindow.ContentType.SUPPORT:
          this.SetupSupportList(this.m_ContentSource);
          break;
      }
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_ContentController, (UnityEngine.Object) null))
        return;
      this.m_ContentController.SetCurrentSource((ContentSource) this.m_ContentSource);
    }

    public void ReleaseContentList()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_ContentController, (UnityEngine.Object) null))
        this.m_ContentController.Release();
      this.m_ContentSource = (UnitListRootWindow.Content.ItemSource) null;
    }

    private void ShowToolTip(string path, UnitData unit)
    {
      if (string.IsNullOrEmpty(path) || unit == null)
        return;
      GameObject root = (GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) AssetManager.Load<GameObject>(this.m_Param.tooltipPrefab));
      DataSource.Bind<UnitData>(root, unit);
      UnitJobDropdown componentInChildren1 = (UnitJobDropdown) root.GetComponentInChildren<UnitJobDropdown>();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) componentInChildren1, (UnityEngine.Object) null))
      {
        ((Component) componentInChildren1).get_gameObject().SetActive(true);
        Selectable component1 = (Selectable) ((Component) componentInChildren1).get_gameObject().GetComponent<Selectable>();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component1, (UnityEngine.Object) null))
          component1.set_interactable(false);
        Image component2 = (Image) ((Component) componentInChildren1).get_gameObject().GetComponent<Image>();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component2, (UnityEngine.Object) null))
          ((Graphic) component2).set_color(new Color(0.5f, 0.5f, 0.5f));
      }
      ArtifactSlots componentInChildren2 = (ArtifactSlots) root.GetComponentInChildren<ArtifactSlots>();
      AbilitySlots componentInChildren3 = (AbilitySlots) root.GetComponentInChildren<AbilitySlots>();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) componentInChildren2, (UnityEngine.Object) null) && UnityEngine.Object.op_Inequality((UnityEngine.Object) componentInChildren3, (UnityEngine.Object) null))
      {
        componentInChildren2.Refresh(false);
        componentInChildren3.Refresh(false);
      }
      ConceptCardSlots componentInChildren4 = (ConceptCardSlots) root.GetComponentInChildren<ConceptCardSlots>();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) componentInChildren4, (UnityEngine.Object) null))
        componentInChildren4.Refresh(false);
      GameParameter.UpdateAll(root);
    }

    public void SetRoot(UnitListWindow root)
    {
      this.m_Root = root;
    }

    public void ClearData()
    {
      List<SerializeValue> list = this.m_ValueList.list;
      for (int index = 0; index < list.Count; ++index)
      {
        if (list[index].key.IndexOf("data_") != -1)
        {
          this.m_ValueList.RemoveFieldAt(index);
          --index;
        }
      }
    }

    public void RemoveData(string key)
    {
      this.m_ValueList.RemoveField(key);
    }

    public void AddData(string key, object value)
    {
      this.m_ValueList.AddObject(key, value);
    }

    public object GetData(string key)
    {
      return this.m_ValueList.GetObject(key);
    }

    public object GetData(string key, object defaultValue)
    {
      return this.m_ValueList.GetObject(key, defaultValue);
    }

    public T GetData<T>(string key)
    {
      return this.m_ValueList.GetObject<T>(key);
    }

    public T GetData<T>(string key, T defaultValue)
    {
      return this.m_ValueList.GetObject<T>(key, defaultValue);
    }

    public UnitListWindow.EditType GetEditType()
    {
      return this.m_EditType;
    }

    public UnitListRootWindow.ContentType GetContentType()
    {
      return this.m_ContentType;
    }

    public RectTransform AttachSlotLabel(UnitListWindow.Data data, ContentNode node)
    {
      RectTransform rectTransform = (RectTransform) null;
      if (!this.GetData<bool>("data_heroOnly", false))
      {
        if (data.partyMainSlot != -1)
        {
          if (this.m_MainSlotLabel != null && data.partyMainSlot >= 0 && data.partyMainSlot < this.m_MainSlotLabel.Length)
            rectTransform = this.m_MainSlotLabel[data.partyMainSlot];
        }
        else if (data.partySubSlot != -1 && this.m_SubSlotLabel != null && (data.partySubSlot >= 0 && data.partySubSlot < this.m_SubSlotLabel.Length))
          rectTransform = this.m_SubSlotLabel[data.partySubSlot];
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) rectTransform, (UnityEngine.Object) null))
        {
          rectTransform.set_anchoredPosition(new Vector2(0.0f, 0.0f));
          ((Component) rectTransform).get_gameObject().SetActive(true);
        }
      }
      return rectTransform;
    }

    public void DettachSlotLabel(RectTransform tr)
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) tr, (UnityEngine.Object) null))
        return;
      tr.set_anchoredPosition(Vector2.get_zero());
      ((Component) tr).get_gameObject().SetActive(false);
    }

    public void RegistAnchorePos(Vector2 pos)
    {
      using (Dictionary<string, UnitListRootWindow.ListData>.Enumerator enumerator = this.m_Dict.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          UnitListRootWindow.ListData listData = enumerator.Current.Value;
          if (listData != null)
            listData.anchorePos = pos;
        }
      }
    }

    public void RegistAnchorePos(string key, Vector2 pos)
    {
      UnitListRootWindow.ListData listData = this.GetListData(key);
      if (listData == null)
        return;
      listData.anchorePos = pos;
    }

    public override int OnActivate(int pinId)
    {
      if (this.m_ContentType == UnitListRootWindow.ContentType.UNIT || pinId == 100 || (pinId == 101 || pinId == 102) || (pinId == 103 || pinId == 105 || pinId == 104))
        return this.OnActivate_Unit(pinId);
      if (this.m_ContentType == UnitListRootWindow.ContentType.PIECE || pinId == 110)
        return this.OnActivate_Piece(pinId);
      if (this.m_ContentType == UnitListRootWindow.ContentType.SUPPORT || pinId == 300)
        return this.OnActivate_Support(pinId);
      return -1;
    }

    private int OnActivate_Base(int pinId)
    {
      switch (pinId)
      {
        case 400:
          this.RegistAnchorePos(Vector2.get_zero());
          this.RefreshContentList();
          if (this.isClosed)
            this.Open();
          ButtonEvent.ResetLock("unitlist");
          break;
        case 410:
          this.RegistAnchorePos(this.m_ContentController.anchoredPosition);
          this.RefreshContentList();
          if (this.isClosed)
            this.Open();
          ButtonEvent.ResetLock("unitlist");
          break;
        case 430:
          this.RemoveListDataAll();
          this.RegistAnchorePos(this.m_ContentController.anchoredPosition);
          this.RefreshContentList();
          if (this.isClosed)
            this.Open();
          ButtonEvent.ResetLock("unitlist");
          break;
        case 490:
          this.m_ContentType = UnitListRootWindow.ContentType.NONE;
          this.RegistAnchorePos(this.m_ContentController.anchoredPosition);
          this.m_Destroy = true;
          this.Close(false);
          ButtonEvent.ResetLock("unitlist");
          return 491;
      }
      return -1;
    }

    protected override int OnOpened()
    {
      return 191;
    }

    protected override int OnClosed()
    {
      return this.m_Destroy ? 492 : -1;
    }

    public static UnitListRootWindow.Tab GetTabMask(UnitListWindow.Data data)
    {
      if (data.param == null)
        return UnitListRootWindow.Tab.ALL;
      UnitParam unitParam = data.param;
      UnitListRootWindow.Tab tab = UnitListRootWindow.Tab.NONE;
      if (data.unit != null && data.unit.IsFavorite)
        tab |= UnitListRootWindow.Tab.FAVORITE;
      if (unitParam != null)
      {
        if (unitParam.element == EElement.Fire)
          tab |= UnitListRootWindow.Tab.FIRE;
        else if (unitParam.element == EElement.Water)
          tab |= UnitListRootWindow.Tab.WATER;
        else if (unitParam.element == EElement.Thunder)
          tab |= UnitListRootWindow.Tab.THUNDER;
        else if (unitParam.element == EElement.Wind)
          tab |= UnitListRootWindow.Tab.WIND;
        else if (unitParam.element == EElement.Shine)
          tab |= UnitListRootWindow.Tab.LIGHT;
        else if (unitParam.element == EElement.Dark)
          tab |= UnitListRootWindow.Tab.DARK;
      }
      return tab;
    }

    private void InitializePieceList()
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_PieceList, (UnityEngine.Object) null))
        return;
      this.m_PieceController = (ContentController) this.m_PieceList.GetComponentInChildren<ContentController>();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_PieceController, (UnityEngine.Object) null))
        this.m_PieceController.SetWork((object) this);
      this.m_PieceList.SetActive(false);
    }

    public UnitListRootWindow.ListData CreatePieceList()
    {
      UnitListRootWindow.ListData listData = this.GetListData("piecelist");
      if (listData == null)
        listData = this.AddListData("piecelist");
      else
        listData.Delete();
      List<ItemParam> items = MonoSingleton<GameManager>.Instance.MasterParam.Items;
      UnitParam[] allUnits = MonoSingleton<GameManager>.Instance.MasterParam.GetAllUnits();
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      for (int index = 0; index < items.Count; ++index)
      {
        ItemParam item = items[index];
        if (item.type == EItemType.UnitPiece)
        {
          UnitParam focus = Array.Find<UnitParam>(allUnits, (Predicate<UnitParam>) (p => p.piece == item.iname));
          if (focus != null && focus.IsSummon() && (player.FindUnitDataByUnitID(focus.iname) == null && focus.CheckAvailable(TimeManager.ServerTime)) && listData.data.FindIndex((Predicate<UnitListWindow.Data>) (prop => prop.param == focus)) == -1)
          {
            UnitListWindow.Data data = new UnitListWindow.Data(focus);
            if (data.unlockable)
              listData.data.Add(data);
          }
        }
      }
      for (int index = 0; index < listData.data.Count; ++index)
        listData.data[index].sortPriority = (long) index;
      listData.data.Sort((Comparison<UnitListWindow.Data>) ((a, b) =>
      {
        int num1 = b.pieceAmount - a.pieceAmount;
        if (num1 != 0)
          return num1;
        int num2 = (int) b.param.rare - (int) a.param.rare;
        if (num2 != 0)
          return num2;
        return a.sortPriority.CompareTo(b.sortPriority);
      }));
      listData.calcData.AddRange((IEnumerable<UnitListWindow.Data>) listData.data);
      listData.isValid = true;
      return listData;
    }

    private UnitListRootWindow.Content.ItemSource SetupPieceList(UnitListRootWindow.Content.ItemSource source)
    {
      bool flag = false;
      UnitListRootWindow.ListData listData1 = this.GetListData("unitlist");
      UnitListRootWindow.ListData listData2 = this.GetListData("piecelist");
      if (listData2 == null || !listData2.isValid || source == null)
      {
        listData2 = this.CreatePieceList();
        flag = true;
      }
      if (source == null)
      {
        source = new UnitListRootWindow.Content.ItemSource();
        UnitListRootWindow.Tab start = listData1 == null || listData1.selectTab == UnitListRootWindow.Tab.NONE ? UnitListRootWindow.Tab.ALL : listData1.selectTab;
        if (start == UnitListRootWindow.Tab.FAVORITE)
          start = UnitListRootWindow.Tab.ALL;
        this.SetupTab(new UnitListRootWindow.Tab[7]
        {
          UnitListRootWindow.Tab.ALL,
          UnitListRootWindow.Tab.FIRE,
          UnitListRootWindow.Tab.WATER,
          UnitListRootWindow.Tab.THUNDER,
          UnitListRootWindow.Tab.WIND,
          UnitListRootWindow.Tab.LIGHT,
          UnitListRootWindow.Tab.DARK
        }, start);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Param_PieceList, (UnityEngine.Object) null))
        {
          ContentNode component = this.m_ValueList.GetComponent<ContentNode>("node_piece");
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
          {
            this.m_ContentController = this.m_PieceController;
            this.m_ContentController.m_Node = component;
            ((Component) this.m_ContentController).get_gameObject().SetActive(true);
          }
          this.Param_PieceList.SetActive(true);
        }
        this.m_ValueList.SetActive(1, false);
        this.m_ValueList.SetActive(2, false);
        this.m_ValueList.SetActive("btn_close", true);
        this.m_ValueList.SetActive("desc_piece", true);
        listData1.anchorePos = this.m_ContentController.anchoredPosition;
        flag = true;
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_UnitList, (UnityEngine.Object) null))
        this.m_UnitList.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_SupportList, (UnityEngine.Object) null))
        this.m_SupportList.SetActive(false);
      if (listData2 != null)
      {
        if (flag)
          listData2.RefreshTabMask();
        listData1.selectTab = this.GetCurrentTab();
        listData2.calcData.Clear();
        listData2.calcData.AddRange((IEnumerable<UnitListWindow.Data>) listData2.data);
        this.CalcUnit(listData2.calcData);
        for (int index = 0; index < listData2.calcData.Count; ++index)
        {
          UnitListWindow.Data data = listData2.calcData[index];
          if (data != null)
          {
            UnitListRootWindow.Content.ItemSource.ItemParam itemParam = new UnitListRootWindow.Content.ItemSource.ItemParam(this.m_Root, data);
            if (itemParam != null && itemParam.IsValid())
              source.Add(itemParam);
          }
        }
        source.AnchorePos(listData2.anchorePos);
      }
      return source;
    }

    private int OnActivate_Piece(int pinId)
    {
      switch (pinId)
      {
        case 110:
          this.InitializeContentList(UnitListRootWindow.ContentType.PIECE);
          this.Open();
          break;
        case 400:
          return this.OnActivate_Base(pinId);
        case 410:
          return this.OnActivate_Base(pinId);
        case 420:
          SerializeValueList currentValue = FlowNode_ButtonEvent.currentValue as SerializeValueList;
          if (currentValue != null)
          {
            UnitParam dataSource = currentValue.GetDataSource<UnitParam>("_self");
            if (dataSource != null)
            {
              GlobalVars.UnlockUnitID = dataSource.iname;
              return 423;
            }
            break;
          }
          break;
        case 430:
          this.RemoveListDataAll();
          return this.OnActivate_Base(pinId);
        case 490:
          this.InitializeContentList(UnitListRootWindow.ContentType.UNIT);
          this.Open();
          break;
      }
      return -1;
    }

    private void InitializeSupportList()
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_SupportList, (UnityEngine.Object) null))
        return;
      this.m_SupportController = (ContentController) this.m_SupportList.GetComponentInChildren<ContentController>();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_SupportController, (UnityEngine.Object) null))
        this.m_SupportController.SetWork((object) this);
      this.m_SupportList.SetActive(false);
      SerializeValueBehaviour component = (SerializeValueBehaviour) this.m_SupportList.GetComponent<SerializeValueBehaviour>();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
        this.m_SupportValueList = component.list;
      else
        this.m_SupportValueList = new SerializeValueList();
    }

    private void Update_Support()
    {
      if ((double) this.m_SupportRefreshLock <= 0.0)
        return;
      if ((double) (Time.get_realtimeSinceStartup() - this.m_SupportRefreshLock) > 10.0)
      {
        this.m_SupportRefreshLock = 0.0f;
        this.m_ValueList.SetInteractable("btn_refresh", true);
      }
      else
        this.m_ValueList.SetInteractable("btn_refresh", false);
    }

    private string GetSupportListKey(EElement element)
    {
      return "supportlist_" + element.ToString();
    }

    private UnitListRootWindow.ListData GetSupportList(EElement element)
    {
      return this.GetListData(this.GetSupportListKey(element));
    }

    private UnitListRootWindow.ListData GetSupportList(FlowNode_ReqSupportList.SupportList support)
    {
      return this.GetSupportList(support == null ? EElement.None : support.m_Element);
    }

    public UnitListRootWindow.ListData CreateSupportList(EElement element)
    {
      UnitListRootWindow.ListData listData = this.GetSupportList(element);
      if (listData == null)
      {
        listData = this.AddListData(this.GetSupportListKey(element));
        listData.selectTab = this.GetTab(element);
      }
      else
        listData.Delete();
      FlowNode_ReqSupportList.SupportList response = listData.response as FlowNode_ReqSupportList.SupportList;
      if (response != null && response.m_SupportData != null && element == response.m_Element)
      {
        for (int index = 0; index < response.m_SupportData.Length; ++index)
        {
          if (response.m_SupportData[index] != null)
            listData.data.Add(new UnitListWindow.Data(response.m_SupportData[index]));
        }
      }
      SupportData[] data1 = this.GetData<SupportData[]>("data_support");
      int data2 = this.GetData<int>("data_party_index", -1);
      if (data1 != null)
      {
        SupportData sup_2 = data1[data2];
        if (sup_2 != null)
        {
          UnitListWindow.Data data3 = new UnitListWindow.Data("empty");
          data3.RefreshPartySlotPriority();
          listData.data.Insert(0, data3);
          for (int index = 0; index < listData.data.Count; ++index)
            listData.data[index].partySelect = this.IsSameSupportUnit(listData.data[index].support, sup_2);
        }
      }
      QuestParam data4 = this.GetData<QuestParam>("data_quest", (QuestParam) null);
      if (data4 != null)
      {
        for (int index1 = 0; index1 < listData.data.Count; ++index1)
        {
          UnitListWindow.Data data3 = listData.data[index1];
          if (data3.unit != null)
          {
            bool flag = data4.IsAvailableUnit(data3.unit);
            if (data4.type == QuestTypes.Ordeal)
            {
              int index2;
              if (this.FindIncludingTeamIndexFromSupport(data1, data3.support, out index2))
              {
                if (index2 != data2)
                  flag = false;
                data3.partyIndex = index2;
              }
              else
                data3.partyIndex = -1;
            }
            data3.interactable = flag;
          }
        }
      }
      listData.calcData.AddRange((IEnumerable<UnitListWindow.Data>) listData.data);
      listData.isValid = true;
      return listData;
    }

    private bool IsSameSupportUnit(SupportData sup_1, SupportData sup_2)
    {
      return sup_1 != null && sup_2.Unit != null && (sup_1.FUID == sup_2.FUID && sup_1.UnitID == sup_2.UnitID) && sup_1.Unit.SupportElement == sup_2.Unit.SupportElement;
    }

    private bool FindIncludingTeamIndexFromSupport(SupportData[] supports, SupportData target, out int index)
    {
      index = 0;
      for (int index1 = 0; index1 < supports.Length; ++index1)
      {
        SupportData support = supports[index1];
        if (support != null && this.IsSameSupportUnit(support, target))
        {
          index = index1;
          return true;
        }
      }
      return false;
    }

    private UnitListRootWindow.Content.ItemSource SetupSupportList(UnitListRootWindow.Content.ItemSource source)
    {
      bool flag = false;
      EElement element = this.GetElement(this.GetCurrentTab());
      UnitListRootWindow.ListData supportList1 = this.GetSupportList(element);
      if (supportList1 == null || !supportList1.isValid || source == null)
      {
        supportList1 = this.CreateSupportList(element);
        flag = true;
      }
      UnitListRootWindow.ListData supportList2 = this.GetSupportList(EElement.None);
      if (supportList2 == null || !supportList2.isValid)
        supportList2 = this.CreateSupportList(EElement.None);
      this.RemoveData("data_supportres");
      if (source == null)
      {
        source = new UnitListRootWindow.Content.ItemSource();
        this.SetupTab(new UnitListRootWindow.Tab[7]
        {
          UnitListRootWindow.Tab.MAINSUPPORT,
          UnitListRootWindow.Tab.FIRE,
          UnitListRootWindow.Tab.WATER,
          UnitListRootWindow.Tab.THUNDER,
          UnitListRootWindow.Tab.WIND,
          UnitListRootWindow.Tab.LIGHT,
          UnitListRootWindow.Tab.DARK
        }, supportList2 != null ? supportList2.selectTab : UnitListRootWindow.Tab.MAINSUPPORT);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Param_SupportList, (UnityEngine.Object) null))
        {
          ContentNode component = this.m_ValueList.GetComponent<ContentNode>("node_support");
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
          {
            this.m_ContentController = this.m_SupportController;
            this.m_ContentController.m_Node = component;
            ((Component) this.m_ContentController).get_gameObject().SetActive(true);
          }
          this.Param_SupportList.SetActive(true);
        }
        this.m_ValueList.SetActive(1, false);
        this.m_ValueList.SetActive(2, false);
        this.m_ValueList.SetActive("btn_close", true);
        this.m_ValueList.SetActive("btn_refresh", true);
        this.m_ValueList.SetActive("btn_help_support", true);
        flag = true;
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_UnitList, (UnityEngine.Object) null))
        this.m_UnitList.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_PieceList, (UnityEngine.Object) null))
        this.m_PieceList.SetActive(false);
      if (supportList1 != null)
      {
        if (flag)
          supportList1.RefreshTabMask();
        if (supportList2 != null)
          supportList2.selectTab = this.GetCurrentTab();
        supportList1.selectTab = this.GetCurrentTab();
        supportList1.calcData.Clear();
        supportList1.calcData.AddRange((IEnumerable<UnitListWindow.Data>) supportList1.data);
        this.CalcUnit(supportList1.calcData);
        int num = 0;
        for (int index = 0; index < supportList1.calcData.Count; ++index)
        {
          UnitListWindow.Data data = supportList1.calcData[index];
          if (data != null)
          {
            UnitListRootWindow.Content.ItemSource.ItemParam itemParam = new UnitListRootWindow.Content.ItemSource.ItemParam(this.m_Root, data);
            if (itemParam != null && itemParam.IsValid())
              num = source.Add(itemParam);
          }
        }
        source.AnchorePos(supportList2 == null ? Vector2.get_zero() : supportList2.anchorePos);
        if (num == 0)
          this.m_ValueList.SetActive("text_nosupport", true);
        else
          this.m_ValueList.SetActive("text_nosupport", false);
      }
      return source;
    }

    private int GetOutputSupportWebApiPin(EElement element, bool isForce)
    {
      this.m_SupportValueList.SetField(nameof (element), (int) element);
      return isForce ? 481 : 480;
    }

    private int OnActivate_Support(int pinId)
    {
      switch (pinId)
      {
        case 300:
          this.InitializeContentList(UnitListRootWindow.ContentType.SUPPORT);
          return this.OnActivate_Support(400);
        case 400:
          EElement element1 = this.GetElement(this.GetCurrentTab());
          UnitListRootWindow.ListData supportList = this.GetSupportList(element1);
          if (supportList != null)
            supportList.Delete();
          if (supportList != null && supportList.response != null)
            return this.OnActivate_Base(pinId);
          this.m_ValueList.SetInteractable("btn_refresh", false);
          return this.GetOutputSupportWebApiPin(element1, true);
        case 410:
          return this.OnActivate_Base(pinId);
        case 419:
          if (!string.IsNullOrEmpty(this.m_Param.tooltipPrefab))
          {
            SerializeValueList currentValue = FlowNode_ButtonEvent.currentValue as SerializeValueList;
            if (currentValue != null)
            {
              SupportData dataSource = currentValue.GetDataSource<SupportData>("_self");
              if (dataSource != null)
                this.ShowToolTip(this.m_Param.tooltipPrefab, dataSource.Unit);
            }
          }
          return 429;
        case 420:
          SerializeValueList currentValue1 = FlowNode_ButtonEvent.currentValue as SerializeValueList;
          if (currentValue1 != null)
          {
            this.RegistAnchorePos(this.m_ContentController.anchoredPosition);
            return currentValue1.GetDataSource<SupportData>("_self") != null ? 426 : 427;
          }
          break;
        case 430:
          EElement element2 = this.GetElement(this.GetCurrentTab());
          UnitListRootWindow.ListData listData = this.GetSupportList(element2) ?? this.CreateSupportList(element2);
          listData.Delete();
          listData.response = (object) this.GetData<FlowNode_ReqSupportList.SupportList>("data_supportres");
          this.RemoveData("data_supportres");
          this.RegistAnchorePos(this.m_ContentController.anchoredPosition);
          this.RefreshContentList();
          this.m_SupportRefreshLock = Time.get_realtimeSinceStartup();
          if (this.isClosed)
            this.Open();
          ButtonEvent.ResetLock("unitlist");
          break;
        case 440:
          this.m_ValueList.SetInteractable("btn_refresh", false);
          return this.GetOutputSupportWebApiPin(this.GetElement(this.GetCurrentTab()), false);
        case 490:
          return this.OnActivate_Base(pinId);
      }
      return -1;
    }

    private void InitializeUnitList()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.m_UnitList, (UnityEngine.Object) null))
        return;
      this.m_UnitController = (ContentController) this.m_UnitList.GetComponentInChildren<ContentController>();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_UnitController, (UnityEngine.Object) null))
      {
        Transform transform = ((Component) this.m_UnitController).get_transform();
        for (int index = 0; index < transform.get_childCount(); ++index)
          ((Component) transform.GetChild(index)).get_gameObject().SetActive(false);
        this.m_UnitController.SetWork((object) this);
      }
      this.m_UnitList.SetActive(false);
      RectTransform rectTransform = Array.Find<RectTransform>((RectTransform[]) this.m_UnitList.GetComponentsInChildren<RectTransform>(), (Predicate<RectTransform>) (mono => ((UnityEngine.Object) mono).get_name() == "accessory"));
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) rectTransform, (UnityEngine.Object) null))
        return;
      this.m_AccessoryRoot = ((Component) rectTransform).get_gameObject();
      List<RectTransform> rectTransformList = new List<RectTransform>();
      this.SetActiveChild(this.m_AccessoryRoot, false);
      rectTransformList.Add(this.GetChildComponent<RectTransform>(this.m_AccessoryRoot, "party_leader"));
      rectTransformList.Add(this.GetChildComponent<RectTransform>(this.m_AccessoryRoot, "party_main2"));
      rectTransformList.Add(this.GetChildComponent<RectTransform>(this.m_AccessoryRoot, "party_main3"));
      rectTransformList.Add(this.GetChildComponent<RectTransform>(this.m_AccessoryRoot, "party_main4"));
      rectTransformList.Add(this.GetChildComponent<RectTransform>(this.m_AccessoryRoot, "party_main5"));
      this.m_MainSlotLabel = rectTransformList.ToArray();
      rectTransformList.Clear();
      rectTransformList.Add(this.GetChildComponent<RectTransform>(this.m_AccessoryRoot, "party_sub1"));
      rectTransformList.Add(this.GetChildComponent<RectTransform>(this.m_AccessoryRoot, "party_sub2"));
      this.m_SubSlotLabel = rectTransformList.ToArray();
    }

    public UnitListRootWindow.ListData CreateUnitList(UnitListWindow.EditType editType, UnitData[] units)
    {
      UnitListRootWindow.ListData listData = this.GetListData("unitlist");
      if (listData == null)
        listData = this.AddListData("unitlist");
      else
        listData.Delete();
      if (units == null)
        units = MonoSingleton<GameManager>.Instance.Player.Units.ToArray();
      if (units != null)
      {
        for (int index = 0; index < units.Length; ++index)
        {
          if (units[index] != null)
            listData.data.Add(new UnitListWindow.Data(units[index]));
        }
        switch (editType)
        {
          case UnitListWindow.EditType.PARTY:
          case UnitListWindow.EditType.PARTY_TOWER:
            PartyEditData[] data1 = this.GetData<PartyEditData[]>("data_party", (PartyEditData[]) null);
            int data2 = this.GetData<int>("data_party_index", -1);
            int data3 = this.GetData<int>("data_slot", -1);
            QuestParam data4 = this.GetData<QuestParam>("data_quest", (QuestParam) null);
            PartyEditData partyEditData = data1?[data2];
            if (partyEditData != null && data3 != -1 && data3 < partyEditData.PartyData.MAX_UNIT)
            {
              int mainMemberCount = partyEditData.GetMainMemberCount();
              bool flag1 = partyEditData.IsSubSlot(data3);
              for (int slotNo = 0; slotNo < partyEditData.PartyData.MAX_UNIT; ++slotNo)
              {
                UnitData unit = partyEditData.Units[slotNo];
                if (unit != null)
                {
                  UnitListWindow.Data data5 = listData.data.Find((Predicate<UnitListWindow.Data>) (prop =>
                  {
                    if (prop.unit != null)
                      return prop.unit.UnitID == unit.UnitID;
                    return false;
                  }));
                  if (data5 != null)
                  {
                    if (slotNo == 0 && flag1 && (mainMemberCount <= 1 && partyEditData.Units[data3] == null))
                    {
                      listData.data.Remove(data5);
                    }
                    else
                    {
                      if (partyEditData.IsSubSlot(slotNo))
                        data5.partySubSlot = partyEditData.GetSubSlotNum(data5.GetUniq());
                      else
                        data5.partyMainSlot = partyEditData.GetMainSlotNum(data5.GetUniq());
                      data5.partySelect = data3 == slotNo;
                      data5.RefreshPartySlotPriority();
                    }
                  }
                }
              }
              if (partyEditData.Units[data3] != null && (data3 != 0 || this.GetData<bool>("data_heroOnly", false)))
              {
                UnitListWindow.Data data5 = new UnitListWindow.Data("empty");
                data5.RefreshPartySlotPriority();
                listData.data.Insert(0, data5);
              }
              if (data4 != null)
              {
                for (int index1 = 0; index1 < listData.data.Count; ++index1)
                {
                  UnitListWindow.Data data5 = listData.data[index1];
                  if (data5.unit != null)
                  {
                    bool flag2 = data4.IsUnitAllowed(data5.unit) || data5.partyMainSlot != -1 || data5.partySubSlot != -1;
                    string error = (string) null;
                    bool flag3 = flag2 & data4.IsEntryQuestCondition(data5.unit, ref error);
                    if (data4.type == QuestTypes.Ordeal)
                    {
                      int index2;
                      if (this.FindIncludingTeamIndexFromParty(data1, data5.unit.UniqueID, out index2))
                      {
                        if (index2 != data2)
                          flag3 = false;
                        data5.partyIndex = index2;
                      }
                      else
                        data5.partyIndex = -1;
                    }
                    data5.interactable = flag3;
                  }
                }
                break;
              }
              break;
            }
            break;
          case UnitListWindow.EditType.EQUIP:
          case UnitListWindow.EditType.SHOP_EQUIP:
            for (int index = 0; index < listData.data.Count; ++index)
            {
              UnitListWindow.Data data5 = listData.data[index];
              if (data5.unit != null)
                data5.interactable = data5.unit.CheckEnableEnhanceEquipment();
            }
            break;
          case UnitListWindow.EditType.SUPPORT:
            UnitData data6 = this.GetData<UnitData>("data_unit", (UnitData) null);
            if (this.GetData<int>("data_element", 0) != 0 && data6 != null)
            {
              UnitListWindow.Data data5 = new UnitListWindow.Data("empty");
              data5.RefreshPartySlotPriority();
              listData.data.Insert(0, data5);
            }
            for (int index = 0; index < listData.data.Count; ++index)
            {
              UnitListWindow.Data data5 = listData.data[index];
              data5.partySelect = data6 != null && data5.unit == data6;
            }
            break;
        }
      }
      listData.calcData.AddRange((IEnumerable<UnitListWindow.Data>) listData.data);
      listData.isValid = true;
      return listData;
    }

    private bool FindIncludingTeamIndexFromParty(PartyEditData[] partyEditDataAry, long uniqueID, out int index)
    {
      index = 0;
      for (int index1 = 0; index1 < partyEditDataAry.Length; ++index1)
      {
        foreach (UnitData unit in partyEditDataAry[index1].Units)
        {
          if (unit != null && unit.UniqueID == uniqueID)
          {
            index = index1;
            return true;
          }
        }
      }
      return false;
    }

    private UnitListRootWindow.Content.ItemSource SetupUnitList(UnitListRootWindow.Content.ItemSource source)
    {
      bool flag = false;
      UnitListRootWindow.TabRegister data1 = this.GetData<UnitListRootWindow.TabRegister>("data_register", (UnitListRootWindow.TabRegister) null);
      this.m_EditType = this.GetData<UnitListWindow.EditType>("data_edit", UnitListWindow.EditType.DEFAULT);
      UnitListRootWindow.ListData listData = this.GetListData("unitlist");
      if (listData == null || !listData.isValid || source == null)
      {
        listData = this.CreateUnitList(this.m_EditType, this.GetData<UnitData[]>("data_units"));
        flag = true;
      }
      if (source == null)
      {
        source = new UnitListRootWindow.Content.ItemSource();
        UnitListRootWindow.Tab start = listData.selectTab != UnitListRootWindow.Tab.NONE ? listData.selectTab : UnitListRootWindow.Tab.ALL;
        if (data1 != null)
          start = data1.tab;
        this.SetupTab(new UnitListRootWindow.Tab[8]
        {
          UnitListRootWindow.Tab.ALL,
          UnitListRootWindow.Tab.FAVORITE,
          UnitListRootWindow.Tab.FIRE,
          UnitListRootWindow.Tab.WATER,
          UnitListRootWindow.Tab.THUNDER,
          UnitListRootWindow.Tab.WIND,
          UnitListRootWindow.Tab.LIGHT,
          UnitListRootWindow.Tab.DARK
        }, start);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Param_UnitList, (UnityEngine.Object) null))
        {
          ContentNode component1 = this.m_ValueList.GetComponent<ContentNode>(this.m_EditType != UnitListWindow.EditType.PARTY_TOWER ? "node_unit" : "node_tower");
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component1, (UnityEngine.Object) null))
          {
            SerializeValueBehaviour component2 = (SerializeValueBehaviour) ((Component) component1).GetComponent<SerializeValueBehaviour>();
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component2, (UnityEngine.Object) null))
            {
              component2.list.SetActive("empty", false);
              component2.list.SetActive("body", false);
              component2.list.SetActive("select", false);
            }
            this.m_ContentController = this.m_UnitController;
            this.m_ContentController.m_Node = component1;
            ((Component) this.m_ContentController).get_gameObject().SetActive(true);
          }
          this.Param_UnitList.SetActive(true);
        }
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_PieceList, (UnityEngine.Object) null))
          this.m_PieceList.SetActive(false);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_SupportList, (UnityEngine.Object) null))
          this.m_SupportList.SetActive(false);
        this.m_ValueList.SetActive(1, false);
        this.m_ValueList.SetActive(2, false);
        this.m_ValueList.SetActive("btn_sort", true);
        this.m_ValueList.SetActive("btn_filter", true);
        if (this.m_EditType == UnitListWindow.EditType.PARTY || this.m_EditType == UnitListWindow.EditType.PARTY_TOWER)
        {
          this.m_ValueList.SetActive("btn_close", true);
          MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
          if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) instance, (UnityEngine.Object) null) || !instance.IsConnectedInRoom() || GlobalVars.SelectedMultiPlayRoomType != JSON_MyPhotonRoomParam.EType.RAID)
            this.m_ValueList.SetActive("btn_attackable", true);
        }
        else if (this.m_EditType == UnitListWindow.EditType.EQUIP)
          this.m_ValueList.SetActive("btn_backhome", true);
        else if (this.m_EditType == UnitListWindow.EditType.SHOP_EQUIP)
          this.m_ValueList.SetActive("btn_close", true);
        else if (this.m_EditType == UnitListWindow.EditType.SUPPORT)
        {
          this.m_ValueList.SetActive("btn_close", true);
        }
        else
        {
          this.m_ValueList.SetActive("btn_backhome", true);
          this.m_ValueList.SetActive("btn_help", true);
          this.m_ValueList.SetActive("btn_piece", true);
        }
        flag = true;
      }
      if (listData != null)
      {
        if (flag)
          listData.Refresh();
        listData.selectTab = this.GetCurrentTab();
        listData.calcData.Clear();
        listData.calcData.AddRange((IEnumerable<UnitListWindow.Data>) listData.data);
        this.CalcUnit(listData.calcData);
        if (this.m_Root.filterWindow != null)
          this.m_Root.filterWindow.CalcUnit(listData.calcData);
        if (this.m_Root.sortWindow != null)
          this.m_Root.sortWindow.CalcUnit(listData.calcData);
        if ((this.m_EditType == UnitListWindow.EditType.PARTY || this.m_EditType == UnitListWindow.EditType.PARTY_TOWER) && this.m_ValueList.GetUIOn("btn_attackable"))
        {
          QuestParam data2 = this.GetData<QuestParam>("data_quest", (QuestParam) null);
          if (data2 != null)
          {
            string empty = string.Empty;
            int data3 = this.GetData<int>("data_party_index", -1);
            for (int index = 0; index < listData.calcData.Count; ++index)
            {
              if (listData.calcData[index].unit != null)
              {
                if (!data2.IsEntryQuestCondition(listData.calcData[index].unit, ref empty))
                {
                  listData.calcData.RemoveAt(index);
                  --index;
                }
                else if (data2.type == QuestTypes.Ordeal && listData.calcData[index].partyIndex >= 0 && listData.calcData[index].partyIndex != data3)
                {
                  listData.calcData.RemoveAt(index);
                  --index;
                }
              }
            }
          }
        }
        if (this.m_EditType == UnitListWindow.EditType.PARTY || this.m_EditType == UnitListWindow.EditType.PARTY_TOWER || this.m_EditType == UnitListWindow.EditType.SUPPORT)
          SortUtility.StableSort<UnitListWindow.Data>(listData.calcData, (Comparison<UnitListWindow.Data>) ((p1, p2) => p1.subSortPriority.CompareTo(p2.subSortPriority)));
        long num1 = data1 == null ? 0L : data1.forcus;
        int index1 = -1;
        int num2 = 0;
        for (int index2 = 0; index2 < listData.calcData.Count; ++index2)
        {
          UnitListWindow.Data data2 = listData.calcData[index2];
          if (data2 != null)
          {
            UnitListRootWindow.Content.ItemSource.ItemParam itemParam = new UnitListRootWindow.Content.ItemSource.ItemParam(this.m_Root, data2);
            if (itemParam != null && itemParam.IsValid())
            {
              if (num1 == data2.GetUniq())
                index1 = num2;
              num2 = source.Add(itemParam);
            }
          }
        }
        if (data1 != null)
        {
          source.AnchorePos(data1.anchorePos);
          source.ForcusIndex(index1);
        }
        else
          source.AnchorePos(listData.anchorePos);
        if (num2 == 0)
          this.m_ValueList.SetActive("text_nounit", true);
        else
          this.m_ValueList.SetActive("text_nounit", false);
        this.RemoveData("data_register");
      }
      return source;
    }

    private int OnActivate_Unit(int pinId)
    {
      switch (pinId)
      {
        case 100:
        case 101:
        case 102:
        case 103:
        case 104:
        case 105:
          if (!this.m_ValueList.HasField("data_edit"))
          {
            UnitListWindow.EditType editType = UnitListWindow.EditType.DEFAULT;
            switch (pinId)
            {
              case 101:
                editType = UnitListWindow.EditType.PARTY;
                break;
              case 102:
                editType = UnitListWindow.EditType.PARTY_TOWER;
                break;
              case 103:
                editType = UnitListWindow.EditType.EQUIP;
                break;
              case 104:
                editType = UnitListWindow.EditType.SUPPORT;
                break;
              case 105:
                editType = UnitListWindow.EditType.SHOP_EQUIP;
                break;
            }
            this.AddData("data_edit", (object) editType);
            if (pinId == 104)
              this.AddData("data_unit", (object) this.m_ValueList.GetDataSource<UnitData>("_self"));
          }
          this.InitializeContentList(UnitListRootWindow.ContentType.UNIT);
          this.Open();
          break;
        case 110:
          this.InitializeContentList(UnitListRootWindow.ContentType.PIECE);
          this.Open();
          break;
        case 400:
          return this.OnActivate_Base(pinId);
        case 410:
          return this.OnActivate_Base(pinId);
        case 419:
          if (this.m_EditType != UnitListWindow.EditType.DEFAULT && !string.IsNullOrEmpty(this.m_Param.tooltipPrefab))
          {
            SerializeValueList currentValue = FlowNode_ButtonEvent.currentValue as SerializeValueList;
            if (currentValue != null)
            {
              UnitData dataSource = currentValue.GetDataSource<UnitData>("_self");
              if (dataSource != null)
                this.ShowToolTip(this.m_Param.tooltipPrefab, dataSource);
            }
          }
          return 429;
        case 420:
          SerializeValueList currentValue1 = FlowNode_ButtonEvent.currentValue as SerializeValueList;
          if (currentValue1 != null)
          {
            this.RegistAnchorePos(this.m_ContentController.anchoredPosition);
            UnitData dataSource = currentValue1.GetDataSource<UnitData>("_self");
            if (dataSource == null)
              return 422;
            UnitListRootWindow.ListData listData = this.GetListData("unitlist");
            if (listData != null)
            {
              listData.selectUniqueID = dataSource.UniqueID;
              if (this.m_EditType == UnitListWindow.EditType.DEFAULT || this.m_EditType == UnitListWindow.EditType.EQUIP || (this.m_EditType == UnitListWindow.EditType.SHOP_EQUIP || this.m_EditType == UnitListWindow.EditType.SUPPORT))
              {
                GlobalVars.SelectedUnitUniqueID.Set(dataSource.UniqueID);
                GlobalVars.SelectedUnitJobIndex.Set(dataSource.JobIndex);
              }
              return 421;
            }
            break;
          }
          break;
        case 430:
          this.RemoveListDataAll();
          return this.OnActivate_Base(pinId);
        case 490:
          return this.OnActivate_Base(pinId);
      }
      return -1;
    }

    public enum ContentType
    {
      NONE,
      UNIT,
      PIECE,
      SUPPORT,
    }

    public enum Tab
    {
      NONE = 0,
      FAVORITE = 1,
      FIRE = 2,
      WATER = 4,
      THUNDER = 8,
      WIND = 16, // 0x00000010
      LIGHT = 32, // 0x00000020
      DARK = 64, // 0x00000040
      MAINSUPPORT = 128, // 0x00000080
      ALL = 65535, // 0x0000FFFF
    }

    public static class Content
    {
      public static UnitListRootWindow.Content.ItemAccessor clickItem;

      public class ItemAccessor
      {
        private UnitListRootWindow m_RootWindow;
        private UnitListSortWindow m_SortWindow;
        private UnitListRootWindow.ContentType m_ContentType;
        private ContentNode m_Node;
        private UnitListWindow.Data m_Param;
        private DataSource m_DataSource;
        private SerializeValueBehaviour m_Value;
        private SortBadge m_SortBadge;
        private RectTransform m_SlotLabel;

        public ContentNode node
        {
          get
          {
            return this.m_Node;
          }
        }

        public UnitListWindow.Data param
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
            return this.m_Param != null;
          }
        }

        public void Setup(UnitListWindow window, UnitListWindow.Data param)
        {
          this.m_RootWindow = window.rootWindow;
          this.m_SortWindow = window.sortWindow;
          this.m_ContentType = this.m_RootWindow.GetContentType();
          this.m_Param = param;
        }

        public void Release()
        {
          this.Clear();
          this.m_Node = (ContentNode) null;
          this.m_Param = (UnitListWindow.Data) null;
        }

        public void Bind(ContentNode node)
        {
          this.m_Node = node;
          this.m_DataSource = DataSource.Create(((Component) node).get_gameObject());
          this.m_DataSource.Add(typeof (UnitParam), (object) this.m_Param.param);
          if (this.m_Param.unit != null)
            this.m_DataSource.Add(typeof (UnitData), (object) this.m_Param.unit);
          if (this.m_Param.support != null)
            this.m_DataSource.Add(typeof (SupportData), (object) this.m_Param.support);
          this.m_Value = (SerializeValueBehaviour) ((Component) this.m_Node).GetComponent<SerializeValueBehaviour>();
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_Value, (UnityEngine.Object) null))
          {
            this.m_Value.list.SetActive(1, false);
            if (!string.IsNullOrEmpty(this.m_Param.body))
              this.m_Value.list.SetActive(this.m_Param.body, true);
            else
              this.m_Value.list.SetActive("body", true);
            this.m_Value.list.SetActive("select", this.m_Param.partySelect);
            if (this.m_ContentType == UnitListRootWindow.ContentType.UNIT || this.m_ContentType == UnitListRootWindow.ContentType.UNIT)
            {
              if (this.m_RootWindow != null)
              {
                UnitListWindow.EditType editType = this.m_RootWindow.GetEditType();
                switch (editType)
                {
                  case UnitListWindow.EditType.DEFAULT:
                  case UnitListWindow.EditType.EQUIP:
                  case UnitListWindow.EditType.SHOP_EQUIP:
                    List<PartyData> partys = MonoSingleton<GameManager>.Instance.Player.Partys;
                    bool sw = false;
                    for (int index = 0; index < partys.Count; ++index)
                    {
                      if (partys[index].IsPartyUnit(this.m_Param.GetUniq()))
                      {
                        sw = true;
                        break;
                      }
                    }
                    this.m_Value.list.SetActive("use", sw);
                    if (editType == UnitListWindow.EditType.EQUIP || editType == UnitListWindow.EditType.SHOP_EQUIP)
                    {
                      this.m_Value.list.SetActive("badge", false);
                      if (!this.m_Param.interactable)
                      {
                        this.m_Value.list.SetActive("noequip", true);
                        break;
                      }
                      break;
                    }
                    this.m_Value.list.SetActive("badge", true);
                    break;
                  case UnitListWindow.EditType.PARTY:
                  case UnitListWindow.EditType.PARTY_TOWER:
                    this.m_SlotLabel = this.m_RootWindow.AttachSlotLabel(this.m_Param, this.m_Node);
                    this.m_Value.list.SetActive("use", false);
                    this.m_Value.list.SetActive("badge", false);
                    if (editType != UnitListWindow.EditType.EQUIP && editType != UnitListWindow.EditType.SHOP_EQUIP)
                    {
                      CanvasGroup component1 = (CanvasGroup) ((Component) this.m_Node).get_gameObject().GetComponent<CanvasGroup>();
                      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component1, (UnityEngine.Object) null))
                      {
                        component1.set_alpha(!this.m_Param.interactable ? 0.5f : 1f);
                        component1.set_interactable(this.m_Param.interactable);
                        ImageArray component2 = this.m_Value.list.GetComponent<ImageArray>("team");
                        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component2, (UnityEngine.Object) null))
                        {
                          int data = this.m_RootWindow.GetData<int>("data_party_index", -1);
                          ((Component) component2).get_gameObject().SetActive(false);
                          if (this.m_Param.partyIndex >= 0 && this.m_Param.partyIndex < component2.Images.Length && data != this.m_Param.partyIndex)
                          {
                            component2.ImageIndex = this.m_Param.partyIndex;
                            ((Component) component2).get_gameObject().SetActive(true);
                          }
                        }
                      }
                      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_SlotLabel, (UnityEngine.Object) null))
                      {
                        Image componentInChildren = (Image) ((Component) this.m_SlotLabel).GetComponentInChildren<Image>();
                        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) componentInChildren, (UnityEngine.Object) null))
                        {
                          ((Graphic) componentInChildren).set_color(new Color(1f, 1f, 1f, !this.m_Param.interactable ? 0.5f : 1f));
                          break;
                        }
                        break;
                      }
                      break;
                    }
                    break;
                  case UnitListWindow.EditType.SUPPORT:
                    this.m_Value.list.SetActive("use", false);
                    this.m_Value.list.SetActive("badge", false);
                    break;
                  default:
                    this.m_Value.list.SetActive("use", false);
                    this.m_Value.list.SetActive("badge", false);
                    break;
                }
              }
            }
            else if (this.m_ContentType == UnitListRootWindow.ContentType.SUPPORT)
            {
              this.m_Value.list.SetActive("use", this.m_Param.partySelect);
              if (this.m_Param.support != null && this.m_Param.support.IsFriend())
                this.m_Value.list.SetActive("friend", true);
              this.m_Value.list.SetActive("locked", !this.m_Param.interactable);
              ImageArray component = this.m_Value.list.GetComponent<ImageArray>("team");
              if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
              {
                ((Component) component).get_gameObject().SetActive(false);
                if (this.m_Param.partyIndex >= 0)
                {
                  component.ImageIndex = this.m_Param.partyIndex;
                  ((Component) component).get_gameObject().SetActive(true);
                }
              }
            }
            this.m_SortBadge = this.m_Value.list.GetComponent<SortBadge>("sort");
          }
          if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_SortBadge, (UnityEngine.Object) null))
            return;
          if (this.m_SortWindow != null)
          {
            UnitListSortWindow.SelectType section = this.m_SortWindow.GetSection();
            this.SetSortValue(section, UnitListSortWindow.GetSortStatus(this.m_Param, section));
          }
          else
            this.SetSortValue(UnitListSortWindow.SelectType.TIME, 0);
        }

        public void Clear()
        {
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_DataSource, (UnityEngine.Object) null))
          {
            this.m_DataSource.Clear();
            this.m_DataSource = (DataSource) null;
          }
          this.m_SortBadge = (SortBadge) null;
          if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_SlotLabel, (UnityEngine.Object) null))
            return;
          if (this.m_RootWindow != null)
            this.m_RootWindow.DettachSlotLabel(this.m_SlotLabel);
          this.m_SlotLabel = (RectTransform) null;
        }

        public void ForceUpdate()
        {
          if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_Node, (UnityEngine.Object) null))
            return;
          GameParameter.UpdateAll(((Component) this.m_Node).get_gameObject());
        }

        public void LateUpdate()
        {
          if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_SlotLabel, (UnityEngine.Object) null))
            return;
          this.m_SlotLabel.set_anchoredPosition(this.m_Node.GetWorldPos());
        }

        public void SetSortValue(UnitListSortWindow.SelectType section, int value)
        {
          if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_SortBadge, (UnityEngine.Object) null))
            return;
          UnitListSortWindow.SelectType selectType = UnitListSortWindow.SelectType.TIME | UnitListSortWindow.SelectType.RARITY | UnitListSortWindow.SelectType.LEVEL;
          if ((section & selectType) == UnitListSortWindow.SelectType.NONE)
          {
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_SortBadge.Value, (UnityEngine.Object) null))
              this.m_SortBadge.Value.set_text(value.ToString());
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_SortBadge.Icon, (UnityEngine.Object) null))
              this.m_SortBadge.Icon.set_sprite(UnitListSortWindow.GetIcon(section));
            ((Component) this.m_SortBadge).get_gameObject().SetActive(true);
            this.m_Value.list.SetActive("lv", false);
          }
          else
          {
            ((Component) this.m_SortBadge).get_gameObject().SetActive(false);
            this.m_Value.list.SetActive("lv", true);
          }
        }
      }

      public class ItemSource : ContentSource
      {
        private List<UnitListRootWindow.Content.ItemSource.ItemParam> m_Params = new List<UnitListRootWindow.Content.ItemSource.ItemParam>();
        private Vector2 m_AnchorePos = Vector2.get_zero();
        private int m_Forcus = -1;

        public override void Initialize(ContentController controller)
        {
          base.Initialize(controller);
          this.Setup();
        }

        public override void Release()
        {
          this.m_Params.Clear();
          base.Release();
        }

        public int Add(UnitListRootWindow.Content.ItemSource.ItemParam param)
        {
          this.m_Params.Add(param);
          return this.m_Params.Count;
        }

        public void AnchorePos(Vector2 pos)
        {
          this.m_AnchorePos = pos;
        }

        public void ForcusIndex(int index)
        {
          this.m_Forcus = index;
        }

        public void Setup()
        {
          this.Clear();
          this.SetTable((ContentSource.Param[]) this.m_Params.ToArray());
          this.contentController.Resize(0);
          if (this.m_Forcus != -1)
          {
            ContentSource.Param obj = this.GetParam(this.m_Forcus);
            if (obj != null)
            {
              ContentGrid grid = this.contentController.GetGrid(obj.id);
              Vector2 anchorePosFromGrid = this.contentController.GetAnchorePosFromGrid(grid.x, grid.y);
              // ISSUE: explicit reference operation
              // ISSUE: variable of a reference type
              Vector2& local1 = @anchorePosFromGrid;
              // ISSUE: explicit reference operation
              // ISSUE: explicit reference operation
              (^local1).x = (^local1).x + this.contentController.GetSpacing().x;
              // ISSUE: explicit reference operation
              // ISSUE: variable of a reference type
              Vector2& local2 = @anchorePosFromGrid;
              // ISSUE: explicit reference operation
              // ISSUE: explicit reference operation
              (^local2).y = (^local2).y - this.contentController.GetSpacing().y;
              this.contentController.anchoredPosition = anchorePosFromGrid;
            }
            else
              this.contentController.anchoredPosition = Vector2.get_zero();
          }
          else
            this.contentController.anchoredPosition = this.m_AnchorePos;
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
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.contentController.scroller, (UnityEngine.Object) null))
            this.contentController.scroller.StopMovement();
          this.m_AnchorePos = Vector2.get_zero();
          this.m_Forcus = -1;
        }

        public class ItemParam : ContentSource.Param
        {
          private UnitListRootWindow.Content.ItemAccessor m_Accessor = new UnitListRootWindow.Content.ItemAccessor();

          public ItemParam(UnitListWindow window, UnitListWindow.Data param)
          {
            this.m_Accessor.Setup(window, param);
          }

          public override bool IsValid()
          {
            return this.m_Accessor.isValid;
          }

          public UnitListRootWindow.Content.ItemAccessor accerror
          {
            get
            {
              return this.m_Accessor;
            }
          }

          public override void Release()
          {
            this.m_Accessor.Release();
          }

          public override void LateUpdate()
          {
            this.m_Accessor.LateUpdate();
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
      public string unitList;
      public string pieceList;
      public string supportList;
      public GameObject listParent;
      public string tooltipPrefab;

      public override System.Type type
      {
        get
        {
          return typeof (UnitListRootWindow);
        }
      }
    }

    public class ListData
    {
      public string key = string.Empty;
      public List<UnitListWindow.Data> data = new List<UnitListWindow.Data>();
      public List<UnitListWindow.Data> calcData = new List<UnitListWindow.Data>();
      public Vector2 anchorePos = Vector2.get_zero();
      public bool isValid;
      public object response;
      public UnitListRootWindow.Tab selectTab;
      public long selectUniqueID;

      public void Delete()
      {
        this.isValid = false;
        this.data.Clear();
        this.calcData.Clear();
      }

      public void Refresh()
      {
        for (int index = 0; index < this.data.Count; ++index)
          this.data[index].Refresh();
      }

      public void RefreshTabMask()
      {
        for (int index = 0; index < this.data.Count; ++index)
          this.data[index].RefreshTabMask();
      }

      public List<long> GetUniqs()
      {
        List<long> longList = new List<long>();
        for (int index = 0; index < this.calcData.Count; ++index)
        {
          long uniq = this.calcData[index].GetUniq();
          if (uniq > 0L)
            longList.Add(uniq);
        }
        return longList;
      }
    }

    public class TabRegister
    {
      public UnitListRootWindow.Tab tab;
      public Vector2 anchorePos;
      public long forcus;
    }

    protected class Json_ReqSupporterResponse
    {
      public Json_Support[] supports;
    }
  }
}
