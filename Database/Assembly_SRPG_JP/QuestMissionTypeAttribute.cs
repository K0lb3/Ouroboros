// Decompiled with JetBrains decompiler
// Type: SRPG.QuestMissionTypeAttribute
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System;

namespace SRPG
{
  public class QuestMissionTypeAttribute : Attribute
  {
    private QuestMissionValueType m_ValueType;

    public QuestMissionTypeAttribute(QuestMissionValueType valueType)
    {
      this.m_ValueType = valueType;
    }

    public QuestMissionValueType ValueType
    {
      get
      {
        return this.m_ValueType;
      }
    }
  }
}
