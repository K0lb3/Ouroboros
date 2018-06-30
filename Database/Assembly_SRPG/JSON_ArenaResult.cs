namespace SRPG
{
    using System;

    [Serializable]
    public class JSON_ArenaResult
    {
        public int rank;
        public int coin;
        public int gold;
        public int ac;
        public string item1;
        public string item2;
        public string item3;
        public string item4;
        public string item5;
        public int num1;
        public int num2;
        public int num3;
        public int num4;
        public int num5;

        public JSON_ArenaResult()
        {
            base..ctor();
            return;
        }
    }
}

