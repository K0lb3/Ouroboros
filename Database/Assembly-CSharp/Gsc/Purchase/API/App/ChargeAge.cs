// Decompiled with JetBrains decompiler
// Type: Gsc.Purchase.API.App.ChargeAge
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using Gsc.Network.Support.MiniJsonHelper;
using System;
using System.Collections.Generic;

namespace Gsc.Purchase.API.App
{
  public class ChargeAge : GenericRequest<ChargeAge, Gsc.Purchase.API.Response.ChargeAge>
  {
    private const string ___path = "/age";

    public ChargeAge(int birthMonth, int birthYear)
    {
      this.BirthMonth = birthMonth;
      this.BirthYear = birthYear;
    }

    public int BirthDay { get; set; }

    public int BirthYear { get; set; }

    public int BirthMonth { get; set; }

    public override string GetPath()
    {
      return SDK.Configuration.Env.PurchaseApiPrefix + "/age";
    }

    public override string GetMethod()
    {
      return "POST";
    }

    protected override Dictionary<string, object> GetParameters()
    {
      Dictionary<string, object> dictionary = new Dictionary<string, object>();
      dictionary["birth_day"] = Serializer.Instance.Add<int>(new Func<int, object>(Serializer.From<int>)).Serialize<int>(this.BirthDay);
      dictionary["birth_year"] = Serializer.Instance.Add<int>(new Func<int, object>(Serializer.From<int>)).Serialize<int>(this.BirthYear);
      dictionary["birth_month"] = Serializer.Instance.Add<int>(new Func<int, object>(Serializer.From<int>)).Serialize<int>(this.BirthMonth);
      return dictionary;
    }
  }
}
