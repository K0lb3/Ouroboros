// Decompiled with JetBrains decompiler
// Type: SRPG.QuestCondParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System;

namespace SRPG
{
  public class QuestCondParam
  {
    public string iname;
    public int plvmax;
    public int plvmin;
    public int ulvmax;
    public int ulvmin;
    public int[] elem;
    public bool isElemLimit;
    public string[] job;
    public PartyCondType party_type;
    public string[] unit;
    public ESex sex;
    public int rmax;
    public int rmin;
    public int hmax;
    public int hmin;
    public int wmax;
    public int wmin;
    public int[] jobset;
    public string[] birth;

    public bool Deserialize(JSON_QuestCondParam json)
    {
      this.iname = json.iname;
      this.plvmax = json.plvmax;
      this.plvmin = json.plvmin;
      this.ulvmax = json.ulvmax;
      this.ulvmin = json.ulvmin;
      this.sex = (ESex) json.sex;
      this.rmax = json.rmax;
      this.rmin = json.rmin;
      this.hmax = json.hmax;
      this.hmin = json.hmin;
      this.wmax = json.wmax;
      this.wmin = json.wmin;
      int num1 = 0;
      this.elem = new int[Enum.GetValues(typeof (EElement)).Length];
      this.isElemLimit = num1 + (this.elem[0] = json.el_none) + (this.elem[1] = json.el_fire) + (this.elem[2] = json.el_watr) + (this.elem[3] = json.el_wind) + (this.elem[4] = json.el_thdr) + (this.elem[5] = json.el_lit) + (this.elem[6] = json.el_drk) > 0;
      int num2 = 0;
      this.jobset = new int[4];
      int[] jobset1 = this.jobset;
      int index1 = num2;
      int num3 = index1 + 1;
      int jobset1_1 = json.jobset1;
      jobset1[index1] = jobset1_1;
      int[] jobset2 = this.jobset;
      int index2 = num3;
      int num4 = index2 + 1;
      int jobset2_1 = json.jobset2;
      jobset2[index2] = jobset2_1;
      int[] jobset3 = this.jobset;
      int index3 = num4;
      int num5 = index3 + 1;
      int jobset3_1 = json.jobset3;
      jobset3[index3] = jobset3_1;
      if (json.job != null)
      {
        this.job = new string[json.job.Length];
        for (int index4 = 0; index4 < this.job.Length; ++index4)
          this.job[index4] = json.job[index4];
      }
      if (json.unit != null)
      {
        this.unit = new string[json.unit.Length];
        for (int index4 = 0; index4 < this.unit.Length; ++index4)
          this.unit[index4] = json.unit[index4];
      }
      if (json.birth != null)
      {
        this.birth = new string[json.birth.Length];
        for (int index4 = 0; index4 < this.birth.Length; ++index4)
          this.birth[index4] = json.birth[index4];
      }
      this.party_type = !Enum.IsDefined(typeof (PartyCondType), (object) json.party_type) ? PartyCondType.None : (PartyCondType) json.party_type;
      return true;
    }
  }
}
