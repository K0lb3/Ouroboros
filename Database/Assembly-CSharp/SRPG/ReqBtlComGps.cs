// Decompiled with JetBrains decompiler
// Type: SRPG.ReqBtlComGps
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System.Text;
using UnityEngine;

namespace SRPG
{
  public class ReqBtlComGps : WebAPI
  {
    public ReqBtlComGps(Network.ResponseCallback response, Vector2 location)
    {
      StringBuilder stringBuilder = WebAPI.GetStringBuilder();
      this.name = "btl/com/areaquest";
      stringBuilder.Append("\"location\":{");
      stringBuilder.Append("\"lat\":" + (object) (float) location.x + ",");
      stringBuilder.Append("\"lng\":" + (object) (float) location.y);
      stringBuilder.Append("}");
      this.body = WebAPI.GetRequestString(stringBuilder.ToString());
      this.callback = response;
    }
  }
}
