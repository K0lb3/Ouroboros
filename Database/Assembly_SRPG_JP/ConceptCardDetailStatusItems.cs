// Decompiled with JetBrains decompiler
// Type: SRPG.ConceptCardDetailStatusItems
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class ConceptCardDetailStatusItems : MonoBehaviour
  {
    [SerializeField]
    private Text mGroupNameField;
    [SerializeField]
    private StatusList mStatusList;
    private ConceptCardData mConceptCardData;
    private ConceptCardEffectsParam mConceptCardEffectsParam;
    private int mAddExp;
    private int mAddAwakeLv;
    private bool mIsEnhance;
    private bool mIsBaseParam;

    public ConceptCardDetailStatusItems()
    {
      base.\u002Ector();
    }

    public void SetParam(ConceptCardData conceptCardData, ConceptCardEffectsParam conceptCardEffectsParam, int addExp, int addAwakeLv, bool isEnhance, bool isBaseParam)
    {
      this.mConceptCardData = conceptCardData;
      this.mConceptCardEffectsParam = conceptCardEffectsParam;
      this.mAddExp = addExp;
      this.mAddAwakeLv = addAwakeLv;
      this.mIsEnhance = isEnhance;
      this.mIsBaseParam = isBaseParam;
    }

    public void Refresh()
    {
      if (this.mConceptCardData == null)
        return;
      this.RefreshEquipTarget(this.mConceptCardEffectsParam);
      if (!this.RefreshEquipParam(this.mConceptCardData, this.mConceptCardEffectsParam) || this.mIsBaseParam)
        return;
      ((Component) this).get_gameObject().SetActive(false);
    }

    private void RefreshEquipTarget(ConceptCardEffectsParam equipParam)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mGroupNameField, (UnityEngine.Object) null))
        return;
      this.mGroupNameField.set_text(string.Empty);
      if (this.mIsBaseParam)
      {
        this.mGroupNameField.set_text(LocalizedText.Get("sys.CONCEPT_CARD_STATUS_DEFAULT_TITLE"));
      }
      else
      {
        if (equipParam == null || string.IsNullOrEmpty(equipParam.statusup_skill))
          return;
        SkillParam skillParam = MonoSingleton<GameManager>.Instance.MasterParam.GetSkillParam(equipParam.statusup_skill);
        if (skillParam == null)
          return;
        this.mGroupNameField.set_text(skillParam.expr + LocalizedText.Get("sys.CONCEPT_CARD_STATUS_ADDPARAM_TITLE"));
      }
    }

    private bool RefreshEquipParam(ConceptCardData conceptCardData, ConceptCardEffectsParam equipParam)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mStatusList, (UnityEngine.Object) null))
        return false;
      if (equipParam == null)
      {
        this.mStatusList.SetValues(new BaseStatus(), new BaseStatus(), false);
        return false;
      }
      BaseStatus add1 = new BaseStatus();
      BaseStatus add2 = new BaseStatus();
      BaseStatus scale1 = new BaseStatus();
      BaseStatus scale2 = new BaseStatus();
      ConceptCardParam.GetSkillStatus(equipParam.statusup_skill, (int) conceptCardData.LvCap, (int) conceptCardData.Lv, ref add1, ref scale1);
      if (this.mIsEnhance)
      {
        int levelCap = Mathf.Min((int) conceptCardData.LvCap, (int) conceptCardData.CurrentLvCap + this.mAddAwakeLv);
        int totalExp = (int) conceptCardData.Exp + this.mAddExp;
        int lv = MonoSingleton<GameManager>.Instance.MasterParam.CalcConceptCardLevel((int) conceptCardData.Rarity, totalExp, levelCap);
        ConceptCardParam.GetSkillStatus(equipParam.statusup_skill, (int) conceptCardData.LvCap, lv, ref add2, ref scale2);
        this.mStatusList.SetValuesAfterOnly(add1, scale1, add2, scale2, false, true);
      }
      else
        this.mStatusList.SetValues(add1, scale1, false);
      return this.IsValueEmpty(add1, scale1);
    }

    public bool IsValueEmpty(BaseStatus paramAdd, BaseStatus paramMul)
    {
      Array values = Enum.GetValues(typeof (ParamTypes));
      for (int index = 0; index < values.Length; ++index)
      {
        if ((int) paramAdd[(ParamTypes) values.GetValue(index)] != 0 && index != 2 || (int) paramMul[(ParamTypes) values.GetValue(index)] != 0 && index != 2)
          return false;
      }
      return true;
    }
  }
}
