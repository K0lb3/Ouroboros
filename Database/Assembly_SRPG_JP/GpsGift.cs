// Decompiled with JetBrains decompiler
// Type: SRPG.GpsGift
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System.Text;
using UnityEngine;

namespace SRPG
{
  public class GpsGift : WebAPI
  {
    public GpsGift(Vector2 location, Network.ResponseCallback response)
    {
      StringBuilder stringBuilder = WebAPI.GetStringBuilder();
      this.name = "mail/area";
      stringBuilder.Append("\"location\":{");
      stringBuilder.Append("\"lat\":" + (object) (float) location.x + ",");
      stringBuilder.Append("\"lng\":" + (object) (float) location.y);
      stringBuilder.Append("}");
      this.body = WebAPI.GetRequestString(stringBuilder.ToString());
      this.callback = response;
    }
  }
}
