namespace SRPG
{
    using System;

    public class LogError : BattleLog
    {
        public int code;
        public string text;

        public LogError()
        {
            base..ctor();
            return;
        }
    }
}

