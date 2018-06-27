// Decompiled with JetBrains decompiler
// Type: SRPG.GachaReceiptData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class GachaReceiptData
  {
    public string iname;
    public string type;
    public int val;

    public void Init()
    {
      this.iname = (string) null;
      this.type = (string) null;
      this.val = 0;
    }

    public bool Deserialize(Json_GachaReceipt json)
    {
      this.Init();
      if (json == null)
        return false;
      this.iname = json.iname;
      this.type = json.type;
      this.val = json.val;
      return true;
    }
  }
}
