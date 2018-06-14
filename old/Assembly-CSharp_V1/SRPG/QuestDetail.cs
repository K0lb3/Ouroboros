// Decompiled with JetBrains decompiler
// Type: SRPG.QuestDetail
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  public class QuestDetail : MonoBehaviour
  {
    public GameObject[] Missions;
    public GameObject NoMissionText;

    public QuestDetail()
    {
      base.\u002Ector();
    }

    private void Start()
    {
      QuestParam dataOfClass = DataSource.FindDataOfClass<QuestParam>(((Component) this).get_gameObject(), (QuestParam) null);
      if (dataOfClass == null)
        return;
      this.SetMissionListActive(!string.IsNullOrEmpty(dataOfClass.mission));
    }

    private void SetMissionListActive(bool active)
    {
      for (int index = 0; index < this.Missions.Length; ++index)
      {
        if (this.Missions != null)
          this.Missions[index].SetActive(active);
      }
      if (!Object.op_Inequality((Object) this.NoMissionText, (Object) null))
        return;
      this.NoMissionText.SetActive(!active);
    }
  }
}
