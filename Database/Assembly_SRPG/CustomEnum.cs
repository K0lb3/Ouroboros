namespace SRPG
{
    using System;
    using UnityEngine;

    public class CustomEnum : PropertyAttribute
    {
        public Type EnumType;
        public int DefaultValue;

        public CustomEnum(Type enumType, int defaultValue)
        {
            base..ctor();
            this.EnumType = enumType;
            this.DefaultValue = defaultValue;
            return;
        }
    }
}

