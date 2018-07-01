// Decompiled with JetBrains decompiler
// Type: SRPG.AppealChargeParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System;

namespace SRPG
{
  public class AppealChargeParam
  {
    public string m_AppealId = string.Empty;
    public string m_BeforeImg = string.Empty;
    public string m_AfterImg = string.Empty;
    public long m_StartAt;
    public long m_EndAt;

    public AppealChargeParam()
    {
      this.m_AppealId = string.Empty;
      this.m_BeforeImg = string.Empty;
      this.m_AfterImg = string.Empty;
      this.m_StartAt = 0L;
      this.m_EndAt = 0L;
    }

    public string appeal_id
    {
      get
      {
        return this.m_AppealId;
      }
    }

    public string before_img
    {
      get
      {
        return this.m_BeforeImg;
      }
    }

    public string after_img
    {
      get
      {
        return this.m_AfterImg;
      }
    }

    public long start_at
    {
      get
      {
        return this.m_StartAt;
      }
    }

    public long end_at
    {
      get
      {
        return this.m_EndAt;
      }
    }

    public void Deserialize(JSON_AppealChargeParam _json)
    {
      if (_json == null)
        throw new InvalidJSONException();
      this.m_AppealId = _json.fields.appeal_id;
      this.m_BeforeImg = _json.fields.before_img_id;
      this.m_AfterImg = _json.fields.after_img_id;
      try
      {
        if (!string.IsNullOrEmpty(_json.fields.start_at))
          this.m_StartAt = TimeManager.GetUnixSec(DateTime.Parse(_json.fields.start_at));
        if (string.IsNullOrEmpty(_json.fields.end_at))
          return;
        this.m_EndAt = TimeManager.GetUnixSec(DateTime.Parse(_json.fields.end_at));
      }
      catch (Exception ex)
      {
        DebugUtility.LogError(ex.ToString());
      }
    }
  }
}
