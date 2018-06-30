namespace SRPG
{
    using System;
    using UnityEngine.UI;

    public class InputFieldCensorship : SRPG_InputField
    {
        public InputFieldCensorship()
        {
            base..ctor();
            return;
        }

        public void EndEdit(string text)
        {
            if (text.Length <= base.get_characterLimit())
            {
                goto Label_0020;
            }
            text = text.Substring(0, base.get_characterLimit());
        Label_0020:
            base.set_text(text);
            return;
        }

        private char MyValidate(string input, int charIndex, char addedChar)
        {
            GameSettings settings;
            settings = GameSettings.Instance;
            if ((settings == null) == null)
            {
                goto Label_0014;
            }
            return 0;
        Label_0014:
            if (Array.IndexOf<char>(settings.ValidInputChars, addedChar) >= 0)
            {
                goto Label_0028;
            }
            return 0;
        Label_0028:
            return addedChar;
        }

        protected override void Start()
        {
            base.set_onValidateInput((InputField.OnValidateInput) Delegate.Combine(base.get_onValidateInput(), new InputField.OnValidateInput(this, this.MyValidate)));
            return;
        }

        [Serializable]
        public class ValidCodeSegment
        {
            public int Start;
            public int End;

            public ValidCodeSegment()
            {
                base..ctor();
                return;
            }
        }
    }
}

