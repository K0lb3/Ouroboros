// Decompiled with JetBrains decompiler
// Type: SRPG.NewBadgeParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class NewBadgeParam
  {
    private bool mIsUseNewFlag;
    private bool mIsNew;
    private NewBadgeType mType;

    public NewBadgeParam(bool use, bool isnew, NewBadgeType type)
    {
      this.mIsUseNewFlag = use;
      this.mIsNew = isnew;
      this.mType = type;
    }

    public bool use_newflag
    {
      get
      {
        return this.mIsUseNewFlag;
      }
    }

    public bool is_new
    {
      get
      {
        return this.mIsNew;
      }
    }

    public NewBadgeType type
    {
      get
      {
        return this.mType;
      }
    }
  }
}
