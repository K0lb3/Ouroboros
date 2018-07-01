// Decompiled with JetBrains decompiler
// Type: SRPG.RankMatchMapInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System;
using UnityEngine;

namespace SRPG
{
  public class RankMatchMapInfo : MonoBehaviour
  {
    [SerializeField]
    private GameParameter NameParam;
    [SerializeField]
    private GameParameter DescriptionParam;
    [SerializeField]
    private GameParameter ThumbnailParam;
    [SerializeField]
    private LText TimeText;

    public RankMatchMapInfo()
    {
      base.\u002Ector();
    }

    private void OnEnable()
    {
      this.UpdateValue();
    }

    public void UpdateValue()
    {
      VersusEnableTimeScheduleParam dataOfClass = DataSource.FindDataOfClass<VersusEnableTimeScheduleParam>(((Component) this).get_gameObject(), (VersusEnableTimeScheduleParam) null);
      if (dataOfClass == null)
        return;
      DataSource.Bind<QuestParam>(((Component) this).get_gameObject(), MonoSingleton<GameManager>.Instance.FindQuest(dataOfClass.QuestIname));
      this.NameParam.UpdateValue();
      this.DescriptionParam.UpdateValue();
      this.ThumbnailParam.UpdateValue();
      DateTime dateTime1 = DateTime.Parse(TimeManager.ServerTime.ToShortDateString() + " " + dataOfClass.Begin + ":00");
      TimeSpan timeSpan = TimeSpan.Parse(dataOfClass.Open);
      DateTime dateTime2 = dateTime1 + timeSpan;
      this.TimeText.set_text(string.Format(LocalizedText.Get("sys.RANK_MATCH_ENABLE_TIME"), new object[4]
      {
        (object) dateTime1.Hour,
        (object) dateTime1.Minute,
        (object) dateTime2.Hour,
        (object) dateTime2.Minute
      }));
    }
  }
}
