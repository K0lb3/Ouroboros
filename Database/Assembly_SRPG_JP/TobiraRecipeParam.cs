// Decompiled with JetBrains decompiler
// Type: SRPG.TobiraRecipeParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;

namespace SRPG
{
  public class TobiraRecipeParam
  {
    private List<TobiraRecipeMaterialParam> mMaterials = new List<TobiraRecipeMaterialParam>();
    private string mRecipeIname;
    private int mLevel;
    private int mCost;
    private int mUnitPieceNum;
    private int mElementNum;
    private int mUnlockElementNum;
    private int mUnlockBirthNum;

    public string RecipeIname
    {
      get
      {
        return this.mRecipeIname;
      }
    }

    public int Level
    {
      get
      {
        return this.mLevel;
      }
    }

    public int Cost
    {
      get
      {
        return this.mCost;
      }
    }

    public int UnitPieceNum
    {
      get
      {
        return this.mUnitPieceNum;
      }
    }

    public int ElementNum
    {
      get
      {
        return this.mElementNum;
      }
    }

    public int UnlockElementNum
    {
      get
      {
        return this.mUnlockElementNum;
      }
    }

    public int UnlockBirthNum
    {
      get
      {
        return this.mUnlockBirthNum;
      }
    }

    public TobiraRecipeMaterialParam[] Materials
    {
      get
      {
        return this.mMaterials.ToArray();
      }
    }

    public void Deserialize(JSON_TobiraRecipeParam json)
    {
      if (json == null)
        return;
      this.mRecipeIname = json.recipe_iname;
      this.mLevel = json.tobira_lv;
      this.mCost = json.cost;
      this.mUnitPieceNum = json.unit_piece_num;
      this.mElementNum = json.piece_elem_num;
      this.mUnlockElementNum = json.unlock_elem_num;
      this.mUnlockBirthNum = json.unlock_birth_num;
      this.mMaterials.Clear();
      if (json.mats == null)
        return;
      for (int index = 0; index < json.mats.Length; ++index)
      {
        TobiraRecipeMaterialParam recipeMaterialParam = new TobiraRecipeMaterialParam();
        recipeMaterialParam.Deserialize(json.mats[index]);
        this.mMaterials.Add(recipeMaterialParam);
      }
    }
  }
}
