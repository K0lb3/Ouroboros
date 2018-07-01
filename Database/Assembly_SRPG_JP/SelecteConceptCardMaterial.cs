// Decompiled with JetBrains decompiler
// Type: SRPG.SelecteConceptCardMaterial
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class SelecteConceptCardMaterial
  {
    public OLong mUniqueID;
    public ConceptCardData mSelectedData;
    public int mSelectNum;

    public string iname
    {
      get
      {
        if (this.mSelectedData == null)
          return (string) null;
        return this.mSelectedData.Param.iname;
      }
    }
  }
}
