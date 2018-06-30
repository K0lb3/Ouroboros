namespace SRPG
{
    using System;

    [Serializable]
    public class JSON_StatusCoefficientParam
    {
        public float hp;
        public float atk;
        public float def;
        public float matk;
        public float mdef;
        public float dex;
        public float spd;
        public float cri;
        public float luck;
        public float cmb;
        public float move;
        public float jmp;

        public JSON_StatusCoefficientParam()
        {
            base..ctor();
            return;
        }
    }
}

