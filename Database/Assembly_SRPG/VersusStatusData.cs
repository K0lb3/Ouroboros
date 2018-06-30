namespace SRPG
{
    using System;

    public class VersusStatusData
    {
        public int Hp;
        public int Atk;
        public int Def;
        public int Matk;
        public int Mdef;
        public int Dex;
        public int Spd;
        public int Cri;
        public int Luck;
        public int Cmb;
        public int Move;
        public int Jmp;

        public VersusStatusData()
        {
            base..ctor();
            return;
        }

        public void Add(StatusParam status, int comb)
        {
            this.Hp += status.hp;
            this.Atk += status.atk;
            this.Def += status.def;
            this.Matk += status.mag;
            this.Mdef += status.mnd;
            this.Dex += status.dex;
            this.Spd += status.spd;
            this.Cri += status.cri;
            this.Luck += status.luk;
            this.Move += status.mov;
            this.Jmp += status.jmp;
            this.Cmb += comb;
            return;
        }
    }
}

