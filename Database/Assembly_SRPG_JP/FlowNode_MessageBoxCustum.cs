// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_MessageBoxCustum
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(10, "Open", FlowNode.PinTypes.Input, 0)]
  [FlowNode.NodeType("UI/MessageBoxCustum", 32741)]
  [FlowNode.Pin(1, "Closed", FlowNode.PinTypes.Output, 1)]
  [FlowNode.Pin(11, "ForceClose", FlowNode.PinTypes.Input, 11)]
  [FlowNode.Pin(100, "Opened", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(101, "ForceClosed", FlowNode.PinTypes.Output, 101)]
  public class FlowNode_MessageBoxCustum : FlowNode
  {
    [StringIsResourcePath(typeof (GameObject))]
    public string ResourcePath;
    public string Caption;
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
          this.winGO = this.CreatePrefab(this.ResourcePath, LocalizedText.Get(this.Caption), str, (UIUtility.DialogResultEvent) (go =>
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
          Win_Btn_Decide_Title_Flx btnDecideTitleFlx = !Object.op_Equality((Object) this.winGO, (Object) null) ? (Win_Btn_Decide_Title_Flx) this.winGO.GetComponent<Win_Btn_Decide_Title_Flx>() : (Win_Btn_Decide_Title_Flx) null;
          this.winGO = (GameObject) null;
          if (Object.op_Inequality((Object) btnDecideTitleFlx, (Object) null))
            btnDecideTitleFlx.BeginClose();
          this.ActivateOutputLinks(101);
          break;
      }
    }

    private GameObject CreatePrefab(string resource_path, string title, string msg, UIUtility.DialogResultEvent ok_event_listener, GameObject go_parent, bool system_modal, int system_modal_priority)
    {
      Canvas canvas = UIUtility.PushCanvas(system_modal, system_modal_priority);
      if (Object.op_Inequality((Object) go_parent, (Object) null))
        ((Component) canvas).get_transform().SetParent(go_parent.get_transform());
      GameObject gameObject1 = AssetManager.Load<GameObject>(resource_path);
      if (!Object.op_Implicit((Object) gameObject1))
      {
        Debug.LogError((object) ("FlowNode_MessageBoxCustum/Load failed. '" + resource_path + "'"));
        return (GameObject) null;
      }
      GameObject gameObject2 = (GameObject) Object.Instantiate<GameObject>((M0) gameObject1);
      if (!Object.op_Implicit((Object) gameObject2))
      {
        Debug.LogError((object) ("FlowNode_MessageBoxCustum/Instantiate failed. '" + resource_path + "'"));
        return (GameObject) null;
      }
      Win_Btn_Decide_Title_Flx component = (Win_Btn_Decide_Title_Flx) gameObject2.GetComponent<Win_Btn_Decide_Title_Flx>();
      if (!Object.op_Implicit((Object) component))
      {
        Debug.LogError((object) "FlowNode_MessageBoxCustum/Component not attached. 'Win_Btn_Decide_Title_Flx'");
        return (GameObject) null;
      }
      ((Component) component).get_transform().SetParent(((Component) canvas).get_transform(), false);
      component.Text_Title.set_text(title);
      component.Text_Message.set_text(msg);
      component.OnClickYes = ok_event_listener;
      return ((Component) component).get_gameObject();
    }
  }
}
