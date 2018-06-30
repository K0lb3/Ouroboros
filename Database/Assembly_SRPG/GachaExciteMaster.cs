namespace SRPG
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class GachaExciteMaster
    {
        [CompilerGenerated]
        private static Dictionary<string, int> <>f__switch$map9;

        public GachaExciteMaster()
        {
            base..ctor();
            return;
        }

        private static unsafe int ColorString2Int(string cstr)
        {
            int num;
            string str;
            Dictionary<string, int> dictionary;
            int num2;
            num = 1;
            str = cstr;
            if (str == null)
            {
                goto Label_0083;
            }
            if (<>f__switch$map9 != null)
            {
                goto Label_0045;
            }
            dictionary = new Dictionary<string, int>(3);
            dictionary.Add("blue", 0);
            dictionary.Add("yellow", 1);
            dictionary.Add("red", 2);
            <>f__switch$map9 = dictionary;
        Label_0045:
            if (<>f__switch$map9.TryGetValue(str, &num2) == null)
            {
                goto Label_0083;
            }
            switch (num2)
            {
                case 0:
                    goto Label_006E;

                case 1:
                    goto Label_0075;

                case 2:
                    goto Label_007C;
            }
            goto Label_0083;
        Label_006E:
            num = 1;
            goto Label_0094;
        Label_0075:
            num = 2;
            goto Label_0094;
        Label_007C:
            num = 3;
            goto Label_0094;
        Label_0083:
            DebugUtility.LogError("Invalid color string.");
            num = 0;
        Label_0094:
            return num;
        }

        public static int[] Select(Json_GachaExcite[] json, int rare)
        {
            int[] numArray1;
            int num;
            Json_GachaExcite excite;
            Json_GachaExcite[] exciteArray;
            int num2;
            int num3;
            int num4;
            Json_GachaExcite excite2;
            Json_GachaExcite[] exciteArray2;
            int num5;
            num = 0;
            exciteArray = json;
            num2 = 0;
            goto Label_0037;
        Label_000B:
            excite = exciteArray[num2];
            if (rare == excite.fields.rarity)
            {
                goto Label_0025;
            }
            goto Label_0033;
        Label_0025:
            num += excite.fields.weight;
        Label_0033:
            num2 += 1;
        Label_0037:
            if (num2 < ((int) exciteArray.Length))
            {
                goto Label_000B;
            }
            num3 = new Random().Next(num);
            num4 = 0;
            exciteArray2 = json;
            num5 = 0;
            goto Label_0104;
        Label_005B:
            excite2 = exciteArray2[num5];
            if (rare == excite2.fields.rarity)
            {
                goto Label_0079;
            }
            goto Label_00FE;
        Label_0079:
            num4 += excite2.fields.weight;
            if (num3 >= num4)
            {
                goto Label_00FE;
            }
            numArray1 = new int[] { ColorString2Int(excite2.fields.color1), ColorString2Int(excite2.fields.color2), ColorString2Int(excite2.fields.color3), ColorString2Int(excite2.fields.color4), ColorString2Int(excite2.fields.color5) };
            return numArray1;
        Label_00FE:
            num5 += 1;
        Label_0104:
            if (num5 < ((int) exciteArray2.Length))
            {
                goto Label_005B;
            }
            return new int[] { 1, 1, 1, 1, 1 };
        }

        public static int[] SelectStone(Json_GachaExcite[] json, int rare)
        {
            int[] numArray2;
            int[] numArray1;
            int num;
            Json_GachaExcite excite;
            Json_GachaExcite[] exciteArray;
            int num2;
            int num3;
            int num4;
            Json_GachaExcite excite2;
            Json_GachaExcite[] exciteArray2;
            int num5;
            num = 0;
            exciteArray = json;
            num2 = 0;
            goto Label_0037;
        Label_000B:
            excite = exciteArray[num2];
            if (rare == excite.fields.rarity)
            {
                goto Label_0025;
            }
            goto Label_0033;
        Label_0025:
            num += excite.fields.weight;
        Label_0033:
            num2 += 1;
        Label_0037:
            if (num2 < ((int) exciteArray.Length))
            {
                goto Label_000B;
            }
            num3 = new Random().Next(num);
            num4 = 0;
            exciteArray2 = json;
            num5 = 0;
            goto Label_00DC;
        Label_005B:
            excite2 = exciteArray2[num5];
            if (rare == excite2.fields.rarity)
            {
                goto Label_0079;
            }
            goto Label_00D6;
        Label_0079:
            num4 += excite2.fields.weight;
            if (num3 >= num4)
            {
                goto Label_00D6;
            }
            numArray1 = new int[] { ColorString2Int(excite2.fields.color1), ColorString2Int(excite2.fields.color2), ColorString2Int(excite2.fields.color3) };
            return numArray1;
        Label_00D6:
            num5 += 1;
        Label_00DC:
            if (num5 < ((int) exciteArray2.Length))
            {
                goto Label_005B;
            }
            numArray2 = new int[] { 1, 1, 1 };
            return numArray2;
        }
    }
}

