namespace SRPG
{
    using System;
    using System.Runtime.InteropServices;
    using UnityEngine;

    public class StringIsTextID : PropertyAttribute
    {
        public bool ContainsVoiceID;

        public StringIsTextID(bool containsVoiceID)
        {
            base..ctor();
            this.ContainsVoiceID = containsVoiceID;
            return;
        }
    }
}

