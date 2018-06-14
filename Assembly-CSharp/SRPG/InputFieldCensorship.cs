// Decompiled with JetBrains decompiler
// Type: SRPG.InputFieldCensorship
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using UnityEngine.UI;

namespace SRPG
{
  public class InputFieldCensorship : SRPG_InputField
  {
    protected virtual void Start()
    {
      InputFieldCensorship inputFieldCensorship = this;
      // ISSUE: method pointer
      inputFieldCensorship.set_onValidateInput((InputField.OnValidateInput) System.Delegate.Combine((System.Delegate) inputFieldCensorship.get_onValidateInput(), (System.Delegate) new InputField.OnValidateInput((object) this, __methodptr(MyValidate))));
    }

    private char MyValidate(string input, int charIndex, char addedChar)
    {
      GameSettings instance = GameSettings.Instance;
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) instance, (UnityEngine.Object) null) || Array.IndexOf<char>(instance.ValidInputChars, addedChar) < 0 || this.get_characterValidation() == 4 && (int) addedChar == 32)
        return char.MinValue;
      return addedChar;
    }

    public void EndEdit(string text)
    {
      if (text.Length > this.get_characterLimit())
        text = text.Substring(0, this.get_characterLimit());
      this.set_text(text);
    }

    [Serializable]
    public class ValidCodeSegment
    {
      public int Start;
      public int End;
    }
  }
}
