// Decompiled with JetBrains decompiler
// Type: SRPG.InGameMenu_Audience
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(2, "Give Up", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(100, "Close Give Up Window", FlowNode.PinTypes.Input, 0)]
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
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      InGameMenu_Audience.\u003CStart\u003Ec__AnonStorey33D startCAnonStorey33D = new InGameMenu_Audience.\u003CStart\u003Ec__AnonStorey33D();
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      for (startCAnonStorey33D.i = 0; startCAnonStorey33D.i < audiencePlayer.Count; ++startCAnonStorey33D.i)
      {
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: variable of a compiler-generated type
        InGameMenu_Audience.\u003CStart\u003Ec__AnonStorey33E startCAnonStorey33E = new InGameMenu_Audience.\u003CStart\u003Ec__AnonStorey33E();
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        startCAnonStorey33E.units = audiencePlayer[startCAnonStorey33D.i].units;
        // ISSUE: reference to a compiler-generated field
        GenericSlot[] genericSlotArray = startCAnonStorey33D.i != 0 ? this.Units_2P : this.Units_1P;
        if (genericSlotArray != null)
        {
          // ISSUE: object of a compiler-generated type is created
          // ISSUE: variable of a compiler-generated type
          InGameMenu_Audience.\u003CStart\u003Ec__AnonStorey33F startCAnonStorey33F = new InGameMenu_Audience.\u003CStart\u003Ec__AnonStorey33F();
          // ISSUE: reference to a compiler-generated field
          startCAnonStorey33F.\u003C\u003Ef__ref\u0024829 = startCAnonStorey33D;
          // ISSUE: reference to a compiler-generated field
          startCAnonStorey33F.\u003C\u003Ef__ref\u0024830 = startCAnonStorey33E;
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          for (startCAnonStorey33F.j = 0; startCAnonStorey33F.j < genericSlotArray.Length; ++startCAnonStorey33F.j)
          {
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            if (startCAnonStorey33F.j < startCAnonStorey33E.units.Length)
            {
              // ISSUE: reference to a compiler-generated field
              // ISSUE: reference to a compiler-generated field
              // ISSUE: reference to a compiler-generated field
              genericSlotArray[startCAnonStorey33F.j].SetSlotData<UnitData>(startCAnonStorey33E.units[startCAnonStorey33F.j].unit);
              // ISSUE: reference to a compiler-generated field
              Button component = (Button) ((Component) genericSlotArray[startCAnonStorey33F.j]).GetComponent<Button>();
              if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
              {
                // ISSUE: reference to a compiler-generated method
                Unit data = allUnits.Find(new Predicate<Unit>(startCAnonStorey33F.\u003C\u003Em__392));
                if (data != null)
                {
                  DataSource.Bind<Unit>(((Component) component).get_gameObject(), data);
                  this.mButtonObj.Add(((Component) component).get_gameObject());
                  ((Selectable) component).set_interactable(!data.IsDeadCondition());
                }
              }
            }
            else
            {
              // ISSUE: reference to a compiler-generated field
              genericSlotArray[startCAnonStorey33F.j].SetSlotData<UnitData>((UnitData) null);
            }
          }
        }
        // ISSUE: reference to a compiler-generated field
        Text text1 = startCAnonStorey33D.i != 0 ? this.Name2P : this.Name1P;
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) text1, (UnityEngine.Object) null))
        {
          // ISSUE: reference to a compiler-generated field
          text1.set_text(audiencePlayer[startCAnonStorey33D.i].playerName);
        }
        // ISSUE: reference to a compiler-generated field
        Text text2 = startCAnonStorey33D.i != 0 ? this.TotalAtk2P : this.TotalAtk1P;
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) text2, (UnityEngine.Object) null))
        {
          // ISSUE: reference to a compiler-generated field
          text2.set_text(audiencePlayer[startCAnonStorey33D.i].totalAtk.ToString());
        }
        // ISSUE: reference to a compiler-generated field
        Text text3 = startCAnonStorey33D.i != 0 ? this.Lv2P : this.Lv1P;
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) text2, (UnityEngine.Object) null))
        {
          // ISSUE: reference to a compiler-generated field
          text3.set_text(audiencePlayer[startCAnonStorey33D.i].playerLevel.ToString());
        }
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
