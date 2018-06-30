namespace SRPG
{
    using System;
    using UnityEngine;

    public class AbilityPowerUpResultContent : MonoBehaviour
    {
        public AbilityPowerUpResultContent()
        {
            base..ctor();
            return;
        }

        public void SetData(Param param)
        {
            DataSource.Bind<AbilityParam>(base.get_gameObject(), param.data);
            GameParameter.UpdateAll(base.get_gameObject());
            return;
        }

        public class Param
        {
            public AbilityParam data;

            public Param()
            {
                base..ctor();
                return;
            }
        }
    }
}

