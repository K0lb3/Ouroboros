// Decompiled with JetBrains decompiler
// Type: SRPG.ChatTemplateMessage
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.Events;

namespace SRPG
{
  public class ChatTemplateMessage : MonoBehaviour
  {
    [SerializeField]
    private SRPG_Button[] template_message_buttons;
    [SerializeField]
    private ChatWindow chat_window;
    [SerializeField]
    private SRPG_Button next_page_button;
    [SerializeField]
    private SRPG_Button back_page_button;
    private ChatUtility.Json_ChatTemplateData prefs_data;
    private int ONE_PAGE_ITEM_MAX;
    private int LAST_PAGE;
    private int current_page;

    public ChatTemplateMessage()
    {
      base.\u002Ector();
    }

    public void LoadTemplateMessage()
    {
      if (this.template_message_buttons == null)
        return;
      this.prefs_data = ChatUtility.LoadChatTemplateMessage();
      if (this.prefs_data == null)
        return;
      this.ONE_PAGE_ITEM_MAX = this.template_message_buttons.Length;
      this.LAST_PAGE = Mathf.Max(this.prefs_data.messages.Length / this.ONE_PAGE_ITEM_MAX + (this.prefs_data.messages.Length % this.ONE_PAGE_ITEM_MAX != 0 ? 1 : 0) - 1, 0);
      this.SetupButtons();
    }

    private void Awake()
    {
      this.LoadTemplateMessage();
      this.SetupPageButton();
    }

    private void SetupButtons()
    {
      if (this.template_message_buttons == null)
        return;
      int num1 = this.current_page * this.ONE_PAGE_ITEM_MAX;
      int num2 = num1 + (this.ONE_PAGE_ITEM_MAX - 1);
      int index1 = 0;
      for (int index2 = 0; index2 < this.template_message_buttons.Length; ++index2)
      {
        ((UnityEventBase) this.template_message_buttons[index2].get_onClick()).RemoveAllListeners();
        ((Component) this.template_message_buttons[index2]).get_gameObject().SetActive(false);
      }
      for (int index2 = num1; index2 <= num2; ++index2)
      {
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: variable of a compiler-generated type
        ChatTemplateMessage.\u003CSetupButtons\u003Ec__AnonStorey315 buttonsCAnonStorey315 = new ChatTemplateMessage.\u003CSetupButtons\u003Ec__AnonStorey315();
        // ISSUE: reference to a compiler-generated field
        buttonsCAnonStorey315.\u003C\u003Ef__this = this;
        if (this.prefs_data.messages.Length <= index2)
          break;
        if (this.prefs_data.messages[index2] != null && !Object.op_Equality((Object) this.template_message_buttons[index1], (Object) null))
        {
          ((Component) this.template_message_buttons[index1]).get_gameObject().SetActive(true);
          LText componentInChildren = (LText) ((Component) this.template_message_buttons[index1]).GetComponentInChildren<LText>();
          if (Object.op_Inequality((Object) componentInChildren, (Object) null))
            componentInChildren.set_text(this.prefs_data.messages[index2]);
          // ISSUE: reference to a compiler-generated field
          buttonsCAnonStorey315.msg_index = index2;
          // ISSUE: method pointer
          ((UnityEvent) this.template_message_buttons[index1].get_onClick()).AddListener(new UnityAction((object) buttonsCAnonStorey315, __methodptr(\u003C\u003Em__2B9)));
          ++index1;
        }
      }
    }

    private void SetupPageButton()
    {
      // ISSUE: method pointer
      ((UnityEvent) this.next_page_button.get_onClick()).AddListener(new UnityAction((object) this, __methodptr(\u003CSetupPageButton\u003Em__2BA)));
      // ISSUE: method pointer
      ((UnityEvent) this.back_page_button.get_onClick()).AddListener(new UnityAction((object) this, __methodptr(\u003CSetupPageButton\u003Em__2BB)));
    }

    private void OnTapTemplateMessage(int msg_index)
    {
      if (Object.op_Equality((Object) this.chat_window, (Object) null))
        return;
      this.chat_window.SetMessageDataToFlowNode(this.prefs_data.messages[msg_index], true);
      this.chat_window.SetActiveUsefulWindowObject(false);
    }

    private void PageNext()
    {
      ++this.current_page;
      if (this.current_page > this.LAST_PAGE)
        this.current_page = 0;
      this.SetupButtons();
    }

    private void PageBack()
    {
      --this.current_page;
      if (this.current_page < 0)
        this.current_page = this.LAST_PAGE;
      this.SetupButtons();
    }
  }
}
