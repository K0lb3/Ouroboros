// Decompiled with JetBrains decompiler
// Type: SRPG.ArtifactJobs
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  public class ArtifactJobs : MonoBehaviour, IGameParameter
  {
    public GameObject ListItem;
    public GameObject AnyJob;
    private List<JobParam> mCurrentJobs;
    private List<GameObject> mJobListItems;
    private bool mUpdated;

    public ArtifactJobs()
    {
      base.\u002Ector();
    }

    private void Start()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ListItem, (UnityEngine.Object) null) && this.ListItem.get_activeInHierarchy())
        this.ListItem.SetActive(false);
      if (this.mUpdated)
        return;
      this.UpdateValue();
    }

    public void UpdateValue()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.ListItem, (UnityEngine.Object) null))
        return;
      ArtifactData dataOfClass = DataSource.FindDataOfClass<ArtifactData>(((Component) this).get_gameObject(), (ArtifactData) null);
      ArtifactParam artifactParam = dataOfClass == null ? DataSource.FindDataOfClass<ArtifactParam>(((Component) this).get_gameObject(), (ArtifactParam) null) : dataOfClass.ArtifactParam;
      if (artifactParam == null)
      {
        for (int index = 0; index < this.mJobListItems.Count; ++index)
          this.mJobListItems[index].SetActive(false);
      }
      else
      {
        Transform transform = ((Component) this).get_transform();
        MasterParam masterParam = MonoSingleton<GameManager>.Instance.MasterParam;
        string[] conditionJobs = artifactParam.condition_jobs;
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
                  gameObject.get_transform().SetParent(transform, false);
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
                  DataSource.Bind<JobParam>(this.mJobListItems[index1], jobParam);
                  GameParameter.UpdateAll(this.mJobListItems[index1]);
                  ++index1;
                }
              }
            }
          }
        }
        for (int index2 = index1; index2 < this.mJobListItems.Count; ++index2)
          this.mJobListItems[index2].SetActive(false);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.AnyJob, (UnityEngine.Object) null))
          this.AnyJob.SetActive(index1 == 0);
        this.mUpdated = true;
      }
    }
  }
}
