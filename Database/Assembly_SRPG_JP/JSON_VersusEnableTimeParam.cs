// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_VersusEnableTimeParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System;

namespace SRPG
{
  [Serializable]
  public class JSON_VersusEnableTimeParam
  {
    public int id;
    public int mode;
    public string begin_at;
    public string end_at;
    public JSON_VersusEnableTimeScheduleParam[] schedule;
    public int draft_type;
  }
}
