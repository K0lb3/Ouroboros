// Decompiled with JetBrains decompiler
// Type: SRPG.TowerQuestInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(0, "表示更新", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1200, "クエスト詳細ウィンドウ（開いた）", FlowNode.PinTypes.Output, 1200)]
  [FlowNode.Pin(210, "クエスト詳細ウィンドウ（閉じる）", FlowNode.PinTypes.Input, 210)]
  [FlowNode.Pin(200, "クエスト詳細ウィンドウ（開く）", FlowNode.PinTypes.Input, 200)]
  [FlowNode.Pin(10, "表示更新(強制)", FlowNode.PinTypes.Input, 10)]
  public class TowerQuestInfo : MonoBehaviour, IFlowInterface
  {
    private const int INPUT_REFRESH_UI = 0;
    private const int INPUT_FORCE_REFRESH_UI = 10;
    private const int INPUT_QUEST_DETAIL_OPEN = 200;
    private const int INPUT_QUEST_DETAIL_CLOSE = 210;
    private const int OUTPUT_QUEST_DETAIL_OPENED = 1200;
    [SerializeField]
    [HeaderBar("▼敵情報表示する用")]
    private GameObject EnemiesRoot;
    [SerializeField]
    private GameObject EnemyTemplate;
    [SerializeField]
    private GameObject EnemyTemplateUnKnown;
    [HeaderBar("▼報酬表示用")]
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
    [HeaderBar("▼敵情報が表示できない階層に表示される")]
    [SerializeField]
    private GameObject UnkownIcon;
    [HeaderBar("▼クリア済みの階層に表示される")]
    [SerializeField]
    private GameObject ClearIcon;
    [StringIsResourcePath(typeof (GameObject))]
    [HeaderBar("▼詳細表示用プレハブ")]
    [SerializeField]
    private string Path_TowerQuestDetail;
    [SerializeField]
    [HeaderBar("▼バトルリセットボタン")]
    private Button BattleResetButton;
    [SerializeField]
    [HeaderBar("▼リセットに必要な幻晶石数")]
    private Text m_BattleResetCost;
    private GameObject m_TowerQuestDetail;
    private List<TowerEnemyListItem> EnemyList;
    private List<TowerEnemyListItem> UnknownEnemyList;
    private string FloorID;
    private Coroutine m_PrefabLoadRoutine;

    public TowerQuestInfo()
    {
      base.\u002Ector();
    }

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 0:
          if (string.IsNullOrEmpty(this.FloorID) || this.FloorID != GlobalVars.SelectedQuestID)
            this.Refresh();
          this.CheckBattleResetButton();
          break;
        case 10:
          this.Refresh();
          this.CheckBattleResetButton();
          break;
        case 200:
          this.OnQuestDetailOpen();
          break;
        case 210:
          this.Refresh();
          break;
      }
    }

    private void Awake()
    {
      GameUtility.SetGameObjectActive(this.EnemyTemplate, false);
      GameUtility.SetGameObjectActive(this.UnkownIcon, false);
      GameUtility.SetGameObjectActive(this.ClearIcon, false);
      GameUtility.SetGameObjectActive(this.EnemyTemplateUnKnown, false);
    }

    private void OnDestroy()
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_TowerQuestDetail, (UnityEngine.Object) null))
        return;
      UnityEngine.Object.DestroyObject((UnityEngine.Object) this.m_TowerQuestDetail);
      this.m_TowerQuestDetail = (GameObject) null;
    }

    private void SetRecommendText(int lv, int joblv)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.RecommendText, (UnityEngine.Object) null))
        return;
      this.RecommendText.set_text(LocalizedText.Get("sys.TOWER_RECOMMENDATION_TEXT", (object) lv, (object) joblv));
    }

    private void HideAllEnemyIcon()
    {
      for (int index = 0; index < this.UnknownEnemyList.Count; ++index)
        ((Component) this.UnknownEnemyList[index]).get_gameObject().SetActive(false);
      for (int index = 0; index < this.EnemyList.Count; ++index)
        ((Component) this.EnemyList[index]).get_gameObject().SetActive(false);
    }

    private void SetIcon(List<TowerQuestInfo.EnemyIconData> icon_datas)
    {
      for (int index = 0; index < icon_datas.Count; ++index)
      {
        bool flag = icon_datas[index].unit == null;
        GameObject gameObject = this.EnemyTemplate;
        List<TowerEnemyListItem> towerEnemyListItemList = this.EnemyList;
        if (flag)
        {
          gameObject = this.EnemyTemplateUnKnown;
          towerEnemyListItemList = this.UnknownEnemyList;
        }
        TowerEnemyListItem component;
        if (towerEnemyListItemList.Count <= index)
        {
          component = (TowerEnemyListItem) ((GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) gameObject)).GetComponent<TowerEnemyListItem>();
          ((Component) component).get_transform().SetParent(this.EnemiesRoot.get_transform(), false);
          ((Component) component).get_gameObject().SetActive(true);
          towerEnemyListItemList.Add(component);
        }
        else
        {
          component = towerEnemyListItemList[index];
          ((Component) component).get_gameObject().SetActive(true);
        }
        if (!flag)
        {
          DataSource.Bind<Unit>(((Component) component).get_gameObject(), icon_datas[index].unit);
          component.UpdateValue();
        }
      }
      int num = 0;
      for (int index = 0; index < this.EnemyList.Count; ++index)
      {
        ((Component) this.EnemyList[index]).get_transform().SetSiblingIndex(num);
        ++num;
      }
      for (int index = 0; index < this.UnknownEnemyList.Count; ++index)
      {
        ((Component) this.UnknownEnemyList[index]).get_transform().SetSiblingIndex(num);
        ++num;
      }
    }

    private void SetRewards(TowerRewardParam rewardParam)
    {
      if (rewardParam == null || UnityEngine.Object.op_Equality((UnityEngine.Object) this.RewardText, (UnityEngine.Object) null))
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
          this.HideAllEnemyIcon();
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
          this.HideAllEnemyIcon();
          if (jsonObject.enemy != null)
          {
            TowerFloorParam towerFloorParam = (TowerFloorParam) null;
            if (MonoSingleton<GameManager>.Instance.TowerResuponse != null)
              towerFloorParam = MonoSingleton<GameManager>.Instance.TowerResuponse.GetCurrentFloor();
            bool flag1 = towerResuponse.lot_enemies != null && towerResuponse.lot_enemies.Length > 0;
            bool flag2 = towerFloor.iname == currentFloor.iname;
            if (flag1 && flag2)
              jsonObject.enemy = jsonObject.ReplacedRandEnemy(towerResuponse.lot_enemies, false);
            List<TowerQuestInfo.EnemyIconData> enemyIconDataList1 = new List<TowerQuestInfo.EnemyIconData>();
            for (int i = 0; i < jsonObject.enemy.Length; ++i)
            {
              bool flag3 = false;
              if (jsonObject.enemy[i].IsRandSymbol)
                flag3 = true;
              if (flag1 && flag2)
                flag3 = Array.Find<RandDeckResult>(towerResuponse.lot_enemies, (Predicate<RandDeckResult>) (lot => lot.set_id == i)) != null;
              Unit unit = (Unit) null;
              if (!jsonObject.enemy[i].IsRandSymbol)
              {
                NPCSetting npcSetting = new NPCSetting(jsonObject.enemy[i]);
                unit = new Unit();
                unit.Setup((UnitData) null, (UnitSetting) npcSetting, (Unit.DropItem) null, (Unit.DropItem) null);
              }
              enemyIconDataList1.Add(new TowerQuestInfo.EnemyIconData()
              {
                unit = unit,
                enemy = jsonObject.enemy[i],
                is_rand = flag3
              });
            }
            List<TowerQuestInfo.EnemyIconData> enemyIconDataList2 = new List<TowerQuestInfo.EnemyIconData>();
            for (int index = 0; index < enemyIconDataList1.Count; ++index)
            {
              if (!enemyIconDataList1[index].enemy.IsRandSymbol && !enemyIconDataList1[index].unit.IsGimmick)
                enemyIconDataList2.Add(enemyIconDataList1[index]);
            }
            List<TowerQuestInfo.EnemyIconData> enemyIconDataList3 = new List<TowerQuestInfo.EnemyIconData>((IEnumerable<TowerQuestInfo.EnemyIconData>) enemyIconDataList2);
            if (towerFloorParam != null && towerFloorParam.iname == GlobalVars.SelectedQuestID && MonoSingleton<GameManager>.Instance.TowerEnemyUnit != null)
            {
              for (int index = 0; index < enemyIconDataList3.Count; ++index)
              {
                if (enemyIconDataList3[index].unit != null)
                {
                  int num = (int) enemyIconDataList3[index].unit.MaximumStatus.param.hp - MonoSingleton<GameManager>.Instance.TowerEnemyUnit[index].hp;
                  enemyIconDataList3[index].unit.Damage(num, false);
                }
              }
            }
            List<TowerQuestInfo.EnemyIconData> icon_datas = new List<TowerQuestInfo.EnemyIconData>();
            List<TowerQuestInfo.EnemyIconData> enemyIconDataList4 = new List<TowerQuestInfo.EnemyIconData>();
            List<TowerQuestInfo.EnemyIconData> enemyIconDataList5 = new List<TowerQuestInfo.EnemyIconData>();
            for (int index = 0; index < enemyIconDataList3.Count; ++index)
            {
              if (!enemyIconDataList3[index].is_rand)
                enemyIconDataList4.Add(enemyIconDataList3[index]);
            }
            if (towerResuponse.lot_enemies == null || (int) towerFloor.FloorIndex > (int) currentFloor.FloorIndex)
            {
              if (jsonObject.deck != null && jsonObject.deck.Length > 0 && (jsonObject.rand_tag != null && jsonObject.rand_tag.Length > 0))
              {
                int num = 0;
                for (int index = 0; index < jsonObject.rand_tag.Length; ++index)
                  num += jsonObject.rand_tag[index].spawn;
                for (int index = 0; index < num; ++index)
                {
                  TowerQuestInfo.EnemyIconData enemyIconData = new TowerQuestInfo.EnemyIconData();
                  enemyIconDataList5.Add(enemyIconData);
                }
              }
            }
            else
            {
              for (int index = 0; index < enemyIconDataList3.Count; ++index)
              {
                if (enemyIconDataList3[index].is_rand)
                  enemyIconDataList5.Add(enemyIconDataList3[index]);
              }
            }
            icon_datas.AddRange((IEnumerable<TowerQuestInfo.EnemyIconData>) enemyIconDataList4);
            icon_datas.AddRange((IEnumerable<TowerQuestInfo.EnemyIconData>) enemyIconDataList5);
            this.SetIcon(icon_datas);
          }
          this.SetRewards(MonoSingleton<GameManager>.Instance.FindTowerReward(towerFloor.reward_id));
        }
      }
      else
      {
        GameUtility.SetGameObjectActive(this.UnkownIcon, true);
        GameUtility.SetGameObjectActive((Component) this.RewardText, true);
        GameUtility.SetGameObjectActive(this.ClearIcon, false);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.UnkownIcon, (UnityEngine.Object) null))
        {
          Text component = (Text) this.UnkownIcon.GetComponent<Text>();
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
            component.set_text(LocalizedText.Get("sys.TOWER_UNKNOWN_TEXT", new object[1]
            {
              (object) ((int) towerFloor.FloorIndex - downloadAssetNum + 1)
            }));
        }
        this.HideAllEnemyIcon();
        this.SetRewards(MonoSingleton<GameManager>.Instance.FindTowerReward(towerFloor.reward_id));
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_BattleResetCost, (UnityEngine.Object) null))
        this.m_BattleResetCost.set_text(MonoSingleton<GameManager>.Instance.FindTower(towerFloor.tower_id).floor_reset_coin.ToString());
      GameParameter.UpdateAll(((Component) this).get_gameObject());
      this.FloorID = GlobalVars.SelectedQuestID;
    }

    public void OnQuestDetailOpen()
    {
      if (this.m_PrefabLoadRoutine != null)
        return;
      this.m_PrefabLoadRoutine = this.StartCoroutine(this.PrefabLoadRoutine(this.Path_TowerQuestDetail, new Action<UnityEngine.Object>(this.CreateQuestDetailWindow)));
    }

    private void CreateQuestDetailWindow(UnityEngine.Object template)
    {
      QuestParam dataOfClass = DataSource.FindDataOfClass<QuestParam>(((Component) this).get_gameObject(), (QuestParam) null);
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.m_TowerQuestDetail, (UnityEngine.Object) null) && dataOfClass != null)
      {
        this.m_TowerQuestDetail = UnityEngine.Object.Instantiate(template) as GameObject;
        DataSource.Bind<QuestParam>(this.m_TowerQuestDetail, dataOfClass);
        QuestCampaignData[] questCampaigns = MonoSingleton<GameManager>.Instance.FindQuestCampaigns(dataOfClass);
        DataSource.Bind<QuestCampaignData[]>(this.m_TowerQuestDetail, questCampaigns.Length != 0 ? questCampaigns : (QuestCampaignData[]) null);
        TowerResuponse towerResuponse = MonoSingleton<GameManager>.Instance.TowerResuponse;
        TowerScore componentInChildren = (TowerScore) this.m_TowerQuestDetail.GetComponentInChildren<TowerScore>();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) componentInChildren, (UnityEngine.Object) null) && towerResuponse != null)
        {
          componentInChildren.SetScoreTitleText(dataOfClass.name);
          componentInChildren.Setup(towerResuponse.FloorScores, towerResuponse.FloorSpdRank, towerResuponse.FloorTecRank);
        }
        this.m_TowerQuestDetail.SetActive(true);
      }
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 1200);
    }

    [DebuggerHidden]
    private IEnumerator PrefabLoadRoutine(string path, Action<UnityEngine.Object> onLoadComplete)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new TowerQuestInfo.\u003CPrefabLoadRoutine\u003Ec__Iterator13F()
      {
        path = path,
        onLoadComplete = onLoadComplete,
        \u003C\u0024\u003Epath = path,
        \u003C\u0024\u003EonLoadComplete = onLoadComplete,
        \u003C\u003Ef__this = this
      };
    }

    private void CheckBattleResetButton()
    {
      TowerParam tower = MonoSingleton<GameManager>.Instance.FindTower(GlobalVars.SelectedTowerID);
      if (tower == null)
        return;
      TowerResuponse towerResuponse = MonoSingleton<GameManager>.Instance.TowerResuponse;
      if (towerResuponse == null)
        return;
      TowerFloorParam currentFloor = towerResuponse.GetCurrentFloor();
      if (currentFloor == null)
        return;
      if (MonoSingleton<GameManager>.Instance.Player.Coin > (int) tower.floor_reset_coin && GlobalVars.SelectedQuestID == currentFloor.iname && towerResuponse.CheckEnemyDeck())
        ((Selectable) this.BattleResetButton).set_interactable(true);
      else
        ((Selectable) this.BattleResetButton).set_interactable(false);
    }

    private class EnemyIconData
    {
      public bool is_rand;
      public JSON_MapEnemyUnit enemy;
      public Unit unit;
    }
  }
}
