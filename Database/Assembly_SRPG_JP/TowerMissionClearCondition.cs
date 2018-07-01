// Decompiled with JetBrains decompiler
// Type: SRPG.TowerMissionClearCondition
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(0, "表示内容更新", FlowNode.PinTypes.Input, 0)]
  public class TowerMissionClearCondition : MonoBehaviour, IFlowInterface
  {
    private const int INPUT_REFRESH_MISSION = 0;
    [SerializeField]
    [HeaderBar("▼ウィンドウのタイトル")]
    private Text TowerName;
    [SerializeField]
    private Text TowerFloorNum;
    [HeaderBar("▼吹き出し内のオブジェクト")]
    [SerializeField]
    private Text ClearConditionText01;
    [SerializeField]
    private Text ClearConditionText02;
    [SerializeField]
    private ImageArray ClearConditionImage01;
    [SerializeField]
    private ImageArray ClearConditionImage02;
    [HeaderBar("▼ミッション表示エリアのタイトル")]
    [SerializeField]
    private Text m_MissionListTitle;
    [SerializeField]
    private Text m_MissionListTitleNoItem;
    [SerializeField]
    [HeaderBar("▼ミッション表示リストの親")]
    private RectTransform m_MissionListParent;
    [HeaderBar("▼ミッション表示用テンプレート")]
    [SerializeField]
    private QuestMissionItem m_MissionItemTemplate;
    private List<GameObject> m_MissionListItems;

    public TowerMissionClearCondition()
    {
      base.\u002Ector();
    }

    private void Start()
    {
      GameUtility.SetGameObjectActive((Component) this.m_MissionItemTemplate, false);
      GameUtility.SetGameObjectActive((Component) this.m_MissionListTitle, false);
      GameUtility.SetGameObjectActive((Component) this.m_MissionListTitleNoItem, false);
      this.Refresh();
    }

    private void Refresh()
    {
      TowerParam tower = MonoSingleton<GameManager>.Instance.FindTower(GlobalVars.SelectedTowerID);
      if (tower == null)
        return;
      TowerFloorParam towerFloor = MonoSingleton<GameManager>.Instance.FindTowerFloor(GlobalVars.SelectedQuestID);
      if (towerFloor == null)
        return;
      QuestParam questParam = towerFloor.GetQuestParam();
      if (questParam == null || questParam.bonusObjective == null)
        return;
      int clearMissionNum = questParam.GetClearMissionNum();
      int length = questParam.bonusObjective.Length;
      if (questParam.IsMissionCompleteALL())
      {
        this.ClearConditionText01.set_text(string.Format(LocalizedText.Get("sys.TOWER_CLEAR_CINDITION_01_CLEAR"), (object) clearMissionNum, (object) length));
        this.ClearConditionImage01.ImageIndex = 0;
      }
      else
      {
        this.ClearConditionText01.set_text(string.Format(LocalizedText.Get("sys.TOWER_CLEAR_CINDITION_01"), (object) clearMissionNum, (object) length));
        this.ClearConditionImage01.ImageIndex = 1;
      }
      if (questParam.state == QuestStates.Cleared)
      {
        this.ClearConditionText02.set_text(LocalizedText.Get("sys.TOWER_CLEAR_CINDITION_02_CLEAR"));
        this.ClearConditionImage02.ImageIndex = 0;
      }
      else
      {
        this.ClearConditionText02.set_text(LocalizedText.Get("sys.TOWER_CLEAR_CINDITION_02"));
        this.ClearConditionImage02.ImageIndex = 1;
      }
      if (Object.op_Inequality((Object) this.TowerName, (Object) null))
        this.TowerName.set_text(tower.name);
      if (Object.op_Inequality((Object) this.TowerFloorNum, (Object) null))
        this.TowerFloorNum.set_text(towerFloor.name);
      this.CreateResetMissionItems(questParam);
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

    public void Activated(int pinID)
    {
      if (pinID != 0)
        return;
      this.Refresh();
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
