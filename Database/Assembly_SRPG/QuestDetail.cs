// Decompiled with JetBrains decompiler
// Type: SRPG.QuestDetail
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  public class QuestDetail : MonoBehaviour
  {
    public GameObject[] Missions;
    public GameObject NoMissionText;
    [SerializeField]
    private GameObject[] mission_reward_parent_objects;
    [SerializeField]
    private GameObject[] mission_reward_item_objects;
    [SerializeField]
    private GameObject[] mission_reward_unit_objects;
    [SerializeField]
    private GameObject[] mission_reward_artifact_objects;

    public QuestDetail()
    {
      base.\u002Ector();
    }

    private void Start()
    {
      QuestParam dataOfClass = DataSource.FindDataOfClass<QuestParam>(((Component) this).get_gameObject(), (QuestParam) null);
      if (dataOfClass == null)
        return;
      this.SetMissionListActive(dataOfClass);
    }

    private void SetMissionListActive(QuestParam param)
    {
      bool flag = !string.IsNullOrEmpty(param.mission);
      for (int index = 0; index < this.Missions.Length; ++index)
      {
        if (this.Missions != null)
          this.Missions[index].SetActive(flag);
      }
      if (Object.op_Inequality((Object) this.NoMissionText, (Object) null))
        this.NoMissionText.SetActive(!flag);
      this.HideAllRewardObjects();
      this.RefreshActiveRewardObjects(param);
    }

    private void HideAllRewardObjects()
    {
      if (this.mission_reward_parent_objects != null)
      {
        for (int index = 0; index < this.mission_reward_parent_objects.Length; ++index)
        {
          if (!Object.op_Equality((Object) this.mission_reward_parent_objects[index], (Object) null))
            this.mission_reward_parent_objects[index].SetActive(false);
        }
      }
      if (this.mission_reward_item_objects != null)
      {
        for (int index = 0; index < this.mission_reward_item_objects.Length; ++index)
        {
          if (!Object.op_Equality((Object) this.mission_reward_item_objects[index], (Object) null))
            this.mission_reward_item_objects[index].SetActive(false);
        }
      }
      if (this.mission_reward_unit_objects != null)
      {
        for (int index = 0; index < this.mission_reward_unit_objects.Length; ++index)
        {
          if (!Object.op_Equality((Object) this.mission_reward_unit_objects[index], (Object) null))
            this.mission_reward_unit_objects[index].SetActive(false);
        }
      }
      if (this.mission_reward_artifact_objects == null)
        return;
      for (int index = 0; index < this.mission_reward_artifact_objects.Length; ++index)
      {
        if (!Object.op_Equality((Object) this.mission_reward_artifact_objects[index], (Object) null))
          this.mission_reward_artifact_objects[index].SetActive(false);
      }
    }

    private void RefreshActiveRewardObjects(QuestParam param)
    {
      for (int index = 0; index < param.bonusObjective.Length; ++index)
      {
        if (this.mission_reward_parent_objects.Length > index && Object.op_Inequality((Object) this.mission_reward_parent_objects[index], (Object) null))
          this.mission_reward_parent_objects[index].SetActive(true);
        if (param.bonusObjective[index].itemType == RewardType.Artifact)
        {
          if (MonoSingleton<GameManager>.Instance.MasterParam.GetArtifactParam(param.bonusObjective[index].item) != null && this.mission_reward_artifact_objects.Length > index && Object.op_Inequality((Object) this.mission_reward_artifact_objects[index], (Object) null))
            this.mission_reward_artifact_objects[index].SetActive(true);
        }
        else
        {
          ItemParam itemParam = MonoSingleton<GameManager>.Instance.GetItemParam(param.bonusObjective[index].item);
          if (itemParam != null)
          {
            if (itemParam.type == EItemType.Unit)
            {
              if (this.mission_reward_unit_objects.Length > index && Object.op_Inequality((Object) this.mission_reward_unit_objects[index], (Object) null))
                this.mission_reward_unit_objects[index].SetActive(true);
            }
            else if (this.mission_reward_item_objects.Length > index && Object.op_Inequality((Object) this.mission_reward_item_objects[index], (Object) null))
              this.mission_reward_item_objects[index].SetActive(true);
          }
        }
      }
    }
  }
}
