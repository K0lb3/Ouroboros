namespace SRPG
{
    using System;

    [Serializable]
    public class MapBreakObj
    {
        public int clash_type;
        public int ai_type;
        public int side_type;
        public int ray_type;
        public int is_ui;
        public int max_hp;
        public int[] rest_hps;

        public MapBreakObj()
        {
            this.is_ui = 1;
            base..ctor();
            return;
        }

        public void CopyTo(MapBreakObj dst)
        {
            int num;
            dst.clash_type = this.clash_type;
            dst.ai_type = this.ai_type;
            dst.side_type = this.side_type;
            dst.ray_type = this.ray_type;
            dst.is_ui = this.is_ui;
            dst.max_hp = this.max_hp;
            if (this.rest_hps == null)
            {
                goto Label_009C;
            }
            if (((int) this.rest_hps.Length) == null)
            {
                goto Label_009C;
            }
            dst.rest_hps = new int[(int) this.rest_hps.Length];
            num = 0;
            goto Label_008E;
        Label_007A:
            dst.rest_hps[num] = this.rest_hps[num];
            num += 1;
        Label_008E:
            if (num < ((int) this.rest_hps.Length))
            {
                goto Label_007A;
            }
        Label_009C:
            return;
        }
    }
}

