namespace SRPG
{
    using System;
    using UnityEngine;

    [EventActionInfo("New/立ち絵2/配置3(2D)", "立ち絵2を配置します", 0x555555, 0x444488)]
    public class Event2dAction_StandChara3 : EventAction
    {
        private const string SHADER_NAME = "UI/Custom/EventStandChara";
        private const string PROPERTYNAME_SCALE_X = "_ScaleX";
        private const string PROPERTYNAME_SCALE_Y = "_ScaleY";
        private const string PROPERTYNAME_OFFSET_X = "_OffsetX";
        private const string PROPERTYNAME_OFFSET_Y = "_OffsetY";
        private const string PROPERTYNAME_FACE_TEX = "_FaceTex";
        private static readonly string AssetPath;
        public string CharaID;
        public GameObject StandTemplate;
        private string DummyID;
        public bool NewMaterial;
        private GameObject mStandObject;
        private EventStandCharaController2 mEVCharaController;
        private static readonly Vector2 START_POSITION;

        static Event2dAction_StandChara3()
        {
            AssetPath = "Event2dAssets/Event2dStand";
            START_POSITION = new Vector2(-1f, 0f);
            return;
        }

        public Event2dAction_StandChara3()
        {
            this.DummyID = "dummyID";
            this.NewMaterial = 1;
            base..ctor();
            return;
        }

        public override void OnActivate()
        {
            if ((this.mStandObject != null) == null)
            {
                goto Label_0037;
            }
            if (this.mStandObject.get_gameObject().get_activeInHierarchy() != null)
            {
                goto Label_0037;
            }
            this.mStandObject.get_gameObject().SetActive(1);
        Label_0037:
            this.mEVCharaController.Open(0f);
            base.ActivateNext();
            return;
        }

        protected override void OnDestroy()
        {
            if ((this.mStandObject != null) == null)
            {
                goto Label_0021;
            }
            Object.Destroy(this.mStandObject.get_gameObject());
        Label_0021:
            return;
        }

