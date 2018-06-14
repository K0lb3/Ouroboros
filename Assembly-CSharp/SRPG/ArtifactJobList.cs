// Decompiled with JetBrains decompiler
// Type: SRPG.ArtifactJobList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  public class ArtifactJobList : MonoBehaviour, IGameParameter
  {
    public GameObject ListItem;
    public GameObject AnyJob;
    private List<JobParam> mCurrentJobs;
    private List<GameObject> mJobListItems;

    public ArtifactJobList()
    {
      base.\u002Ector();
    }

    private void Start()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ListItem, (UnityEngine.Object) null))
        this.ListItem.SetActive(false);
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.AnyJob, (UnityEngine.Object) null))
        return;
      this.AnyJob.SetActive(false);
    }

    private void OnDestroy()
    {
      GlobalVars.ConditionJobs = (string[]) null;
    }

    public void UpdateValue()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.ListItem, (UnityEngine.Object) null) || UnityEngine.Object.op_Equality((UnityEngine.Object) this.AnyJob, (UnityEngine.Object) null))
        return;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ListItem, (UnityEngine.Object) null))
        this.ListItem.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.AnyJob, (UnityEngine.Object) null))
        this.AnyJob.SetActive(false);
      string[] conditionJobs = GlobalVars.ConditionJobs;
      MasterParam masterParam = MonoSingleton<GameManager>.Instance.MasterParam;
      int index1 = 0;
      if (conditionJobs != null)
      {
        for (int index2 = 0; index2 < conditionJobs.Length; ++index2)
        {
          if (!string.IsNullOrEmpty(conditionJobs[index2]))
          {
            JobParam jobParam;
            try
            {
              jobParam = masterParam.GetJobParam(conditionJobs[index2]);
            }
            catch (Exception ex)
            {
              continue;
            }
            if (jobParam != null)
            {
              if (this.mJobListItems.Count <= index1)
              {
                GameObject gameObject = (GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) this.ListItem);
                gameObject.get_transform().SetParent(((Component) this).get_transform(), false);
                this.mJobListItems.Add(gameObject);
                this.mCurrentJobs.Add((JobParam) null);
              }
              this.mJobListItems[index1].SetActive(true);
              if (this.mCurrentJobs[index1] == jobParam)
              {
                ++index1;
              }
              else
              {
                this.mCurrentJobs[index1] = jobParam;
                ArtifactJobItem component = (ArtifactJobItem) this.mJobListItems[index1].GetComponent<ArtifactJobItem>();
                DataSource.Bind<JobParam>(component.jobIcon, jobParam);
                component.jobName.set_text(jobParam.name);
                ++index1;
              }
            }
          }
        }
      }
      for (int index2 = index1; index2 < this.mJobListItems.Count; ++index2)
        this.mJobListItems[index2].SetActive(false);
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.AnyJob, (UnityEngine.Object) null))
        return;
      this.AnyJob.SetActive(index1 == 0);
    }
  }
}
