// Decompiled with JetBrains decompiler
// Type: SRPG.ArtifactDetailWindow
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
  [FlowNode.Pin(100, "アビリティ詳細画面の表示", FlowNode.PinTypes.Output, 100)]
  public class ArtifactDetailWindow : MonoBehaviour, IFlowInterface
  {
    [SerializeField]
    private GameObject mAbilityContentTemplate;
    [SerializeField]
    private GameObject mJobContentTemplate;
    [SerializeField]
    private GameObject mAbilityEmptyTextObject;
    [SerializeField]
    private Toggle mHideDisplayAbilityToggle;
    [HeaderBar("▼セット効果確認用のボタン")]
    [SerializeField]
    private Button mSetEffectsButton;
    private ArtifactParam mCurrentArtifactParam;
    private ArtifactData mCurrentArtifactData;
    private UnitData mCurrentUnit;
    private JobData mCurrentJob;
    private ArtifactTypes mCurrentEquipSlot;
    private Dictionary<string, ViewAbilityData> mViewAbilities;
    private List<ArtifactDetailAbilityContent> mAbilityContents;
    private static ArtifactParam s_ArtifactParam;

    public ArtifactDetailWindow()
    {
      base.\u002Ector();
    }

    public static void SetArtifactParam(ArtifactParam artifactParam)
    {
      ArtifactDetailWindow.s_ArtifactParam = artifactParam;
    }

    private bool IsNeedCheck
    {
      get
      {
        return this.mCurrentUnit != null && this.mCurrentArtifactData != null;
      }
    }

    public void Activated(int pinID)
    {
    }

    private void Start()
    {
      this.mCurrentArtifactParam = DataSource.FindDataOfClass<ArtifactParam>(((Component) this).get_gameObject(), (ArtifactParam) null);
      this.mCurrentArtifactData = DataSource.FindDataOfClass<ArtifactData>(((Component) this).get_gameObject(), (ArtifactData) null);
      this.mCurrentUnit = DataSource.FindDataOfClass<UnitData>(((Component) this).get_gameObject(), (UnitData) null);
      this.mCurrentEquipSlot = DataSource.FindDataOfClass<ArtifactTypes>(((Component) this).get_gameObject(), ArtifactTypes.None);
      this.mCurrentJob = DataSource.FindDataOfClass<JobData>(((Component) this).get_gameObject(), (JobData) null);
      if (this.mCurrentArtifactParam == null)
      {
        this.mCurrentArtifactParam = ArtifactDetailWindow.s_ArtifactParam;
        ArtifactDetailWindow.s_ArtifactParam = (ArtifactParam) null;
        if (this.mCurrentArtifactParam != null)
          DataSource.Bind<ArtifactParam>(((Component) this).get_gameObject(), this.mCurrentArtifactParam);
      }
      if (this.mCurrentArtifactParam == null && this.mCurrentArtifactData != null)
        this.mCurrentArtifactParam = this.mCurrentArtifactData.ArtifactParam;
      if (this.mCurrentArtifactParam == null)
        return;
      ArtifactSetList.SetSelectedArtifactParam(this.mCurrentArtifactParam);
      this.RefreshView();
    }

    private void RefreshView()
    {
      List<AbilityParam> artifact_all_abilities = new List<AbilityParam>();
      if (this.mCurrentArtifactParam != null && this.mCurrentArtifactParam.abil_inames != null)
      {
        for (int index = 0; index < this.mCurrentArtifactParam.abil_inames.Length; ++index)
        {
          if (!string.IsNullOrEmpty(this.mCurrentArtifactParam.abil_inames[index]))
          {
            AbilityParam abilityParam = MonoSingleton<GameManager>.Instance.MasterParam.GetAbilityParam(this.mCurrentArtifactParam.abil_inames[index]);
            if (abilityParam != null)
              artifact_all_abilities.Add(abilityParam);
          }
        }
      }
      List<AbilityData> weapon_abilities = (List<AbilityData>) null;
      if (this.IsNeedCheck)
        weapon_abilities = this.mCurrentArtifactData.LearningAbilities;
      List<AbilityDeriveParam> derive_params = (List<AbilityDeriveParam>) null;
      if (this.IsNeedCheck)
      {
        SkillAbilityDeriveData abilityDeriveData = this.mCurrentUnit.GetSkillAbilityDeriveData(this.mCurrentJob, this.mCurrentEquipSlot, this.mCurrentArtifactParam);
        if (abilityDeriveData != null)
          derive_params = abilityDeriveData.GetAvailableAbilityDeriveParams();
      }
      this.mViewAbilities = this.CreateViewData(artifact_all_abilities, weapon_abilities, derive_params);
      this.mAbilityEmptyTextObject.SetActive(this.mViewAbilities.Count <= 0);
      this.CreateInstance(this.mViewAbilities);
      bool flag = false;
      foreach (ArtifactDetailAbilityItem componentsInChild in (ArtifactDetailAbilityItem[]) ((Component) this).GetComponentsInChildren<ArtifactDetailAbilityItem>(true))
      {
        if (componentsInChild.HasDeriveAbility)
        {
          flag = true;
          break;
        }
      }
      ((Selectable) this.mHideDisplayAbilityToggle).set_interactable(flag);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mSetEffectsButton, (UnityEngine.Object) null) && this.mCurrentArtifactParam != null)
        ((Selectable) this.mSetEffectsButton).set_interactable(MonoSingleton<GameManager>.Instance.MasterParam.ExistSkillAbilityDeriveDataWithArtifact(this.mCurrentArtifactParam.iname));
      this.Reposition();
    }

    private void CreateInstance(Dictionary<string, ViewAbilityData> view_datas)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mAbilityContentTemplate, (UnityEngine.Object) null))
        return;
      using (Dictionary<string, ViewAbilityData>.KeyCollection.Enumerator enumerator = view_datas.Keys.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          string current = enumerator.Current;
          GameObject gameObject = (GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) this.mAbilityContentTemplate);
          gameObject.get_transform().SetParent(this.mAbilityContentTemplate.get_transform().get_parent(), false);
          ArtifactDetailAbilityContent component = (ArtifactDetailAbilityContent) gameObject.GetComponent<ArtifactDetailAbilityContent>();
          component.Init(view_datas[current]);
          this.mAbilityContents.Add(component);
        }
      }
      this.mAbilityContentTemplate.SetActive(false);
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mJobContentTemplate, (UnityEngine.Object) null))
        return;
      this.mJobContentTemplate.SetActive(false);
    }

    private Dictionary<string, ViewAbilityData> CreateViewData(List<AbilityParam> artifact_all_abilities, List<AbilityData> weapon_abilities, List<AbilityDeriveParam> derive_params)
    {
      Dictionary<string, ViewAbilityData> dictionary = new Dictionary<string, ViewAbilityData>();
      if (artifact_all_abilities != null)
      {
        for (int i = 0; i < artifact_all_abilities.Count; ++i)
        {
          if (!dictionary.ContainsKey(artifact_all_abilities[i].iname))
          {
            bool is_enable = true;
            if (this.IsNeedCheck)
              is_enable = artifact_all_abilities[i].CheckEnableUseAbility(this.mCurrentUnit, this.mCurrentUnit.JobIndex);
            bool is_locked = false;
            if (this.IsNeedCheck && weapon_abilities != null && weapon_abilities.FindIndex((Predicate<AbilityData>) (abil_data => abil_data.Param.iname == artifact_all_abilities[i].iname)) <= -1)
              is_locked = true;
            ViewAbilityData viewAbilityData = new ViewAbilityData();
            viewAbilityData.AddAbility(artifact_all_abilities[i], is_enable, is_locked);
            dictionary.Add(artifact_all_abilities[i].iname, viewAbilityData);
          }
        }
      }
      if (derive_params != null)
      {
        for (int index = 0; index < derive_params.Count; ++index)
        {
          string baseAbilityIname = derive_params[index].BaseAbilityIname;
          if (dictionary.ContainsKey(baseAbilityIname))
          {
            bool is_enable = true;
            if (this.IsNeedCheck)
              is_enable = MonoSingleton<GameManager>.Instance.MasterParam.GetAbilityParam(derive_params[index].DeriveAbilityIname).CheckEnableUseAbility(this.mCurrentUnit, this.mCurrentUnit.JobIndex);
            dictionary[baseAbilityIname].AddAbility(baseAbilityIname, derive_params[index], derive_params[index].DeriveAbilityIname, is_enable);
          }
        }
      }
      return dictionary;
    }

    private void ChangeDisplayBaseAbility(bool is_display)
    {
      ArtifactDetailAbilityItem[] componentsInChildren = (ArtifactDetailAbilityItem[]) ((Component) this).GetComponentsInChildren<ArtifactDetailAbilityItem>(true);
      for (int index = 0; index < componentsInChildren.Length; ++index)
      {
        if (!componentsInChildren[index].IsEnable && componentsInChildren[index].HasDeriveAbility)
          componentsInChildren[index].SetActive(is_display);
      }
      for (int index = 0; index < componentsInChildren.Length; ++index)
        componentsInChildren[index].SetActiveLine(is_display);
    }

    private void Reposition()
    {
      int num = 0;
      for (int index = 0; index < this.mAbilityContents.Count; ++index)
      {
        if (this.mAbilityContents[index].IsExistEnableAbility)
        {
          ((Component) this.mAbilityContents[index]).get_transform().SetSiblingIndex(num);
          ++num;
        }
      }
    }

    public void OpenAbilityDetail()
    {
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
    }

    public void OnChangeDisplayBaseAbility()
    {
      this.ChangeDisplayBaseAbility(this.mHideDisplayAbilityToggle.get_isOn());
    }

    public void OnCheckJobButton()
    {
      if (this.mCurrentArtifactParam == null)
        return;
      GlobalVars.ConditionJobs = this.mCurrentArtifactParam.condition_jobs;
    }
  }
}
