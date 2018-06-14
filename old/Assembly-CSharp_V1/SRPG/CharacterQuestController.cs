// Decompiled with JetBrains decompiler
// Type: SRPG.CharacterQuestController
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(2, "Refresh", FlowNode.PinTypes.Input, 2)]
  [FlowNode.Pin(1, "Start(Restore)", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(0, "Start", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(13, "Collabo(Restore)", FlowNode.PinTypes.Output, 13)]
  [FlowNode.Pin(12, "Chara(Restore)", FlowNode.PinTypes.Output, 12)]
  [FlowNode.Pin(11, "Collabo", FlowNode.PinTypes.Output, 11)]
  [FlowNode.Pin(10, "Chara", FlowNode.PinTypes.Output, 10)]
  public class CharacterQuestController : MonoBehaviour, IFlowInterface
  {
    private const int PIN_ID_START = 0;
    private const int PIN_ID_START_RESTORE = 1;
    private const int PIN_ID_REFRESH = 2;
    private const int PIN_ID_CHARA = 10;
    private const int PIN_ID_COLLABO = 11;
    private const int PIN_ID_CHARA_RESTORE = 12;
    private const int PIN_ID_COLLABO_RESTORE = 13;
    public string VARIABLE_KEY;
    public string VARIABLE_VALUE_CHARA;
    public string VARIABLE_VALUE_COLLABO;

    public CharacterQuestController()
    {
      base.\u002Ector();
    }

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 0:
          FlowNode_Variable.Set(this.VARIABLE_KEY, this.VARIABLE_VALUE_CHARA);
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 10);
          break;
        case 1:
          string a1 = FlowNode_Variable.Get(this.VARIABLE_KEY);
          if (string.IsNullOrEmpty(a1))
          {
            FlowNode_Variable.Set(this.VARIABLE_KEY, this.VARIABLE_VALUE_CHARA);
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 10);
            break;
          }
          if (string.Equals(a1, this.VARIABLE_VALUE_CHARA))
          {
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 12);
            break;
          }
          if (!string.Equals(a1, this.VARIABLE_VALUE_COLLABO))
            break;
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 13);
          break;
        case 2:
          string a2 = FlowNode_Variable.Get(this.VARIABLE_KEY);
          if (string.IsNullOrEmpty(a2))
          {
            FlowNode_Variable.Set(this.VARIABLE_KEY, this.VARIABLE_VALUE_CHARA);
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 10);
            break;
          }
          if (string.Equals(a2, this.VARIABLE_VALUE_CHARA))
          {
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 10);
            break;
          }
          if (!string.Equals(a2, this.VARIABLE_VALUE_COLLABO))
            break;
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 11);
          break;
      }
    }
  }
}
