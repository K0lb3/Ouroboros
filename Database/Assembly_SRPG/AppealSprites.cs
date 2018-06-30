namespace SRPG
{
    using System;
    using System.Runtime.InteropServices;
    using UnityEngine;

    public class AppealSprites : ScriptableObject
    {
        public Item[] Items;
        private bool Dirty;

        public AppealSprites()
        {
            this.Items = new Item[7];
            this.Dirty = 1;
            base..ctor();
            return;
        }

        public unsafe Sprite GetSprite(string id)
        {
            int num;
            int num2;
            num = id.GetHashCode();
            if (this.Dirty == null)
            {
                goto Label_001F;
            }
            this.RecalcHashCodes();
            this.Dirty = 0;
        Label_001F:
            num2 = 0;
            goto Label_006F;
        Label_0026:
            if (num != &(this.Items[num2]).hash)
            {
                goto Label_006B;
            }
            if ((id == &(this.Items[num2]).ID) == null)
            {
                goto Label_006B;
            }
            return &(this.Items[num2]).Sprite;
        Label_006B:
            num2 += 1;
        Label_006F:
            if (num2 < ((int) this.Items.Length))
            {
                goto Label_0026;
            }
            return null;
        }

        private unsafe void RecalcHashCodes()
        {
            int num;
            num = 0;
            goto Label_0032;
        Label_0007:
            &(this.Items[num]).hash = &(this.Items[num]).ID.GetHashCode();
            num += 1;
        Label_0032:
            if (num < ((int) this.Items.Length))
            {
                goto Label_0007;
            }
            return;
        }

        [Serializable, StructLayout(LayoutKind.Sequential)]
        public struct Item
        {
            public string ID;
            public UnityEngine.Sprite Sprite;
            [NonSerialized]
            public int hash;
        }
    }
}