        public override unsafe void PreStart()
        {
            string str;
            int num;
            EventStandChara2 chara;
            RectTransform transform;
            RectTransform transform2;
            Vector2 vector;
            Vector2 vector2;
            Vector2 vector3;
            Vector2 vector4;
            Vector2 vector5;
            Vector2 vector6;
            Vector2 vector7;
            Material material;
            Texture texture;
            RectTransform transform3;
            Vector2 vector8;
            Vector3 vector9;
            Vector2 vector10;
            Vector3 vector11;
            Vector2 vector12;
            Vector3 vector13;
            Vector2 vector14;
            Vector3 vector15;
            Vector3 vector16;
            Vector2 vector17;
            Vector3 vector18;
            Vector2 vector19;
            Vector3 vector20;
            Vector2 vector21;
            Vector3 vector22;
            Vector2 vector23;
            Vector2 vector24;
            if (this.NewMaterial == null)
            {
                goto Label_001F;
            }
            Shader.DisableKeyword("EVENT_MONOCHROME_ON");
            Shader.DisableKeyword("EVENT_SEPIA_ON");
        Label_001F:
            if ((this.mStandObject == null) == null)
            {
                goto Label_043A;
            }
            str = this.DummyID;
            if (string.IsNullOrEmpty(this.CharaID) != null)
            {
                goto Label_004E;
            }
            str = this.CharaID;
        Label_004E:
            if ((EventStandCharaController2.FindInstances(str) != null) == null)
            {
                goto Label_007C;
            }
            this.mEVCharaController = EventStandCharaController2.FindInstances(str);
            this.mStandObject = this.mEVCharaController.get_gameObject();
        Label_007C:
            if ((this.mStandObject == null) == null)
            {
                goto Label_03C6;
            }
            if ((this.StandTemplate != null) == null)
            {
                goto Label_03C6;
            }
            this.mStandObject = Object.Instantiate<GameObject>(this.StandTemplate);
            this.mEVCharaController = this.mStandObject.GetComponent<EventStandCharaController2>();
            this.mEVCharaController.CharaID = this.CharaID;
            if (this.NewMaterial == null)
            {
                goto Label_03C6;
            }
            num = 0;
            goto Label_03B3;
        Label_00E3:
            try
            {
                chara = this.mEVCharaController.StandCharaList[num].GetComponent<EventStandChara2>();
                transform = chara.BodyObject.GetComponent<RectTransform>();
                transform2 = chara.FaceObject.GetComponent<RectTransform>();
                &vector..ctor(&transform.get_sizeDelta().x * &transform.get_localScale().x, &transform.get_sizeDelta().y * &transform.get_localScale().y);
                &vector2..ctor(&transform2.get_sizeDelta().x * &transform2.get_localScale().x, &transform2.get_sizeDelta().y * &transform2.get_localScale().y);
                if (Mathf.Approximately(&vector2.x, 0f) != null)
                {
                    goto Label_01C9;
                }
                if (Mathf.Approximately(&vector2.y, 0f) == null)
                {
                    goto Label_01F0;
                }
            Label_01C9:
                &vector3..ctor(0f, 0f);
                &vector4..ctor(2f, 2f);
                goto Label_02FF;
            Label_01F0:
                &vector3..ctor(&vector.x / &vector2.x, &vector.y / &vector2.y);
                &vector5..ctor(&transform2.get_localPosition().x - (&transform2.get_pivot().x * &vector2.x), &transform2.get_localPosition().y - (&transform2.get_pivot().y * &vector2.y));
                &vector6..ctor(&transform.get_localPosition().x - (&transform.get_pivot().x * &vector.x), &transform.get_localPosition().y - (&transform.get_pivot().y * &vector.y));
                vector7 = vector5 - vector6;
                &vector4..ctor((-1f * &vector7.x) / &vector2.x, (-1f * &vector7.y) / &vector2.y);
            Label_02FF:
                material = new Material(Shader.Find("UI/Custom/EventStandChara"));
                texture = chara.FaceObject.GetComponent<RawImage>().get_mainTexture();
                this.SetMaterialProperty(material, "_FaceTex", texture);
                this.SetMaterialProperty(material, "_ScaleX", &vector3.x);
                this.SetMaterialProperty(material, "_ScaleY", &vector3.y);
                this.SetMaterialProperty(material, "_OffsetX", &vector4.x);
                this.SetMaterialProperty(material, "_OffsetY", &vector4.y);
                chara.BodyObject.GetComponent<RawImage>().set_material(material);
                GameUtility.SetGameObjectActive(chara.FaceObject, 0);
                goto Label_03AF;
            }
            catch (Exception)
            {
            Label_03A9:
                goto Label_03AF;
            }
        Label_03AF:
            num += 1;
        Label_03B3:
            if (num < ((int) this.mEVCharaController.StandCharaList.Length))
            {
                goto Label_00E3;
            }
        Label_03C6:
            if ((this.mStandObject != null) == null)
            {
                goto Label_043A;
            }
            this.mStandObject.get_transform().SetParent(base.ActiveCanvas.get_transform(), 0);
            this.mStandObject.get_transform().SetAsLastSibling();
            this.mStandObject.get_gameObject().SetActive(0);
            transform3 = this.mStandObject.GetComponent<RectTransform>();
            vector24 = START_POSITION;
            transform3.set_anchorMax(vector24);
            transform3.set_anchorMin(vector24);
        Label_043A:
            return;
        }

        private bool SetMaterialProperty(Material material, string name, float val)
        {
            if (material.HasProperty(name) != null)
            {
                goto Label_000E;
            }
            return 0;
        Label_000E:
            material.SetFloat(name, val);
            return 1;
        }

        private bool SetMaterialProperty(Material material, string name, Texture val)
        {
            if (material.HasProperty(name) != null)
            {
                goto Label_000E;
            }
            return 0;
        Label_000E:
            material.SetTexture(name, val);
            return 1;
        }
    }
}

