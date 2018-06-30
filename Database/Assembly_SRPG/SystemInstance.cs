namespace SRPG
{
    using GR;
    using System;
    using UnityEngine;

    [AddComponentMenu("Scripts/SRPG/System/SystemInstance")]
    public class SystemInstance : MonoSingleton<SystemInstance>
    {
        private static Type[] SYSTEM_INSTANCE;

        static SystemInstance()
        {
            Type[] typeArray1;
            typeArray1 = new Type[] { typeof(GameManager), typeof(TimeManager), typeof(Network) };
            SYSTEM_INSTANCE = typeArray1;
            return;
        }

        public SystemInstance()
        {
            base..ctor();
            return;
        }

        protected override void Initialize()
        {
            int num;
            int num2;
            Type type;
            GameObject obj2;
            DeployGateSDK.Install();
            num = (int) SYSTEM_INSTANCE.Length;
            num2 = 0;
            goto Label_0050;
        Label_0014:
            type = SYSTEM_INSTANCE[num2];
            obj2 = new GameObject();
            obj2.get_transform().set_parent(base.get_transform());
            obj2.get_transform().set_name(type.Name);
            obj2.AddComponent(type);
            num2 += 1;
        Label_0050:
            if (num2 < num)
            {
                goto Label_0014;
            }
            Object.DontDestroyOnLoad(this);
            return;
        }
    }
}

