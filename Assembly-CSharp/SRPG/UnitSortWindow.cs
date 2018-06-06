// Decompiled with JetBrains decompiler
// Type: SRPG.UnitSortWindow
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(101, "決定", FlowNode.PinTypes.Output, 1)]
  [FlowNode.Pin(102, "キャンセル", FlowNode.PinTypes.Output, 2)]
  public class UnitSortWindow : MonoBehaviour, IFlowInterface
  {
    public Toggle[] ToggleElements;
    public Button BtnReset;
    public Button BtnOk;
    public Button BtnClose;
    private bool[] mToggleBkupValues;
    private bool mPlaySE;

    public UnitSortWindow()
    {
      base.\u002Ector();
    }

    public void Activated(int pinID)
    {
    }

    private void Start()
    {
      if (this.ToggleElements != null)
      {
        this.mToggleBkupValues = new bool[this.ToggleElements.Length];
        for (int index = 0; index < this.ToggleElements.Length; ++index)
        {
          // ISSUE: object of a compiler-generated type is created
          // ISSUE: method pointer
          ((UnityEvent<bool>) this.ToggleElements[index].onValueChanged).AddListener(new UnityAction<bool>((object) new UnitSortWindow.\u003CStart\u003Ec__AnonStorey28A()
          {
            \u003C\u003Ef__this = this,
            toggle_index = index
          }, __methodptr(\u003C\u003Em__325)));
          this.ToggleElements[index].set_isOn(this.mToggleBkupValues[index] = GameUtility.GetUnitShowSetting(index));
        }
      }
      if (Object.op_Inequality((Object) this.BtnReset, (Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent) this.BtnReset.get_onClick()).AddListener(new UnityAction((object) this, __methodptr(OnReset)));
      }
      if (Object.op_Inequality((Object) this.BtnOk, (Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent) this.BtnOk.get_onClick()).AddListener(new UnityAction((object) this, __methodptr(OnDecide)));
      }
      if (Object.op_Inequality((Object) this.BtnClose, (Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent) this.BtnClose.get_onClick()).AddListener(new UnityAction((object) this, __methodptr(OnCancel)));
      }
      this.mPlaySE = true;
    }

    private void SelectToggleElement(int index)
    {
      GameUtility.SetUnitShowSetting(index, this.ToggleElements[index].get_isOn());
      if (!this.mPlaySE)
        return;
      Toggle toggle = this.ToggleElements == null || index < 0 || index >= this.ToggleElements.Length ? (Toggle) null : this.ToggleElements[index];
      GameObject gameObject = !Object.op_Equality((Object) toggle, (Object) null) ? ((Component) toggle).get_gameObject() : (GameObject) null;
      SystemSound systemSound = !Object.op_Equality((Object) gameObject, (Object) null) ? (SystemSound) gameObject.GetComponentInChildren<SystemSound>() : (SystemSound) null;
      if (!Object.op_Inequality((Object) systemSound, (Object) null))
        return;
      systemSound.Play();
    }

    private void OnReset()
    {
      GameUtility.ResetUnitShowSetting();
      bool mPlaySe = this.mPlaySE;
      this.mPlaySE = false;
      for (int index = 0; index < this.ToggleElements.Length; ++index)
        this.ToggleElements[index].set_isOn(GameUtility.GetUnitShowSetting(index));
      this.mPlaySE = mPlaySe;
    }

    private void OnDecide()
    {
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 101);
    }

    private void OnCancel()
    {
      for (int index = 0; index < this.mToggleBkupValues.Length; ++index)
        GameUtility.SetUnitShowSetting(index, this.mToggleBkupValues[index]);
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 102);
    }
  }
}
