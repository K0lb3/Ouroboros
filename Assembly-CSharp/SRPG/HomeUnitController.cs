// Decompiled with JetBrains decompiler
// Type: SRPG.HomeUnitController
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System.Collections;
using System.Diagnostics;
using UnityEngine;

namespace SRPG
{
  public class HomeUnitController : UnitController
  {
    public Color DirectLitColor = Color.get_white();
    public Color IndirectLitColor = Color.get_clear();
    private const string ID_WALK = "WALK";

    public static SectionParam GetHomeWorld()
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      SectionParam sectionParam = (SectionParam) null;
      if (!string.IsNullOrEmpty((string) GlobalVars.SelectedSection))
      {
        sectionParam = instance.FindWorld((string) GlobalVars.SelectedSection);
        if (sectionParam != null && (string.IsNullOrEmpty(sectionParam.home) || sectionParam.hidden))
          sectionParam = (SectionParam) null;
      }
      if (sectionParam == null)
      {
        QuestParam lastStoryQuest = instance.Player.FindLastStoryQuest();
        if (lastStoryQuest != null)
        {
          ChapterParam area = instance.FindArea(lastStoryQuest.ChapterID);
          if (area != null)
            sectionParam = instance.FindWorld(area.section);
        }
      }
      return sectionParam;
    }

    protected override void Start()
    {
      CriticalSection.Enter(CriticalSections.SceneChange);
      SectionParam homeWorld = HomeUnitController.GetHomeWorld();
      if (homeWorld == null)
      {
        Debug.LogError((object) "home world is null.");
      }
      else
      {
        UnitData unitDataByUnitId = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUnitID(homeWorld.unit);
        if (unitDataByUnitId == null)
        {
          Debug.LogError((object) ("ホーム画面用ユニットが存在しません '" + homeWorld.unit + "'"));
        }
        else
        {
          this.SetupUnit(unitDataByUnitId, -1);
          base.Start();
        }
      }
    }

    protected override void OnEnable()
    {
      base.OnEnable();
    }

    protected override void OnDisable()
    {
      base.OnDisable();
    }

    protected override void PostSetup()
    {
      base.PostSetup();
      this.LoadAnimationAsync("WALK", "CHM/Home_" + this.UnitData.UnitParam.model + "_walk0");
      this.StartCoroutine(this.LoadAsync());
    }

    [DebuggerHidden]
    private IEnumerator LoadAsync()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new HomeUnitController.\u003CLoadAsync\u003Ec__Iterator56() { \u003C\u003Ef__this = this };
    }
  }
}
