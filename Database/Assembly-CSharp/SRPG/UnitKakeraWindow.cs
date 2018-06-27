// Decompiled with JetBrains decompiler
// Type: SRPG.UnitKakeraWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(1, "Refresh", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(100, "クエスト選択", FlowNode.PinTypes.Output, 100)]
  public class UnitKakeraWindow : MonoBehaviour, IFlowInterface
  {
    public UnitKakeraWindow.KakuseiWindowEvent OnKakuseiAccept;
    public UnitKakeraWindow.AwakeEvent OnAwakeAccept;
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
      base.\u002Ector();
    }

    private void Start()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.QuestListItemTemplate, (UnityEngine.Object) null))
        this.QuestListItemTemplate.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.DecideButton, (UnityEngine.Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent) this.DecideButton.get_onClick()).AddListener(new UnityAction((object) this, __methodptr(OnDecideClick)));
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.PlusBtn, (UnityEngine.Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent) this.PlusBtn.get_onClick()).AddListener(new UnityAction((object) this, __methodptr(OnAdd)));
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.MinusBtn, (UnityEngine.Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent) this.MinusBtn.get_onClick()).AddListener(new UnityAction((object) this, __methodptr(OnRemove)));
      }
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.JobUnlock, (UnityEngine.Object) null))
        return;
      this.JobUnlock.SetActive(false);
    }

    public void Activated(int pinID)
    {
      if (pinID != 1)
        return;
      this.Refresh(this.mCurrentUnit, this.mUnlockJobParam);
    }

    public void Refresh(UnitData unit = null, JobParam jobUnlock = null)
    {
      if (unit == null)
        return;
      this.mCurrentUnit = unit;
      this.mUnlockJobParam = jobUnlock;
      this.mUsedElemPieces.Clear();
      this.mCacheCCJobs.Clear();
      this.mTempUnit = new UnitData();
      this.mTempUnit.Setup(unit);
      for (int index = 0; index < this.mUnlockJobList.Count; ++index)
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mUnlockJobList[index], (UnityEngine.Object) null))
        {
          DataSource.Bind<JobParam>(this.mUnlockJobList[index], (JobParam) null);
          this.mUnlockJobList[index].SetActive(false);
        }
      }
      int length = unit.Jobs.Length;
      if (this.mUnlockJobList.Count < length)
      {
        int num = length - this.mUnlockJobList.Count;
        for (int index = 0; index < num; ++index)
        {
          GameObject gameObject = (GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) this.JobUnlock);
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) gameObject, (UnityEngine.Object) null))
          {
            gameObject.get_transform().SetParent((Transform) this.JobUnlockParent, false);
            this.mUnlockJobList.Add(gameObject);
          }
        }
      }
      JobSetParam[] changeJobSetParam = MonoSingleton<GameManager>.Instance.MasterParam.GetClassChangeJobSetParam(unit.UnitParam.iname);
      if (changeJobSetParam != null)
        this.mCacheCCJobs.AddRange((IEnumerable<JobSetParam>) changeJobSetParam);
      for (int jobNo = 0; jobNo < length; ++jobNo)
      {
        if (!unit.CheckJobUnlockable(jobNo) && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mUnlockJobList[jobNo], (UnityEngine.Object) null))
          DataSource.Bind<JobParam>(this.mUnlockJobList[jobNo], unit.Jobs[jobNo].Param);
      }
      DataSource.Bind<UnitData>(((Component) this).get_gameObject(), (UnitData) null);
      GameParameter.UpdateAll(((Component) this).get_gameObject());
      DataSource.Bind<UnitData>(((Component) this).get_gameObject(), this.mTempUnit);
      int awakeLv = unit.AwakeLv;
      bool flag1 = unit.GetAwakeLevelCap() > awakeLv;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.CautionObject, (UnityEngine.Object) null))
        this.CautionObject.SetActive(!flag1);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.DecideButton, (UnityEngine.Object) null))
        ((Selectable) this.DecideButton).set_interactable(flag1 && unit.CheckUnitAwaking());
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.SelectAwakeSlider, (UnityEngine.Object) null))
        ((Selectable) this.SelectAwakeSlider).set_interactable(flag1 && unit.CheckUnitAwaking());
      if (flag1)
      {
        PlayerData player = MonoSingleton<GameManager>.Instance.Player;
        int awakeNeedPieces = unit.GetAwakeNeedPieces();
        bool flag2 = false;
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Kakera_Unit, (UnityEngine.Object) null))
        {
          ItemData data = player.FindItemDataByItemID((string) unit.UnitParam.piece);
          if (data == null)
          {
            ItemParam itemParam = MonoSingleton<GameManager>.Instance.MasterParam.GetItemParam((string) unit.UnitParam.piece);
            if (itemParam == null)
            {
              DebugUtility.LogError("Not Unit Piece Settings => [" + unit.UnitParam.iname + "]");
              return;
            }
            data = new ItemData();
            data.Setup(0L, itemParam, 0);
          }
          if (data != null)
            DataSource.Bind<ItemData>(this.Kakera_Unit, data);
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Kakera_Amount_Unit, (UnityEngine.Object) null))
            this.Kakera_Amount_Unit.set_text(data.Num.ToString());
          int num = Math.Min(awakeNeedPieces, data.Num);
          if (awakeNeedPieces > 0)
          {
            num = Math.Min(awakeNeedPieces, data.Num);
            flag2 = true;
            awakeNeedPieces -= num;
          }
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Kakera_Consume_Unit, (UnityEngine.Object) null))
            this.Kakera_Consume_Unit.set_text("@" + num.ToString());
          this.Kakera_Unit.SetActive(true);
        }
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Kakera_Elem, (UnityEngine.Object) null))
        {
          ItemData data = unit.GetElementPieceData();
          if (data == null)
          {
            ItemParam elementPieceParam = unit.GetElementPieceParam();
            if (elementPieceParam == null)
            {
              DebugUtility.LogError("[Unit Setting Error?]Not Element Piece!");
              return;
            }
            data = new ItemData();
            data.Setup(0L, elementPieceParam, 0);
          }
          if (data != null)
            DataSource.Bind<ItemData>(this.Kakera_Elem, data);
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Kakera_Amount_Elem, (UnityEngine.Object) null))
            this.Kakera_Amount_Elem.set_text(data.Num.ToString());
          int num = Math.Min(awakeNeedPieces, data.Num);
          if (data.Num > 0 && awakeNeedPieces > 0)
          {
            num = Math.Min(awakeNeedPieces, data.Num);
            flag2 = true;
            awakeNeedPieces -= num;
            if (!this.mUsedElemPieces.Contains(data))
              this.mUsedElemPieces.Add(data);
          }
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Kakera_Consume_Elem, (UnityEngine.Object) null))
            this.Kakera_Consume_Elem.set_text("@" + num.ToString());
          this.Kakera_Elem.SetActive(true);
        }
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Kakera_Common, (UnityEngine.Object) null))
        {
          ItemData data = unit.GetCommonPieceData();
          if (data == null)
          {
            ItemParam commonPieceParam = unit.GetCommonPieceParam();
            if (commonPieceParam == null)
            {
              DebugUtility.LogError("[FixParam Setting Error?]Not Common Piece Settings!");
              return;
            }
            data = new ItemData();
            data.Setup(0L, commonPieceParam, 0);
          }
          if (data != null)
            DataSource.Bind<ItemData>(this.Kakera_Common, data);
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Kakera_Amount_Common, (UnityEngine.Object) null))
            this.Kakera_Amount_Common.set_text(data.Num.ToString());
          int num = 0;
          if (data.Num > 0 && awakeNeedPieces > 0)
          {
            num = Math.Min(awakeNeedPieces, data.Num);
            flag2 = true;
            awakeNeedPieces -= num;
            if (!this.mUsedElemPieces.Contains(data))
              this.mUsedElemPieces.Add(data);
          }
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Kakera_Consume_Common, (UnityEngine.Object) null))
            this.Kakera_Consume_Common.set_text("@" + num.ToString());
          this.Kakera_Common.SetActive(true);
        }
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.SelectAwakeSlider, (UnityEngine.Object) null))
        {
          ItemData itemDataByItemId = player.FindItemDataByItemID((string) unit.UnitParam.piece);
          int piece_amount = itemDataByItemId == null ? 0 : itemDataByItemId.Num;
          ItemData elementPieceData = unit.GetElementPieceData();
          int element_piece_amount = elementPieceData == null ? 0 : elementPieceData.Num;
          ItemData commonPieceData = unit.GetCommonPieceData();
          int common_piece_amount = commonPieceData == null ? 0 : commonPieceData.Num;
          int num = this.CalcCanAwakeMaxLv(unit.AwakeLv, unit.GetAwakeLevelCap(), piece_amount, element_piece_amount, common_piece_amount);
          ((UnityEventBase) this.SelectAwakeSlider.get_onValueChanged()).RemoveAllListeners();
          this.SelectAwakeSlider.set_minValue(num - unit.AwakeLv <= 0 ? 0.0f : 1f);
          this.SelectAwakeSlider.set_maxValue((float) (num - unit.AwakeLv));
          this.SelectAwakeSlider.set_value(this.SelectAwakeSlider.get_minValue());
          // ISSUE: method pointer
          ((UnityEvent<float>) this.SelectAwakeSlider.get_onValueChanged()).AddListener(new UnityAction<float>((object) this, __methodptr(OnAwakeLvSelect)));
        }
        if (this.mUnlockJobList != null)
        {
          for (int index = 0; index < this.mUnlockJobList.Count && index <= length; ++index)
          {
            if (this.mCacheCCJobs != null && this.mCacheCCJobs.Count > 0)
            {
              // ISSUE: object of a compiler-generated type is created
              // ISSUE: variable of a compiler-generated type
              UnitKakeraWindow.\u003CRefresh\u003Ec__AnonStorey399 refreshCAnonStorey399 = new UnitKakeraWindow.\u003CRefresh\u003Ec__AnonStorey399();
              // ISSUE: reference to a compiler-generated field
              refreshCAnonStorey399.js = unit.GetJobSetParam(index);
              // ISSUE: reference to a compiler-generated field
              // ISSUE: reference to a compiler-generated method
              if (refreshCAnonStorey399.js == null || this.mCacheCCJobs.Find(new Predicate<JobSetParam>(refreshCAnonStorey399.\u003C\u003Em__45E)) != null)
                continue;
            }
            this.mUnlockJobList[index].SetActive(this.CheckUnlockJob(index, awakeLv + (int) this.SelectAwakeSlider.get_value()));
          }
        }
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.AwakeResultLv, (UnityEngine.Object) null))
          this.AwakeResultLv.set_text(LocalizedText.Get("sys.TEXT_UNITAWAKE_RESULT_LV", new object[1]
          {
            (object) (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.SelectAwakeSlider, (UnityEngine.Object) null) ? 1 : (int) this.SelectAwakeSlider.get_value())
          }));
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.AwakeResultComb, (UnityEngine.Object) null))
          this.AwakeResultComb.set_text(LocalizedText.Get("sys.TEXT_UNITAWAKE_RESULT_COMB", new object[1]
          {
            (object) (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.SelectAwakeSlider, (UnityEngine.Object) null) ? 1 : (int) this.SelectAwakeSlider.get_value())
          }));
        int num1 = 0;
        OInt[] artifactSlotUnlock = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.FixParam.EquipArtifactSlotUnlock;
        for (int index = 0; index < artifactSlotUnlock.Length; ++index)
        {
          if ((int) artifactSlotUnlock[index] != 0 && (int) artifactSlotUnlock[index] > unit.AwakeLv && (int) artifactSlotUnlock[index] <= unit.AwakeLv + (int) this.SelectAwakeSlider.get_value())
            ++num1;
        }
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.UnlockArtifactSlot, (UnityEngine.Object) null))
        {
          this.UnlockArtifactSlot.SetActive(num1 > 0);
          if (num1 > 0)
            this.AwakeResultArtifactSlots.set_text(LocalizedText.Get("sys.TEXT_UNITAWAKE_RESULT_SLOT", new object[1]
            {
              (object) num1
            }));
        }
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.NotPieceDataMask, (UnityEngine.Object) null))
          this.NotPieceDataMask.SetActive(awakeNeedPieces > 0);
        if (flag2)
        {
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Kakera_Consume_Message, (UnityEngine.Object) null))
            this.Kakera_Consume_Message.set_text(LocalizedText.Get(awakeNeedPieces != 0 ? "sys.CONFIRM_KAKUSEI4" : "sys.CONFIRM_KAKUSEI2"));
        }
        else
        {
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Kakera_Caution_Message, (UnityEngine.Object) null))
            this.Kakera_Caution_Message.set_text(LocalizedText.Get("sys.CONFIRM_KAKUSEI3"));
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.CautionObject, (UnityEngine.Object) null))
            this.CautionObject.SetActive(true);
        }
      }
      else if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Kakera_Caution_Message, (UnityEngine.Object) null))
        this.Kakera_Caution_Message.set_text(LocalizedText.Get("sys.KAKUSEI_CAPPED"));
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.PlusBtn, (UnityEngine.Object) null))
        ((Selectable) this.PlusBtn).set_interactable(UnityEngine.Object.op_Inequality((UnityEngine.Object) this.SelectAwakeSlider, (UnityEngine.Object) null) && (double) this.SelectAwakeSlider.get_value() < (double) this.SelectAwakeSlider.get_maxValue());
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.MinusBtn, (UnityEngine.Object) null))
        ((Selectable) this.MinusBtn).set_interactable(UnityEngine.Object.op_Inequality((UnityEngine.Object) this.SelectAwakeSlider, (UnityEngine.Object) null) && (double) this.SelectAwakeSlider.get_value() > (double) this.SelectAwakeSlider.get_minValue());
      this.RefreshGainedQuests(unit);
      GameParameter.UpdateAll(((Component) this).get_gameObject());
    }

    private bool CheckUnlockJob(int jobno, int awake_lv)
    {
      if (awake_lv == 0 || this.mCurrentUnit.CheckJobUnlockable(jobno))
        return false;
      JobSetParam jobSetParam = this.mCurrentUnit.GetJobSetParam(jobno);
      return jobSetParam != null && jobSetParam.lock_awakelv != 0 && jobSetParam.lock_awakelv <= awake_lv;
    }

    private int CalcCanAwakeMaxLv(int awakelv, int awakelvcap, int piece_amount, int element_piece_amount, int common_piece_amount)
    {
      int num1 = awakelv;
      MasterParam masterParam = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam;
      if (masterParam == null)
        return num1;
      int val2 = awakelv;
      for (int awakeLv = val2; awakeLv < awakelvcap; ++awakeLv)
      {
        int awakeNeedPieces = masterParam.GetAwakeNeedPieces(awakeLv);
        if (piece_amount > 0 && awakeNeedPieces > 0)
        {
          int num2 = Math.Min(awakeNeedPieces, piece_amount);
          awakeNeedPieces -= num2;
          piece_amount -= num2;
        }
        if (element_piece_amount > 0 && awakeNeedPieces > 0)
        {
          int num2 = Math.Min(awakeNeedPieces, element_piece_amount);
          awakeNeedPieces -= num2;
          element_piece_amount -= num2;
        }
        if (common_piece_amount > 0 && awakeNeedPieces > 0)
        {
          int num2 = Math.Min(awakeNeedPieces, common_piece_amount);
          awakeNeedPieces -= num2;
          common_piece_amount -= num2;
        }
        if (awakeNeedPieces == 0)
          val2 = awakeLv + 1;
        if (piece_amount == 0 && element_piece_amount == 0 && common_piece_amount == 0)
          break;
      }
      return Math.Min(awakelvcap, val2);
    }

    private void OnAwakeLvSelect(float value)
    {
      this.PointRefresh();
    }

    private int CalcNeedPieceAll(int value)
    {
      int num1 = 0;
      int awakeLv1 = this.mCurrentUnit.AwakeLv;
      int awakeLevelCap = this.mCurrentUnit.GetAwakeLevelCap();
      int num2 = this.mCurrentUnit.AwakeLv + value;
      if (value == 0 || awakeLv1 >= num2 || num2 > awakeLevelCap)
        return 0;
      MasterParam masterParam = MonoSingleton<GameManager>.Instance.MasterParam;
      for (int awakeLv2 = awakeLv1; awakeLv2 < num2; ++awakeLv2)
      {
        int awakeNeedPieces = masterParam.GetAwakeNeedPieces(awakeLv2);
        if (awakeLv2 >= 0)
          num1 += awakeNeedPieces;
      }
      return num1;
    }

    public void PointRefresh()
    {
      this.mUsedElemPieces.Clear();
      PlayerData player = MonoSingleton<GameManager>.GetInstanceDirect().Player;
      UnitData unitData = new UnitData();
      unitData.Setup(this.mCurrentUnit);
      int awake_lv = unitData.AwakeLv + (int) this.SelectAwakeSlider.get_value();
      int val1 = this.CalcNeedPieceAll((int) this.SelectAwakeSlider.get_value());
      this.mTempUnit.SetVirtualAwakeLv(Mathf.Min(unitData.GetAwakeLevelCap(), awake_lv - 1));
      int num1 = 0;
      OInt[] artifactSlotUnlock = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.FixParam.EquipArtifactSlotUnlock;
      for (int index = 0; index < artifactSlotUnlock.Length; ++index)
      {
        if ((int) artifactSlotUnlock[index] != 0 && (int) artifactSlotUnlock[index] > unitData.AwakeLv && (int) artifactSlotUnlock[index] <= unitData.AwakeLv + (int) this.SelectAwakeSlider.get_value())
          ++num1;
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Kakera_Unit, (UnityEngine.Object) null))
      {
        ItemData itemData = player.FindItemDataByItemID((string) unitData.UnitParam.piece);
        if (itemData == null)
        {
          ItemParam itemParam = MonoSingleton<GameManager>.Instance.MasterParam.GetItemParam((string) unitData.UnitParam.piece);
          if (itemParam == null)
          {
            DebugUtility.LogError("Not Unit Piece Settings => [" + unitData.UnitParam.iname + "]");
            return;
          }
          itemData = new ItemData();
          itemData.Setup(0L, itemParam, 0);
        }
        int num2 = Math.Min(val1, itemData.Num);
        if (val1 > 0)
        {
          num2 = Math.Min(val1, itemData.Num);
          val1 -= num2;
        }
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Kakera_Consume_Unit, (UnityEngine.Object) null))
          this.Kakera_Consume_Unit.set_text("@" + num2.ToString());
        this.Kakera_Unit.SetActive(true);
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Kakera_Elem, (UnityEngine.Object) null) && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Kakera_Elem, (UnityEngine.Object) null))
      {
        ItemData data = unitData.GetElementPieceData();
        if (data == null)
        {
          ItemParam elementPieceParam = unitData.GetElementPieceParam();
          if (elementPieceParam == null)
          {
            DebugUtility.LogError("[Unit Setting Error?]Not Element Piece!");
            return;
          }
          data = new ItemData();
          data.Setup(0L, elementPieceParam, 0);
        }
        if (data != null)
          DataSource.Bind<ItemData>(this.Kakera_Elem, data);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Kakera_Amount_Elem, (UnityEngine.Object) null))
          this.Kakera_Amount_Elem.set_text(data.Num.ToString());
        int num2 = 0;
        if (data.Num > 0 && val1 > 0)
        {
          num2 = Math.Min(val1, data.Num);
          val1 -= num2;
          if (!this.mUsedElemPieces.Contains(data))
            this.mUsedElemPieces.Add(data);
        }
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Kakera_Consume_Elem, (UnityEngine.Object) null))
          this.Kakera_Consume_Elem.set_text("@" + num2.ToString());
        this.Kakera_Elem.SetActive(true);
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Kakera_Common, (UnityEngine.Object) null))
      {
        ItemData data = unitData.GetCommonPieceData();
        if (data == null)
        {
          ItemParam commonPieceParam = unitData.GetCommonPieceParam();
          if (commonPieceParam == null)
          {
            DebugUtility.LogError("[FixParam Setting Error?]Not Common Piece Settings!");
            return;
          }
          data = new ItemData();
          data.Setup(0L, commonPieceParam, 0);
        }
        if (data != null)
          DataSource.Bind<ItemData>(this.Kakera_Common, data);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Kakera_Amount_Common, (UnityEngine.Object) null))
          this.Kakera_Amount_Common.set_text(data.Num.ToString());
        int num2 = 0;
        if (data.Num > 0 && val1 > 0)
        {
          num2 = Math.Min(val1, data.Num);
          int num3 = val1 - num2;
          if (!this.mUsedElemPieces.Contains(data))
            this.mUsedElemPieces.Add(data);
        }
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Kakera_Consume_Common, (UnityEngine.Object) null))
          this.Kakera_Consume_Common.set_text("@" + num2.ToString());
        this.Kakera_Common.SetActive(true);
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.AwakeResultLv, (UnityEngine.Object) null))
        this.AwakeResultLv.set_text(LocalizedText.Get("sys.TEXT_UNITAWAKE_RESULT_LV", new object[1]
        {
          (object) (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.SelectAwakeSlider, (UnityEngine.Object) null) ? 1 : (int) this.SelectAwakeSlider.get_value())
        }));
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.AwakeResultComb, (UnityEngine.Object) null))
        this.AwakeResultComb.set_text(LocalizedText.Get("sys.TEXT_UNITAWAKE_RESULT_COMB", new object[1]
        {
          (object) (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.SelectAwakeSlider, (UnityEngine.Object) null) ? 1 : (int) this.SelectAwakeSlider.get_value())
        }));
      if (this.mUnlockJobList != null)
      {
        for (int index = 0; index < this.mUnlockJobList.Count && index <= unitData.Jobs.Length; ++index)
        {
          if (this.mCacheCCJobs != null && this.mCacheCCJobs.Count > 0)
          {
            // ISSUE: object of a compiler-generated type is created
            // ISSUE: variable of a compiler-generated type
            UnitKakeraWindow.\u003CPointRefresh\u003Ec__AnonStorey39A refreshCAnonStorey39A = new UnitKakeraWindow.\u003CPointRefresh\u003Ec__AnonStorey39A();
            // ISSUE: reference to a compiler-generated field
            refreshCAnonStorey39A.js = unitData.GetJobSetParam(index);
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated method
            if (refreshCAnonStorey39A.js == null || this.mCacheCCJobs.Find(new Predicate<JobSetParam>(refreshCAnonStorey39A.\u003C\u003Em__45F)) != null)
              continue;
          }
          this.mUnlockJobList[index].SetActive(this.CheckUnlockJob(index, awake_lv));
        }
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.UnlockArtifactSlot, (UnityEngine.Object) null))
      {
        this.UnlockArtifactSlot.SetActive(num1 > 0);
        if (num1 > 0 && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.AwakeResultArtifactSlots, (UnityEngine.Object) null))
          this.AwakeResultArtifactSlots.set_text(LocalizedText.Get("sys.TEXT_UNITAWAKE_RESULT_SLOT", new object[1]
          {
            (object) num1
          }));
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.PlusBtn, (UnityEngine.Object) null))
        ((Selectable) this.PlusBtn).set_interactable(UnityEngine.Object.op_Inequality((UnityEngine.Object) this.SelectAwakeSlider, (UnityEngine.Object) null) && (double) this.SelectAwakeSlider.get_value() < (double) this.SelectAwakeSlider.get_maxValue());
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.MinusBtn, (UnityEngine.Object) null))
        ((Selectable) this.MinusBtn).set_interactable(UnityEngine.Object.op_Inequality((UnityEngine.Object) this.SelectAwakeSlider, (UnityEngine.Object) null) && (double) this.SelectAwakeSlider.get_value() > (double) this.SelectAwakeSlider.get_minValue());
      GameParameter.UpdateAll(((Component) this).get_gameObject());
    }

    private void RefreshGainedQuests(UnitData unit)
    {
      this.ClearPanel();
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.QuestList, (UnityEngine.Object) null))
        return;
      this.QuestList.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.NotFoundGainQuestObject, (UnityEngine.Object) null))
      {
        Text component = (Text) this.NotFoundGainQuestObject.GetComponent<Text>();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
          component.set_text(LocalizedText.Get("sys.UNIT_GAINED_COMMENT"));
        this.NotFoundGainQuestObject.SetActive(true);
      }
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.QuestListItemTemplate, (UnityEngine.Object) null) || UnityEngine.Object.op_Equality((UnityEngine.Object) this.QuestListParent, (UnityEngine.Object) null) || unit == null)
        return;
      ItemParam itemParam = MonoSingleton<GameManager>.Instance.GetItemParam((string) unit.UnitParam.piece);
      DataSource.Bind<ItemParam>(this.QuestList, itemParam);
      if (this.LastUpadatedItemParam != itemParam)
      {
        this.SetScrollTop();
        this.LastUpadatedItemParam = itemParam;
      }
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) QuestDropParam.Instance, (UnityEngine.Object) null))
        return;
      QuestParam[] availableQuests = MonoSingleton<GameManager>.Instance.Player.AvailableQuests;
      using (List<QuestParam>.Enumerator enumerator = QuestDropParam.Instance.GetItemDropQuestList(itemParam, GlobalVars.GetDropTableGeneratedDateTime()).GetEnumerator())
      {
        while (enumerator.MoveNext())
          this.AddPanel(enumerator.Current, availableQuests);
      }
    }

    private void SetScrollTop()
    {
      RectTransform component = (RectTransform) ((Component) this.QuestListParent).GetComponent<RectTransform>();
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
        return;
      Vector2 anchoredPosition = component.get_anchoredPosition();
      anchoredPosition.y = (__Null) 0.0;
      component.set_anchoredPosition(anchoredPosition);
    }

    public void ClearPanel()
    {
      this.mGainedQuests.Clear();
      for (int index = 0; index < ((Transform) this.QuestListParent).get_childCount(); ++index)
      {
        GameObject gameObject = ((Component) ((Transform) this.QuestListParent).GetChild(index)).get_gameObject();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.QuestListItemTemplate, (UnityEngine.Object) gameObject))
          UnityEngine.Object.Destroy((UnityEngine.Object) gameObject);
      }
    }

    private void AddPanel(QuestParam questparam, QuestParam[] availableQuests)
    {
      this.QuestList.SetActive(true);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.NotFoundGainQuestObject, (UnityEngine.Object) null))
        this.NotFoundGainQuestObject.SetActive(false);
      if (questparam == null || questparam.IsMulti)
        return;
      GameObject gameObject = (GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) this.QuestListItemTemplate);
      SRPG_Button component1 = (SRPG_Button) gameObject.GetComponent<SRPG_Button>();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component1, (UnityEngine.Object) null))
        component1.AddListener(new SRPG_Button.ButtonClickEvent(this.OnQuestSelect));
      this.mGainedQuests.Add(gameObject);
      Button component2 = (Button) gameObject.GetComponent<Button>();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component2, (UnityEngine.Object) null))
      {
        bool flag1 = questparam.IsDateUnlock(-1L);
        bool flag2 = Array.Find<QuestParam>(availableQuests, (Predicate<QuestParam>) (p => p == questparam)) != null;
        ((Selectable) component2).set_interactable(flag1 && flag2);
      }
      DataSource.Bind<QuestParam>(gameObject, questparam);
      gameObject.get_transform().SetParent((Transform) this.QuestListParent, false);
      gameObject.SetActive(true);
    }

    private void OnQuestSelect(SRPG_Button button)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      UnitKakeraWindow.\u003COnQuestSelect\u003Ec__AnonStorey39C selectCAnonStorey39C = new UnitKakeraWindow.\u003COnQuestSelect\u003Ec__AnonStorey39C();
      int index = this.mGainedQuests.IndexOf(((Component) button).get_gameObject());
      // ISSUE: reference to a compiler-generated field
      selectCAnonStorey39C.quest = DataSource.FindDataOfClass<QuestParam>(this.mGainedQuests[index], (QuestParam) null);
      // ISSUE: reference to a compiler-generated field
      if (selectCAnonStorey39C.quest == null)
        return;
      // ISSUE: reference to a compiler-generated field
      if (!selectCAnonStorey39C.quest.IsDateUnlock(-1L))
      {
        UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get("sys.DISABLE_QUEST_DATE_UNLOCK"), (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1);
      }
      else
      {
        // ISSUE: reference to a compiler-generated method
        if (Array.Find<QuestParam>(MonoSingleton<GameManager>.Instance.Player.AvailableQuests, new Predicate<QuestParam>(selectCAnonStorey39C.\u003C\u003Em__461)) == null)
        {
          UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get("sys.DISABLE_QUEST_CHALLENGE"), (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1);
        }
        else
        {
          // ISSUE: reference to a compiler-generated field
          GlobalVars.SelectedQuestID = selectCAnonStorey39C.quest.iname;
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
        }
      }
    }

    private void OnDecideClick()
    {
      if (this.mUsedElemPieces.Count > 0)
      {
        string str = (string) null;
        for (int index = 0; index < this.mUsedElemPieces.Count; ++index)
        {
          if (index > 0)
            str += "、";
          str += this.mUsedElemPieces[index].Param.name;
        }
        UIUtility.ConfirmBox(string.Format(LocalizedText.Get("sys.KAKUSEI_CONFIRM_ELEMENT_KAKERA"), (object) str), new UIUtility.DialogResultEvent(this.OnKakusei), (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1, (string) null, (string) null);
      }
      else
        this.OnKakusei((GameObject) null);
    }

    private void OnKakusei(GameObject go)
    {
      if (this.OnAwakeAccept != null)
        this.OnAwakeAccept((int) this.SelectAwakeSlider.get_value());
      else
        MonoSingleton<GameManager>.Instance.Player.AwakingUnit(this.mCurrentUnit);
    }

    private void OnAdd()
    {
      this.RefreshAwakeLv(1);
    }

    private void OnRemove()
    {
      this.RefreshAwakeLv(-1);
    }

    private void RefreshAwakeLv(int value = 0)
    {
      if (value > 0)
      {
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.SelectAwakeSlider, (UnityEngine.Object) null) || (double) this.SelectAwakeSlider.get_maxValue() < (double) this.SelectAwakeSlider.get_value() + (double) value)
          return;
        Slider selectAwakeSlider = this.SelectAwakeSlider;
        selectAwakeSlider.set_value(selectAwakeSlider.get_value() + (float) value);
      }
      else
      {
        if (value >= 0 || !UnityEngine.Object.op_Inequality((UnityEngine.Object) this.SelectAwakeSlider, (UnityEngine.Object) null) || (double) this.SelectAwakeSlider.get_value() + (double) value < (double) this.SelectAwakeSlider.get_minValue())
          return;
        Slider selectAwakeSlider = this.SelectAwakeSlider;
        selectAwakeSlider.set_value(selectAwakeSlider.get_value() + (float) value);
      }
    }

    public delegate void KakuseiWindowEvent();

    public delegate void AwakeEvent(int value);
  }
}
