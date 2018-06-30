namespace SRPG
{
    using System;
    using System.Runtime.InteropServices;
    using UnityEngine;

    public class ParamChangeEffects : ScriptableObject
    {
        [HideInInspector]
        public EffectData[] Effects;

        public ParamChangeEffects()
        {
            this.Effects = new EffectData[0];
            base..ctor();
            return;
        }

        public unsafe Sprite FindSprite(string type, bool isDebuff)
        {
            int num;
            num = 0;
            goto Label_0051;
        Label_0007:
            if ((&(this.Effects[num]).TypeName == type) == null)
            {
                goto Label_004D;
            }
            if (isDebuff == null)
            {
                goto Label_003B;
            }
            return &(this.Effects[num]).OnDebuff;
        Label_003B:
            return &(this.Effects[num]).OnBuff;
        Label_004D:
            num += 1;
        Label_0051:
            if (num < ((int) this.Effects.Length))
            {
                goto Label_0007;
            }
            return null;
        }

        [Serializable, StructLayout(LayoutKind.Sequential)]
        public struct EffectData
        {
            public string TypeName;
            public Sprite OnBuff;
            public Sprite OnDebuff;
        }
    }
}

