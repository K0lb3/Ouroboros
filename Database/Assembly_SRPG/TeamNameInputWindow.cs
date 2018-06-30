namespace SRPG
{
    using System;
    using UnityEngine;

    public class TeamNameInputWindow : MonoBehaviour
    {
        [SerializeField]
        private InputFieldCensorship inputField;

        public TeamNameInputWindow()
        {
            base..ctor();
            return;
        }

        public void SetInputName()
        {
            string str;
            if (string.IsNullOrEmpty(this.inputField.get_text()) != null)
            {
                goto Label_0050;
            }
            str = this.inputField.get_text();
            if (str.Length <= this.inputField.get_characterLimit())
            {
                goto Label_004A;
            }
            str = str.Substring(0, this.inputField.get_characterLimit());
        Label_004A:
            GlobalVars.TeamName = str;
        Label_0050:
            return;
        }
    }
}

