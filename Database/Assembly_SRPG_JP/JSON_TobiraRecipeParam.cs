// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_TobiraRecipeParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System;

namespace SRPG
{
  [Serializable]
  public class JSON_TobiraRecipeParam
  {
    public string recipe_iname;
    public int tobira_lv;
    public int cost;
    public int unit_piece_num;
    public int piece_elem_num;
    public int unlock_elem_num;
    public int unlock_birth_num;
    public JSON_TobiraRecipeMaterialParam[] mats;
  }
}
