// Decompiled with JetBrains decompiler
// Type: SRPG.ShareStringList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;

namespace SRPG
{
  public class ShareStringList
  {
    private short[] mIndexs;
    private ShareString.Type mType;

    public ShareStringList(ShareString.Type type)
    {
      this.mType = type;
    }

    public int Length
    {
      get
      {
        if (this.mIndexs == null || this.mIndexs.Length < 0)
          return 0;
        return this.mIndexs.Length;
      }
    }

    public void Setup(int length)
    {
      this.mIndexs = new short[length];
      for (int index = 0; index < length; ++index)
        this.mIndexs[index] = (short) -1;
    }

    public void Clear()
    {
      this.mIndexs = (short[]) null;
      this.mType = ShareString.Type.QuestParam_cond;
    }

    public bool IsNotNull()
    {
      return this.mIndexs != null;
    }

    public string[] GetList()
    {
      if (this.mIndexs == null || this.mIndexs.Length < 0)
        return (string[]) null;
      string[] strArray = new string[this.mIndexs.Length];
      for (int index = 0; index < this.mIndexs.Length; ++index)
        strArray[index] = Singleton<ShareVariable>.Instance.str.Get(this.mType, this.mIndexs[index]);
      return strArray;
    }

    public string Get(int index)
    {
      if (this.mIndexs == null || index >= this.mIndexs.Length)
        return (string) null;
      return Singleton<ShareVariable>.Instance.str.Get(this.mType, this.mIndexs[index]);
    }

    public void Set(int index, string value)
    {
      if (this.mIndexs == null || index >= this.mIndexs.Length)
        return;
      this.mIndexs[index] = Singleton<ShareVariable>.Instance.str.Set(this.mType, value);
    }
  }
}
