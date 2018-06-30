namespace SRPG
{
    using System;

    [Serializable]
    public class JSON_MapEffectParam
    {
        public string iname;
        public string name;
        public string expr;
        public string[] skills;

        public JSON_MapEffectParam()
        {
            base..ctor();
            return;
        }
    }
}

