// Decompiled with JetBrains decompiler
// Type: SRPG.ItemListWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(100, "詳細表示", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(1, "Start", FlowNode.PinTypes.Input, 1)]
  [AddComponentMenu("UI/リスト/アイテム")]
  [FlowNode.Pin(2, "Refresh", FlowNode.PinTypes.Input, 1)]
  public class ItemListWindow : MonoBehaviour, IFlowInterface
  {
    public GameObject ItemTemplate;
    public Toggle ToggleShowAll;
    public Toggle ToggleShowUsed;
    public Toggle ToggleShowEquip;
    public Toggle ToggleShowUnitPierce;
    public Toggle ToggleShowItemPierce;
    public Toggle ToggleShowMaterial;
    private ItemData SelectItem;
    private ItemListWindow.ItemSource m_ItemSource;
    private ContentController m_ContenController;

    public ItemListWindow()
    {
      base.\u002Ector();
    }

    private void Awake()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ToggleShowAll, (UnityEngine.Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent<bool>) this.ToggleShowAll.onValueChanged).AddListener(new UnityAction<bool>((object) this, __methodptr(OnShowAll)));
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ToggleShowUsed, (UnityEngine.Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent<bool>) this.ToggleShowUsed.onValueChanged).AddListener(new UnityAction<bool>((object) this, __methodptr(OnShowUsed)));
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ToggleShowEquip, (UnityEngine.Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent<bool>) this.ToggleShowEquip.onValueChanged).AddListener(new UnityAction<bool>((object) this, __methodptr(OnShowEquip)));
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ToggleShowUnitPierce, (UnityEngine.Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent<bool>) this.ToggleShowUnitPierce.onValueChanged).AddListener(new UnityAction<bool>((object) this, __methodptr(OnShowUnitPiece)));
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ToggleShowItemPierce, (UnityEngine.Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent<bool>) this.ToggleShowItemPierce.onValueChanged).AddListener(new UnityAction<bool>((object) this, __methodptr(OnShowItemPiece)));
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ToggleShowMaterial, (UnityEngine.Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent<bool>) this.ToggleShowMaterial.onValueChanged).AddListener(new UnityAction<bool>((object) this, __methodptr(OnShowMaterial)));
      }
      this.m_ContenController = (ContentController) ((Component) this).GetComponent<ContentController>();
      this.m_ContenController.SetWork((object) this);
    }

    private void Start()
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_ContenController, (UnityEngine.Object) null))
        return;
      this.m_ItemSource = new ItemListWindow.ItemSource();
      List<ItemData> items = MonoSingleton<GameManager>.Instance.Player.Items;
      for (int index = 0; index < items.Count; ++index)
      {
        ItemData itemData = items[index];
        if (itemData.Num != 0 && itemData.Param.CheckCanShowInList())
          this.m_ItemSource.Add(new ItemListWindow.ItemSource.ItemParam(itemData));
      }
      this.m_ContenController.Initialize((ContentSource) this.m_ItemSource, Vector2.get_zero());
    }

    private void OnDestroy()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_ContenController, (UnityEngine.Object) null))
        this.m_ContenController.Release();
      this.m_ContenController = (ContentController) null;
      this.m_ItemSource = (ItemListWindow.ItemSource) null;
    }

    private void Update()
    {
    }

    public void Activated(int pinID)
    {
    }

    public void SetupNodeEvent(ContentNode node)
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) node, (UnityEngine.Object) null))
        return;
      ListItemEvents component = (ListItemEvents) ((Component) node).GetComponent<ListItemEvents>();
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
        return;
      component.OnSelect = new ListItemEvents.ListItemEvent(this.OnSelect);
    }

    private void OnShowAll(bool isActive)
    {
      if (!isActive || this.m_ItemSource == null)
        return;
      this.m_ItemSource.SelectType(ItemListWindow.ItemSource.ItemType.ALL);
    }

    private void OnShowUsed(bool isActive)
    {
      if (!isActive || this.m_ItemSource == null)
        return;
      this.m_ItemSource.SelectType(ItemListWindow.ItemSource.ItemType.USED);
    }

    private void OnShowEquip(bool isActive)
    {
      if (!isActive || this.m_ItemSource == null)
        return;
      this.m_ItemSource.SelectType(ItemListWindow.ItemSource.ItemType.EQUIP);
    }

    private void OnShowItemPiece(bool isActive)
    {
      if (!isActive || this.m_ItemSource == null)
        return;
      this.m_ItemSource.SelectType(ItemListWindow.ItemSource.ItemType.ITEM_PIECE);
    }

    private void OnShowMaterial(bool isActive)
    {
      if (!isActive || this.m_ItemSource == null)
        return;
      this.m_ItemSource.SelectType(ItemListWindow.ItemSource.ItemType.MATERIAL);
    }

    private void OnShowUnitPiece(bool isActive)
    {
      if (!isActive || this.m_ItemSource == null)
        return;
      this.m_ItemSource.SelectType(ItemListWindow.ItemSource.ItemType.UNIT_PIECE);
    }

    private void OnSelect(GameObject go)
    {
      ItemData dataOfClass = DataSource.FindDataOfClass<ItemData>(go, (ItemData) null);
      if (dataOfClass == null)
        return;
      GlobalVars.SelectedItemID = dataOfClass.Param.iname;
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
    }

    private class ItemNode : ContentNode
    {
      private DataSource m_DataSource;
      private GameParameter[] m_GameParameters;

      public DataSource dataSource
      {
        get
        {
          return this.m_DataSource;
        }
      }

      public override void Initialize(ContentController controller)
      {
        base.Initialize(controller);
        this.m_DataSource = DataSource.Create(((Component) this).get_gameObject());
        this.m_GameParameters = (GameParameter[]) ((Component) this).get_gameObject().GetComponentsInChildren<GameParameter>();
        ItemListWindow work = controller.GetWork() as ItemListWindow;
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) work, (UnityEngine.Object) null))
          return;
        work.SetupNodeEvent((ContentNode) this);
      }

      public override void Release()
      {
        base.Release();
      }

      public void ForceUpdate()
      {
        if (this.m_GameParameters == null)
          return;
        for (int index = 0; index < this.m_GameParameters.Length; ++index)
        {
          GameParameter gameParameter = this.m_GameParameters[index];
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) gameParameter, (UnityEngine.Object) null))
            gameParameter.UpdateValue();
        }
      }

      private void OnSelect(GameObject go)
      {
        ItemData dataOfClass = DataSource.FindDataOfClass<ItemData>(go, (ItemData) null);
        if (dataOfClass == null)
          return;
        GlobalVars.SelectedItemID = dataOfClass.Param.iname;
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
      }
    }

    public class ItemSource : ContentSource
    {
      private List<ItemListWindow.ItemSource.ItemParam> m_Params = new List<ItemListWindow.ItemSource.ItemParam>();
      private ItemListWindow.ItemSource.ItemType m_ItemType;

      public override void Initialize(ContentController controller)
      {
        base.Initialize(controller);
        this.SelectType(ItemListWindow.ItemSource.ItemType.ALL);
      }

      public override void Release()
      {
        base.Release();
        this.m_ItemType = ItemListWindow.ItemSource.ItemType.NONE;
      }

      public override ContentNode Instantiate(ContentNode res)
      {
        GameObject gameObject = (GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) ((Component) res).get_gameObject());
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) gameObject, (UnityEngine.Object) null))
          return (ContentNode) gameObject.AddComponent<ItemListWindow.ItemNode>();
        return (ContentNode) null;
      }

      public void Add(ItemListWindow.ItemSource.ItemParam param)
      {
        if (!param.IsValid())
          return;
        this.m_Params.Add(param);
      }

      public void SelectType(ItemListWindow.ItemSource.ItemType itemType)
      {
        if (this.m_ItemType == itemType)
          return;
        Func<ItemListWindow.ItemSource.ItemParam, bool> predicate = (Func<ItemListWindow.ItemSource.ItemParam, bool>) null;
        switch (itemType)
        {
          case ItemListWindow.ItemSource.ItemType.USED:
            // ISSUE: object of a compiler-generated type is created
            // ISSUE: reference to a compiler-generated method
            predicate = new Func<ItemListWindow.ItemSource.ItemParam, bool>(new ItemListWindow.ItemSource.\u003CSelectType\u003Ec__AnonStorey340()
            {
              UsedItemTypes = new List<EItemType>()
              {
                EItemType.ExpUpPlayer,
                EItemType.ExpUpUnit,
                EItemType.ExpUpSkill,
                EItemType.ExpUpEquip,
                EItemType.ExpUpArtifact,
                EItemType.GoldConvert,
                EItemType.Ticket,
                EItemType.Used
              }
            }.\u003C\u003Em__393);
            break;
          case ItemListWindow.ItemSource.ItemType.EQUIP:
            predicate = (Func<ItemListWindow.ItemSource.ItemParam, bool>) (prop => prop.itemType == EItemType.Equip);
            break;
          case ItemListWindow.ItemSource.ItemType.ITEM_PIECE:
            predicate = (Func<ItemListWindow.ItemSource.ItemParam, bool>) (prop =>
            {
              if (prop.itemType != EItemType.ItemPiece && prop.itemType != EItemType.ItemPiecePiece)
                return prop.itemType == EItemType.ArtifactPiece;
              return true;
            });
            break;
          case ItemListWindow.ItemSource.ItemType.MATERIAL:
            predicate = (Func<ItemListWindow.ItemSource.ItemParam, bool>) (prop => prop.itemType == EItemType.Material);
            break;
          case ItemListWindow.ItemSource.ItemType.UNIT_PIECE:
            predicate = (Func<ItemListWindow.ItemSource.ItemParam, bool>) (prop => prop.itemType == EItemType.UnitPiece);
            break;
        }
        this.Clear();
        if (predicate != null)
          this.SetTable((ContentSource.Param[]) this.m_Params.Where<ItemListWindow.ItemSource.ItemParam>(predicate).ToArray<ItemListWindow.ItemSource.ItemParam>());
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
        this.contentController.scroller.StopMovement();
        this.m_ItemType = itemType;
      }

      public enum ItemType
      {
        NONE,
        ALL,
        USED,
        EQUIP,
        ITEM_PIECE,
        MATERIAL,
        UNIT_PIECE,
      }

      public class ItemParam : ContentSource.Param
      {
        private ItemData m_Item;

        public ItemParam(ItemData item)
        {
          this.m_Item = item;
        }

        public override bool IsValid()
        {
          return this.m_Item != null;
        }

        public ItemData data
        {
          get
          {
            return this.m_Item;
          }
        }

        public EItemType itemType
        {
          get
          {
            return this.m_Item.ItemType;
          }
        }

        public override void OnSetup(ContentNode node)
        {
          ItemListWindow.ItemNode itemNode = node as ItemListWindow.ItemNode;
          if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) itemNode, (UnityEngine.Object) null))
            return;
          itemNode.dataSource.Clear();
          itemNode.dataSource.Add(typeof (ItemData), (object) this.m_Item);
          itemNode.ForceUpdate();
        }
      }
    }
  }
}
