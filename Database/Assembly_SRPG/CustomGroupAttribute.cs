namespace SRPG
{
    using System;

    public class CustomGroupAttribute : Attribute
    {
        public CustomGroupAttribute(string _groupName)
        {
            base..ctor();
            return;
        }
    }
}

