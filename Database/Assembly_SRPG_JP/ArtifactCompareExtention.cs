// Decompiled with JetBrains decompiler
// Type: SRPG.ArtifactCompareExtention
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public static class ArtifactCompareExtention
  {
    public static int CompareByID(this ArtifactParam x, ArtifactParam y)
    {
      return string.Compare(x.iname, y.iname);
    }

    public static int CompareByID(this ArtifactData x, ArtifactData y)
    {
      return x.ArtifactParam.CompareByID(y.ArtifactParam);
    }

    public static int CompareByType(this ArtifactParam x, ArtifactParam y)
    {
      if (x.type > y.type)
        return 1;
      return x.type < y.type ? -1 : 0;
    }

    public static int CompareByType(this ArtifactData x, ArtifactData y)
    {
      return x.ArtifactParam.CompareByType(y.ArtifactParam);
    }

    public static int CompareByTypeAndID(this ArtifactParam x, ArtifactParam y)
    {
      int num = x.CompareByType(y);
      if (num != 0)
        return num;
      return x.CompareByID(y);
    }

    public static int CompareByTypeAndID(this ArtifactData x, ArtifactData y)
    {
      return x.ArtifactParam.CompareByTypeAndID(y.ArtifactParam);
    }
  }
}
