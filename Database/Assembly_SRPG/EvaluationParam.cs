namespace SRPG
{
    using System;

    public class EvaluationParam
    {
        public string iname;
        public OInt value;
        public StatusParam status;

        public EvaluationParam()
        {
            this.status = new StatusParam();
            base..ctor();
            return;
        }

        public bool Deserialize(JSON_EvaluationParam json)
        {
            if (json != null)
            {
                goto Label_0008;
            }
            return 0;
        Label_0008:
            this.iname = json.iname;
            this.value = json.val;
            this.status.Clear();
            this.status.hp = json.hp;
            this.status.mp = json.mp;
            this.status.atk = json.atk;
            this.status.def = json.def;
            this.status.mag = json.mag;
            this.status.mnd = json.mnd;
            this.status.dex = json.dex;
            this.status.spd = json.spd;
            this.status.cri = json.cri;
            this.status.luk = json.luk;
            return 1;
        }
    }
}

