namespace SRPG
{
    using System;
    using UnityEngine;

    public class StringIsResourcePathPopup : PropertyAttribute
    {
        public Type ResourceType;
        public string ParentDirectory;

        public StringIsResourcePathPopup(Type type)
        {
            base..ctor();
            this.ResourceType = type;
            this.ParentDirectory = null;
            return;
        }

        public StringIsResourcePathPopup(Type type, string dir)
        {
            base..ctor();
            this.ResourceType = type;
            this.ParentDirectory = dir;
            return;
        }
    }
}

