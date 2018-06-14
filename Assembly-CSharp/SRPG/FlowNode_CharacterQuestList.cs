// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_CharacterQuestList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  [FlowNode.NodeType("UI/CharacterQuestList")]
  public class FlowNode_CharacterQuestList : FlowNode_GUI
  {
    protected override void OnInstanceCreate()
    {
      base.OnInstanceCreate();
      UnitCharacterQuestWindow componentInChildren = (UnitCharacterQuestWindow) this.Instance.GetComponentInChildren<UnitCharacterQuestWindow>();
      if (Object.op_Equality((Object) componentInChildren, (Object) null))
        return;
      componentInChildren.CurrentUnit = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID((long) GlobalVars.SelectedUnitUniqueID);
      ((WindowController) ((Component) componentInChildren).GetComponent<WindowController>()).SetCollision(false);
      ((WindowController) ((Component) componentInChildren).GetComponent<WindowController>()).OnWindowStateChange = new WindowController.WindowStateChangeEvent(this.OnBack);
      WindowController.OpenIfAvailable((Component) componentInChildren);
    }

    private void OnBack(GameObject go, bool visible)
    {
      if (visible)
        return;
      UnitCharacterQuestWindow componentInChildren = (UnitCharacterQuestWindow) this.Instance.GetComponentInChildren<UnitCharacterQuestWindow>();
      if (Object.op_Equality((Object) componentInChildren, (Object) null) || visible)
        return;
      ((WindowController) ((Component) componentInChildren).GetComponent<WindowController>()).SetCollision(true);
      ((WindowController) ((Component) componentInChildren).GetComponent<WindowController>()).OnWindowStateChange = (WindowController.WindowStateChangeEvent) null;
      Object.Destroy((Object) ((Component) componentInChildren).get_gameObject());
    }
  }
}
