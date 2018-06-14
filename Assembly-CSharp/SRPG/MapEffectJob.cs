// Decompiled with JetBrains decompiler
// Type: SRPG.MapEffectJob
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  public class MapEffectJob : MonoBehaviour
  {
    public GameObject GoMapEffectParent;
    public GameObject GoMapEffectBaseItem;

    public MapEffectJob()
    {
      base.\u002Ector();
    }

    public void Setup()
    {
      JobParam dataOfClass = DataSource.FindDataOfClass<JobParam>(((Component) this).get_gameObject(), (JobParam) null);
      if (dataOfClass == null)
        return;
      GameManager instanceDirect = MonoSingleton<GameManager>.GetInstanceDirect();
      if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) instanceDirect))
        return;
      if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.GoMapEffectParent) && UnityEngine.Object.op_Implicit((UnityEngine.Object) this.GoMapEffectBaseItem))
      {
        this.GoMapEffectBaseItem.SetActive(false);
        BattleUnitDetail.DestroyChildGameObjects(this.GoMapEffectParent, new List<GameObject>((IEnumerable<GameObject>) new GameObject[1]
        {
          this.GoMapEffectBaseItem
        }));
      }
      DataSource component = (DataSource) ((Component) this).get_gameObject().GetComponent<DataSource>();
      if (UnityEngine.Object.op_Implicit((UnityEngine.Object) component))
        component.Clear();
      DataSource.Bind<JobParam>(((Component) this).get_gameObject(), dataOfClass);
      if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) this.GoMapEffectParent) || !UnityEngine.Object.op_Implicit((UnityEngine.Object) this.GoMapEffectBaseItem))
        return;
      List<MapEffectJob.MeSkill> l = new List<MapEffectJob.MeSkill>();
      if (!string.IsNullOrEmpty(dataOfClass.MapEffectAbility))
      {
        AbilityParam abilityParam = instanceDirect.GetAbilityParam(dataOfClass.MapEffectAbility);
        if (abilityParam != null)
        {
          foreach (LearningSkill skill in abilityParam.skills)
          {
            SkillParam skillParam = instanceDirect.GetSkillParam(skill.iname);
            if (skillParam != null)
            {
              using (List<MapEffectParam>.Enumerator enumerator1 = MapEffectParam.GetHaveMapEffectLists(skill.iname).GetEnumerator())
              {
                while (enumerator1.MoveNext())
                {
                  MapEffectParam current = enumerator1.Current;
                  bool flag = false;
                  using (List<MapEffectJob.MeSkill>.Enumerator enumerator2 = l.GetEnumerator())
                  {
                    while (enumerator2.MoveNext())
                    {
                      if (enumerator2.Current.Equals(current, skillParam))
                      {
                        flag = true;
                        break;
                      }
                    }
                  }
                  if (!flag)
                    l.Add(new MapEffectJob.MeSkill(current, skillParam));
                }
              }
            }
          }
        }
      }
      if (l.Count == 0)
        return;
      MySort<MapEffectJob.MeSkill>.Sort(l, (Comparison<MapEffectJob.MeSkill>) ((src, dsc) =>
      {
        if (src == dsc)
          return 0;
        return dsc.mMapEffectParam.Index - src.mMapEffectParam.Index;
      }));
      using (List<MapEffectJob.MeSkill>.Enumerator enumerator = l.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          MapEffectJob.MeSkill current = enumerator.Current;
          GameObject gameObject = (GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) this.GoMapEffectBaseItem);
          if (UnityEngine.Object.op_Implicit((UnityEngine.Object) gameObject))
          {
            gameObject.get_transform().SetParent(this.GoMapEffectParent.get_transform());
            gameObject.get_transform().set_localScale(Vector3.get_one());
            DataSource.Bind<MapEffectParam>(gameObject, current.mMapEffectParam);
            DataSource.Bind<SkillParam>(gameObject, current.mSkillParam);
            gameObject.SetActive(true);
          }
        }
      }
    }

    private class MeSkill
    {
      public MapEffectParam mMapEffectParam;
      public SkillParam mSkillParam;

      public MeSkill(MapEffectParam me_param, SkillParam skill_param)
      {
        this.mMapEffectParam = me_param;
        this.mSkillParam = skill_param;
      }

      public bool Equals(MapEffectParam me_param, SkillParam skill_param)
      {
        if (this.mMapEffectParam == me_param)
          return this.mSkillParam == skill_param;
        return false;
      }
    }
  }
}
