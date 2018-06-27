// Decompiled with JetBrains decompiler
// Type: SRPG.MapEffectQuestBattleUI
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  public class MapEffectQuestBattleUI : MonoBehaviour
  {
    public SRPG_Button ButtonMapEffect;
    public string PrefabMapEffectQuest;
    private LoadRequest mReqMapEffect;

    public MapEffectQuestBattleUI()
    {
      base.\u002Ector();
    }

    private void Start()
    {
      if (!Object.op_Implicit((Object) this.ButtonMapEffect))
        return;
      this.ButtonMapEffect.AddListener((SRPG_Button.ButtonClickEvent) (button => this.OpenMapEffect()));
      if (string.IsNullOrEmpty(this.PrefabMapEffectQuest))
        return;
      this.mReqMapEffect = AssetManager.LoadAsync<GameObject>(this.PrefabMapEffectQuest);
    }

    private void OpenMapEffect()
    {
      if (this.mReqMapEffect == null || !this.mReqMapEffect.isDone && Object.op_Equality(this.mReqMapEffect.asset, (Object) null))
        return;
      GameManager instanceDirect = MonoSingleton<GameManager>.GetInstanceDirect();
      SceneBattle instance1 = SceneBattle.Instance;
      if (!Object.op_Implicit((Object) instanceDirect) || !Object.op_Implicit((Object) instance1))
        return;
      QuestParam quest = instanceDirect.FindQuest(instance1.CurrentQuest.iname);
      if (quest == null)
        return;
      GameObject instance2 = MapEffectQuest.CreateInstance(this.mReqMapEffect.asset as GameObject, ((Component) this).get_transform().get_parent());
      if (!Object.op_Implicit((Object) instance2))
        return;
      DataSource.Bind<QuestParam>(instance2, quest);
      instance2.SetActive(true);
      MapEffectQuest component = (MapEffectQuest) instance2.GetComponent<MapEffectQuest>();
      if (!Object.op_Implicit((Object) component))
        return;
      component.Setup();
    }
  }
}
