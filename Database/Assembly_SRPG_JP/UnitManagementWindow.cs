// Decompiled with JetBrains decompiler
// Type: SRPG.UnitManagementWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(1, "初期化開始", FlowNode.PinTypes.Input, 10)]
  [FlowNode.Pin(12, "限界突破画面へ戻る", FlowNode.PinTypes.Output, 120)]
  [FlowNode.Pin(11, "キャラクタークエスト画面へ戻る", FlowNode.PinTypes.Output, 110)]
  [FlowNode.Pin(10, "強化画面へ戻る", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(13, "進化ウィンドウへ戻る", FlowNode.PinTypes.Output, 130)]
  [FlowNode.Pin(14, "錬成画面へ戻る", FlowNode.PinTypes.Output, 140)]
  [FlowNode.Pin(15, "真理開眼ウィンドウへ戻る", FlowNode.PinTypes.Output, 150)]
  [FlowNode.Pin(2, "ユニット選択へ", FlowNode.PinTypes.Output, 20)]
  [FlowNode.Pin(510, "ユニット選択へ戻る", FlowNode.PinTypes.Output, 510)]
  [FlowNode.Pin(500, "ユニット強化", FlowNode.PinTypes.Input, 500)]
  public class UnitManagementWindow : MonoBehaviour, IFlowInterface
  {
    [SerializeField]
    private string PreviewParentID;
    [SerializeField]
    private string PreviewBaseID;
    [SerializeField]
    private string UnitDetailPreviewPosID;
    [SerializeField]
    private string UnitDetailPreviewBasePosID;
    [SerializeField]
    private string UnitTobiraPreviewPosID;
    [SerializeField]
    private string UnitTobiraPreviewBasePosID;
    [SerializeField]
    public string PATH_UNIT_LIST_WINDOW;
    [SerializeField]
    public string PATH_UNIT_EQUIPMENT_WINDOW;
    [SerializeField]
    public string PATH_UNIT_KAKERA_WINDOW;
    [SerializeField]
    public string PATH_UNIT_INVENTORY_WINDOW;
    [SerializeField]
    public string PATH_TOBIRA_INVENTORY_WINDOW;
    private bool mInitialize;
    private UnitListWindow mUnitListWindow;
    private UnitEnhanceV3 mUEMain;
    private UnitEquipmentWindow mEquipWindow;
    private UnitKakeraWindow mKakeraWindow;
    private LoadRequest mReqAbilityDetail;
    private LoadRequest mUnitInventoryWindowLoadRequest;
    private UnitListRootWindow.Tab mCurrentTab;
    private Vector2 mCurrentTabAnchorePos;
    private long mCurrentUnit;
    private Transform mUnitModelPreviewParent;
    private Transform mUnitModelPreviewBase;
    private QuestDropParam mQuestDropParam;
    private static UnitManagementWindow instance;

    public UnitManagementWindow()
    {
      base.\u002Ector();
    }

    public static UnitManagementWindow Instance
    {
      get
      {
        return UnitManagementWindow.instance;
      }
    }

    public Transform UnitModelPreviewParent
    {
      get
      {
        return this.mUnitModelPreviewParent;
      }
    }

    public Transform UnitModelPreviewBase
    {
      get
      {
        return this.mUnitModelPreviewBase;
      }
    }

    private void Awake()
    {
      UnitManagementWindow.instance = this;
    }

    [DebuggerHidden]
    private IEnumerator Start()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new UnitManagementWindow.\u003CStart\u003Ec__Iterator16B()
      {
        \u003C\u003Ef__this = this
      };
    }

    [DebuggerHidden]
    private IEnumerator SetupByRestorePoint(RestorePoints restore_point)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new UnitManagementWindow.\u003CSetupByRestorePoint\u003Ec__Iterator16C()
      {
        restore_point = restore_point,
        \u003C\u0024\u003Erestore_point = restore_point,
        \u003C\u003Ef__this = this
      };
    }

    [DebuggerHidden]
    private IEnumerator CreateUnitInventoryWindow()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new UnitManagementWindow.\u003CCreateUnitInventoryWindow\u003Ec__Iterator16D()
      {
        \u003C\u003Ef__this = this
      };
    }

    [DebuggerHidden]
    private IEnumerator InitUnitInventoryWindow(UnitListRootWindow window, long unique_id)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new UnitManagementWindow.\u003CInitUnitInventoryWindow\u003Ec__Iterator16E()
      {
        window = window,
        unique_id = unique_id,
        \u003C\u0024\u003Ewindow = window,
        \u003C\u0024\u003Eunique_id = unique_id,
        \u003C\u003Ef__this = this
      };
    }

    [DebuggerHidden]
    private IEnumerator InitializeUnitKyouka()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new UnitManagementWindow.\u003CInitializeUnitKyouka\u003Ec__Iterator16F()
      {
        \u003C\u003Ef__this = this
      };
    }

    [DebuggerHidden]
    private IEnumerator InitializeUnitCharacterQuest()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new UnitManagementWindow.\u003CInitializeUnitCharacterQuest\u003Ec__Iterator170()
      {
        \u003C\u003Ef__this = this
      };
    }

    [DebuggerHidden]
    private IEnumerator InitializeUnitKakera()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new UnitManagementWindow.\u003CInitializeUnitKakera\u003Ec__Iterator171()
      {
        \u003C\u003Ef__this = this
      };
    }

    [DebuggerHidden]
    private IEnumerator InitializeUnitEvolution()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new UnitManagementWindow.\u003CInitializeUnitEvolution\u003Ec__Iterator172()
      {
        \u003C\u003Ef__this = this
      };
    }

    [DebuggerHidden]
    private IEnumerator InitializeUnitUnlock()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new UnitManagementWindow.\u003CInitializeUnitUnlock\u003Ec__Iterator173()
      {
        \u003C\u003Ef__this = this
      };
    }

    [DebuggerHidden]
    private IEnumerator InitializeUnitUnlockTobira()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new UnitManagementWindow.\u003CInitializeUnitUnlockTobira\u003Ec__Iterator174()
      {
        \u003C\u003Ef__this = this
      };
    }

    [DebuggerHidden]
    private IEnumerator InitializeUnitEnhanceTobira()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new UnitManagementWindow.\u003CInitializeUnitEnhanceTobira\u003Ec__Iterator175()
      {
        \u003C\u003Ef__this = this
      };
    }

    private void InitUnitModelPreviewBase()
    {
      if (!string.IsNullOrEmpty(this.PreviewParentID))
        this.mUnitModelPreviewParent = GameObjectID.FindGameObject<Transform>(this.PreviewParentID);
      if (string.IsNullOrEmpty(this.PreviewBaseID))
        return;
      this.mUnitModelPreviewBase = GameObjectID.FindGameObject<Transform>(this.PreviewBaseID);
      if (!Object.op_Inequality((Object) this.mUnitModelPreviewBase, (Object) null))
        return;
      ((Component) this.mUnitModelPreviewBase).get_gameObject().SetActive(false);
    }

    public void ChangeUnitPreviewPos(bool is_unit_detail)
    {
      if (Object.op_Equality((Object) this.mUnitModelPreviewParent, (Object) null) || Object.op_Equality((Object) this.mUnitModelPreviewBase, (Object) null))
        return;
      string name1 = !is_unit_detail ? this.UnitTobiraPreviewPosID : this.UnitDetailPreviewPosID;
      string name2 = !is_unit_detail ? this.UnitTobiraPreviewBasePosID : this.UnitDetailPreviewBasePosID;
      GameObject gameObject1 = GameObjectID.FindGameObject(name1);
      GameObject gameObject2 = GameObjectID.FindGameObject(name2);
      if (!Object.op_Inequality((Object) gameObject1, (Object) null) || !Object.op_Inequality((Object) gameObject2, (Object) null))
        return;
      ((Component) this.mUnitModelPreviewParent).get_transform().set_localPosition(gameObject1.get_transform().get_localPosition());
      ((Component) this.mUnitModelPreviewBase).get_transform().set_localPosition(gameObject2.get_transform().get_localPosition());
    }

    private void Release()
    {
      this.DestroyUnitList();
      if (Object.op_Inequality((Object) this.mUEMain, (Object) null))
      {
        Object.Destroy((Object) ((Component) this.mUEMain).get_gameObject());
        this.mUEMain = (UnitEnhanceV3) null;
      }
      if (Object.op_Inequality((Object) this.mEquipWindow, (Object) null))
      {
        Object.Destroy((Object) ((Component) this.mEquipWindow).get_gameObject());
        this.mEquipWindow = (UnitEquipmentWindow) null;
      }
      if (!Object.op_Inequality((Object) this.mKakeraWindow, (Object) null))
        return;
      Object.Destroy((Object) ((Component) this.mKakeraWindow).get_gameObject());
      this.mKakeraWindow = (UnitKakeraWindow) null;
    }

    public void CreateUnitList()
    {
      if (!Object.op_Equality((Object) this.mUnitListWindow, (Object) null))
        return;
      GameObject gameObject = AssetManager.Load<GameObject>(this.PATH_UNIT_LIST_WINDOW);
      if (!Object.op_Inequality((Object) gameObject, (Object) null))
        return;
      this.mUnitListWindow = (UnitListWindow) ((GameObject) Object.Instantiate<GameObject>((M0) gameObject)).GetComponent<UnitListWindow>();
      ((Component) this.mUnitListWindow).get_transform().SetParent(((Component) this).get_transform(), false);
    }

    public void DestroyUnitList()
    {
      if (!Object.op_Inequality((Object) this.mUnitListWindow, (Object) null))
        return;
      Object.Destroy((Object) ((Component) this.mUnitListWindow).get_gameObject());
      this.mUnitListWindow = (UnitListWindow) null;
    }

    private List<long> GetDefaultUnitList(long selectUniq)
    {
      List<long> longList = new List<long>();
      if (Object.op_Inequality((Object) this.mUEMain, (Object) null))
      {
        List<UnitData> units = MonoSingleton<GameManager>.Instance.Player.Units;
        List<UnitListWindow.Data> list = new List<UnitListWindow.Data>();
        for (int index = 0; index < units.Count; ++index)
        {
          if (units[index] != null)
          {
            UnitListWindow.Data data = new UnitListWindow.Data(units[index]);
            data.Refresh();
            if (data.GetUniq() == selectUniq)
              data.filterMask = UnitListFilterWindow.SelectType.NONE;
            list.Add(data);
          }
        }
        if (Object.op_Inequality((Object) this.mUnitListWindow, (Object) null))
        {
          if (this.mUnitListWindow.filterWindow != null)
            this.mUnitListWindow.filterWindow.CalcUnit(list);
          if (this.mUnitListWindow.sortWindow != null)
            this.mUnitListWindow.sortWindow.CalcUnit(list);
        }
        for (int index = 0; index < list.Count; ++index)
          longList.Add(list[index].GetUniq());
      }
      return longList;
    }

    private int GetOutputPinByRestorePoint(RestorePoints rp)
    {
      int num = 2;
      Dictionary<RestorePoints, int> dictionary = new Dictionary<RestorePoints, int>()
      {
        {
          RestorePoints.UnitKyouka,
          10
        },
        {
          RestorePoints.UnitCharacterQuest,
          11
        },
        {
          RestorePoints.UnitKakera,
          12
        },
        {
          RestorePoints.UnitEvolution,
          13
        },
        {
          RestorePoints.UnitUnlock,
          14
        },
        {
          RestorePoints.UnlockTobira,
          15
        }
      };
      if (dictionary.ContainsKey(rp))
        num = dictionary[rp];
      return num;
    }

    public void Activated(int pinID)
    {
      if (pinID == 1)
      {
        this.mInitialize = true;
      }
      else
      {
        if (pinID != 500)
          return;
        this.OnUnitSelect(GlobalVars.SelectedUnitUniqueID.Get());
      }
    }

    private void OnDestroy()
    {
      this.Release();
    }

    private void OnUnitSelect(long uniqueID)
    {
      if (Object.op_Equality((Object) this.mUnitListWindow, (Object) null) || !this.mUnitListWindow.IsEnabled())
        return;
      UnitListRootWindow rootWindow = this.mUnitListWindow.rootWindow;
      if (rootWindow == null || rootWindow.GetEditType() != UnitListWindow.EditType.DEFAULT)
        return;
      this.StartCoroutine(this.InitUnitInventoryWindow(rootWindow, uniqueID));
    }

    private void OnUEWindowClosedByUser()
    {
      this.mUEMain.OnUserClose = (UnitEnhanceV3.CloseEvent) null;
      if (this.mUEMain.GetDirtyUnits().Length > 0)
        this.mUEMain.ClearDirtyUnits();
      UnitListRootWindow.TabRegister tabRegister = new UnitListRootWindow.TabRegister();
      if (this.mCurrentTab != UnitListRootWindow.Tab.NONE)
      {
        tabRegister.tab = this.mCurrentTab;
        tabRegister.anchorePos = this.mCurrentTabAnchorePos;
        string s = FlowNode_Variable.Get("LAST_SELECTED_UNITID");
        if (!string.IsNullOrEmpty(s))
        {
          long num = long.Parse(s);
          if (num > 0L && this.mCurrentUnit != num)
            tabRegister.forcus = num;
          FlowNode_Variable.Set("LAST_SELECTED_UNITID", string.Empty);
        }
      }
      else
      {
        tabRegister.tab = UnitListRootWindow.Tab.ALL;
        tabRegister.forcus = GlobalVars.SelectedUnitUniqueID.Get();
        string s = FlowNode_Variable.Get("LAST_SELECTED_UNITID");
        if (!string.IsNullOrEmpty(s))
        {
          long num = long.Parse(s);
          if (num > 0L)
            tabRegister.forcus = num;
          FlowNode_Variable.Set("LAST_SELECTED_UNITID", string.Empty);
        }
      }
      SerializeValueList serializeValueList = new SerializeValueList();
      serializeValueList.AddObject("data_register", (object) tabRegister);
      FlowNode_ButtonEvent.currentValue = (object) serializeValueList;
      if (Object.op_Equality((Object) this.mUnitListWindow, (Object) null))
        this.CreateUnitList();
      this.mUnitListWindow.Enabled(true);
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 510);
    }
  }
}
