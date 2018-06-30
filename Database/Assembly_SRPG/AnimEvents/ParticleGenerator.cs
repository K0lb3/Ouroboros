namespace SRPG.AnimEvents
{
    using SRPG;
    using System;
    using UnityEngine;

    public class ParticleGenerator : AnimEventWithTarget
    {
        public GameObject Template;
        public bool Attach;
        public bool NotParticle;

        public ParticleGenerator()
        {
            base..ctor();
            return;
        }

        protected virtual void OnGenerate(GameObject go)
        {
        }

        public override unsafe void OnStart(GameObject go)
        {
            Vector3 vector;
            Quaternion quaternion;
            GameObject obj2;
            Vector3 vector2;
            Transform transform;
            DestructTimer timer;
            Vector3 vector3;
            Vector3 vector4;
            if ((this.Template == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            base.CalcPosition(go, this.Template, &vector, &quaternion);
            obj2 = (GameObject) Object.Instantiate(this.Template, vector, quaternion);
            if (this.NotParticle != null)
            {
                goto Label_0048;
            }
            GameUtility.RequireComponent<OneShotParticle>(obj2);
        Label_0048:
            if ((&go.get_transform().get_lossyScale().x * &go.get_transform().get_lossyScale().z) >= 0f)
            {
                goto Label_00A6;
            }
            vector2 = obj2.get_transform().get_localScale();
            &vector2.z *= -1f;
            obj2.get_transform().set_localScale(vector2);
        Label_00A6:
            if (this.Attach == null)
            {
                goto Label_011E;
            }
            if (string.IsNullOrEmpty(base.BoneName) != null)
            {
                goto Label_011E;
            }
            transform = GameUtility.findChildRecursively(go.get_transform(), base.BoneName);
            if ((base.BoneName == "CAMERA") == null)
            {
                goto Label_0104;
            }
            if (Camera.get_main() == null)
            {
                goto Label_0104;
            }
            transform = Camera.get_main().get_transform();
        Label_0104:
            if ((transform != null) == null)
            {
                goto Label_011E;
            }
            obj2.get_transform().SetParent(transform);
        Label_011E:
            this.OnGenerate(obj2);
            if (base.End <= (base.Start + 0.1f))
            {
                goto Label_0164;
            }
            timer = GameUtility.RequireComponent<DestructTimer>(obj2);
            if (timer == null)
            {
                goto Label_0164;
            }
            timer.Timer = base.End - base.Start;
        Label_0164:
            return;
        }
    }
}

