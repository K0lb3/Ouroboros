// Decompiled with JetBrains decompiler
// Type: UnitVoice
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using SRPG;
using System;
using UnityEngine;

[AddComponentMenu("Audio/UnitVoice")]
public class UnitVoice : MonoBehaviour
{
  public UnitVoice.ECharType CharType;
  public string DirectCharName;
  public string CueName;
  public bool PlayOnAwake;
  public UnitVoice.eCollaboType CollaboType;
  private bool mPlayAutomatic;
  private string mCharName;
  private string mCueName;
  private MySound.Voice mVoice;

  public UnitVoice()
  {
    base.\u002Ector();
  }

  private void OnEnable()
  {
    this.mPlayAutomatic = this.PlayOnAwake;
    string directName = (string) null;
    string sheetName = (string) null;
    string cueName = (string) null;
    this.GetDefaultCharName(ref directName, ref sheetName, ref cueName);
    this.SetCharName(directName, sheetName, cueName);
  }

  private void OnDisable()
  {
    this.mPlayAutomatic = false;
  }

  private void OnDestroy()
  {
    this.Discard();
  }

  private void Update()
  {
    if (!this.mPlayAutomatic)
      return;
    this.Play();
    this.mPlayAutomatic = false;
  }

  public void SearchUnitSkinVoiceName(ref string sheetName, ref string cueName)
  {
    UnitParam dataOfClass1 = DataSource.FindDataOfClass<UnitParam>(((Component) this).get_gameObject(), (UnitParam) null);
    if (dataOfClass1 != null)
    {
      sheetName = dataOfClass1.voice;
      cueName = dataOfClass1.voice;
      if (!string.IsNullOrEmpty(sheetName) && !string.IsNullOrEmpty(cueName))
        return;
    }
    Unit dataOfClass2 = DataSource.FindDataOfClass<Unit>(((Component) this).get_gameObject(), (Unit) null);
    if (dataOfClass2 != null)
    {
      sheetName = dataOfClass2.GetUnitSkinVoiceSheetName(-1);
      cueName = dataOfClass2.GetUnitSkinVoiceCueName(-1);
      if (!string.IsNullOrEmpty(sheetName) && !string.IsNullOrEmpty(cueName))
        return;
    }
    UnitData dataOfClass3 = DataSource.FindDataOfClass<UnitData>(((Component) this).get_gameObject(), (UnitData) null);
    if (dataOfClass3 != null)
    {
      sheetName = dataOfClass3.GetUnitSkinVoiceSheetName(-1);
      cueName = dataOfClass3.GetUnitSkinVoiceCueName(-1);
      if (!string.IsNullOrEmpty(sheetName) && !string.IsNullOrEmpty(cueName))
        return;
    }
    SceneBattle instance = SceneBattle.Instance;
    BattleCore battleCore = !Object.op_Equality((Object) instance, (Object) null) ? instance.Battle : (BattleCore) null;
    Unit unit = battleCore != null ? battleCore.CurrentUnit : (Unit) null;
    if (unit != null)
    {
      sheetName = unit.GetUnitSkinVoiceSheetName(-1);
      cueName = unit.GetUnitSkinVoiceCueName(-1);
      if (!string.IsNullOrEmpty(sheetName) && !string.IsNullOrEmpty(cueName))
        return;
    }
    if (GameUtility.GetCurrentScene() != GameUtility.EScene.HOME_MULTI)
    {
      UnitData unitDataByUniqueId = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID((long) GlobalVars.SelectedUnitUniqueID);
      if (unitDataByUniqueId == null)
        return;
      sheetName = unitDataByUniqueId.GetUnitSkinVoiceSheetName(-1);
      cueName = unitDataByUniqueId.GetUnitSkinVoiceCueName(-1);
      if (string.IsNullOrEmpty(sheetName) || string.IsNullOrEmpty(cueName))
        ;
    }
    else
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      UnitVoice.\u003CSearchUnitSkinVoiceName\u003Ec__AnonStorey208 nameCAnonStorey208 = new UnitVoice.\u003CSearchUnitSkinVoiceName\u003Ec__AnonStorey208();
      JSON_MyPhotonPlayerParam multiPlayerParam = GlobalVars.SelectedMultiPlayerParam;
      // ISSUE: reference to a compiler-generated field
      nameCAnonStorey208.slotID = !Object.op_Equality((Object) PartyUnitSlot.Active, (Object) null) ? PartyUnitSlot.Active.Index : -1;
      // ISSUE: reference to a compiler-generated field
      if (multiPlayerParam == null || multiPlayerParam.units == null || nameCAnonStorey208.slotID < 0)
        return;
      // ISSUE: reference to a compiler-generated method
      JSON_MyPhotonPlayerParam.UnitDataElem unitDataElem = Array.Find<JSON_MyPhotonPlayerParam.UnitDataElem>(multiPlayerParam.units, new Predicate<JSON_MyPhotonPlayerParam.UnitDataElem>(nameCAnonStorey208.\u003C\u003Em__1DF));
      UnitData unitData = unitDataElem != null ? unitDataElem.unit : (UnitData) null;
      if (unitData == null)
        return;
      sheetName = unitData.GetUnitSkinVoiceSheetName(-1);
      cueName = unitData.GetUnitSkinVoiceCueName(-1);
      if (string.IsNullOrEmpty(sheetName) || string.IsNullOrEmpty(cueName))
        ;
    }
  }

  public void GetDefaultCharName(ref string directName, ref string sheetName, ref string cueName)
  {
    directName = this.DirectCharName;
    if (this.CharType != UnitVoice.ECharType.AUTO)
      return;
    this.SearchUnitSkinVoiceName(ref sheetName, ref cueName);
  }

  public bool SetCharName(string directName, string sheetName, string cueName)
  {
    if (this.CharType == UnitVoice.ECharType.BATTLE_SKILL)
    {
      SceneBattle instance = SceneBattle.Instance;
      BattleCore battleCore = !Object.op_Equality((Object) instance, (Object) null) ? instance.Battle : (BattleCore) null;
      Unit unit = battleCore != null ? battleCore.CurrentUnit : (Unit) null;
      if (unit != null)
      {
        sheetName = unit.GetUnitSkinVoiceSheetName(-1);
        cueName = unit.GetUnitSkinVoiceCueName(-1);
        if (string.IsNullOrEmpty(sheetName) || this.mVoice != null && cueName.Equals(this.mCharName))
          return false;
        this.mCharName = cueName;
        this.mVoice = new MySound.Voice("VO_" + sheetName, sheetName, cueName + "_");
        this.SetupCueName();
        return true;
      }
      this.mVoice = (MySound.Voice) null;
      this.mCharName = (string) null;
      return false;
    }
    if (this.CharType == UnitVoice.ECharType.BATTLE_SKILL_COLLABO)
    {
      this.mVoice = (MySound.Voice) null;
      this.mCharName = (string) null;
      SceneBattle instance = SceneBattle.Instance;
      Unit unit = instance.CollaboMainUnit;
      if (this.CollaboType == UnitVoice.eCollaboType.SUB)
        unit = instance.CollaboSubUnit;
      if (unit == null)
        return false;
      sheetName = unit.GetUnitSkinVoiceSheetName(-1);
      cueName = unit.GetUnitSkinVoiceCueName(-1);
      if (string.IsNullOrEmpty(sheetName) || this.mVoice != null && cueName.Equals(this.mCharName))
        return false;
      this.mCharName = cueName;
      this.mVoice = new MySound.Voice("VO_" + sheetName, sheetName, cueName + "_");
      this.SetupCueName();
      return true;
    }
    if (!string.IsNullOrEmpty(sheetName) && !string.IsNullOrEmpty(cueName))
    {
      if (this.mVoice != null && cueName.Equals(this.mCharName))
        return false;
      this.mCharName = cueName;
      this.mVoice = new MySound.Voice("VO_" + sheetName, sheetName, cueName + "_");
      this.SetupCueName();
      return true;
    }
    if (!string.IsNullOrEmpty(directName))
    {
      if (this.mVoice != null && directName.Equals(this.mCharName))
        return false;
      this.mCharName = directName;
      this.mVoice = new MySound.Voice(this.mCharName);
      this.SetupCueName();
      return true;
    }
    this.mVoice = (MySound.Voice) null;
    this.mCharName = (string) null;
    return false;
  }

  public bool SetupCueName()
  {
    if (string.IsNullOrEmpty(this.CueName) || string.IsNullOrEmpty(this.mCharName))
      return false;
    this.mCueName = MySound.Voice.ReplaceCharNameOfCueName(this.CueName, this.mCharName);
    return true;
  }

  public void Play()
  {
    if (this.mVoice == null)
      return;
    this.mVoice.PlayDirect(this.mCueName, 0.0f);
  }

  public void Discard()
  {
    if (this.mVoice != null)
      this.mVoice.Cleanup();
    this.mVoice = (MySound.Voice) null;
    this.mCharName = (string) null;
  }

  public enum ECharType
  {
    AUTO,
    DIRECT_CHAR_NAME,
    BATTLE_SKILL,
    BATTLE_SKILL_COLLABO,
  }

  public enum eCollaboType
  {
    MAIN,
    SUB,
  }
}
