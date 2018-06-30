namespace SRPG
{
    using System;
    using UnityEngine;

    public class WeatherInfo : MonoBehaviour
    {
        public GameObject GoWeatherInfo;

        public WeatherInfo()
        {
            base..ctor();
            return;
        }

        public void Refresh(WeatherData wd)
        {
            GameObject obj2;
            DataSource source;
            if (this.GoWeatherInfo == null)
            {
                goto Label_0059;
            }
            if (wd == null)
            {
                goto Label_0047;
            }
            obj2 = this.GoWeatherInfo;
            source = obj2.GetComponent<DataSource>();
            if (source == null)
            {
                goto Label_0035;
            }
            source.Clear();
        Label_0035:
            DataSource.Bind<WeatherParam>(obj2, wd.WeatherParam);
            GameParameter.UpdateAll(obj2);
        Label_0047:
            this.GoWeatherInfo.SetActive((wd == null) == 0);
        Label_0059:
            return;
        }

        private void Start()
        {
            if (this.GoWeatherInfo == null)
            {
                goto Label_001C;
            }
            this.GoWeatherInfo.SetActive(0);
        Label_001C:
            return;
        }
    }
}

