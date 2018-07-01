// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_Message
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(10, "Open", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Closed", FlowNode.PinTypes.Output, 1)]
  [FlowNode.Pin(11, "ForceClose", FlowNode.PinTypes.Input, 11)]
  [FlowNode.Pin(100, "Opened", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(101, "ForceClosed", FlowNode.PinTypes.Output, 101)]
  [FlowNode.NodeType("UI/Message", 32741)]
  public class FlowNode_Message : FlowNode
  {
    public string Text;
    public bool systemModal;
    public int systemModalPriority;
    public GameObject parent;
    public string parentName;
    public bool unscaledTime;
    public bool richTag;
    public bool anim;
    public bool input;
    public float bgAlpha;
    public float autoClose;
    private Win_SysMessage_Flx m_Window;

    public void Update()
    {
      if (!Object.op_Inequality((Object) this.m_Window, (Object) null))
        return;
      if (Object.op_Equality((Object) ((Component) this.m_Window).get_gameObject(), (Object) null))
        this.ActivateOutputLinks(1);
      this.m_Window = (Win_SysMessage_Flx) null;
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
          GameSettings instance = GameSettings.Instance;
          Canvas canvas = UIUtility.PushCanvas(this.systemModal, this.systemModalPriority);
          if (Object.op_Inequality((Object) this.parent, (Object) null))
            ((Component) canvas).get_transform().SetParent(this.parent.get_transform());
          this.m_Window = (Win_SysMessage_Flx) Object.Instantiate<Win_SysMessage_Flx>((M0) instance.Dialogs.SysMsgDialog);
          ((Component) this.m_Window).get_transform().SetParent(((Component) canvas).get_transform(), false);
          this.m_Window.Text_Message.set_text(text);
          this.m_Window.Initialize(this.input, this.bgAlpha);
          if (this.anim)
            this.m_Window.StartAnim();
          if ((double) this.autoClose > 0.0)
            this.m_Window.AutoClose(this.autoClose);
          if (Object.op_Implicit((Object) this.m_Window) && this.unscaledTime)
          {
            Animator component = (Animator) ((Component) this.m_Window).GetComponent<Animator>();
            if (Object.op_Inequality((Object) component, (Object) null))
              component.set_updateMode((AnimatorUpdateMode) 2);
          }
          this.ActivateOutputLinks(100);
          break;
        case 11:
          if (Object.op_Equality((Object) this.m_Window, (Object) null))
            break;
          if (Object.op_Inequality((Object) this.m_Window, (Object) null))
          {
            this.m_Window.BeginClose();
            this.m_Window = (Win_SysMessage_Flx) null;
          }
          this.ActivateOutputLinks(101);
          break;
      }
    }
  }
}
