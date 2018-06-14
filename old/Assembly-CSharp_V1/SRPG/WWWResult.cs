// Decompiled with JetBrains decompiler
// Type: SRPG.WWWResult
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  public struct WWWResult
  {
    private WWW mResult;
    private string mResultValue;

    public WWWResult(WWW www)
    {
      this.mResult = www;
      this.mResultValue = (string) null;
    }

    public WWWResult(string result)
    {
      this.mResult = (WWW) null;
      this.mResultValue = result;
    }

    public string text
    {
      get
      {
        if (this.mResult != null)
          return this.mResult.get_text();
        return this.mResultValue;
      }
    }
  }
}
