namespace SRPG
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    [Pin(0x65, "NGワードが存在", 1, 0x65), Pin(100, "定型文の変更", 1, 100), Pin(1, "初期化", 0, 1)]
    public class ChatEditTemplateMessage : MonoBehaviour, IFlowInterface
    {
        [SerializeField]
        private SRPG_Button[] submit_buttons;
        [SerializeField]
        private InputFieldCensorship[] input_fields;
        private ChatUtility.Json_ChatTemplateData prefs_data;
        private List<ChatUtility.ChatInspectionMaster> chat_inspecton_master;
        private bool is_loaded_inspection_master;

        public ChatEditTemplateMessage()
        {
            base..ctor();
            return;
        }

        public void Activated(int pinID)
        {
            if (pinID != 1)
            {
                goto Label_000D;
            }
            this.Init();
        Label_000D:
            return;
        }

        private unsafe void Init()
        {
            if (this.is_loaded_inspection_master != null)
            {
                goto Label_0031;
            }
            this.chat_inspecton_master = ChatUtility.LoadInspectionMaster(&this.is_loaded_inspection_master);
            if (this.is_loaded_inspection_master != null)
            {
                goto Label_0031;
            }
            DebugUtility.LogError("FAILED : ChatUtility.LoadInspectionMaster");
        Label_0031:
            this.InitButtons();
            this.InitInputFields();
            return;
        }

        private unsafe void InitButtons()
        {
            int num;
            if (this.submit_buttons != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            num = 0;
            goto Label_005D;
        Label_0013:
            this.submit_buttons[num].set_name(&num.ToString());
            this.submit_buttons[num].RemoveListener(new SRPG_Button.ButtonClickEvent(this.OnClickSubmitButton));
            this.submit_buttons[num].AddListener(new SRPG_Button.ButtonClickEvent(this.OnClickSubmitButton));
            num += 1;
        Label_005D:
            if (num < ((int) this.submit_buttons.Length))
            {
                goto Label_0013;
            }
            return;
        }

        private void InitInputFields()
        {
            string[] strArray;
            int num;
            int num2;
            if (this.input_fields != null)
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
            if (((int) this.prefs_data.messages.Length) >= ((int) this.input_fields.Length))
            {
                goto Label_00B2;
            }
            strArray = new string[(int) this.input_fields.Length];
            num = 0;
            goto Label_0098;
        Label_0052:
            if (((int) this.prefs_data.messages.Length) < num)
            {
                goto Label_007A;
            }
            strArray[num] = this.prefs_data.messages[num];
            goto Label_0094;
        Label_007A:
            strArray[num] = LocalizedText.Get("sys.CHAT_DEFAULT_TEMPLATE_MESSAGE_" + ((int) (num + 1)));
        Label_0094:
            num += 1;
        Label_0098:
            if (num < ((int) this.input_fields.Length))
            {
                goto Label_0052;
            }
            this.prefs_data.messages = strArray;
        Label_00B2:
            num2 = 0;
            goto Label_00EA;
        Label_00B9:
            if (((int) this.input_fields.Length) > num2)
            {
                goto Label_00CC;
            }
            goto Label_00FD;
        Label_00CC:
            SRPG_Extensions.SetText(this.input_fields[num2], this.prefs_data.messages[num2]);
            num2 += 1;
        Label_00EA:
            if (num2 < ((int) this.prefs_data.messages.Length))
            {
                goto Label_00B9;
            }
        Label_00FD:
            return;
        }

        private unsafe void OnClickSubmitButton(SRPG_Button button)
        {
            int num;
            string str;
            string str2;
            bool flag;
            num = 0;
            if (int.TryParse(button.get_name(), &num) != null)
            {
                goto Label_0015;
            }
            return;
        Label_0015:
            if (this.input_fields[num].get_text().Length <= 0)
            {
                goto Label_0051;
            }
            if ((this.input_fields[num].get_text() == this.prefs_data.messages[num]) == null)
            {
                goto Label_005A;
            }
        Label_0051:
            FlowNode_GameObject.ActivateOutputLinks(this, 100);
            return;
        Label_005A:
            str = this.input_fields[num].get_text();
            str2 = ChatUtility.ReplaceNGWord(this.input_fields[num].get_text(), this.chat_inspecton_master, "*");
            if ((str != str2) == null)
            {
                goto Label_00B7;
            }
            SRPG_Extensions.SetText(this.input_fields[num], this.prefs_data.messages[num]);
            FlowNode_GameObject.ActivateOutputLinks(this, 0x65);
            return;
        Label_00B7:
            SRPG_Extensions.SetText(this.input_fields[num], str2);
            this.prefs_data.messages[num] = str2;
            ChatUtility.SaveTemplateMessage(this.prefs_data);
            FlowNode_GameObject.ActivateOutputLinks(this, 100);
            return;
        }
    }
}

