namespace SRPG
{
    using System;

    public class QuestCondParam
    {
        public string iname;
        public int plvmax;
        public int plvmin;
        public int ulvmax;
        public int ulvmin;
        public int[] elem;
        public bool isElemLimit;
        public string[] job;
        public PartyCondType party_type;
        public string[] unit;
        public ESex sex;
        public int rmax;
        public int rmin;
        public int hmax;
        public int hmin;
        public int wmax;
        public int wmin;
        public int[] jobset;
        public string[] birth;

        public QuestCondParam()
        {
            base..ctor();
            return;
        }

        public bool Deserialize(JSON_QuestCondParam json)
        {
            int num;
            int num2;
            int num3;
            int num4;
            int num5;
            int num6;
            this.iname = json.iname;
            this.plvmax = json.plvmax;
            this.plvmin = json.plvmin;
            this.ulvmax = json.ulvmax;
            this.ulvmin = json.ulvmin;
            this.sex = json.sex;
            this.rmax = json.rmax;
            this.rmin = json.rmin;
            this.hmax = json.hmax;
            this.hmin = json.hmin;
            this.wmax = json.wmax;
            this.wmin = json.wmin;
            num = 0;
            this.elem = new int[Enum.GetValues(typeof(EElement)).Length];
            this.elem[0] = num6 = json.el_none;
            num += num6;
            this.elem[1] = num6 = json.el_fire;
            num += num6;
            this.elem[2] = num6 = json.el_watr;
            num += num6;
            this.elem[3] = num6 = json.el_wind;
            num += num6;
            this.elem[4] = num6 = json.el_thdr;
            num += num6;
            this.elem[5] = num6 = json.el_lit;
            num += num6;
            this.elem[6] = num6 = json.el_drk;
            num += num6;
            this.isElemLimit = num > 0;
            num2 = 0;
            this.jobset = new int[4];
            this.jobset[num2++] = json.jobset1;
            this.jobset[num2++] = json.jobset2;
            this.jobset[num2++] = json.jobset3;
            if (json.job == null)
            {
                goto Label_01E0;
            }
            this.job = new string[(int) json.job.Length];
            num3 = 0;
            goto Label_01D2;
        Label_01BE:
            this.job[num3] = json.job[num3];
            num3 += 1;
        Label_01D2:
            if (num3 < ((int) this.job.Length))
            {
                goto Label_01BE;
            }
        Label_01E0:
            if (json.unit == null)
            {
                goto Label_0227;
            }
            this.unit = new string[(int) json.unit.Length];
            num4 = 0;
            goto Label_0219;
        Label_0205:
            this.unit[num4] = json.unit[num4];
            num4 += 1;
        Label_0219:
            if (num4 < ((int) this.unit.Length))
            {
                goto Label_0205;
            }
        Label_0227:
            if (json.birth == null)
            {
                goto Label_0274;
            }
            this.birth = new string[(int) json.birth.Length];
            num5 = 0;
            goto Label_0265;
        Label_024D:
            this.birth[num5] = json.birth[num5];
            num5 += 1;
        Label_0265:
            if (num5 < ((int) this.birth.Length))
            {
                goto Label_024D;
            }
        Label_0274:
            this.party_type = (Enum.IsDefined(typeof(PartyCondType), (int) json.party_type) == null) ? 0 : json.party_type;
            return 1;
        }
    }
}

