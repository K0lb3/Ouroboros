namespace SRPG
{
    using System;
    using System.Runtime.InteropServices;
    using UnityEngine;

    public class StringIsTextIDPopup : PropertyAttribute
    {
        public bool ContainsVoiceID;

        public StringIsTextIDPopup(bool containsVoiceID)
        {
            base..ctor();
            this.ContainsVoiceID = containsVoiceID;
            return;
        }
    }
}

