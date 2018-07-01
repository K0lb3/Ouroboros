// Decompiled with JetBrains decompiler
// Type: SRPG.ConceptCardBonusContentLvMax
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  public class ConceptCardBonusContentLvMax : MonoBehaviour
  {
    [SerializeField]
    private GameObject mParamTemplate;
    [SerializeField]
    private ImageArray mAwakeIconImageArray;
    [SerializeField]
    private ImageArray mAwakeIconBgArray;
    [SerializeField]
    private StatusList mParamStatusList;
    [SerializeField]
    private StatusList mBonusStatusList;
    private int mCreatedCount;

    public ConceptCardBonusContentLvMax()
    {
      base.\u002Ector();
    }

    public void Setup(ConceptCardEffectsParam[] effect_params, int lv, int lv_cap, int awake_count_cap, bool is_enable, ConceptCardBonusWindow.eViewType view_type)
    {
      switch (view_type)
      {
        case ConceptCardBonusWindow.eViewType.CARD_SKILL:
          this.CreateCardSkillBonus(effect_params, lv, lv_cap, awake_count_cap, is_enable);
          break;
        case ConceptCardBonusWindow.eViewType.ABILITY:
          this.CreateAbilityBonus(effect_params, lv, lv_cap, awake_count_cap, is_enable);
          break;
      }
      ((Component) this).get_gameObject().SetActive(this.mCreatedCount > 0);
    }

    private void CreateCardSkillBonus(ConceptCardEffectsParam[] effect_params, int lv, int lv_cap, int awake_count_cap, bool is_enable)
    {
      if (Object.op_Equality((Object) this.mParamTemplate, (Object) null))
        return;
      Transform parent = this.mParamTemplate.get_transform().get_parent();
      List<string> stringList = new List<string>();
      for (int index1 = 0; index1 < effect_params.Length; ++index1)
      {
        if (!string.IsNullOrEmpty(effect_params[index1].card_skill))
        {
          SkillParam skillParam = MonoSingleton<GameManager>.Instance.MasterParam.GetSkillParam(effect_params[index1].card_skill);
          if (skillParam != null && !string.IsNullOrEmpty(effect_params[index1].add_card_skill_buff_lvmax) && !stringList.Contains(skillParam.iname))
          {
            BaseStatus status = new BaseStatus();
            BaseStatus scale_status = new BaseStatus();
            SkillData skill = new SkillData();
            skill.Setup(skillParam.iname, lv, lv_cap, (MasterParam) null);
            SkillData.GetPassiveBuffStatus(skill, (BuffEffect[]) null, EElement.None, ref status, ref scale_status);
            BaseStatus total_add = new BaseStatus();
            BaseStatus total_scale = new BaseStatus();
            effect_params[index1].GetAddCardSkillBuffStatusLvMax(lv, lv_cap, awake_count_cap, ref total_add, ref total_scale);
            string str1 = !Object.op_Inequality((Object) this.mParamStatusList, (Object) null) ? string.Empty : ((Object) this.mParamStatusList).get_name();
            string str2 = !Object.op_Inequality((Object) this.mBonusStatusList, (Object) null) ? string.Empty : ((Object) this.mBonusStatusList).get_name();
            GameObject root = (GameObject) Object.Instantiate<GameObject>((M0) this.mParamTemplate);
            root.get_transform().SetParent(parent, false);
            StatusList[] componentsInChildren = (StatusList[]) root.GetComponentsInChildren<StatusList>();
            for (int index2 = 0; index2 < componentsInChildren.Length; ++index2)
            {
              if (((Object) componentsInChildren[index2]).get_name().StartsWith(str1))
                componentsInChildren[index2].SetValues_Restrict(status, scale_status, total_add, total_scale, false);
              else if (((Object) componentsInChildren[index2]).get_name().StartsWith(str2))
                componentsInChildren[index2].SetValues_Restrict(status, scale_status, total_add, total_scale, true);
            }
            if (Object.op_Inequality((Object) this.mAwakeIconImageArray, (Object) null))
              this.mAwakeIconImageArray.ImageIndex = this.mAwakeIconImageArray.Images.Length - 1;
            DataSource.Bind<SkillParam>(root, skillParam);
            DataSource.Bind<bool>(((Component) this).get_gameObject(), is_enable);
            GameParameter.UpdateAll(root);
            stringList.Add(skillParam.iname);
            ++this.mCreatedCount;
          }
        }
      }
      if (Object.op_Inequality((Object) this.mAwakeIconBgArray, (Object) null))
        this.mAwakeIconBgArray.ImageIndex = !is_enable ? 1 : 0;
      this.mParamTemplate.SetActive(false);
    }

    private void CreateAbilityBonus(ConceptCardEffectsParam[] effect_params, int lv, int lv_cap, int awake_count_cap, bool is_enable)
    {
      if (Object.op_Equality((Object) this.mParamTemplate, (Object) null))
        return;
      Transform parent = this.mParamTemplate.get_transform().get_parent();
      List<string> stringList = new List<string>();
      for (int index1 = 0; index1 < effect_params.Length; ++index1)
      {
        if (!string.IsNullOrEmpty(effect_params[index1].abil_iname) && !string.IsNullOrEmpty(effect_params[index1].abil_iname_lvmax))
        {
          AbilityParam abilityParam = MonoSingleton<GameManager>.Instance.MasterParam.GetAbilityParam(effect_params[index1].abil_iname_lvmax);
          if (!stringList.Contains(abilityParam.iname))
          {
            for (int index2 = 0; index2 < abilityParam.skills.Length; ++index2)
            {
              SkillParam skillParam = MonoSingleton<GameManager>.Instance.MasterParam.GetSkillParam(abilityParam.skills[index2].iname);
              if (skillParam != null)
              {
                GameObject root = (GameObject) Object.Instantiate<GameObject>((M0) this.mParamTemplate);
                root.get_transform().SetParent(parent, false);
                if (Object.op_Inequality((Object) this.mAwakeIconImageArray, (Object) null))
                  this.mAwakeIconImageArray.ImageIndex = this.mAwakeIconImageArray.Images.Length - 1;
                DataSource.Bind<SkillParam>(root, skillParam);
                DataSource.Bind<bool>(((Component) this).get_gameObject(), is_enable);
                GameParameter.UpdateAll(root);
                ++this.mCreatedCount;
              }
            }
            stringList.Add(abilityParam.iname);
          }
        }
      }
      if (Object.op_Inequality((Object) this.mAwakeIconBgArray, (Object) null))
        this.mAwakeIconBgArray.ImageIndex = !is_enable ? 1 : 0;
      this.mParamTemplate.SetActive(false);
    }
  }
}
