// Decompiled with JetBrains decompiler
// Type: SRPG.TobiraLearnAbilityParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class TobiraLearnAbilityParam
  {
    private string mAbilityIname;
    private int mLevel;
    private TobiraLearnAbilityParam.AddType mAddType;
    private string mAbilityOverwrite;

    public string AbilityIname
    {
      get
      {
        return this.mAbilityIname;
      }
    }

    public int Level
    {
      get
      {
        return this.mLevel;
      }
    }

    public TobiraLearnAbilityParam.AddType AbilityAddType
    {
      get
      {
        return this.mAddType;
      }
    }

    public string AbilityOverwrite
    {
      get
      {
        return this.mAbilityOverwrite;
      }
    }

    public void Deserialize(JSON_TobiraLearnAbilityParam json)
    {
      if (json == null)
        return;
      this.mAbilityIname = json.abil_iname;
      this.mLevel = json.learn_lv;
      this.mAddType = (TobiraLearnAbilityParam.AddType) json.add_type;
      this.mAbilityOverwrite = json.abil_overwrite;
    }

    public enum AddType
    {
      Unknow,
      JobOverwrite,
      MasterAdd,
      MasterOverwrite,
    }
  }
}
