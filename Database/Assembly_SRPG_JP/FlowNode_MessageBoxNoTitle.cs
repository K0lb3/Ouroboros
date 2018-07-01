// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_MessageBoxNoTitle
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(1, "Closed", FlowNode.PinTypes.Output, 1)]
  [FlowNode.NodeType("UI/MessageBoxNoTitle", 32741)]
  [FlowNode.Pin(10, "Open", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(11, "ForceClose", FlowNode.PinTypes.Input, 11)]
  [FlowNode.Pin(100, "Opened", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(101, "ForceClosed", FlowNode.PinTypes.Output, 101)]
  public class FlowNode_MessageBoxNoTitle : FlowNode
  {
    public string Text;
    public bool systemModal;
    public int systemModalPriority;
    public GameObject parent;
    public string parentName;
    public bool unscaledTime;
    public bool richTag;
    private GameObject winGO;

    public override void OnActivate(int pinID)
    {
      switch (pinID)
      {
        case 10:
          if (!string.IsNullOrEmpty(this.parentName))
          {
            this.parent = GameObject.Find(this.parentName);
            if (Object.op_Equality((Object) this.parent, (Object) null))
              DebugUtility.LogWarning("can not found gameObject:" + this.parentName);
          }
          string str = LocalizedText.Get(this.Text);
          if (this.richTag)
            str = LocalizedText.ReplaceTag(str);
          this.winGO = UIUtility.SystemMessage(str, (UIUtility.DialogResultEvent) (go =>
          {
            if (!Object.op_Inequality((Object) this.winGO, (Object) null))
              return;
            this.winGO = (GameObject) null;
            this.ActivateOutputLinks(1);
          }), this.parent, this.systemModal, this.systemModalPriority);
          if (Object.op_Implicit((Object) this.winGO) && this.unscaledTime)
          {
            Animator component = (Animator) this.winGO.GetComponent<Animator>();
            if (Object.op_Inequality((Object) component, (Object) null))
              component.set_updateMode((AnimatorUpdateMode) 2);
          }
          this.ActivateOutputLinks(100);
          break;
        case 11:
          if (Object.op_Equality((Object) this.winGO, (Object) null))
            break;
          Win_Btn_Decide_Flx winBtnDecideFlx = !Object.op_Equality((Object) this.winGO, (Object) null) ? (Win_Btn_Decide_Flx) this.winGO.GetComponent<Win_Btn_Decide_Flx>() : (Win_Btn_Decide_Flx) null;
          this.winGO = (GameObject) null;
          if (Object.op_Inequality((Object) winBtnDecideFlx, (Object) null))
            winBtnDecideFlx.BeginClose();
          this.ActivateOutputLinks(101);
          break;
      }
    }
  }
}
