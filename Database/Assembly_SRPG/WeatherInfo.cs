// Decompiled with JetBrains decompiler
// Type: SRPG.WeatherInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  public class WeatherInfo : MonoBehaviour
  {
    public GameObject GoWeatherInfo;

    public WeatherInfo()
    {
      base.\u002Ector();
    }

    private void Start()
    {
      if (!Object.op_Implicit((Object) this.GoWeatherInfo))
        return;
      this.GoWeatherInfo.SetActive(false);
    }

    public void Refresh(WeatherData wd)
    {
      if (!Object.op_Implicit((Object) this.GoWeatherInfo))
        return;
      if (wd != null)
      {
        GameObject goWeatherInfo = this.GoWeatherInfo;
        DataSource component = (DataSource) goWeatherInfo.GetComponent<DataSource>();
        if (Object.op_Implicit((Object) component))
          component.Clear();
        DataSource.Bind<WeatherParam>(goWeatherInfo, wd.WeatherParam);
        GameParameter.UpdateAll(goWeatherInfo);
      }
      this.GoWeatherInfo.SetActive(wd != null);
    }
  }
}
