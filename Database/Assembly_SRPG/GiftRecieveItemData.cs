// Decompiled with JetBrains decompiler
// Type: SRPG.GiftRecieveItemData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

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
