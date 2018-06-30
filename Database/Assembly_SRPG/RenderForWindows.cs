namespace SRPG
{
    using System;
    using System.Runtime.InteropServices;
    using UnityEngine;
    using UnityEngine.UI;

    public class RenderForWindows : MonoBehaviour
    {
        private const string MATERIAL_SHADER_NAME = "Unlit/Texture";
        private const string RAW_IMAGE_NAME = "RawImage";
        private const string CANVAS_NAME = "Canvas";
        [SerializeField]
        private eTargetType target_type;
        [SerializeField]
        private Camera target_camera;
        private int canvas_sort_order;
        private bool is_enable;
        private RenderTexture render_texture;
        private Canvas canvas;
        private RawImage image;
        private RectTransform img_rect_transform;
        private bool is_dont_create_render_texture;

        public RenderForWindows()
        {
            this.canvas_sort_order = -1;
            base..ctor();
            return;
        }

        private void Awake()
        {
            this.is_enable = 1;
            if (this.is_enable != null)
            {
                goto Label_0013;
            }
            return;
        Label_0013:
            this.canvas = this.CreateCanvas();
            this.image = this.CreateRawImage(this.canvas.get_transform(), (float) Screen.get_width(), (float) Screen.get_height());
            this.img_rect_transform = this.image.get_transform() as RectTransform;
            return;
        }

        private Canvas CreateCanvas()
        {
            GameObject obj2;
            Canvas canvas;
            if (this.is_enable != null)
            {
                goto Label_000D;
            }
            return null;
        Label_000D:
            obj2 = new GameObject();
            obj2.set_name("Canvas");
            obj2.get_transform().SetParent(base.get_transform().get_parent(), 1);
            canvas = obj2.AddComponent<Canvas>();
            canvas.set_renderMode(0);
            canvas.set_sortingOrder(this.canvas_sort_order);
            return canvas;
        }

        private RawImage CreateRawImage(Transform _parent, float _width, float _height)
        {
            GameObject obj2;
            RawImage image;
            RectTransform transform;
            if (this.is_enable != null)
            {
                goto Label_000D;
            }
            return null;
        Label_000D:
            obj2 = new GameObject();
            obj2.set_name("RawImage");
            obj2.get_transform().SetParent(_parent, 1);
            image = obj2.AddComponent<RawImage>();
            image.set_material(new Material(Shader.Find("Unlit/Texture")));
            transform = image.get_transform() as RectTransform;
            transform.set_anchoredPosition(Vector2.get_zero());
            transform.set_sizeDelta(new Vector2(_width, _height));
            return image;
        }

        private RenderTexture CreateRenderTexture(int _width, int _height)
        {
            RenderTexture texture;
            if (this.is_enable != null)
            {
                goto Label_000D;
            }
            return null;
        Label_000D:
            texture = new RenderTexture(_width, _height, 0);
            texture.set_format(0);
            texture.set_depth(0x18);
            texture.set_filterMode(0);
            texture.set_generateMips(0);
            texture.set_useMipMap(0);
            if (texture.Create() != null)
            {
                goto Label_0051;
            }
            DebugUtility.LogError("RenderTexture生成に失敗");
            return null;
        Label_0051:
            return texture;
        }

        private void Init()
        {
            eTargetType type;
            if (this.is_enable != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            type = this.target_type;
            if (type == null)
            {
                goto Label_0025;
            }
            if (type == 1)
            {
                goto Label_0035;
            }
            goto Label_0061;
        Label_0025:
            this.target_camera = Camera.get_main();
            goto Label_0061;
        Label_0035:
            this.target_camera = ((this.target_camera != null) == null) ? Camera.get_main() : this.target_camera;
        Label_0061:
            if ((this.render_texture == null) == null)
            {
                goto Label_00B5;
            }
            if (this.is_dont_create_render_texture != null)
            {
                goto Label_00B5;
            }
            this.render_texture = this.CreateRenderTexture(Screen.get_width(), Screen.get_height());
            this.target_camera.set_targetTexture(this.render_texture);
            this.image.set_texture(this.render_texture);
        Label_00B5:
            return;
        }

        private unsafe void LateUpdate()
        {
            Vector2 vector;
            Vector2 vector2;
            if (this.is_enable != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            if ((this.img_rect_transform == null) == null)
            {
                goto Label_001E;
            }
            return;
        Label_001E:
            if (&this.img_rect_transform.get_sizeDelta().x != ((float) Screen.get_width()))
            {
                goto Label_005A;
            }
            if (&this.img_rect_transform.get_sizeDelta().y == ((float) Screen.get_height()))
            {
                goto Label_0076;
            }
        Label_005A:
            this.img_rect_transform.set_sizeDelta(new Vector2((float) Screen.get_width(), (float) Screen.get_height()));
        Label_0076:
            return;
        }

        public void SetRenderTexture(RenderTexture _render_texture)
        {
            this.render_texture = _render_texture;
            this.image.set_texture(this.render_texture);
            return;
        }

        public void SetTargetType(eTargetType _type, Camera _target_camera)
        {
            this.target_type = _type;
            if (this.target_type != 1)
            {
                goto Label_001A;
            }
            this.target_camera = _target_camera;
        Label_001A:
            return;
        }

        private void Start()
        {
            if (this.is_enable != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            this.Init();
            return;
        }

        public bool IsDontCreteRenderTexture
        {
            set
            {
                this.is_dont_create_render_texture = value;
                return;
            }
        }

        public enum eTargetType
        {
            MAIN_CAMERA,
            SELECTED
        }
    }
}

