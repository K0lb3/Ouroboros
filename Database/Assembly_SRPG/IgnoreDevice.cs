namespace SRPG
{
    using System;
    using System.Collections.Generic;

    public class IgnoreDevice
    {
        public string maker;
        public List<string> type_name_list;
        public string os_version;

        public IgnoreDevice()
        {
            this.type_name_list = new List<string>();
            base..ctor();
            return;
        }

        public unsafe bool checkIgnoreDevice(string str_maker, string str_device, string str_osversion)
        {
            string str;
            List<string>.Enumerator enumerator;
            bool flag;
            if ((this.os_version != str_osversion.ToLower()) == null)
            {
                goto Label_0018;
            }
            return 0;
        Label_0018:
            if ((this.maker != str_maker.ToLower()) == null)
            {
                goto Label_0030;
            }
            return 0;
        Label_0030:
            enumerator = this.type_name_list.GetEnumerator();
        Label_003C:
            try
            {
                goto Label_0061;
            Label_0041:
                str = &enumerator.Current;
                if ((str == str_device.ToLower()) == null)
                {
                    goto Label_0061;
                }
                flag = 1;
                goto Label_0080;
            Label_0061:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0041;
                }
                goto Label_007E;
            }
            finally
            {
            Label_0072:
                ((List<string>.Enumerator) enumerator).Dispose();
            }
        Label_007E:
            return 0;
        Label_0080:
            return flag;
        }

        public void SetDevices(string str_maker, string[] device_list, string str_osversion)
        {
            string str;
            string[] strArray;
            int num;
            this.maker = str_maker.ToLower();
            strArray = device_list;
            num = 0;
            goto Label_002E;
        Label_0015:
            str = strArray[num];
            this.type_name_list.Add(str.ToLower());
            num += 1;
        Label_002E:
            if (num < ((int) strArray.Length))
            {
                goto Label_0015;
            }
            this.os_version = str_osversion.ToLower();
            return;
        }
    }
}

