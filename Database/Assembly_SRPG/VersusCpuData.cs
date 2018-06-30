namespace SRPG
{
    using System;

    public class VersusCpuData
    {
        public string Name;
        public int Lv;
        public UnitData[] Units;
        public int[] Place;
        public int Score;
        public int Deck;

        public VersusCpuData()
        {
            base..ctor();
            return;
        }

        public bool Deserialize(Json_VersusCpuData json, int idx)
        {
            int num;
            int num2;
            int num3;
            int num4;
            if (json != null)
            {
                goto Label_0008;
            }
            return 0;
        Label_0008:
            this.Name = json.name;
            this.Lv = json.lv;
            this.Deck = idx;
            if (json.units == null)
            {
                goto Label_007B;
            }
            num = (int) json.units.Length;
            this.Units = new UnitData[num];
            num2 = 0;
            goto Label_0074;
        Label_004E:
            this.Units[num2] = new UnitData();
            this.Units[num2].Deserialize(json.units[num2]);
            num2 += 1;
        Label_0074:
            if (num2 < num)
            {
                goto Label_004E;
            }
        Label_007B:
            if (json.place == null)
            {
                goto Label_00BD;
            }
            num3 = (int) json.place.Length;
            this.Place = new int[num3];
            num4 = 0;
            goto Label_00B6;
        Label_00A2:
            this.Place[num4] = json.place[num4];
            num4 += 1;
        Label_00B6:
            if (num4 < num3)
            {
                goto Label_00A2;
            }
        Label_00BD:
            this.Score = json.score;
            return 1;
        }
    }
}

