namespace SRPG
{
    using System;

    [Serializable]
    public class JSON_AwardList
    {
        public AwardParam[] awards;

        public JSON_AwardList()
        {
            base..ctor();
            return;
        }
    }
}

