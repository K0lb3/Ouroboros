// Decompiled with JetBrains decompiler
// Type: SRPG.TobiraRecipeMaterialParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class TobiraRecipeMaterialParam
  {
    private string mIname;
    private int mNum;

    public string Iname
    {
      get
      {
        return this.mIname;
      }
    }

    public int Num
    {
      get
      {
        return this.mNum;
      }
    }

    public void Deserialize(JSON_TobiraRecipeMaterialParam json)
    {
      if (json == null)
        return;
      this.mIname = json.iname;
      this.mNum = json.num;
    }
  }
}
