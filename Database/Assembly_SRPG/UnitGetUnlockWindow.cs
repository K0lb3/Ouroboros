// Decompiled with JetBrains decompiler
// Type: SRPG.UnitGetUnlockWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(101, "Selected Quest", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(1, "Refresh", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(100, "Unlock", FlowNode.PinTypes.Output, 100)]
  public class UnitGetUnlockWindow : MonoBehaviour, IFlowInterface
  {
    private UnitParam UnlockUnit;
    public Text UnitName;

    public UnitGetUnlockWindow()
    {
      base.\u002Ector();
    }

    private void Start()
    {
      this.Refresh();
    }

    public void Activated(int pinID)
    {
      if (pinID != 1)
        return;
      this.Refresh();
    }

    private void Refresh()
    {
      this.UnlockUnit = MonoSingleton<GameManager>.Instance.GetUnitParam(GlobalVars.UnlockUnitID);
      DataSource.Bind<UnitParam>(((Component) this).get_gameObject(), this.UnlockUnit);
      this.UnitName.set_text(LocalizedText.Get("sys.GET_UNIT_WINDOW_UNIT_NAME", new object[1]
      {
        (object) this.UnlockUnit.name
      }));
      GameParameter.UpdateAll(((Component) this).get_gameObject());
    }
  }
}
