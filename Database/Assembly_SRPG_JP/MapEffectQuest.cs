// Decompiled with JetBrains decompiler
// Type: SRPG.MapEffectQuest
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  public class MapEffectQuest : MonoBehaviour
  {
    public GameObject GoMapEffectParent;
    public GameObject GoMapEffectBaseItem;

    public MapEffectQuest()
    {
      base.\u002Ector();
    }

    public void Setup()
    {
      QuestParam dataOfClass = DataSource.FindDataOfClass<QuestParam>(((Component) this).get_gameObject(), (QuestParam) null);
      if (dataOfClass == null)
        return;
      GameManager instanceDirect = MonoSingleton<GameManager>.GetInstanceDirect();
      if (!Object.op_Implicit((Object) instanceDirect))
        return;
      if (Object.op_Implicit((Object) this.GoMapEffectParent) && Object.op_Implicit((Object) this.GoMapEffectBaseItem))
      {
        this.GoMapEffectBaseItem.SetActive(false);
        BattleUnitDetail.DestroyChildGameObjects(this.GoMapEffectParent, new List<GameObject>((IEnumerable<GameObject>) new GameObject[1]
        {
          this.GoMapEffectBaseItem
        }));
      }
      MapEffectParam mapEffectParam = instanceDirect.GetMapEffectParam(dataOfClass.MapEffectId);
      if (mapEffectParam == null)
        return;
      DataSource component = (DataSource) ((Component) this).get_gameObject().GetComponent<DataSource>();
      if (Object.op_Implicit((Object) component))
        component.Clear();
      DataSource.Bind<MapEffectParam>(((Component) this).get_gameObject(), mapEffectParam);
      if (!Object.op_Implicit((Object) this.GoMapEffectParent) || !Object.op_Implicit((Object) this.GoMapEffectBaseItem))
        return;
      for (int index = 0; index < mapEffectParam.ValidSkillLists.Count; ++index)
      {
        SkillParam skillParam = instanceDirect.GetSkillParam(mapEffectParam.ValidSkillLists[index]);
        if (skillParam != null)
        {
          using (List<JobParam>.Enumerator enumerator = MapEffectParam.GetHaveJobLists(skillParam.iname).GetEnumerator())
          {
            while (enumerator.MoveNext())
            {
              JobParam current = enumerator.Current;
              GameObject gameObject = (GameObject) Object.Instantiate<GameObject>((M0) this.GoMapEffectBaseItem);
              if (Object.op_Implicit((Object) gameObject))
              {
                gameObject.get_transform().SetParent(this.GoMapEffectParent.get_transform());
                gameObject.get_transform().set_localScale(Vector3.get_one());
                DataSource.Bind<JobParam>(gameObject, current);
                DataSource.Bind<SkillParam>(gameObject, skillParam);
                gameObject.SetActive(true);
              }
            }
          }
        }
      }
    }

    public static GameObject CreateInstance(GameObject go_target, Transform parent = null)
    {
      if (!Object.op_Implicit((Object) go_target))
        return (GameObject) null;
      GameObject gameObject = (GameObject) Object.Instantiate<GameObject>((M0) go_target);
      if (!Object.op_Implicit((Object) gameObject))
        return (GameObject) null;
      if (Object.op_Implicit((Object) parent))
        gameObject.get_transform().SetParent(parent);
      RectTransform component1 = (RectTransform) go_target.GetComponent<RectTransform>();
      if (Object.op_Inequality((Object) component1, (Object) null) && Object.op_Inequality((Object) go_target.GetComponent<Canvas>(), (Object) null))
      {
        RectTransform component2 = (RectTransform) gameObject.GetComponent<RectTransform>();
        if (Object.op_Implicit((Object) component2))
        {
          component2.set_anchorMax(component1.get_anchorMax());
          component2.set_anchorMin(component1.get_anchorMin());
          component2.set_anchoredPosition(component1.get_anchoredPosition());
          component2.set_sizeDelta(component1.get_sizeDelta());
        }
      }
      gameObject.get_transform().set_localScale(Vector3.get_one());
      return gameObject;
    }
  }
}
