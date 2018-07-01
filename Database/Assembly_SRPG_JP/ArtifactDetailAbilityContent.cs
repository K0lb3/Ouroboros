// Decompiled with JetBrains decompiler
// Type: SRPG.ArtifactDetailAbilityContent
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  public class ArtifactDetailAbilityContent : MonoBehaviour
  {
    [SerializeField]
    private GameObject mBaseAbilityObject;
    [SerializeField]
    private GameObject mDeriveAbilityObject;
    [SerializeField]
    private ArtifactDetailAbilityItem mBaseAbilityItem;
    [SerializeField]
    private GameObject mDeriveParent;
    [SerializeField]
    private ArtifactDetailAbilityItem mDeriveTemplate;
    private List<ArtifactDetailAbilityItem> mAbilityItems;

    public ArtifactDetailAbilityContent()
    {
      base.\u002Ector();
    }

    public bool IsExistEnableAbility
    {
      get
      {
        if (this.mBaseAbilityItem.IsEnable)
          return true;
        for (int index = 0; index < this.mAbilityItems.Count; ++index)
        {
          if (this.mAbilityItems[index].IsEnable)
            return true;
        }
        return false;
      }
    }

    public void Init(ViewAbilityData view_ability_data)
    {
      if (view_ability_data == null || view_ability_data.baseAbility == null)
        return;
      bool has_derive_ability = this.CreateDeriveAbilityContents(view_ability_data) > 0;
      this.CreateBaseAbilityContent(view_ability_data, has_derive_ability);
      if (this.mAbilityItems.Count > 0)
        this.mAbilityItems[this.mAbilityItems.Count - 1].DestoryLastLine();
      ((Component) this.mDeriveTemplate).get_gameObject().SetActive(false);
    }

    private void CreateBaseAbilityContent(ViewAbilityData view_ability_data, bool has_derive_ability)
    {
      if (view_ability_data.baseAbility == null)
        return;
      this.BindData(((Component) this.mBaseAbilityItem).get_gameObject(), view_ability_data.baseAbility, view_ability_data.abilityDeriveParam);
      this.mBaseAbilityItem.Setup(view_ability_data.baseAbility, false, view_ability_data.isEnableBaseAbility, view_ability_data.isLockedBaseAbility, has_derive_ability);
    }

    private int CreateDeriveAbilityContents(ViewAbilityData view_data)
    {
      List<ViewDeriveAbilityData> deriveAbilities = view_data.deriveAbilities;
      int num = 0;
      if (deriveAbilities != null)
      {
        for (int index = 0; index < deriveAbilities.Count; ++index)
        {
          if (deriveAbilities[index].is_enable)
          {
            ArtifactDetailAbilityItem detailAbilityItem = (ArtifactDetailAbilityItem) Object.Instantiate<ArtifactDetailAbilityItem>((M0) this.mDeriveTemplate);
            ((Component) detailAbilityItem).get_transform().SetParent(this.mDeriveParent.get_transform(), false);
            this.mAbilityItems.Add(detailAbilityItem);
            this.BindData(((Component) detailAbilityItem).get_gameObject(), deriveAbilities[index].ability, view_data.abilityDeriveParam);
            detailAbilityItem.Setup(deriveAbilities[index].ability, true, deriveAbilities[index].is_enable, false, false);
            ++num;
          }
        }
      }
      if (num <= 0)
        this.mDeriveAbilityObject.SetActive(false);
      return num;
    }

    private void BindData(GameObject target, AbilityParam ability_data, AbilityDeriveParam ability_derive_param)
    {
      DataSource.Bind<AbilityParam>(target, ability_data);
      if (ability_data.condition_units != null)
      {
        for (int index = 0; index < ability_data.condition_units.Length; ++index)
        {
          if (!string.IsNullOrEmpty(ability_data.condition_units[index]))
          {
            UnitParam unitParam = MonoSingleton<GameManager>.Instance.MasterParam.GetUnitParam(ability_data.condition_units[index]);
            if (unitParam != null)
              DataSource.Bind<UnitParam>(target, unitParam);
          }
        }
      }
      if (ability_data.condition_jobs != null)
      {
        for (int index = 0; index < ability_data.condition_jobs.Length; ++index)
        {
          if (!string.IsNullOrEmpty(ability_data.condition_jobs[index]))
          {
            JobParam jobParam = MonoSingleton<GameManager>.Instance.MasterParam.GetJobParam(ability_data.condition_jobs[index]);
            if (jobParam != null)
              DataSource.Bind<JobParam>(target, jobParam);
          }
        }
      }
      if (ability_derive_param == null)
        return;
      DataSource.Bind<AbilityDeriveParam>(target, ability_derive_param);
    }
  }
}
