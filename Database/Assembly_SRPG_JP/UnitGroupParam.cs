// Decompiled with JetBrains decompiler
// Type: SRPG.UnitGroupParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Text;

namespace SRPG
{
  public class UnitGroupParam
  {
    public string iname;
    public string name;
    public string[] units;

    public bool Deserialize(JSON_UnitGroupParam json)
    {
      this.iname = json.iname;
      this.name = json.name;
      this.units = json.units;
      return true;
    }

    public bool IsInGroup(string unit_iname)
    {
      return Array.FindIndex<string>(this.units, (Predicate<string>) (u => u == unit_iname)) >= 0;
    }

    public string GetName()
    {
      if (string.IsNullOrEmpty(this.name))
        return this.GetGroupUnitAllNameText();
      return this.name;
    }

    public string GetGroupUnitAllNameText()
    {
      StringBuilder stringBuilder = new StringBuilder();
      if (this.units == null)
        return string.Empty;
      for (int index = 0; index < this.units.Length; ++index)
      {
        UnitParam unitParam = MonoSingleton<GameManager>.Instance.MasterParam.GetUnitParam(this.units[index]);
        if (unitParam != null)
        {
          stringBuilder.Append(unitParam.name);
          if (index < this.units.Length - 1)
            stringBuilder.Append("CONCEPT_CARD_SKILL_DESCRIPTION_COMMA");
        }
      }
      return stringBuilder.ToString();
    }
  }
}
