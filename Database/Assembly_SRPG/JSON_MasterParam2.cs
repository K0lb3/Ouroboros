namespace SRPG
{
    using System;

    [Serializable]
    public class JSON_MasterParam2
    {
        public JSON_InitPlayer[] InitPlayer;
        public JSON_InitUnit[] InitUnit;

        public JSON_MasterParam2()
        {
            base..ctor();
            return;
        }
    }
}

