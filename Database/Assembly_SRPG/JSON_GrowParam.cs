namespace SRPG
{
    using System;

    [Serializable]
    public class JSON_GrowParam
    {
        public string type;
        public JSON_GrowCurve[] curve;

        public JSON_GrowParam()
        {
            base..ctor();
            return;
        }
    }
}

