namespace SRPG
{
    using SRPG.AnimEvents;
    using System;
    using System.Runtime.InteropServices;
    using UnityEngine;

    public abstract class AnimEventWithTarget : AnimEvent
    {
        protected const string BONE_NAME_CAMERA = "CAMERA";
        public string BoneName;
        public Vector3 Offset;
        public Vector3 Rotation;
        public bool LocalOffset;
        public bool LocalRotation;

        protected AnimEventWithTarget()
        {
            this.BoneName = string.Empty;
            this.Offset = Vector3.get_zero();
            this.Rotation = Vector3.get_zero();
            this.LocalOffset = 1;
            this.LocalRotation = 1;
            base..ctor();
            return;
        }

        public void CalcPosition(GameObject go, GameObject prefab, out Vector3 spawnPos, out Quaternion spawnRot)
        {
            this.CalcPosition(go, prefab.get_transform().get_localPosition(), prefab.get_transform().get_localRotation(), spawnPos, spawnRot);
            return;
        }

        public unsafe void CalcPosition(GameObject go, Vector3 deltaOffset, Quaternion deltaRotation, out Vector3 spawnPos, out Quaternion spawnRot)
        {
            Transform transform;
            *(spawnPos) = this.Offset + deltaOffset;
            *(spawnRot) = Quaternion.Euler(&this.Rotation.x, &this.Rotation.y, &this.Rotation.z) * deltaRotation;
            transform = (string.IsNullOrEmpty(this.BoneName) != null) ? go.get_transform() : GameUtility.findChildRecursively(go.get_transform(), this.BoneName);
            if ((this as ParticleGenerator) == null)
            {
                goto Label_00AD;
            }
            if ((this.BoneName == "CAMERA") == null)
            {
                goto Label_00AD;
            }
            if (Camera.get_main() == null)
            {
                goto Label_00AD;
            }
            transform = Camera.get_main().get_transform();
        Label_00AD:
            if ((transform != null) == null)
            {
                goto Label_011F;
            }
            if (this.LocalOffset == null)
            {
                goto Label_00DD;
            }
            *(spawnPos) = transform.TransformPoint(*(spawnPos));
            goto Label_00FB;
        Label_00DD:
            *(spawnPos) = transform.TransformPoint(Vector3.get_zero()) + *(spawnPos);
        Label_00FB:
            if (this.LocalRotation == null)
            {
                goto Label_011F;
            }
            *(spawnRot) = transform.get_rotation() * *(spawnRot);
        Label_011F:
            return;
        }
    }
}

