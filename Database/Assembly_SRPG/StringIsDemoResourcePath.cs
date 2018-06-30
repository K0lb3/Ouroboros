namespace SRPG
{
    using System;
    using UnityEngine;

    public class StringIsDemoResourcePath : PropertyAttribute
    {
        public Type ResourceType;
        public string ParentDirectory;

        public StringIsDemoResourcePath(Type type)
        {
            base..ctor();
            this.ResourceType = type;
            this.ParentDirectory = null;
            return;
        }

        public StringIsDemoResourcePath(Type type, string dir)
        {
            base..ctor();
            this.ResourceType = type;
            this.ParentDirectory = dir;
            return;
        }
    }
}

