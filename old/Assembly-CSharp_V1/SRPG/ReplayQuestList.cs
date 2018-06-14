// Decompiled with JetBrains decompiler
// Type: SRPG.ReplayQuestList
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(0, "更新", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(10, "クエストが選択された", FlowNode.PinTypes.Output, 101)]
  public class ReplayQuestList : SRPG_ListBase, IFlowInterface
  {
    public Dictionary<string, GameObject> mListItemTemplates = new Dictionary<string, GameObject>();
    public bool Descending = true;
    public bool RefreshOnStart = true;
    public bool ShowAllQuests = true;
    private List<QuestParam> mQuests = new List<QuestParam>();
    private const int PIN_ID_REFRESH = 0;
    private const int PIN_ID_SELECT = 10;
    public GameObject ItemTemplate;
    public GameObject ItemContainer;
    public ScrollRect ScrollRect;
    private string chapterName;

    public void Activated(int pinID)
    {
      if (pinID != 0)
        return;
      this.Refresh();
    }

    protected override void Start()
    {
      base.Start();
      ((Action<GameObject, bool>) ((lie, value) =>
      {
        if (!Object.op_Inequality((Object) lie, (Object) null) || !lie.get_activeInHierarchy())
          return;
        lie.SetActive(value);
      }))(this.ItemTemplate, false);
      this.Refresh();
    }

    private GameObject LoadQuestListItem(QuestParam param)
    {
      if (string.IsNullOrEmpty(param.ItemLayout))
        return (GameObject) null;
      if (this.mListItemTemplates.ContainsKey(param.ItemLayout))
        return this.mListItemTemplates[param.ItemLayout];
      GameObject gameObject = AssetManager.Load<GameObject>("QuestListItems/" + param.ItemLayout);
      if (Object.op_Inequality((Object) gameObject, (Object) null))
        this.mListItemTemplates.Add(param.ItemLayout, gameObject);
      return gameObject;
    }

    public void Refresh()
    {
      if (this.chapterName == null)
        this.chapterName = (string) GlobalVars.ReplaySelectedChapter;
      else if (this.chapterName.Equals((string) GlobalVars.ReplaySelectedChapter))
        return;
      this.chapterName = (string) GlobalVars.ReplaySelectedChapter;
      this.RefreshQuests();
      this.RefreshItems();
      if (!Object.op_Inequality((Object) this.ScrollRect, (Object) null))
        return;
      this.ScrollRect.set_normalizedPosition(Vector2.get_one());
    }

    private bool CheckQuest(QuestParam quest)
    {
      return !quest.IsMulti && quest.IsReplayDateUnlock(-1L) && (!string.IsNullOrEmpty(quest.event_start) || !string.IsNullOrEmpty(quest.event_clear)) && ((quest.state == QuestStates.Challenged || quest.state == QuestStates.Cleared) && (quest.state != QuestStates.Challenged || !string.IsNullOrEmpty(quest.event_start))) && (this.ShowAllQuests || !((string) GlobalVars.ReplaySelectedChapter != quest.ChapterID));
    }

    private void RefreshQuests()
    {
      this.mQuests.Clear();
      foreach (QuestParam availableQuest in MonoSingleton<GameManager>.Instance.Player.AvailableQuests)
      {
        if (this.CheckQuest(availableQuest))
          this.mQuests.Add(availableQuest);
      }
    }

    private void RefreshItems()
    {
      this.ClearItems();
      if (Object.op_Equality((Object) this.ItemTemplate, (Object) null))
        return;
      QuestParam[] array = this.mQuests.ToArray();
      if (this.Descending)
        Array.Reverse((Array) array);
      for (int index = 0; index < array.Length; ++index)
      {
        QuestParam data = array[index];
        GameObject gameObject1 = (GameObject) null;
        if (!string.IsNullOrEmpty(data.ItemLayout))
          gameObject1 = this.LoadQuestListItem(data);
        if (Object.op_Equality((Object) gameObject1, (Object) null))
          gameObject1 = this.ItemTemplate;
        if (!Object.op_Equality((Object) gameObject1, (Object) null))
        {
          GameObject gameObject2 = (GameObject) Object.Instantiate<GameObject>((M0) gameObject1);
          ((Object) gameObject2).set_hideFlags((HideFlags) 52);
          DataSource.Bind<QuestParam>(gameObject2, data);
          ListItemEvents component = (ListItemEvents) gameObject2.GetComponent<ListItemEvents>();
          component.OnSelect = new ListItemEvents.ListItemEvent(this.OnSelectItem);
          if (data.IsEvent)
            this.SetQuestTimerActive(gameObject2.get_transform(), false);
          gameObject2.get_transform().SetParent(this.ItemContainer.get_transform(), false);
          gameObject2.get_gameObject().SetActive(true);
          this.AddItem(component);
        }
      }
    }

    private void SetQuestTimerActive(Transform obj, bool value)
    {
      if (Object.op_Equality((Object) obj, (Object) null))
        return;
      QuestTimeLimit component = (QuestTimeLimit) ((Component) obj).GetComponent<QuestTimeLimit>();
      if (Object.op_Inequality((Object) component, (Object) null))
        ((Behaviour) component).set_enabled(value);
      Transform child1 = obj.FindChild("bg");
      if (!Object.op_Inequality((Object) child1, (Object) null))
        return;
      Transform child2 = child1.FindChild("timer_base");
      if (!Object.op_Inequality((Object) child2, (Object) null))
        return;
      ((Component) child2).get_gameObject().SetActive(value);
    }

    private void OnSelectItem(GameObject go)
    {
      QuestParam dataOfClass = DataSource.FindDataOfClass<QuestParam>(go, (QuestParam) null);
      if (dataOfClass == null)
        return;
      GlobalVars.ReplaySelectedQuestID.Set(dataOfClass.iname);
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 10);
    }
  }
}
