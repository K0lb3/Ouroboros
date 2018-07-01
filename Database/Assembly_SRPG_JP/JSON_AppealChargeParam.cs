// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_AppealChargeParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System;

namespace SRPG
{
  [Serializable]
  public class JSON_AppealChargeParam
  {
    public JSON_AppealChargeParam.AppealParam fields;

    public class AppealParam
    {
      public string appeal_id = string.Empty;
      public string before_img_id = string.Empty;
      public string after_img_id = string.Empty;
      public string start_at = string.Empty;
      public string end_at = string.Empty;
    }
  }
}
