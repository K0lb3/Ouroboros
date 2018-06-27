// Decompiled with JetBrains decompiler
// Type: FlowNode_MultiPlaySelectContinue
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using SRPG;
using System;
using System.Collections.Generic;

[FlowNode.Pin(1, "Out", FlowNode.PinTypes.Output, 0)]
[FlowNode.Pin(100, "する", FlowNode.PinTypes.Input, 0)]
[FlowNode.NodeType("Multi/MultiPlaySelectContinue", 32741)]
[FlowNode.Pin(200, "しない", FlowNode.PinTypes.Input, 0)]
public class FlowNode_MultiPlaySelectContinue : FlowNode
{
  public override void OnActivate(int pinID)
  {
    switch (pinID)
    {
      case 100:
        GlobalVars.SelectedMultiPlayerUnitIDs = new List<int>();
        SceneBattle instance = SceneBattle.Instance;
        BattleCore battleCore = !UnityEngine.Object.op_Equality((UnityEngine.Object) instance, (UnityEngine.Object) null) ? instance.Battle : (BattleCore) null;
        if (battleCore != null)
        {
          // ISSUE: object of a compiler-generated type is created
          // ISSUE: variable of a compiler-generated type
          FlowNode_MultiPlaySelectContinue.\u003COnActivate\u003Ec__AnonStorey2CD activateCAnonStorey2Cd = new FlowNode_MultiPlaySelectContinue.\u003COnActivate\u003Ec__AnonStorey2CD();
          using (List<Unit>.Enumerator enumerator = battleCore.Units.GetEnumerator())
          {
            while (enumerator.MoveNext())
            {
              // ISSUE: reference to a compiler-generated field
              activateCAnonStorey2Cd.unit = enumerator.Current;
              // ISSUE: reference to a compiler-generated field
              // ISSUE: reference to a compiler-generated field
              if (activateCAnonStorey2Cd.unit.OwnerPlayerIndex > 0 && activateCAnonStorey2Cd.unit.IsDead)
              {
                // ISSUE: reference to a compiler-generated method
                int index = battleCore.AllUnits.FindIndex(new Predicate<Unit>(activateCAnonStorey2Cd.\u003C\u003Em__2AC));
                GlobalVars.SelectedMultiPlayerUnitIDs.Add(index);
              }
            }
          }
        }
        GlobalVars.SelectedMultiPlayContinue = GlobalVars.EMultiPlayContinue.CONTINUE;
        this.ActivateOutputLinks(1);
        break;
      case 200:
        GlobalVars.SelectedMultiPlayContinue = GlobalVars.EMultiPlayContinue.CANCEL;
        GlobalVars.SelectedMultiPlayerUnitIDs = new List<int>();
        this.ActivateOutputLinks(1);
        break;
    }
  }
}
