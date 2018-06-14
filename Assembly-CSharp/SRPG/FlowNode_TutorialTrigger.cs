// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_TutorialTrigger
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(0, "Activate", FlowNode.PinTypes.Input, 0)]
  [AddComponentMenu("")]
  [FlowNode.NodeType("Event/Tutorial Trigger", 58751)]
  [FlowNode.Pin(1, "Deactivate", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(10, "Triggered", FlowNode.PinTypes.Output, 10)]
  public class FlowNode_TutorialTrigger : FlowNode
  {
    [BitMask]
    public FlowNode_TutorialTrigger.ActivateFlags Flags = FlowNode_TutorialTrigger.ActivateFlags.AutoDeactivate;
    public FlowNode_TutorialTrigger.TriggerTypes TriggerType;
    [SerializeField]
    private string sVal0;
    [SerializeField]
    private string sVal1;
    [SerializeField]
    private int iVal0;
    [SerializeField]
    private int iVal1;

    private bool CompareUnit(Unit unit, int turn)
    {
      if (!string.IsNullOrEmpty(this.sVal0) && !(unit.UniqueName == this.sVal0))
        return false;
      if (this.iVal0 >= 0)
        return this.iVal0 == turn;
      return true;
    }

    public void OnUnitStart(Unit unit, int turn)
    {
      this.TriggerIf(this.TriggerType == FlowNode_TutorialTrigger.TriggerTypes.UnitStart && this.CompareUnit(unit, turn));
    }

    public void OnUnitEnd(Unit unit, int turn)
    {
      this.TriggerIf(this.TriggerType == FlowNode_TutorialTrigger.TriggerTypes.UnitEnd && this.CompareUnit(unit, turn));
    }

    public void OnUnitMoveStart(Unit unit, int turn)
    {
      this.TriggerIf(this.TriggerType == FlowNode_TutorialTrigger.TriggerTypes.UnitMoveStart && this.CompareUnit(unit, turn));
    }

    public void OnUnitSkillStart(Unit unit, int turn)
    {
      this.TriggerIf(this.TriggerType == FlowNode_TutorialTrigger.TriggerTypes.UnitSkillStart && this.CompareUnit(unit, turn));
    }

    public void OnUnitSkillEnd(Unit unit, int turn)
    {
      this.TriggerIf(this.TriggerType == FlowNode_TutorialTrigger.TriggerTypes.UnitSkillEnd && this.CompareUnit(unit, turn));
    }

    public void OnTargetChange(Unit target, int turn)
    {
      this.TriggerIf(this.TriggerType == FlowNode_TutorialTrigger.TriggerTypes.TargetChange && this.CompareUnit(target, turn));
    }

    public void OnHotTargetsChange(Unit unit, int turn)
    {
      this.TriggerIf(this.TriggerType == FlowNode_TutorialTrigger.TriggerTypes.HotTargetsChange && this.CompareUnit(unit, turn));
    }

    public void OnSelectDirectionStart(Unit unit, int turn)
    {
      this.TriggerIf(this.TriggerType == FlowNode_TutorialTrigger.TriggerTypes.UnitSelectDirection && this.CompareUnit(unit, turn));
    }

    private void TriggerIf(bool b)
    {
      if (!b)
        return;
      this.Trigger();
    }

    private void Trigger()
    {
      if (!((Behaviour) this).get_enabled())
        return;
      this.ActivateOutputLinks(10);
      ((Behaviour) this).set_enabled((this.Flags & FlowNode_TutorialTrigger.ActivateFlags.AutoDeactivate) == (FlowNode_TutorialTrigger.ActivateFlags) 0);
    }

    protected override void Awake()
    {
      base.Awake();
      ((Behaviour) this).set_enabled((this.Flags & FlowNode_TutorialTrigger.ActivateFlags.AutoActivate) != (FlowNode_TutorialTrigger.ActivateFlags) 0);
    }

    public override void OnActivate(int pinID)
    {
      switch (pinID)
      {
        case 0:
          ((Behaviour) this).set_enabled(true);
          break;
        case 1:
          ((Behaviour) this).set_enabled(false);
          break;
      }
    }

    [System.Flags]
    public enum ActivateFlags
    {
      AutoActivate = 1,
      AutoDeactivate = 2,
    }

    public enum TriggerTypes
    {
      None = 0,
      UnitStart = 10, // 0x0000000A
      UnitSkillStart = 11, // 0x0000000B
      UnitMoveStart = 12, // 0x0000000C
      UnitSkillEnd = 13, // 0x0000000D
      UnitEnd = 14, // 0x0000000E
      TargetChange = 15, // 0x0000000F
      HotTargetsChange = 16, // 0x00000010
      UnitSelectDirection = 17, // 0x00000011
    }
  }
}
