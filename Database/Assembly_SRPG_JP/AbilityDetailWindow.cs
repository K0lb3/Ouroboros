// Decompiled with JetBrains decompiler
// Type: SRPG.AbilityDetailWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(1, "変更前のスキルを表示", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(2, "変更前のスキルを非表示", FlowNode.PinTypes.Input, 2)]
  public class AbilityDetailWindow : MonoBehaviour, IFlowInterface
  {
    public const int INPUT_ENABLE_BASE_SKILL = 1;
    public const int INPUT_DIABLE_BASE_SKILL = 2;
    private static UnitData BindUnit;
    private static AbilityParam BindAbility;
    public Transform SkillLayoutParent;
    public GameObject SkillTemplate;
    public GameObject SkillLockedTemplate;
    [StringIsGameObjectID]
    public string UnlockCondTextId;
    public GameObject SkillUnlockCondWindow;
    [SerializeField]
    private Selectable m_ShowBaseToggle;
    public static bool IsEnableSkillChange;
    private List<SkillDeriveList> m_SkillDeriveList;

    public AbilityDetailWindow()
    {
      base.\u002Ector();
    }

    public static void SetBindObject(UnitData udata)
    {
      AbilityDetailWindow.BindUnit = udata;
      AbilityDetailWindow.BindAbility = (AbilityParam) null;
    }

    public static void SetBindAbility(AbilityParam ability)
    {
      AbilityDetailWindow.BindUnit = (UnitData) null;
      AbilityDetailWindow.BindAbility = ability;
    }

    private void Awake()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.SkillTemplate, (UnityEngine.Object) null))
        this.SkillTemplate.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.SkillLockedTemplate, (UnityEngine.Object) null))
        this.SkillLockedTemplate.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.SkillUnlockCondWindow, (UnityEngine.Object) null))
        this.SkillUnlockCondWindow.SetActive(false);
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_ShowBaseToggle, (UnityEngine.Object) null))
        return;
      this.m_ShowBaseToggle.set_interactable(false);
    }

    private void OnEnable()
    {
      this.Refresh();
    }

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 1:
          this.SwitchBaseSkillEnable(true);
          break;
        case 2:
          this.SwitchBaseSkillEnable(false);
          break;
      }
    }

    private void SwitchBaseSkillEnable(bool enable)
    {
      using (List<SkillDeriveList>.Enumerator enumerator = this.m_SkillDeriveList.GetEnumerator())
      {
        while (enumerator.MoveNext())
          enumerator.Current.SwitchBaseSkillEnable(enable);
      }
    }

    private void Refresh()
    {
      UnitData data1 = (UnitData) null;
      if (UnityEngine.Object.op_Implicit((UnityEngine.Object) UnitEnhanceV3.Instance))
        data1 = UnitEnhanceV3.Instance.CurrentUnit;
      if (data1 == null)
        data1 = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID((long) GlobalVars.SelectedUnitUniqueID);
      if (data1 == null && AbilityDetailWindow.BindUnit != null)
        data1 = AbilityDetailWindow.BindUnit;
      if (data1 == null && AbilityDetailWindow.BindAbility == null)
      {
        DebugUtility.LogError("Not found bind unit data.");
      }
      else
      {
        if (AbilityDetailWindow.BindAbility != null)
        {
          DataSource.Bind<AbilityParam>(((Component) this).get_gameObject(), AbilityDetailWindow.BindAbility);
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.SkillTemplate, (UnityEngine.Object) null))
          {
            List<AbilityDetailWindow.ViewContentParam> viewContentParams = new List<AbilityDetailWindow.ViewContentParam>();
            if (AbilityDetailWindow.BindAbility.skills != null)
            {
              for (int index = 0; index < AbilityDetailWindow.BindAbility.skills.Length; ++index)
              {
                SkillParam skillParam = MonoSingleton<GameManager>.Instance.GetSkillParam(AbilityDetailWindow.BindAbility.skills[index].iname);
                AbilityDetailWindow.AddSkillParam(viewContentParams, skillParam, (SkillDeriveParam) null);
              }
            }
            for (int index = 0; index < viewContentParams.Count; ++index)
            {
              GameObject gameObject = (GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) this.SkillTemplate);
              SkillDeriveList componentInChildren = (SkillDeriveList) gameObject.GetComponentInChildren<SkillDeriveList>();
              componentInChildren.Setup(viewContentParams[index].m_SkillParam, viewContentParams[index].m_SkillDeriveParams);
              gameObject.get_transform().SetParent(this.SkillLayoutParent, false);
              gameObject.SetActive(true);
              this.m_SkillDeriveList.Add(componentInChildren);
            }
          }
        }
        else
        {
          AbilityData data2 = data1.GetAbilityData((long) GlobalVars.SelectedAbilityUniqueID);
          AbilityParam abilityParam = MonoSingleton<GameManager>.Instance.GetAbilityParam((string) GlobalVars.SelectedAbilityID);
          QuestClearUnlockUnitDataParam[] unlockedSkills = data1.UnlockedSkills;
          List<QuestClearUnlockUnitDataParam> unlockUnitDataParamList = new List<QuestClearUnlockUnitDataParam>();
          QuestClearUnlockUnitDataParam[] allUnlockUnitDatas = MonoSingleton<GameManager>.Instance.MasterParam.GetAllUnlockUnitDatas();
          if (allUnlockUnitDatas != null)
          {
            for (int index = 0; index < allUnlockUnitDatas.Length; ++index)
            {
              QuestClearUnlockUnitDataParam param = allUnlockUnitDatas[index];
              if (param.type == QuestClearUnlockUnitDataParam.EUnlockTypes.Skill && param.uid == data1.UnitID && param.parent_id == abilityParam.iname && (unlockedSkills == null || Array.FindIndex<QuestClearUnlockUnitDataParam>(unlockedSkills, (Predicate<QuestClearUnlockUnitDataParam>) (p => p.iname == param.iname)) == -1))
                unlockUnitDataParamList.Add(param);
            }
          }
          if (data2 != null && data2.Param != null && data2.IsDeriveBaseAbility)
            data2 = data2.DerivedAbility;
          RarityParam rarityParam = MonoSingleton<GameManager>.Instance.GetRarityParam((int) data1.UnitParam.raremax);
          int num = Math.Min((int) rarityParam.UnitLvCap + (int) rarityParam.UnitAwakeLvCap, abilityParam.GetRankCap());
          DataSource.Bind<UnitData>(((Component) this).get_gameObject(), data1);
          DataSource.Bind<AbilityData>(((Component) this).get_gameObject(), data2);
          AbilityParam data3 = abilityParam;
          if (AbilityDetailWindow.IsEnableSkillChange)
          {
            string key = data1.SearchAbilityReplacementSkill(abilityParam.iname);
            if (!string.IsNullOrEmpty(key))
              data3 = MonoSingleton<GameManager>.Instance.GetAbilityParam(key);
          }
          DataSource.Bind<AbilityParam>(((Component) this).get_gameObject(), data3);
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.SkillTemplate, (UnityEngine.Object) null))
          {
            List<AbilityDetailWindow.ViewContentParam> viewContentParams = new List<AbilityDetailWindow.ViewContentParam>();
            if (data2 != null && data2.LearningSkills != null)
            {
              for (int index1 = 0; index1 < data2.LearningSkills.Length; ++index1)
              {
                if (data2.LearningSkills[index1].locklv <= num)
                {
                  string str1 = data2.LearningSkills[index1].iname;
                  if (unlockedSkills != null)
                  {
                    for (int index2 = 0; index2 < unlockedSkills.Length; ++index2)
                    {
                      if (unlockedSkills[index2].old_id == str1)
                      {
                        str1 = unlockedSkills[index2].new_id;
                        break;
                      }
                    }
                  }
                  if (AbilityDetailWindow.IsEnableSkillChange)
                  {
                    string str2 = data1.SearchReplacementSkill(str1);
                    if (!string.IsNullOrEmpty(str2))
                      str1 = str2;
                  }
                  SkillData[] deriveSkills = data2.FindDeriveSkills(str1);
                  if (deriveSkills.Length > 0)
                  {
                    foreach (SkillData skillData in deriveSkills)
                      AbilityDetailWindow.AddSkillParam(viewContentParams, skillData.SkillParam, skillData.DeriveParam);
                  }
                  else
                    AbilityDetailWindow.AddSkillParam(viewContentParams, MonoSingleton<GameManager>.Instance.GetSkillParam(str1), (SkillDeriveParam) null);
                }
              }
              if (unlockedSkills != null)
              {
                for (int index = 0; index < unlockedSkills.Length; ++index)
                {
                  if (unlockedSkills[index].add && unlockedSkills[index].parent_id == data2.AbilityID)
                    AbilityDetailWindow.AddSkillParam(viewContentParams, MonoSingleton<GameManager>.Instance.GetSkillParam(unlockedSkills[index].new_id), (SkillDeriveParam) null);
                }
              }
            }
            else
            {
              for (int index = 0; index < abilityParam.skills.Length; ++index)
              {
                if (abilityParam.skills[index].locklv <= num)
                  AbilityDetailWindow.AddSkillParam(viewContentParams, MonoSingleton<GameManager>.Instance.GetSkillParam(abilityParam.skills[index].iname), (SkillDeriveParam) null);
              }
            }
            if (data1.MasterAbility != null && data1.CollaboAbility != null)
            {
              AbilityData abilityData = data1.MasterAbility;
              if (data1.MasterAbility.IsDeriveBaseAbility)
                abilityData = data1.MasterAbility.DerivedAbility;
              if (abilityData == data2)
              {
                using (List<SkillData>.Enumerator enumerator = data1.CollaboAbility.Skills.GetEnumerator())
                {
                  while (enumerator.MoveNext())
                  {
                    SkillData current = enumerator.Current;
                    AbilityDetailWindow.AddSkillParam(viewContentParams, current.SkillParam, (SkillDeriveParam) null);
                  }
                }
              }
            }
            for (int index = 0; index < viewContentParams.Count; ++index)
            {
              GameObject gameObject = (GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) this.SkillTemplate);
              SkillDeriveList componentInChildren = (SkillDeriveList) gameObject.GetComponentInChildren<SkillDeriveList>();
              componentInChildren.Setup(viewContentParams[index].m_SkillParam, viewContentParams[index].m_SkillDeriveParams);
              gameObject.get_transform().SetParent(this.SkillLayoutParent, false);
              gameObject.SetActive(true);
              this.m_SkillDeriveList.Add(componentInChildren);
            }
            for (int index = 0; index < unlockUnitDataParamList.Count; ++index)
            {
              GameObject gameObject = (GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) this.SkillLockedTemplate);
              DataSource.Bind<SkillParam>(gameObject, MonoSingleton<GameManager>.Instance.GetSkillParam(unlockUnitDataParamList[index].new_id));
              DataSource.Bind<QuestClearUnlockUnitDataParam>(gameObject, unlockUnitDataParamList[index]);
              gameObject.get_transform().SetParent(this.SkillLayoutParent, false);
              gameObject.SetActive(true);
            }
            for (int index = 0; index < viewContentParams.Count; ++index)
            {
              if (viewContentParams[index].m_SkillDeriveParams != null && viewContentParams[index].m_SkillDeriveParams.Count >= 1)
              {
                this.m_ShowBaseToggle.set_interactable(true);
                break;
              }
            }
          }
        }
        GameParameter.UpdateAll(((Component) this).get_gameObject());
      }
    }

    private static void AddSkillParam(List<AbilityDetailWindow.ViewContentParam> viewContentParams, SkillParam skillParam, SkillDeriveParam skillDeriveParam)
    {
      if (skillDeriveParam != null)
        skillParam = skillDeriveParam.m_BaseParam;
      AbilityDetailWindow.ViewContentParam viewContentParam1 = viewContentParams.Find((Predicate<AbilityDetailWindow.ViewContentParam>) (param => param.m_SkillParam == skillParam));
      if (viewContentParam1 != null)
      {
        viewContentParam1.m_SkillDeriveParams.Add(skillDeriveParam);
      }
      else
      {
        AbilityDetailWindow.ViewContentParam viewContentParam2 = new AbilityDetailWindow.ViewContentParam();
        viewContentParam2.m_SkillParam = skillParam;
        if (skillDeriveParam != null)
        {
          viewContentParam2.m_SkillDeriveParams = new List<SkillDeriveParam>();
          viewContentParam2.m_SkillDeriveParams.Add(skillDeriveParam);
        }
        viewContentParams.Add(viewContentParam2);
      }
    }

    public void OnOpenSkillUnlockCondWindow(GameObject button)
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.SkillUnlockCondWindow, (UnityEngine.Object) null))
        return;
      GameObject gameObject1 = (GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) this.SkillUnlockCondWindow);
      QuestClearUnlockUnitDataParam dataOfClass = DataSource.FindDataOfClass<QuestClearUnlockUnitDataParam>(button, (QuestClearUnlockUnitDataParam) null);
      DataSource.Bind<QuestClearUnlockUnitDataParam>(gameObject1, dataOfClass);
      gameObject1.SetActive(true);
      gameObject1.get_transform().SetParent(((Component) this).get_transform(), false);
      Text gameObject2 = GameObjectID.FindGameObject<Text>(this.UnlockCondTextId);
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) gameObject2, (UnityEngine.Object) null))
        return;
      gameObject2.set_text(dataOfClass.GetUnitLevelCond());
      string abilityLevelCond = dataOfClass.GetAbilityLevelCond();
      if (!string.IsNullOrEmpty(abilityLevelCond))
      {
        Text text = gameObject2;
        text.set_text(text.get_text() + (string.IsNullOrEmpty(gameObject2.get_text()) ? string.Empty : "\n") + abilityLevelCond);
      }
      string clearQuestCond = dataOfClass.GetClearQuestCond();
      if (string.IsNullOrEmpty(clearQuestCond))
        return;
      Text text1 = gameObject2;
      text1.set_text(text1.get_text() + (string.IsNullOrEmpty(gameObject2.get_text()) ? string.Empty : "\n") + clearQuestCond);
    }

    private class ViewContentParam
    {
      public SkillParam m_SkillParam;
      public List<SkillDeriveParam> m_SkillDeriveParams;
    }
  }
}
