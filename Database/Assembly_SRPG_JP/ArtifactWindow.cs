// Decompiled with JetBrains decompiler
// Type: SRPG.ArtifactWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(302, "Transmute End", FlowNode.PinTypes.Output, 302)]
  [FlowNode.Pin(101, "Finalize Begin", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(400, "Decompose", FlowNode.PinTypes.Input, 400)]
  [FlowNode.Pin(102, "Finalize End", FlowNode.PinTypes.Output, 102)]
  [FlowNode.Pin(151, "AddExp Begin", FlowNode.PinTypes.Output, 151)]
  [FlowNode.Pin(152, "AddExp End", FlowNode.PinTypes.Output, 152)]
  [FlowNode.Pin(200, "Rarity Up", FlowNode.PinTypes.Input, 200)]
  [FlowNode.Pin(201, "Rarity Up End", FlowNode.PinTypes.Output, 201)]
  [FlowNode.Pin(202, "Rarity Up Dialog Close", FlowNode.PinTypes.Output, 202)]
  [FlowNode.Pin(300, "Transmute", FlowNode.PinTypes.Input, 300)]
  [FlowNode.Pin(301, "Transmute Begin", FlowNode.PinTypes.Output, 301)]
  [FlowNode.Pin(700, "Reset Sending Request", FlowNode.PinTypes.Input, 700)]
  [FlowNode.Pin(602, "Sell End", FlowNode.PinTypes.Output, 602)]
  [FlowNode.Pin(601, "Sell Begin", FlowNode.PinTypes.Output, 601)]
  [FlowNode.Pin(600, "Sell", FlowNode.PinTypes.Input, 600)]
  [FlowNode.Pin(500, "Equip", FlowNode.PinTypes.Output, 500)]
  [FlowNode.Pin(401, "Decompose End", FlowNode.PinTypes.Output, 402)]
  [FlowNode.Pin(402, "Decompose Begin", FlowNode.PinTypes.Output, 401)]
  [FlowNode.Pin(5, "Refresh", FlowNode.PinTypes.Input, 5)]
  [FlowNode.Pin(6, "Refresh Exp Items", FlowNode.PinTypes.Input, 6)]
  [FlowNode.Pin(11, "Show Selection", FlowNode.PinTypes.Input, 7)]
  [FlowNode.Pin(10, "Flush Requests", FlowNode.PinTypes.Input, 10)]
  [FlowNode.Pin(100, "Finalize", FlowNode.PinTypes.Input, 100)]
  public class ArtifactWindow : MonoBehaviour, IFlowInterface
  {
    private static readonly string ARTIFACT_EXPMAX_UI_PATH = "UI/ArtifactLevelUpWindow";
    private static readonly string ARTIFACT_RARITY_CHECK_UI_PATH = "UI/ArtifactRarityCheck";
    public ArtifactList ArtifactList;
    [StringIsGameObjectID]
    public string ArtifactListID;
    public GenericSlot ArtifactSlot;
    public GameObject WindowBody;
    public bool RefreshOnStart;
    public float AutoFlushRequests;
    [FourCC]
    public int ArtifactDataID;
    [FourCC]
    public int ItemDataID;
    [FourCC]
    public int ChangedAbilityID;
    [FourCC]
    public int NewAbilityID;
    [Space(16f)]
    public GameObject DetailWindow;
    public MaterialPanel DetailMaterial;
    [Space(16f)]
    public GameObject KyokaPanel;
    public string KyokaPanelState;
    public int KyokaPanel_Enable;
    public int KyokaPanel_LvCapped;
    public int KyokaPanel_LvMax;
    public GameObject KyokaList;
    public GameObject KyokaListItem;
    public string KyokaSE;
    public SRPG_Button ExpMaxLvupBtn;
    [Space(16f)]
    public StatusList Status;
    public ExpPanel ExpPanel;
    [Space(16f)]
    public GameObject RarityUpPanel;
    public string RarityUpPanelState;
    public int RarityUpPanel_Normal;
    public int RarityUpPanel_MaxRarity;
    public int RarityUpPanel_NoItem;
    public Text RarityUpCost;
    public string RarityUpCostState;
    public int RarityUpCost_Normal;
    public int RarityUpCost_NoGold;
    public Text RarityUpCondition;
    public MaterialPanel RarityUpMaterial;
    public GameObject RarityUpResult;
    public GameObject RarityUpList;
    public GameObject RarityUpListItem;
    public GameObject RarityUpIconRoot;
    public GameObject RarityUpHilit;
    public Button RarityUpButton;
    [Space(16f)]
    public GameObject TransmutePanel;
    public string TransmutePanelState;
    public int TransmutePanel_Normal;
    public int TransmutePanel_Disabled;
    public Text TransmuteCost;
    public string TransmuteCostState;
    public int TransmuteCost_Normal;
    public int TransmuteCost_NoGold;
    public Text TransmuteCondition;
    public MaterialPanel TransmuteMaterial;
    public GameObject TransmuteResult;
    [Space(16f)]
    public GenericSlot OwnerSlot;
    [Space(16f)]
    public GameObject AbilityList;
    public GameObject AbilityListItem;
    public string AbilityListItemState;
    public int AbilityListItem_Hidden;
    public int AbilityListItem_Locked;
    public int AbilityListItem_Unlocked;
    [Space(16f)]
    public GameObject SellResult;
    public Text SellPrice;
    public string SellSE;
    [Space(16f)]
    public GameObject DecomposePanel;
    public string DecomposePanelState;
    public int DecomposePanel_Normal;
    public int DecomposePanel_Disabled;
    public MaterialPanel DecomposeItem;
    public GameObject DecomposeResult;
    public Text DecomposeHelp;
    public Text DecomposeCost;
    public Text DecomposeCostTotal;
    public Text DecomposeKakeraNumOld;
    public Text DecomposeKakeraNumNew;
    public GameObject DecomposeWarning;
    public int DecomposeWarningRarity;
    public string DecomposeCostState;
    public int DecomposeCost_Normal;
    public int DecomposeCost_NoGold;
    public string DecomposeSE;
    [Space(16f)]
    public GenericSlot ArtifactOwnerSlot;
    public ArtifactWindow.EquipEvent OnEquip;
    [Space(16f)]
    public Text SelectionNum;
    public ArtifactIcon SelectionListItem;
    public GameObject SelectionList;
    [Space(16f)]
    public Toggle ProcessToggle_Enhance;
    public Toggle ProcessToggle_Evolution;
    public Toggle ProcessToggle_Detail;
    [Space(16f)]
    public Toggle ToggleExcludeEquiped;
    public int RarityCheckValue;
    [SerializeField]
    [HeaderBar("▼セット効果確認用のボタン")]
    private Button m_SetEffectsButton;
    private List<ChangeListData> mChangeSet;
    private ArtifactData mCurrentArtifact;
    private ArtifactParam mCurrentArtifactParam;
    private object[] mSelectedArtifacts;
    private List<GameObject> mAbilityItems;
    private List<GameObject> mKyokaItems;
    private List<GameObject> mRarityUpItems;
    private List<ArtifactWindow.Request> mRequests;
    private List<GameObject> mDecomposeItems;
    private List<ArtifactIcon> mSelectionItems;
    private bool mFinalizing;
    private bool mSendingRequests;
    private bool mSceneChanging;
    private long mTotalSellPrice;
    private UnitData mOwnerUnitData;
    private int mOwnerUnitJobIndex;
    private GameObject mDetailWindow;
    private GameObject mResultWindow;
    private float mFlushTimer;
    private bool mBusy;
    private bool mDisableFlushRequest;
    private int mUseEnhanceItemNum;
    private UnitData mCurrentArtifactOwner;
    private JobData mCurrentArtifactOwnerJob;
    private ArtifactData[] mCachedArtifacts;
    private List<ItemData> mTmpItems;
    private GameObject mConfirmDialogGo;
    private ArtifactWindow.StatusCache mStatusCache;
    private ArtifactTypes mSelectArtifactSlot;

    public ArtifactWindow()
    {
      base.\u002Ector();
    }

    public void Activated(int pinID)
    {
      int num = pinID;
      switch (num)
      {
        case 5:
          this.RefreshWindow();
          break;
        case 6:
          this.RefreshKyokaList();
          break;
        case 10:
          this.FlushRequests(false);
          break;
        case 11:
          this.ShowSelection();
          break;
        default:
          if (num != 100)
          {
            if (num != 200)
            {
              if (num != 300)
              {
                if (num != 400)
                {
                  if (num != 600)
                  {
                    if (num != 700)
                      break;
                    this.mSendingRequests = false;
                    break;
                  }
                  this.SellArtifacts();
                  break;
                }
                this.DecomposeArtifact();
                break;
              }
              this.TransmuteArtifact();
              break;
            }
            this.AddRarity();
            break;
          }
          this.mDisableFlushRequest = false;
          this.mSendingRequests = true;
          this.mFinalizing = true;
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 101);
          break;
      }
    }

    public List<ItemData> TmpItems
    {
      get
      {
        return this.mTmpItems;
      }
    }

    public ArtifactTypes SelectArtifactSlot
    {
      get
      {
        return this.mSelectArtifactSlot;
      }
      set
      {
        this.mSelectArtifactSlot = value;
      }
    }

    private void Start()
    {
      GameManager instanceDirect = MonoSingleton<GameManager>.GetInstanceDirect();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) instanceDirect, (UnityEngine.Object) null))
        instanceDirect.OnSceneChange += new GameManager.SceneChangeEvent(this.OnSceneCHange);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ExpPanel, (UnityEngine.Object) null))
      {
        this.ExpPanel.SetDelegate(new ExpPanel.CalcEvent(ArtifactData.StaticCalcExpFromLevel), new ExpPanel.CalcEvent(ArtifactData.StaticCalcLevelFromExp));
        this.ExpPanel.OnLevelChange = new ExpPanel.LevelChangeEvent(this.OnLevelChange);
        this.ExpPanel.OnFinish = new ExpPanel.ExpPanelEvent(this.OnKyokaEnd);
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ExpMaxLvupBtn, (UnityEngine.Object) null))
        this.ExpMaxLvupBtn.AddListener(new SRPG_Button.ButtonClickEvent(this.OnExpMaxOpen));
      if (!string.IsNullOrEmpty(this.ArtifactListID))
        this.ArtifactList = GameObjectID.FindGameObject<ArtifactList>(this.ArtifactListID);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ArtifactList, (UnityEngine.Object) null))
        this.ArtifactList.OnSelectionChange += new ArtifactList.SelectionChangeEvent(this.OnArtifactSelect);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.KyokaListItem, (UnityEngine.Object) null) && this.KyokaListItem.get_activeInHierarchy())
        this.KyokaListItem.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.SelectionListItem, (UnityEngine.Object) null) && ((Component) this.SelectionListItem).get_gameObject().get_activeSelf())
        ((Component) this.SelectionListItem).get_gameObject().SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ArtifactSlot, (UnityEngine.Object) null))
        this.ArtifactSlot.SetSlotData<ArtifactData>((ArtifactData) null);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ToggleExcludeEquiped, (UnityEngine.Object) null))
        this.ToggleExcludeEquiped.set_isOn(this.ReadExcludeEquipedSettingValue());
      this.mCurrentArtifact = DataSource.FindDataOfClass<ArtifactData>(((Component) this).get_gameObject(), (ArtifactData) null);
      if (this.mCurrentArtifact != null)
        this.mCurrentArtifactParam = this.mCurrentArtifact.ArtifactParam;
      this.Rebind();
      if (!this.RefreshOnStart)
        return;
      this.RefreshWindow();
    }

    private void OnDestroy()
    {
      GameManager instanceDirect = MonoSingleton<GameManager>.GetInstanceDirect();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) instanceDirect, (UnityEngine.Object) null))
        instanceDirect.OnSceneChange -= new GameManager.SceneChangeEvent(this.OnSceneCHange);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mDetailWindow, (UnityEngine.Object) null))
      {
        UnityEngine.Object.Destroy((UnityEngine.Object) this.mDetailWindow.get_gameObject());
        this.mDetailWindow = (GameObject) null;
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mResultWindow, (UnityEngine.Object) null))
      {
        UnityEngine.Object.Destroy((UnityEngine.Object) this.mResultWindow);
        this.mResultWindow = (GameObject) null;
      }
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ArtifactList, (UnityEngine.Object) null))
        return;
      this.ArtifactList.OnSelectionChange -= new ArtifactList.SelectionChangeEvent(this.OnArtifactSelect);
    }

    private bool IsConnectableNetwork()
    {
      switch ((int) Application.get_internetReachability())
      {
        case 0:
          return false;
        default:
          return true;
      }
    }

    private void FlushRequests(bool immediate)
    {
      if (immediate)
      {
        if (!this.IsConnectableNetwork())
          return;
        while (this.mRequests.Count > 0 && !Network.IsConnecting)
        {
          WebAPI api = this.mRequests[0].Compose();
          this.mRequests.RemoveAt(0);
          if (Network.Mode == Network.EConnectMode.Online)
            Network.RequestAPIImmediate(api, true);
        }
        this.RefreshArtifactList();
      }
      else
      {
        if (this.mRequests.Count <= 0)
          return;
        this.mSendingRequests = true;
      }
    }

    private void OnApplicationPause(bool pausing)
    {
      if (!pausing)
        return;
      this.FlushRequests(true);
    }

    private void OnApplicationFocus(bool focus)
    {
      if (focus)
        return;
      this.FlushRequests(true);
    }

    private bool IsRequestPending<T>()
    {
      return this.mRequests.Count > 0 && this.mRequests[0] is T;
    }

    private void Update()
    {
      if (this.mSendingRequests)
      {
        if (Network.IsConnecting)
          return;
        if (this.mRequests.Count > 0)
        {
          if (Network.Mode == Network.EConnectMode.Online)
          {
            Network.RequestAPI(this.mRequests[0].Compose(), false);
            this.mRequests.RemoveAt(0);
          }
          this.mSendingRequests = true;
          return;
        }
      }
      if (this.mFinalizing)
      {
        if (this.IsBusy)
          return;
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 102);
        this.mFinalizing = false;
        this.mSendingRequests = false;
      }
      else
      {
        if (this.mDisableFlushRequest || (double) this.mFlushTimer <= 0.0)
          return;
        this.mFlushTimer -= Time.get_unscaledDeltaTime();
        if ((double) this.mFlushTimer > 0.0)
          return;
        this.FlushRequests(false);
      }
    }

    public void RefreshKyokaList()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.KyokaList, (UnityEngine.Object) null) || UnityEngine.Object.op_Equality((UnityEngine.Object) this.KyokaListItem, (UnityEngine.Object) null) || (UnityEngine.Object.op_Equality((UnityEngine.Object) this.KyokaList, (UnityEngine.Object) this.KyokaListItem) || this.KyokaList.get_transform().IsChildOf(this.KyokaListItem.get_transform())))
        return;
      List<ItemData> items = MonoSingleton<GameManager>.Instance.Player.Items;
      int index1 = 0;
      Transform transform = this.KyokaList.get_transform();
      for (int index2 = 0; index2 < items.Count; ++index2)
      {
        if (items[index2].ItemType == EItemType.ExpUpArtifact)
        {
          ItemData data = new ItemData();
          data.Setup(items[index2].UniqueID, items[index2].Param, items[index2].NumNonCap);
          GameObject root;
          if (this.mKyokaItems.Count <= index1)
          {
            root = (GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) this.KyokaListItem);
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) root, (UnityEngine.Object) null))
            {
              this.mKyokaItems.Add(root);
              root.get_transform().SetParent(transform, false);
              ListItemEvents component = (ListItemEvents) root.GetComponent<ListItemEvents>();
              if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
                component.OnSelect = new ListItemEvents.ListItemEvent(this.OnKyokaItemSelect);
            }
          }
          else
          {
            root = this.mKyokaItems[index1];
            data = this.mTmpItems[index1];
          }
          DataSource.Bind<ItemData>(root, data);
          root.SetActive(true);
          GameParameter.UpdateAll(root);
          if (!this.mTmpItems.Contains(data))
            this.mTmpItems.Add(data);
          ++index1;
        }
      }
      for (; index1 < this.mKyokaItems.Count; ++index1)
        this.mKyokaItems[index1].SetActive(false);
      this.mDisableFlushRequest = true;
    }

    private void OnKyokaItemSelect(GameObject go)
    {
      ItemData dataOfClass = DataSource.FindDataOfClass<ItemData>(go, (ItemData) null);
      if (dataOfClass == null || dataOfClass.Num <= 0)
        return;
      this.RequestUseAddExpItem(dataOfClass, go);
    }

    private void OnArtifactBulkLevelUp(Dictionary<string, int> data)
    {
      List<ItemData> itemDataList = new List<ItemData>();
      using (Dictionary<string, int>.Enumerator enumerator = data.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          KeyValuePair<string, int> current = enumerator.Current;
          string iname = current.Key;
          int num = current.Value;
          ItemData itemData = this.mTmpItems.Find((Predicate<ItemData>) (tmp => tmp.Param.iname == iname));
          if (itemData != null)
          {
            for (int index = 0; index < num; ++index)
              itemDataList.Add(itemData);
          }
        }
      }
      if (itemDataList.Count <= 0)
        return;
      this.RequestUseAddExpItem((IEnumerable<ItemData>) itemDataList, (GameObject) null);
    }

    private void RequestUseAddExpItem(ItemData item, GameObject updataTarget)
    {
      this.RequestUseAddExpItem((IEnumerable<ItemData>) new List<ItemData>()
      {
        item
      }, updataTarget);
    }

    private void RequestUseAddExpItem(IEnumerable<ItemData> itemList, GameObject updataTarget)
    {
      if (this.IsBusy || this.mCurrentArtifact == null)
        return;
      if ((int) this.mCurrentArtifact.Lv >= this.mCurrentArtifact.GetLevelCap())
      {
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ExpPanel, (UnityEngine.Object) null) || this.ExpPanel.IsBusy)
          return;
        UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get((int) this.mCurrentArtifact.Rarity < (int) this.mCurrentArtifact.RarityCap ? "sys.ARTI_EXPFULL1" : "sys.ARTI_EXPFULL2"), (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1);
      }
      else
      {
        if (!this.ExpPanel.IsBusy)
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 151);
        if (!string.IsNullOrEmpty(this.KyokaSE))
          MonoSingleton<MySound>.Instance.PlaySEOneShot(this.KyokaSE, 0.0f);
        ArtifactWindow.RequestAddExp requestAddExp = (ArtifactWindow.RequestAddExp) null;
        if (this.mRequests.Count > 0 && this.mRequests[this.mRequests.Count - 1] is ArtifactWindow.RequestAddExp)
          requestAddExp = this.mRequests[this.mRequests.Count - 1] as ArtifactWindow.RequestAddExp;
        if (requestAddExp == null)
        {
          requestAddExp = new ArtifactWindow.RequestAddExp();
          requestAddExp.UniqueID = (long) this.mCurrentArtifact.UniqueID;
          requestAddExp.Callback = new Network.ResponseCallback(this.AddExpResult);
          this.AddAsyncRequest((ArtifactWindow.Request) requestAddExp);
        }
        if (this.mStatusCache == null)
        {
          this.mStatusCache = new ArtifactWindow.StatusCache();
          UnitData unit = (UnitData) null;
          int job_index = -1;
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.OwnerSlot, (UnityEngine.Object) null))
          {
            JobData job = (JobData) null;
            if (MonoSingleton<GameManager>.Instance.Player.FindOwner(this.mCurrentArtifact, out unit, out job))
              job_index = Array.IndexOf<JobData>(unit.Jobs, job);
          }
          if (this.mOwnerUnitData != null)
          {
            unit = this.mOwnerUnitData;
            job_index = this.mOwnerUnitJobIndex;
          }
          this.mCurrentArtifact.GetHomePassiveBuffStatus(ref this.mStatusCache.BaseAdd, ref this.mStatusCache.BaseMul, (UnitData) null, 0, true);
          this.mCurrentArtifact.GetHomePassiveBuffStatus(ref this.mStatusCache.UnitAdd, ref this.mStatusCache.UnitMul, unit, job_index, true);
        }
        foreach (ItemData itemData in itemList)
        {
          requestAddExp.Items.Add(itemData.Param);
          this.mCurrentArtifact.GainExp(itemData.Param.value);
          itemData.Used(1);
          ++this.mUseEnhanceItemNum;
        }
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) updataTarget, (UnityEngine.Object) null))
          GameParameter.UpdateAll(updataTarget);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ExpPanel, (UnityEngine.Object) null))
          this.ExpPanel.AnimateTo(this.mCurrentArtifact.Exp);
        else
          this.OnKyokaEnd();
      }
    }

    private void AddExpResult(WWWResult www)
    {
      if (Network.IsError)
      {
        if (Network.ErrCode == Network.EErrCode.ArtifactMatShort)
          FlowNode_Network.Back();
        else
          FlowNode_Network.Retry();
      }
      else
      {
        WebAPI.JSON_BodyResponse<Json_PlayerDataAll> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_PlayerDataAll>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        if (jsonObject.body == null)
        {
          FlowNode_Network.Retry();
        }
        else
        {
          int beforeLevel = -1;
          long iid = -1;
          if (this.mCurrentArtifact != null)
          {
            iid = (long) this.mCurrentArtifact.UniqueID;
            ArtifactData artifactByUniqueId = MonoSingleton<GameManager>.Instance.Player.FindArtifactByUniqueID(iid);
            if (artifactByUniqueId != null)
              beforeLevel = (int) artifactByUniqueId.Lv;
          }
          try
          {
            MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.player);
            MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.items);
            MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.artifacts, false);
            MonoSingleton<GameManager>.Instance.Player.UpdateArtifactOwner();
          }
          catch (Exception ex)
          {
            DebugUtility.LogException(ex);
            FlowNode_Network.Retry();
            return;
          }
          if (!Network.IsImmediateMode)
            this.RefreshArtifactList();
          Network.RemoveAPI();
          this.mSendingRequests = false;
          ArtifactData artifactByUniqueId1 = MonoSingleton<GameManager>.Instance.Player.FindArtifactByUniqueID(iid);
          MonoSingleton<GameManager>.Instance.Player.OnArtifactStrength(this.mCurrentArtifactParam.iname, this.mUseEnhanceItemNum, beforeLevel, (int) artifactByUniqueId1.Lv);
          this.mUseEnhanceItemNum = 0;
        }
      }
    }

    public void RefreshArtifactList()
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ArtifactList, (UnityEngine.Object) null))
        return;
      this.ArtifactList.Refresh();
    }

    private void OnArtifactSelect(ArtifactList list)
    {
      object[] selection = list.Selection;
      this.mTotalSellPrice = 0L;
      this.mSelectedArtifacts = selection;
      if (selection.Length <= 0)
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ArtifactSlot, (UnityEngine.Object) null))
          this.ArtifactSlot.SetSlotData<ArtifactData>((ArtifactData) null);
        this.mCurrentArtifact = (ArtifactData) null;
        this.mCurrentArtifactParam = (ArtifactParam) null;
        this.RefreshWindow();
      }
      else
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.SellPrice, (UnityEngine.Object) null))
        {
          for (int index = 0; index < selection.Length; ++index)
          {
            if (selection[index] is ArtifactData)
              this.mTotalSellPrice += (long) ((ArtifactData) selection[index]).GetSellPrice();
            else if (selection[index] is ArtifactParam)
              this.mTotalSellPrice += (long) ((ArtifactParam) selection[index]).sell;
          }
        }
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.DecomposeCostTotal, (UnityEngine.Object) null))
        {
          long num = 0;
          for (int index = 0; index < selection.Length; ++index)
          {
            if (selection[index] is ArtifactData)
              num += (long) (int) ((ArtifactData) selection[index]).RarityParam.ArtifactChangeCost;
          }
          this.DecomposeCostTotal.set_text(num.ToString());
        }
        this.mCurrentArtifact = (ArtifactData) null;
        this.mCurrentArtifactParam = (ArtifactParam) null;
        if (!(selection[0] is ArtifactData))
        {
          if (selection[0] is ArtifactParam)
          {
            this.mCurrentArtifactParam = (ArtifactParam) selection[0];
            if (this.mCurrentArtifactParam == null)
            {
              Debug.LogWarning((object) "Non ArtifactParam Selected");
              return;
            }
          }
          else
          {
            Debug.LogWarning((object) "Non ArtifactData Selected");
            return;
          }
        }
        if (selection[0] is ArtifactData)
        {
          this.mCurrentArtifact = (ArtifactData) selection[0];
          this.mCurrentArtifact = this.mCurrentArtifact.Copy();
          this.mCurrentArtifactParam = this.mCurrentArtifact.ArtifactParam;
          GlobalVars.SelectedArtifactUniqueID.Set((long) this.mCurrentArtifact.UniqueID);
        }
        else
        {
          Json_Artifact json = new Json_Artifact();
          json.iname = this.mCurrentArtifactParam.iname;
          json.rare = this.mCurrentArtifactParam.rareini;
          this.mCurrentArtifact = new ArtifactData();
          this.mCurrentArtifact.Deserialize(json);
        }
        this.Rebind();
        this.RefreshWindow();
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) list, (UnityEngine.Object) null) || list.GetAutoSelected(true))
          return;
        if (this.mCurrentArtifact.CheckEnableRarityUp() == ArtifactData.RarityUpResults.Success)
        {
          if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ProcessToggle_Evolution, (UnityEngine.Object) null))
            return;
          this.ProcessToggle_Evolution.set_isOn(true);
        }
        else
        {
          if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ProcessToggle_Enhance, (UnityEngine.Object) null))
            return;
          this.ProcessToggle_Enhance.set_isOn(true);
        }
      }
    }

    public void RefreshAbilities()
    {
      if (this.mCurrentArtifact == null || UnityEngine.Object.op_Equality((UnityEngine.Object) this.AbilityListItem, (UnityEngine.Object) null))
        return;
      DataSource.Bind<AbilityParam>(this.AbilityListItem, (AbilityParam) null);
      DataSource.Bind<AbilityData>(this.AbilityListItem, (AbilityData) null);
      DataSource.Bind<SkillAbilityDeriveData>(this.AbilityListItem, (SkillAbilityDeriveData) null);
      List<AbilityDeriveParam> abilityDeriveParamList = (List<AbilityDeriveParam>) null;
      if (this.mOwnerUnitData != null)
        abilityDeriveParamList = this.mOwnerUnitData.GetSkillAbilityDeriveData(this.mOwnerUnitData.Jobs[this.mOwnerUnitJobIndex], this.mSelectArtifactSlot, this.mCurrentArtifact.ArtifactParam)?.GetAvailableAbilityDeriveParams();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.OwnerSlot, (UnityEngine.Object) null))
      {
        UnitData unit = (UnitData) null;
        int num = -1;
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.OwnerSlot, (UnityEngine.Object) null))
        {
          JobData job = (JobData) null;
          if (MonoSingleton<GameManager>.Instance.Player.FindOwner(this.mCurrentArtifact, out unit, out job))
            num = Array.IndexOf<JobData>(unit.Jobs, job);
          if (this.mOwnerUnitData != null)
          {
            unit = this.mOwnerUnitData;
            num = this.mOwnerUnitJobIndex;
          }
          if (unit == null || num == -1)
            ;
        }
      }
      MasterParam masterParam = MonoSingleton<GameManager>.Instance.MasterParam;
      ArtifactParam artifactParam = this.mCurrentArtifact.ArtifactParam;
      int index1 = 0;
      List<AbilityData> learningAbilities = this.mCurrentArtifact.LearningAbilities;
      if (artifactParam.abil_inames != null)
      {
        for (int index2 = 0; index2 < artifactParam.abil_inames.Length; ++index2)
        {
          if (!string.IsNullOrEmpty(artifactParam.abil_inames[index2]) && artifactParam.abil_shows[index2] != 0)
          {
            AbilityParam abilityParam = masterParam.GetAbilityParam(artifactParam.abil_inames[index2]);
            if (abilityParam != null)
            {
              GameObject gameObject1;
              if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.AbilityList, (UnityEngine.Object) null))
              {
                if (DataSource.FindDataOfClass<AbilityParam>(this.AbilityListItem, (AbilityParam) null) == null && DataSource.FindDataOfClass<AbilityData>(this.AbilityListItem, (AbilityData) null) == null)
                  DataSource.Bind<AbilityParam>(this.AbilityListItem, abilityParam);
                gameObject1 = this.AbilityListItem;
              }
              else
              {
                if (this.mAbilityItems.Count <= index1)
                {
                  GameObject gameObject2 = (GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) this.AbilityListItem);
                  gameObject2.get_transform().SetParent(this.AbilityList.get_transform(), false);
                  this.mAbilityItems.Add(gameObject2);
                }
                DataSource.Bind<AbilityParam>(this.mAbilityItems[index1], abilityParam);
                this.mAbilityItems[index1].SetActive(true);
                gameObject1 = this.mAbilityItems[index1];
              }
              DataSource.Bind<AbilityData>(gameObject1, (AbilityData) null);
              Animator component = (Animator) gameObject1.GetComponent<Animator>();
              bool flag = false;
              if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null) && learningAbilities != null && !string.IsNullOrEmpty(this.AbilityListItemState))
              {
                if (learningAbilities != null)
                {
                  for (int index3 = 0; index3 < learningAbilities.Count; ++index3)
                  {
                    string iname = learningAbilities[index3].Param.iname;
                    if (artifactParam.abil_inames[index2] == iname)
                    {
                      AbilityData deriveAbility = learningAbilities[index3];
                      if (abilityDeriveParamList != null)
                      {
                        AbilityDeriveParam deriveParam = abilityDeriveParamList.Find((Predicate<AbilityDeriveParam>) (derive_param => derive_param.BaseAbilityIname == iname));
                        if (deriveParam != null)
                          deriveAbility = learningAbilities[index3].CreateDeriveAbility(deriveParam);
                      }
                      DataSource.Bind<AbilityData>(gameObject1, deriveAbility);
                      DataSource.Bind<AbilityParam>(gameObject1, deriveAbility.Param);
                      flag = this.mOwnerUnitData == null || abilityParam.CheckEnableUseAbility(this.mOwnerUnitData, this.mOwnerUnitJobIndex);
                      break;
                    }
                  }
                }
                if (flag)
                  component.SetInteger(this.AbilityListItemState, this.AbilityListItem_Unlocked);
                else
                  component.SetInteger(this.AbilityListItemState, this.AbilityListItem_Hidden);
              }
              ++index1;
              if (flag)
                break;
            }
          }
        }
      }
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.AbilityList, (UnityEngine.Object) null) && index1 == 0)
      {
        DataSource.Bind<AbilityParam>(this.AbilityListItem, (AbilityParam) null);
        DataSource.Bind<AbilityData>(this.AbilityListItem, (AbilityData) null);
        Animator component = (Animator) this.AbilityListItem.GetComponent<Animator>();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null) && !string.IsNullOrEmpty(this.AbilityListItemState))
          component.SetInteger(this.AbilityListItemState, this.AbilityListItem_Hidden);
      }
      for (; index1 < this.mAbilityItems.Count; ++index1)
        this.mAbilityItems[index1].SetActive(false);
    }

    public void ShowSelection()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.SelectionListItem, (UnityEngine.Object) null))
        return;
      for (int index = 0; index < this.mSelectionItems.Count; ++index)
        UnityEngine.Object.Destroy((UnityEngine.Object) ((Component) this.mSelectionItems[index]).get_gameObject());
      this.mSelectionItems.Clear();
      if (this.mSelectedArtifacts == null)
        return;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.SelectionNum, (UnityEngine.Object) null))
        this.SelectionNum.set_text(this.mSelectedArtifacts.Length.ToString());
      Transform transform = !UnityEngine.Object.op_Equality((UnityEngine.Object) this.SelectionList, (UnityEngine.Object) null) ? this.SelectionList.get_transform() : ((Component) this.SelectionListItem).get_transform().get_parent();
      for (int index = 0; index < this.mSelectedArtifacts.Length; ++index)
      {
        ArtifactIcon artifactIcon = (ArtifactIcon) UnityEngine.Object.Instantiate<ArtifactIcon>((M0) this.SelectionListItem);
        this.mSelectionItems.Add(artifactIcon);
        DataSource.Bind(((Component) artifactIcon).get_gameObject(), this.mSelectedArtifacts[index].GetType(), this.mSelectedArtifacts[index]);
        ((Component) artifactIcon).get_transform().SetParent(transform, false);
        ((Component) artifactIcon).get_gameObject().SetActive(true);
      }
    }

    public void RefreshWindow()
    {
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      UnitData unit = (UnitData) null;
      int job_index = 0;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.OwnerSlot, (UnityEngine.Object) null) && this.mCurrentArtifact != null)
      {
        JobData job = (JobData) null;
        if (player.FindOwner(this.mCurrentArtifact, out unit, out job))
          job_index = Array.IndexOf<JobData>(unit.Jobs, job);
      }
      if (this.mOwnerUnitData != null)
      {
        unit = this.mOwnerUnitData;
        job_index = this.mOwnerUnitJobIndex;
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Status, (UnityEngine.Object) null))
      {
        if (this.mCurrentArtifact != null)
        {
          BaseStatus fixed_status1 = new BaseStatus();
          BaseStatus scale_status1 = new BaseStatus();
          this.mCurrentArtifact.GetHomePassiveBuffStatus(ref fixed_status1, ref scale_status1, (UnitData) null, 0, true);
          BaseStatus fixed_status2 = new BaseStatus();
          BaseStatus scale_status2 = new BaseStatus();
          this.mCurrentArtifact.GetHomePassiveBuffStatus(ref fixed_status2, ref scale_status2, unit, job_index, true);
          this.Status.SetValues(fixed_status1, scale_status1, fixed_status2, scale_status2, false);
        }
        else
        {
          BaseStatus baseStatus = new BaseStatus();
          this.Status.SetValues(baseStatus, baseStatus, false);
        }
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.OwnerSlot, (UnityEngine.Object) null))
      {
        this.OwnerSlot.SetSlotData<UnitData>(unit);
        if (unit != null && job_index != -1)
          DataSource.Bind<JobData>(((Component) this.OwnerSlot).get_gameObject(), unit.Jobs[job_index]);
        else
          DataSource.Bind<JobData>(((Component) this.OwnerSlot).get_gameObject(), (JobData) null);
      }
      this.RefreshKyokaList();
      this.RefreshDecomposeInfo();
      this.RefreshTransmuteInfo();
      this.RefreshRarityUpInfo();
      this.RefreshAbilities();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.DetailMaterial, (UnityEngine.Object) null) && this.mCurrentArtifact != null)
        this.DetailMaterial.SetMaterial(0, this.mCurrentArtifact.Kakera);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.KyokaPanel, (UnityEngine.Object) null) && this.mCurrentArtifact != null)
      {
        Animator component = (Animator) this.KyokaPanel.GetComponent<Animator>();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null) && !string.IsNullOrEmpty(this.KyokaPanelState))
        {
          if ((int) this.mCurrentArtifact.Lv >= (int) this.mCurrentArtifact.LvCap)
          {
            if ((int) this.mCurrentArtifact.Rarity < (int) this.mCurrentArtifact.RarityCap)
              component.SetInteger(this.KyokaPanelState, this.KyokaPanel_LvCapped);
            else
              component.SetInteger(this.KyokaPanelState, this.KyokaPanel_LvMax);
          }
          else
            component.SetInteger(this.KyokaPanelState, this.KyokaPanel_Enable);
        }
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.SellPrice, (UnityEngine.Object) null))
        this.SellPrice.set_text(this.mTotalSellPrice.ToString());
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.RarityUpCost, (UnityEngine.Object) null) && this.mCurrentArtifact != null)
      {
        Animator component = (Animator) ((Component) this.RarityUpCost).GetComponent<Animator>();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null) && !string.IsNullOrEmpty(this.RarityUpCostState))
        {
          if ((int) this.mCurrentArtifact.RarityParam.ArtifactRarityUpCost <= player.Gold)
            component.SetInteger(this.RarityUpCostState, this.RarityUpCost_Normal);
          else
            component.SetInteger(this.RarityUpCostState, this.RarityUpCost_NoGold);
        }
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ExpPanel, (UnityEngine.Object) null) && this.mCurrentArtifact != null)
      {
        this.ExpPanel.LevelCap = this.mCurrentArtifact.GetLevelCap();
        this.ExpPanel.Exp = this.mCurrentArtifact.Exp;
      }
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.WindowBody, (UnityEngine.Object) null))
        return;
      GameParameter.UpdateAll(this.WindowBody.get_gameObject());
    }

    private void OnLevelChange(int prev, int next)
    {
    }

    private void OnKyokaEnd()
    {
      UnitData unit = (UnitData) null;
      int job_index = 0;
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.OwnerSlot, (UnityEngine.Object) null))
      {
        JobData job = (JobData) null;
        if (player.FindOwner(this.mCurrentArtifact, out unit, out job))
          job_index = Array.IndexOf<JobData>(unit.Jobs, job);
      }
      if (this.mOwnerUnitData != null)
      {
        unit = this.mOwnerUnitData;
        job_index = this.mOwnerUnitJobIndex;
      }
      BaseStatus fixed_status1 = new BaseStatus();
      BaseStatus scale_status1 = new BaseStatus();
      this.mCurrentArtifact.GetHomePassiveBuffStatus(ref fixed_status1, ref scale_status1, (UnitData) null, 0, true);
      BaseStatus fixed_status2 = new BaseStatus();
      BaseStatus scale_status2 = new BaseStatus();
      this.mCurrentArtifact.GetHomePassiveBuffStatus(ref fixed_status2, ref scale_status2, unit, job_index, true);
      this.RefreshWindow();
      this.RefreshArtifactList();
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 152);
    }

    public void AddRarity()
    {
      if (this.IsBusy || this.mCurrentArtifact == null || (int) this.mCurrentArtifact.Rarity >= (int) this.mCurrentArtifact.RarityCap || UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ExpPanel, (UnityEngine.Object) null) && this.ExpPanel.IsBusy)
        return;
      ArtifactData.RarityUpResults rarityUpResults = this.mCurrentArtifact.CheckEnableRarityUp();
      if (rarityUpResults == ArtifactData.RarityUpResults.Success)
      {
        UIUtility.ConfirmBox(LocalizedText.Get("sys.ARTI_RARITYUP_CONFIRM", new object[1]
        {
          (object) this.mCurrentArtifactParam.name
        }), new UIUtility.DialogResultEvent(this.OnAddRarityAccept), new UIUtility.DialogResultEvent(this.OnAddRarityCancel), (GameObject) null, false, -1, (string) null, (string) null);
      }
      else
      {
        string key = (string) null;
        if ((rarityUpResults & ArtifactData.RarityUpResults.RarityMaxed) != ArtifactData.RarityUpResults.Success)
          key = "sys.ARTI_RARITYUP_MAX";
        else if ((rarityUpResults & ArtifactData.RarityUpResults.NoLv) != ArtifactData.RarityUpResults.Success)
          key = "sys.ARTI_RARITYUP_NOLV";
        else if ((rarityUpResults & ArtifactData.RarityUpResults.NoGold) != ArtifactData.RarityUpResults.Success)
          key = "sys.ARTI_RARITYUP_NOGOLD";
        else if ((rarityUpResults & ArtifactData.RarityUpResults.NoKakera) != ArtifactData.RarityUpResults.Success)
          key = "sys.ARTI_RARITYUP_NOMTRL";
        if (key == null)
          return;
        UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get(key), (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1);
      }
    }

    private void OnAddRarityAccept(GameObject go)
    {
      int levelCap1 = this.mCurrentArtifact.GetLevelCap();
      List<AbilityData> abilityDataList1 = new List<AbilityData>(4);
      List<int> intList = new List<int>(4);
      List<AbilityData> abilityDataList2 = new List<AbilityData>(4);
      ArtifactParam artifactParam = this.mCurrentArtifact.ArtifactParam;
      if (artifactParam.abil_shows != null && this.mCurrentArtifact.LearningAbilities != null)
      {
        for (int index = 0; index < this.mCurrentArtifact.LearningAbilities.Count; ++index)
        {
          string iname = this.mCurrentArtifact.LearningAbilities[index].Param.iname;
          if (Array.IndexOf((Array) artifactParam.abil_shows, (object) iname) >= 0)
          {
            abilityDataList1.Add(this.mCurrentArtifact.LearningAbilities[index]);
            intList.Add(this.mCurrentArtifact.LearningAbilities[index].Rank);
          }
        }
      }
      this.mCurrentArtifact.RarityUp();
      if (artifactParam.abil_shows != null && this.mCurrentArtifact.LearningAbilities != null)
      {
        for (int index = 0; index < this.mCurrentArtifact.LearningAbilities.Count; ++index)
        {
          if (!abilityDataList1.Contains(this.mCurrentArtifact.LearningAbilities[index]))
          {
            string iname = this.mCurrentArtifact.LearningAbilities[index].Param.iname;
            if (Array.IndexOf((Array) artifactParam.abil_shows, (object) iname) >= 0)
              abilityDataList2.Add(this.mCurrentArtifact.LearningAbilities[index]);
          }
        }
      }
      int levelCap2 = this.mCurrentArtifact.GetLevelCap();
      int num = 0;
      this.mChangeSet = new List<ChangeListData>(4);
      this.mChangeSet.Add(new ChangeListData());
      this.mChangeSet[0].ItemID = this.ArtifactDataID;
      this.mChangeSet[0].Label = LocalizedText.Get("sys.ARTI_MAXRULV");
      this.mChangeSet[0].ValOld = levelCap1.ToString();
      this.mChangeSet[0].ValNew = levelCap2.ToString();
      int index1 = num + 1;
      for (int index2 = 0; index2 < abilityDataList1.Count; ++index2)
      {
        if (abilityDataList1[index2].Rank != intList[index2])
        {
          this.mChangeSet.Add(new ChangeListData());
          this.mChangeSet[index1].ItemID = this.ChangedAbilityID;
          this.mChangeSet[index1].MetaDataType = typeof (AbilityData);
          this.mChangeSet[index1].MetaData = (object) abilityDataList1[index2];
          this.mChangeSet[index1].ValOld = intList[index2].ToString();
          this.mChangeSet[index1].ValNew = abilityDataList1[index2].Rank.ToString();
          ++index1;
        }
      }
      for (int index2 = 0; index2 < abilityDataList2.Count; ++index2)
      {
        if (abilityDataList1[index2].Rank != intList[index2])
        {
          this.mChangeSet.Add(new ChangeListData());
          this.mChangeSet[index1].ItemID = this.NewAbilityID;
          this.mChangeSet[index1].MetaDataType = typeof (AbilityData);
          this.mChangeSet[index1].MetaData = (object) abilityDataList2[index2];
          this.mChangeSet[index1].ValOld = abilityDataList1[index2].Rank.ToString();
          this.mChangeSet[index1].ValNew = intList[index2].ToString();
          ++index1;
        }
      }
      GlobalEvent.Invoke(PredefinedGlobalEvents.REFRESH_GOLD_STATUS.ToString(), (object) this);
      this.StartCoroutine(this.AddRareAsync());
      MonoSingleton<GameManager>.Instance.Player.OnArtifactEvolution(this.mCurrentArtifactParam.iname);
      string trophy_progs;
      string bingo_progs;
      MonoSingleton<GameManager>.Instance.ServerSyncTrophyExecStart(out trophy_progs, out bingo_progs);
      ArtifactWindow.RequestAddRare requestAddRare = new ArtifactWindow.RequestAddRare();
      requestAddRare.UniqueID = (long) this.mCurrentArtifact.UniqueID;
      requestAddRare.trophyprog = trophy_progs;
      requestAddRare.bingoprog = bingo_progs;
      requestAddRare.Callback = new Network.ResponseCallback(this.AddRarityResult);
      this.AddAsyncRequest((ArtifactWindow.Request) requestAddRare);
      this.mBusy = true;
      this.FlushRequests(false);
    }

    private void OnAddRarityCancel(GameObject go)
    {
    }

    [DebuggerHidden]
    private IEnumerator AddRareAsync()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new ArtifactWindow.\u003CAddRareAsync\u003Ec__IteratorE4()
      {
        \u003C\u003Ef__this = this
      };
    }

    private void AddAsyncRequest(ArtifactWindow.Request req)
    {
      this.mRequests.Add(req);
      this.mFlushTimer = this.AutoFlushRequests;
    }

    private void AddRarityResult(WWWResult www)
    {
      if (Network.IsError)
      {
        Network.EErrCode errCode = Network.ErrCode;
        FlowNode_Network.Retry();
      }
      else
      {
        WebAPI.JSON_BodyResponse<Json_PlayerDataAll> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_PlayerDataAll>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        if (jsonObject.body == null)
        {
          FlowNode_Network.Retry();
        }
        else
        {
          try
          {
            MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.player);
            MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.items);
            MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.artifacts, true);
            MonoSingleton<GameManager>.Instance.Player.UpdateArtifactOwner();
          }
          catch (Exception ex)
          {
            DebugUtility.LogException(ex);
            FlowNode_Network.Retry();
            return;
          }
          if (!Network.IsImmediateMode)
          {
            this.SyncArtifactData();
            this.RefreshWindow();
            this.RefreshArtifactList();
          }
          this.mBusy = false;
          Network.RemoveAPI();
          MonoSingleton<GameManager>.Instance.ServerSyncTrophyExecEnd(www);
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 201);
        }
      }
    }

    public void RefreshDecomposeInfo()
    {
      if (this.mCurrentArtifact == null || (long) this.mCurrentArtifact.UniqueID == 0L)
        return;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.DecomposePanel, (UnityEngine.Object) null) && !string.IsNullOrEmpty(this.DecomposePanelState))
      {
        Animator component = (Animator) this.DecomposePanel.GetComponent<Animator>();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
        {
          if (this.mCurrentArtifact.Kakera != null)
            component.SetInteger(this.DecomposePanelState, this.DecomposePanel_Normal);
          else
            component.SetInteger(this.DecomposePanelState, this.DecomposePanel_Disabled);
        }
      }
      if (this.mCurrentArtifact.Kakera != null)
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.DecomposeHelp, (UnityEngine.Object) null))
          this.DecomposeHelp.set_text(LocalizedText.Get("sys.ARTI_DECOMPOSE_HELP", (object) this.mCurrentArtifact.ArtifactParam.name, (object) this.mCurrentArtifact.Kakera.name, (object) this.mCurrentArtifact.GetKakeraChangeNum()));
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.DecomposeCost, (UnityEngine.Object) null))
        {
          long num = (long) Math.Abs((int) this.mCurrentArtifact.RarityParam.ArtifactChangeCost);
          this.DecomposeCost.set_text(num.ToString());
          if (!string.IsNullOrEmpty(this.DecomposeCostState))
          {
            Animator component = (Animator) ((Component) this.DecomposeCost).GetComponent<Animator>();
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
            {
              if ((long) MonoSingleton<GameManager>.Instance.Player.Gold >= num)
                component.SetInteger(this.DecomposeCostState, this.DecomposeCost_Normal);
              else
                component.SetInteger(this.DecomposeCostState, this.DecomposeCost_NoGold);
            }
          }
        }
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.DecomposeItem, (UnityEngine.Object) null))
          this.DecomposeItem.SetMaterial(this.mCurrentArtifact.GetKakeraChangeNum(), this.mCurrentArtifact.Kakera);
        int itemAmount = MonoSingleton<GameManager>.Instance.Player.GetItemAmount(this.mCurrentArtifact.Kakera.iname);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.DecomposeKakeraNumOld, (UnityEngine.Object) null))
          this.DecomposeKakeraNumOld.set_text(itemAmount.ToString());
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.DecomposeKakeraNumNew, (UnityEngine.Object) null))
          this.DecomposeKakeraNumNew.set_text(Mathf.Min(itemAmount + this.mCurrentArtifact.GetKakeraChangeNum(), this.mCurrentArtifact.Kakera.cap).ToString());
      }
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.DecomposeWarning, (UnityEngine.Object) null))
        return;
      bool flag = false;
      if (this.mSelectedArtifacts != null)
      {
        for (int index = 0; index < this.mSelectedArtifacts.Length; ++index)
        {
          if (this.mSelectedArtifacts[index] is ArtifactData && (int) ((ArtifactData) this.mSelectedArtifacts[index]).Rarity >= this.DecomposeWarningRarity)
          {
            flag = true;
            break;
          }
        }
      }
      this.DecomposeWarning.get_gameObject().SetActive(flag);
    }

    public void RefreshTransmuteInfo()
    {
      if (this.mCurrentArtifactParam == null)
        return;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TransmutePanel, (UnityEngine.Object) null))
      {
        Animator component = (Animator) this.TransmutePanel.GetComponent<Animator>();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null) && !string.IsNullOrEmpty(this.TransmutePanelState))
        {
          if (this.mCurrentArtifactParam.is_create)
            component.SetInteger(this.TransmutePanelState, this.TransmutePanel_Normal);
          else
            component.SetInteger(this.TransmutePanelState, this.TransmutePanel_Disabled);
        }
      }
      GameManager instance = MonoSingleton<GameManager>.Instance;
      RarityParam rarityParam = instance.MasterParam.GetRarityParam(this.mCurrentArtifactParam.rareini);
      ItemParam itemParam = instance.GetItemParam(this.mCurrentArtifactParam.kakera);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TransmuteMaterial, (UnityEngine.Object) null))
        this.TransmuteMaterial.SetMaterial((int) rarityParam.ArtifactCreatePieceNum, itemParam);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TransmuteCost, (UnityEngine.Object) null))
        this.TransmuteCost.set_text((int) rarityParam.ArtifactCreateCost.ToString());
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TransmuteCondition, (UnityEngine.Object) null))
        return;
      int kakeraCreateNum = this.mCurrentArtifact.GetKakeraCreateNum();
      if (itemParam == null || kakeraCreateNum <= 0)
        return;
      this.TransmuteCondition.set_text(LocalizedText.Get("sys.ARTI_TRANSMUTE_REQ", (object) itemParam.name, (object) kakeraCreateNum));
    }

    public void RefreshRarityUpInfo()
    {
      if (this.mCurrentArtifact == null)
        return;
      bool flag = this.mCurrentArtifact.CheckEnableRarityUp() == ArtifactData.RarityUpResults.Success;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.RarityUpPanel, (UnityEngine.Object) null))
      {
        Animator component = (Animator) this.RarityUpPanel.GetComponent<Animator>();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null) && !string.IsNullOrEmpty(this.RarityUpPanelState))
        {
          if ((int) this.mCurrentArtifact.Rarity < (int) this.mCurrentArtifact.RarityCap)
          {
            bool hasKakera = false;
            this.mCurrentArtifact.GetKakeraDataListForRarityUp().ForEach((Action<ItemData>) (kakera =>
            {
              if (kakera.Num <= 0)
                return;
              hasKakera = true;
            }));
            if (hasKakera)
              component.SetInteger(this.RarityUpPanelState, this.RarityUpPanel_Normal);
            else
              component.SetInteger(this.RarityUpPanelState, this.RarityUpPanel_NoItem);
          }
          else
            component.SetInteger(this.RarityUpPanelState, this.RarityUpPanel_MaxRarity);
        }
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.RarityUpButton, (UnityEngine.Object) null))
        ((Selectable) this.RarityUpButton).set_interactable(flag);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.RarityUpHilit, (UnityEngine.Object) null))
        this.RarityUpHilit.SetActive(flag);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.RarityUpIconRoot, (UnityEngine.Object) null))
        this.RarityUpIconRoot.SetActive(flag);
      if ((int) this.mCurrentArtifact.Rarity >= (int) this.mCurrentArtifact.RarityCap)
        return;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.RarityUpCost, (UnityEngine.Object) null))
        this.RarityUpCost.set_text((int) this.mCurrentArtifact.RarityParam.ArtifactRarityUpCost.ToString());
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.RarityUpIconRoot, (UnityEngine.Object) null))
        this.RarityUpIconRoot.SetActive(flag);
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.RarityUpList, (UnityEngine.Object) null) || !UnityEngine.Object.op_Inequality((UnityEngine.Object) this.RarityUpListItem, (UnityEngine.Object) null))
        return;
      List<ItemData> dataListForRarityUp = this.mCurrentArtifact.GetKakeraDataListForRarityUp();
      int index1 = 0;
      Transform transform = this.RarityUpList.get_transform();
      int num = Mathf.Max(dataListForRarityUp.Count, this.mRarityUpItems.Count);
      int kakeraNeedNum = this.mCurrentArtifact.GetKakeraNeedNum();
      for (int index2 = 0; index2 < num; ++index2)
      {
        GameObject root;
        if (this.mRarityUpItems.Count <= index1)
        {
          root = (GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) this.RarityUpListItem);
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) root, (UnityEngine.Object) null))
          {
            this.mRarityUpItems.Add(root);
            root.get_transform().SetParent(transform, false);
          }
        }
        else
          root = this.mRarityUpItems[index1];
        if (index2 < dataListForRarityUp.Count)
        {
          DataSource.Bind<ItemData>(root, dataListForRarityUp[index2]);
          int reqNum = Mathf.Min(kakeraNeedNum, dataListForRarityUp[index2].Num);
          if (reqNum > 0)
          {
            MaterialPanel component = (MaterialPanel) root.GetComponent<MaterialPanel>();
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
              component.SetMaterial(reqNum, dataListForRarityUp[index2].Param);
            root.SetActive(true);
          }
          else
            root.SetActive(false);
          kakeraNeedNum -= Mathf.Min(kakeraNeedNum, dataListForRarityUp[index2].Num);
          GameParameter.UpdateAll(root);
          ++index1;
        }
      }
      for (; index1 < this.mRarityUpItems.Count; ++index1)
        this.mRarityUpItems[index1].SetActive(false);
      this.mDisableFlushRequest = true;
    }

    public void TransmuteArtifact()
    {
      if (this.IsBusy || this.mCurrentArtifactParam == null)
        return;
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      int num1 = 0;
      List<ArtifactData> artifacts = player.Artifacts;
      for (int index = 0; index < artifacts.Count; ++index)
      {
        if (artifacts[index].ArtifactParam == this.mCurrentArtifactParam)
          ++num1;
      }
      if (num1 >= this.mCurrentArtifactParam.maxnum)
      {
        UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get("sys.ARTI_TRANSMUTE_MAXNUM", (object) this.mCurrentArtifactParam.name, (object) this.mCurrentArtifactParam.maxnum), (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1);
      }
      else
      {
        string kakera = this.mCurrentArtifactParam.kakera;
        ItemData itemDataByItemId = player.FindItemDataByItemID(kakera);
        int num2 = itemDataByItemId == null ? 0 : itemDataByItemId.Num;
        RarityParam rarityParam = MonoSingleton<GameManager>.Instance.GetRarityParam(this.mCurrentArtifactParam.rareini);
        if (num2 < (int) rarityParam.ArtifactCreatePieceNum)
          UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get("sys.ARTI_TRANSMUTE_NOKAKERA", (object) this.mCurrentArtifactParam.name, (object) rarityParam.ArtifactCreatePieceNum, (object) ((int) rarityParam.ArtifactCreatePieceNum - num2), (object) num2), (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1);
        else if (player.Gold < (int) rarityParam.ArtifactCreateCost)
          UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get("sys.ARTI_TRANSMUTE_NOGOLD", (object) this.mCurrentArtifactParam.name, (object) rarityParam.ArtifactCreateCost, (object) ((int) rarityParam.ArtifactCreateCost - player.Gold), (object) player.Gold), (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1);
        else
          UIUtility.ConfirmBox(LocalizedText.Get("sys.ARTI_TRANSMUTE_CONFIRM", new object[1]
          {
            (object) this.mCurrentArtifactParam.name
          }), new UIUtility.DialogResultEvent(this.OnTransmuteAccept), new UIUtility.DialogResultEvent(this.OnTransmuteCancel), (GameObject) null, false, -1, (string) null, (string) null);
      }
    }

    private void OnTransmuteAccept(GameObject go)
    {
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      RarityParam rarityParam = MonoSingleton<GameManager>.Instance.GetRarityParam(this.mCurrentArtifactParam.rareini);
      this.mCachedArtifacts = player.Artifacts.ToArray();
      player.GainGold(-(int) rarityParam.ArtifactCreateCost);
      GlobalEvent.Invoke(PredefinedGlobalEvents.REFRESH_GOLD_STATUS.ToString(), (object) this);
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 301);
      if (Network.Mode == Network.EConnectMode.Online)
      {
        player.OnArtifactTransmute(this.mCurrentArtifactParam.iname);
        string trophy_progs;
        string bingo_progs;
        MonoSingleton<GameManager>.Instance.ServerSyncTrophyExecStart(out trophy_progs, out bingo_progs);
        Network.RequestAPI((WebAPI) new ReqArtifactAdd(this.mCurrentArtifactParam.iname, new Network.ResponseCallback(this.OnTransmuteResult), trophy_progs, bingo_progs), false);
      }
      else
      {
        ItemData itemDataByItemId = player.FindItemDataByItemID(this.mCurrentArtifactParam.kakera);
        if (itemDataByItemId != null)
          itemDataByItemId.Used((int) rarityParam.ArtifactCreatePieceNum);
        Json_Artifact[] json = new Json_Artifact[1]
        {
          new Json_Artifact()
        };
        json[0].iid = (long) Random.Range(1, int.MaxValue);
        json[0].iname = this.mCurrentArtifactParam.iname;
        MonoSingleton<GameManager>.Instance.Deserialize(json, false);
        this.ShowTransmuteResult();
        player.OnArtifactTransmute(this.mCurrentArtifactParam.iname);
      }
    }

    private void OnTransmuteCancel(GameObject go)
    {
    }

    private void OnTransmuteResult(WWWResult www)
    {
      if (Network.IsError)
      {
        switch (Network.ErrCode)
        {
          case Network.EErrCode.ArtifactBoxLimit:
            FlowNode_Network.Back();
            UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get("sys.ARTI_FULL"), (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1);
            break;
          case Network.EErrCode.ArtifactPieceShort:
            UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get("sys.ARTI_NOKAKERA_ERR"), (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1);
            FlowNode_Network.Back();
            break;
          default:
            FlowNode_Network.Retry();
            break;
        }
      }
      else
      {
        WebAPI.JSON_BodyResponse<Json_PlayerDataAll> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_PlayerDataAll>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        if (jsonObject.body == null)
        {
          FlowNode_Network.Retry();
        }
        else
        {
          GameManager instance = MonoSingleton<GameManager>.Instance;
          try
          {
            instance.Deserialize(jsonObject.body.player);
            instance.Deserialize(jsonObject.body.items);
            instance.Deserialize(jsonObject.body.units);
            instance.Deserialize(jsonObject.body.artifacts, false);
          }
          catch (Exception ex)
          {
            DebugUtility.LogException(ex);
            FlowNode_Network.Retry();
            return;
          }
          Network.RemoveAPI();
          MonoSingleton<GameManager>.Instance.ServerSyncTrophyExecEnd(www);
          if (Network.IsImmediateMode)
            return;
          this.ShowTransmuteResult();
        }
      }
    }

    private void ShowTransmuteResult()
    {
      List<ArtifactData> artifacts = MonoSingleton<GameManager>.Instance.Player.Artifacts;
      List<ChangeListData> changeListDataList = new List<ChangeListData>(4);
      for (int index1 = 0; index1 < artifacts.Count; ++index1)
      {
        bool flag = false;
        for (int index2 = 0; index2 < this.mCachedArtifacts.Length; ++index2)
        {
          if ((long) artifacts[index1].UniqueID == (long) this.mCachedArtifacts[index2].UniqueID)
          {
            flag = true;
            break;
          }
        }
        if (!flag)
          changeListDataList.Add(new ChangeListData()
          {
            ItemID = this.ArtifactDataID,
            MetaData = (object) artifacts[index1],
            MetaDataType = typeof (ArtifactData)
          });
      }
      this.RefreshArtifactList();
      this.StartCoroutine(this.ShowTransmuteResultAsync(changeListDataList.ToArray()));
    }

    [DebuggerHidden]
    private IEnumerator ShowTransmuteResultAsync(ChangeListData[] changeset)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new ArtifactWindow.\u003CShowTransmuteResultAsync\u003Ec__IteratorE5()
      {
        changeset = changeset,
        \u003C\u0024\u003Echangeset = changeset,
        \u003C\u003Ef__this = this
      };
    }

    private ArtifactData[] GetSelectedArtifacts()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.ArtifactList, (UnityEngine.Object) null))
        return new ArtifactData[0];
      object[] selection = this.ArtifactList.Selection;
      List<ArtifactData> artifactDataList = new List<ArtifactData>(selection.Length);
      for (int index = 0; index < selection.Length; ++index)
      {
        if (selection[index] is ArtifactData && !artifactDataList.Contains(selection[index] as ArtifactData))
          artifactDataList.Add(selection[index] as ArtifactData);
      }
      return artifactDataList.ToArray();
    }

    private long CalcDecomposeCost(ArtifactData[] artifacts)
    {
      long num = 0;
      for (int index = 0; index < artifacts.Length; ++index)
      {
        if (artifacts[index].Kakera != null)
          num += (long) (int) artifacts[index].RarityParam.ArtifactChangeCost;
      }
      return num;
    }

    public void DecomposeArtifact()
    {
      if (this.IsBusy || this.mSelectedArtifacts == null && this.mSelectedArtifacts.Length < 1)
        return;
      int num = 0;
      for (int index = 0; index < this.mSelectedArtifacts.Length; ++index)
      {
        ArtifactData selectedArtifact = (ArtifactData) this.mSelectedArtifacts[index];
        if (selectedArtifact == null || (long) selectedArtifact.UniqueID == 0L)
          return;
        if (selectedArtifact.Kakera == null)
        {
          UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get("sys.ARTI_DECOMPOSE_NODEC"), (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1);
          return;
        }
        num += Math.Abs((int) selectedArtifact.RarityParam.ArtifactChangeCost);
      }
      if (MonoSingleton<GameManager>.Instance.Player.Gold < num)
      {
        UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get("sys.ARTI_DECOMPOSE_NOGOLD"), (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1);
      }
      else
      {
        this.mConfirmDialogGo = UIUtility.ConfirmBox(LocalizedText.Get("sys.ARTI_DECOMPOSE_CONFIRM"), new UIUtility.DialogResultEvent(this.OnDecomposeRarityCheck), new UIUtility.DialogResultEvent(this.OnDecomposeCancel), (GameObject) null, false, -1, (string) null, (string) null);
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mConfirmDialogGo, (UnityEngine.Object) null))
          return;
        Win_Btn_DecideCancel_FL_C component = (Win_Btn_DecideCancel_FL_C) this.mConfirmDialogGo.GetComponent<Win_Btn_DecideCancel_FL_C>();
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
          return;
        component.AutoClose = false;
      }
    }

    private void OnDecomposeRarityCheck(GameObject go)
    {
      bool flag = false;
      ArtifactData[] selectedArtifacts = this.GetSelectedArtifacts();
      for (int index = 0; index < selectedArtifacts.Length; ++index)
      {
        if ((int) selectedArtifacts[index].Rarity + 1 >= this.RarityCheckValue)
        {
          flag = true;
          break;
        }
      }
      if (flag)
      {
        GameObject gameObject1 = AssetManager.Load<GameObject>(ArtifactWindow.ARTIFACT_RARITY_CHECK_UI_PATH);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) gameObject1, (UnityEngine.Object) null))
        {
          GameObject gameObject2 = (GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) gameObject1);
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) gameObject2, (UnityEngine.Object) null))
          {
            ArtifactRarityCheck componentInChildren = (ArtifactRarityCheck) gameObject2.GetComponentInChildren<ArtifactRarityCheck>();
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) componentInChildren, (UnityEngine.Object) null))
            {
              componentInChildren.Setup(ArtifactRarityCheck.Type.DECOMPOSE, go, selectedArtifacts, this.RarityCheckValue);
              componentInChildren.OnDecideEvent = new ArtifactRarityCheck.OnArtifactRarityCheckDecideEvent(this.OnDecomposeAccept);
              componentInChildren.OnCancelEvent = new ArtifactRarityCheck.OnArtifactRarityCheckCancelEvent(this.OnDecomposeCancel);
              return;
            }
          }
        }
      }
      this.OnDecomposeAccept(go);
    }

    private void OnDecomposeAccept(GameObject go)
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mConfirmDialogGo, (UnityEngine.Object) null))
      {
        Win_Btn_DecideCancel_FL_C component = (Win_Btn_DecideCancel_FL_C) this.mConfirmDialogGo.GetComponent<Win_Btn_DecideCancel_FL_C>();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null) && !component.AutoClose)
          component.BeginClose();
      }
      GameManager instance = MonoSingleton<GameManager>.Instance;
      bool flag = false;
      for (int index = 0; index < this.mSelectedArtifacts.Length; ++index)
      {
        ArtifactData selectedArtifact = (ArtifactData) this.mSelectedArtifacts[index];
        UnitData unit;
        JobData job;
        if (instance.Player.FindOwner(selectedArtifact, out unit, out job))
        {
          flag = true;
          break;
        }
      }
      if (flag)
        UIUtility.ConfirmBox(LocalizedText.Get("sys.ARTI_DECOMPOSE_CONFIRM2"), new UIUtility.DialogResultEvent(this.OnDecomposeAccept2), new UIUtility.DialogResultEvent(this.OnDecomposeCancel), (GameObject) null, false, -1, (string) null, (string) null);
      else
        this.OnDecomposeAccept2(go);
    }

    private void OnDecomposeAccept2(GameObject go)
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      int num = 0;
      for (int index = 0; index < this.mSelectedArtifacts.Length; ++index)
      {
        ArtifactData selectedArtifact = (ArtifactData) this.mSelectedArtifacts[index];
        num += Math.Abs((int) selectedArtifact.RarityParam.ArtifactChangeCost);
      }
      instance.Player.GainGold(-num);
      GlobalEvent.Invoke(PredefinedGlobalEvents.REFRESH_GOLD_STATUS.ToString(), (object) this);
      if (!string.IsNullOrEmpty(this.DecomposeSE))
        MonoSingleton<MySound>.Instance.PlaySEOneShot(this.DecomposeSE, 0.0f);
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 402);
      if (Network.Mode == Network.EConnectMode.Online)
      {
        long[] artifact_iids;
        if (this.mSelectedArtifacts != null && this.mSelectedArtifacts.Length > 0)
        {
          artifact_iids = new long[this.mSelectedArtifacts.Length];
          for (int index = 0; index < artifact_iids.Length; ++index)
            artifact_iids[index] = (long) ((ArtifactData) this.mSelectedArtifacts[index]).UniqueID;
        }
        else
          artifact_iids = new long[1]
          {
            (long) this.mCurrentArtifact.UniqueID
          };
        Network.RequestAPI((WebAPI) new ReqArtifactConvert(artifact_iids, new Network.ResponseCallback(this.OnDecomposeResult)), false);
      }
      else
      {
        Dictionary<ItemParam, int> itemSnapshot = instance.Player.CreateItemSnapshot();
        instance.Player.GainItem(this.mCurrentArtifact.Kakera.iname, (int) this.mCurrentArtifact.RarityParam.ArtifactChangePieceNum);
        this.CreateItemChangeSet(itemSnapshot);
        this.ShowDecomposeResult();
        for (int index = 0; index < instance.Player.Artifacts.Count; ++index)
        {
          if ((long) instance.Player.Artifacts[index].UniqueID == (long) this.mCurrentArtifact.UniqueID)
          {
            instance.Player.Artifacts.RemoveAt(index);
            break;
          }
        }
      }
    }

    private void OnDecomposeCancel(GameObject go)
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mConfirmDialogGo, (UnityEngine.Object) null))
        return;
      Win_Btn_DecideCancel_FL_C component = (Win_Btn_DecideCancel_FL_C) this.mConfirmDialogGo.GetComponent<Win_Btn_DecideCancel_FL_C>();
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null) || component.AutoClose)
        return;
      component.BeginClose();
    }

    private void OnDecomposeResult(WWWResult www)
    {
      if (Network.IsError)
      {
        Network.EErrCode errCode = Network.ErrCode;
        FlowNode_Network.Retry();
      }
      else
      {
        WebAPI.JSON_BodyResponse<Json_PlayerDataAll> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_PlayerDataAll>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        if (jsonObject.body == null)
        {
          FlowNode_Network.Retry();
        }
        else
        {
          GameManager instance = MonoSingleton<GameManager>.Instance;
          Dictionary<ItemParam, int> itemSnapshot = instance.Player.CreateItemSnapshot();
          try
          {
            instance.Deserialize(jsonObject.body.player);
            instance.Deserialize(jsonObject.body.units);
            instance.Deserialize(jsonObject.body.items);
            instance.Deserialize(jsonObject.body.artifacts, false);
            instance.Player.UpdateArtifactOwner();
          }
          catch (Exception ex)
          {
            DebugUtility.LogException(ex);
            FlowNode_Network.Retry();
            return;
          }
          Network.RemoveAPI();
          this.CreateItemChangeSet(itemSnapshot);
          this.ShowDecomposeResult();
        }
      }
    }

    private void ShowDecomposeResult()
    {
      this.StartCoroutine(this.ShowDecomposeResultAsync());
    }

    [DebuggerHidden]
    private IEnumerator ShowDecomposeResultAsync()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new ArtifactWindow.\u003CShowDecomposeResultAsync\u003Ec__IteratorE6()
      {
        \u003C\u003Ef__this = this
      };
    }

    public void SellArtifacts()
    {
      if (this.IsBusy)
        return;
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.ArtifactList, (UnityEngine.Object) null))
        DebugUtility.LogWarning("ArtifactList is not set");
      else if (this.GetSelectedArtifacts().Length <= 0)
      {
        UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get("sys.ARTI_NOTHING2SELL"), (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1);
      }
      else
      {
        this.mConfirmDialogGo = UIUtility.ConfirmBox(LocalizedText.Get("sys.ARTI_SELL_CONFIRM"), new UIUtility.DialogResultEvent(this.OnSellRarityCheck), new UIUtility.DialogResultEvent(this.OnSellCancel), (GameObject) null, false, -1, (string) null, (string) null);
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mConfirmDialogGo, (UnityEngine.Object) null))
          return;
        Win_Btn_DecideCancel_FL_C component = (Win_Btn_DecideCancel_FL_C) this.mConfirmDialogGo.GetComponent<Win_Btn_DecideCancel_FL_C>();
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
          return;
        component.AutoClose = false;
      }
    }

    private void OnSellRarityCheck(GameObject go)
    {
      bool flag = false;
      ArtifactData[] selectedArtifacts = this.GetSelectedArtifacts();
      for (int index = 0; index < selectedArtifacts.Length; ++index)
      {
        if ((int) selectedArtifacts[index].Rarity + 1 >= this.RarityCheckValue)
        {
          flag = true;
          break;
        }
      }
      if (flag)
      {
        GameObject gameObject1 = AssetManager.Load<GameObject>(ArtifactWindow.ARTIFACT_RARITY_CHECK_UI_PATH);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) gameObject1, (UnityEngine.Object) null))
        {
          GameObject gameObject2 = (GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) gameObject1);
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) gameObject2, (UnityEngine.Object) null))
          {
            ArtifactRarityCheck componentInChildren = (ArtifactRarityCheck) gameObject2.GetComponentInChildren<ArtifactRarityCheck>();
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) componentInChildren, (UnityEngine.Object) null))
            {
              componentInChildren.Setup(ArtifactRarityCheck.Type.SELL, go, selectedArtifacts, this.RarityCheckValue);
              componentInChildren.OnDecideEvent = new ArtifactRarityCheck.OnArtifactRarityCheckDecideEvent(this.OnSellAccept);
              componentInChildren.OnCancelEvent = new ArtifactRarityCheck.OnArtifactRarityCheckCancelEvent(this.OnSellCancel);
              return;
            }
          }
        }
      }
      this.OnSellAccept(go);
    }

    private void OnSellAccept(GameObject go)
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mConfirmDialogGo, (UnityEngine.Object) null))
      {
        Win_Btn_DecideCancel_FL_C component = (Win_Btn_DecideCancel_FL_C) this.mConfirmDialogGo.GetComponent<Win_Btn_DecideCancel_FL_C>();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null) && !component.AutoClose)
          component.BeginClose();
      }
      ArtifactData[] selectedArtifacts = this.GetSelectedArtifacts();
      bool flag = false;
      for (int index = 0; index < selectedArtifacts.Length; ++index)
      {
        UnitData unit = (UnitData) null;
        JobData job = (JobData) null;
        if (MonoSingleton<GameManager>.Instance.Player.FindOwner(selectedArtifacts[index], out unit, out job))
        {
          flag = true;
          break;
        }
      }
      if (flag)
        UIUtility.ConfirmBox(LocalizedText.Get("sys.ARTI_SELL_CONFIRM2"), new UIUtility.DialogResultEvent(this.OnSellAccept2), new UIUtility.DialogResultEvent(this.OnSellCancel), (GameObject) null, false, -1, (string) null, (string) null);
      else
        this.OnSellAccept2(go);
    }

    private void OnSellAccept2(GameObject go)
    {
      ArtifactData[] selectedArtifacts = this.GetSelectedArtifacts();
      if (!string.IsNullOrEmpty(this.SellSE))
        MonoSingleton<MySound>.Instance.PlaySEOneShot(this.SellSE, 0.0f);
      long[] artifact_iids = new long[selectedArtifacts.Length];
      for (int index = 0; index < selectedArtifacts.Length; ++index)
        artifact_iids[index] = (long) selectedArtifacts[index].UniqueID;
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 601);
      if (Network.Mode == Network.EConnectMode.Online)
      {
        Network.RequestAPI((WebAPI) new ReqArtifactSell(artifact_iids, new Network.ResponseCallback(this.OnSellResult)), false);
      }
      else
      {
        CurrencyTracker track = new CurrencyTracker();
        MonoSingleton<GameManager>.Instance.Player.OfflineSellArtifacts(selectedArtifacts);
        track.EndTracking();
        List<ChangeListData> changeset = new List<ChangeListData>(4);
        this.AddCurrencyChangeSet(track, changeset);
        this.mChangeSet = changeset;
        this.ShowSellResult();
      }
    }

    private void OnSellCancel(GameObject go)
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mConfirmDialogGo, (UnityEngine.Object) null))
        return;
      Win_Btn_DecideCancel_FL_C component = (Win_Btn_DecideCancel_FL_C) this.mConfirmDialogGo.GetComponent<Win_Btn_DecideCancel_FL_C>();
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null) || component.AutoClose)
        return;
      component.BeginClose();
    }

    private void OnSellResult(WWWResult www)
    {
      if (Network.IsError)
      {
        Network.EErrCode errCode = Network.ErrCode;
        FlowNode_Network.Retry();
      }
      else
      {
        GameManager instance = MonoSingleton<GameManager>.Instance;
        CurrencyTracker track = new CurrencyTracker();
        WebAPI.JSON_BodyResponse<Json_PlayerDataAll> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_PlayerDataAll>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        if (jsonObject.body == null)
        {
          FlowNode_Network.Retry();
        }
        else
        {
          try
          {
            instance.Deserialize(jsonObject.body.player);
            instance.Deserialize(jsonObject.body.units);
            instance.Deserialize(jsonObject.body.artifacts, false);
            instance.Player.UpdateArtifactOwner();
          }
          catch (Exception ex)
          {
            DebugUtility.LogException(ex);
            FlowNode_Network.Retry();
            return;
          }
          Network.RemoveAPI();
          track.EndTracking();
          List<ChangeListData> changeset = new List<ChangeListData>(4);
          this.AddCurrencyChangeSet(track, changeset);
          this.mChangeSet = changeset;
          this.ShowSellResult();
        }
      }
    }

    private void ShowSellResult()
    {
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 602);
      GlobalEvent.Invoke(PredefinedGlobalEvents.REFRESH_GOLD_STATUS.ToString(), (object) this);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.SellResult, (UnityEngine.Object) null))
      {
        ChangeList componentInChildren = (ChangeList) ((GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) this.SellResult)).GetComponentInChildren<ChangeList>();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) componentInChildren, (UnityEngine.Object) null))
          componentInChildren.SetData(this.mChangeSet.ToArray());
      }
      this.mChangeSet = (List<ChangeListData>) null;
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ArtifactList, (UnityEngine.Object) null))
        return;
      this.ArtifactList.ClearSelection();
      this.ArtifactList.Refresh();
    }

    private void AddCurrencyChangeSet(CurrencyTracker track, List<ChangeListData> changeset)
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      if (track.Gold != 0)
        changeset.Add(new ChangeListData()
        {
          ItemID = 1145851719,
          ValOld = (instance.Player.Gold - track.Gold).ToString(),
          ValNew = instance.Player.Gold.ToString()
        });
      if (track.Coin != 0)
        changeset.Add(new ChangeListData()
        {
          ItemID = 1313427267,
          ValOld = (instance.Player.Coin - track.Coin).ToString(),
          ValNew = instance.Player.Coin.ToString()
        });
      if (track.MultiCoin != 0)
        changeset.Add(new ChangeListData()
        {
          ItemID = 1313817421,
          ValOld = (instance.Player.MultiCoin - track.MultiCoin).ToString(),
          ValNew = instance.Player.MultiCoin.ToString()
        });
      if (track.ArenaCoin == 0)
        return;
      changeset.Add(new ChangeListData()
      {
        ItemID = 1313817409,
        ValOld = (instance.Player.ArenaCoin - track.ArenaCoin).ToString(),
        ValNew = instance.Player.ArenaCoin.ToString()
      });
    }

    private void CreateItemChangeSet(Dictionary<ItemParam, int> snapshot)
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      this.mChangeSet = new List<ChangeListData>();
      List<ItemData> items = instance.Player.Items;
      for (int index = 0; index < items.Count; ++index)
      {
        int num = 0;
        int numNonCap = items[index].NumNonCap;
        if (snapshot.ContainsKey(items[index].Param))
          num = snapshot[items[index].Param];
        if (num != numNonCap)
          this.mChangeSet.Add(new ChangeListData()
          {
            ItemID = this.ItemDataID,
            MetaData = (object) items[index].Param,
            MetaDataType = typeof (ItemParam),
            ValOld = num.ToString(),
            ValNew = numNonCap.ToString()
          });
      }
    }

    public void SetOwnerUnit(UnitData unit, int jobIndex)
    {
      this.mOwnerUnitData = unit;
      this.mOwnerUnitJobIndex = jobIndex;
    }

    public void EquipArtifact()
    {
      if (this.IsBusy || UnityEngine.Object.op_Equality((UnityEngine.Object) this.ArtifactList, (UnityEngine.Object) null))
        return;
      ArtifactData[] selectedArtifacts = this.GetSelectedArtifacts();
      if (selectedArtifacts.Length <= 0)
      {
        this.EndEquip();
      }
      else
      {
        UnitData unit;
        JobData job;
        bool owner = MonoSingleton<GameManager>.Instance.Player.FindOwner(selectedArtifacts[0], out unit, out job);
        if (owner && unit.UniqueID == this.mOwnerUnitData.UniqueID && this.mOwnerUnitData.Jobs[this.mOwnerUnitJobIndex].UniqueID == job.UniqueID)
          this.EndEquip();
        else if (this.mOwnerUnitData != null && !selectedArtifacts[0].CheckEnableEquip(this.mOwnerUnitData, this.mOwnerUnitJobIndex))
          UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get("sys.ARTI_CANT_EQUIP"), (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1);
        else if (owner && (unit.UniqueID != this.mOwnerUnitData.UniqueID || this.mOwnerUnitData.Jobs[this.mOwnerUnitJobIndex].UniqueID != job.UniqueID))
          UIUtility.ConfirmBox(LocalizedText.Get("sys.ARTI_EQUIP_CONFIRM", (object) unit.UnitParam.name, (object) job.Name), new UIUtility.DialogResultEvent(this.OnEquipArtifactAccept), new UIUtility.DialogResultEvent(this.OnEquipArtifactCancel), (GameObject) null, false, -1, (string) null, (string) null);
        else
          this.OnEquipArtifactAccept((GameObject) null);
      }
    }

    private void OnEquipArtifactAccept(GameObject go)
    {
      ArtifactData[] selectedArtifacts = this.GetSelectedArtifacts();
      if (selectedArtifacts.Length < 0)
        return;
      this.OnEquip(selectedArtifacts[0], this.SelectArtifactSlot);
      this.EndEquip();
    }

    private void EndEquip()
    {
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 500);
    }

    private void OnEquipArtifactCancel(GameObject go)
    {
    }

    public void UnequipArtifact()
    {
      if (this.IsBusy)
        return;
      bool flag = false;
      if (this.mOwnerUnitData != null)
      {
        for (int index = 0; index < this.mOwnerUnitData.Jobs[this.mOwnerUnitJobIndex].Artifacts.Length; ++index)
        {
          if (this.mOwnerUnitData.Jobs[this.mOwnerUnitJobIndex].Artifacts[index] != 0L)
          {
            flag = true;
            break;
          }
        }
      }
      if (flag && this.OnEquip != null)
        this.OnEquip((ArtifactData) null, this.SelectArtifactSlot);
      this.EndEquip();
    }

    public void ShowDetail()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mDetailWindow, (UnityEngine.Object) null) || this.mCurrentArtifact == null || UnityEngine.Object.op_Equality((UnityEngine.Object) this.DetailWindow, (UnityEngine.Object) null))
        return;
      this.StartCoroutine(this.ShowDetailAsync());
    }

    [DebuggerHidden]
    private IEnumerator ShowDetailAsync()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new ArtifactWindow.\u003CShowDetailAsync\u003Ec__IteratorE7()
      {
        \u003C\u003Ef__this = this
      };
    }

    private bool OnSceneCHange()
    {
      this.mSceneChanging = true;
      this.FlushRequests(false);
      if (((Component) this).get_gameObject().get_activeInHierarchy())
        MonoSingleton<GameManager>.Instance.RegisterImportantJob(this.StartCoroutine(this.OnSceneChangeAsync()));
      return true;
    }

    [DebuggerHidden]
    private IEnumerator OnSceneChangeAsync()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new ArtifactWindow.\u003COnSceneChangeAsync\u003Ec__IteratorE8()
      {
        \u003C\u003Ef__this = this
      };
    }

    private void SyncArtifactData()
    {
      if (this.mCurrentArtifact == null || (long) this.mCurrentArtifact.UniqueID == 0L)
        return;
      ArtifactData artifactByUniqueId = MonoSingleton<GameManager>.Instance.Player.FindArtifactByUniqueID((long) this.mCurrentArtifact.UniqueID);
      if (artifactByUniqueId == null)
        return;
      this.mCurrentArtifact = artifactByUniqueId.Copy();
      this.mCurrentArtifactParam = this.mCurrentArtifact.ArtifactParam;
      this.Rebind();
    }

    private void UpdateArtifactOwner()
    {
      this.mCurrentArtifactOwner = (UnitData) null;
      this.mCurrentArtifactOwnerJob = (JobData) null;
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.ArtifactOwnerSlot, (UnityEngine.Object) null))
        return;
      if (this.mCurrentArtifact == null)
      {
        this.ArtifactOwnerSlot.SetSlotData<UnitData>((UnitData) null);
      }
      else
      {
        UnitData unit;
        JobData job;
        if (MonoSingleton<GameManager>.GetInstanceDirect().Player.FindOwner(this.mCurrentArtifact, out unit, out job))
        {
          this.mCurrentArtifactOwner = unit;
          this.mCurrentArtifactOwnerJob = job;
        }
        this.ArtifactOwnerSlot.SetSlotData<UnitData>(this.mCurrentArtifactOwner);
      }
    }

    private void Rebind()
    {
      this.UpdateArtifactOwner();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ArtifactSlot, (UnityEngine.Object) null))
        this.ArtifactSlot.SetSlotData<ArtifactData>(this.mCurrentArtifact);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.WindowBody, (UnityEngine.Object) null))
      {
        DataSource.Bind<ArtifactData>(this.WindowBody, this.mCurrentArtifact);
        DataSource.Bind<UnitData>(this.WindowBody, this.mCurrentArtifactOwner);
        DataSource.Bind<JobData>(this.WindowBody, this.mCurrentArtifactOwnerJob);
      }
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_SetEffectsButton, (UnityEngine.Object) null) || this.mCurrentArtifact == null)
        return;
      ((Selectable) this.m_SetEffectsButton).set_interactable(MonoSingleton<GameManager>.Instance.MasterParam.ExistSkillAbilityDeriveDataWithArtifact(this.mCurrentArtifact.ArtifactParam.iname));
      ArtifactSetList.SetSelectedArtifactParam(this.mCurrentArtifact.ArtifactParam);
    }

    private bool IsBusy
    {
      get
      {
        if (!this.mSceneChanging)
          return this.mBusy;
        return true;
      }
    }

    public void SetArtifactData()
    {
      if (this.mCurrentArtifact == null)
        return;
      GlobalVars.ConditionJobs = this.mCurrentArtifact.ArtifactParam.condition_jobs;
    }

    public void OnExpMaxOpen(SRPG_Button button)
    {
      if ((int) this.mCurrentArtifact.Lv >= this.mCurrentArtifact.GetLevelCap())
      {
        UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get("sys.LEVEL_CAPPED"), (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1);
      }
      else
      {
        GameObject gameObject = AssetManager.Load<GameObject>(ArtifactWindow.ARTIFACT_EXPMAX_UI_PATH);
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) gameObject, (UnityEngine.Object) null))
          return;
        GameObject root = (GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) gameObject);
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) root, (UnityEngine.Object) null))
          return;
        ArtifactLevelUpWindow componentInChildren = (ArtifactLevelUpWindow) root.GetComponentInChildren<ArtifactLevelUpWindow>();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) componentInChildren, (UnityEngine.Object) null))
          componentInChildren.OnDecideEvent = new ArtifactLevelUpWindow.OnArtifactLevelupEvent(this.OnArtifactBulkLevelUp);
        DataSource.Bind<ArtifactData>(root, this.mCurrentArtifact);
        DataSource.Bind<ArtifactWindow>(root, this);
        GameParameter.UpdateAll(root);
      }
    }

    public void OnExcludeEquipedValueChanged(bool isOn)
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ArtifactList, (UnityEngine.Object) null))
        return;
      this.WriteExcludeEquipedSettingValue(isOn);
      this.ArtifactList.ExcludeEquiped = isOn;
      this.ArtifactList.Refresh();
    }

    private bool ReadExcludeEquipedSettingValue()
    {
      return PlayerPrefsUtility.GetInt(PlayerPrefsUtility.ARTIFACT_EXCLUDE_EQUIPED, 0) != 0;
    }

    private void WriteExcludeEquipedSettingValue(bool isOn)
    {
      int num = isOn ? 1 : 0;
      PlayerPrefsUtility.SetInt(PlayerPrefsUtility.ARTIFACT_EXCLUDE_EQUIPED, num, false);
    }

    private class StatusCache
    {
      public BaseStatus BaseAdd = new BaseStatus();
      public BaseStatus BaseMul = new BaseStatus();
      public BaseStatus UnitAdd = new BaseStatus();
      public BaseStatus UnitMul = new BaseStatus();
    }

    private abstract class Request
    {
      public Network.ResponseCallback Callback;

      public abstract WebAPI Compose();
    }

    private class RequestAddRare : ArtifactWindow.Request
    {
      public long UniqueID;
      public string trophyprog;
      public string bingoprog;

      public override WebAPI Compose()
      {
        return (WebAPI) new ReqArtifactAddRare(this.UniqueID, this.Callback, this.trophyprog, this.bingoprog);
      }
    }

    private class RequestAddExp : ArtifactWindow.Request
    {
      public List<ItemParam> Items = new List<ItemParam>(32);
      public long UniqueID;

      public override WebAPI Compose()
      {
        Dictionary<string, int> usedItems = new Dictionary<string, int>();
        for (int index = 0; index < this.Items.Count; ++index)
        {
          if (usedItems.ContainsKey(this.Items[index].iname))
          {
            Dictionary<string, int> dictionary;
            string iname;
            (dictionary = usedItems)[iname = this.Items[index].iname] = dictionary[iname] + 1;
          }
          else
            usedItems[this.Items[index].iname] = 1;
        }
        return (WebAPI) new ReqArtifactEnforce(this.UniqueID, usedItems, this.Callback);
      }
    }

    public delegate void EquipEvent(ArtifactData artifact, ArtifactTypes type = ArtifactTypes.None);
  }
}
