namespace SRPG
{
    using System;

    public class RecipeParam
    {
        public string iname;
        public int cost;
        public RecipeItem[] items;

        public RecipeParam()
        {
            base..ctor();
            return;
        }

        public bool Deserialize(JSON_RecipeParam json)
        {
            int[] numArray1;
            string[] textArray1;
            int num;
            string[] strArray;
            int num2;
            int[] numArray;
            int num3;
            if (json != null)
            {
                goto Label_0008;
            }
            return 0;
        Label_0008:
            this.iname = json.iname;
            this.cost = json.cost;
            num = 0;
            textArray1 = new string[] { json.mat1, json.mat2, json.mat3, json.mat4, json.mat5 };
            strArray = textArray1;
            num2 = 0;
            goto Label_0077;
        Label_005D:
            if (string.IsNullOrEmpty(strArray[num2]) == null)
            {
                goto Label_006F;
            }
            goto Label_0080;
        Label_006F:
            num += 1;
            num2 += 1;
        Label_0077:
            if (num2 < ((int) strArray.Length))
            {
                goto Label_005D;
            }
        Label_0080:
            if (num <= 0)
            {
                goto Label_010F;
            }
            numArray1 = new int[] { json.num1, json.num2, json.num3, json.num4, json.num5 };
            numArray = numArray1;
            this.items = new RecipeItem[num];
            num3 = 0;
            goto Label_0107;
        Label_00CF:
            this.items[num3] = new RecipeItem();
            this.items[num3].iname = strArray[num3];
            this.items[num3].num = numArray[num3];
            num3 += 1;
        Label_0107:
            if (num3 < num)
            {
                goto Label_00CF;
            }
        Label_010F:
            return 1;
        }
    }
}

