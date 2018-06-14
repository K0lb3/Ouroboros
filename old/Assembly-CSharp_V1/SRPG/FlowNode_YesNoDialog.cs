// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_YesNoDialog
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [FlowNode.NodeType("UI/YesNoDialog", 32741)]
  [FlowNode.Pin(101, "ForceClosed", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(100, "Opened", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(11, "ForceClose", FlowNode.PinTypes.Input, 11)]
  [FlowNode.Pin(2, "No", FlowNode.PinTypes.Output, 2)]
  [FlowNode.Pin(1, "Yes", FlowNode.PinTypes.Output, 1)]
  [FlowNode.Pin(10, "Open", FlowNode.PinTypes.Input, 0)]
  public class FlowNode_YesNoDialog : FlowNode
  {
    public string Title;
    public string Text;
    public bool systemModal;
    public int systemModalPriority;
    public GameObject parent;
    public string parentName;
    public bool richTag;
    public bool unscaledTime;
    private GameObject winGO;

    public override string[] GetInfoLines()
    {
      if (string.IsNullOrEmpty(this.Text))
        return base.GetInfoLines();
      return new string[1]{ "Text is [" + this.Text + "]" };
    }

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
          string text = LocalizedText.Get(this.Text);
          if (this.richTag)
            text = LocalizedText.ReplaceTag(text);
          this.winGO = !string.IsNullOrEmpty(this.Title) ? UIUtility.ConfirmBoxTitle(LocalizedText.Get(this.Title), text, new UIUtility.DialogResultEvent(this.OnClickOK), new UIUtility.DialogResultEvent(this.OnClickCancel), this.parent, this.systemModal, this.systemModalPriority) : UIUtility.ConfirmBox(text, new UIUtility.DialogResultEvent(this.OnClickOK), new UIUtility.DialogResultEvent(this.OnClickCancel), this.parent, this.systemModal, this.systemModalPriority);
          if (Object.op_Inequality((Object) this.winGO, (Object) null) && this.unscaledTime)
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
          if (string.IsNullOrEmpty(this.Title))
          {
            Win_Btn_DecideCancel_FL_C btnDecideCancelFlC = !Object.op_Equality((Object) this.winGO, (Object) null) ? (Win_Btn_DecideCancel_FL_C) this.winGO.GetComponent<Win_Btn_DecideCancel_FL_C>() : (Win_Btn_DecideCancel_FL_C) null;
            this.winGO = (GameObject) null;
            if (Object.op_Inequality((Object) btnDecideCancelFlC, (Object) null))
              btnDecideCancelFlC.BeginClose();
          }
          else
          {
            Win_Btn_YN_Title_Flx winBtnYnTitleFlx = !Object.op_Equality((Object) this.winGO, (Object) null) ? (Win_Btn_YN_Title_Flx) this.winGO.GetComponent<Win_Btn_YN_Title_Flx>() : (Win_Btn_YN_Title_Flx) null;
            this.winGO = (GameObject) null;
            if (Object.op_Inequality((Object) winBtnYnTitleFlx, (Object) null))
              winBtnYnTitleFlx.BeginClose();
          }
          this.ActivateOutputLinks(101);
          break;
      }
    }

    private void OnClickOK(GameObject go)
    {
      if (Object.op_Equality((Object) this.winGO, (Object) null))
        return;
      this.winGO = (GameObject) null;
      this.ActivateOutputLinks(1);
    }

    private void OnClickCancel(GameObject go)
    {
      if (Object.op_Equality((Object) this.winGO, (Object) null))
        return;
      this.winGO = (GameObject) null;
      this.ActivateOutputLinks(2);
    }
  }
}
