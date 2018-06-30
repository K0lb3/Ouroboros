namespace SRPG
{
    using System;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.UI;

    public class TouchControlArea : MonoBehaviour
    {
        public string TargetObjID;
        public string SillObjID;
        [SerializeField]
        private Button ResetButton;
        private GameObject TargetObj;
        private GameObject SillObj;
        private Vector3 sPos;
        private Quaternion sRot;
        private float tx;
        private float ty;
        private float v;
        private static readonly float vMin;
        private static readonly float vMax;
        private Vector3 targetScale;
        private Vector3 sillScale;
        private float wid;

        static TouchControlArea()
        {
            vMin = 1f;
            vMax = 1.4f;
            return;
        }

        public TouchControlArea()
        {
            this.TargetObjID = "UNITPREVIEW";
            this.SillObjID = "UNITPREVIEWBASE";
            base..ctor();
            return;
        }

        private unsafe void GetTouch()
        {
            TouchState state;
            Vector3 vector;
            float num;
            float num2;
            float num3;
            state = -1;
            if (Input.GetMouseButtonDown(0) != null)
            {
                goto Label_0023;
            }
            if (Input.GetMouseButtonDown(1) != null)
            {
                goto Label_0023;
            }
            if (Input.GetMouseButtonDown(2) == null)
            {
                goto Label_0025;
            }
        Label_0023:
            state = 0;
        Label_0025:
            if (state != -1)
            {
                goto Label_004F;
            }
            if (Input.GetMouseButton(0) != null)
            {
                goto Label_004D;
            }
            if (Input.GetMouseButton(1) != null)
            {
                goto Label_004D;
            }
            if (Input.GetMouseButton(2) == null)
            {
                goto Label_004F;
            }
        Label_004D:
            state = 1;
        Label_004F:
            if (state != -1)
            {
                goto Label_0079;
            }
            if (Input.GetMouseButtonUp(0) != null)
            {
                goto Label_0077;
            }
            if (Input.GetMouseButton(1) != null)
            {
                goto Label_0077;
            }
            if (Input.GetMouseButton(2) == null)
            {
                goto Label_0079;
            }
        Label_0077:
            state = 3;
        Label_0079:
            if (state != null)
            {
                goto Label_00A5;
            }
            this.sPos = Input.get_mousePosition();
            this.sRot = this.TargetObj.get_transform().get_rotation();
            goto Label_0112;
        Label_00A5:
            if (state != 1)
            {
                goto Label_0112;
            }
            num = (&Input.get_mousePosition().x - &this.sPos.x) / this.wid;
            num2 = 0f;
            this.TargetObj.get_transform().set_rotation(this.sRot);
            this.TargetObj.get_transform().Rotate(new Vector3(90f * num2, -90f * num, 0f), 0);
        Label_0112:
            num3 = Input.GetAxis("Mouse ScrollWheel");
            if (num3 == 0f)
            {
                goto Label_01B1;
            }
            this.v += num3;
            if (this.v < vMax)
            {
                goto Label_0154;
            }
            this.v = vMax;
        Label_0154:
            if (this.v >= vMin)
            {
                goto Label_016F;
            }
            this.v = vMin;
        Label_016F:
            this.TargetObj.get_transform().set_localScale(this.targetScale * this.v);
            this.SillObj.get_transform().set_localScale(this.sillScale * this.v);
        Label_01B1:
            return;
        }

        public void Reset()
        {
            this.TargetObj.get_transform().set_rotation(Quaternion.get_identity());
            this.TargetObj.get_transform().Rotate(new Vector3(0f, 180f, 0f), 0);
            this.TargetObj.get_transform().set_localScale(this.targetScale);
            this.SillObj.get_transform().set_localScale(this.sillScale);
            this.v = vMin;
            return;
        }

        private void Start()
        {
            if ((this.ResetButton != null) == null)
            {
                goto Label_002D;
            }
            this.ResetButton.get_onClick().AddListener(new UnityAction(this, this.Reset));
        Label_002D:
            if (string.IsNullOrEmpty(this.TargetObjID) != null)
            {
                goto Label_004E;
            }
            this.TargetObj = GameObjectID.FindGameObject(this.TargetObjID);
        Label_004E:
            if (string.IsNullOrEmpty(this.SillObjID) != null)
            {
                goto Label_006F;
            }
            this.SillObj = GameObjectID.FindGameObject(this.SillObjID);
        Label_006F:
            this.wid = (float) (Screen.get_width() / 5);
            if ((this.TargetObj != null) == null)
            {
                goto Label_00A4;
            }
            this.targetScale = this.TargetObj.get_transform().get_localScale();
        Label_00A4:
            if ((this.SillObj != null) == null)
            {
                goto Label_00CB;
            }
            this.sillScale = this.SillObj.get_transform().get_localScale();
        Label_00CB:
            return;
        }

        private void Update()
        {
            this.GetTouch();
            return;
        }

        public enum TouchState
        {
            None = -1,
            Began = 0,
            Moved = 1,
            Stationary = 2,
            Ended = 3,
            Canceled = 4
        }
    }
}

