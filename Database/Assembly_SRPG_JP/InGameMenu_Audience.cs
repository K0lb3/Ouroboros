// Decompiled with JetBrains decompiler
// Type: SRPG.InGameMenu_Audience
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(100, "Close Give Up Window", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(2, "Give Up", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Start Debug", FlowNode.PinTypes.Input, 0)]
  public class InGameMenu_Audience : MonoBehaviour, IFlowInterface
  {
    public const int PINID_DEBUG = 1;
    public const int PINID_GIVEUP = 2;
    public const int PINID_CLOSE_GIVEUP_WINDOW = 100;
    public GameObject ExitButton;
    public GenericSlot[] Units_1P;
    public GenericSlot[] Units_2P;
    public Text Name1P;
    public Text Name2P;
    public Text TotalAtk1P;
    public Text TotalAtk2P;
    public Text Lv1P;
    public Text Lv2P;
    private GameObject mExitWindow;
    private List<GameObject> mButtonObj;

    public InGameMenu_Audience()
    {
      base.\u002Ector();
    }

    private void Start()
    {
      SceneBattle instance = SceneBattle.Instance;
      if (this.mButtonObj != null)
        this.mButtonObj.Clear();
      else
        this.mButtonObj = new List<GameObject>();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ExitButton, (UnityEngine.Object) null))
        this.ExitButton.SetActive(true);
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) instance, (UnityEngine.Object) null))
        return;
      List<JSON_MyPhotonPlayerParam> audiencePlayer = instance.AudiencePlayer;
      List<Unit> allUnits = instance.Battle.AllUnits;
      for (int i = 0; i < audiencePlayer.Count; ++i)
      {
        JSON_MyPhotonPlayerParam.UnitDataElem[] units = audiencePlayer[i].units;
        GenericSlot[] genericSlotArray = i != 0 ? this.Units_2P : this.Units_1P;
        if (genericSlotArray != null)
        {
          for (int j = 0; j < genericSlotArray.Length; ++j)
          {
            if (j < units.Length)
            {
              genericSlotArray[j].SetSlotData<UnitData>(units[j].unit);
              Button component = (Button) ((Component) genericSlotArray[j]).GetComponent<Button>();
              if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
              {
                Unit data = allUnits.Find((Predicate<Unit>) (un =>
                {
                  if (un.OwnerPlayerIndex == i + 1)
                    return un.UnitData.UnitParam.iname == units[j].unit.UnitParam.iname;
                  return false;
                }));
                if (data != null)
                {
                  DataSource.Bind<Unit>(((Component) component).get_gameObject(), data);
                  this.mButtonObj.Add(((Component) component).get_gameObject());
                  ((Selectable) component).set_interactable(!data.IsDeadCondition());
                }
              }
            }
            else
              genericSlotArray[j].SetSlotData<UnitData>((UnitData) null);
          }
        }
        Text text1 = i != 0 ? this.Name2P : this.Name1P;
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) text1, (UnityEngine.Object) null))
          text1.set_text(audiencePlayer[i].playerName);
        Text text2 = i != 0 ? this.TotalAtk2P : this.TotalAtk1P;
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) text2, (UnityEngine.Object) null))
          text2.set_text(audiencePlayer[i].totalAtk.ToString());
        Text text3 = i != 0 ? this.Lv2P : this.Lv1P;
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) text2, (UnityEngine.Object) null))
          text3.set_text(audiencePlayer[i].playerLevel.ToString());
      }
    }

    private void OnDestroy()
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) SceneBattle.Instance, (UnityEngine.Object) null))
        return;
      SceneBattle.Instance.OnQuestEnd -= new SceneBattle.QuestEndEvent(this.OnQuestEnd);
    }

    private void OnQuestEnd()
    {
      this.Activated(100);
    }

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 2:
          this.mExitWindow = UIUtility.ConfirmBox(LocalizedText.Get("sys.MULTI_VERSUS_COMFIRM_EXIT"), new UIUtility.DialogResultEvent(this.OnGiveUp), (UIUtility.DialogResultEvent) null, (GameObject) null, true, 1, (string) null, (string) null);
          break;
        case 100:
          if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mExitWindow, (UnityEngine.Object) null))
            break;
          Win_Btn_DecideCancel_FL_C component = (Win_Btn_DecideCancel_FL_C) this.mExitWindow.GetComponent<Win_Btn_DecideCancel_FL_C>();
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
            component.BeginClose();
          this.mExitWindow = (GameObject) null;
          break;
      }
    }

    private void OnGiveUp(GameObject go)
    {
      Network.Abort();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) SceneBattle.Instance, (UnityEngine.Object) null))
        SceneBattle.Instance.ForceEndQuest();
      CanvasGroup component = (CanvasGroup) ((Component) this).GetComponent<CanvasGroup>();
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
        return;
      component.set_blocksRaycasts(false);
    }

    private void Update()
    {
      if (this.mButtonObj == null)
        return;
      for (int index = 0; index < this.mButtonObj.Count; ++index)
      {
        Unit dataOfClass = DataSource.FindDataOfClass<Unit>(this.mButtonObj[index], (Unit) null);
        if (dataOfClass != null)
        {
          Button component = (Button) this.mButtonObj[index].GetComponent<Button>();
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
            ((Selectable) component).set_interactable(!dataOfClass.IsDeadCondition());
        }
      }
    }
  }
}
