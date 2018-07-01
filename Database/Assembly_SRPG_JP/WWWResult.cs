// Decompiled with JetBrains decompiler
// Type: SRPG.WWWResult
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

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
