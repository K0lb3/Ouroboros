namespace SRPG
{
    using System;
    using UnityEngine;

    public class PortraitSet : ScriptableObject
    {
        public Texture2D Normal;
        public Texture2D Smile;
        public Texture2D Sad;
        public Texture2D Angry;

        public PortraitSet()
        {
            base..ctor();
            return;
        }

        public Texture2D GetEmotionImage(EmotionTypes type)
        {
            EmotionTypes types;
            types = type;
            switch ((types - 1))
            {
                case 0:
                    goto Label_0055;

                case 1:
                    goto Label_0038;

                case 2:
                    goto Label_001B;
            }
            goto Label_0072;
        Label_001B:
            if ((this.Angry == null) == null)
            {
                goto Label_0031;
            }
            goto Label_0072;
        Label_0031:
            return this.Angry;
        Label_0038:
            if ((this.Sad == null) == null)
            {
                goto Label_004E;
            }
            goto Label_0072;
        Label_004E:
            return this.Sad;
        Label_0055:
            if ((this.Smile == null) == null)
            {
                goto Label_006B;
            }
            goto Label_0072;
        Label_006B:
            return this.Smile;
        Label_0072:
            return this.Normal;
        }

        public enum EmotionTypes
        {
            Normal,
            Smile,
            Sad,
            Angry
        }
    }
}

