// Decompiled with JetBrains decompiler
// Type: SRPG.ConceptCardDetailAbility
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System.Text;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(10, "データ設定完了", FlowNode.PinTypes.Output, 10)]
  public class ConceptCardDetailAbility : ConceptCardDetailBase, IFlowInterface
  {
    private const int PIN_ABILITY_DETAIL = 0;
    private const int PIN_ABILITY_DETAIL_END = 10;
    [SerializeField]
    private GameObject mCardAbilityObject;
    [SerializeField]
    private GameObject mCardSkillObject;
    [SerializeField]
    private GameObject mCardAbilityNameObject;
    [SerializeField]
    private GameObject mCardSkillNameObject;
    [SerializeField]
    private GameObject mCardUniqueUnitObject;
    [SerializeField]
    private GameObject mCardUniqueJobObject;
    [SerializeField]
    private GameObject mCardCommonUnitObject;
    [SerializeField]
    private GameObject mCardCommonJobObject;
    [SerializeField]
    private UnityEngine.UI.Text mCardUseUnitText;
    [SerializeField]
    private UnityEngine.UI.Text mCardUseJobText;
    [SerializeField]
    private Image_Transparent mCardUseUnitImage;
    [SerializeField]
    private RawImage_Transparent mCardUseJobImage;
    [SerializeField]
    private UnityEngine.UI.Text mCardAbilityDescription;
    [SerializeField]
    private StatusList mCardSkillStatusList;
    [SerializeField]
    private GameObject mLock;
    private ConceptCardSkillDatailData mShowData;
    private ConceptCardDetailAbility.ClickDetail mClickDetail;

    public void Activated(int pinID)
    {
    }

    public void OnClickDetail()
    {
      this.mClickDetail(this.mShowData);
    }

    public void SetCardSkill(ConceptCardSkillDatailData data)
    {
      this.SwitchObject(true, this.mCardSkillObject, this.mCardAbilityObject);
      this.SwitchObject(data.type == ConceptCardDetailAbility.ShowType.Skill, this.mCardSkillNameObject, this.mCardAbilityNameObject);
      if (data.skill_data == null)
      {
        this.SetText(this.mCardAbilityDescription, string.Empty);
      }
      else
      {
        DataSource.Bind<AbilityData>(((Component) this).get_gameObject(), data.effect == null || data.effect.Ability == null ? (AbilityData) null : data.effect.Ability);
        DataSource.Bind<SkillData>(((Component) this).get_gameObject(), data.skill_data);
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append(data.skill_data.SkillParam.expr);
        this.SetText(this.mCardAbilityDescription, stringBuilder.ToString());
        if (Object.op_Inequality((Object) this.mCardSkillStatusList, (Object) null))
          ((Component) this.mCardSkillStatusList).get_gameObject().SetActive(false);
        if (data.skill_data.Condition == ESkillCondition.CardSkill && Object.op_Inequality((Object) this.mCardSkillStatusList, (Object) null) && data.skill_data != null)
        {
          ((Component) this.mCardSkillStatusList).get_gameObject().SetActive(true);
          BaseStatus status = new BaseStatus();
          BaseStatus scale_status = new BaseStatus();
          BaseStatus baseStatus1 = new BaseStatus();
          BaseStatus baseStatus2 = new BaseStatus();
          SkillData.GetPassiveBuffStatus(data.skill_data, (BuffEffect[]) null, EElement.None, ref status, ref scale_status);
          if (data.effect != null && data.effect.AddCardSkillBuffEffectAwake != null)
          {
            BaseStatus total_add = new BaseStatus();
            BaseStatus total_scale = new BaseStatus();
            data.effect.AddCardSkillBuffEffectAwake.GetBaseStatus(ref total_add, ref total_scale);
            baseStatus1.Add(total_add);
            baseStatus2.Add(total_scale);
          }
          if (data.effect != null && data.effect.AddCardSkillBuffEffectLvMax != null)
          {
            BaseStatus total_add = new BaseStatus();
            BaseStatus total_scale = new BaseStatus();
            data.effect.AddCardSkillBuffEffectLvMax.GetBaseStatus(ref total_add, ref total_scale);
            baseStatus1.Add(total_add);
            baseStatus2.Add(total_scale);
          }
          if (ConceptCardDescription.EnhanceInfo == null)
          {
            this.mCardSkillStatusList.SetValues_TotalAndBonus(status, scale_status, status, scale_status, baseStatus1, baseStatus2, baseStatus1, baseStatus2);
          }
          else
          {
            int lvCap = (int) ConceptCardDescription.EnhanceInfo.Target.LvCap;
            int predictLv = ConceptCardDescription.EnhanceInfo.predictLv;
            int predictAwake = ConceptCardDescription.EnhanceInfo.predictAwake;
            int awakeCountCap = ConceptCardDescription.EnhanceInfo.Target.AwakeCountCap;
            BaseStatus add = new BaseStatus();
            BaseStatus scale = new BaseStatus();
            ConceptCardParam.GetSkillAllStatus(data.skill_data.SkillID, lvCap, predictLv, ref add, ref scale);
            BaseStatus total_add1 = new BaseStatus();
            BaseStatus total_scale1 = new BaseStatus();
            data.effect.GetAddCardSkillBuffStatusAwake(predictAwake, awakeCountCap, ref total_add1, ref total_scale1);
            BaseStatus total_add2 = new BaseStatus();
            BaseStatus total_scale2 = new BaseStatus();
            data.effect.GetAddCardSkillBuffStatusLvMax(predictLv, lvCap, predictAwake, ref total_add2, ref total_scale2);
            BaseStatus modBonusAdd = new BaseStatus();
            BaseStatus modBonusMul = new BaseStatus();
            modBonusAdd.Add(total_add1);
            modBonusAdd.Add(total_add2);
            modBonusMul.Add(total_scale1);
            modBonusMul.Add(total_scale2);
            this.mCardSkillStatusList.SetValues_TotalAndBonus(status, scale_status, add, scale, baseStatus1, baseStatus2, modBonusAdd, modBonusMul);
          }
        }
        if (Object.op_Inequality((Object) this.mLock, (Object) null))
          this.mLock.SetActive(data.type == ConceptCardDetailAbility.ShowType.LockSkill);
        this.mShowData = data;
        GameParameter.UpdateAll(((Component) this).get_gameObject());
      }
    }

    public void SetUnitIcon(Image_Transparent image, UnitParam unit_param)
    {
      if (Object.op_Equality((Object) image, (Object) null) || unit_param == null)
        return;
      SpriteSheet spriteSheet = AssetManager.Load<SpriteSheet>("ItemIcon/small");
      ItemParam itemParam = MonoSingleton<GameManager>.Instance.GetItemParam(unit_param.piece);
      image.set_sprite(spriteSheet.GetSprite(itemParam.icon));
    }

    public void SetAbilityDetailParent(ConceptCardDetailAbility.ClickDetail detail)
    {
      this.mClickDetail = detail;
    }

    public enum ShowType
    {
      None,
      Skill,
      Ability,
      LockSkill,
    }

    public delegate void ClickDetail(ConceptCardSkillDatailData data);
  }
}
