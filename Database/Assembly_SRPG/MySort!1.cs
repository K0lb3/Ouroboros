namespace SRPG
{
    using System;
    using System.Collections.Generic;

    public class MySort<T>
    {
        public MySort()
        {
            base..ctor();
            return;
        }

        public static void Sort(List<T> l, Comparison<T> c)
        {
            int num;
            int num2;
            int num3;
            T local;
            num = 0;
            goto Label_005E;
        Label_0007:
            num2 = num + 1;
            goto Label_004E;
        Label_0010:
            if (c(l[num], l[num2]) <= 0)
            {
                goto Label_004A;
            }
            local = l[num];
            l[num] = l[num2];
            l[num2] = local;
        Label_004A:
            num2 += 1;
        Label_004E:
            if (num2 < l.Count)
            {
                goto Label_0010;
            }
            num += 1;
        Label_005E:
            if (num < l.Count)
            {
                goto Label_0007;
            }
            return;
        }
    }
}

