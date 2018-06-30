namespace SRPG
{
    using GR;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using UnityEngine;
    using UnityEngine.UI;

    [Pin(0x12e, "Transmute End", 1, 0x12e), Pin(0x65, "Finalize Begin", 1, 0x65), Pin(400, "Decompose", 0, 400), Pin(0x66, "Finalize End", 1, 0x66), Pin(0x97, "AddExp Begin", 1, 0x97), Pin(0x98, "AddExp End", 1, 0x98), Pin(200, "Rarity Up", 0, 200), Pin(0xc9, "Rarity Up End", 1, 0xc9), Pin(0xca, "Rarity Up Dialog Close", 1, 0xca), Pin(300, "Transmute", 0, 300), Pin(0x12d, "Transmute Begin", 1, 0x12d), Pin(700, "Reset Sending Request", 0, 700), Pin(0x25a, "Sell End", 1, 0x25a), Pin(0x259, "Sell Begin", 1, 0x259), Pin(600, "Sell", 0, 600), Pin(500, "Equip", 1, 500), Pin(0x191, "Decompose End", 1, 0x192), Pin(0x192, "Decompose Begin", 1, 0x191), Pin(5, "Refresh", 0, 5), Pin(6, "Refresh Exp Items", 0, 6), Pin(11, "Show Selection", 0, 7), Pin(10, "Flush Requests", 0, 10), Pin(100, "Finalize", 0, 100)]
    public class ArtifactWindow : MonoBehaviour, IFlowInterface
    {
        public SRPG.ArtifactList ArtifactList;
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
        public SRPG.ExpPanel ExpPanel;
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
        public EquipEvent OnEquip;
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
        [SerializeField, HeaderBar("▼セット効果確認用のボタン")]
        private Button m_SetEffectsButton;
        private List<ChangeListData> mChangeSet;
        private ArtifactData mCurrentArtifact;
        private ArtifactParam mCurrentArtifactParam;
        private object[] mSelectedArtifacts;
        private List<GameObject> mAbilityItems;
        private List<GameObject> mKyokaItems;
        private List<GameObject> mRarityUpItems;
        private List<Request> mRequests;
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
        private static readonly string ARTIFACT_EXPMAX_UI_PATH;
        private static readonly string ARTIFACT_RARITY_CHECK_UI_PATH;
        private StatusCache mStatusCache;
        private ArtifactTypes mSelectArtifactSlot;

        static ArtifactWindow()
        {
            ARTIFACT_EXPMAX_UI_PATH = "UI/ArtifactLevelUpWindow";
            ARTIFACT_RARITY_CHECK_UI_PATH = "UI/ArtifactRarityCheck";
            return;
        }

        public ArtifactWindow()
        {
            this.AutoFlushRequests = 1f;
            this.KyokaPanel_LvCapped = 1;
            this.KyokaPanel_LvMax = 2;
            this.RarityUpPanel_MaxRarity = 1;
            this.RarityUpPanel_NoItem = 2;
            this.RarityUpCost_NoGold = -1;
            this.TransmutePanel_Disabled = 1;
            this.TransmuteCost_NoGold = -1;
            this.AbilityListItem_Locked = 1;
            this.AbilityListItem_Unlocked = 2;
            this.DecomposePanel_Disabled = 1;
            this.DecomposeWarningRarity = 2;
            this.DecomposeCost_NoGold = -1;
            this.mAbilityItems = new List<GameObject>(4);
            this.mKyokaItems = new List<GameObject>(8);
            this.mRarityUpItems = new List<GameObject>(3);
            this.mRequests = new List<Request>();
            this.mSelectionItems = new List<ArtifactIcon>(10);
            this.mTmpItems = new List<ItemData>();
            base..ctor();
            return;
        }

        public void Activated(int pinID)
        {
            int num;
            num = pinID;
            switch ((num - 5))
            {
                case 0:
                    goto Label_006A;

                case 1:
                    goto Label_0075;

                case 2:
                    goto Label_0026;

                case 3:
                    goto Label_0026;

                case 4:
                    goto Label_0026;

                case 5:
                    goto Label_0080;

                case 6:
                    goto Label_008C;
            }
        Label_0026:
            if (num == 100)
            {
                goto Label_0097;
            }
            if (num == 200)
            {
                goto Label_00B9;
            }
            if (num == 300)
            {
                goto Label_00C4;
            }
            if (num == 400)
            {
                goto Label_00CF;
            }
            if (num == 600)
            {
                goto Label_00DA;
            }
            if (num == 700)
            {
                goto Label_00E5;
            }
            goto Label_00F1;
        Label_006A:
            this.RefreshWindow();
            goto Label_00F1;
        Label_0075:
            this.RefreshKyokaList();
            goto Label_00F1;
        Label_0080:
            this.FlushRequests(0);
            goto Label_00F1;
        Label_008C:
            this.ShowSelection();
            goto Label_00F1;
        Label_0097:
            this.mDisableFlushRequest = 0;
            this.mSendingRequests = 1;
            this.mFinalizing = 1;
            FlowNode_GameObject.ActivateOutputLinks(this, 0x65);
            goto Label_00F1;
        Label_00B9:
            this.AddRarity();
            goto Label_00F1;
        Label_00C4:
            this.TransmuteArtifact();
            goto Label_00F1;
        Label_00CF:
            this.DecomposeArtifact();
            goto Label_00F1;
        Label_00DA:
            this.SellArtifacts();
            goto Label_00F1;
        Label_00E5:
            this.mSendingRequests = 0;
        Label_00F1:
            return;
        }

        private void AddAsyncRequest(Request req)
        {
            this.mRequests.Add(req);
            this.mFlushTimer = this.AutoFlushRequests;
            return;
        }

        private unsafe void AddCurrencyChangeSet(CurrencyTracker track, List<ChangeListData> changeset)
        {
            GameManager manager;
            ChangeListData data;
            ChangeListData data2;
            ChangeListData data3;
            ChangeListData data4;
            int num;
            int num2;
            int num3;
            int num4;
            int num5;
            int num6;
            int num7;
            int num8;
            manager = MonoSingleton<GameManager>.Instance;
            if (track.Gold == null)
            {
                goto Label_0064;
            }
            data = new ChangeListData();
            data.ItemID = 0x444c4f47;
            num = manager.Player.Gold - track.Gold;
            data.ValOld = &num.ToString();
            data.ValNew = &manager.Player.Gold.ToString();
            changeset.Add(data);
        Label_0064:
            if (track.Coin == null)
            {
                goto Label_00C2;
            }
            data2 = new ChangeListData();
            data2.ItemID = 0x4e494f43;
            num3 = manager.Player.Coin - track.Coin;
            data2.ValOld = &num3.ToString();
            data2.ValNew = &manager.Player.Coin.ToString();
            changeset.Add(data2);
        Label_00C2:
            if (track.MultiCoin == null)
            {
                goto Label_0120;
            }
            data3 = new ChangeListData();
            data3.ItemID = 0x4e4f434d;
            num5 = manager.Player.MultiCoin - track.MultiCoin;
            data3.ValOld = &num5.ToString();
            data3.ValNew = &manager.Player.MultiCoin.ToString();
            changeset.Add(data3);
        Label_0120:
            if (track.ArenaCoin == null)
            {
                goto Label_0183;
            }
            data4 = new ChangeListData();
            data4.ItemID = 0x4e4f4341;
            num7 = manager.Player.ArenaCoin - track.ArenaCoin;
            data4.ValOld = &num7.ToString();
            data4.ValNew = &manager.Player.ArenaCoin.ToString();
            changeset.Add(data4);
        Label_0183:
            return;
        }

        private unsafe void AddExpResult(WWWResult www)
        {
            WebAPI.JSON_BodyResponse<Json_PlayerDataAll> response;
            int num;
            ArtifactData data;
            ArtifactData data2;
            long num2;
            Exception exception;
            Network.EErrCode code;
            if (Network.IsError == null)
            {
                goto Label_002E;
            }
            if (Network.ErrCode == 0x232a)
            {
                goto Label_0022;
            }
            goto Label_0028;
        Label_0022:
            FlowNode_Network.Back();
            return;
        Label_0028:
            FlowNode_Network.Retry();
            return;
        Label_002E:
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_PlayerDataAll>>(&www.text);
            DebugUtility.Assert((response == null) == 0, "res == null");
            if (response.body != null)
            {
                goto Label_005D;
            }
            FlowNode_Network.Retry();
            return;
        Label_005D:
            num = -1;
            data = null;
            data2 = null;
            num2 = -1L;
            if (this.mCurrentArtifact == null)
            {
                goto Label_00A8;
            }
            num2 = this.mCurrentArtifact.UniqueID;
            data = MonoSingleton<GameManager>.Instance.Player.FindArtifactByUniqueID(num2);
            if (data == null)
            {
                goto Label_00A8;
            }
            num = data.Lv;
        Label_00A8:
            try
            {
                MonoSingleton<GameManager>.Instance.Deserialize(response.body.player);
                MonoSingleton<GameManager>.Instance.Deserialize(response.body.items);
                MonoSingleton<GameManager>.Instance.Deserialize(response.body.artifacts, 0);
                MonoSingleton<GameManager>.Instance.Player.UpdateArtifactOwner();
                goto Label_0114;
            }
            catch (Exception exception1)
            {
            Label_00FC:
                exception = exception1;
                DebugUtility.LogException(exception);
                FlowNode_Network.Retry();
                goto Label_0175;
            }
        Label_0114:
            if (Network.IsImmediateMode != null)
            {
                goto Label_0124;
            }
            this.RefreshArtifactList();
        Label_0124:
            Network.RemoveAPI();
            this.mSendingRequests = 0;
            data2 = MonoSingleton<GameManager>.Instance.Player.FindArtifactByUniqueID(num2);
            MonoSingleton<GameManager>.Instance.Player.OnArtifactStrength(this.mCurrentArtifactParam.iname, this.mUseEnhanceItemNum, num, data2.Lv);
            this.mUseEnhanceItemNum = 0;
        Label_0175:
            return;
        }

        [DebuggerHidden]
        private IEnumerator AddRareAsync()
        {
            <AddRareAsync>c__IteratorE4 re;
            re = new <AddRareAsync>c__IteratorE4();
            re.<>f__this = this;
            return re;
        }

        public void AddRarity()
        {
            object[] objArray1;
            ArtifactData.RarityUpResults results;
            string str;
            string str2;
            if (this.IsBusy == null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            if (this.mCurrentArtifact != null)
            {
                goto Label_0018;
            }
            return;
        Label_0018:
            if (this.mCurrentArtifact.Rarity < this.mCurrentArtifact.RarityCap)
            {
                goto Label_003E;
            }
            return;
        Label_003E:
            if ((this.ExpPanel != null) == null)
            {
                goto Label_0060;
            }
            if (this.ExpPanel.IsBusy == null)
            {
                goto Label_0060;
            }
            return;
        Label_0060:
            results = this.mCurrentArtifact.CheckEnableRarityUp();
            if (results != null)
            {
                goto Label_00BA;
            }
            objArray1 = new object[] { this.mCurrentArtifactParam.name };
            UIUtility.ConfirmBox(LocalizedText.Get("sys.ARTI_RARITYUP_CONFIRM", objArray1), new UIUtility.DialogResultEvent(this.OnAddRarityAccept), new UIUtility.DialogResultEvent(this.OnAddRarityCancel), null, 0, -1, null, null);
            goto Label_011A;
        Label_00BA:
            str2 = null;
            if ((results & 8) == null)
            {
                goto Label_00CF;
            }
            str2 = "sys.ARTI_RARITYUP_MAX";
            goto Label_0103;
        Label_00CF:
            if ((results & 1) == null)
            {
                goto Label_00E2;
            }
            str2 = "sys.ARTI_RARITYUP_NOLV";
            goto Label_0103;
        Label_00E2:
            if ((results & 2) == null)
            {
                goto Label_00F5;
            }
            str2 = "sys.ARTI_RARITYUP_NOGOLD";
            goto Label_0103;
        Label_00F5:
            if ((results & 4) == null)
            {
                goto Label_0103;
            }
            str2 = "sys.ARTI_RARITYUP_NOMTRL";
        Label_0103:
            if (str2 == null)
            {
                goto Label_011A;
            }
            UIUtility.NegativeSystemMessage(null, LocalizedText.Get(str2), null, null, 0, -1);
        Label_011A:
            return;
        }

        private unsafe void AddRarityResult(WWWResult www)
        {
            WebAPI.JSON_BodyResponse<Json_PlayerDataAll> response;
            Exception exception;
            Network.EErrCode code;
            if (Network.IsError == null)
            {
                goto Label_0016;
            }
            code = Network.ErrCode;
            FlowNode_Network.Retry();
            return;
        Label_0016:
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_PlayerDataAll>>(&www.text);
            DebugUtility.Assert((response == null) == 0, "res == null");
            if (response.body != null)
            {
                goto Label_0045;
            }
            FlowNode_Network.Retry();
            return;
        Label_0045:
            try
            {
                MonoSingleton<GameManager>.Instance.Deserialize(response.body.player);
                MonoSingleton<GameManager>.Instance.Deserialize(response.body.items);
                MonoSingleton<GameManager>.Instance.Deserialize(response.body.artifacts, 1);
                MonoSingleton<GameManager>.Instance.Player.UpdateArtifactOwner();
                goto Label_00AF;
            }
            catch (Exception exception1)
            {
            Label_0099:
                exception = exception1;
                DebugUtility.LogException(exception);
                FlowNode_Network.Retry();
                goto Label_00ED;
            }
        Label_00AF:
            if (Network.IsImmediateMode != null)
            {
                goto Label_00CB;
            }
            this.SyncArtifactData();
            this.RefreshWindow();
            this.RefreshArtifactList();
        Label_00CB:
            this.mBusy = 0;
            Network.RemoveAPI();
            MonoSingleton<GameManager>.Instance.ServerSyncTrophyExecEnd(www);
            FlowNode_GameObject.ActivateOutputLinks(this, 0xc9);
        Label_00ED:
            return;
        }

        private long CalcDecomposeCost(ArtifactData[] artifacts)
        {
            long num;
            int num2;
            num = 0L;
            num2 = 0;
            goto Label_0031;
        Label_000A:
            if (artifacts[num2].Kakera == null)
            {
                goto Label_002D;
            }
            num += (long) artifacts[num2].RarityParam.ArtifactChangeCost;
        Label_002D:
            num2 += 1;
        Label_0031:
            if (num2 < ((int) artifacts.Length))
            {
                goto Label_000A;
            }
            return num;
        }

        private unsafe void CreateItemChangeSet(Dictionary<ItemParam, int> snapshot)
        {
            GameManager manager;
            List<ItemData> list;
            int num;
            int num2;
            int num3;
            ChangeListData data;
            manager = MonoSingleton<GameManager>.Instance;
            this.mChangeSet = new List<ChangeListData>();
            list = manager.Player.Items;
            num = 0;
            goto Label_00CB;
        Label_0024:
            num2 = 0;
            num3 = list[num].NumNonCap;
            if (snapshot.ContainsKey(list[num].Param) == null)
            {
                goto Label_005E;
            }
            num2 = snapshot[list[num].Param];
        Label_005E:
            if (num2 == num3)
            {
                goto Label_00C7;
            }
            data = new ChangeListData();
            data.ItemID = this.ItemDataID;
            data.MetaData = list[num].Param;
            data.MetaDataType = typeof(ItemParam);
            data.ValOld = &num2.ToString();
            data.ValNew = &num3.ToString();
            this.mChangeSet.Add(data);
        Label_00C7:
            num += 1;
        Label_00CB:
            if (num < list.Count)
            {
                goto Label_0024;
            }
            return;
        }

        public void DecomposeArtifact()
        {
            int num;
            int num2;
            ArtifactData data;
            string str;
            string str2;
            string str3;
            Win_Btn_DecideCancel_FL_C l_fl_c;
            if (this.IsBusy == null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            if (this.mSelectedArtifacts != null)
            {
                goto Label_0026;
            }
            if (((int) this.mSelectedArtifacts.Length) >= 1)
            {
                goto Label_0026;
            }
            return;
        Label_0026:
            num = 0;
            num2 = 0;
            goto Label_0093;
        Label_002F:
            data = (ArtifactData) this.mSelectedArtifacts[num2];
            if (data == null)
            {
                goto Label_0053;
            }
            if (data.UniqueID != null)
            {
                goto Label_0054;
            }
        Label_0053:
            return;
        Label_0054:
            if (data.Kakera != null)
            {
                goto Label_0077;
            }
            str = LocalizedText.Get("sys.ARTI_DECOMPOSE_NODEC");
            UIUtility.NegativeSystemMessage(null, str, null, null, 0, -1);
            return;
        Label_0077:
            num += Math.Abs(data.RarityParam.ArtifactChangeCost);
            num2 += 1;
        Label_0093:
            if (num2 < ((int) this.mSelectedArtifacts.Length))
            {
                goto Label_002F;
            }
            if (MonoSingleton<GameManager>.Instance.Player.Gold >= num)
            {
                goto Label_00D0;
            }
            str2 = LocalizedText.Get("sys.ARTI_DECOMPOSE_NOGOLD");
            UIUtility.NegativeSystemMessage(null, str2, null, null, 0, -1);
            return;
        Label_00D0:
            str3 = LocalizedText.Get("sys.ARTI_DECOMPOSE_CONFIRM");
            this.mConfirmDialogGo = UIUtility.ConfirmBox(str3, new UIUtility.DialogResultEvent(this.OnDecomposeRarityCheck), new UIUtility.DialogResultEvent(this.OnDecomposeCancel), null, 0, -1, null, null);
            if ((this.mConfirmDialogGo != null) == null)
            {
                goto Label_0139;
            }
            l_fl_c = this.mConfirmDialogGo.GetComponent<Win_Btn_DecideCancel_FL_C>();
            if ((l_fl_c != null) == null)
            {
                goto Label_0139;
            }
            l_fl_c.AutoClose = 0;
        Label_0139:
            return;
        }

        private void EndEquip()
        {
            FlowNode_GameObject.ActivateOutputLinks(this, 500);
            return;
        }

        public unsafe void EquipArtifact()
        {
            object[] objArray1;
            ArtifactData[] dataArray;
            UnitData data;
            JobData data2;
            bool flag;
            bool flag2;
            string str;
            string str2;
            if (this.IsBusy == null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            if ((this.ArtifactList == null) == null)
            {
                goto Label_001E;
            }
            return;
        Label_001E:
            dataArray = this.GetSelectedArtifacts();
            if (((int) dataArray.Length) > 0)
            {
                goto Label_0035;
            }
            this.EndEquip();
            return;
        Label_0035:
            flag = MonoSingleton<GameManager>.Instance.Player.FindOwner(dataArray[0], &data, &data2);
            if (flag == null)
            {
                goto Label_0091;
            }
            if (data.UniqueID != this.mOwnerUnitData.UniqueID)
            {
                goto Label_0091;
            }
            if (this.mOwnerUnitData.Jobs[this.mOwnerUnitJobIndex].UniqueID != data2.UniqueID)
            {
                goto Label_0091;
            }
            this.EndEquip();
            return;
        Label_0091:
            if (this.mOwnerUnitData == null)
            {
                goto Label_00D3;
            }
            if (dataArray[0].CheckEnableEquip(this.mOwnerUnitData, this.mOwnerUnitJobIndex) != null)
            {
                goto Label_00D3;
            }
            str = LocalizedText.Get("sys.ARTI_CANT_EQUIP");
            UIUtility.NegativeSystemMessage(null, str, null, null, 0, -1);
            return;
        Label_00D3:
            if (flag == null)
            {
                goto Label_0160;
            }
            if (data.UniqueID != this.mOwnerUnitData.UniqueID)
            {
                goto Label_0111;
            }
            if (this.mOwnerUnitData.Jobs[this.mOwnerUnitJobIndex].UniqueID == data2.UniqueID)
            {
                goto Label_0160;
            }
        Label_0111:
            objArray1 = new object[] { data.UnitParam.name, data2.Name };
            UIUtility.ConfirmBox(LocalizedText.Get("sys.ARTI_EQUIP_CONFIRM", objArray1), new UIUtility.DialogResultEvent(this.OnEquipArtifactAccept), new UIUtility.DialogResultEvent(this.OnEquipArtifactCancel), null, 0, -1, null, null);
            return;
        Label_0160:
            this.OnEquipArtifactAccept(null);
            return;
        }

        private void FlushRequests(bool immediate)
        {
            WebAPI bapi;
            if (immediate == null)
            {
                goto Label_006C;
            }
            if (this.IsConnectableNetwork() != null)
            {
                goto Label_0046;
            }
            return;
            goto Label_0046;
        Label_0017:
            bapi = this.mRequests[0].Compose();
            this.mRequests.RemoveAt(0);
            if (Network.Mode != null)
            {
                goto Label_0046;
            }
            Network.RequestAPIImmediate(bapi, 1);
        Label_0046:
            if (this.mRequests.Count <= 0)
            {
                goto Label_0061;
            }
            if (Network.IsConnecting == null)
            {
                goto Label_0017;
            }
        Label_0061:
            this.RefreshArtifactList();
            goto Label_0084;
        Label_006C:
            if (this.mRequests.Count <= 0)
            {
                goto Label_0084;
            }
            this.mSendingRequests = 1;
        Label_0084:
            return;
        }

        private ArtifactData[] GetSelectedArtifacts()
        {
            object[] objArray;
            List<ArtifactData> list;
            int num;
            if ((this.ArtifactList == null) == null)
            {
                goto Label_0018;
            }
            return new ArtifactData[0];
        Label_0018:
            objArray = this.ArtifactList.Selection;
            list = new List<ArtifactData>((int) objArray.Length);
            num = 0;
            goto Label_0066;
        Label_0034:
            if ((objArray[num] as ArtifactData) == null)
            {
                goto Label_0062;
            }
            if (list.Contains(objArray[num] as ArtifactData) != null)
            {
                goto Label_0062;
            }
            list.Add(objArray[num] as ArtifactData);
        Label_0062:
            num += 1;
        Label_0066:
            if (num < ((int) objArray.Length))
            {
                goto Label_0034;
            }
            return list.ToArray();
        }

        private bool IsConnectableNetwork()
        {
            NetworkReachability reachability;
            switch (Application.get_internetReachability())
            {
                case 0:
                    goto Label_0022;

                case 1:
                    goto Label_001D;

                case 2:
                    goto Label_001D;
            }
            goto Label_0024;
        Label_001D:
            goto Label_0024;
        Label_0022:
            return 0;
        Label_0024:
            return 1;
        }

        private bool IsRequestPending<T>()
        {
            if (this.mRequests.Count <= 0)
            {
                goto Label_0029;
            }
            if ((this.mRequests[0] as T) == null)
            {
                goto Label_0029;
            }
            return 1;
        Label_0029:
            return 0;
        }

        private unsafe void OnAddRarityAccept(GameObject go)
        {
            int num;
            List<AbilityData> list;
            List<int> list2;
            List<AbilityData> list3;
            ArtifactParam param;
            int num2;
            string str;
            int num3;
            string str2;
            int num4;
            int num5;
            int num6;
            int num7;
            string str3;
            string str4;
            RequestAddRare rare;
            int num8;
            int num9;
            int num10;
            int num11;
            num = this.mCurrentArtifact.GetLevelCap();
            list = new List<AbilityData>(4);
            list2 = new List<int>(4);
            list3 = new List<AbilityData>(4);
            param = this.mCurrentArtifact.ArtifactParam;
            if (param.abil_shows == null)
            {
                goto Label_00D6;
            }
            if (this.mCurrentArtifact.LearningAbilities == null)
            {
                goto Label_00D6;
            }
            num2 = 0;
            goto Label_00BF;
        Label_0052:
            str = this.mCurrentArtifact.LearningAbilities[num2].Param.iname;
            if (Array.IndexOf(param.abil_shows, str) < 0)
            {
                goto Label_00B9;
            }
            list.Add(this.mCurrentArtifact.LearningAbilities[num2]);
            list2.Add(this.mCurrentArtifact.LearningAbilities[num2].Rank);
        Label_00B9:
            num2 += 1;
        Label_00BF:
            if (num2 < this.mCurrentArtifact.LearningAbilities.Count)
            {
                goto Label_0052;
            }
        Label_00D6:
            this.mCurrentArtifact.RarityUp();
            if (param.abil_shows == null)
            {
                goto Label_018E;
            }
            if (this.mCurrentArtifact.LearningAbilities == null)
            {
                goto Label_018E;
            }
            num3 = 0;
            goto Label_0177;
        Label_0105:
            if (list.Contains(this.mCurrentArtifact.LearningAbilities[num3]) == null)
            {
                goto Label_0127;
            }
            goto Label_0171;
        Label_0127:
            str2 = this.mCurrentArtifact.LearningAbilities[num3].Param.iname;
            if (Array.IndexOf(param.abil_shows, str2) < 0)
            {
                goto Label_0171;
            }
            list3.Add(this.mCurrentArtifact.LearningAbilities[num3]);
        Label_0171:
            num3 += 1;
        Label_0177:
            if (num3 < this.mCurrentArtifact.LearningAbilities.Count)
            {
                goto Label_0105;
            }
        Label_018E:
            num4 = this.mCurrentArtifact.GetLevelCap();
            num5 = 0;
            this.mChangeSet = new List<ChangeListData>(4);
            this.mChangeSet.Add(new ChangeListData());
            this.mChangeSet[0].ItemID = this.ArtifactDataID;
            this.mChangeSet[0].Label = LocalizedText.Get("sys.ARTI_MAXRULV");
            this.mChangeSet[0].ValOld = &num.ToString();
            this.mChangeSet[0].ValNew = &num4.ToString();
            num5 += 1;
            num6 = 0;
            goto Label_02F9;
        Label_022A:
            if (list[num6].Rank == list2[num6])
            {
                goto Label_02F3;
            }
            this.mChangeSet.Add(new ChangeListData());
            this.mChangeSet[num5].ItemID = this.ChangedAbilityID;
            this.mChangeSet[num5].MetaDataType = typeof(AbilityData);
            this.mChangeSet[num5].MetaData = list[num6];
            num8 = list2[num6];
            this.mChangeSet[num5].ValOld = &num8.ToString();
            this.mChangeSet[num5].ValNew = &list[num6].Rank.ToString();
            num5 += 1;
        Label_02F3:
            num6 += 1;
        Label_02F9:
            if (num6 < list.Count)
            {
                goto Label_022A;
            }
            num7 = 0;
            goto Label_03DD;
        Label_030E:
            if (list[num7].Rank == list2[num7])
            {
                goto Label_03D7;
            }
            this.mChangeSet.Add(new ChangeListData());
            this.mChangeSet[num5].ItemID = this.NewAbilityID;
            this.mChangeSet[num5].MetaDataType = typeof(AbilityData);
            this.mChangeSet[num5].MetaData = list3[num7];
            this.mChangeSet[num5].ValOld = &list[num7].Rank.ToString();
            num11 = list2[num7];
            this.mChangeSet[num5].ValNew = &num11.ToString();
            num5 += 1;
        Label_03D7:
            num7 += 1;
        Label_03DD:
            if (num7 < list3.Count)
            {
                goto Label_030E;
            }
            GlobalEvent.Invoke(((PredefinedGlobalEvents) 1).ToString(), this);
            base.StartCoroutine(this.AddRareAsync());
            MonoSingleton<GameManager>.Instance.Player.OnArtifactEvolution(this.mCurrentArtifactParam.iname);
            MonoSingleton<GameManager>.Instance.ServerSyncTrophyExecStart(&str3, &str4);
            rare = new RequestAddRare();
            rare.UniqueID = this.mCurrentArtifact.UniqueID;
            rare.trophyprog = str3;
            rare.bingoprog = str4;
            rare.Callback = new Network.ResponseCallback(this.AddRarityResult);
            this.AddAsyncRequest(rare);
            this.mBusy = 1;
            this.FlushRequests(0);
            return;
        }

        private void OnAddRarityCancel(GameObject go)
        {
        }

        private void OnApplicationFocus(bool focus)
        {
            if (focus != null)
            {
                goto Label_000D;
            }
            this.FlushRequests(1);
        Label_000D:
            return;
        }

        private void OnApplicationPause(bool pausing)
        {
            if (pausing == null)
            {
                goto Label_000D;
            }
            this.FlushRequests(1);
        Label_000D:
            return;
        }

        private unsafe void OnArtifactBulkLevelUp(Dictionary<string, int> data)
        {
            List<ItemData> list;
            KeyValuePair<string, int> pair;
            Dictionary<string, int>.Enumerator enumerator;
            int num;
            ItemData data2;
            int num2;
            <OnArtifactBulkLevelUp>c__AnonStorey2FD storeyfd;
            list = new List<ItemData>();
            enumerator = data.GetEnumerator();
        Label_000D:
            try
            {
                goto Label_0076;
            Label_0012:
                pair = &enumerator.Current;
                storeyfd = new <OnArtifactBulkLevelUp>c__AnonStorey2FD();
                storeyfd.iname = &pair.Key;
                num = &pair.Value;
                data2 = this.mTmpItems.Find(new Predicate<ItemData>(storeyfd.<>m__290));
                if (data2 == null)
                {
                    goto Label_0076;
                }
                num2 = 0;
                goto Label_006E;
            Label_0060:
                list.Add(data2);
                num2 += 1;
            Label_006E:
                if (num2 < num)
                {
                    goto Label_0060;
                }
            Label_0076:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0012;
                }
                goto Label_0093;
            }
            finally
            {
            Label_0087:
                ((Dictionary<string, int>.Enumerator) enumerator).Dispose();
            }
        Label_0093:
            if (list.Count <= 0)
            {
                goto Label_00A7;
            }
            this.RequestUseAddExpItem(list, null);
        Label_00A7:
            return;
        }

        private unsafe void OnArtifactSelect(SRPG.ArtifactList list)
        {
            object[] objArray;
            int num;
            long num2;
            int num3;
            Json_Artifact artifact;
            ArtifactData.RarityUpResults results;
            objArray = list.Selection;
            this.mTotalSellPrice = 0L;
            this.mSelectedArtifacts = objArray;
            if (((int) objArray.Length) > 0)
            {
                goto Label_0051;
            }
            if ((this.ArtifactSlot != null) == null)
            {
                goto Label_003C;
            }
            this.ArtifactSlot.SetSlotData<ArtifactData>(null);
        Label_003C:
            this.mCurrentArtifact = null;
            this.mCurrentArtifactParam = null;
            this.RefreshWindow();
            return;
        Label_0051:
            if ((this.SellPrice != null) == null)
            {
                goto Label_00CB;
            }
            num = 0;
            goto Label_00C2;
        Label_0069:
            if ((objArray[num] as ArtifactData) == null)
            {
                goto Label_0096;
            }
            this.mTotalSellPrice += (long) ((ArtifactData) objArray[num]).GetSellPrice();
            goto Label_00BE;
        Label_0096:
            if ((objArray[num] as ArtifactParam) == null)
            {
                goto Label_00BE;
            }
            this.mTotalSellPrice += (long) ((ArtifactParam) objArray[num]).sell;
        Label_00BE:
            num += 1;
        Label_00C2:
            if (num < ((int) objArray.Length))
            {
                goto Label_0069;
            }
        Label_00CB:
            if ((this.DecomposeCostTotal != null) == null)
            {
                goto Label_012D;
            }
            num2 = 0L;
            num3 = 0;
            goto Label_0112;
        Label_00E6:
            if ((objArray[num3] as ArtifactData) == null)
            {
                goto Label_010E;
            }
            num2 += (long) ((ArtifactData) objArray[num3]).RarityParam.ArtifactChangeCost;
        Label_010E:
            num3 += 1;
        Label_0112:
            if (num3 < ((int) objArray.Length))
            {
                goto Label_00E6;
            }
            this.DecomposeCostTotal.set_text(&num2.ToString());
        Label_012D:
            this.mCurrentArtifact = null;
            this.mCurrentArtifactParam = null;
            if ((objArray[0] as ArtifactData) != null)
            {
                goto Label_0189;
            }
            if ((objArray[0] as ArtifactParam) == null)
            {
                goto Label_017E;
            }
            this.mCurrentArtifactParam = (ArtifactParam) objArray[0];
            if (this.mCurrentArtifactParam != null)
            {
                goto Label_0189;
            }
            Debug.LogWarning("Non ArtifactParam Selected");
            return;
            goto Label_0189;
        Label_017E:
            Debug.LogWarning("Non ArtifactData Selected");
            return;
        Label_0189:
            if ((objArray[0] as ArtifactData) == null)
            {
                goto Label_01E5;
            }
            this.mCurrentArtifact = (ArtifactData) objArray[0];
            this.mCurrentArtifact = this.mCurrentArtifact.Copy();
            this.mCurrentArtifactParam = this.mCurrentArtifact.ArtifactParam;
            GlobalVars.SelectedArtifactUniqueID.Set(this.mCurrentArtifact.UniqueID);
            goto Label_0228;
        Label_01E5:
            artifact = new Json_Artifact();
            artifact.iname = this.mCurrentArtifactParam.iname;
            artifact.rare = this.mCurrentArtifactParam.rareini;
            this.mCurrentArtifact = new ArtifactData();
            this.mCurrentArtifact.Deserialize(artifact);
        Label_0228:
            this.Rebind();
            this.RefreshWindow();
            if ((list != null) == null)
            {
                goto Label_029F;
            }
            if (list.GetAutoSelected(1) != null)
            {
                goto Label_029F;
            }
            if (this.mCurrentArtifact.CheckEnableRarityUp() != null)
            {
                goto Label_0282;
            }
            if ((this.ProcessToggle_Evolution != null) == null)
            {
                goto Label_029F;
            }
            this.ProcessToggle_Evolution.set_isOn(1);
            goto Label_029F;
        Label_0282:
            if ((this.ProcessToggle_Enhance != null) == null)
            {
                goto Label_029F;
            }
            this.ProcessToggle_Enhance.set_isOn(1);
        Label_029F:
            return;
        }

        private unsafe void OnDecomposeAccept(GameObject go)
        {
            Win_Btn_DecideCancel_FL_C l_fl_c;
            GameManager manager;
            UnitData data;
            JobData data2;
            bool flag;
            int num;
            ArtifactData data3;
            string str;
            if ((this.mConfirmDialogGo != null) == null)
            {
                goto Label_003A;
            }
            l_fl_c = this.mConfirmDialogGo.GetComponent<Win_Btn_DecideCancel_FL_C>();
            if ((l_fl_c != null) == null)
            {
                goto Label_003A;
            }
            if (l_fl_c.AutoClose != null)
            {
                goto Label_003A;
            }
            l_fl_c.BeginClose();
        Label_003A:
            manager = MonoSingleton<GameManager>.Instance;
            flag = 0;
            num = 0;
            goto Label_007F;
        Label_004B:
            data3 = (ArtifactData) this.mSelectedArtifacts[num];
            if (manager.Player.FindOwner(data3, &data, &data2) == null)
            {
                goto Label_0079;
            }
            flag = 1;
            goto Label_008E;
        Label_0079:
            num += 1;
        Label_007F:
            if (num < ((int) this.mSelectedArtifacts.Length))
            {
                goto Label_004B;
            }
        Label_008E:
            if (flag == null)
            {
                goto Label_00C7;
            }
            UIUtility.ConfirmBox(LocalizedText.Get("sys.ARTI_DECOMPOSE_CONFIRM2"), new UIUtility.DialogResultEvent(this.OnDecomposeAccept2), new UIUtility.DialogResultEvent(this.OnDecomposeCancel), null, 0, -1, null, null);
            return;
        Label_00C7:
            this.OnDecomposeAccept2(go);
            return;
        }

        private void OnDecomposeAccept2(GameObject go)
        {
            long[] numArray1;
            GameManager manager;
            int num;
            int num2;
            ArtifactData data;
            long[] numArray;
            int num3;
            Dictionary<ItemParam, int> dictionary;
            int num4;
            manager = MonoSingleton<GameManager>.Instance;
            num = 0;
            num2 = 0;
            goto Label_0039;
        Label_000F:
            data = (ArtifactData) this.mSelectedArtifacts[num2];
            num += Math.Abs(data.RarityParam.ArtifactChangeCost);
            num2 += 1;
        Label_0039:
            if (num2 < ((int) this.mSelectedArtifacts.Length))
            {
                goto Label_000F;
            }
            manager.Player.GainGold(-num);
            GlobalEvent.Invoke(((PredefinedGlobalEvents) 1).ToString(), this);
            if (string.IsNullOrEmpty(this.DecomposeSE) != null)
            {
                goto Label_008A;
            }
            MonoSingleton<MySound>.Instance.PlaySEOneShot(this.DecomposeSE, 0f);
        Label_008A:
            FlowNode_GameObject.ActivateOutputLinks(this, 0x192);
            if (Network.Mode != null)
            {
                goto Label_013B;
            }
            if (this.mSelectedArtifacts == null)
            {
                goto Label_0102;
            }
            if (((int) this.mSelectedArtifacts.Length) <= 0)
            {
                goto Label_0102;
            }
            numArray = new long[(int) this.mSelectedArtifacts.Length];
            num3 = 0;
            goto Label_00F2;
        Label_00CF:
            numArray[num3] = ((ArtifactData) this.mSelectedArtifacts[num3]).UniqueID;
            num3 += 1;
        Label_00F2:
            if (num3 < ((int) numArray.Length))
            {
                goto Label_00CF;
            }
            goto Label_011D;
        Label_0102:
            numArray1 = new long[] { this.mCurrentArtifact.UniqueID };
            numArray = numArray1;
        Label_011D:
            Network.RequestAPI(new ReqArtifactConvert(numArray, new Network.ResponseCallback(this.OnDecomposeResult)), 0);
            goto Label_01F3;
        Label_013B:
            dictionary = manager.Player.CreateItemSnapshot();
            manager.Player.GainItem(this.mCurrentArtifact.Kakera.iname, this.mCurrentArtifact.RarityParam.ArtifactChangePieceNum);
            this.CreateItemChangeSet(dictionary);
            this.ShowDecomposeResult();
            num4 = 0;
            goto Label_01DC;
        Label_018E:
            if (manager.Player.Artifacts[num4].UniqueID != this.mCurrentArtifact.UniqueID)
            {
                goto Label_01D6;
            }
            manager.Player.Artifacts.RemoveAt(num4);
            goto Label_01F3;
        Label_01D6:
            num4 += 1;
        Label_01DC:
            if (num4 < manager.Player.Artifacts.Count)
            {
                goto Label_018E;
            }
        Label_01F3:
            return;
        }

        private void OnDecomposeCancel(GameObject go)
        {
            Win_Btn_DecideCancel_FL_C l_fl_c;
            if ((this.mConfirmDialogGo != null) == null)
            {
                goto Label_003A;
            }
            l_fl_c = this.mConfirmDialogGo.GetComponent<Win_Btn_DecideCancel_FL_C>();
            if ((l_fl_c != null) == null)
            {
                goto Label_003A;
            }
            if (l_fl_c.AutoClose != null)
            {
                goto Label_003A;
            }
            l_fl_c.BeginClose();
        Label_003A:
            return;
        }

        private void OnDecomposeRarityCheck(GameObject go)
        {
            bool flag;
            ArtifactData[] dataArray;
            int num;
            GameObject obj2;
            GameObject obj3;
            ArtifactRarityCheck check;
            flag = 0;
            dataArray = this.GetSelectedArtifacts();
            num = 0;
            goto Label_0035;
        Label_0010:
            if ((dataArray[num].Rarity + 1) < this.RarityCheckValue)
            {
                goto Label_0031;
            }
            flag = 1;
            goto Label_003E;
        Label_0031:
            num += 1;
        Label_0035:
            if (num < ((int) dataArray.Length))
            {
                goto Label_0010;
            }
        Label_003E:
            if (flag == null)
            {
                goto Label_00BD;
            }
            obj2 = AssetManager.Load<GameObject>(ARTIFACT_RARITY_CHECK_UI_PATH);
            if ((obj2 != null) == null)
            {
                goto Label_00BD;
            }
            obj3 = Object.Instantiate<GameObject>(obj2);
            if ((obj3 != null) == null)
            {
                goto Label_00BD;
            }
            check = obj3.GetComponentInChildren<ArtifactRarityCheck>();
            if ((check != null) == null)
            {
                goto Label_00BD;
            }
            check.Setup(2, go, dataArray, this.RarityCheckValue);
            check.OnDecideEvent = new ArtifactRarityCheck.OnArtifactRarityCheckDecideEvent(this.OnDecomposeAccept);
            check.OnCancelEvent = new ArtifactRarityCheck.OnArtifactRarityCheckCancelEvent(this.OnDecomposeCancel);
            return;
        Label_00BD:
            this.OnDecomposeAccept(go);
            return;
        }

        private unsafe void OnDecomposeResult(WWWResult www)
        {
            WebAPI.JSON_BodyResponse<Json_PlayerDataAll> response;
            GameManager manager;
            Dictionary<ItemParam, int> dictionary;
            Exception exception;
            Network.EErrCode code;
            if (Network.IsError == null)
            {
                goto Label_0017;
            }
            code = Network.ErrCode;
            FlowNode_Network.Retry();
            return;
        Label_0017:
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_PlayerDataAll>>(&www.text);
            DebugUtility.Assert((response == null) == 0, "res == null");
            if (response.body != null)
            {
                goto Label_0046;
            }
            FlowNode_Network.Retry();
            return;
        Label_0046:
            manager = MonoSingleton<GameManager>.Instance;
            dictionary = manager.Player.CreateItemSnapshot();
        Label_0058:
            try
            {
                manager.Deserialize(response.body.player);
                manager.Deserialize(response.body.units);
                manager.Deserialize(response.body.items);
                manager.Deserialize(response.body.artifacts, 0);
                manager.Player.UpdateArtifactOwner();
                goto Label_00C3;
            }
            catch (Exception exception1)
            {
            Label_00AD:
                exception = exception1;
                DebugUtility.LogException(exception);
                FlowNode_Network.Retry();
                goto Label_00D5;
            }
        Label_00C3:
            Network.RemoveAPI();
            this.CreateItemChangeSet(dictionary);
            this.ShowDecomposeResult();
        Label_00D5:
            return;
        }

        private void OnDestroy()
        {
            GameManager manager;
            manager = MonoSingleton<GameManager>.GetInstanceDirect();
            if ((manager != null) == null)
            {
                goto Label_0034;
            }
            manager.OnSceneChange = (GameManager.SceneChangeEvent) Delegate.Remove(manager.OnSceneChange, new GameManager.SceneChangeEvent(this.OnSceneCHange));
        Label_0034:
            if ((this.mDetailWindow != null) == null)
            {
                goto Label_005C;
            }
            Object.Destroy(this.mDetailWindow.get_gameObject());
            this.mDetailWindow = null;
        Label_005C:
            if ((this.mResultWindow != null) == null)
            {
                goto Label_007F;
            }
            Object.Destroy(this.mResultWindow);
            this.mResultWindow = null;
        Label_007F:
            if ((this.ArtifactList != null) == null)
            {
                goto Label_00B7;
            }
            this.ArtifactList.OnSelectionChange = (SRPG.ArtifactList.SelectionChangeEvent) Delegate.Remove(this.ArtifactList.OnSelectionChange, new SRPG.ArtifactList.SelectionChangeEvent(this.OnArtifactSelect));
        Label_00B7:
            return;
        }

        private void OnEquipArtifactAccept(GameObject go)
        {
            ArtifactData[] dataArray;
            dataArray = this.GetSelectedArtifacts();
            if (((int) dataArray.Length) >= 0)
            {
                goto Label_0011;
            }
            return;
        Label_0011:
            this.OnEquip(dataArray[0], this.SelectArtifactSlot);
            this.EndEquip();
            return;
        }

        private void OnEquipArtifactCancel(GameObject go)
        {
        }

        public void OnExcludeEquipedValueChanged(bool isOn)
        {
            if ((this.ArtifactList != null) == null)
            {
                goto Label_002F;
            }
            this.WriteExcludeEquipedSettingValue(isOn);
            this.ArtifactList.ExcludeEquiped = isOn;
            this.ArtifactList.Refresh();
        Label_002F:
            return;
        }

        public void OnExpMaxOpen(SRPG_Button button)
        {
            GameObject obj2;
            GameObject obj3;
            ArtifactLevelUpWindow window;
            if (this.mCurrentArtifact.Lv < this.mCurrentArtifact.GetLevelCap())
            {
                goto Label_0036;
            }
            UIUtility.NegativeSystemMessage(null, LocalizedText.Get("sys.LEVEL_CAPPED"), null, null, 0, -1);
            return;
        Label_0036:
            obj2 = AssetManager.Load<GameObject>(ARTIFACT_EXPMAX_UI_PATH);
            if ((obj2 != null) == null)
            {
                goto Label_009E;
            }
            obj3 = Object.Instantiate<GameObject>(obj2);
            if ((obj3 != null) == null)
            {
                goto Label_009E;
            }
            window = obj3.GetComponentInChildren<ArtifactLevelUpWindow>();
            if ((window != null) == null)
            {
                goto Label_0085;
            }
            window.OnDecideEvent = new ArtifactLevelUpWindow.OnArtifactLevelupEvent(this.OnArtifactBulkLevelUp);
        Label_0085:
            DataSource.Bind<ArtifactData>(obj3, this.mCurrentArtifact);
            DataSource.Bind<ArtifactWindow>(obj3, this);
            GameParameter.UpdateAll(obj3);
        Label_009E:
            return;
        }

        private unsafe void OnKyokaEnd()
        {
            UnitData data;
            int num;
            PlayerData data2;
            JobData data3;
            BaseStatus status;
            BaseStatus status2;
            BaseStatus status3;
            BaseStatus status4;
            data = null;
            num = 0;
            data2 = MonoSingleton<GameManager>.Instance.Player;
            if ((this.OwnerSlot != null) == null)
            {
                goto Label_0044;
            }
            data3 = null;
            if (data2.FindOwner(this.mCurrentArtifact, &data, &data3) == null)
            {
                goto Label_0044;
            }
            num = Array.IndexOf<JobData>(data.Jobs, data3);
        Label_0044:
            if (this.mOwnerUnitData == null)
            {
                goto Label_005D;
            }
            data = this.mOwnerUnitData;
            num = this.mOwnerUnitJobIndex;
        Label_005D:
            status = new BaseStatus();
            status2 = new BaseStatus();
            this.mCurrentArtifact.GetHomePassiveBuffStatus(&status, &status2, null, 0, 1);
            status3 = new BaseStatus();
            status4 = new BaseStatus();
            this.mCurrentArtifact.GetHomePassiveBuffStatus(&status3, &status4, data, num, 1);
            this.RefreshWindow();
            this.RefreshArtifactList();
            FlowNode_GameObject.ActivateOutputLinks(this, 0x98);
            return;
        }

        private void OnKyokaItemSelect(GameObject go)
        {
            ItemData data;
            data = DataSource.FindDataOfClass<ItemData>(go, null);
            if (data == null)
            {
                goto Label_001A;
            }
            if (data.Num > 0)
            {
                goto Label_001B;
            }
        Label_001A:
            return;
        Label_001B:
            this.RequestUseAddExpItem(data, go);
            return;
        }

        private void OnLevelChange(int prev, int next)
        {
        }

        private bool OnSceneCHange()
        {
            this.mSceneChanging = 1;
            this.FlushRequests(0);
            if (base.get_gameObject().get_activeInHierarchy() == null)
            {
                goto Label_0034;
            }
            MonoSingleton<GameManager>.Instance.RegisterImportantJob(base.StartCoroutine(this.OnSceneChangeAsync()));
        Label_0034:
            return 1;
        }

        [DebuggerHidden]
        private IEnumerator OnSceneChangeAsync()
        {
            <OnSceneChangeAsync>c__IteratorE8 re;
            re = new <OnSceneChangeAsync>c__IteratorE8();
            re.<>f__this = this;
            return re;
        }

        private unsafe void OnSellAccept(GameObject go)
        {
            Win_Btn_DecideCancel_FL_C l_fl_c;
            ArtifactData[] dataArray;
            bool flag;
            int num;
            UnitData data;
            JobData data2;
            string str;
            if ((this.mConfirmDialogGo != null) == null)
            {
                goto Label_003A;
            }
            l_fl_c = this.mConfirmDialogGo.GetComponent<Win_Btn_DecideCancel_FL_C>();
            if ((l_fl_c != null) == null)
            {
                goto Label_003A;
            }
            if (l_fl_c.AutoClose != null)
            {
                goto Label_003A;
            }
            l_fl_c.BeginClose();
        Label_003A:
            dataArray = this.GetSelectedArtifacts();
            flag = 0;
            num = 0;
            goto Label_0076;
        Label_004A:
            data = null;
            data2 = null;
            if (MonoSingleton<GameManager>.Instance.Player.FindOwner(dataArray[num], &data, &data2) == null)
            {
                goto Label_0072;
            }
            flag = 1;
            goto Label_007F;
        Label_0072:
            num += 1;
        Label_0076:
            if (num < ((int) dataArray.Length))
            {
                goto Label_004A;
            }
        Label_007F:
            if (flag == null)
            {
                goto Label_00B7;
            }
            UIUtility.ConfirmBox(LocalizedText.Get("sys.ARTI_SELL_CONFIRM2"), new UIUtility.DialogResultEvent(this.OnSellAccept2), new UIUtility.DialogResultEvent(this.OnSellCancel), null, 0, -1, null, null);
            return;
        Label_00B7:
            this.OnSellAccept2(go);
            return;
        }

        private void OnSellAccept2(GameObject go)
        {
            ArtifactData[] dataArray;
            long[] numArray;
            int num;
            CurrencyTracker tracker;
            List<ChangeListData> list;
            dataArray = this.GetSelectedArtifacts();
            if (string.IsNullOrEmpty(this.SellSE) != null)
            {
                goto Label_002C;
            }
            MonoSingleton<MySound>.Instance.PlaySEOneShot(this.SellSE, 0f);
        Label_002C:
            numArray = new long[(int) dataArray.Length];
            num = 0;
            goto Label_0050;
        Label_003C:
            numArray[num] = dataArray[num].UniqueID;
            num += 1;
        Label_0050:
            if (num < ((int) dataArray.Length))
            {
                goto Label_003C;
            }
            FlowNode_GameObject.ActivateOutputLinks(this, 0x259);
            if (Network.Mode != null)
            {
                goto Label_008B;
            }
            Network.RequestAPI(new ReqArtifactSell(numArray, new Network.ResponseCallback(this.OnSellResult)), 0);
            goto Label_00C6;
        Label_008B:
            tracker = new CurrencyTracker();
            MonoSingleton<GameManager>.Instance.Player.OfflineSellArtifacts(dataArray);
            tracker.EndTracking();
            list = new List<ChangeListData>(4);
            this.AddCurrencyChangeSet(tracker, list);
            this.mChangeSet = list;
            this.ShowSellResult();
        Label_00C6:
            return;
        }

        private void OnSellCancel(GameObject go)
        {
            Win_Btn_DecideCancel_FL_C l_fl_c;
            if ((this.mConfirmDialogGo != null) == null)
            {
                goto Label_003A;
            }
            l_fl_c = this.mConfirmDialogGo.GetComponent<Win_Btn_DecideCancel_FL_C>();
            if ((l_fl_c != null) == null)
            {
                goto Label_003A;
            }
            if (l_fl_c.AutoClose != null)
            {
                goto Label_003A;
            }
            l_fl_c.BeginClose();
        Label_003A:
            return;
        }

        private void OnSellRarityCheck(GameObject go)
        {
            bool flag;
            ArtifactData[] dataArray;
            int num;
            GameObject obj2;
            GameObject obj3;
            ArtifactRarityCheck check;
            flag = 0;
            dataArray = this.GetSelectedArtifacts();
            num = 0;
            goto Label_0035;
        Label_0010:
            if ((dataArray[num].Rarity + 1) < this.RarityCheckValue)
            {
                goto Label_0031;
            }
            flag = 1;
            goto Label_003E;
        Label_0031:
            num += 1;
        Label_0035:
            if (num < ((int) dataArray.Length))
            {
                goto Label_0010;
            }
        Label_003E:
            if (flag == null)
            {
                goto Label_00BD;
            }
            obj2 = AssetManager.Load<GameObject>(ARTIFACT_RARITY_CHECK_UI_PATH);
            if ((obj2 != null) == null)
            {
                goto Label_00BD;
            }
            obj3 = Object.Instantiate<GameObject>(obj2);
            if ((obj3 != null) == null)
            {
                goto Label_00BD;
            }
            check = obj3.GetComponentInChildren<ArtifactRarityCheck>();
            if ((check != null) == null)
            {
                goto Label_00BD;
            }
            check.Setup(1, go, dataArray, this.RarityCheckValue);
            check.OnDecideEvent = new ArtifactRarityCheck.OnArtifactRarityCheckDecideEvent(this.OnSellAccept);
            check.OnCancelEvent = new ArtifactRarityCheck.OnArtifactRarityCheckCancelEvent(this.OnSellCancel);
            return;
        Label_00BD:
            this.OnSellAccept(go);
            return;
        }

        private unsafe void OnSellResult(WWWResult www)
        {
            GameManager manager;
            CurrencyTracker tracker;
            WebAPI.JSON_BodyResponse<Json_PlayerDataAll> response;
            Exception exception;
            List<ChangeListData> list;
            Network.EErrCode code;
            if (Network.IsError == null)
            {
                goto Label_0017;
            }
            code = Network.ErrCode;
            FlowNode_Network.Retry();
            return;
        Label_0017:
            manager = MonoSingleton<GameManager>.Instance;
            tracker = new CurrencyTracker();
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_PlayerDataAll>>(&www.text);
            DebugUtility.Assert((response == null) == 0, "res == null");
            if (response.body != null)
            {
                goto Label_0052;
            }
            FlowNode_Network.Retry();
            return;
        Label_0052:
            try
            {
                manager.Deserialize(response.body.player);
                manager.Deserialize(response.body.units);
                manager.Deserialize(response.body.artifacts, 0);
                manager.Player.UpdateArtifactOwner();
                goto Label_00AC;
            }
            catch (Exception exception1)
            {
            Label_0096:
                exception = exception1;
                DebugUtility.LogException(exception);
                FlowNode_Network.Retry();
                goto Label_00D6;
            }
        Label_00AC:
            Network.RemoveAPI();
            tracker.EndTracking();
            list = new List<ChangeListData>(4);
            this.AddCurrencyChangeSet(tracker, list);
            this.mChangeSet = list;
            this.ShowSellResult();
        Label_00D6:
            return;
        }

        private unsafe void OnTransmuteAccept(GameObject go)
        {
            PlayerData data;
            RarityParam param;
            string str;
            string str2;
            ItemData data2;
            Json_Artifact[] artifactArray;
            data = MonoSingleton<GameManager>.Instance.Player;
            param = MonoSingleton<GameManager>.Instance.GetRarityParam(this.mCurrentArtifactParam.rareini);
            this.mCachedArtifacts = data.Artifacts.ToArray();
            data.GainGold(-param.ArtifactCreateCost);
            GlobalEvent.Invoke(((PredefinedGlobalEvents) 1).ToString(), this);
            FlowNode_GameObject.ActivateOutputLinks(this, 0x12d);
            if (Network.Mode != null)
            {
                goto Label_00B2;
            }
            data.OnArtifactTransmute(this.mCurrentArtifactParam.iname);
            MonoSingleton<GameManager>.Instance.ServerSyncTrophyExecStart(&str, &str2);
            Network.RequestAPI(new ReqArtifactAdd(this.mCurrentArtifactParam.iname, new Network.ResponseCallback(this.OnTransmuteResult), str, str2), 0);
            goto Label_013C;
        Label_00B2:
            data2 = data.FindItemDataByItemID(this.mCurrentArtifactParam.kakera);
            if (data2 == null)
            {
                goto Label_00DE;
            }
            data2.Used(param.ArtifactCreatePieceNum);
        Label_00DE:
            artifactArray = new Json_Artifact[] { new Json_Artifact() };
            artifactArray[0].iid = (long) Random.Range(1, 0x7fffffff);
            artifactArray[0].iname = this.mCurrentArtifactParam.iname;
            MonoSingleton<GameManager>.Instance.Deserialize(artifactArray, 0);
            this.ShowTransmuteResult();
            data.OnArtifactTransmute(this.mCurrentArtifactParam.iname);
        Label_013C:
            return;
        }

        private void OnTransmuteCancel(GameObject go)
        {
        }

        private unsafe void OnTransmuteResult(WWWResult www)
        {
            WebAPI.JSON_BodyResponse<Json_PlayerDataAll> response;
            GameManager manager;
            Exception exception;
            Network.EErrCode code;
            if (Network.IsError == null)
            {
                goto Label_0067;
            }
            code = Network.ErrCode;
            if (code == 0x2328)
            {
                goto Label_002B;
            }
            if (code == 0x2329)
            {
                goto Label_0046;
            }
            goto Label_0061;
        Label_002B:
            FlowNode_Network.Back();
            UIUtility.NegativeSystemMessage(null, LocalizedText.Get("sys.ARTI_FULL"), null, null, 0, -1);
            return;
        Label_0046:
            UIUtility.NegativeSystemMessage(null, LocalizedText.Get("sys.ARTI_NOKAKERA_ERR"), null, null, 0, -1);
            FlowNode_Network.Back();
            return;
        Label_0061:
            FlowNode_Network.Retry();
            return;
        Label_0067:
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_PlayerDataAll>>(&www.text);
            DebugUtility.Assert((response == null) == 0, "res == null");
            if (response.body != null)
            {
                goto Label_0096;
            }
            FlowNode_Network.Retry();
            return;
        Label_0096:
            manager = MonoSingleton<GameManager>.Instance;
        Label_009C:
            try
            {
                manager.Deserialize(response.body.player);
                manager.Deserialize(response.body.items);
                manager.Deserialize(response.body.units);
                manager.Deserialize(response.body.artifacts, 0);
                goto Label_00FC;
            }
            catch (Exception exception1)
            {
            Label_00E6:
                exception = exception1;
                DebugUtility.LogException(exception);
                FlowNode_Network.Retry();
                goto Label_011C;
            }
        Label_00FC:
            Network.RemoveAPI();
            MonoSingleton<GameManager>.Instance.ServerSyncTrophyExecEnd(www);
            if (Network.IsImmediateMode != null)
            {
                goto Label_011C;
            }
            this.ShowTransmuteResult();
        Label_011C:
            return;
        }

        private bool ReadExcludeEquipedSettingValue()
        {
            return ((PlayerPrefsUtility.GetInt(PlayerPrefsUtility.ARTIFACT_EXCLUDE_EQUIPED, 0) != null) ? 1 : 0);
        }

        private void Rebind()
        {
            this.UpdateArtifactOwner();
            if ((this.ArtifactSlot != null) == null)
            {
                goto Label_0028;
            }
            this.ArtifactSlot.SetSlotData<ArtifactData>(this.mCurrentArtifact);
        Label_0028:
            if ((this.WindowBody != null) == null)
            {
                goto Label_006C;
            }
            DataSource.Bind<ArtifactData>(this.WindowBody, this.mCurrentArtifact);
            DataSource.Bind<UnitData>(this.WindowBody, this.mCurrentArtifactOwner);
            DataSource.Bind<JobData>(this.WindowBody, this.mCurrentArtifactOwnerJob);
        Label_006C:
            if ((this.m_SetEffectsButton != null) == null)
            {
                goto Label_00C2;
            }
            if (this.mCurrentArtifact == null)
            {
                goto Label_00C2;
            }
            this.m_SetEffectsButton.set_interactable(MonoSingleton<GameManager>.Instance.MasterParam.ExistSkillAbilityDeriveDataWithArtifact(this.mCurrentArtifact.ArtifactParam.iname));
            ArtifactSetList.SetSelectedArtifactParam(this.mCurrentArtifact.ArtifactParam);
        Label_00C2:
            return;
        }

        public unsafe void RefreshAbilities()
        {
            List<AbilityDeriveParam> list;
            SkillAbilityDeriveData data;
            UnitData data2;
            int num;
            JobData data3;
            MasterParam param;
            ArtifactParam param2;
            int num2;
            List<AbilityData> list2;
            int num3;
            AbilityParam param3;
            GameObject obj2;
            GameObject obj3;
            Animator animator;
            bool flag;
            int num4;
            AbilityData data4;
            AbilityDeriveParam param4;
            Animator animator2;
            <RefreshAbilities>c__AnonStorey2FE storeyfe;
            if ((this.mCurrentArtifact != null) && ((this.AbilityListItem == null) == null))
            {
                goto Label_001D;
            }
            return;
        Label_001D:
            DataSource.Bind<AbilityParam>(this.AbilityListItem, null);
            DataSource.Bind<AbilityData>(this.AbilityListItem, null);
            DataSource.Bind<SkillAbilityDeriveData>(this.AbilityListItem, null);
            list = null;
            if (this.mOwnerUnitData == null)
            {
                goto Label_0090;
            }
            data = this.mOwnerUnitData.GetSkillAbilityDeriveData(this.mOwnerUnitData.Jobs[this.mOwnerUnitJobIndex], this.mSelectArtifactSlot, this.mCurrentArtifact.ArtifactParam);
            list = (data != null) ? data.GetAvailableAbilityDeriveParams() : null;
        Label_0090:
            if ((this.OwnerSlot != null) == null)
            {
                goto Label_010B;
            }
            data2 = null;
            num = -1;
            if ((this.OwnerSlot != null) == null)
            {
                goto Label_010B;
            }
            data3 = null;
            if (MonoSingleton<GameManager>.Instance.Player.FindOwner(this.mCurrentArtifact, &data2, &data3) == null)
            {
                goto Label_00E5;
            }
            num = Array.IndexOf<JobData>(data2.Jobs, data3);
        Label_00E5:
            if (this.mOwnerUnitData == null)
            {
                goto Label_00FE;
            }
            data2 = this.mOwnerUnitData;
            num = this.mOwnerUnitJobIndex;
        Label_00FE:
            if (data2 == null)
            {
                goto Label_010B;
            }
            if (num == -1)
            {
                goto Label_010B;
            }
        Label_010B:
            param = MonoSingleton<GameManager>.Instance.MasterParam;
            param2 = this.mCurrentArtifact.ArtifactParam;
            num2 = 0;
            list2 = this.mCurrentArtifact.LearningAbilities;
            if (param2.abil_inames == null)
            {
                goto Label_03D2;
            }
            num3 = 0;
            goto Label_03C2;
        Label_0148:
            if (string.IsNullOrEmpty(param2.abil_inames[num3]) == null)
            {
                goto Label_0161;
            }
            goto Label_03BC;
        Label_0161:
            if (param2.abil_shows[num3] != null)
            {
                goto Label_0175;
            }
            goto Label_03BC;
        Label_0175:
            param3 = param.GetAbilityParam(param2.abil_inames[num3]);
            if (param3 != null)
            {
                goto Label_0194;
            }
            goto Label_03BC;
        Label_0194:
            obj2 = null;
            if ((this.AbilityList == null) == null)
            {
                goto Label_01E4;
            }
            if (DataSource.FindDataOfClass<AbilityParam>(this.AbilityListItem, null) != null)
            {
                goto Label_01D7;
            }
            if (DataSource.FindDataOfClass<AbilityData>(this.AbilityListItem, null) != null)
            {
                goto Label_01D7;
            }
            DataSource.Bind<AbilityParam>(this.AbilityListItem, param3);
        Label_01D7:
            obj2 = this.AbilityListItem;
            goto Label_025E;
        Label_01E4:
            if (this.mAbilityItems.Count > num2)
            {
                goto Label_0228;
            }
            obj3 = Object.Instantiate<GameObject>(this.AbilityListItem);
            obj3.get_transform().SetParent(this.AbilityList.get_transform(), 0);
            this.mAbilityItems.Add(obj3);
        Label_0228:
            DataSource.Bind<AbilityParam>(this.mAbilityItems[num2], param3);
            this.mAbilityItems[num2].SetActive(1);
            obj2 = this.mAbilityItems[num2];
        Label_025E:
            DataSource.Bind<AbilityData>(obj2, null);
            animator = obj2.GetComponent<Animator>();
            flag = 0;
            if ((animator != null) == null)
            {
                goto Label_03AA;
            }
            if (list2 == null)
            {
                goto Label_03AA;
            }
            if (string.IsNullOrEmpty(this.AbilityListItemState) != null)
            {
                goto Label_03AA;
            }
            if (list2 == null)
            {
                goto Label_0378;
            }
            num4 = 0;
            goto Label_036A;
        Label_02A5:
            storeyfe = new <RefreshAbilities>c__AnonStorey2FE();
            storeyfe.iname = list2[num4].Param.iname;
            if ((param2.abil_inames[num3] == storeyfe.iname) == null)
            {
                goto Label_0364;
            }
            data4 = list2[num4];
            if (list == null)
            {
                goto Label_0320;
            }
            param4 = list.Find(new Predicate<AbilityDeriveParam>(storeyfe.<>m__291));
            if (param4 == null)
            {
                goto Label_0320;
            }
            data4 = list2[num4].CreateDeriveAbility(param4);
        Label_0320:
            DataSource.Bind<AbilityData>(obj2, data4);
            DataSource.Bind<AbilityParam>(obj2, data4.Param);
            if (this.mOwnerUnitData == null)
            {
                goto Label_035C;
            }
            flag = param3.CheckEnableUseAbility(this.mOwnerUnitData, this.mOwnerUnitJobIndex);
            goto Label_035F;
        Label_035C:
            flag = 1;
        Label_035F:
            goto Label_0378;
        Label_0364:
            num4 += 1;
        Label_036A:
            if (num4 < list2.Count)
            {
                goto Label_02A5;
            }
        Label_0378:
            if (flag == null)
            {
                goto Label_0397;
            }
            animator.SetInteger(this.AbilityListItemState, this.AbilityListItem_Unlocked);
            goto Label_03AA;
        Label_0397:
            animator.SetInteger(this.AbilityListItemState, this.AbilityListItem_Hidden);
        Label_03AA:
            num2 += 1;
            if (flag == null)
            {
                goto Label_03BC;
            }
            goto Label_03D2;
        Label_03BC:
            num3 += 1;
        Label_03C2:
            if (num3 < ((int) param2.abil_inames.Length))
            {
                goto Label_0148;
            }
        Label_03D2:
            if ((this.AbilityList == null) == null)
            {
                goto Label_045D;
            }
            if (num2 != null)
            {
                goto Label_045D;
            }
            DataSource.Bind<AbilityParam>(this.AbilityListItem, null);
            DataSource.Bind<AbilityData>(this.AbilityListItem, null);
            animator2 = this.AbilityListItem.GetComponent<Animator>();
            if ((animator2 != null) == null)
            {
                goto Label_045D;
            }
            if (string.IsNullOrEmpty(this.AbilityListItemState) != null)
            {
                goto Label_045D;
            }
            animator2.SetInteger(this.AbilityListItemState, this.AbilityListItem_Hidden);
            goto Label_045D;
        Label_0444:
            this.mAbilityItems[num2].SetActive(0);
            num2 += 1;
        Label_045D:
            if (num2 < this.mAbilityItems.Count)
            {
                goto Label_0444;
            }
            return;
        }

        public void RefreshArtifactList()
        {
            if ((this.ArtifactList != null) == null)
            {
                goto Label_001C;
            }
            this.ArtifactList.Refresh();
        Label_001C:
            return;
        }

        public unsafe void RefreshDecomposeInfo()
        {
            object[] objArray1;
            Animator animator;
            string str;
            long num;
            Animator animator2;
            int num2;
            bool flag;
            int num3;
            int num4;
            int num5;
            if (this.mCurrentArtifact == null)
            {
                goto Label_0020;
            }
            if (this.mCurrentArtifact.UniqueID != null)
            {
                goto Label_0021;
            }
        Label_0020:
            return;
        Label_0021:
            if ((this.DecomposePanel != null) == null)
            {
                goto Label_0093;
            }
            if (string.IsNullOrEmpty(this.DecomposePanelState) != null)
            {
                goto Label_0093;
            }
            animator = this.DecomposePanel.GetComponent<Animator>();
            if ((animator != null) == null)
            {
                goto Label_0093;
            }
            if (this.mCurrentArtifact.Kakera == null)
            {
                goto Label_0081;
            }
            animator.SetInteger(this.DecomposePanelState, this.DecomposePanel_Normal);
            goto Label_0093;
        Label_0081:
            animator.SetInteger(this.DecomposePanelState, this.DecomposePanel_Disabled);
        Label_0093:
            if (this.mCurrentArtifact.Kakera == null)
            {
                goto Label_026E;
            }
            if ((this.DecomposeHelp != null) == null)
            {
                goto Label_010A;
            }
            objArray1 = new object[] { this.mCurrentArtifact.ArtifactParam.name, this.mCurrentArtifact.Kakera.name, (int) this.mCurrentArtifact.GetKakeraChangeNum() };
            str = LocalizedText.Get("sys.ARTI_DECOMPOSE_HELP", objArray1);
            this.DecomposeHelp.set_text(str);
        Label_010A:
            if ((this.DecomposeCost != null) == null)
            {
                goto Label_01B0;
            }
            num = (long) Math.Abs(this.mCurrentArtifact.RarityParam.ArtifactChangeCost);
            this.DecomposeCost.set_text(&num.ToString());
            if (string.IsNullOrEmpty(this.DecomposeCostState) != null)
            {
                goto Label_01B0;
            }
            animator2 = this.DecomposeCost.GetComponent<Animator>();
            if ((animator2 != null) == null)
            {
                goto Label_01B0;
            }
            if (((long) MonoSingleton<GameManager>.Instance.Player.Gold) < num)
            {
                goto Label_019E;
            }
            animator2.SetInteger(this.DecomposeCostState, this.DecomposeCost_Normal);
            goto Label_01B0;
        Label_019E:
            animator2.SetInteger(this.DecomposeCostState, this.DecomposeCost_NoGold);
        Label_01B0:
            if ((this.DecomposeItem != null) == null)
            {
                goto Label_01E2;
            }
            this.DecomposeItem.SetMaterial(this.mCurrentArtifact.GetKakeraChangeNum(), this.mCurrentArtifact.Kakera);
        Label_01E2:
            num2 = MonoSingleton<GameManager>.Instance.Player.GetItemAmount(this.mCurrentArtifact.Kakera.iname);
            if ((this.DecomposeKakeraNumOld != null) == null)
            {
                goto Label_0226;
            }
            this.DecomposeKakeraNumOld.set_text(&num2.ToString());
        Label_0226:
            if ((this.DecomposeKakeraNumNew != null) == null)
            {
                goto Label_026E;
            }
            this.DecomposeKakeraNumNew.set_text(&Mathf.Min(num2 + this.mCurrentArtifact.GetKakeraChangeNum(), this.mCurrentArtifact.Kakera.cap).ToString());
        Label_026E:
            if ((this.DecomposeWarning != null) == null)
            {
                goto Label_02FE;
            }
            flag = 0;
            if (this.mSelectedArtifacts == null)
            {
                goto Label_02EC;
            }
            num3 = 0;
            goto Label_02DD;
        Label_0295:
            if ((this.mSelectedArtifacts[num3] as ArtifactData) == null)
            {
                goto Label_02D7;
            }
            if (((ArtifactData) this.mSelectedArtifacts[num3]).Rarity < this.DecomposeWarningRarity)
            {
                goto Label_02D7;
            }
            flag = 1;
            goto Label_02EC;
        Label_02D7:
            num3 += 1;
        Label_02DD:
            if (num3 < ((int) this.mSelectedArtifacts.Length))
            {
                goto Label_0295;
            }
        Label_02EC:
            this.DecomposeWarning.get_gameObject().SetActive(flag);
        Label_02FE:
            return;
        }

        public void RefreshKyokaList()
        {
            PlayerData data;
            List<ItemData> list;
            int num;
            Transform transform;
            int num2;
            GameObject obj2;
            ItemData data2;
            ListItemEvents events;
            if ((this.KyokaList == null) != null)
            {
                goto Label_0022;
            }
            if ((this.KyokaListItem == null) == null)
            {
                goto Label_0023;
            }
        Label_0022:
            return;
        Label_0023:
            if ((this.KyokaList == this.KyokaListItem) != null)
            {
                goto Label_0059;
            }
            if (this.KyokaList.get_transform().IsChildOf(this.KyokaListItem.get_transform()) == null)
            {
                goto Label_005A;
            }
        Label_0059:
            return;
        Label_005A:
            list = MonoSingleton<GameManager>.Instance.Player.Items;
            num = 0;
            transform = this.KyokaList.get_transform();
            num2 = 0;
            goto Label_01A0;
        Label_0082:
            if (list[num2].ItemType != 13)
            {
                goto Label_019A;
            }
            obj2 = null;
            data2 = new ItemData();
            data2.Setup(list[num2].UniqueID, list[num2].Param, list[num2].NumNonCap);
            if (this.mKyokaItems.Count > num)
            {
                goto Label_0143;
            }
            obj2 = Object.Instantiate<GameObject>(this.KyokaListItem);
            if ((obj2 != null) == null)
            {
                goto Label_015F;
            }
            this.mKyokaItems.Add(obj2);
            obj2.get_transform().SetParent(transform, 0);
            events = obj2.GetComponent<ListItemEvents>();
            if ((events != null) == null)
            {
                goto Label_015F;
            }
            events.OnSelect = new ListItemEvents.ListItemEvent(this.OnKyokaItemSelect);
            goto Label_015F;
        Label_0143:
            obj2 = this.mKyokaItems[num];
            data2 = this.mTmpItems[num];
        Label_015F:
            DataSource.Bind<ItemData>(obj2, data2);
            obj2.SetActive(1);
            GameParameter.UpdateAll(obj2);
            if (this.mTmpItems.Contains(data2) != null)
            {
                goto Label_0196;
            }
            this.mTmpItems.Add(data2);
        Label_0196:
            num += 1;
        Label_019A:
            num2 += 1;
        Label_01A0:
            if (num2 < list.Count)
            {
                goto Label_0082;
            }
            goto Label_01C8;
        Label_01B2:
            this.mKyokaItems[num].SetActive(0);
            num += 1;
        Label_01C8:
            if (num < this.mKyokaItems.Count)
            {
                goto Label_01B2;
            }
            this.mDisableFlushRequest = 1;
            return;
        }

        public unsafe void RefreshRarityUpInfo()
        {
            bool flag;
            Animator animator;
            List<ItemData> list;
            int num;
            List<ItemData> list2;
            int num2;
            Transform transform;
            int num3;
            int num4;
            int num5;
            GameObject obj2;
            int num6;
            MaterialPanel panel;
            <RefreshRarityUpInfo>c__AnonStorey2FF storeyff;
            if (this.mCurrentArtifact != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            flag = this.mCurrentArtifact.CheckEnableRarityUp() == 0;
            if ((this.RarityUpPanel != null) == null)
            {
                goto Label_00F3;
            }
            animator = this.RarityUpPanel.GetComponent<Animator>();
            if ((animator != null) == null)
            {
                goto Label_00F3;
            }
            if (string.IsNullOrEmpty(this.RarityUpPanelState) != null)
            {
                goto Label_00F3;
            }
            if (this.mCurrentArtifact.Rarity >= this.mCurrentArtifact.RarityCap)
            {
                goto Label_00E1;
            }
            storeyff = new <RefreshRarityUpInfo>c__AnonStorey2FF();
            storeyff.hasKakera = 0;
            this.mCurrentArtifact.GetKakeraDataListForRarityUp().ForEach(new Action<ItemData>(storeyff.<>m__292));
            if (storeyff.hasKakera == null)
            {
                goto Label_00CA;
            }
            animator.SetInteger(this.RarityUpPanelState, this.RarityUpPanel_Normal);
            goto Label_00DC;
        Label_00CA:
            animator.SetInteger(this.RarityUpPanelState, this.RarityUpPanel_NoItem);
        Label_00DC:
            goto Label_00F3;
        Label_00E1:
            animator.SetInteger(this.RarityUpPanelState, this.RarityUpPanel_MaxRarity);
        Label_00F3:
            if ((this.RarityUpButton != null) == null)
            {
                goto Label_0110;
            }
            this.RarityUpButton.set_interactable(flag);
        Label_0110:
            if ((this.RarityUpHilit != null) == null)
            {
                goto Label_012D;
            }
            this.RarityUpHilit.SetActive(flag);
        Label_012D:
            if ((this.RarityUpIconRoot != null) == null)
            {
                goto Label_014A;
            }
            this.RarityUpIconRoot.SetActive(flag);
        Label_014A:
            if (this.mCurrentArtifact.Rarity >= this.mCurrentArtifact.RarityCap)
            {
                goto Label_0382;
            }
            if ((this.RarityUpCost != null) == null)
            {
                goto Label_01A8;
            }
            num = this.mCurrentArtifact.RarityParam.ArtifactRarityUpCost;
            this.RarityUpCost.set_text(&num.ToString());
        Label_01A8:
            if ((this.RarityUpIconRoot != null) == null)
            {
                goto Label_01C5;
            }
            this.RarityUpIconRoot.SetActive(flag);
        Label_01C5:
            if ((this.RarityUpList != null) == null)
            {
                goto Label_0382;
            }
            if ((this.RarityUpListItem != null) == null)
            {
                goto Label_0382;
            }
            list2 = this.mCurrentArtifact.GetKakeraDataListForRarityUp();
            num2 = 0;
            transform = this.RarityUpList.get_transform();
            num3 = Mathf.Max(list2.Count, this.mRarityUpItems.Count);
            num4 = this.mCurrentArtifact.GetKakeraNeedNum();
            num5 = 0;
            goto Label_0342;
        Label_0232:
            obj2 = null;
            if (this.mRarityUpItems.Count > num2)
            {
                goto Label_0282;
            }
            obj2 = Object.Instantiate<GameObject>(this.RarityUpListItem);
            if ((obj2 != null) == null)
            {
                goto Label_0291;
            }
            this.mRarityUpItems.Add(obj2);
            obj2.get_transform().SetParent(transform, 0);
            goto Label_0291;
        Label_0282:
            obj2 = this.mRarityUpItems[num2];
        Label_0291:
            if (num5 < list2.Count)
            {
                goto Label_02A4;
            }
            goto Label_033C;
        Label_02A4:
            DataSource.Bind<ItemData>(obj2, list2[num5]);
            num6 = Mathf.Min(num4, list2[num5].Num);
            if (num6 <= 0)
            {
                goto Label_030D;
            }
            panel = obj2.GetComponent<MaterialPanel>();
            if ((panel != null) == null)
            {
                goto Label_0300;
            }
            panel.SetMaterial(num6, list2[num5].Param);
        Label_0300:
            obj2.SetActive(1);
            goto Label_0315;
        Label_030D:
            obj2.SetActive(0);
        Label_0315:
            num4 -= Mathf.Min(num4, list2[num5].Num);
            GameParameter.UpdateAll(obj2);
            num2 += 1;
        Label_033C:
            num5 += 1;
        Label_0342:
            if (num5 < num3)
            {
                goto Label_0232;
            }
            goto Label_0369;
        Label_0350:
            this.mRarityUpItems[num2].SetActive(0);
            num2 += 1;
        Label_0369:
            if (num2 < this.mRarityUpItems.Count)
            {
                goto Label_0350;
            }
            this.mDisableFlushRequest = 1;
        Label_0382:
            return;
        }

        public unsafe void RefreshTransmuteInfo()
        {
            object[] objArray1;
            Animator animator;
            GameManager manager;
            RarityParam param;
            ItemParam param2;
            int num;
            int num2;
            string str;
            if (this.mCurrentArtifactParam != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            if ((this.TransmutePanel != null) == null)
            {
                goto Label_007E;
            }
            animator = this.TransmutePanel.GetComponent<Animator>();
            if ((animator != null) == null)
            {
                goto Label_007E;
            }
            if (string.IsNullOrEmpty(this.TransmutePanelState) != null)
            {
                goto Label_007E;
            }
            if (this.mCurrentArtifactParam.is_create == null)
            {
                goto Label_006C;
            }
            animator.SetInteger(this.TransmutePanelState, this.TransmutePanel_Normal);
            goto Label_007E;
        Label_006C:
            animator.SetInteger(this.TransmutePanelState, this.TransmutePanel_Disabled);
        Label_007E:
            manager = MonoSingleton<GameManager>.Instance;
            param = manager.MasterParam.GetRarityParam(this.mCurrentArtifactParam.rareini);
            param2 = manager.GetItemParam(this.mCurrentArtifactParam.kakera);
            if ((this.TransmuteMaterial != null) == null)
            {
                goto Label_00D5;
            }
            this.TransmuteMaterial.SetMaterial(param.ArtifactCreatePieceNum, param2);
        Label_00D5:
            if ((this.TransmuteCost != null) == null)
            {
                goto Label_0105;
            }
            num = param.ArtifactCreateCost;
            this.TransmuteCost.set_text(&num.ToString());
        Label_0105:
            if ((this.TransmuteCondition != null) == null)
            {
                goto Label_0163;
            }
            num2 = this.mCurrentArtifact.GetKakeraCreateNum();
            if (param2 == null)
            {
                goto Label_0163;
            }
            if (num2 <= 0)
            {
                goto Label_0163;
            }
            objArray1 = new object[] { param2.name, (int) num2 };
            str = LocalizedText.Get("sys.ARTI_TRANSMUTE_REQ", objArray1);
            this.TransmuteCondition.set_text(str);
        Label_0163:
            return;
        }

        public unsafe void RefreshWindow()
        {
            PlayerData data;
            UnitData data2;
            int num;
            JobData data3;
            BaseStatus status;
            BaseStatus status2;
            BaseStatus status3;
            BaseStatus status4;
            BaseStatus status5;
            Animator animator;
            Animator animator2;
            data = MonoSingleton<GameManager>.Instance.Player;
            data2 = null;
            num = 0;
            if ((this.OwnerSlot != null) == null)
            {
                goto Label_004F;
            }
            if (this.mCurrentArtifact == null)
            {
                goto Label_004F;
            }
            data3 = null;
            if (data.FindOwner(this.mCurrentArtifact, &data2, &data3) == null)
            {
                goto Label_004F;
            }
            num = Array.IndexOf<JobData>(data2.Jobs, data3);
        Label_004F:
            if (this.mOwnerUnitData == null)
            {
                goto Label_0068;
            }
            data2 = this.mOwnerUnitData;
            num = this.mOwnerUnitJobIndex;
        Label_0068:
            if ((this.Status != null) == null)
            {
                goto Label_00F4;
            }
            if (this.mCurrentArtifact == null)
            {
                goto Label_00DD;
            }
            status = new BaseStatus();
            status2 = new BaseStatus();
            this.mCurrentArtifact.GetHomePassiveBuffStatus(&status, &status2, null, 0, 1);
            status3 = new BaseStatus();
            status4 = new BaseStatus();
            this.mCurrentArtifact.GetHomePassiveBuffStatus(&status3, &status4, data2, num, 1);
            this.Status.SetValues(status, status2, status3, status4, 0);
            goto Label_00F4;
        Label_00DD:
            status5 = new BaseStatus();
            this.Status.SetValues(status5, status5, 0);
        Label_00F4:
            if ((this.OwnerSlot != null) == null)
            {
                goto Label_014C;
            }
            this.OwnerSlot.SetSlotData<UnitData>(data2);
            if (data2 == null)
            {
                goto Label_013B;
            }
            if (num == -1)
            {
                goto Label_013B;
            }
            DataSource.Bind<JobData>(this.OwnerSlot.get_gameObject(), data2.Jobs[num]);
            goto Label_014C;
        Label_013B:
            DataSource.Bind<JobData>(this.OwnerSlot.get_gameObject(), null);
        Label_014C:
            this.RefreshKyokaList();
            this.RefreshDecomposeInfo();
            this.RefreshTransmuteInfo();
            this.RefreshRarityUpInfo();
            this.RefreshAbilities();
            if ((this.DetailMaterial != null) == null)
            {
                goto Label_019D;
            }
            if (this.mCurrentArtifact == null)
            {
                goto Label_019D;
            }
            this.DetailMaterial.SetMaterial(0, this.mCurrentArtifact.Kakera);
        Label_019D:
            if ((this.KyokaPanel != null) == null)
            {
                goto Label_0270;
            }
            if (this.mCurrentArtifact == null)
            {
                goto Label_0270;
            }
            animator = this.KyokaPanel.GetComponent<Animator>();
            if ((animator != null) == null)
            {
                goto Label_0270;
            }
            if (string.IsNullOrEmpty(this.KyokaPanelState) != null)
            {
                goto Label_0270;
            }
            if (this.mCurrentArtifact.Lv < this.mCurrentArtifact.LvCap)
            {
                goto Label_025D;
            }
            if (this.mCurrentArtifact.Rarity >= this.mCurrentArtifact.RarityCap)
            {
                goto Label_0245;
            }
            animator.SetInteger(this.KyokaPanelState, this.KyokaPanel_LvCapped);
            goto Label_0258;
        Label_0245:
            animator.SetInteger(this.KyokaPanelState, this.KyokaPanel_LvMax);
        Label_0258:
            goto Label_0270;
        Label_025D:
            animator.SetInteger(this.KyokaPanelState, this.KyokaPanel_Enable);
        Label_0270:
            if ((this.SellPrice != null) == null)
            {
                goto Label_0297;
            }
            this.SellPrice.set_text(&this.mTotalSellPrice.ToString());
        Label_0297:
            if ((this.RarityUpCost != null) == null)
            {
                goto Label_0328;
            }
            if (this.mCurrentArtifact == null)
            {
                goto Label_0328;
            }
            animator2 = this.RarityUpCost.GetComponent<Animator>();
            if ((animator2 != null) == null)
            {
                goto Label_0328;
            }
            if (string.IsNullOrEmpty(this.RarityUpCostState) != null)
            {
                goto Label_0328;
            }
            if (this.mCurrentArtifact.RarityParam.ArtifactRarityUpCost > data.Gold)
            {
                goto Label_0315;
            }
            animator2.SetInteger(this.RarityUpCostState, this.RarityUpCost_Normal);
            goto Label_0328;
        Label_0315:
            animator2.SetInteger(this.RarityUpCostState, this.RarityUpCost_NoGold);
        Label_0328:
            if ((this.ExpPanel != null) == null)
            {
                goto Label_0370;
            }
            if (this.mCurrentArtifact == null)
            {
                goto Label_0370;
            }
            this.ExpPanel.LevelCap = this.mCurrentArtifact.GetLevelCap();
            this.ExpPanel.Exp = this.mCurrentArtifact.Exp;
        Label_0370:
            if ((this.WindowBody != null) == null)
            {
                goto Label_0391;
            }
            GameParameter.UpdateAll(this.WindowBody.get_gameObject());
        Label_0391:
            return;
        }

        private void RequestUseAddExpItem(ItemData item, GameObject updataTarget)
        {
            List<ItemData> list;
            list = new List<ItemData>();
            list.Add(item);
            this.RequestUseAddExpItem(list, updataTarget);
            return;
        }

        private unsafe void RequestUseAddExpItem(IEnumerable<ItemData> itemList, GameObject updataTarget)
        {
            string str;
            RequestAddExp exp;
            UnitData data;
            int num;
            JobData data2;
            ItemData data3;
            IEnumerator<ItemData> enumerator;
            if (this.IsBusy == null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            if (this.mCurrentArtifact != null)
            {
                goto Label_0018;
            }
            return;
        Label_0018:
            if (this.mCurrentArtifact.Lv < this.mCurrentArtifact.GetLevelCap())
            {
                goto Label_00A1;
            }
            if ((this.ExpPanel != null) == null)
            {
                goto Label_00A0;
            }
            if (this.ExpPanel.IsBusy != null)
            {
                goto Label_00A0;
            }
            if (this.mCurrentArtifact.Rarity < this.mCurrentArtifact.RarityCap)
            {
                goto Label_0089;
            }
            str = "sys.ARTI_EXPFULL2";
            goto Label_008F;
        Label_0089:
            str = "sys.ARTI_EXPFULL1";
        Label_008F:
            UIUtility.NegativeSystemMessage(null, LocalizedText.Get(str), null, null, 0, -1);
        Label_00A0:
            return;
        Label_00A1:
            if (this.ExpPanel.IsBusy != null)
            {
                goto Label_00BC;
            }
            FlowNode_GameObject.ActivateOutputLinks(this, 0x97);
        Label_00BC:
            if (string.IsNullOrEmpty(this.KyokaSE) != null)
            {
                goto Label_00E1;
            }
            MonoSingleton<MySound>.Instance.PlaySEOneShot(this.KyokaSE, 0f);
        Label_00E1:
            exp = null;
            if (this.mRequests.Count <= 0)
            {
                goto Label_0134;
            }
            if ((this.mRequests[this.mRequests.Count - 1] as RequestAddExp) == null)
            {
                goto Label_0134;
            }
            exp = this.mRequests[this.mRequests.Count - 1] as RequestAddExp;
        Label_0134:
            if (exp != null)
            {
                goto Label_016F;
            }
            exp = new RequestAddExp();
            exp.UniqueID = this.mCurrentArtifact.UniqueID;
            exp.Callback = new Network.ResponseCallback(this.AddExpResult);
            this.AddAsyncRequest(exp);
        Label_016F:
            if (this.mStatusCache != null)
            {
                goto Label_022A;
            }
            this.mStatusCache = new StatusCache();
            data = null;
            num = -1;
            if ((this.OwnerSlot != null) == null)
            {
                goto Label_01C9;
            }
            data2 = null;
            if (MonoSingleton<GameManager>.Instance.Player.FindOwner(this.mCurrentArtifact, &data, &data2) == null)
            {
                goto Label_01C9;
            }
            num = Array.IndexOf<JobData>(data.Jobs, data2);
        Label_01C9:
            if (this.mOwnerUnitData == null)
            {
                goto Label_01E2;
            }
            data = this.mOwnerUnitData;
            num = this.mOwnerUnitJobIndex;
        Label_01E2:
            this.mCurrentArtifact.GetHomePassiveBuffStatus(&this.mStatusCache.BaseAdd, &this.mStatusCache.BaseMul, null, 0, 1);
            this.mCurrentArtifact.GetHomePassiveBuffStatus(&this.mStatusCache.UnitAdd, &this.mStatusCache.UnitMul, data, num, 1);
        Label_022A:
            enumerator = itemList.GetEnumerator();
        Label_0232:
            try
            {
                goto Label_027F;
            Label_0237:
                data3 = enumerator.Current;
                exp.Items.Add(data3.Param);
                this.mCurrentArtifact.GainExp(data3.Param.value);
                data3.Used(1);
                this.mUseEnhanceItemNum += 1;
            Label_027F:
                if (enumerator.MoveNext() != null)
                {
                    goto Label_0237;
                }
                goto Label_029D;
            }
            finally
            {
            Label_0290:
                if (enumerator != null)
                {
                    goto Label_0295;
                }
            Label_0295:
                enumerator.Dispose();
            }
        Label_029D:
            if ((updataTarget != null) == null)
            {
                goto Label_02AF;
            }
            GameParameter.UpdateAll(updataTarget);
        Label_02AF:
            if ((this.ExpPanel != null) == null)
            {
                goto Label_02DB;
            }
            this.ExpPanel.AnimateTo(this.mCurrentArtifact.Exp);
            goto Label_02E1;
        Label_02DB:
            this.OnKyokaEnd();
        Label_02E1:
            return;
        }

        public void SellArtifacts()
        {
            ArtifactData[] dataArray;
            string str;
            Win_Btn_DecideCancel_FL_C l_fl_c;
            if (this.IsBusy == null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            if ((this.ArtifactList == null) == null)
            {
                goto Label_0028;
            }
            DebugUtility.LogWarning("ArtifactList is not set");
            return;
        Label_0028:
            if (((int) this.GetSelectedArtifacts().Length) > 0)
            {
                goto Label_004E;
            }
            UIUtility.NegativeSystemMessage(null, LocalizedText.Get("sys.ARTI_NOTHING2SELL"), null, null, 0, -1);
            return;
        Label_004E:
            str = LocalizedText.Get("sys.ARTI_SELL_CONFIRM");
            this.mConfirmDialogGo = UIUtility.ConfirmBox(str, new UIUtility.DialogResultEvent(this.OnSellRarityCheck), new UIUtility.DialogResultEvent(this.OnSellCancel), null, 0, -1, null, null);
            if ((this.mConfirmDialogGo != null) == null)
            {
                goto Label_00B2;
            }
            l_fl_c = this.mConfirmDialogGo.GetComponent<Win_Btn_DecideCancel_FL_C>();
            if ((l_fl_c != null) == null)
            {
                goto Label_00B2;
            }
            l_fl_c.AutoClose = 0;
        Label_00B2:
            return;
        }

        public void SetArtifactData()
        {
            if (this.mCurrentArtifact == null)
            {
                goto Label_0020;
            }
            GlobalVars.ConditionJobs = this.mCurrentArtifact.ArtifactParam.condition_jobs;
        Label_0020:
            return;
        }

        public void SetOwnerUnit(UnitData unit, int jobIndex)
        {
            this.mOwnerUnitData = unit;
            this.mOwnerUnitJobIndex = jobIndex;
            return;
        }

        private void ShowDecomposeResult()
        {
            base.StartCoroutine(this.ShowDecomposeResultAsync());
            return;
        }

        [DebuggerHidden]
        private IEnumerator ShowDecomposeResultAsync()
        {
            <ShowDecomposeResultAsync>c__IteratorE6 re;
            re = new <ShowDecomposeResultAsync>c__IteratorE6();
            re.<>f__this = this;
            return re;
        }

        public void ShowDetail()
        {
            if ((this.mDetailWindow != null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            if (this.mCurrentArtifact != null)
            {
                goto Label_001E;
            }
            return;
        Label_001E:
            if ((this.DetailWindow == null) == null)
            {
                goto Label_0030;
            }
            return;
        Label_0030:
            base.StartCoroutine(this.ShowDetailAsync());
            return;
        }

        [DebuggerHidden]
        private IEnumerator ShowDetailAsync()
        {
            <ShowDetailAsync>c__IteratorE7 re;
            re = new <ShowDetailAsync>c__IteratorE7();
            re.<>f__this = this;
            return re;
        }

        public unsafe void ShowSelection()
        {
            int num;
            Transform transform;
            int num2;
            ArtifactIcon icon;
            int num3;
            if ((this.SelectionListItem == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            num = 0;
            goto Label_0033;
        Label_0019:
            Object.Destroy(this.mSelectionItems[num].get_gameObject());
            num += 1;
        Label_0033:
            if (num < this.mSelectionItems.Count)
            {
                goto Label_0019;
            }
            this.mSelectionItems.Clear();
            if (this.mSelectedArtifacts == null)
            {
                goto Label_0126;
            }
            if ((this.SelectionNum != null) == null)
            {
                goto Label_0087;
            }
            num3 = (int) this.mSelectedArtifacts.Length;
            this.SelectionNum.set_text(&num3.ToString());
        Label_0087:
            transform = null;
            if ((this.SelectionList == null) == null)
            {
                goto Label_00B0;
            }
            transform = this.SelectionListItem.get_transform().get_parent();
            goto Label_00BC;
        Label_00B0:
            transform = this.SelectionList.get_transform();
        Label_00BC:
            num2 = 0;
            goto Label_0118;
        Label_00C3:
            icon = Object.Instantiate<ArtifactIcon>(this.SelectionListItem);
            this.mSelectionItems.Add(icon);
            DataSource.Bind(icon.get_gameObject(), this.mSelectedArtifacts[num2].GetType(), this.mSelectedArtifacts[num2]);
            icon.get_transform().SetParent(transform, 0);
            icon.get_gameObject().SetActive(1);
            num2 += 1;
        Label_0118:
            if (num2 < ((int) this.mSelectedArtifacts.Length))
            {
                goto Label_00C3;
            }
        Label_0126:
            return;
        }

        private void ShowSellResult()
        {
            GameObject obj2;
            ChangeList list;
            FlowNode_GameObject.ActivateOutputLinks(this, 0x25a);
            GlobalEvent.Invoke(((PredefinedGlobalEvents) 1).ToString(), this);
            if ((this.SellResult != null) == null)
            {
                goto Label_005D;
            }
            list = Object.Instantiate<GameObject>(this.SellResult).GetComponentInChildren<ChangeList>();
            if ((list != null) == null)
            {
                goto Label_005D;
            }
            list.SetData(this.mChangeSet.ToArray());
        Label_005D:
            this.mChangeSet = null;
            if ((this.ArtifactList != null) == null)
            {
                goto Label_008B;
            }
            this.ArtifactList.ClearSelection();
            this.ArtifactList.Refresh();
        Label_008B:
            return;
        }

        private void ShowTransmuteResult()
        {
            GameManager manager;
            List<ArtifactData> list;
            List<ChangeListData> list2;
            int num;
            bool flag;
            int num2;
            ChangeListData data;
            list = MonoSingleton<GameManager>.Instance.Player.Artifacts;
            list2 = new List<ChangeListData>(4);
            num = 0;
            goto Label_00B7;
        Label_0020:
            flag = 0;
            num2 = 0;
            goto Label_0062;
        Label_002B:
            if (list[num].UniqueID != this.mCachedArtifacts[num2].UniqueID)
            {
                goto Label_005C;
            }
            flag = 1;
            goto Label_0071;
        Label_005C:
            num2 += 1;
        Label_0062:
            if (num2 < ((int) this.mCachedArtifacts.Length))
            {
                goto Label_002B;
            }
        Label_0071:
            if (flag != null)
            {
                goto Label_00B3;
            }
            data = new ChangeListData();
            data.ItemID = this.ArtifactDataID;
            data.MetaData = list[num];
            data.MetaDataType = typeof(ArtifactData);
            list2.Add(data);
        Label_00B3:
            num += 1;
        Label_00B7:
            if (num < list.Count)
            {
                goto Label_0020;
            }
            this.RefreshArtifactList();
            base.StartCoroutine(this.ShowTransmuteResultAsync(list2.ToArray()));
            return;
        }

        [DebuggerHidden]
        private IEnumerator ShowTransmuteResultAsync(ChangeListData[] changeset)
        {
            <ShowTransmuteResultAsync>c__IteratorE5 re;
            re = new <ShowTransmuteResultAsync>c__IteratorE5();
            re.changeset = changeset;
            re.<$>changeset = changeset;
            re.<>f__this = this;
            return re;
        }

        private void Start()
        {
            GameManager manager;
            manager = MonoSingleton<GameManager>.GetInstanceDirect();
            if ((manager != null) == null)
            {
                goto Label_0034;
            }
            manager.OnSceneChange = (GameManager.SceneChangeEvent) Delegate.Combine(manager.OnSceneChange, new GameManager.SceneChangeEvent(this.OnSceneCHange));
        Label_0034:
            if ((this.ExpPanel != null) == null)
            {
                goto Label_0096;
            }
            this.ExpPanel.SetDelegate(new SRPG.ExpPanel.CalcEvent(ArtifactData.StaticCalcExpFromLevel), new SRPG.ExpPanel.CalcEvent(ArtifactData.StaticCalcLevelFromExp));
            this.ExpPanel.OnLevelChange = new SRPG.ExpPanel.LevelChangeEvent(this.OnLevelChange);
            this.ExpPanel.OnFinish = new SRPG.ExpPanel.ExpPanelEvent(this.OnKyokaEnd);
        Label_0096:
            if ((this.ExpMaxLvupBtn != null) == null)
            {
                goto Label_00BE;
            }
            this.ExpMaxLvupBtn.AddListener(new SRPG_Button.ButtonClickEvent(this.OnExpMaxOpen));
        Label_00BE:
            if (string.IsNullOrEmpty(this.ArtifactListID) != null)
            {
                goto Label_00DF;
            }
            this.ArtifactList = GameObjectID.FindGameObject<SRPG.ArtifactList>(this.ArtifactListID);
        Label_00DF:
            if ((this.ArtifactList != null) == null)
            {
                goto Label_0117;
            }
            this.ArtifactList.OnSelectionChange = (SRPG.ArtifactList.SelectionChangeEvent) Delegate.Combine(this.ArtifactList.OnSelectionChange, new SRPG.ArtifactList.SelectionChangeEvent(this.OnArtifactSelect));
        Label_0117:
            if ((this.KyokaListItem != null) == null)
            {
                goto Label_0144;
            }
            if (this.KyokaListItem.get_activeInHierarchy() == null)
            {
                goto Label_0144;
            }
            this.KyokaListItem.SetActive(0);
        Label_0144:
            if ((this.SelectionListItem != null) == null)
            {
                goto Label_017B;
            }
            if (this.SelectionListItem.get_gameObject().get_activeSelf() == null)
            {
                goto Label_017B;
            }
            this.SelectionListItem.get_gameObject().SetActive(0);
        Label_017B:
            if ((this.ArtifactSlot != null) == null)
            {
                goto Label_0198;
            }
            this.ArtifactSlot.SetSlotData<ArtifactData>(null);
        Label_0198:
            if ((this.ToggleExcludeEquiped != null) == null)
            {
                goto Label_01BA;
            }
            this.ToggleExcludeEquiped.set_isOn(this.ReadExcludeEquipedSettingValue());
        Label_01BA:
            this.mCurrentArtifact = DataSource.FindDataOfClass<ArtifactData>(base.get_gameObject(), null);
            if (this.mCurrentArtifact == null)
            {
                goto Label_01E8;
            }
            this.mCurrentArtifactParam = this.mCurrentArtifact.ArtifactParam;
        Label_01E8:
            this.Rebind();
            if (this.RefreshOnStart == null)
            {
                goto Label_01FF;
            }
            this.RefreshWindow();
        Label_01FF:
            return;
        }

        private void SyncArtifactData()
        {
            ArtifactData data;
            if (this.mCurrentArtifact == null)
            {
                goto Label_0020;
            }
            if (this.mCurrentArtifact.UniqueID != null)
            {
                goto Label_0021;
            }
        Label_0020:
            return;
        Label_0021:
            data = MonoSingleton<GameManager>.Instance.Player.FindArtifactByUniqueID(this.mCurrentArtifact.UniqueID);
            if (data == null)
            {
                goto Label_006A;
            }
            this.mCurrentArtifact = data.Copy();
            this.mCurrentArtifactParam = this.mCurrentArtifact.ArtifactParam;
            this.Rebind();
        Label_006A:
            return;
        }

        public void TransmuteArtifact()
        {
            object[] objArray4;
            object[] objArray3;
            object[] objArray2;
            object[] objArray1;
            PlayerData data;
            int num;
            List<ArtifactData> list;
            int num2;
            string str;
            string str2;
            ItemData data2;
            int num3;
            RarityParam param;
            string str3;
            string str4;
            string str5;
            if (this.IsBusy == null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            if (this.mCurrentArtifactParam != null)
            {
                goto Label_0018;
            }
            return;
        Label_0018:
            data = MonoSingleton<GameManager>.Instance.Player;
            num = 0;
            list = data.Artifacts;
            num2 = 0;
            goto Label_0052;
        Label_0033:
            if (list[num2].ArtifactParam != this.mCurrentArtifactParam)
            {
                goto Label_004E;
            }
            num += 1;
        Label_004E:
            num2 += 1;
        Label_0052:
            if (num2 < list.Count)
            {
                goto Label_0033;
            }
            if (num < this.mCurrentArtifactParam.maxnum)
            {
                goto Label_00B0;
            }
            objArray1 = new object[] { this.mCurrentArtifactParam.name, (int) this.mCurrentArtifactParam.maxnum };
            str = LocalizedText.Get("sys.ARTI_TRANSMUTE_MAXNUM", objArray1);
            UIUtility.NegativeSystemMessage(null, str, null, null, 0, -1);
            return;
        Label_00B0:
            str2 = this.mCurrentArtifactParam.kakera;
            data2 = data.FindItemDataByItemID(str2);
            num3 = (data2 == null) ? 0 : data2.Num;
            param = MonoSingleton<GameManager>.Instance.GetRarityParam(this.mCurrentArtifactParam.rareini);
            if (num3 >= param.ArtifactCreatePieceNum)
            {
                goto Label_0165;
            }
            objArray2 = new object[] { this.mCurrentArtifactParam.name, (OInt) param.ArtifactCreatePieceNum, (int) (param.ArtifactCreatePieceNum - num3), (int) num3 };
            str3 = LocalizedText.Get("sys.ARTI_TRANSMUTE_NOKAKERA", objArray2);
            UIUtility.NegativeSystemMessage(null, str3, null, null, 0, -1);
            return;
        Label_0165:
            if (data.Gold >= param.ArtifactCreateCost)
            {
                goto Label_01E2;
            }
            objArray3 = new object[] { this.mCurrentArtifactParam.name, (OInt) param.ArtifactCreateCost, (int) (param.ArtifactCreateCost - data.Gold), (int) data.Gold };
            str4 = LocalizedText.Get("sys.ARTI_TRANSMUTE_NOGOLD", objArray3);
            UIUtility.NegativeSystemMessage(null, str4, null, null, 0, -1);
            return;
        Label_01E2:
            objArray4 = new object[] { this.mCurrentArtifactParam.name };
            UIUtility.ConfirmBox(LocalizedText.Get("sys.ARTI_TRANSMUTE_CONFIRM", objArray4), new UIUtility.DialogResultEvent(this.OnTransmuteAccept), new UIUtility.DialogResultEvent(this.OnTransmuteCancel), null, 0, -1, null, null);
            return;
        }

        public void UnequipArtifact()
        {
            bool flag;
            int num;
            if (this.IsBusy == null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            flag = 0;
            if (this.mOwnerUnitData == null)
            {
                goto Label_0068;
            }
            num = 0;
            goto Label_0049;
        Label_0020:
            if (this.mOwnerUnitData.Jobs[this.mOwnerUnitJobIndex].Artifacts[num] == null)
            {
                goto Label_0045;
            }
            flag = 1;
            goto Label_0068;
        Label_0045:
            num += 1;
        Label_0049:
            if (num < ((int) this.mOwnerUnitData.Jobs[this.mOwnerUnitJobIndex].Artifacts.Length))
            {
                goto Label_0020;
            }
        Label_0068:
            if (flag == null)
            {
                goto Label_008B;
            }
            if (this.OnEquip == null)
            {
                goto Label_008B;
            }
            this.OnEquip(null, this.SelectArtifactSlot);
        Label_008B:
            this.EndEquip();
            return;
        }

        private void Update()
        {
            if (this.mSendingRequests == null)
            {
                goto Label_005C;
            }
            if (Network.IsConnecting == null)
            {
                goto Label_0016;
            }
            return;
        Label_0016:
            if (this.mRequests.Count <= 0)
            {
                goto Label_005C;
            }
            if (Network.Mode != null)
            {
                goto Label_0054;
            }
            Network.RequestAPI(this.mRequests[0].Compose(), 0);
            this.mRequests.RemoveAt(0);
        Label_0054:
            this.mSendingRequests = 1;
            return;
        Label_005C:
            if (this.mFinalizing == null)
            {
                goto Label_008A;
            }
            if (this.IsBusy == null)
            {
                goto Label_0073;
            }
            return;
        Label_0073:
            FlowNode_GameObject.ActivateOutputLinks(this, 0x66);
            this.mFinalizing = 0;
            this.mSendingRequests = 0;
            return;
        Label_008A:
            if (this.mDisableFlushRequest != null)
            {
                goto Label_00CE;
            }
            if (this.mFlushTimer <= 0f)
            {
                goto Label_00CE;
            }
            this.mFlushTimer -= Time.get_unscaledDeltaTime();
            if (this.mFlushTimer > 0f)
            {
                goto Label_00CE;
            }
            this.FlushRequests(0);
        Label_00CE:
            return;
        }

        private unsafe void UpdateArtifactOwner()
        {
            UnitData data;
            JobData data2;
            this.mCurrentArtifactOwner = null;
            this.mCurrentArtifactOwnerJob = null;
            if ((this.ArtifactOwnerSlot == null) == null)
            {
                goto Label_0020;
            }
            return;
        Label_0020:
            if (this.mCurrentArtifact != null)
            {
                goto Label_0038;
            }
            this.ArtifactOwnerSlot.SetSlotData<UnitData>(null);
            return;
        Label_0038:
            if (MonoSingleton<GameManager>.GetInstanceDirect().Player.FindOwner(this.mCurrentArtifact, &data, &data2) == null)
            {
                goto Label_0064;
            }
            this.mCurrentArtifactOwner = data;
            this.mCurrentArtifactOwnerJob = data2;
        Label_0064:
            this.ArtifactOwnerSlot.SetSlotData<UnitData>(this.mCurrentArtifactOwner);
            return;
        }

        private void WriteExcludeEquipedSettingValue(bool isOn)
        {
            int num;
            num = (isOn != null) ? 1 : 0;
            PlayerPrefsUtility.SetInt(PlayerPrefsUtility.ARTIFACT_EXCLUDE_EQUIPED, num, 0);
            return;
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
                return;
            }
        }

        private bool IsBusy
        {
            get
            {
                return ((this.mSceneChanging != null) ? 1 : this.mBusy);
            }
        }

        [CompilerGenerated]
        private sealed class <AddRareAsync>c__IteratorE4 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal GameObject <go>__0;
            internal ChangeList <cl>__1;
            internal int $PC;
            internal object $current;
            internal ArtifactWindow <>f__this;

            public <AddRareAsync>c__IteratorE4()
            {
                base..ctor();
                return;
            }

            [DebuggerHidden]
            public void Dispose()
            {
                this.$PC = -1;
                return;
            }

            public bool MoveNext()
            {
                uint num;
                bool flag;
                num = this.$PC;
                this.$PC = -1;
                switch (num)
                {
                    case 0:
                        goto Label_0025;

                    case 1:
                        goto Label_00CF;

                    case 2:
                        goto Label_0124;
                }
                goto Label_012B;
            Label_0025:
                this.<go>__0 = null;
                if ((this.<>f__this.RarityUpResult != null) == null)
                {
                    goto Label_00CF;
                }
                this.<go>__0 = Object.Instantiate<GameObject>(this.<>f__this.RarityUpResult);
                DataSource.Bind<ArtifactData>(this.<go>__0, this.<>f__this.mCurrentArtifact);
                this.<cl>__1 = this.<go>__0.GetComponentInChildren<ChangeList>();
                if ((this.<cl>__1 != null) == null)
                {
                    goto Label_00CF;
                }
                this.<cl>__1.SetData(this.<>f__this.mChangeSet.ToArray());
                this.<>f__this.mChangeSet = null;
                goto Label_00CF;
            Label_00BC:
                this.$current = null;
                this.$PC = 1;
                goto Label_012D;
            Label_00CF:
                if ((this.<go>__0 != null) != null)
                {
                    goto Label_00BC;
                }
                if (Network.Mode != 1)
                {
                    goto Label_0101;
                }
                this.<>f__this.RefreshWindow();
                this.<>f__this.RefreshArtifactList();
            Label_0101:
                FlowNode_GameObject.ActivateOutputLinks(this.<>f__this, 0xca);
                this.$current = null;
                this.$PC = 2;
                goto Label_012D;
            Label_0124:
                this.$PC = -1;
            Label_012B:
                return 0;
            Label_012D:
                return 1;
                return flag;
            }

            [DebuggerHidden]
            public void Reset()
            {
                throw new NotSupportedException();
            }

            object IEnumerator<object>.Current
            {
                [DebuggerHidden]
                get
                {
                    return this.$current;
                }
            }

            object IEnumerator.Current
            {
                [DebuggerHidden]
                get
                {
                    return this.$current;
                }
            }
        }

        [CompilerGenerated]
        private sealed class <OnArtifactBulkLevelUp>c__AnonStorey2FD
        {
            internal string iname;

            public <OnArtifactBulkLevelUp>c__AnonStorey2FD()
            {
                base..ctor();
                return;
            }

            internal bool <>m__290(ItemData tmp)
            {
                return (tmp.Param.iname == this.iname);
            }
        }

        [CompilerGenerated]
        private sealed class <OnSceneChangeAsync>c__IteratorE8 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal int $PC;
            internal object $current;
            internal ArtifactWindow <>f__this;

            public <OnSceneChangeAsync>c__IteratorE8()
            {
                base..ctor();
                return;
            }

            [DebuggerHidden]
            public void Dispose()
            {
                this.$PC = -1;
                return;
            }

            public bool MoveNext()
            {
                uint num;
                bool flag;
                num = this.$PC;
                this.$PC = -1;
                switch (num)
                {
                    case 0:
                        goto Label_0025;

                    case 1:
                        goto Label_003D;

                    case 2:
                        goto Label_0070;
                }
                goto Label_0077;
            Label_0025:
                goto Label_003D;
            Label_002A:
                this.$current = null;
                this.$PC = 1;
                goto Label_0079;
            Label_003D:
                if (this.<>f__this.mRequests.Count > 0)
                {
                    goto Label_002A;
                }
                if (Network.IsConnecting != null)
                {
                    goto Label_002A;
                }
                this.$current = null;
                this.$PC = 2;
                goto Label_0079;
            Label_0070:
                this.$PC = -1;
            Label_0077:
                return 0;
            Label_0079:
                return 1;
                return flag;
            }

            [DebuggerHidden]
            public void Reset()
            {
                throw new NotSupportedException();
            }

            object IEnumerator<object>.Current
            {
                [DebuggerHidden]
                get
                {
                    return this.$current;
                }
            }

            object IEnumerator.Current
            {
                [DebuggerHidden]
                get
                {
                    return this.$current;
                }
            }
        }

        [CompilerGenerated]
        private sealed class <RefreshAbilities>c__AnonStorey2FE
        {
            internal string iname;

            public <RefreshAbilities>c__AnonStorey2FE()
            {
                base..ctor();
                return;
            }

            internal bool <>m__291(AbilityDeriveParam derive_param)
            {
                return (derive_param.BaseAbilityIname == this.iname);
            }
        }

        [CompilerGenerated]
        private sealed class <RefreshRarityUpInfo>c__AnonStorey2FF
        {
            internal bool hasKakera;

            public <RefreshRarityUpInfo>c__AnonStorey2FF()
            {
                base..ctor();
                return;
            }

            internal void <>m__292(ItemData kakera)
            {
                if (kakera.Num <= 0)
                {
                    goto Label_0013;
                }
                this.hasKakera = 1;
            Label_0013:
                return;
            }
        }

        [CompilerGenerated]
        private sealed class <ShowDecomposeResultAsync>c__IteratorE6 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal GameObject <go>__0;
            internal ChangeList <cl>__1;
            internal int $PC;
            internal object $current;
            internal ArtifactWindow <>f__this;

            public <ShowDecomposeResultAsync>c__IteratorE6()
            {
                base..ctor();
                return;
            }

            [DebuggerHidden]
            public void Dispose()
            {
                this.$PC = -1;
                return;
            }

            public bool MoveNext()
            {
                uint num;
                bool flag;
                num = this.$PC;
                this.$PC = -1;
                switch (num)
                {
                    case 0:
                        goto Label_0021;

                    case 1:
                        goto Label_00A2;
                }
                goto Label_00E1;
            Label_0021:
                if ((this.<>f__this.DecomposeResult != null) == null)
                {
                    goto Label_00B3;
                }
                this.<go>__0 = Object.Instantiate<GameObject>(this.<>f__this.DecomposeResult);
                this.<cl>__1 = this.<go>__0.GetComponentInChildren<ChangeList>();
                if ((this.<cl>__1 != null) == null)
                {
                    goto Label_00A2;
                }
                this.<cl>__1.SetData(this.<>f__this.mChangeSet.ToArray());
                goto Label_00A2;
            Label_008F:
                this.$current = null;
                this.$PC = 1;
                goto Label_00E3;
            Label_00A2:
                if ((this.<go>__0 != null) != null)
                {
                    goto Label_008F;
                }
            Label_00B3:
                this.<>f__this.mChangeSet = null;
                this.<>f__this.RefreshArtifactList();
                FlowNode_GameObject.ActivateOutputLinks(this.<>f__this, 0x191);
                this.$PC = -1;
            Label_00E1:
                return 0;
            Label_00E3:
                return 1;
                return flag;
            }

            [DebuggerHidden]
            public void Reset()
            {
                throw new NotSupportedException();
            }

            object IEnumerator<object>.Current
            {
                [DebuggerHidden]
                get
                {
                    return this.$current;
                }
            }

            object IEnumerator.Current
            {
                [DebuggerHidden]
                get
                {
                    return this.$current;
                }
            }
        }

        [CompilerGenerated]
        private sealed class <ShowDetailAsync>c__IteratorE7 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal int $PC;
            internal object $current;
            internal ArtifactWindow <>f__this;

            public <ShowDetailAsync>c__IteratorE7()
            {
                base..ctor();
                return;
            }

            [DebuggerHidden]
            public void Dispose()
            {
                this.$PC = -1;
                return;
            }

            public bool MoveNext()
            {
                uint num;
                bool flag;
                num = this.$PC;
                this.$PC = -1;
                switch (num)
                {
                    case 0:
                        goto Label_0025;

                    case 1:
                        goto Label_0130;

                    case 2:
                        goto Label_0165;
                }
                goto Label_016C;
            Label_0025:
                if ((this.<>f__this.DetailWindow != null) == null)
                {
                    goto Label_0152;
                }
                if ((this.<>f__this.mDetailWindow == null) == null)
                {
                    goto Label_0152;
                }
                this.<>f__this.mDetailWindow = Object.Instantiate<GameObject>(this.<>f__this.DetailWindow);
                DataSource.Bind<ArtifactData>(this.<>f__this.mDetailWindow, this.<>f__this.mCurrentArtifact);
                if (this.<>f__this.mOwnerUnitData == null)
                {
                    goto Label_00B2;
                }
                DataSource.Bind<UnitData>(this.<>f__this.mDetailWindow, this.<>f__this.mOwnerUnitData);
            Label_00B2:
                if (this.<>f__this.mOwnerUnitData == null)
                {
                    goto Label_00ED;
                }
                DataSource.Bind<JobData>(this.<>f__this.mDetailWindow, this.<>f__this.mOwnerUnitData.GetJobData(this.<>f__this.mOwnerUnitJobIndex));
            Label_00ED:
                if (this.<>f__this.mSelectArtifactSlot == null)
                {
                    goto Label_0130;
                }
                DataSource.Bind<ArtifactTypes>(this.<>f__this.mDetailWindow, this.<>f__this.SelectArtifactSlot);
                goto Label_0130;
            Label_011D:
                this.$current = null;
                this.$PC = 1;
                goto Label_016E;
            Label_0130:
                if ((this.<>f__this.mDetailWindow != null) != null)
                {
                    goto Label_011D;
                }
                this.<>f__this.mDetailWindow = null;
            Label_0152:
                this.$current = null;
                this.$PC = 2;
                goto Label_016E;
            Label_0165:
                this.$PC = -1;
            Label_016C:
                return 0;
            Label_016E:
                return 1;
                return flag;
            }

            [DebuggerHidden]
            public void Reset()
            {
                throw new NotSupportedException();
            }

            object IEnumerator<object>.Current
            {
                [DebuggerHidden]
                get
                {
                    return this.$current;
                }
            }

            object IEnumerator.Current
            {
                [DebuggerHidden]
                get
                {
                    return this.$current;
                }
            }
        }

        [CompilerGenerated]
        private sealed class <ShowTransmuteResultAsync>c__IteratorE5 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal ChangeListData[] changeset;
            internal ChangeList <changeList>__0;
            internal int $PC;
            internal object $current;
            internal ChangeListData[] <$>changeset;
            internal ArtifactWindow <>f__this;

            public <ShowTransmuteResultAsync>c__IteratorE5()
            {
                base..ctor();
                return;
            }

            [DebuggerHidden]
            public void Dispose()
            {
                this.$PC = -1;
                return;
            }

            public bool MoveNext()
            {
                uint num;
                bool flag;
                num = this.$PC;
                this.$PC = -1;
                switch (num)
                {
                    case 0:
                        goto Label_0021;

                    case 1:
                        goto Label_00D1;
                }
                goto Label_010F;
            Label_0021:
                CriticalSection.Enter(8);
                if ((this.<>f__this.TransmuteResult != null) == null)
                {
                    goto Label_00E7;
                }
                if (((int) this.changeset.Length) <= 0)
                {
                    goto Label_00E7;
                }
                this.<>f__this.mResultWindow = Object.Instantiate<GameObject>(this.<>f__this.TransmuteResult);
                this.<changeList>__0 = this.<>f__this.mResultWindow.GetComponentInChildren<ChangeList>();
                if ((this.<changeList>__0 != null) == null)
                {
                    goto Label_009E;
                }
                this.<changeList>__0.SetData(this.changeset);
            Label_009E:
                DataSource.Bind<ArtifactData>(this.<>f__this.mResultWindow, this.<>f__this.mCurrentArtifact);
                goto Label_00D1;
            Label_00BE:
                this.$current = null;
                this.$PC = 1;
                goto Label_0111;
            Label_00D1:
                if ((this.<>f__this.mResultWindow != null) != null)
                {
                    goto Label_00BE;
                }
            Label_00E7:
                FlowNode_GameObject.ActivateOutputLinks(this.<>f__this, 0x12e);
                CriticalSection.Leave(8);
                this.<>f__this.RefreshWindow();
                this.$PC = -1;
            Label_010F:
                return 0;
            Label_0111:
                return 1;
                return flag;
            }

            [DebuggerHidden]
            public void Reset()
            {
                throw new NotSupportedException();
            }

            object IEnumerator<object>.Current
            {
                [DebuggerHidden]
                get
                {
                    return this.$current;
                }
            }

            object IEnumerator.Current
            {
                [DebuggerHidden]
                get
                {
                    return this.$current;
                }
            }
        }

        public delegate void EquipEvent(ArtifactData artifact, ArtifactTypes type);

        private abstract class Request
        {
            public Network.ResponseCallback Callback;

            protected Request()
            {
                base..ctor();
                return;
            }

            public abstract WebAPI Compose();
        }

        private class RequestAddExp : ArtifactWindow.Request
        {
            public long UniqueID;
            public List<ItemParam> Items;

            public RequestAddExp()
            {
                this.Items = new List<ItemParam>(0x20);
                base..ctor();
                return;
            }

            public override WebAPI Compose()
            {
                Dictionary<string, int> dictionary;
                int num;
                Dictionary<string, int> dictionary2;
                string str;
                int num2;
                dictionary = new Dictionary<string, int>();
                num = 0;
                goto Label_0072;
            Label_000D:
                if (dictionary.ContainsKey(this.Items[num].iname) == null)
                {
                    goto Label_0056;
                }
                num2 = dictionary2[str];
                (dictionary2 = dictionary)[str = this.Items[num].iname] = num2 + 1;
                goto Label_006E;
            Label_0056:
                dictionary[this.Items[num].iname] = 1;
            Label_006E:
                num += 1;
            Label_0072:
                if (num < this.Items.Count)
                {
                    goto Label_000D;
                }
                return new ReqArtifactEnforce(this.UniqueID, dictionary, base.Callback);
            }
        }

        private class RequestAddRare : ArtifactWindow.Request
        {
            public long UniqueID;
            public string trophyprog;
            public string bingoprog;

            public RequestAddRare()
            {
                base..ctor();
                return;
            }

            public override WebAPI Compose()
            {
                return new ReqArtifactAddRare(this.UniqueID, base.Callback, this.trophyprog, this.bingoprog);
            }
        }

        private class StatusCache
        {
            public BaseStatus BaseAdd;
            public BaseStatus BaseMul;
            public BaseStatus UnitAdd;
            public BaseStatus UnitMul;

            public StatusCache()
            {
                this.BaseAdd = new BaseStatus();
                this.BaseMul = new BaseStatus();
                this.UnitAdd = new BaseStatus();
                this.UnitMul = new BaseStatus();
                base..ctor();
                return;
            }
        }
    }
}

