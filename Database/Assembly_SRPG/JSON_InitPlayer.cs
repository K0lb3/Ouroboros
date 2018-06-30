namespace SRPG
{
    using System;

    [Serializable]
    public class JSON_InitPlayer
    {
        public int gold;
        public int coin;
        public int ap;
        public int exp;

        public JSON_InitPlayer()
        {
            base..ctor();
            return;
        }
    }
}

