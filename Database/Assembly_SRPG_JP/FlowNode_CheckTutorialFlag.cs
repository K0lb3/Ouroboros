// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_CheckTutorialFlag
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;

namespace SRPG
{
  [FlowNode.Pin(3, "False", FlowNode.PinTypes.Output, 0)]
  [FlowNode.Pin(1, "CheckFlag", FlowNode.PinTypes.Input, 0)]
  [FlowNode.NodeType("Tutorial/CheckTutorialFlag", 32741)]
  [FlowNode.Pin(2, "True", FlowNode.PinTypes.Output, 0)]
  public class FlowNode_CheckTutorialFlag : FlowNode
  {
    private const int PIN_ID_IN = 1;
    private const int PIN_ID_TRUE = 2;
    private const int PIN_ID_FALSE = 3;
    public TutorialFlags mTutorialFlags;
    public string FlagID;

    public override void OnActivate(int pinID)
    {
      if (pinID != 1)
        return;
      if (this.CheckFlag(this.mTutorialFlags))
        this.ActivateOutputLinks(2);
      else
        this.ActivateOutputLinks(3);
    }

    private bool CheckFlag(TutorialFlags flag)
    {
      if ((long) this.mTutorialFlags == 0L)
        return MonoSingleton<GameManager>.Instance.IsTutorialFlagSet(this.FlagID);
      return (MonoSingleton<GameManager>.Instance.Player.TutorialFlags & (long) flag) != 0L;
    }
  }
}
