// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_ConceptCardParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System;

namespace SRPG
{
  [Serializable]
  public class JSON_ConceptCardParam
  {
    public string iname;
    public string name;
    public string expr;
    public int type;
    public string icon;
    public int rare;
    public int lvcap;
    public int sell;
    public int en_cost;
    public int en_exp;
    public int en_trust;
    public string trust_reward;
    public string first_get_unit;
    public JSON_ConceptCardEquipParam[] effects;
    public int not_sale;
  }
}
