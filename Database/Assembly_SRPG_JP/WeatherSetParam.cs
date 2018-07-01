// Decompiled with JetBrains decompiler
// Type: SRPG.WeatherSetParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System.Collections.Generic;

namespace SRPG
{
  public class WeatherSetParam
  {
    private List<string> mStartWeatherIdLists = new List<string>();
    private List<int> mStartWeatherRateLists = new List<int>();
    private List<string> mChangeWeatherIdLists = new List<string>();
    private List<int> mChangeWeatherRateLists = new List<int>();
    private string mIname;
    private string mName;
    private int mChangeClockMin;
    private int mChangeClockMax;

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

    public void Deserialize(JSON_WeatherSetParam json)
    {
      if (json == null)
        return;
      this.mIname = json.iname;
      this.mName = json.name;
      this.mStartWeatherIdLists.Clear();
      if (json.st_wth != null)
      {
        foreach (string str in json.st_wth)
          this.mStartWeatherIdLists.Add(str);
      }
      this.mStartWeatherRateLists.Clear();
      if (json.st_rate != null)
      {
        foreach (int num in json.st_rate)
          this.mStartWeatherRateLists.Add(num);
      }
      if (this.mStartWeatherIdLists.Count > this.mStartWeatherRateLists.Count)
      {
        for (int index = 0; index < this.mStartWeatherIdLists.Count - this.mStartWeatherRateLists.Count; ++index)
          this.mStartWeatherRateLists.Add(0);
      }
      this.mChangeClockMin = json.ch_cl_min;
      this.mChangeClockMax = json.ch_cl_max;
      if (this.mChangeClockMin > this.mChangeClockMax)
        this.mChangeClockMax = this.mChangeClockMin;
      this.mChangeWeatherIdLists.Clear();
      if (json.ch_wth != null)
      {
        foreach (string str in json.ch_wth)
          this.mChangeWeatherIdLists.Add(str);
      }
      this.mChangeWeatherRateLists.Clear();
      if (json.ch_rate != null)
      {
        foreach (int num in json.ch_rate)
          this.mChangeWeatherRateLists.Add(num);
      }
      if (this.mChangeWeatherIdLists.Count <= this.mChangeWeatherRateLists.Count)
        return;
      for (int index = 0; index < this.mChangeWeatherIdLists.Count - this.mChangeWeatherRateLists.Count; ++index)
        this.mChangeWeatherRateLists.Add(0);
    }

    public string GetStartWeather(RandXorshift rand = null)
    {
      if (this.mStartWeatherIdLists.Count == 0)
        return (string) null;
      int index1 = 0;
      if (rand != null)
      {
        int num1 = (int) (rand.Get() % 100U);
        int num2 = 0;
        for (int index2 = 0; index2 < this.mStartWeatherRateLists.Count; ++index2)
        {
          num2 += this.mStartWeatherRateLists[index2];
          if (num1 < num2)
          {
            index1 = index2;
            break;
          }
        }
      }
      if (index1 >= this.mStartWeatherIdLists.Count)
        index1 = 0;
      return this.mStartWeatherIdLists[index1];
    }

    public string GetChangeWeather(RandXorshift rand = null)
    {
      if (this.mChangeWeatherIdLists.Count == 0)
        return (string) null;
      int index1 = 0;
      if (rand != null)
      {
        int num1 = (int) (rand.Get() % 100U);
        int num2 = 0;
        for (int index2 = 0; index2 < this.mChangeWeatherRateLists.Count; ++index2)
        {
          num2 += this.mChangeWeatherRateLists[index2];
          if (num1 < num2)
          {
            index1 = index2;
            break;
          }
        }
      }
      if (index1 >= this.mChangeWeatherIdLists.Count)
        index1 = 0;
      return this.mChangeWeatherIdLists[index1];
    }

    public int GetNextChangeClock(int now_clock, RandXorshift rand = null)
    {
      if (this.mChangeClockMin == 0 || this.mChangeClockMax == 0)
        return 0;
      int mChangeClockMin = this.mChangeClockMin;
      int num = this.mChangeClockMax - this.mChangeClockMin + 1;
      if (num > 1 && rand != null)
        mChangeClockMin += (int) ((long) rand.Get() % (long) num);
      return now_clock + mChangeClockMin;
    }
  }
}
