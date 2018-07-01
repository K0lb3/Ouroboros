// Decompiled with JetBrains decompiler
// Type: SRPG.ReqBtlComGps
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System.Text;
using UnityEngine;

namespace SRPG
{
  public class ReqBtlComGps : WebAPI
  {
    public ReqBtlComGps(Network.ResponseCallback response, Vector2 location, bool isMulti = false)
    {
      StringBuilder stringBuilder = WebAPI.GetStringBuilder();
      this.name = "btl/com/areaquest";
      stringBuilder.Append("\"location\":{");
      stringBuilder.Append("\"lat\":" + (object) (float) location.x + ",");
      stringBuilder.Append("\"lng\":" + (object) (float) location.y);
      stringBuilder.Append("}");
      stringBuilder.Append(",");
      stringBuilder.Append("\"is_multi\":");
      stringBuilder.Append(!isMulti ? 0 : 1);
      this.body = WebAPI.GetRequestString(stringBuilder.ToString());
      this.callback = response;
    }
  }
}
