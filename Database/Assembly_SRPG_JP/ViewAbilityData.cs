// Decompiled with JetBrains decompiler
// Type: SRPG.ViewAbilityData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System.Collections.Generic;

namespace SRPG
{
  public class ViewAbilityData
  {
    private AbilityParam base_ability;
    private AbilityDeriveParam ability_derive_param;
    private bool is_enable_base_ability;
    private bool is_locked_base_ability;
    private List<ViewDeriveAbilityData> derive_abilities;

    public AbilityParam baseAbility
    {
      get
      {
        return this.base_ability;
      }
    }

    public AbilityDeriveParam abilityDeriveParam
    {
      get
      {
        return this.ability_derive_param;
      }
    }

    public bool isEnableBaseAbility
    {
      get
      {
        return this.is_enable_base_ability;
      }
    }

    public bool isLockedBaseAbility
    {
      get
      {
        return this.is_locked_base_ability;
      }
    }

    public List<ViewDeriveAbilityData> deriveAbilities
    {
      get
      {
        return this.derive_abilities;
      }
    }

    public void AddAbility(AbilityParam ability_param, bool is_enable, bool is_locked)
    {
      this.base_ability = ability_param;
      this.is_enable_base_ability = is_enable;
      this.is_locked_base_ability = is_locked;
    }

    public void AddAbility(string base_ability_iname, AbilityDeriveParam derive_param, string derive_ability_iname, bool is_enable)
    {
      if (this.derive_abilities == null)
        this.derive_abilities = new List<ViewDeriveAbilityData>();
      this.base_ability = MonoSingleton<GameManager>.Instance.MasterParam.GetAbilityParam(base_ability_iname);
      if (is_enable)
        this.is_enable_base_ability = false;
      this.derive_abilities.Add(new ViewDeriveAbilityData()
      {
        ability = MonoSingleton<GameManager>.Instance.MasterParam.GetAbilityParam(derive_ability_iname),
        is_enable = is_enable
      });
      this.ability_derive_param = derive_param;
    }
  }
}
