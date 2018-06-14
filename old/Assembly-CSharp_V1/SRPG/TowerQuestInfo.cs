// Decompiled with JetBrains decompiler
// Type: SRPG.TowerQuestInfo
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(0, "更新", FlowNode.PinTypes.Input, 0)]
  public class TowerQuestInfo : MonoBehaviour, IFlowInterface
  {
    [SerializeField]
    private GameObject EnemiesRoot;
    [SerializeField]
    private GameObject EnemyTemplate;
    [SerializeField]
    private GameObject EnemyTemplateUnKnown;
    [SerializeField]
    private Text RewardText;
    [SerializeField]
    private Text RecommendText;
    [SerializeField]
    private GameObject ItemRoot;
    [SerializeField]
    private GameObject ArtifactRoot;
    [SerializeField]
    private GameObject CoinRoot;
    [SerializeField]
    private GameObject UnkownIcon;
    [SerializeField]
    private GameObject ClearIcon;
    [SerializeField]
    private GameObject DetailtTmplate;
    private GameObject Detail;
    private List<TowerEnemyListItem> EnemyList;
    private List<TowerEnemyListItem> UnknownEnemyList;
    private List<Unit> EnemyUnits;
    private string FloorID;

    public TowerQuestInfo()
    {
      base.\u002Ector();
    }

    public void Activated(int pinID)
    {
      if (pinID != 0 || !string.IsNullOrEmpty(this.FloorID) && !(this.FloorID != GlobalVars.SelectedQuestID))
        return;
      this.Refresh();
    }

    private void Awake()
    {
      GameUtility.SetGameObjectActive(this.EnemyTemplate, false);
      GameUtility.SetGameObjectActive(this.UnkownIcon, false);
      GameUtility.SetGameObjectActive(this.ClearIcon, false);
    }

    private void SetRecommendText(int lv, int joblv)
    {
      if (Object.op_Equality((Object) this.RecommendText, (Object) null))
        return;
      this.RecommendText.set_text(LocalizedText.Get("sys.TOWER_RECOMMENDATION_TEXT", (object) lv, (object) joblv));
    }

    private void SetEnemies(JSON_MapEnemyUnit[] json)
    {
      for (int index = 0; index < this.UnknownEnemyList.Count; ++index)
        ((Component) this.UnknownEnemyList[index]).get_gameObject().SetActive(false);
      List<JSON_MapEnemyUnit> list = ((IEnumerable<JSON_MapEnemyUnit>) json).Where<JSON_MapEnemyUnit>((Func<JSON_MapEnemyUnit, bool>) (enemy =>
      {
        if (enemy.elem != 0)
          return enemy.side == 1;
        return false;
      })).ToList<JSON_MapEnemyUnit>();
      TowerResuponse towerResuponse = MonoSingleton<GameManager>.Instance.TowerResuponse;
      TowerFloorParam towerFloorParam = (TowerFloorParam) null;
      if (towerResuponse != null)
        towerFloorParam = towerResuponse.GetCurrentFloor();
      this.EnemyUnits.Clear();
      for (int index = 0; index < list.Count; ++index)
      {
        TowerEnemyListItem towerEnemyListItem;
        if (this.EnemyList.Count <= index)
        {
          towerEnemyListItem = (TowerEnemyListItem) ((GameObject) Object.Instantiate<GameObject>((M0) this.EnemyTemplate)).GetComponent<TowerEnemyListItem>();
          ((Component) towerEnemyListItem).get_transform().SetParent(this.EnemiesRoot.get_transform(), false);
          this.EnemyList.Add(towerEnemyListItem);
        }
        else
          towerEnemyListItem = this.EnemyList[index];
        NPCSetting npcSetting = new NPCSetting(list[index]);
        Unit data = new Unit();
        data.Setup((UnitData) null, (UnitSetting) npcSetting, (Unit.DropItem) null, (Unit.DropItem) null);
        DataSource.Bind<Unit>(((Component) towerEnemyListItem).get_gameObject(), data);
        this.EnemyUnits.Add(data);
      }
      if (towerFloorParam != null && towerFloorParam.iname == GlobalVars.SelectedQuestID)
        MonoSingleton<GameManager>.Instance.TowerResuponse.CalcEnemyDamage(this.EnemyUnits);
      for (int index = 0; index < this.EnemyList.Count; ++index)
      {
        bool flag = index < list.Count && index < 10;
        ((Component) this.EnemyList[index]).get_gameObject().SetActive(flag);
        this.EnemyList[index].UpdateValue();
      }
    }

    private void SetRewards(TowerRewardParam rewardParam)
    {
      if (rewardParam == null || Object.op_Equality((Object) this.RewardText, (Object) null))
        return;
      GameUtility.SetGameObjectActive(this.ItemRoot, false);
      GameUtility.SetGameObjectActive(this.ArtifactRoot, false);
      GameUtility.SetGameObjectActive(this.CoinRoot, false);
      GameManager instanceDirect = MonoSingleton<GameManager>.GetInstanceDirect();
      using (List<TowerRewardItem>.Enumerator enumerator = rewardParam.GetTowerRewardItem().GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          TowerRewardItem current = enumerator.Current;
          if (current.visible && current.type != TowerRewardItem.RewardType.Gold)
          {
            string str = string.Empty;
            switch (current.type)
            {
              case TowerRewardItem.RewardType.Item:
                ItemParam itemParam = instanceDirect.GetItemParam(current.iname);
                if (itemParam != null)
                  str = itemParam.name;
                DataSource.Bind<ItemParam>(this.ItemRoot, itemParam);
                GameUtility.SetGameObjectActive(this.ItemRoot, true);
                GameParameter.UpdateAll(this.ItemRoot);
                break;
              case TowerRewardItem.RewardType.Coin:
                str = LocalizedText.Get("sys.COIN");
                this.CoinRoot.get_gameObject().SetActive(true);
                break;
              case TowerRewardItem.RewardType.ArenaCoin:
                str = LocalizedText.Get("sys.ARENA_COIN");
                break;
              case TowerRewardItem.RewardType.MultiCoin:
                str = LocalizedText.Get("sys.MULTI_COIN");
                break;
              case TowerRewardItem.RewardType.KakeraCoin:
                str = LocalizedText.Get("sys.PIECE_POINT");
                break;
              case TowerRewardItem.RewardType.Artifact:
                ArtifactParam artifactParam = instanceDirect.MasterParam.GetArtifactParam(current.iname);
                if (artifactParam != null)
                  str = artifactParam.name;
                DataSource.Bind<ArtifactParam>(this.ArtifactRoot, artifactParam);
                GameUtility.SetGameObjectActive(this.ArtifactRoot, true);
                GameParameter.UpdateAll(this.ArtifactRoot);
                break;
            }
            this.RewardText.set_text(string.Format("{0} × {1}", (object) str, (object) current.num));
            break;
          }
        }
      }
    }

    public void Refresh()
    {
      TowerFloorParam towerFloor = MonoSingleton<GameManager>.Instance.FindTowerFloor(GlobalVars.SelectedQuestID);
      if (towerFloor == null)
        return;
      QuestParam questParam = towerFloor.GetQuestParam();
      DataSource.Bind<QuestParam>(((Component) this).get_gameObject(), questParam);
      this.SetRecommendText((int) towerFloor.lv, (int) towerFloor.joblv);
      int downloadAssetNum = ((FlowNode_DownloadTowerMapSets) ((Component) this).GetComponentInParent<FlowNode_DownloadTowerMapSets>()).DownloadAssetNum;
      TowerFloorParam currentFloor = MonoSingleton<GameManager>.Instance.TowerResuponse.GetCurrentFloor();
      if (currentFloor == null)
        return;
      if ((int) towerFloor.FloorIndex < (int) currentFloor.FloorIndex + downloadAssetNum)
      {
        if (questParam.state == QuestStates.Cleared)
        {
          GameUtility.SetGameObjectActive(this.UnkownIcon, false);
          GameUtility.SetGameObjectActive((Component) this.RewardText, true);
          GameUtility.SetGameObjectActive(this.ClearIcon, true);
          for (int index = 0; index < this.EnemyList.Count; ++index)
            ((Component) this.EnemyList[index]).get_gameObject().SetActive(false);
          for (int index = 0; index < this.UnknownEnemyList.Count; ++index)
            ((Component) this.UnknownEnemyList[index]).get_gameObject().SetActive(false);
          this.SetRewards(MonoSingleton<GameManager>.Instance.FindTowerReward(towerFloor.reward_id));
        }
        else
        {
          string path = AssetPath.LocalMap(towerFloor.map[0].mapSetName);
          string src = AssetManager.LoadTextData(path);
          if (string.IsNullOrEmpty(src))
          {
            DebugUtility.LogError("配置ファイルがありません : QuestIname = " + towerFloor.iname + ",SetFilePath = " + path);
            return;
          }
          JSON_MapUnit jsonObject = JSONParser.parseJSONObject<JSON_MapUnit>(src);
          GameUtility.SetGameObjectActive(this.UnkownIcon, false);
          GameUtility.SetGameObjectActive((Component) this.RewardText, true);
          GameUtility.SetGameObjectActive(this.ClearIcon, false);
          TowerResuponse towerResuponse = MonoSingleton<GameManager>.Instance.TowerResuponse;
          if ((int) jsonObject.is_rand > 0)
          {
            if (towerResuponse.lot_enemies == null || (int) towerFloor.FloorIndex > (int) currentFloor.FloorIndex)
            {
              for (int index = 0; index < this.EnemyList.Count; ++index)
                ((Component) this.EnemyList[index]).get_gameObject().SetActive(false);
              this.EnemyTemplateUnKnown.SetActive(true);
              int num = 0;
              for (int index = 0; index < towerFloor.rand_tag.Length; ++index)
                num += (int) towerFloor.rand_tag[index];
              for (int index = 0; index < num; ++index)
              {
                if (index >= this.UnknownEnemyList.Count)
                {
                  TowerEnemyListItem component = (TowerEnemyListItem) ((GameObject) Object.Instantiate<GameObject>((M0) this.EnemyTemplateUnKnown)).GetComponent<TowerEnemyListItem>();
                  ((Component) component).get_transform().SetParent(this.EnemiesRoot.get_transform(), false);
                  this.UnknownEnemyList.Add(component);
                }
                ((Component) this.UnknownEnemyList[index]).get_gameObject().SetActive(true);
              }
              for (int index = num; index < this.UnknownEnemyList.Count; ++index)
                ((Component) this.UnknownEnemyList[index]).get_gameObject().SetActive(false);
              this.EnemyTemplateUnKnown.SetActive(false);
            }
            else
            {
              jsonObject.enemy = new JSON_MapEnemyUnit[towerResuponse.lot_enemies.Length];
              for (int index = 0; index < jsonObject.enemy.Length; ++index)
                jsonObject.enemy[index] = jsonObject.deck[(int) towerResuponse.lot_enemies[index]];
              this.SetEnemies(jsonObject.enemy);
            }
          }
          else if (jsonObject.enemy != null)
            this.SetEnemies(jsonObject.enemy);
          this.SetRewards(MonoSingleton<GameManager>.Instance.FindTowerReward(towerFloor.reward_id));
        }
      }
      else
      {
        GameUtility.SetGameObjectActive(this.UnkownIcon, true);
        GameUtility.SetGameObjectActive((Component) this.RewardText, true);
        GameUtility.SetGameObjectActive(this.ClearIcon, false);
        if (Object.op_Inequality((Object) this.UnkownIcon, (Object) null))
        {
          Text component = (Text) this.UnkownIcon.GetComponent<Text>();
          if (Object.op_Inequality((Object) component, (Object) null))
            component.set_text(LocalizedText.Get("sys.TOWER_UNKNOWN_TEXT", new object[1]
            {
              (object) ((int) towerFloor.FloorIndex - downloadAssetNum + 1)
            }));
        }
        for (int index = 0; index < this.EnemyList.Count; ++index)
          ((Component) this.EnemyList[index]).get_gameObject().SetActive(false);
        for (int index = 0; index < this.UnknownEnemyList.Count; ++index)
          ((Component) this.UnknownEnemyList[index]).get_gameObject().SetActive(false);
        this.SetRewards(MonoSingleton<GameManager>.Instance.FindTowerReward(towerFloor.reward_id));
      }
      GameParameter.UpdateAll(((Component) this).get_gameObject());
      this.FloorID = GlobalVars.SelectedQuestID;
    }

    public void OnClickDetail()
    {
      QuestParam dataOfClass = DataSource.FindDataOfClass<QuestParam>(((Component) this).get_gameObject(), (QuestParam) null);
      if (!Object.op_Equality((Object) this.Detail, (Object) null) || dataOfClass == null)
        return;
      this.Detail = (GameObject) Object.Instantiate<GameObject>((M0) this.DetailtTmplate);
      DataSource.Bind<QuestParam>(this.Detail, dataOfClass);
      QuestCampaignData[] questCampaigns = MonoSingleton<GameManager>.Instance.FindQuestCampaigns(dataOfClass);
      DataSource.Bind<QuestCampaignData[]>(this.Detail, questCampaigns.Length != 0 ? questCampaigns : (QuestCampaignData[]) null);
      TowerResuponse towerResuponse = MonoSingleton<GameManager>.Instance.TowerResuponse;
      TowerFloorParam data = (TowerFloorParam) null;
      if (towerResuponse != null)
        data = towerResuponse.GetCurrentFloor();
      DataSource.Bind<TowerFloorParam>(this.Detail, data);
      this.Detail.SetActive(true);
    }
  }
}
