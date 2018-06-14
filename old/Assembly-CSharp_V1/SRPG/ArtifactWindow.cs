// Decompiled with JetBrains decompiler
// Type: SRPG.ArtifactWindow
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(601, "Sell Begin", FlowNode.PinTypes.Output, 601)]
  [FlowNode.Pin(700, "Reset Sending Request", FlowNode.PinTypes.Input, 700)]
  [FlowNode.Pin(5, "Refresh", FlowNode.PinTypes.Input, 5)]
  [FlowNode.Pin(6, "Refresh Exp Items", FlowNode.PinTypes.Input, 6)]
  [FlowNode.Pin(11, "Show Selection", FlowNode.PinTypes.Input, 7)]
  [FlowNode.Pin(10, "Flush Requests", FlowNode.PinTypes.Input, 10)]
  [FlowNode.Pin(100, "Finalize", FlowNode.PinTypes.Input, 100)]
  [FlowNode.Pin(101, "Finalize Begin", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(102, "Finalize End", FlowNode.PinTypes.Output, 102)]
  [FlowNode.Pin(151, "AddExp Begin", FlowNode.PinTypes.Output, 151)]
  [FlowNode.Pin(152, "AddExp End", FlowNode.PinTypes.Output, 152)]
  [FlowNode.Pin(200, "Rarity Up", FlowNode.PinTypes.Input, 200)]
  [FlowNode.Pin(201, "Rarity Up End", FlowNode.PinTypes.Output, 201)]
  [FlowNode.Pin(300, "Transmute", FlowNode.PinTypes.Input, 300)]
  [FlowNode.Pin(301, "Transmute Begin", FlowNode.PinTypes.Output, 301)]
  [FlowNode.Pin(302, "Transmute End", FlowNode.PinTypes.Output, 302)]
  [FlowNode.Pin(400, "Decompose", FlowNode.PinTypes.Input, 400)]
  [FlowNode.Pin(402, "Decompose Begin", FlowNode.PinTypes.Output, 401)]
  [FlowNode.Pin(401, "Decompose End", FlowNode.PinTypes.Output, 402)]
  [FlowNode.Pin(500, "Equip", FlowNode.PinTypes.Output, 500)]
  [FlowNode.Pin(600, "Sell", FlowNode.PinTypes.Input, 600)]
  [FlowNode.Pin(602, "Sell End", FlowNode.PinTypes.Output, 602)]
  public class ArtifactWindow : MonoBehaviour, IFlowInterface
  {
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
    private UnitData mCurrentArtifactOwner;
    private JobData mCurrentArtifactOwnerJob;
    private ArtifactData[] mCachedArtifacts;
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
      if (Object.op_Inequality((Object) instanceDirect, (Object) null))
        instanceDirect.OnSceneChange += new GameManager.SceneChangeEvent(this.OnSceneCHange);
      if (Object.op_Inequality((Object) this.ExpPanel, (Object) null))
      {
        this.ExpPanel.SetDelegate(new ExpPanel.CalcEvent(ArtifactData.StaticCalcExpFromLevel), new ExpPanel.CalcEvent(ArtifactData.StaticCalcLevelFromExp));
        this.ExpPanel.OnLevelChange = new ExpPanel.LevelChangeEvent(this.OnLevelChange);
        this.ExpPanel.OnFinish = new ExpPanel.ExpPanelEvent(this.OnKyokaEnd);
      }
      if (!string.IsNullOrEmpty(this.ArtifactListID))
        this.ArtifactList = GameObjectID.FindGameObject<ArtifactList>(this.ArtifactListID);
      if (Object.op_Inequality((Object) this.ArtifactList, (Object) null))
        this.ArtifactList.OnSelectionChange += new ArtifactList.SelectionChangeEvent(this.OnArtifactSelect);
      if (Object.op_Inequality((Object) this.KyokaListItem, (Object) null) && this.KyokaListItem.get_activeInHierarchy())
        this.KyokaListItem.SetActive(false);
      if (Object.op_Inequality((Object) this.SelectionListItem, (Object) null) && ((Component) this.SelectionListItem).get_gameObject().get_activeSelf())
        ((Component) this.SelectionListItem).get_gameObject().SetActive(false);
      if (Object.op_Inequality((Object) this.ArtifactSlot, (Object) null))
        this.ArtifactSlot.SetSlotData<ArtifactData>((ArtifactData) null);
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
      if (Object.op_Inequality((Object) instanceDirect, (Object) null))
        instanceDirect.OnSceneChange -= new GameManager.SceneChangeEvent(this.OnSceneCHange);
      if (Object.op_Inequality((Object) this.mDetailWindow, (Object) null))
      {
        Object.Destroy((Object) this.mDetailWindow.get_gameObject());
        this.mDetailWindow = (GameObject) null;
      }
      if (Object.op_Inequality((Object) this.mResultWindow, (Object) null))
      {
        Object.Destroy((Object) this.mResultWindow);
        this.mResultWindow = (GameObject) null;
      }
      if (!Object.op_Inequality((Object) this.ArtifactList, (Object) null))
        return;
      this.ArtifactList.OnSelectionChange -= new ArtifactList.SelectionChangeEvent(this.OnArtifactSelect);
    }

    private void FlushRequests(bool immediate)
    {
      if (immediate)
      {
        while (this.mRequests.Count > 0 && !Network.IsConnecting)
        {
          WebAPI api = this.mRequests[0].Compose();
          this.mRequests.RemoveAt(0);
          if (Network.Mode == Network.EConnectMode.Online)
          {
            int num = (int) Network.RequestAPIImmediate(api, true);
          }
        }
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
      if (Object.op_Equality((Object) this.KyokaList, (Object) null) || Object.op_Equality((Object) this.KyokaListItem, (Object) null) || (Object.op_Equality((Object) this.KyokaList, (Object) this.KyokaListItem) || this.KyokaList.get_transform().IsChildOf(this.KyokaListItem.get_transform())))
        return;
      List<ItemData> items = MonoSingleton<GameManager>.Instance.Player.Items;
      int index1 = 0;
      Transform transform = this.KyokaList.get_transform();
      for (int index2 = 0; index2 < items.Count; ++index2)
      {
        if (items[index2].ItemType == EItemType.ExpUpArtifact)
        {
          GameObject root;
          if (this.mKyokaItems.Count <= index1)
          {
            root = (GameObject) Object.Instantiate<GameObject>((M0) this.KyokaListItem);
            if (Object.op_Inequality((Object) root, (Object) null))
            {
              this.mKyokaItems.Add(root);
              root.get_transform().SetParent(transform, false);
              ListItemEvents component = (ListItemEvents) root.GetComponent<ListItemEvents>();
              if (Object.op_Inequality((Object) component, (Object) null))
                component.OnSelect = new ListItemEvents.ListItemEvent(this.OnKyokaItemSelect);
            }
          }
          else
            root = this.mKyokaItems[index1];
          DataSource.Bind<ItemData>(root, items[index2]);
          root.SetActive(true);
          GameParameter.UpdateAll(root);
          ++index1;
        }
      }
      for (; index1 < this.mKyokaItems.Count; ++index1)
        this.mKyokaItems[index1].SetActive(false);
      this.mDisableFlushRequest = true;
    }

    private void OnKyokaItemSelect(GameObject go)
    {
      if (this.IsBusy || this.mCurrentArtifact == null)
        return;
      ItemData dataOfClass = DataSource.FindDataOfClass<ItemData>(go, (ItemData) null);
      if (dataOfClass == null || dataOfClass.Num <= 0)
        return;
      if ((int) this.mCurrentArtifact.Lv >= this.mCurrentArtifact.GetLevelCap())
      {
        if (!Object.op_Inequality((Object) this.ExpPanel, (Object) null) || this.ExpPanel.IsBusy)
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
          if (Object.op_Inequality((Object) this.OwnerSlot, (Object) null))
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
        requestAddExp.Items.Add(dataOfClass.Param);
        this.mCurrentArtifact.GainExp((int) dataOfClass.Param.value);
        MonoSingleton<GameManager>.Instance.Player.GainItem(dataOfClass.Param.iname, -1);
        GameParameter.UpdateAll(go);
        if (Object.op_Inequality((Object) this.ExpPanel, (Object) null))
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
          MonoSingleton<GameManager>.Instance.Player.OnArtifactStrength(this.mCurrentArtifactParam.iname);
        }
      }
    }

    public void RefreshArtifactList()
    {
      if (!Object.op_Inequality((Object) this.ArtifactList, (Object) null))
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
        this.RefreshWindow();
      }
      else
      {
        if (Object.op_Inequality((Object) this.SellPrice, (Object) null))
        {
          for (int index = 0; index < selection.Length; ++index)
          {
            if (selection[index] is ArtifactData)
              this.mTotalSellPrice += (long) ((ArtifactData) selection[index]).GetSellPrice();
            else if (selection[index] is ArtifactParam)
              this.mTotalSellPrice += (long) ((ArtifactParam) selection[index]).sell;
          }
        }
        if (Object.op_Inequality((Object) this.DecomposeCostTotal, (Object) null))
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
        if (!Object.op_Inequality((Object) list, (Object) null) || list.GetAutoSelected(true))
          return;
        if (this.mCurrentArtifact.CheckEnableRarityUp() == ArtifactData.RarityUpResults.Success)
        {
          if (!Object.op_Inequality((Object) this.ProcessToggle_Evolution, (Object) null))
            return;
          this.ProcessToggle_Evolution.set_isOn(true);
        }
        else
        {
          if (!Object.op_Inequality((Object) this.ProcessToggle_Enhance, (Object) null))
            return;
          this.ProcessToggle_Enhance.set_isOn(true);
        }
      }
    }

    public void RefreshAbilities()
    {
      if (this.mCurrentArtifact == null || Object.op_Equality((Object) this.AbilityListItem, (Object) null))
        return;
      if (Object.op_Inequality((Object) this.OwnerSlot, (Object) null))
      {
        UnitData unit = (UnitData) null;
        int num = -1;
        if (Object.op_Inequality((Object) this.OwnerSlot, (Object) null))
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
              if (Object.op_Equality((Object) this.AbilityList, (Object) null))
              {
                DataSource.Bind<AbilityParam>(this.AbilityListItem, abilityParam);
                gameObject1 = this.AbilityListItem;
              }
              else
              {
                if (this.mAbilityItems.Count <= index1)
                {
                  GameObject gameObject2 = (GameObject) Object.Instantiate<GameObject>((M0) this.AbilityListItem);
                  gameObject2.get_transform().SetParent(this.AbilityList.get_transform(), false);
                  this.mAbilityItems.Add(gameObject2);
                }
                DataSource.Bind<AbilityParam>(this.mAbilityItems[index1], abilityParam);
                this.mAbilityItems[index1].SetActive(true);
                gameObject1 = this.mAbilityItems[index1];
              }
              DataSource.Bind<AbilityData>(gameObject1, (AbilityData) null);
              Animator component = (Animator) gameObject1.GetComponent<Animator>();
              if (Object.op_Inequality((Object) component, (Object) null) && learningAbilities != null && !string.IsNullOrEmpty(this.AbilityListItemState))
              {
                bool flag = false;
                if (learningAbilities != null)
                {
                  for (int index3 = 0; index3 < learningAbilities.Count; ++index3)
                  {
                    string iname = learningAbilities[index3].Param.iname;
                    if (artifactParam.abil_inames[index2] == iname)
                    {
                      DataSource.Bind<AbilityData>(gameObject1, learningAbilities[index3]);
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
              if (Object.op_Equality((Object) this.AbilityList, (Object) null))
                break;
            }
          }
        }
      }
      if (Object.op_Equality((Object) this.AbilityList, (Object) null) && index1 == 0)
      {
        DataSource.Bind<AbilityParam>(this.AbilityListItem, (AbilityParam) null);
        DataSource.Bind<AbilityData>(this.AbilityListItem, (AbilityData) null);
      }
      for (; index1 < this.mAbilityItems.Count; ++index1)
        this.mAbilityItems[index1].SetActive(false);
    }

    public void ShowSelection()
    {
      if (Object.op_Equality((Object) this.SelectionListItem, (Object) null))
        return;
      for (int index = 0; index < this.mSelectionItems.Count; ++index)
        Object.Destroy((Object) ((Component) this.mSelectionItems[index]).get_gameObject());
      this.mSelectionItems.Clear();
      if (this.mSelectedArtifacts == null)
        return;
      if (Object.op_Inequality((Object) this.SelectionNum, (Object) null))
        this.SelectionNum.set_text(this.mSelectedArtifacts.Length.ToString());
      Transform transform = !Object.op_Equality((Object) this.SelectionList, (Object) null) ? this.SelectionList.get_transform() : ((Component) this.SelectionListItem).get_transform().get_parent();
      for (int index = 0; index < this.mSelectedArtifacts.Length; ++index)
      {
        ArtifactIcon artifactIcon = (ArtifactIcon) Object.Instantiate<ArtifactIcon>((M0) this.SelectionListItem);
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
      if (Object.op_Inequality((Object) this.OwnerSlot, (Object) null) && this.mCurrentArtifact != null)
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
      if (Object.op_Inequality((Object) this.Status, (Object) null))
      {
        if (this.mCurrentArtifact != null)
        {
          BaseStatus fixed_status1 = new BaseStatus();
          BaseStatus scale_status1 = new BaseStatus();
          this.mCurrentArtifact.GetHomePassiveBuffStatus(ref fixed_status1, ref scale_status1, (UnitData) null, 0, true);
          BaseStatus fixed_status2 = new BaseStatus();
          BaseStatus scale_status2 = new BaseStatus();
          this.mCurrentArtifact.GetHomePassiveBuffStatus(ref fixed_status2, ref scale_status2, unit, job_index, true);
          this.Status.SetValues(fixed_status1, scale_status1, fixed_status2, scale_status2);
        }
        else
        {
          BaseStatus baseStatus = new BaseStatus();
          this.Status.SetValues(baseStatus, baseStatus);
        }
      }
      if (Object.op_Inequality((Object) this.OwnerSlot, (Object) null))
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
      if (Object.op_Inequality((Object) this.DetailMaterial, (Object) null) && this.mCurrentArtifact != null)
        this.DetailMaterial.SetMaterial(0, this.mCurrentArtifact.Kakera);
      if (Object.op_Inequality((Object) this.KyokaPanel, (Object) null) && this.mCurrentArtifact != null)
      {
        Animator component = (Animator) this.KyokaPanel.GetComponent<Animator>();
        if (Object.op_Inequality((Object) component, (Object) null) && !string.IsNullOrEmpty(this.KyokaPanelState))
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
      if (Object.op_Inequality((Object) this.SellPrice, (Object) null))
        this.SellPrice.set_text(this.mTotalSellPrice.ToString());
      if (Object.op_Inequality((Object) this.RarityUpCost, (Object) null) && this.mCurrentArtifact != null)
      {
        Animator component = (Animator) ((Component) this.RarityUpCost).GetComponent<Animator>();
        if (Object.op_Inequality((Object) component, (Object) null) && !string.IsNullOrEmpty(this.RarityUpCostState))
        {
          if ((int) this.mCurrentArtifact.RarityParam.ArtifactRarityUpCost <= player.Gold)
            component.SetInteger(this.RarityUpCostState, this.RarityUpCost_Normal);
          else
            component.SetInteger(this.RarityUpCostState, this.RarityUpCost_NoGold);
        }
      }
      if (Object.op_Inequality((Object) this.ExpPanel, (Object) null) && this.mCurrentArtifact != null)
      {
        this.ExpPanel.LevelCap = this.mCurrentArtifact.GetLevelCap();
        this.ExpPanel.Exp = this.mCurrentArtifact.Exp;
      }
      if (!Object.op_Inequality((Object) this.WindowBody, (Object) null))
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
      if (Object.op_Inequality((Object) this.OwnerSlot, (Object) null))
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
      if (this.IsBusy || this.mCurrentArtifact == null || (int) this.mCurrentArtifact.Rarity >= (int) this.mCurrentArtifact.RarityCap || Object.op_Inequality((Object) this.ExpPanel, (Object) null) && this.ExpPanel.IsBusy)
        return;
      ArtifactData.RarityUpResults rarityUpResults = this.mCurrentArtifact.CheckEnableRarityUp();
      if (rarityUpResults == ArtifactData.RarityUpResults.Success)
      {
        UIUtility.ConfirmBox(LocalizedText.Get("sys.ARTI_RARITYUP_CONFIRM", new object[1]
        {
          (object) this.mCurrentArtifactParam.name
        }), new UIUtility.DialogResultEvent(this.OnAddRarityAccept), new UIUtility.DialogResultEvent(this.OnAddRarityCancel), (GameObject) null, false, -1);
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
      ArtifactWindow.RequestAddRare requestAddRare = new ArtifactWindow.RequestAddRare();
      requestAddRare.UniqueID = (long) this.mCurrentArtifact.UniqueID;
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
      return (IEnumerator) new ArtifactWindow.\u003CAddRareAsync\u003Ec__Iterator9B() { \u003C\u003Ef__this = this };
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
          MonoSingleton<GameManager>.Instance.Player.OnArtifactEvolution(this.mCurrentArtifactParam.iname);
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 201);
        }
      }
    }

    public void RefreshDecomposeInfo()
    {
      if (this.mCurrentArtifact == null || (long) this.mCurrentArtifact.UniqueID == 0L)
        return;
      if (Object.op_Inequality((Object) this.DecomposePanel, (Object) null) && !string.IsNullOrEmpty(this.DecomposePanelState))
      {
        Animator component = (Animator) this.DecomposePanel.GetComponent<Animator>();
        if (Object.op_Inequality((Object) component, (Object) null))
        {
          if (this.mCurrentArtifact.Kakera != null)
            component.SetInteger(this.DecomposePanelState, this.DecomposePanel_Normal);
          else
            component.SetInteger(this.DecomposePanelState, this.DecomposePanel_Disabled);
        }
      }
      if (this.mCurrentArtifact.Kakera != null)
      {
        if (Object.op_Inequality((Object) this.DecomposeHelp, (Object) null))
          this.DecomposeHelp.set_text(LocalizedText.Get("sys.ARTI_DECOMPOSE_HELP", (object) this.mCurrentArtifact.ArtifactParam.name, (object) this.mCurrentArtifact.Kakera.name, (object) this.mCurrentArtifact.GetKakeraChangeNum()));
        if (Object.op_Inequality((Object) this.DecomposeCost, (Object) null))
        {
          long num = (long) Math.Abs((int) this.mCurrentArtifact.RarityParam.ArtifactChangeCost);
          this.DecomposeCost.set_text(num.ToString());
          if (!string.IsNullOrEmpty(this.DecomposeCostState))
          {
            Animator component = (Animator) ((Component) this.DecomposeCost).GetComponent<Animator>();
            if (Object.op_Inequality((Object) component, (Object) null))
            {
              if ((long) MonoSingleton<GameManager>.Instance.Player.Gold >= num)
                component.SetInteger(this.DecomposeCostState, this.DecomposeCost_Normal);
              else
                component.SetInteger(this.DecomposeCostState, this.DecomposeCost_NoGold);
            }
          }
        }
        if (Object.op_Inequality((Object) this.DecomposeItem, (Object) null))
          this.DecomposeItem.SetMaterial(this.mCurrentArtifact.GetKakeraChangeNum(), this.mCurrentArtifact.Kakera);
        int itemAmount = MonoSingleton<GameManager>.Instance.Player.GetItemAmount(this.mCurrentArtifact.Kakera.iname);
        if (Object.op_Inequality((Object) this.DecomposeKakeraNumOld, (Object) null))
          this.DecomposeKakeraNumOld.set_text(itemAmount.ToString());
        if (Object.op_Inequality((Object) this.DecomposeKakeraNumNew, (Object) null))
          this.DecomposeKakeraNumNew.set_text(Mathf.Min(itemAmount + this.mCurrentArtifact.GetKakeraChangeNum(), (int) this.mCurrentArtifact.Kakera.cap).ToString());
      }
      if (!Object.op_Inequality((Object) this.DecomposeWarning, (Object) null))
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
      if (Object.op_Inequality((Object) this.TransmutePanel, (Object) null))
      {
        Animator component = (Animator) this.TransmutePanel.GetComponent<Animator>();
        if (Object.op_Inequality((Object) component, (Object) null) && !string.IsNullOrEmpty(this.TransmutePanelState))
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
      if (Object.op_Inequality((Object) this.TransmuteMaterial, (Object) null))
        this.TransmuteMaterial.SetMaterial((int) rarityParam.ArtifactCreatePieceNum, itemParam);
      if (Object.op_Inequality((Object) this.TransmuteCost, (Object) null))
        this.TransmuteCost.set_text((int) rarityParam.ArtifactCreateCost.ToString());
      if (!Object.op_Inequality((Object) this.TransmuteCondition, (Object) null))
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
      if (Object.op_Inequality((Object) this.RarityUpPanel, (Object) null))
      {
        Animator component = (Animator) this.RarityUpPanel.GetComponent<Animator>();
        if (Object.op_Inequality((Object) component, (Object) null) && !string.IsNullOrEmpty(this.RarityUpPanelState))
        {
          if ((int) this.mCurrentArtifact.Rarity < (int) this.mCurrentArtifact.RarityCap)
          {
            // ISSUE: object of a compiler-generated type is created
            // ISSUE: variable of a compiler-generated type
            ArtifactWindow.\u003CRefreshRarityUpInfo\u003Ec__AnonStorey230 infoCAnonStorey230 = new ArtifactWindow.\u003CRefreshRarityUpInfo\u003Ec__AnonStorey230();
            // ISSUE: reference to a compiler-generated field
            infoCAnonStorey230.hasKakera = false;
            // ISSUE: reference to a compiler-generated method
            this.mCurrentArtifact.GetKakeraDataListForRarityUp().ForEach(new Action<ItemData>(infoCAnonStorey230.\u003C\u003Em__23E));
            // ISSUE: reference to a compiler-generated field
            if (infoCAnonStorey230.hasKakera)
              component.SetInteger(this.RarityUpPanelState, this.RarityUpPanel_Normal);
            else
              component.SetInteger(this.RarityUpPanelState, this.RarityUpPanel_NoItem);
          }
          else
            component.SetInteger(this.RarityUpPanelState, this.RarityUpPanel_MaxRarity);
        }
      }
      if (Object.op_Inequality((Object) this.RarityUpButton, (Object) null))
        ((Selectable) this.RarityUpButton).set_interactable(flag);
      if (Object.op_Inequality((Object) this.RarityUpHilit, (Object) null))
        this.RarityUpHilit.SetActive(flag);
      if (Object.op_Inequality((Object) this.RarityUpIconRoot, (Object) null))
        this.RarityUpIconRoot.SetActive(flag);
      if ((int) this.mCurrentArtifact.Rarity >= (int) this.mCurrentArtifact.RarityCap)
        return;
      if (Object.op_Inequality((Object) this.RarityUpCost, (Object) null))
        this.RarityUpCost.set_text((int) this.mCurrentArtifact.RarityParam.ArtifactRarityUpCost.ToString());
      if (Object.op_Inequality((Object) this.RarityUpIconRoot, (Object) null))
        this.RarityUpIconRoot.SetActive(flag);
      if (!Object.op_Inequality((Object) this.RarityUpList, (Object) null) || !Object.op_Inequality((Object) this.RarityUpListItem, (Object) null))
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
          root = (GameObject) Object.Instantiate<GameObject>((M0) this.RarityUpListItem);
          if (Object.op_Inequality((Object) root, (Object) null))
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
            if (Object.op_Inequality((Object) component, (Object) null))
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
          }), new UIUtility.DialogResultEvent(this.OnTransmuteAccept), new UIUtility.DialogResultEvent(this.OnTransmuteCancel), (GameObject) null, false, -1);
      }
    }

    private void OnTransmuteAccept(GameObject go)
    {
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      RarityParam rarityParam = MonoSingleton<GameManager>.Instance.GetRarityParam(this.mCurrentArtifactParam.rareini);
      this.mCachedArtifacts = player.Artifacts.ToArray();
      player.GainGold(-(int) rarityParam.ArtifactCreateCost);
      AnalyticsManager.TrackCurrencyUse(AnalyticsManager.CurrencyType.Zeni, AnalyticsManager.CurrencySubType.FREE, (long) (int) rarityParam.ArtifactCreateCost, "Transmute Equip", (Dictionary<string, object>) null);
      GlobalEvent.Invoke(PredefinedGlobalEvents.REFRESH_GOLD_STATUS.ToString(), (object) this);
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 301);
      if (Network.Mode == Network.EConnectMode.Online)
      {
        Network.RequestAPI((WebAPI) new ReqArtifactAdd(this.mCurrentArtifactParam.iname, new Network.ResponseCallback(this.OnTransmuteResult)), false);
      }
      else
      {
        ItemData itemDataByItemId = player.FindItemDataByItemID(this.mCurrentArtifactParam.kakera);
        if (itemDataByItemId != null)
          itemDataByItemId.Used((int) rarityParam.ArtifactCreatePieceNum);
        Json_Artifact[] json = new Json_Artifact[1]{ new Json_Artifact() };
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
          instance.Player.OnArtifactTransmute(this.mCurrentArtifactParam.iname);
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
      return (IEnumerator) new ArtifactWindow.\u003CShowTransmuteResultAsync\u003Ec__Iterator9C() { changeset = changeset, \u003C\u0024\u003Echangeset = changeset, \u003C\u003Ef__this = this };
    }

    private ArtifactData[] GetSelectedArtifacts()
    {
      if (Object.op_Equality((Object) this.ArtifactList, (Object) null))
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
        UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get("sys.ARTI_DECOMPOSE_NOGOLD"), (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1);
      else
        UIUtility.ConfirmBox(LocalizedText.Get("sys.ARTI_DECOMPOSE_CONFIRM"), new UIUtility.DialogResultEvent(this.OnDecomposeAccept), new UIUtility.DialogResultEvent(this.OnDecomposeCancel), (GameObject) null, false, -1);
    }

    private void OnDecomposeAccept(GameObject go)
    {
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
        UIUtility.ConfirmBox(LocalizedText.Get("sys.ARTI_DECOMPOSE_CONFIRM2"), new UIUtility.DialogResultEvent(this.OnDecomposeAccept2), new UIUtility.DialogResultEvent(this.OnDecomposeCancel), (GameObject) null, false, -1);
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
      AnalyticsManager.TrackCurrencyUse(AnalyticsManager.CurrencyType.Zeni, AnalyticsManager.CurrencySubType.FREE, (long) num, "Disassemble Equip", (Dictionary<string, object>) null);
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
      return (IEnumerator) new ArtifactWindow.\u003CShowDecomposeResultAsync\u003Ec__Iterator9D() { \u003C\u003Ef__this = this };
    }

    public void SellArtifacts()
    {
      if (this.IsBusy)
        return;
      if (Object.op_Equality((Object) this.ArtifactList, (Object) null))
        DebugUtility.LogWarning("ArtifactList is not set");
      else if (this.GetSelectedArtifacts().Length <= 0)
        UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get("sys.ARTI_NOTHING2SELL"), (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1);
      else
        UIUtility.ConfirmBox(LocalizedText.Get("sys.ARTI_SELL_CONFIRM"), new UIUtility.DialogResultEvent(this.OnSellAccept), new UIUtility.DialogResultEvent(this.OnSellCancel), (GameObject) null, false, -1);
    }

    private void OnSellAccept(GameObject go)
    {
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
        UIUtility.ConfirmBox(LocalizedText.Get("sys.ARTI_SELL_CONFIRM2"), new UIUtility.DialogResultEvent(this.OnSellAccept2), new UIUtility.DialogResultEvent(this.OnSellCancel), (GameObject) null, false, -1);
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
      if (Object.op_Inequality((Object) this.SellResult, (Object) null))
      {
        ChangeList componentInChildren = (ChangeList) ((GameObject) Object.Instantiate<GameObject>((M0) this.SellResult)).GetComponentInChildren<ChangeList>();
        if (Object.op_Inequality((Object) componentInChildren, (Object) null))
          componentInChildren.SetData(this.mChangeSet.ToArray());
      }
      this.mChangeSet = (List<ChangeListData>) null;
      if (!Object.op_Inequality((Object) this.ArtifactList, (Object) null))
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
      if (this.IsBusy || Object.op_Equality((Object) this.ArtifactList, (Object) null))
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
          UIUtility.ConfirmBox(LocalizedText.Get("sys.ARTI_EQUIP_CONFIRM", (object) unit.UnitParam.name, (object) job.Name), new UIUtility.DialogResultEvent(this.OnEquipArtifactAccept), new UIUtility.DialogResultEvent(this.OnEquipArtifactCancel), (GameObject) null, false, -1);
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
      if (Object.op_Inequality((Object) this.mDetailWindow, (Object) null) || this.mCurrentArtifact == null || Object.op_Equality((Object) this.DetailWindow, (Object) null))
        return;
      this.StartCoroutine(this.ShowDetailAsync());
    }

    [DebuggerHidden]
    private IEnumerator ShowDetailAsync()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new ArtifactWindow.\u003CShowDetailAsync\u003Ec__Iterator9E() { \u003C\u003Ef__this = this };
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
      return (IEnumerator) new ArtifactWindow.\u003COnSceneChangeAsync\u003Ec__Iterator9F() { \u003C\u003Ef__this = this };
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
      if (Object.op_Equality((Object) this.ArtifactOwnerSlot, (Object) null))
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
      if (Object.op_Inequality((Object) this.ArtifactSlot, (Object) null))
        this.ArtifactSlot.SetSlotData<ArtifactData>(this.mCurrentArtifact);
      if (!Object.op_Inequality((Object) this.WindowBody, (Object) null))
        return;
      DataSource.Bind<ArtifactData>(this.WindowBody, this.mCurrentArtifact);
      DataSource.Bind<UnitData>(this.WindowBody, this.mCurrentArtifactOwner);
      DataSource.Bind<JobData>(this.WindowBody, this.mCurrentArtifactOwnerJob);
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

      public override WebAPI Compose()
      {
        return (WebAPI) new ReqArtifactAddRare(this.UniqueID, this.Callback);
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
