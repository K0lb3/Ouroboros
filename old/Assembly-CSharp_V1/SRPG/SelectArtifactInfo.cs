// Decompiled with JetBrains decompiler
// Type: SRPG.SelectArtifactInfo
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

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
      if (Object.op_Inequality((Object) this.AbilityListItem, (Object) null))
      {
        MasterParam masterParam = MonoSingleton<GameManager>.Instance.MasterParam;
        GameObject abilityListItem = this.AbilityListItem;
        CanvasGroup component = (CanvasGroup) abilityListItem.GetComponent<CanvasGroup>();
        bool flag = false;
        ArtifactParam artifactParam = data2.ArtifactParam;
        List<AbilityData> learningAbilities = data2.LearningAbilities;
        if (artifactParam.abil_inames != null)
        {
          // ISSUE: object of a compiler-generated type is created
          // ISSUE: variable of a compiler-generated type
          SelectArtifactInfo.\u003CRefresh\u003Ec__AnonStorey26F refreshCAnonStorey26F = new SelectArtifactInfo.\u003CRefresh\u003Ec__AnonStorey26F();
          AbilityParam data3 = (AbilityParam) null;
          // ISSUE: reference to a compiler-generated field
          refreshCAnonStorey26F.abil_iname = (string) null;
          for (int index = 0; index < artifactParam.abil_inames.Length; ++index)
          {
            if (!string.IsNullOrEmpty(artifactParam.abil_inames[index]) && artifactParam.abil_shows[index] != 0)
            {
              // ISSUE: reference to a compiler-generated field
              refreshCAnonStorey26F.abil_iname = artifactParam.abil_inames[index];
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
          if (Object.op_Inequality((Object) component, (Object) null) && learningAbilities != null && learningAbilities != null)
          {
            // ISSUE: reference to a compiler-generated method
            AbilityData data4 = learningAbilities.Find(new Predicate<AbilityData>(refreshCAnonStorey26F.\u003C\u003Em__2DE));
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
