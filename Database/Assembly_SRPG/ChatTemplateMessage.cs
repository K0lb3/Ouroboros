namespace SRPG
{
    using System;
    using System.Runtime.CompilerServices;
    using UnityEngine;
    using UnityEngine.Events;

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
            base..ctor();
            return;
        }

        [CompilerGenerated]
        private void <SetupPageButton>m__2BA()
        {
            this.PageNext();
            return;
        }

        [CompilerGenerated]
        private void <SetupPageButton>m__2BB()
        {
            this.PageBack();
            return;
        }

        private void Awake()
        {
            this.LoadTemplateMessage();
            this.SetupPageButton();
            return;
        }

        public void LoadTemplateMessage()
        {
            int num;
            int num2;
            if (this.template_message_buttons != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            this.prefs_data = ChatUtility.LoadChatTemplateMessage();
            if (this.prefs_data != null)
            {
                goto Label_0023;
            }
            return;
        Label_0023:
            this.ONE_PAGE_ITEM_MAX = (int) this.template_message_buttons.Length;
            num = ((((int) this.prefs_data.messages.Length) % this.ONE_PAGE_ITEM_MAX) != null) ? 1 : 0;
            num2 = ((int) this.prefs_data.messages.Length) / this.ONE_PAGE_ITEM_MAX;
            this.LAST_PAGE = Mathf.Max((num2 + num) - 1, 0);
            this.SetupButtons();
            return;
        }

        private void OnTapTemplateMessage(int msg_index)
        {
            if ((this.chat_window == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            this.chat_window.SetMessageDataToFlowNode(this.prefs_data.messages[msg_index], 1);
            this.chat_window.SetActiveUsefulWindowObject(0);
            return;
        }

        private void PageBack()
        {
            this.current_page -= 1;
            if (this.current_page >= 0)
            {
                goto Label_0026;
            }
            this.current_page = this.LAST_PAGE;
        Label_0026:
            this.SetupButtons();
            return;
        }

        private void PageNext()
        {
            this.current_page += 1;
            if (this.current_page <= this.LAST_PAGE)
            {
                goto Label_0026;
            }
            this.current_page = 0;
        Label_0026:
            this.SetupButtons();
            return;
        }

        private void SetupButtons()
        {
            int num;
            int num2;
            int num3;
            int num4;
            int num5;
            LText text;
            <SetupButtons>c__AnonStorey315 storey;
            if (this.template_message_buttons != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            num = this.current_page * this.ONE_PAGE_ITEM_MAX;
            num2 = num + (this.ONE_PAGE_ITEM_MAX - 1);
            num3 = 0;
            num4 = 0;
            goto Label_0057;
        Label_002E:
            this.template_message_buttons[num4].get_onClick().RemoveAllListeners();
            this.template_message_buttons[num4].get_gameObject().SetActive(0);
            num4 += 1;
        Label_0057:
            if (num4 < ((int) this.template_message_buttons.Length))
            {
                goto Label_002E;
            }
            num5 = num;
            goto Label_013B;
        Label_006D:
            storey = new <SetupButtons>c__AnonStorey315();
            storey.<>f__this = this;
            if (((int) this.prefs_data.messages.Length) > num5)
            {
                goto Label_0095;
            }
            goto Label_0143;
        Label_0095:
            if (this.prefs_data.messages[num5] != null)
            {
                goto Label_00AD;
            }
            goto Label_0135;
        Label_00AD:
            if ((this.template_message_buttons[num3] == null) == null)
            {
                goto Label_00C5;
            }
            goto Label_0135;
        Label_00C5:
            this.template_message_buttons[num3].get_gameObject().SetActive(1);
            text = this.template_message_buttons[num3].GetComponentInChildren<LText>();
            if ((text != null) == null)
            {
                goto Label_0109;
            }
            text.set_text(this.prefs_data.messages[num5]);
        Label_0109:
            storey.msg_index = num5;
            this.template_message_buttons[num3].get_onClick().AddListener(new UnityAction(storey, this.<>m__2B9));
            num3 += 1;
        Label_0135:
            num5 += 1;
        Label_013B:
            if (num5 <= num2)
            {
                goto Label_006D;
            }
        Label_0143:
            return;
        }

        private void SetupPageButton()
        {
            this.next_page_button.get_onClick().AddListener(new UnityAction(this, this.<SetupPageButton>m__2BA));
            this.back_page_button.get_onClick().AddListener(new UnityAction(this, this.<SetupPageButton>m__2BB));
            return;
        }

        [CompilerGenerated]
        private sealed class <SetupButtons>c__AnonStorey315
        {
            internal int msg_index;
            internal ChatTemplateMessage <>f__this;

            public <SetupButtons>c__AnonStorey315()
            {
                base..ctor();
                return;
            }

            internal void <>m__2B9()
            {
                this.<>f__this.OnTapTemplateMessage(this.msg_index);
                return;
            }
        }
    }
}

