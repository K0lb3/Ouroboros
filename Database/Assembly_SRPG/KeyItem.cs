namespace SRPG
{
    using GR;
    using System;

    [Serializable]
    public class KeyItem
    {
        public string iname;
        public int num;

        public KeyItem()
        {
            base..ctor();
            return;
        }

        public bool IsHasItem()
        {
            int num;
            if (MonoSingleton<GameManager>.Instance.Player.GetItemAmount(this.iname) >= this.num)
            {
                goto Label_0024;
            }
            return 0;
        Label_0024:
            return 1;
        }
    }
}

