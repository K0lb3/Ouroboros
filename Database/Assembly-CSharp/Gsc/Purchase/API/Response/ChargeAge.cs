// Decompiled with JetBrains decompiler
// Type: Gsc.Purchase.API.Response.ChargeAge
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using Gsc.DOM;
using Gsc.Network;
using Gsc.Purchase.API.App;

namespace Gsc.Purchase.API.Response
{
  public class ChargeAge : GenericResponse<ChargeAge>
  {
    public ChargeAge(WebInternalResponse response)
    {
      using (IDocument document = this.Parse(response))
        this.Age = document.Root["age"].ToInt();
    }

    public int Age { get; private set; }
  }
}
