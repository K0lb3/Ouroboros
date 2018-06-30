namespace SRPG
{
    using System;

    [Serializable]
    public class JSON_ArenaWinResult : JSON_ArenaResult
    {
        public string scale;

        public JSON_ArenaWinResult()
        {
            base..ctor();
            return;
        }
    }
}

