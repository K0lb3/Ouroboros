// Decompiled with JetBrains decompiler
// Type: SRPG.TowerBattleReset
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(0, "初期化（実行）", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(100, "初期化（終了）", FlowNode.PinTypes.Output, 100)]
  public class TowerBattleReset : MonoBehaviour, IFlowInterface
  {
    private const int INPUT_INITIALIZE_BEGIN = 0;
    private const int OUTPUT_INITIALIZE_END = 100;
    [SerializeField]
    private GameObject TollRecovery;
    [SerializeField]
    private GameObject FreeRecovery;
    [SerializeField]
    private GameObject HaveItemObject;
    [SerializeField]
    private Text HaveItemNum;
    [SerializeField]
    [HeaderBar("▼ミッション表示エリアのタイトル")]
    private Text m_MissionListTitle;
    [SerializeField]
    private Text m_MissionListTitleNoItem;
    [HeaderBar("▼ミッション表示リストの親")]
    [SerializeField]
    private RectTransform m_MissionListParent;
    [HeaderBar("▼ミッション表示用テンプレート")]
    [SerializeField]
    private QuestMissionItem m_MissionItemTemplate;
    private List<GameObject> m_MissionListItems;

    public TowerBattleReset()
    {
      base.\u002Ector();
    }

    public void Activated(int pinID)
    {
      if (pinID != 0)
        return;
      this.InitializeMissionItems();
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
    }

    private void Start()
    {
      GameUtility.SetGameObjectActive((Component) this.m_MissionItemTemplate, false);
      GameUtility.SetGameObjectActive((Component) this.m_MissionListTitle, false);
      GameUtility.SetGameObjectActive((Component) this.m_MissionListTitleNoItem, false);
      GameUtility.SetGameObjectActive((Component) this.m_MissionListParent, false);
      TowerParam tower = MonoSingleton<GameManager>.Instance.FindTower(GlobalVars.SelectedTowerID);
      if (tower == null)
        return;
      if (tower.floor_reset_coin > (short) 0)
      {
        this.TollRecovery.SetActive(true);
        this.FreeRecovery.SetActive(false);
        ((Text) this.TollRecovery.GetComponent<Text>()).set_text(string.Format(LocalizedText.Get("sys.TOWER_BATTLE_RESET_DESCRIPTION_02"), (object) tower.floor_reset_coin.ToString()));
        this.HaveItemNum.set_text(MonoSingleton<GameManager>.Instance.Player.Coin.ToString());
      }
      else
      {
        this.TollRecovery.SetActive(false);
        this.FreeRecovery.SetActive(true);
        this.HaveItemObject.SetActive(false);
      }
    }

    private void InitializeMissionItems()
    {
      this.CreateResetMissionItems(MonoSingleton<GameManager>.Instance.FindQuest(GlobalVars.SelectedQuestID));
      if (this.m_MissionListItems != null && this.m_MissionListItems.Count > 0)
      {
        GameUtility.SetGameObjectActive((Component) this.m_MissionListTitle, true);
      }
      else
      {
        GameUtility.SetGameObjectActive((Component) this.m_MissionListTitleNoItem, true);
        GameUtility.SetGameObjectActive((Component) this.m_MissionListParent, false);
      }
    }

    private void CreateResetMissionItems(QuestParam questParam)
    {
      this.DeleteMissionItems();
      if (questParam == null || questParam.bonusObjective == null || (Object.op_Equality((Object) this.m_MissionListParent, (Object) null) || Object.op_Equality((Object) this.m_MissionItemTemplate, (Object) null)))
        return;
      if (this.m_MissionListItems == null)
        this.m_MissionListItems = new List<GameObject>();
      DataSource.Bind<QuestParam>(((Component) this).get_gameObject(), questParam);
      for (int index = 0; index < questParam.bonusObjective.Length; ++index)
      {
        QuestBonusObjective bonusObjective = questParam.bonusObjective[index];
        if (!questParam.IsMissionClear(index) && bonusObjective.IsProgressMission())
        {
          QuestMissionItem missionItem = this.CreateMissionItem(bonusObjective);
          missionItem.SetGameParameterIndex(index);
          this.m_MissionListItems.Add(((Component) missionItem).get_gameObject());
          GameParameter.UpdateAll(((Component) missionItem).get_gameObject());
        }
      }
    }

    private QuestMissionItem CreateMissionItem(QuestBonusObjective bonusObjective)
    {
      if (bonusObjective == null)
        return (QuestMissionItem) null;
      GameObject gameObject = (GameObject) Object.Instantiate<GameObject>((M0) ((Component) this.m_MissionItemTemplate).get_gameObject());
      gameObject.SetActive(true);
      gameObject.get_transform().SetParent(((Component) this.m_MissionListParent).get_transform(), false);
      return (QuestMissionItem) gameObject.GetComponent<QuestMissionItem>();
    }

    private void DeleteMissionItems()
    {
      if (this.m_MissionListItems == null)
        return;
      for (int index = 0; index < this.m_MissionListItems.Count; ++index)
      {
        Object.Destroy((Object) this.m_MissionListItems[index]);
        this.m_MissionListItems[index] = (GameObject) null;
      }
      this.m_MissionListItems.Clear();
    }
  }
}
