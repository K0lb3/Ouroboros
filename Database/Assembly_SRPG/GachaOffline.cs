namespace SRPG
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    public class GachaOffline
    {
        public GachaOffline()
        {
            base..ctor();
            return;
        }

        public unsafe string ExecGacha(string fileName)
        {
            string str;
            List<KeyValuePair<int, string>> list;
            int num;
            KeyValuePair<int, string> pair;
            List<KeyValuePair<int, string>>.Enumerator enumerator;
            int num2;
            KeyValuePair<int, string> pair2;
            List<KeyValuePair<int, string>>.Enumerator enumerator2;
            string str2;
            str = "LocalMaps/drop/";
            list = this.MakeDropTable(str + fileName);
            num = 0;
            enumerator = list.GetEnumerator();
        Label_001E:
            try
            {
                goto Label_0035;
            Label_0023:
                pair = &enumerator.Current;
                num += &pair.Key;
            Label_0035:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0023;
                }
                goto Label_0053;
            }
            finally
            {
            Label_0046:
                ((List<KeyValuePair<int, string>>.Enumerator) enumerator).Dispose();
            }
        Label_0053:
            num2 = Random.Range(0, num);
            enumerator2 = list.GetEnumerator();
        Label_0064:
            try
            {
                goto Label_0094;
            Label_0069:
                pair2 = &enumerator2.Current;
                num2 -= &pair2.Key;
                if (num2 >= 0)
                {
                    goto Label_0094;
                }
                str2 = &pair2.Value;
                goto Label_00B8;
            Label_0094:
                if (&enumerator2.MoveNext() != null)
                {
                    goto Label_0069;
                }
                goto Label_00B2;
            }
            finally
            {
            Label_00A5:
                ((List<KeyValuePair<int, string>>.Enumerator) enumerator2).Dispose();
            }
        Label_00B2:
            return "none";
        Label_00B8:
            return str2;
        }

        private unsafe List<KeyValuePair<int, string>> MakeDropTable(string tablePath)
        {
            object[] objArray1;
            char[] chArray1;
            List<KeyValuePair<int, string>> list;
            TextAsset asset;
            string str;
            string[] strArray;
            string str2;
            string str3;
            string[] strArray2;
            int num;
            int num2;
            string str4;
            KeyValuePair<int, string> pair;
            List<KeyValuePair<int, string>>.Enumerator enumerator;
            string str5;
            list = new List<KeyValuePair<int, string>>();
            asset = Resources.Load<TextAsset>(tablePath);
            if ((asset == null) == null)
            {
                goto Label_0034;
            }
            DebugUtility.LogWarning("ドロップテーブル '" + tablePath + "' の読み込みに失敗しました。");
            return new List<KeyValuePair<int, string>>();
        Label_0034:
            chArray1 = new char[] { 10 };
            strArray = asset.get_text().Replace("\r\n", "\n").Split(chArray1);
            str2 = null;
            strArray2 = strArray;
            num = 0;
            goto Label_00CB;
        Label_006A:
            str3 = strArray2[num];
            num2 = 0;
        Label_0074:
            try
            {
                num2 = int.Parse(str3);
                goto Label_00B6;
            }
            catch (Exception)
            {
            Label_0082:
                str2 = str3;
                if (str3.IndexOf("\r") == -1)
                {
                    goto Label_00AC;
                }
                str2 = str2.Replace("\r", string.Empty);
            Label_00AC:
                goto Label_00C5;
            }
        Label_00B6:
            list.Add(new KeyValuePair<int, string>(num2, str2));
        Label_00C5:
            num += 1;
        Label_00CB:
            if (num < ((int) strArray2.Length))
            {
                goto Label_006A;
            }
            str4 = string.Empty;
            enumerator = list.GetEnumerator();
        Label_00E5:
            try
            {
                goto Label_0132;
            Label_00EA:
                pair = &enumerator.Current;
                str5 = str4;
                objArray1 = new object[] { str5, (int) &pair.Key, ": ", &pair.Value, "\n" };
                str4 = string.Concat(objArray1);
            Label_0132:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_00EA;
                }
                goto Label_0150;
            }
            finally
            {
            Label_0143:
                ((List<KeyValuePair<int, string>>.Enumerator) enumerator).Dispose();
            }
        Label_0150:
            DebugUtility.Log("# drop table #");
            DebugUtility.Log(str4);
            DebugUtility.Log("##############");
            return list;
        }
    }
}

