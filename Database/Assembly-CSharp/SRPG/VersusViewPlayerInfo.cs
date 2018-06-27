// Decompiled with JetBrains decompiler
// Type: SRPG.VersusViewPlayerInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class VersusViewPlayerInfo : MonoBehaviour
  {
    public GameObject EmptyObj;
    public GameObject ValidObj;
    public GameObject LeaderUnit;
    public GameObject ReadyObj;
    public GameObject Award;
    public Text Name;
    public Text Lv;
    public Text Total;

    public VersusViewPlayerInfo()
    {
      base.\u002Ector();
    }

    private void Start()
    {
    }

    public void Refresh()
    {
      JSON_MyPhotonPlayerParam dataOfClass = DataSource.FindDataOfClass<JSON_MyPhotonPlayerParam>(((Component) this).get_gameObject(), (JSON_MyPhotonPlayerParam) null);
      if (dataOfClass != null)
      {
        if (Object.op_Inequality((Object) this.EmptyObj, (Object) null))
          this.EmptyObj.SetActive(false);
        if (Object.op_Inequality((Object) this.ValidObj, (Object) null))
          this.ValidObj.SetActive(true);
        if (Object.op_Inequality((Object) this.LeaderUnit, (Object) null) && dataOfClass.units != null)
        {
          dataOfClass.SetupUnits();
          DataSource.Bind<UnitData>(this.LeaderUnit, dataOfClass.units[0].unit);
        }
        if (Object.op_Inequality((Object) this.Name, (Object) null))
          this.Name.set_text(dataOfClass.playerName);
        if (Object.op_Inequality((Object) this.Lv, (Object) null))
          this.Lv.set_text(dataOfClass.playerLevel.ToString());
        if (Object.op_Inequality((Object) this.Total, (Object) null))
          this.Total.set_text(dataOfClass.totalAtk.ToString());
        if (Object.op_Inequality((Object) this.ReadyObj, (Object) null))
          this.ReadyObj.SetActive(dataOfClass.state != 4);
        if (Object.op_Inequality((Object) this.Award, (Object) null))
        {
          this.Award.get_gameObject().SetActive(false);
          this.Award.get_gameObject().SetActive(true);
        }
        GameParameter.UpdateAll(((Component) this).get_gameObject());
      }
      else
      {
        if (Object.op_Inequality((Object) this.EmptyObj, (Object) null))
          this.EmptyObj.SetActive(true);
        if (Object.op_Inequality((Object) this.ValidObj, (Object) null))
          this.ValidObj.SetActive(false);
        if (!Object.op_Inequality((Object) this.ReadyObj, (Object) null))
          return;
        this.ReadyObj.SetActive(false);
      }
    }
  }
}
