namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.UI;

    [Pin(1, "Refresh", 0, 1), Pin(100, "クエスト選択", 1, 100)]
    public class UnitKakeraWindow : MonoBehaviour, IFlowInterface
    {
        public KakuseiWindowEvent OnKakuseiAccept;
        public AwakeEvent OnAwakeAccept;
        public GameObject QuestList;
        public RectTransform QuestListParent;
        public GameObject QuestListItemTemplate;
        public GameObject Kakera_Unit;
        public GameObject Kakera_Elem;
        public GameObject Kakera_Common;
        public Text Kakera_Consume_Unit;
        public Text Kakera_Consume_Elem;
        public Text Kakera_Consume_Common;
        public Text Kakera_Amount_Unit;
        public Text Kakera_Amount_Elem;
        public Text Kakera_Amount_Common;
        public Text Kakera_Consume_Message;
        public Text Kakera_Caution_Message;
        public GameObject NotFoundGainQuestObject;
        public GameObject CautionObject;
        public Button DecideButton;
        public GameObject JobUnlock;
        public Slider SelectAwakeSlider;
        public GameObject UnlockArtifactSlot;
        public Button PlusBtn;
        public Button MinusBtn;
        public Text AwakeResultLv;
        public Text AwakeResultComb;
        public Text AwakeResultArtifactSlots;
        public RectTransform JobUnlockParent;
        public GameObject NotPieceDataMask;
        private UnitData mCurrentUnit;
        private JobParam mUnlockJobParam;
        private List<GameObject> mGainedQuests;
        private List<ItemData> mUsedElemPieces;
        private ItemParam LastUpadatedItemParam;
        private UnitData mTempUnit;
        private List<GameObject> mUnlockJobList;
        private List<JobSetParam> mCacheCCJobs;

        public UnitKakeraWindow()
        {
            this.mGainedQuests = new List<GameObject>();
            this.mUsedElemPieces = new List<ItemData>();
            this.mUnlockJobList = new List<GameObject>();
            this.mCacheCCJobs = new List<JobSetParam>();
            base..ctor();
            return;
        }

        public void Activated(int pinID)
        {
            if (pinID != 1)
            {
                goto Label_0019;
            }
            this.Refresh(this.mCurrentUnit, this.mUnlockJobParam);
        Label_0019:
            return;
        }

        private void AddPanel(QuestParam questparam, QuestParam[] availableQuests)
        {
            GameObject obj2;
            SRPG_Button button;
            Button button2;
            bool flag;
            bool flag2;
            <AddPanel>c__AnonStorey3D0 storeyd;
            storeyd = new <AddPanel>c__AnonStorey3D0();
            storeyd.questparam = questparam;
            this.QuestList.SetActive(1);
            if ((this.NotFoundGainQuestObject != null) == null)
            {
                goto Label_0038;
            }
            this.NotFoundGainQuestObject.SetActive(0);
        Label_0038:
            if (storeyd.questparam != null)
            {
                goto Label_0045;
            }
            return;
        Label_0045:
            if (storeyd.questparam.IsMulti == null)
            {
                goto Label_0057;
            }
            return;
        Label_0057:
            obj2 = Object.Instantiate<GameObject>(this.QuestListItemTemplate);
            button = obj2.GetComponent<SRPG_Button>();
            if ((button != null) == null)
            {
                goto Label_0088;
            }
            button.AddListener(new SRPG_Button.ButtonClickEvent(this.OnQuestSelect));
        Label_0088:
            this.mGainedQuests.Add(obj2);
            button2 = obj2.GetComponent<Button>();
            if ((button2 != null) == null)
            {
                goto Label_00E2;
            }
            flag = storeyd.questparam.IsDateUnlock(-1L);
            flag2 = (Array.Find<QuestParam>(availableQuests, new Predicate<QuestParam>(storeyd.<>m__46A)) == null) == 0;
            button2.set_interactable((flag == null) ? 0 : flag2);
        Label_00E2:
            DataSource.Bind<QuestParam>(obj2, storeyd.questparam);
            obj2.get_transform().SetParent(this.QuestListParent, 0);
            obj2.SetActive(1);
            return;
        }

        private int CalcCanAwakeMaxLv(int awakelv, int awakelvcap, int piece_amount, int element_piece_amount, int common_piece_amount)
        {
            int num;
            MasterParam param;
            int num2;
            int num3;
            int num4;
            int num5;
            int num6;
            int num7;
            num = awakelv;
            param = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam;
            if (param != null)
            {
                goto Label_0015;
            }
            return num;
        Label_0015:
            num2 = awakelv;
            num3 = num2;
            goto Label_00C7;
        Label_001E:
            num4 = param.GetAwakeNeedPieces(num3);
            if (piece_amount <= 0)
            {
                goto Label_004D;
            }
            if (num4 <= 0)
            {
                goto Label_004D;
            }
            num5 = Math.Min(num4, piece_amount);
            num4 -= num5;
            piece_amount -= num5;
        Label_004D:
            if (element_piece_amount <= 0)
            {
                goto Label_0076;
            }
            if (num4 <= 0)
            {
                goto Label_0076;
            }
            num6 = Math.Min(num4, element_piece_amount);
            num4 -= num6;
            element_piece_amount -= num6;
        Label_0076:
            if (common_piece_amount <= 0)
            {
                goto Label_009F;
            }
            if (num4 <= 0)
            {
                goto Label_009F;
            }
            num7 = Math.Min(num4, common_piece_amount);
            num4 -= num7;
            common_piece_amount -= num7;
        Label_009F:
            if (num4 != null)
            {
                goto Label_00AA;
            }
            num2 = num3 + 1;
        Label_00AA:
            if (piece_amount != null)
            {
                goto Label_00C3;
            }
            if (element_piece_amount != null)
            {
                goto Label_00C3;
            }
            if (common_piece_amount != null)
            {
                goto Label_00C3;
            }
            goto Label_00CE;
        Label_00C3:
            num3 += 1;
        Label_00C7:
            if (num3 < awakelvcap)
            {
                goto Label_001E;
            }
        Label_00CE:
            return Math.Min(awakelvcap, num2);
        }

        private int CalcNeedPieceAll(int value)
        {
            int num;
            int num2;
            int num3;
            int num4;
            MasterParam param;
            int num5;
            int num6;
            num = 0;
            num2 = this.mCurrentUnit.AwakeLv;
            num3 = this.mCurrentUnit.GetAwakeLevelCap();
            num4 = this.mCurrentUnit.AwakeLv + value;
            if (value == null)
            {
                goto Label_003C;
            }
            if (num2 >= num4)
            {
                goto Label_003C;
            }
            if (num4 <= num3)
            {
                goto Label_003E;
            }
        Label_003C:
            return 0;
        Label_003E:
            param = MonoSingleton<GameManager>.Instance.MasterParam;
            num5 = num2;
            goto Label_0075;
        Label_0052:
            num6 = param.GetAwakeNeedPieces(num5);
            if (num5 >= 0)
            {
                goto Label_006A;
            }
            goto Label_006F;
        Label_006A:
            num += num6;
        Label_006F:
            num5 += 1;
        Label_0075:
            if (num5 < num4)
            {
                goto Label_0052;
            }
            return num;
        }

        private bool CheckUnlockJob(int jobno, int awake_lv)
        {
            JobSetParam param;
            if (awake_lv != null)
            {
                goto Label_0008;
            }
            return 0;
        Label_0008:
            if (this.mCurrentUnit.CheckJobUnlockable(jobno) == null)
            {
                goto Label_001B;
            }
            return 0;
        Label_001B:
            param = this.mCurrentUnit.GetJobSetParam(jobno);
            if (param != null)
            {
                goto Label_0030;
            }
            return 0;
        Label_0030:
            if (param.lock_awakelv != null)
            {
                goto Label_003D;
            }
            return 0;
        Label_003D:
            if (param.lock_awakelv <= awake_lv)
            {
                goto Label_004B;
            }
            return 0;
        Label_004B:
            return 1;
        }

        public void ClearPanel()
        {
            int num;
            GameObject obj2;
            this.mGainedQuests.Clear();
            num = 0;
            goto Label_003F;
        Label_0012:
            obj2 = this.QuestListParent.GetChild(num).get_gameObject();
            if ((this.QuestListItemTemplate != obj2) == null)
            {
                goto Label_003B;
            }
            Object.Destroy(obj2);
        Label_003B:
            num += 1;
        Label_003F:
            if (num < this.QuestListParent.get_childCount())
            {
                goto Label_0012;
            }
            return;
        }

        private void OnAdd()
        {
            this.RefreshAwakeLv(1);
            return;
        }

        private void OnAwakeLvSelect(float value)
        {
            this.PointRefresh();
            return;
        }

        private void OnDecideClick()
        {
            string str;
            int num;
            if (this.mUsedElemPieces.Count <= 0)
            {
                goto Label_0088;
            }
            str = null;
            num = 0;
            goto Label_004E;
        Label_001A:
            if (num <= 0)
            {
                goto Label_002D;
            }
            str = str + "、";
        Label_002D:
            str = str + this.mUsedElemPieces[num].Param.name;
            num += 1;
        Label_004E:
            if (num < this.mUsedElemPieces.Count)
            {
                goto Label_001A;
            }
            UIUtility.ConfirmBox(string.Format(LocalizedText.Get("sys.KAKUSEI_CONFIRM_ELEMENT_KAKERA"), str), new UIUtility.DialogResultEvent(this.OnKakusei), null, null, 0, -1, null, null);
            return;
        Label_0088:
            this.OnKakusei(null);
            return;
        }

        private void OnKakusei(GameObject go)
        {
            if (this.OnAwakeAccept == null)
            {
                goto Label_0023;
            }
            this.OnAwakeAccept((int) this.SelectAwakeSlider.get_value());
            return;
        Label_0023:
            MonoSingleton<GameManager>.Instance.Player.AwakingUnit(this.mCurrentUnit);
            return;
        }

        private void OnQuestSelect(SRPG_Button button)
        {
            int num;
            bool flag;
            QuestParam[] paramArray;
            bool flag2;
            <OnQuestSelect>c__AnonStorey3D1 storeyd;
            storeyd = new <OnQuestSelect>c__AnonStorey3D1();
            num = this.mGainedQuests.IndexOf(button.get_gameObject());
            storeyd.quest = DataSource.FindDataOfClass<QuestParam>(this.mGainedQuests[num], null);
            if (storeyd.quest == null)
            {
                goto Label_00C8;
            }
            if (storeyd.quest.IsDateUnlock(-1L) != null)
            {
                goto Label_0069;
            }
            UIUtility.NegativeSystemMessage(null, LocalizedText.Get("sys.DISABLE_QUEST_DATE_UNLOCK"), null, null, 0, -1);
            return;
        Label_0069:
            if (((Array.Find<QuestParam>(MonoSingleton<GameManager>.Instance.Player.AvailableQuests, new Predicate<QuestParam>(storeyd.<>m__46B)) == null) == 0) != null)
            {
                goto Label_00AF;
            }
            UIUtility.NegativeSystemMessage(null, LocalizedText.Get("sys.DISABLE_QUEST_CHALLENGE"), null, null, 0, -1);
            return;
        Label_00AF:
            GlobalVars.SelectedQuestID = storeyd.quest.iname;
            FlowNode_GameObject.ActivateOutputLinks(this, 100);
        Label_00C8:
            return;
        }

        private void OnRemove()
        {
            this.RefreshAwakeLv(-1);
            return;
        }

        public unsafe void PointRefresh()
        {
            object[] objArray3;
            object[] objArray2;
            object[] objArray1;
            PlayerData data;
            UnitData data2;
            int num;
            int num2;
            int num3;
            OInt[] numArray;
            int num4;
            ItemData data3;
            ItemParam param;
            int num5;
            ItemData data4;
            ItemParam param2;
            int num6;
            ItemData data5;
            ItemParam param3;
            int num7;
            int num8;
            string str;
            int num9;
            string str2;
            int num10;
            string str3;
            int num11;
            int num12;
            <PointRefresh>c__AnonStorey3CF storeycf;
            this.mUsedElemPieces.Clear();
            data = MonoSingleton<GameManager>.GetInstanceDirect().Player;
            data2 = new UnitData();
            data2.Setup(this.mCurrentUnit);
            num = data2.AwakeLv + ((int) this.SelectAwakeSlider.get_value());
            num2 = this.CalcNeedPieceAll((int) this.SelectAwakeSlider.get_value());
            this.mTempUnit.SetVirtualAwakeLv(Mathf.Min(data2.GetAwakeLevelCap(), num - 1));
            num3 = 0;
            numArray = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.FixParam.EquipArtifactSlotUnlock;
            num4 = 0;
            goto Label_00FB;
        Label_0089:
            if (*(&(numArray[num4])) == null)
            {
                goto Label_00F5;
            }
            if (*(&(numArray[num4])) > data2.AwakeLv)
            {
                goto Label_00C4;
            }
            goto Label_00F5;
        Label_00C4:
            if (*(&(numArray[num4])) > (data2.AwakeLv + ((int) this.SelectAwakeSlider.get_value())))
            {
                goto Label_00F5;
            }
            num3 += 1;
        Label_00F5:
            num4 += 1;
        Label_00FB:
            if (num4 < ((int) numArray.Length))
            {
                goto Label_0089;
            }
            if ((this.Kakera_Unit != null) == null)
            {
                goto Label_01EB;
            }
            data3 = data.FindItemDataByItemID(data2.UnitParam.piece);
            if (data3 != null)
            {
                goto Label_0188;
            }
            param = MonoSingleton<GameManager>.Instance.MasterParam.GetItemParam(data2.UnitParam.piece);
            if (param != null)
            {
                goto Label_0174;
            }
            DebugUtility.LogError("Not Unit Piece Settings => [" + data2.UnitParam.iname + "]");
            return;
        Label_0174:
            data3 = new ItemData();
            data3.Setup(0L, param, 0);
        Label_0188:
            num5 = Math.Min(num2, data3.Num);
            if (num2 <= 0)
            {
                goto Label_01B2;
            }
            num5 = Math.Min(num2, data3.Num);
            num2 -= num5;
        Label_01B2:
            if ((this.Kakera_Consume_Unit != null) == null)
            {
                goto Label_01DF;
            }
            this.Kakera_Consume_Unit.set_text("@" + &num5.ToString());
        Label_01DF:
            this.Kakera_Unit.SetActive(1);
        Label_01EB:
            if (((this.Kakera_Elem != null) == null) || ((this.Kakera_Elem != null) == null))
            {
                goto Label_030D;
            }
            data4 = data2.GetElementPieceData();
            if (data4 != null)
            {
                goto Label_024A;
            }
            param2 = data2.GetElementPieceParam();
            if (param2 != null)
            {
                goto Label_0236;
            }
            DebugUtility.LogError("[Unit Setting Error?]Not Element Piece!");
            return;
        Label_0236:
            data4 = new ItemData();
            data4.Setup(0L, param2, 0);
        Label_024A:
            if (data4 == null)
            {
                goto Label_025E;
            }
            DataSource.Bind<ItemData>(this.Kakera_Elem, data4);
        Label_025E:
            if ((this.Kakera_Amount_Elem != null) == null)
            {
                goto Label_028A;
            }
            this.Kakera_Amount_Elem.set_text(&data4.Num.ToString());
        Label_028A:
            num6 = 0;
            if ((data4.Num <= 0) || (num2 <= 0))
            {
                goto Label_02D4;
            }
            num6 = Math.Min(num2, data4.Num);
            num2 -= num6;
            if (this.mUsedElemPieces.Contains(data4) != null)
            {
                goto Label_02D4;
            }
            this.mUsedElemPieces.Add(data4);
        Label_02D4:
            if ((this.Kakera_Consume_Elem != null) == null)
            {
                goto Label_0301;
            }
            this.Kakera_Consume_Elem.set_text("@" + &num6.ToString());
        Label_0301:
            this.Kakera_Elem.SetActive(1);
        Label_030D:
            if ((this.Kakera_Common != null) == null)
            {
                goto Label_041E;
            }
            data5 = data2.GetCommonPieceData();
            if (data5 != null)
            {
                goto Label_035B;
            }
            param3 = data2.GetCommonPieceParam();
            if (param3 != null)
            {
                goto Label_0347;
            }
            DebugUtility.LogError("[FixParam Setting Error?]Not Common Piece Settings!");
            return;
        Label_0347:
            data5 = new ItemData();
            data5.Setup(0L, param3, 0);
        Label_035B:
            if (data5 == null)
            {
                goto Label_036F;
            }
            DataSource.Bind<ItemData>(this.Kakera_Common, data5);
        Label_036F:
            if ((this.Kakera_Amount_Common != null) == null)
            {
                goto Label_039B;
            }
            this.Kakera_Amount_Common.set_text(&data5.Num.ToString());
        Label_039B:
            num7 = 0;
            if ((data5.Num <= 0) || (num2 <= 0))
            {
                goto Label_03E5;
            }
            num7 = Math.Min(num2, data5.Num);
            num2 -= num7;
            if (this.mUsedElemPieces.Contains(data5) != null)
            {
                goto Label_03E5;
            }
            this.mUsedElemPieces.Add(data5);
        Label_03E5:
            if ((this.Kakera_Consume_Common != null) == null)
            {
                goto Label_0412;
            }
            this.Kakera_Consume_Common.set_text("@" + &num7.ToString());
        Label_0412:
            this.Kakera_Common.SetActive(1);
        Label_041E:
            if ((this.AwakeResultLv != null) == null)
            {
                goto Label_047D;
            }
            num8 = ((this.SelectAwakeSlider != null) == null) ? 1 : ((int) this.SelectAwakeSlider.get_value());
            objArray1 = new object[] { (int) num8 };
            str = LocalizedText.Get("sys.TEXT_UNITAWAKE_RESULT_LV", objArray1);
            this.AwakeResultLv.set_text(str);
        Label_047D:
            if ((this.AwakeResultComb != null) == null)
            {
                goto Label_04DC;
            }
            num9 = ((this.SelectAwakeSlider != null) == null) ? 1 : ((int) this.SelectAwakeSlider.get_value());
            objArray2 = new object[] { (int) num9 };
            str2 = LocalizedText.Get("sys.TEXT_UNITAWAKE_RESULT_COMB", objArray2);
            this.AwakeResultComb.set_text(str2);
        Label_04DC:
            if (this.mUnlockJobList == null)
            {
                goto Label_059B;
            }
            num10 = 0;
            goto Label_0589;
        Label_04EF:
            if (num10 <= ((int) data2.Jobs.Length))
            {
                goto Label_0503;
            }
            goto Label_059B;
        Label_0503:
            if ((this.mCacheCCJobs == null) || (this.mCacheCCJobs.Count <= 0))
            {
                goto Label_0568;
            }
            storeycf = new <PointRefresh>c__AnonStorey3CF();
            storeycf.js = data2.GetJobSetParam(num10);
            if (storeycf.js != null)
            {
                goto Label_0546;
            }
            goto Label_0583;
        Label_0546:
            if (this.mCacheCCJobs.Find(new Predicate<JobSetParam>(storeycf.<>m__469)) == null)
            {
                goto Label_0568;
            }
            goto Label_0583;
        Label_0568:
            this.mUnlockJobList[num10].SetActive(this.CheckUnlockJob(num10, num));
        Label_0583:
            num10 += 1;
        Label_0589:
            if (num10 < this.mUnlockJobList.Count)
            {
                goto Label_04EF;
            }
        Label_059B:
            if ((this.UnlockArtifactSlot != null) == null)
            {
                goto Label_05FE;
            }
            this.UnlockArtifactSlot.SetActive(num3 > 0);
            if ((num3 <= 0) || ((this.AwakeResultArtifactSlots != null) == null))
            {
                goto Label_05FE;
            }
            objArray3 = new object[] { (int) num3 };
            str3 = LocalizedText.Get("sys.TEXT_UNITAWAKE_RESULT_SLOT", objArray3);
            this.AwakeResultArtifactSlots.set_text(str3);
        Label_05FE:
            if ((this.PlusBtn != null) == null)
            {
                goto Label_0646;
            }
            this.PlusBtn.set_interactable(((this.SelectAwakeSlider != null) == null) ? 0 : (this.SelectAwakeSlider.get_value() < this.SelectAwakeSlider.get_maxValue()));
        Label_0646:
            if ((this.MinusBtn != null) == null)
            {
                goto Label_068E;
            }
            this.MinusBtn.set_interactable(((this.SelectAwakeSlider != null) == null) ? 0 : (this.SelectAwakeSlider.get_value() > this.SelectAwakeSlider.get_minValue()));
        Label_068E:
            GameParameter.UpdateAll(base.get_gameObject());
            return;
        }

        public unsafe void Refresh(UnitData unit, JobParam jobUnlock)
        {
            object[] objArray3;
            object[] objArray2;
            object[] objArray1;
            int num;
            int num2;
            int num3;
            int num4;
            GameObject obj2;
            JobSetParam[] paramArray;
            int num5;
            int num6;
            int num7;
            bool flag;
            PlayerData data;
            int num8;
            bool flag2;
            ItemData data2;
            ItemParam param;
            int num9;
            ItemData data3;
            ItemParam param2;
            int num10;
            ItemData data4;
            ItemParam param3;
            int num11;
            ItemData data5;
            int num12;
            ItemData data6;
            int num13;
            ItemData data7;
            int num14;
            int num15;
            int num16;
            int num17;
            string str;
            int num18;
            string str2;
            int num19;
            OInt[] numArray;
            int num20;
            string str3;
            int num21;
            int num22;
            int num23;
            <Refresh>c__AnonStorey3CE storeyce;
            if (unit != null)
            {
                goto Label_0007;
            }
            return;
        Label_0007:
            this.mCurrentUnit = unit;
            this.mUnlockJobParam = jobUnlock;
            this.mUsedElemPieces.Clear();
            this.mCacheCCJobs.Clear();
            this.mTempUnit = new UnitData();
            this.mTempUnit.Setup(unit);
            num = 0;
            goto Label_0088;
        Label_0049:
            if ((this.mUnlockJobList[num] != null) == null)
            {
                goto Label_0084;
            }
            DataSource.Bind<JobParam>(this.mUnlockJobList[num], null);
            this.mUnlockJobList[num].SetActive(0);
        Label_0084:
            num += 1;
        Label_0088:
            if (num < this.mUnlockJobList.Count)
            {
                goto Label_0049;
            }
            num2 = (int) unit.Jobs.Length;
            if (this.mUnlockJobList.Count >= num2)
            {
                goto Label_010D;
            }
            num3 = num2 - this.mUnlockJobList.Count;
            num4 = 0;
            goto Label_0106;
        Label_00C8:
            obj2 = Object.Instantiate<GameObject>(this.JobUnlock);
            if ((obj2 != null) == null)
            {
                goto Label_0102;
            }
            obj2.get_transform().SetParent(this.JobUnlockParent, 0);
            this.mUnlockJobList.Add(obj2);
        Label_0102:
            num4 += 1;
        Label_0106:
            if (num4 < num3)
            {
                goto Label_00C8;
            }
        Label_010D:
            paramArray = MonoSingleton<GameManager>.Instance.MasterParam.GetClassChangeJobSetParam(unit.UnitParam.iname);
            if (paramArray == null)
            {
                goto Label_013D;
            }
            this.mCacheCCJobs.AddRange(paramArray);
        Label_013D:
            num5 = 0;
            goto Label_0195;
        Label_0145:
            if (unit.CheckJobUnlockable(num5) == null)
            {
                goto Label_0157;
            }
            goto Label_018F;
        Label_0157:
            if ((this.mUnlockJobList[num5] != null) == null)
            {
                goto Label_018F;
            }
            DataSource.Bind<JobParam>(this.mUnlockJobList[num5], unit.Jobs[num5].Param);
        Label_018F:
            num5 += 1;
        Label_0195:
            if (num5 < num2)
            {
                goto Label_0145;
            }
            DataSource.Bind<UnitData>(base.get_gameObject(), null);
            GameParameter.UpdateAll(base.get_gameObject());
            DataSource.Bind<UnitData>(base.get_gameObject(), this.mTempUnit);
            num6 = unit.AwakeLv;
            flag = unit.GetAwakeLevelCap() > num6;
            if ((this.CautionObject != null) == null)
            {
                goto Label_01FE;
            }
            this.CautionObject.SetActive(flag == 0);
        Label_01FE:
            if ((this.DecideButton != null) == null)
            {
                goto Label_022A;
            }
            this.DecideButton.set_interactable((flag == null) ? 0 : unit.CheckUnitAwaking());
        Label_022A:
            if ((this.SelectAwakeSlider != null) == null)
            {
                goto Label_0256;
            }
            this.SelectAwakeSlider.set_interactable((flag == null) ? 0 : unit.CheckUnitAwaking());
        Label_0256:
            if (flag == null)
            {
                goto Label_0A09;
            }
            data = MonoSingleton<GameManager>.Instance.Player;
            num8 = unit.GetAwakeNeedPieces();
            flag2 = 0;
            if ((this.Kakera_Unit != null) == null)
            {
                goto Label_03A2;
            }
            data2 = data.FindItemDataByItemID(unit.UnitParam.piece);
            if (data2 != null)
            {
                goto Label_02F7;
            }
            param = MonoSingleton<GameManager>.Instance.MasterParam.GetItemParam(unit.UnitParam.piece);
            if (param != null)
            {
                goto Label_02E3;
            }
            DebugUtility.LogError("Not Unit Piece Settings => [" + unit.UnitParam.iname + "]");
            return;
        Label_02E3:
            data2 = new ItemData();
            data2.Setup(0L, param, 0);
        Label_02F7:
            if (data2 == null)
            {
                goto Label_030B;
            }
            DataSource.Bind<ItemData>(this.Kakera_Unit, data2);
        Label_030B:
            if ((this.Kakera_Amount_Unit != null) == null)
            {
                goto Label_0337;
            }
            this.Kakera_Amount_Unit.set_text(&data2.Num.ToString());
        Label_0337:
            num9 = Math.Min(num8, data2.Num);
            if (num8 <= 0)
            {
                goto Label_0369;
            }
            num9 = Math.Min(num8, data2.Num);
            flag2 = 1;
            num8 -= num9;
        Label_0369:
            if ((this.Kakera_Consume_Unit != null) == null)
            {
                goto Label_0396;
            }
            this.Kakera_Consume_Unit.set_text("@" + &num9.ToString());
        Label_0396:
            this.Kakera_Unit.SetActive(1);
        Label_03A2:
            if ((this.Kakera_Elem != null) == null)
            {
                goto Label_04C7;
            }
            data3 = unit.GetElementPieceData();
            if (data3 != null)
            {
                goto Label_03F0;
            }
            param2 = unit.GetElementPieceParam();
            if (param2 != null)
            {
                goto Label_03DC;
            }
            DebugUtility.LogError("[Unit Setting Error?]Not Element Piece!");
            return;
        Label_03DC:
            data3 = new ItemData();
            data3.Setup(0L, param2, 0);
        Label_03F0:
            if (data3 == null)
            {
                goto Label_0404;
            }
            DataSource.Bind<ItemData>(this.Kakera_Elem, data3);
        Label_0404:
            if ((this.Kakera_Amount_Elem != null) == null)
            {
                goto Label_0430;
            }
            this.Kakera_Amount_Elem.set_text(&data3.Num.ToString());
        Label_0430:
            num10 = Math.Min(num8, data3.Num);
            if ((data3.Num <= 0) || (num8 <= 0))
            {
                goto Label_048E;
            }
            num10 = Math.Min(num8, data3.Num);
            flag2 = 1;
            num8 -= num10;
            if (this.mUsedElemPieces.Contains(data3) != null)
            {
                goto Label_048E;
            }
            this.mUsedElemPieces.Add(data3);
        Label_048E:
            if ((this.Kakera_Consume_Elem != null) == null)
            {
                goto Label_04BB;
            }
            this.Kakera_Consume_Elem.set_text("@" + &num10.ToString());
        Label_04BB:
            this.Kakera_Elem.SetActive(1);
        Label_04C7:
            if ((this.Kakera_Common != null) == null)
            {
                goto Label_05DF;
            }
            data4 = unit.GetCommonPieceData();
            if (data4 != null)
            {
                goto Label_0515;
            }
            param3 = unit.GetCommonPieceParam();
            if (param3 != null)
            {
                goto Label_0501;
            }
            DebugUtility.LogError("[FixParam Setting Error?]Not Common Piece Settings!");
            return;
        Label_0501:
            data4 = new ItemData();
            data4.Setup(0L, param3, 0);
        Label_0515:
            if (data4 == null)
            {
                goto Label_0529;
            }
            DataSource.Bind<ItemData>(this.Kakera_Common, data4);
        Label_0529:
            if ((this.Kakera_Amount_Common != null) == null)
            {
                goto Label_0555;
            }
            this.Kakera_Amount_Common.set_text(&data4.Num.ToString());
        Label_0555:
            num11 = 0;
            if ((data4.Num <= 0) || (num8 <= 0))
            {
                goto Label_05A6;
            }
            num11 = Math.Min(num8, data4.Num);
            flag2 = 1;
            num8 -= num11;
            if (this.mUsedElemPieces.Contains(data4) != null)
            {
                goto Label_05A6;
            }
            this.mUsedElemPieces.Add(data4);
        Label_05A6:
            if ((this.Kakera_Consume_Common != null) == null)
            {
                goto Label_05D3;
            }
            this.Kakera_Consume_Common.set_text("@" + &num11.ToString());
        Label_05D3:
            this.Kakera_Common.SetActive(1);
        Label_05DF:
            if ((this.SelectAwakeSlider != null) == null)
            {
                goto Label_06E9;
            }
            data5 = data.FindItemDataByItemID(unit.UnitParam.piece);
            num12 = (data5 == null) ? 0 : data5.Num;
            data6 = unit.GetElementPieceData();
            num13 = (data6 == null) ? 0 : data6.Num;
            data7 = unit.GetCommonPieceData();
            num14 = (data7 == null) ? 0 : data7.Num;
            num15 = this.CalcCanAwakeMaxLv(unit.AwakeLv, unit.GetAwakeLevelCap(), num12, num13, num14);
            this.SelectAwakeSlider.get_onValueChanged().RemoveAllListeners();
            this.SelectAwakeSlider.set_minValue((float) (((num15 - unit.AwakeLv) <= 0) ? 0 : 1));
            this.SelectAwakeSlider.set_maxValue((float) (num15 - unit.AwakeLv));
            this.SelectAwakeSlider.set_value(this.SelectAwakeSlider.get_minValue());
            this.SelectAwakeSlider.get_onValueChanged().AddListener(new UnityAction<float>(this, this.OnAwakeLvSelect));
        Label_06E9:
            if (this.mUnlockJobList == null)
            {
                goto Label_07AF;
            }
            num16 = 0;
            goto Label_079D;
        Label_06FC:
            if (num16 <= num2)
            {
                goto Label_0709;
            }
            goto Label_07AF;
        Label_0709:
            if ((this.mCacheCCJobs == null) || (this.mCacheCCJobs.Count <= 0))
            {
                goto Label_076E;
            }
            storeyce = new <Refresh>c__AnonStorey3CE();
            storeyce.js = unit.GetJobSetParam(num16);
            if (storeyce.js != null)
            {
                goto Label_074C;
            }
            goto Label_0797;
        Label_074C:
            if (this.mCacheCCJobs.Find(new Predicate<JobSetParam>(storeyce.<>m__468)) == null)
            {
                goto Label_076E;
            }
            goto Label_0797;
        Label_076E:
            this.mUnlockJobList[num16].SetActive(this.CheckUnlockJob(num16, num6 + ((int) this.SelectAwakeSlider.get_value())));
        Label_0797:
            num16 += 1;
        Label_079D:
            if (num16 < this.mUnlockJobList.Count)
            {
                goto Label_06FC;
            }
        Label_07AF:
            if ((this.AwakeResultLv != null) == null)
            {
                goto Label_080E;
            }
            num17 = ((this.SelectAwakeSlider != null) == null) ? 1 : ((int) this.SelectAwakeSlider.get_value());
            objArray1 = new object[] { (int) num17 };
            str = LocalizedText.Get("sys.TEXT_UNITAWAKE_RESULT_LV", objArray1);
            this.AwakeResultLv.set_text(str);
        Label_080E:
            if ((this.AwakeResultComb != null) == null)
            {
                goto Label_086D;
            }
            num18 = ((this.SelectAwakeSlider != null) == null) ? 1 : ((int) this.SelectAwakeSlider.get_value());
            objArray2 = new object[] { (int) num18 };
            str2 = LocalizedText.Get("sys.TEXT_UNITAWAKE_RESULT_COMB", objArray2);
            this.AwakeResultComb.set_text(str2);
        Label_086D:
            num19 = 0;
            numArray = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.FixParam.EquipArtifactSlotUnlock;
            num20 = 0;
            goto Label_0900;
        Label_088E:
            if (*(&(numArray[num20])) == null)
            {
                goto Label_08FA;
            }
            if (*(&(numArray[num20])) > unit.AwakeLv)
            {
                goto Label_08C9;
            }
            goto Label_08FA;
        Label_08C9:
            if (*(&(numArray[num20])) > (unit.AwakeLv + ((int) this.SelectAwakeSlider.get_value())))
            {
                goto Label_08FA;
            }
            num19 += 1;
        Label_08FA:
            num20 += 1;
        Label_0900:
            if (num20 < ((int) numArray.Length))
            {
                goto Label_088E;
            }
            if ((this.UnlockArtifactSlot != null) == null)
            {
                goto Label_095D;
            }
            this.UnlockArtifactSlot.SetActive(num19 > 0);
            if (num19 <= 0)
            {
                goto Label_095D;
            }
            objArray3 = new object[] { (int) num19 };
            str3 = LocalizedText.Get("sys.TEXT_UNITAWAKE_RESULT_SLOT", objArray3);
            this.AwakeResultArtifactSlots.set_text(str3);
        Label_095D:
            if ((this.NotPieceDataMask != null) == null)
            {
                goto Label_097E;
            }
            this.NotPieceDataMask.SetActive(num8 > 0);
        Label_097E:
            if (flag2 == null)
            {
                goto Label_09C1;
            }
            if ((this.Kakera_Consume_Message != null) == null)
            {
                goto Label_0A2F;
            }
            this.Kakera_Consume_Message.set_text(LocalizedText.Get((num8 != null) ? "sys.CONFIRM_KAKUSEI4" : "sys.CONFIRM_KAKUSEI2"));
            goto Label_0A04;
        Label_09C1:
            if ((this.Kakera_Caution_Message != null) == null)
            {
                goto Label_09E7;
            }
            this.Kakera_Caution_Message.set_text(LocalizedText.Get("sys.CONFIRM_KAKUSEI3"));
        Label_09E7:
            if ((this.CautionObject != null) == null)
            {
                goto Label_0A2F;
            }
            this.CautionObject.SetActive(1);
        Label_0A04:
            goto Label_0A2F;
        Label_0A09:
            if ((this.Kakera_Caution_Message != null) == null)
            {
                goto Label_0A2F;
            }
            this.Kakera_Caution_Message.set_text(LocalizedText.Get("sys.KAKUSEI_CAPPED"));
        Label_0A2F:
            if ((this.PlusBtn != null) == null)
            {
                goto Label_0A77;
            }
            this.PlusBtn.set_interactable(((this.SelectAwakeSlider != null) == null) ? 0 : (this.SelectAwakeSlider.get_value() < this.SelectAwakeSlider.get_maxValue()));
        Label_0A77:
            if ((this.MinusBtn != null) == null)
            {
                goto Label_0ABF;
            }
            this.MinusBtn.set_interactable(((this.SelectAwakeSlider != null) == null) ? 0 : (this.SelectAwakeSlider.get_value() > this.SelectAwakeSlider.get_minValue()));
        Label_0ABF:
            this.RefreshGainedQuests(unit);
            GameParameter.UpdateAll(base.get_gameObject());
            return;
        }

        private void RefreshAwakeLv(int value)
        {
            if (value <= 0)
            {
                goto Label_004F;
            }
            if ((this.SelectAwakeSlider != null) == null)
            {
                goto Label_009F;
            }
            if (this.SelectAwakeSlider.get_maxValue() < (this.SelectAwakeSlider.get_value() + ((float) value)))
            {
                goto Label_009F;
            }
            this.SelectAwakeSlider.set_value(this.SelectAwakeSlider.get_value() + ((float) value));
            goto Label_009F;
        Label_004F:
            if (value >= 0)
            {
                goto Label_009E;
            }
            if ((this.SelectAwakeSlider != null) == null)
            {
                goto Label_009F;
            }
            if ((this.SelectAwakeSlider.get_value() + ((float) value)) < this.SelectAwakeSlider.get_minValue())
            {
                goto Label_009F;
            }
            this.SelectAwakeSlider.set_value(this.SelectAwakeSlider.get_value() + ((float) value));
            goto Label_009F;
        Label_009E:
            return;
        Label_009F:
            return;
        }

        private unsafe void RefreshGainedQuests(UnitData unit)
        {
            Text text;
            ItemParam param;
            QuestParam[] paramArray;
            List<QuestParam> list;
            QuestParam param2;
            List<QuestParam>.Enumerator enumerator;
            this.ClearPanel();
            if ((this.QuestList == null) == null)
            {
                goto Label_0018;
            }
            return;
        Label_0018:
            this.QuestList.SetActive(0);
            if ((this.NotFoundGainQuestObject != null) == null)
            {
                goto Label_0069;
            }
            text = this.NotFoundGainQuestObject.GetComponent<Text>();
            if ((text != null) == null)
            {
                goto Label_005D;
            }
            text.set_text(LocalizedText.Get("sys.UNIT_GAINED_COMMENT"));
        Label_005D:
            this.NotFoundGainQuestObject.SetActive(1);
        Label_0069:
            if ((this.QuestListItemTemplate == null) != null)
            {
                goto Label_0091;
            }
            if ((this.QuestListParent == null) != null)
            {
                goto Label_0091;
            }
            if (unit != null)
            {
                goto Label_0092;
            }
        Label_0091:
            return;
        Label_0092:
            param = MonoSingleton<GameManager>.Instance.GetItemParam(unit.UnitParam.piece);
            DataSource.Bind<ItemParam>(this.QuestList, param);
            if (this.LastUpadatedItemParam == param)
            {
                goto Label_00CD;
            }
            this.SetScrollTop();
            this.LastUpadatedItemParam = param;
        Label_00CD:
            if ((QuestDropParam.Instance != null) == null)
            {
                goto Label_013B;
            }
            paramArray = MonoSingleton<GameManager>.Instance.Player.AvailableQuests;
            enumerator = QuestDropParam.Instance.GetItemDropQuestList(param, GlobalVars.GetDropTableGeneratedDateTime()).GetEnumerator();
        Label_0106:
            try
            {
                goto Label_011D;
            Label_010B:
                param2 = &enumerator.Current;
                this.AddPanel(param2, paramArray);
            Label_011D:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_010B;
                }
                goto Label_013B;
            }
            finally
            {
            Label_012E:
                ((List<QuestParam>.Enumerator) enumerator).Dispose();
            }
        Label_013B:
            return;
        }

        private unsafe void SetScrollTop()
        {
            RectTransform transform;
            Vector2 vector;
            transform = this.QuestListParent.GetComponent<RectTransform>();
            if ((transform != null) == null)
            {
                goto Label_0032;
            }
            vector = transform.get_anchoredPosition();
            &vector.y = 0f;
            transform.set_anchoredPosition(vector);
        Label_0032:
            return;
        }

        private void Start()
        {
            if ((this.QuestListItemTemplate != null) == null)
            {
                goto Label_001D;
            }
            this.QuestListItemTemplate.SetActive(0);
        Label_001D:
            if ((this.DecideButton != null) == null)
            {
                goto Label_004A;
            }
            this.DecideButton.get_onClick().AddListener(new UnityAction(this, this.OnDecideClick));
        Label_004A:
            if ((this.PlusBtn != null) == null)
            {
                goto Label_0077;
            }
            this.PlusBtn.get_onClick().AddListener(new UnityAction(this, this.OnAdd));
        Label_0077:
            if ((this.MinusBtn != null) == null)
            {
                goto Label_00A4;
            }
            this.MinusBtn.get_onClick().AddListener(new UnityAction(this, this.OnRemove));
        Label_00A4:
            if ((this.JobUnlock != null) == null)
            {
                goto Label_00C1;
            }
            this.JobUnlock.SetActive(0);
        Label_00C1:
            return;
        }

        [CompilerGenerated]
        private sealed class <AddPanel>c__AnonStorey3D0
        {
            internal QuestParam questparam;

            public <AddPanel>c__AnonStorey3D0()
            {
                base..ctor();
                return;
            }

            internal bool <>m__46A(QuestParam p)
            {
                return (p == this.questparam);
            }
        }

        [CompilerGenerated]
        private sealed class <OnQuestSelect>c__AnonStorey3D1
        {
            internal QuestParam quest;

            public <OnQuestSelect>c__AnonStorey3D1()
            {
                base..ctor();
                return;
            }

            internal bool <>m__46B(QuestParam p)
            {
                return (p == this.quest);
            }
        }

        [CompilerGenerated]
        private sealed class <PointRefresh>c__AnonStorey3CF
        {
            internal JobSetParam js;

            public <PointRefresh>c__AnonStorey3CF()
            {
                base..ctor();
                return;
            }

            internal bool <>m__469(JobSetParam v)
            {
                return (v.iname == this.js.iname);
            }
        }

        [CompilerGenerated]
        private sealed class <Refresh>c__AnonStorey3CE
        {
            internal JobSetParam js;

            public <Refresh>c__AnonStorey3CE()
            {
                base..ctor();
                return;
            }

            internal bool <>m__468(JobSetParam v)
            {
                return (v.iname == this.js.iname);
            }
        }

        public delegate void AwakeEvent(int value);

        public delegate void KakuseiWindowEvent();
    }
}

