// Decompiled with JetBrains decompiler
// Type: SRPG.InputFieldCensorship
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SRPG
{
  public class InputFieldCensorship : InputField
  {
    private int mCharacterLimit;

    public InputFieldCensorship()
    {
      base.\u002Ector();
    }

    protected virtual void Start()
    {
      ((UIBehaviour) this).Start();
      InputFieldCensorship inputFieldCensorship = this;
      // ISSUE: method pointer
      inputFieldCensorship.set_onValidateInput((InputField.OnValidateInput) System.Delegate.Combine((System.Delegate) inputFieldCensorship.get_onValidateInput(), (System.Delegate) new InputField.OnValidateInput((object) this, __methodptr(\u003CStart\u003Em__1CA))));
      this.mCharacterLimit = this.get_characterLimit();
      if (this.get_characterValidation() == 4)
        this.set_characterLimit(12);
      else
        this.set_characterLimit(1024);
    }

    private char MyValidate(string input, int charIndex, char addedChar)
    {
      GameSettings instance = GameSettings.Instance;
      if (Object.op_Equality((Object) instance, (Object) null) || 0 > Array.IndexOf<char>(instance.ValidInputChars, addedChar) || this.get_characterValidation() == 4 && (int) addedChar == 32)
        return char.MinValue;
      return addedChar;
    }

    public void EndEdit(string text)
    {
      if (text.Length > this.mCharacterLimit)
        text = text.Substring(0, this.mCharacterLimit);
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
