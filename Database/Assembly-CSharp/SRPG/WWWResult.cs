// Decompiled with JetBrains decompiler
// Type: SRPG.WWWResult
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
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
