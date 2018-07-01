// Decompiled with JetBrains decompiler
// Type: SRPG.MultiPlayVersus_MapSelector
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  public class MultiPlayVersus_MapSelector : SRPG_FixedList
  {
    public GameObject ItemTemplate;
    public RectTransform ItemLayoutParent;
    public GameObject SelectWindow;
    public Button ConfirmButton;
    private List<VersusMapParam> m_Param;

    protected override void Start()
    {
      if (Object.op_Equality((Object) this.ItemTemplate, (Object) null))
        return;
      if (Object.op_Inequality((Object) this.ConfirmButton, (Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent) this.ConfirmButton.get_onClick()).AddListener(new UnityAction((object) this, __methodptr(OnConfirm)));
      }
      base.Start();
      this.RefleshData();
    }

    protected override GameObject CreateItem()
    {
      return (GameObject) Object.Instantiate<GameObject>((M0) this.ItemTemplate);
    }

    public override RectTransform ListParent
    {
      get
      {
        if (Object.op_Inequality((Object) this.ItemLayoutParent, (Object) null))
          return (RectTransform) ((Component) this.ItemLayoutParent).GetComponent<RectTransform>();
        return (RectTransform) null;
      }
    }

    protected void RefleshData()
    {
      string selectedQuestId = GlobalVars.SelectedQuestID;
      List<QuestParam> questTypeList = MonoSingleton<GameManager>.Instance.GetQuestTypeList(QuestTypes.VersusFree);
      if (questTypeList == null)
        return;
      this.m_Param = new List<VersusMapParam>(questTypeList.Count);
      for (int index = 0; index < questTypeList.Count; ++index)
        this.m_Param.Add(new VersusMapParam()
        {
          quest = questTypeList[index],
          selected = questTypeList[index].iname == selectedQuestId
        });
      this.SetData((object[]) this.m_Param.ToArray(), typeof (VersusMapParam));
    }

    protected override void Update()
    {
      base.Update();
    }

    protected override void OnItemSelect(GameObject go)
    {
      VersusMapParam dataOfClass = DataSource.FindDataOfClass<VersusMapParam>(go, (VersusMapParam) null);
      if (dataOfClass == null || !Object.op_Inequality((Object) this.SelectWindow, (Object) null))
        return;
      DataSource.Bind<QuestParam>(this.SelectWindow, dataOfClass.quest);
      FlowNode_TriggerLocalEvent.TriggerLocalEvent((Component) this, "OPEN_SELECTWINDOW");
    }

    private void UpdateSelect()
    {
      for (int index = 0; index < this.m_Param.Count; ++index)
        this.m_Param[index].selected = this.m_Param[index].quest.iname == GlobalVars.SelectedQuestID;
    }

    private void OnConfirm()
    {
      QuestParam dataOfClass = DataSource.FindDataOfClass<QuestParam>(this.SelectWindow, (QuestParam) null);
      if (dataOfClass != null)
      {
        GlobalVars.SelectedQuestID = dataOfClass.iname;
        GlobalVars.EditMultiPlayRoomComment = GlobalVars.SelectedMultiPlayRoomComment;
        this.UpdateSelect();
      }
      FlowNode_TriggerLocalEvent.TriggerLocalEvent((Component) this, "CLOSE_SELECT_WINDOW");
    }
  }
}
