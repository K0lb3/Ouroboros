// Decompiled with JetBrains decompiler
// Type: SRPG.ArtifactGetUnlockWindow
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
  [FlowNode.Pin(1, "Refresh", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(100, "Unlock", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(101, "Selected Quest", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(501, "武具詳細情報セット(out)", FlowNode.PinTypes.Output, 501)]
  [FlowNode.Pin(500, "武具詳細情報セット(in)", FlowNode.PinTypes.Input, 500)]
  public class ArtifactGetUnlockWindow : MonoBehaviour, IFlowInterface
  {
    private const int INPUT_REFRESH = 1;
    private const int INPUT_UNLOCK = 100;
    private const int INPUT_SELECT_QUEST = 101;
    private const int INPUT_ARTIFACT_DETAIL_SET = 500;
    private const int OUTPUT_ARTIFACT_DETAIL_SET = 501;
    private ArtifactData UnlockArtifact;
    public StatusList ArtifactStatus;
    public GameObject AbilityListItem;
    public float ability_unlock_alpha;
    public float ability_hidden_alpha;
    public GameObject lock_object;
    [HeaderBar("▼セット効果確認用のボタン")]
    [SerializeField]
    private Button m_SetEffectsButton;

    public ArtifactGetUnlockWindow()
    {
      base.\u002Ector();
    }

    private void Start()
    {
    }

    public void Activated(int pinID)
    {
      if (pinID == 1)
        this.Refresh();
      switch (pinID)
      {
        case 1:
          this.Refresh();
          break;
        case 500:
          this.SetArtifactDetailData();
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 501);
          break;
      }
    }

    private void Refresh()
    {
      ArtifactData artifactData = new ArtifactData();
      artifactData.Deserialize(new Json_Artifact()
      {
        iname = GlobalVars.ArtifactListItem.param.iname,
        rare = GlobalVars.ArtifactListItem.param.rareini
      });
      this.UnlockArtifact = artifactData;
      DataSource.Bind<ArtifactData>(((Component) this).get_gameObject(), this.UnlockArtifact);
      DataSource.Bind<ArtifactParam>(((Component) this).get_gameObject(), this.UnlockArtifact.ArtifactParam);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.AbilityListItem, (UnityEngine.Object) null))
      {
        MasterParam masterParam = MonoSingleton<GameManager>.Instance.MasterParam;
        GameObject abilityListItem = this.AbilityListItem;
        CanvasGroup component = (CanvasGroup) abilityListItem.GetComponent<CanvasGroup>();
        bool flag = false;
        ArtifactParam artifactParam = artifactData.ArtifactParam;
        List<AbilityData> learningAbilities = artifactData.LearningAbilities;
        if (artifactParam.abil_inames != null)
        {
          AbilityParam data1 = (AbilityParam) null;
          string abil_iname = (string) null;
          for (int index = 0; index < artifactParam.abil_inames.Length; ++index)
          {
            if (!string.IsNullOrEmpty(artifactParam.abil_inames[index]) && artifactParam.abil_shows[index] != 0)
            {
              abil_iname = artifactParam.abil_inames[index];
              data1 = masterParam.GetAbilityParam(artifactParam.abil_inames[index]);
              if (data1 != null)
                break;
            }
          }
          if (data1 == null)
          {
            component.set_alpha(this.ability_hidden_alpha);
            DataSource.Bind<AbilityParam>(this.AbilityListItem, (AbilityParam) null);
            DataSource.Bind<AbilityData>(this.AbilityListItem, (AbilityData) null);
            return;
          }
          DataSource.Bind<AbilityParam>(this.AbilityListItem, data1);
          DataSource.Bind<AbilityData>(abilityListItem, (AbilityData) null);
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null) && learningAbilities != null && learningAbilities != null)
          {
            AbilityData data2 = learningAbilities.Find((Predicate<AbilityData>) (x => x.Param.iname == abil_iname));
            if (data2 != null)
            {
              DataSource.Bind<AbilityData>(abilityListItem, data2);
              flag = true;
            }
          }
        }
        if (flag)
          component.set_alpha(this.ability_unlock_alpha);
        else
          component.set_alpha(this.ability_hidden_alpha);
      }
      BaseStatus fixed_status = new BaseStatus();
      BaseStatus scale_status = new BaseStatus();
      this.UnlockArtifact.GetHomePassiveBuffStatus(ref fixed_status, ref scale_status, (UnitData) null, 0, true);
      this.ArtifactStatus.SetValues(fixed_status, scale_status, false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_SetEffectsButton, (UnityEngine.Object) null) && this.UnlockArtifact.ArtifactParam != null)
      {
        ((Selectable) this.m_SetEffectsButton).set_interactable(MonoSingleton<GameManager>.Instance.MasterParam.ExistSkillAbilityDeriveDataWithArtifact(this.UnlockArtifact.ArtifactParam.iname));
        if (((Selectable) this.m_SetEffectsButton).get_interactable())
          ArtifactSetList.SetSelectedArtifactParam(this.UnlockArtifact.ArtifactParam);
      }
      GameParameter.UpdateAll(((Component) this).get_gameObject());
    }

    private void SetArtifactDetailData()
    {
      if (this.UnlockArtifact == null)
        return;
      ArtifactDetailWindow.SetArtifactParam(this.UnlockArtifact.ArtifactParam);
    }

    public void SetArtifactData()
    {
      GlobalVars.ConditionJobs = GlobalVars.ArtifactListItem.param.condition_jobs;
    }
  }
}
