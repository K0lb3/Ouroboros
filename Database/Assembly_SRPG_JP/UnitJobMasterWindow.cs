// Decompiled with JetBrains decompiler
// Type: SRPG.UnitJobMasterWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(100, "Close", FlowNode.PinTypes.Output, 100)]
  public class UnitJobMasterWindow : SRPG_FixedList, IFlowInterface
  {
    private List<JobMasterValue> mStatusValues = new List<JobMasterValue>();
    public GameObject StatusItemTemplate;
    public Button NextButton;

    protected override void Start()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.StatusItemTemplate, (UnityEngine.Object) null) && this.StatusItemTemplate.get_activeInHierarchy())
        this.StatusItemTemplate.SetActive(false);
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.NextButton, (UnityEngine.Object) null))
        return;
      // ISSUE: method pointer
      ((UnityEvent) this.NextButton.get_onClick()).AddListener(new UnityAction((object) this, __methodptr(OnNextClick)));
    }

    public void Activated(int pinID)
    {
    }

    public void Refresh(BaseStatus old_status, BaseStatus new_status)
    {
      if (old_status == null || new_status == null)
        return;
      this.mStatusValues.Clear();
      string[] names = Enum.GetNames(typeof (ParamTypes));
      Array values = Enum.GetValues(typeof (ParamTypes));
      for (int index1 = 0; index1 < values.Length; ++index1)
      {
        ParamTypes index2 = (ParamTypes) values.GetValue(index1);
        switch (index2)
        {
          case ParamTypes.None:
          case ParamTypes.HpMax:
            continue;
          default:
            int oldStatu = (int) old_status[index2];
            int newStatu = (int) new_status[index2];
            if (oldStatu != newStatu)
            {
              this.mStatusValues.Add(new JobMasterValue()
              {
                type = names[index1],
                old_value = oldStatu,
                new_value = newStatu
              });
              continue;
            }
            continue;
        }
      }
      this.SetData((object[]) this.mStatusValues.ToArray(), typeof (JobMasterValue));
    }

    protected override GameObject CreateItem()
    {
      return (GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) this.StatusItemTemplate);
    }

    protected override void OnUpdateItem(GameObject go, int index)
    {
      StatusListItem component = (StatusListItem) go.GetComponent<StatusListItem>();
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
        return;
      string type = this.mStatusValues[index].type;
      int oldValue = this.mStatusValues[index].old_value;
      int newValue = this.mStatusValues[index].new_value;
      ((Component) component).get_gameObject().SetActive(true);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component.Label, (UnityEngine.Object) null))
        component.Label.set_text(LocalizedText.Get("sys." + type));
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component.Value, (UnityEngine.Object) null))
        component.Value.set_text(oldValue.ToString());
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) component.Bonus, (UnityEngine.Object) null))
        return;
      if (newValue != 0)
      {
        component.Bonus.set_text(newValue.ToString());
        ((Component) component.Bonus).get_gameObject().SetActive(true);
      }
      else
        ((Component) component.Bonus).get_gameObject().SetActive(false);
    }

    private void OnNextClick()
    {
      if (this.Page < this.MaxPage - 1)
        this.GotoNextPage();
      else
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
    }
  }
}
