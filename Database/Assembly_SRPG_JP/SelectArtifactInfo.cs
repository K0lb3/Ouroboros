// Decompiled with JetBrains decompiler
// Type: SRPG.SelectArtifactInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(1, "Refresh", FlowNode.PinTypes.Input, 1)]
  public class SelectArtifactInfo : MonoBehaviour, IFlowInterface
  {
    public GameObject AbilityListItem;
    public float ability_unlock_alpha;
    public float ability_hidden_alpha;
    public GameObject lock_object;

    public SelectArtifactInfo()
    {
      base.\u002Ector();
    }

    private void Awake()
    {
    }

    private void Start()
    {
      this.Refresh();
    }

    public void Activated(int pinID)
    {
      if (pinID != 1)
        return;
      this.Refresh();
    }

    private void Refresh()
    {
      ArtifactParam data1 = GlobalVars.ArtifactListItem.param;
      ArtifactData data2 = new ArtifactData();
      data2.Deserialize(new Json_Artifact()
      {
        iname = GlobalVars.ArtifactListItem.param.iname,
        rare = GlobalVars.ArtifactListItem.param.rareini
      });
      DataSource.Bind<ArtifactParam>(((Component) this).get_gameObject(), data1);
      DataSource.Bind<ArtifactData>(((Component) this).get_gameObject(), data2);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.AbilityListItem, (UnityEngine.Object) null))
      {
        MasterParam masterParam = MonoSingleton<GameManager>.Instance.MasterParam;
        GameObject abilityListItem = this.AbilityListItem;
        CanvasGroup component = (CanvasGroup) abilityListItem.GetComponent<CanvasGroup>();
        bool flag = false;
        ArtifactParam artifactParam = data2.ArtifactParam;
        List<AbilityData> learningAbilities = data2.LearningAbilities;
        if (artifactParam.abil_inames != null)
        {
          AbilityParam data3 = (AbilityParam) null;
          string abil_iname = (string) null;
          for (int index = 0; index < artifactParam.abil_inames.Length; ++index)
          {
            if (!string.IsNullOrEmpty(artifactParam.abil_inames[index]) && artifactParam.abil_shows[index] != 0)
            {
              abil_iname = artifactParam.abil_inames[index];
              data3 = masterParam.GetAbilityParam(artifactParam.abil_inames[index]);
              if (data3 != null)
                break;
            }
          }
          if (data3 == null)
          {
            component.set_alpha(this.ability_hidden_alpha);
            DataSource.Bind<AbilityParam>(this.AbilityListItem, (AbilityParam) null);
            DataSource.Bind<AbilityData>(this.AbilityListItem, (AbilityData) null);
            return;
          }
          DataSource.Bind<AbilityParam>(this.AbilityListItem, data3);
          DataSource.Bind<AbilityData>(abilityListItem, (AbilityData) null);
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null) && learningAbilities != null && learningAbilities != null)
          {
            AbilityData data4 = learningAbilities.Find((Predicate<AbilityData>) (x => x.Param.iname == abil_iname));
            if (data4 != null)
            {
              DataSource.Bind<AbilityData>(abilityListItem, data4);
              flag = true;
            }
          }
        }
        if (flag)
          component.set_alpha(this.ability_unlock_alpha);
        else
          component.set_alpha(this.ability_hidden_alpha);
      }
      GameParameter.UpdateAll(((Component) this).get_gameObject());
    }
  }
}
