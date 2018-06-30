namespace SRPG
{
    using Photon;
    using System;
    using UnityEngine;

    public abstract class PunMonoSingleton<T> : PunBehaviour where T: PunBehaviour
    {
        private static T instance_;

        static PunMonoSingleton()
        {
        }

        protected PunMonoSingleton()
        {
            base..ctor();
            return;
        }

        private void Awake()
        {
            if ((((T) PunMonoSingleton<T>.Instance) != this) == null)
            {
                goto Label_001C;
            }
            Object.Destroy(this);
            return;
        Label_001C:
            this.Initialize();
            return;
        }

        protected virtual void Initialize()
        {
        }

        private void OnApplicationQuit()
        {
            this.Release();
            PunMonoSingleton<T>.instance_ = (T) null;
            return;
        }

        private void OnDestroy()
        {
            this.Release();
            PunMonoSingleton<T>.instance_ = (T) null;
            return;
        }

        protected virtual void Release()
        {
        }

        public static T Instance
        {
            get
            {
                Type[] typeArray1;
                Type type;
                GameObject obj2;
                if ((((T) PunMonoSingleton<T>.instance_) == null) == null)
                {
                    goto Label_00A3;
                }
                type = typeof(T);
                PunMonoSingleton<T>.instance_ = (T) (Object.FindObjectOfType(type) as T);
                if ((((T) PunMonoSingleton<T>.instance_) == null) == null)
                {
                    goto Label_00A3;
                }
                typeArray1 = new Type[] { type };
                obj2 = new GameObject(type.ToString(), typeArray1);
                if ((obj2 == null) == null)
                {
                    goto Label_006C;
                }
            Label_006C:
                obj2.get_transform().set_name(type.Name);
                PunMonoSingleton<T>.instance_ = obj2.GetComponent<T>();
                if ((((T) PunMonoSingleton<T>.instance_) == null) == null)
                {
                    goto Label_00A3;
                }
                Object.Destroy(obj2);
            Label_00A3:
                return PunMonoSingleton<T>.instance_;
            }
        }
    }
}

