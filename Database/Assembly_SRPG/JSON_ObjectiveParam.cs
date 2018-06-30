namespace SRPG
{
    using System;

    [Serializable]
    public class JSON_ObjectiveParam
    {
        public string iname;
        public JSON_InnerObjective[] objective;

        public JSON_ObjectiveParam()
        {
            base..ctor();
            return;
        }
    }
}

