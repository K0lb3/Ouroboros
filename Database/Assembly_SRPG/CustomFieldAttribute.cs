namespace SRPG
{
    using System;

    public class CustomFieldAttribute : Attribute
    {
        public CustomFieldAttribute(string _text, Type _type)
        {
            base..ctor();
            return;
        }

        public enum Type
        {
            MonoBehaviour,
            GameObject,
            UIText,
            UIRawImage,
            UIImage,
            UISprite
        }
    }
}

