namespace SRPG
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public static class SortUtility
    {
        public static unsafe void StableSort<T>(List<T> list, Comparison<T> comparison)
        {
            List<KeyValuePair<int, T>> list2;
            int num;
            int num2;
            <StableSort>c__AnonStorey1CA<T> storeyca;
            KeyValuePair<int, T> pair;
            storeyca = new <StableSort>c__AnonStorey1CA<T>();
            storeyca.comparison = comparison;
            list2 = new List<KeyValuePair<int, T>>(list.Count);
            num = 0;
            goto Label_0037;
        Label_0020:
            list2.Add(new KeyValuePair<int, T>(num, list[num]));
            num += 1;
        Label_0037:
            if (num < list.Count)
            {
                goto Label_0020;
            }
            list2.Sort(new Comparison<KeyValuePair<int, T>>(storeyca.<>m__71));
            num2 = 0;
            goto Label_0077;
        Label_005C:
            pair = list2[num2];
            list[num2] = &pair.Value;
            num2 += 1;
        Label_0077:
            if (num2 < list.Count)
            {
                goto Label_005C;
            }
            return;
        }

        [CompilerGenerated]
        private sealed class <StableSort>c__AnonStorey1CA<T>
        {
            internal Comparison<T> comparison;

            public <StableSort>c__AnonStorey1CA()
            {
                base..ctor();
                return;
            }

            internal unsafe int <>m__71(KeyValuePair<int, T> x, KeyValuePair<int, T> y)
            {
                int num;
                int num2;
                num = this.comparison(&x.Value, &y.Value);
                if (num != null)
                {
                    goto Label_0037;
                }
                num = &&x.Key.CompareTo(&y.Key);
            Label_0037:
                return num;
            }
        }
    }
}

