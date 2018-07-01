// Decompiled with JetBrains decompiler
// Type: SRPG.GiftRecieveItemData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class GiftRecieveItemData
  {
    public string iname;
    public int rarity;
    public int num;
    public string name;
    public GiftTypes type;

    public void Set(string iname, GiftTypes giftTipe, int rarity, int num)
    {
      this.iname = iname;
      this.type = giftTipe;
      this.rarity = rarity;
      this.num = num;
    }
  }
}
