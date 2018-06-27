// Decompiled with JetBrains decompiler
// Type: SRPG.CharLimitFormat
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
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
    private CharLimitFormat.EditType edit_type;
    [SerializeField]
    private CharLimitFormat.FormatType format_type;
    [SerializeField]
    private InputField input_field;
    [SerializeField]
    private bool check_update;
    private bool is_finish_edit;
    private Text text;

    public CharLimitFormat()
    {
      base.\u002Ector();
    }

    private void Awake()
    {
      this.text = (Text) ((Component) this).GetComponent<Text>();
      if (this.check_update)
        return;
      this.Edit();
    }

    private void Update()
    {
      if (!this.check_update)
        return;
      this.EditForUpdate();
    }

    private void EditForUpdate()
    {
      if (this.is_finish_edit || this.text.get_text().StartsWith("sys."))
        return;
      this.Edit();
    }

    private void Edit()
    {
      if (Object.op_Equality((Object) this.text, (Object) null) || !this.force_override_limit && Object.op_Equality((Object) this.input_field, (Object) null))
        return;
      string stringFormat = this.GetStringFormat(this.format_type);
      string empty = string.Empty;
      int num = !this.force_override_limit ? this.input_field.get_characterLimit() : this.force_override_limit_value;
      if (num > 0)
        empty = num.ToString();
      string str1 = string.Empty;
      if (this.append_char_type && Object.op_Inequality((Object) this.input_field, (Object) null))
        str1 = this.GetCharTypeText(this.input_field);
      string str2 = string.Format(stringFormat, (object) empty, (object) str1);
      if (this.display_brackets)
        str2 = "(" + str2 + ")";
      switch (this.edit_type)
      {
        case CharLimitFormat.EditType.Append:
          Text text = this.text;
          text.set_text(text.get_text() + str2);
          break;
        case CharLimitFormat.EditType.Replace:
          this.text.set_text(str2);
          break;
      }
      this.is_finish_edit = true;
    }

    private string GetStringFormat(CharLimitFormat.FormatType _format_type)
    {
      switch (_format_type)
      {
        case CharLimitFormat.FormatType.Simple:
          return LocalizedText.Get("sys.CHAR_LIMIT_FORMAT_SIMPLE");
        case CharLimitFormat.FormatType.Append_Saidai:
          return LocalizedText.Get("sys.CHAR_LIMIT_FORMAT_APPEND_SAIDAI");
        case CharLimitFormat.FormatType.Append_Inai:
          return LocalizedText.Get("sys.CHAR_LIMIT_FORMAT_APPEND_INAI");
        case CharLimitFormat.FormatType.Navi_Saidai:
          return LocalizedText.Get("sys.CHAR_LIMIT_FORMAT_NAVI_SAIDAI");
        default:
          return string.Empty;
      }
    }

    private string GetCharTypeText(InputField _input_field)
    {
      string separatorString = this.SEPARATOR_STRING;
      InputField.ContentType contentType = _input_field.get_contentType();
      switch (contentType - 2)
      {
        case 0:
          return separatorString + LocalizedText.Get("sys.CHAR_TYPE_RESTRICTED_FORMAT_NUMBER");
        case 2:
          return separatorString + LocalizedText.Get("sys.CHAR_TYPE_RESTRICTED_FORMAT_ALPHANUMERIC");
        default:
          if (contentType == 9)
            return this.GetCharTypeTextByTypeCustom(_input_field);
          return string.Empty;
      }
    }

    private string GetCharTypeTextByTypeCustom(InputField _input_field)
    {
      string separatorString = this.SEPARATOR_STRING;
      switch (_input_field.get_characterValidation() - 1)
      {
        case 0:
          return separatorString + LocalizedText.Get("sys.CHAR_TYPE_RESTRICTED_FORMAT_NUMBER");
        case 2:
          return separatorString + LocalizedText.Get("sys.CHAR_TYPE_RESTRICTED_FORMAT_ALPHANUMERIC");
        default:
          return string.Empty;
      }
    }

    private enum EditType
    {
      Append,
      Replace,
    }

    private enum FormatType
    {
      Simple,
      Append_Saidai,
      Append_Inai,
      Navi_Saidai,
    }
  }
}
