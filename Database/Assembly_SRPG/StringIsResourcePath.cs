namespace SRPG
{
    using System;
    using UnityEngine;

    public class StringIsResourcePath : PropertyAttribute
    {
        public Type ResourceType;
        public string ParentDirectory;

        public StringIsResourcePath(Type type)
        {
            base..ctor();
            this.ResourceType = type;
            this.ParentDirectory = null;
            return;
        }

        public StringIsResourcePath(Type type, string dir)
        {
            base..ctor();
            this.ResourceType = type;
            this.ParentDirectory = dir;
            return;
        }
    }
}

