namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    public class WeatherSetParam
    {
        private string mIname;
        private string mName;
        private List<string> mStartWeatherIdLists;
        private List<int> mStartWeatherRateLists;
        private int mChangeClockMin;
        private int mChangeClockMax;
        private List<string> mChangeWeatherIdLists;
        private List<int> mChangeWeatherRateLists;

        public WeatherSetParam()
        {
            this.mStartWeatherIdLists = new List<string>();
            this.mStartWeatherRateLists = new List<int>();
            this.mChangeWeatherIdLists = new List<string>();
            this.mChangeWeatherRateLists = new List<int>();
            base..ctor();
            return;
        }

        public void Deserialize(JSON_WeatherSetParam json)
        {
            string str;
            string[] strArray;
            int num;
            int num2;
            int[] numArray;
            int num3;
            int num4;
            string str2;
            string[] strArray2;
            int num5;
            int num6;
            int[] numArray2;
            int num7;
            int num8;
            if (json != null)
            {
                goto Label_0007;
            }
            return;
        Label_0007:
            this.mIname = json.iname;
            this.mName = json.name;
            this.mStartWeatherIdLists.Clear();
            if (json.st_wth == null)
            {
                goto Label_0060;
            }
            strArray = json.st_wth;
            num = 0;
            goto Label_0057;
        Label_0043:
            str = strArray[num];
            this.mStartWeatherIdLists.Add(str);
            num += 1;
        Label_0057:
            if (num < ((int) strArray.Length))
            {
                goto Label_0043;
            }
        Label_0060:
            this.mStartWeatherRateLists.Clear();
            if (json.st_rate == null)
            {
                goto Label_00A9;
            }
            numArray = json.st_rate;
            num3 = 0;
            goto Label_009E;
        Label_0086:
            num2 = numArray[num3];
            this.mStartWeatherRateLists.Add(num2);
            num3 += 1;
        Label_009E:
            if (num3 < ((int) numArray.Length))
            {
                goto Label_0086;
            }
        Label_00A9:
            if (this.mStartWeatherIdLists.Count <= this.mStartWeatherRateLists.Count)
            {
                goto Label_00FC;
            }
            num4 = 0;
            goto Label_00DE;
        Label_00CC:
            this.mStartWeatherRateLists.Add(0);
            num4 += 1;
        Label_00DE:
            if (num4 < (this.mStartWeatherIdLists.Count - this.mStartWeatherRateLists.Count))
            {
                goto Label_00CC;
            }
        Label_00FC:
            this.mChangeClockMin = json.ch_cl_min;
            this.mChangeClockMax = json.ch_cl_max;
            if (this.mChangeClockMin <= this.mChangeClockMax)
            {
                goto Label_0131;
            }
            this.mChangeClockMax = this.mChangeClockMin;
        Label_0131:
            this.mChangeWeatherIdLists.Clear();
            if (json.ch_wth == null)
            {
                goto Label_017C;
            }
            strArray2 = json.ch_wth;
            num5 = 0;
            goto Label_0171;
        Label_0157:
            str2 = strArray2[num5];
            this.mChangeWeatherIdLists.Add(str2);
            num5 += 1;
        Label_0171:
            if (num5 < ((int) strArray2.Length))
            {
                goto Label_0157;
            }
        Label_017C:
            this.mChangeWeatherRateLists.Clear();
            if (json.ch_rate == null)
            {
                goto Label_01C7;
            }
            numArray2 = json.ch_rate;
            num7 = 0;
            goto Label_01BC;
        Label_01A2:
            num6 = numArray2[num7];
            this.mChangeWeatherRateLists.Add(num6);
            num7 += 1;
        Label_01BC:
            if (num7 < ((int) numArray2.Length))
            {
                goto Label_01A2;
            }
        Label_01C7:
            if (this.mChangeWeatherIdLists.Count <= this.mChangeWeatherRateLists.Count)
            {
                goto Label_021A;
            }
            num8 = 0;
            goto Label_01FC;
        Label_01EA:
            this.mChangeWeatherRateLists.Add(0);
            num8 += 1;
        Label_01FC:
            if (num8 < (this.mChangeWeatherIdLists.Count - this.mChangeWeatherRateLists.Count))
            {
                goto Label_01EA;
            }
        Label_021A:
            return;
        }

        public string GetChangeWeather(RandXorshift rand)
        {
            int num;
            int num2;
            int num3;
            int num4;
            if (this.mChangeWeatherIdLists.Count != null)
            {
                goto Label_0012;
            }
            return null;
        Label_0012:
            num = 0;
            if (rand == null)
            {
                goto Label_005F;
            }
            num2 = rand.Get() % 100;
            num3 = 0;
            num4 = 0;
            goto Label_004E;
        Label_002D:
            num3 += this.mChangeWeatherRateLists[num4];
            if (num2 >= num3)
            {
                goto Label_004A;
            }
            num = num4;
            goto Label_005F;
        Label_004A:
            num4 += 1;
        Label_004E:
            if (num4 < this.mChangeWeatherRateLists.Count)
            {
                goto Label_002D;
            }
        Label_005F:
            if (num < this.mChangeWeatherIdLists.Count)
            {
                goto Label_0072;
            }
            num = 0;
        Label_0072:
            return this.mChangeWeatherIdLists[num];
        }

        public int GetNextChangeClock(int now_clock, RandXorshift rand)
        {
            int num;
            int num2;
            if (this.mChangeClockMin == null)
            {
                goto Label_0016;
            }
            if (this.mChangeClockMax != null)
            {
                goto Label_0018;
            }
        Label_0016:
            return 0;
        Label_0018:
            num = this.mChangeClockMin;
            num2 = (this.mChangeClockMax - this.mChangeClockMin) + 1;
            if (num2 <= 1)
            {
                goto Label_004A;
            }
            if (rand == null)
            {
                goto Label_004A;
            }
            num += (int) (((ulong) rand.Get()) % ((long) num2));
        Label_004A:
            return (now_clock + num);
        }

        public string GetStartWeather(RandXorshift rand)
        {
            int num;
            int num2;
            int num3;
            int num4;
            if (this.mStartWeatherIdLists.Count != null)
            {
                goto Label_0012;
            }
            return null;
        Label_0012:
            num = 0;
            if (rand == null)
            {
                goto Label_005F;
            }
            num2 = rand.Get() % 100;
            num3 = 0;
            num4 = 0;
            goto Label_004E;
        Label_002D:
            num3 += this.mStartWeatherRateLists[num4];
            if (num2 >= num3)
            {
                goto Label_004A;
            }
            num = num4;
            goto Label_005F;
        Label_004A:
            num4 += 1;
        Label_004E:
            if (num4 < this.mStartWeatherRateLists.Count)
            {
                goto Label_002D;
            }
        Label_005F:
            if (num < this.mStartWeatherIdLists.Count)
            {
                goto Label_0072;
            }
            num = 0;
        Label_0072:
            return this.mStartWeatherIdLists[num];
        }

        public string Iname
        {
            get
            {
                return this.mIname;
            }
        }

        public string Name
        {
            get
            {
                return this.mName;
            }
        }

        public List<string> StartWeatherIdLists
        {
            get
            {
                return this.mStartWeatherIdLists;
            }
        }

        public List<int> StartWeatherRateLists
        {
            get
            {
                return this.mStartWeatherRateLists;
            }
        }

        public int ChangeClockMin
        {
            get
            {
                return this.mChangeClockMin;
            }
        }

        public int ChangeClockMax
        {
            get
            {
                return this.mChangeClockMax;
            }
        }

        public List<string> ChangeWeatherIdLists
        {
            get
            {
                return this.mChangeWeatherIdLists;
            }
        }

        public List<int> ChangeWeatherRateLists
        {
            get
            {
                return this.mChangeWeatherRateLists;
            }
        }
    }
}

