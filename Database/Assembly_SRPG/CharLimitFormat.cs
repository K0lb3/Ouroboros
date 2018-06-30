namespace SRPG
{
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    public class CharLimitFormat : MonoBehaviour
    {
        private readonly string SEPARATOR_STRING;
        [SerializeField]
        private bool display_brackets;
        [SerializeField]
        private bool force_override_limit;
        [SerializeField]
        private int force_override_limit_value;
        [SerializeField]
        private bool append_char_type;
        [SerializeField]
        private EditType edit_type;
        [SerializeField]
        private FormatType format_type;
        [SerializeField]
        private InputField input_field;
        [SerializeField]
        private bool check_update;
        private bool is_finish_edit;
        private Text text;

        public CharLimitFormat()
        {
            this.SEPARATOR_STRING = "  ";
            base..ctor();
            return;
        }

        private void Awake()
        {
            this.text = base.GetComponent<Text>();
            if (this.check_update == null)
            {
                goto Label_0018;
            }
            return;
        Label_0018:
            this.Edit();
            return;
        }

        private unsafe void Edit()
        {
            string str;
            string str2;
            int num;
            string str3;
            string str4;
            EditType type;
            if ((this.text == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            if ((this.force_override_limit != null) || ((this.input_field == null) == null))
            {
                goto Label_002F;
            }
            return;
        Label_002F:
            str = this.GetStringFormat(this.format_type);
            str2 = string.Empty;
            num = (this.force_override_limit == null) ? this.input_field.get_characterLimit() : this.force_override_limit_value;
            if (num <= 0)
            {
                goto Label_0073;
            }
            str2 = &num.ToString();
        Label_0073:
            str3 = string.Empty;
            if (this.append_char_type == null)
            {
                goto Label_00A2;
            }
            if ((this.input_field != null) == null)
            {
                goto Label_00A2;
            }
            str3 = this.GetCharTypeText(this.input_field);
        Label_00A2:
            str4 = string.Format(str, str2, str3);
            if (this.display_brackets == null)
            {
                goto Label_00CA;
            }
            str4 = "(" + str4 + ")";
        Label_00CA:
            type = this.edit_type;
            if (type == null)
            {
                goto Label_00E6;
            }
            if (type == 1)
            {
                goto Label_0103;
            }
            goto Label_0115;
        Label_00E6:
            this.text.set_text(this.text.get_text() + str4);
            goto Label_0115;
        Label_0103:
            this.text.set_text(str4);
        Label_0115:
            this.is_finish_edit = 1;
            return;
        }

        private void EditForUpdate()
        {
            if (this.is_finish_edit == null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            if (this.text.get_text().StartsWith("sys.") == null)
            {
                goto Label_0027;
            }
            return;
        Label_0027:
            this.Edit();
            return;
        }

        private string GetCharTypeText(InputField _input_field)
        {
            string str;
            InputField.ContentType type;
            str = this.SEPARATOR_STRING;
            type = _input_field.get_contentType();
            switch ((type - 2))
            {
                case 0:
                    goto Label_002F;

                case 1:
                    goto Label_0022;

                case 2:
                    goto Label_0040;
            }
        Label_0022:
            if (type == 9)
            {
                goto Label_0051;
            }
            goto Label_0059;
        Label_002F:
            return (str + LocalizedText.Get("sys.CHAR_TYPE_RESTRICTED_FORMAT_NUMBER"));
        Label_0040:
            return (str + LocalizedText.Get("sys.CHAR_TYPE_RESTRICTED_FORMAT_ALPHANUMERIC"));
        Label_0051:
            return this.GetCharTypeTextByTypeCustom(_input_field);
        Label_0059:;
        Label_005E:
            return string.Empty;
        }

        private string GetCharTypeTextByTypeCustom(InputField _input_field)
        {
            string str;
            InputField.CharacterValidation validation;
            str = this.SEPARATOR_STRING;
            switch ((_input_field.get_characterValidation() - 1))
            {
                case 0:
                    goto Label_0027;

                case 1:
                    goto Label_0049;

                case 2:
                    goto Label_0038;
            }
            goto Label_0049;
        Label_0027:
            return (str + LocalizedText.Get("sys.CHAR_TYPE_RESTRICTED_FORMAT_NUMBER"));
        Label_0038:
            return (str + LocalizedText.Get("sys.CHAR_TYPE_RESTRICTED_FORMAT_ALPHANUMERIC"));
        Label_0049:;
        Label_004E:
            return string.Empty;
        }

        private string GetStringFormat(FormatType _format_type)
        {
            FormatType type;
            type = _format_type;
            switch (type)
            {
                case 0:
                    goto Label_001D;

                case 1:
                    goto Label_0028;

                case 2:
                    goto Label_0033;

                case 3:
                    goto Label_003E;
            }
            goto Label_0049;
        Label_001D:
            return LocalizedText.Get("sys.CHAR_LIMIT_FORMAT_SIMPLE");
        Label_0028:
            return LocalizedText.Get("sys.CHAR_LIMIT_FORMAT_APPEND_SAIDAI");
        Label_0033:
            return LocalizedText.Get("sys.CHAR_LIMIT_FORMAT_APPEND_INAI");
        Label_003E:
            return LocalizedText.Get("sys.CHAR_LIMIT_FORMAT_NAVI_SAIDAI");
        Label_0049:
            return string.Empty;
        }

        private void Update()
        {
            if (this.check_update != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            this.EditForUpdate();
            return;
        }

        private enum EditType
        {
            Append,
            Replace
        }

        private enum FormatType
        {
            Simple,
            Append_Saidai,
            Append_Inai,
            Navi_Saidai
        }
    }
}

