namespace SRPG
{
    using System;

    public class GuerrillaShopScheduleParam
    {
        public int id;
        public string begin_at;
        public string end_at;
        public int accum_ap;
        public string open_time;
        public string cool_time;
        public GuerrillaShopScheduleAdvent[] advent;

        public GuerrillaShopScheduleParam()
        {
            base..ctor();
            return;
        }

        public bool Deserialize(JSON_GuerrillaShopScheduleParam json)
        {
            GuerrillaShopScheduleAdvent[] adventArray;
            int num;
            this.id = json.id;
            this.begin_at = json.begin_at;
            this.end_at = json.end_at;
            this.accum_ap = json.accum_ap;
            this.open_time = json.open_time;
            this.cool_time = json.cool_time;
            if (json.advent == null)
            {
                goto Label_00AC;
            }
            adventArray = new GuerrillaShopScheduleAdvent[(int) json.advent.Length];
            num = 0;
            goto Label_009E;
        Label_0068:
            adventArray[num] = new GuerrillaShopScheduleAdvent();
            adventArray[num].id = json.advent[num].id;
            adventArray[num].coef = json.advent[num].coef;
            num += 1;
        Label_009E:
            if (num < ((int) json.advent.Length))
            {
                goto Label_0068;
            }
        Label_00AC:
            return 1;
        }
    }
}

