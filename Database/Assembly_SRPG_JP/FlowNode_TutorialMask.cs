// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_TutorialMask
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System.Collections;
using System.Diagnostics;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(102, "OPEND", FlowNode.PinTypes.Output, 102)]
  [FlowNode.NodeType("Tutorial/Mask", 32741)]
  [FlowNode.Pin(1, "CREATE", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(101, "DESTORY", FlowNode.PinTypes.Output, 101)]
  public class FlowNode_TutorialMask : FlowNode
  {
    private const int PIN_IN_CREATE = 1;
    private const int PIN_OUT_DESTORY = 101;
    private const int PIN_OUT_OPEND = 102;
    public FlowNode_TutorialMask.eComponentId ComponentId;
    [SerializeField]
    private GameObject Mask;
    [SerializeField]
    private float NoResponseTime;
    private Vector3 mWorldPosition;
    private TutorialMask.eActionType mActionType;
    private bool mIsWorld2Screen;
    private string mText;
    private Vector2 mSize;

    public override void OnActivate(int pinID)
    {
      if (Object.op_Equality((Object) this.Mask, (Object) null) || pinID != 1)
        return;
      this.StartCoroutine(this.CreateMask());
    }

    public void Setup(TutorialMask.eActionType act_type, Vector3 world_pos, bool is_world2screen, string text = null)
    {
      this.mActionType = act_type;
      this.mWorldPosition = world_pos;
      this.mIsWorld2Screen = is_world2screen;
      this.mText = text;
    }

    public void SetupMaskSize(float width, float height)
    {
      this.mSize = new Vector2(width, height);
    }

    [DebuggerHidden]
    private IEnumerator CreateMask()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new FlowNode_TutorialMask.\u003CCreateMask\u003Ec__IteratorD0()
      {
        \u003C\u003Ef__this = this
      };
    }

    private void OnOpendMask()
    {
      this.ActivateOutputLinks(102);
    }

    private void OnDestroyMask()
    {
      this.ActivateOutputLinks(101);
    }

    public enum eComponentId
    {
      MOVE_UNIT,
      ATTACK_TARGET_DESC,
      NORMAL_ATTACK_DESC,
      ABILITY_DESC,
      TAP_NORMAL_ATTACK,
      EXEC_NORMAL_ATTACK,
      SELECT_DIR,
      CLOSE_BATTLE_INFO,
    }
  }
}
