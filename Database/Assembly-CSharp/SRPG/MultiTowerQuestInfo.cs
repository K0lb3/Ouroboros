// Decompiled with JetBrains decompiler
// Type: SRPG.MultiTowerQuestInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class MultiTowerQuestInfo : MonoBehaviour
  {
    public GameObject EnemyTemplate;
    public GameObject EnemyRoot;
    public Text QuestTitle;
    public Text RecommendLv;
    public GameObject DetailObject;
    public GameObject RewardTemplate;
    public GameObject RewardRoot;
    private GameObject Detail;
    private List<GameObject> mEnemyObject;

    public MultiTowerQuestInfo()
    {
      base.\u002Ector();
    }

    private void Start()
    {
    }

    public void Refresh()
    {
      MultiTowerFloorParam dataOfClass = DataSource.FindDataOfClass<MultiTowerFloorParam>(((Component) this).get_gameObject(), (MultiTowerFloorParam) null);
      if (dataOfClass == null)
        return;
      this.SetEnemy(dataOfClass);
      if (Object.op_Inequality((Object) this.QuestTitle, (Object) null))
        this.QuestTitle.set_text(dataOfClass.title + " " + dataOfClass.name);
      if (Object.op_Inequality((Object) this.RecommendLv, (Object) null))
        this.RecommendLv.set_text(string.Format(LocalizedText.Get("sys.MULTI_TOWER_RECOMMEND"), (object) dataOfClass.lv, (object) dataOfClass.joblv));
      this.SetReward(dataOfClass);
    }

    private void SetEnemy(MultiTowerFloorParam param)
    {
      int index1 = 0;
      if (param.map == null)
        return;
      string src = AssetManager.LoadTextData(AssetPath.LocalMap(param.map[0].mapSetName));
      if (src == null)
        return;
      JSON_MapUnit jsonObject = JSONParser.parseJSONObject<JSON_MapUnit>(src);
      if (jsonObject == null || !Object.op_Inequality((Object) this.EnemyTemplate, (Object) null))
        return;
      for (int index2 = 0; index2 < jsonObject.enemy.Length; ++index2)
      {
        NPCSetting npcSetting = new NPCSetting(jsonObject.enemy[index2]);
        Unit data = new Unit();
        if (data != null && data.Setup((UnitData) null, (UnitSetting) npcSetting, (Unit.DropItem) null, (Unit.DropItem) null) && !data.IsGimmick)
        {
          GameObject root;
          if (index1 + 1 > this.mEnemyObject.Count)
          {
            root = (GameObject) Object.Instantiate<GameObject>((M0) this.EnemyTemplate);
            if (!Object.op_Equality((Object) root, (Object) null))
              this.mEnemyObject.Add(root);
            else
              continue;
          }
          else
            root = this.mEnemyObject[index1];
          DataSource.Bind<Unit>(root, data);
          GameParameter.UpdateAll(root);
          if (Object.op_Inequality((Object) this.EnemyRoot, (Object) null))
            root.get_transform().SetParent(this.EnemyRoot.get_transform(), false);
          root.SetActive(true);
          ++index1;
        }
      }
      for (int index2 = index1; index2 < this.mEnemyObject.Count; ++index2)
        this.mEnemyObject[index2].SetActive(false);
      this.EnemyTemplate.SetActive(false);
    }

    private void SetReward(MultiTowerFloorParam param)
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      List<MultiTowerRewardItem> mtFloorReward = instance.GetMTFloorReward(param.reward_id, instance.GetMTRound((int) param.floor));
      if (mtFloorReward == null || Object.op_Equality((Object) this.RewardTemplate, (Object) null))
        return;
      MultiTowerRewardItem data = mtFloorReward.Count <= 0 ? (MultiTowerRewardItem) null : mtFloorReward[0];
      if (!Object.op_Inequality((Object) this.RewardTemplate, (Object) null))
        return;
      DataSource.Bind<MultiTowerRewardItem>(this.RewardTemplate, data);
      MultiTowerRewardInfo component = (MultiTowerRewardInfo) this.RewardTemplate.GetComponent<MultiTowerRewardInfo>();
      if (!Object.op_Inequality((Object) component, (Object) null))
        return;
      component.Refresh();
    }
  }
}
