namespace SRPG
{
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    public class RawPolyImage : RawImage
    {
        public Quad[] Quads;
        public bool Transparent;
        public string Preview;
        private RectTransform mRectTransform;

        public RawPolyImage()
        {
            this.Quads = new Quad[0];
            base..ctor();
            return;
        }

        protected override void Awake()
        {
            base.Awake();
            this.mRectTransform = base.GetComponent<RectTransform>();
            return;
        }

        protected override unsafe void OnPopulateMesh(VertexHelper vh)
        {
            Texture texture;
            UIVertex vertex;
            Rect rect;
            Rect rect2;
            Color32 color;
            int num;
            int num2;
            int num3;
            Color color2;
            vh.Clear();
            if ((base.get_texture() == null) == null)
            {
                goto Label_0024;
            }
            if (this.Transparent != null)
            {
                goto Label_003D;
            }
        Label_0024:
            if (&base.get_color().a > 0f)
            {
                goto Label_003E;
            }
        Label_003D:
            return;
        Label_003E:
            vertex = new UIVertex();
            rect = this.mRectTransform.get_rect();
            rect2 = base.get_uvRect();
            color = base.get_color();
            num = 0;
            if (&color.r != 0xff)
            {
                goto Label_04D7;
            }
            if (&color.g != 0xff)
            {
                goto Label_04D7;
            }
            if (&color.b != 0xff)
            {
                goto Label_04D7;
            }
            if (&color.a != 0xff)
            {
                goto Label_04D7;
            }
            num2 = ((int) this.Quads.Length) - 1;
            goto Label_04CA;
        Label_00BE:
            float introduced9 = &rect.get_xMin();
            &&vertex.position.x = Mathf.Lerp(introduced9, &rect.get_xMax(), &&(this.Quads[num2]).v0.x);
            float introduced10 = &rect.get_yMin();
            &&vertex.position.y = Mathf.Lerp(introduced10, &rect.get_yMax(), &&(this.Quads[num2]).v0.y);
            &vertex.color = &(this.Quads[num2]).c0;
            float introduced11 = &rect2.get_xMin();
            &&vertex.uv0.x = Mathf.Lerp(introduced11, &rect2.get_xMax(), &&(this.Quads[num2]).v0.x);
            float introduced12 = &rect2.get_yMin();
            &&vertex.uv0.y = Mathf.Lerp(introduced12, &rect2.get_yMax(), &&(this.Quads[num2]).v0.y);
            vh.AddVert(vertex);
            float introduced13 = &rect.get_xMin();
            &&vertex.position.x = Mathf.Lerp(introduced13, &rect.get_xMax(), &&(this.Quads[num2]).v1.x);
            float introduced14 = &rect.get_yMin();
            &&vertex.position.y = Mathf.Lerp(introduced14, &rect.get_yMax(), &&(this.Quads[num2]).v1.y);
            &vertex.color = &(this.Quads[num2]).c1;
            float introduced15 = &rect2.get_xMin();
            &&vertex.uv0.x = Mathf.Lerp(introduced15, &rect2.get_xMax(), &&(this.Quads[num2]).v1.x);
            float introduced16 = &rect2.get_yMin();
            &&vertex.uv0.y = Mathf.Lerp(introduced16, &rect2.get_yMax(), &&(this.Quads[num2]).v1.y);
            vh.AddVert(vertex);
            float introduced17 = &rect.get_xMin();
            &&vertex.position.x = Mathf.Lerp(introduced17, &rect.get_xMax(), &&(this.Quads[num2]).v2.x);
            float introduced18 = &rect.get_yMin();
            &&vertex.position.y = Mathf.Lerp(introduced18, &rect.get_yMax(), &&(this.Quads[num2]).v2.y);
            &vertex.color = &(this.Quads[num2]).c2;
            float introduced19 = &rect2.get_xMin();
            &&vertex.uv0.x = Mathf.Lerp(introduced19, &rect2.get_xMax(), &&(this.Quads[num2]).v2.x);
            float introduced20 = &rect2.get_yMin();
            &&vertex.uv0.y = Mathf.Lerp(introduced20, &rect2.get_yMax(), &&(this.Quads[num2]).v2.y);
            vh.AddVert(vertex);
            float introduced21 = &rect.get_xMin();
            &&vertex.position.x = Mathf.Lerp(introduced21, &rect.get_xMax(), &&(this.Quads[num2]).v3.x);
            float introduced22 = &rect.get_yMin();
            &&vertex.position.y = Mathf.Lerp(introduced22, &rect.get_yMax(), &&(this.Quads[num2]).v3.y);
            &vertex.color = &(this.Quads[num2]).c3;
            float introduced23 = &rect2.get_xMin();
            &&vertex.uv0.x = Mathf.Lerp(introduced23, &rect2.get_xMax(), &&(this.Quads[num2]).v3.x);
            float introduced24 = &rect2.get_yMin();
            &&vertex.uv0.y = Mathf.Lerp(introduced24, &rect2.get_yMax(), &&(this.Quads[num2]).v3.y);
            vh.AddVert(vertex);
            vh.AddTriangle(num, num + 1, num + 2);
            vh.AddTriangle(num + 2, num + 3, num);
            num += 4;
            num2 -= 1;
        Label_04CA:
            if (num2 >= 0)
            {
                goto Label_00BE;
            }
            goto Label_0BB8;
        Label_04D7:
            num3 = ((int) this.Quads.Length) - 1;
            goto Label_0BB0;
        Label_04E8:
            float introduced25 = &rect.get_xMin();
            &&vertex.position.x = Mathf.Lerp(introduced25, &rect.get_xMax(), &&(this.Quads[num3]).v0.x);
            float introduced26 = &rect.get_yMin();
            &&vertex.position.y = Mathf.Lerp(introduced26, &rect.get_yMax(), &&(this.Quads[num3]).v0.y);
            &&vertex.color.r = (byte) ((&&(this.Quads[num3]).c0.r * &color.r) / 0xff);
            &&vertex.color.g = (byte) ((&&(this.Quads[num3]).c0.g * &color.g) / 0xff);
            &&vertex.color.b = (byte) ((&&(this.Quads[num3]).c0.b * &color.b) / 0xff);
            &&vertex.color.a = (byte) ((&&(this.Quads[num3]).c0.a * &color.a) / 0xff);
            float introduced27 = &rect2.get_xMin();
            &&vertex.uv0.x = Mathf.Lerp(introduced27, &rect2.get_xMax(), &&(this.Quads[num3]).v0.x);
            float introduced28 = &rect2.get_yMin();
            &&vertex.uv0.y = Mathf.Lerp(introduced28, &rect2.get_yMax(), &&(this.Quads[num3]).v0.y);
            vh.AddVert(vertex);
            float introduced29 = &rect.get_xMin();
            &&vertex.position.x = Mathf.Lerp(introduced29, &rect.get_xMax(), &&(this.Quads[num3]).v1.x);
            float introduced30 = &rect.get_yMin();
            &&vertex.position.y = Mathf.Lerp(introduced30, &rect.get_yMax(), &&(this.Quads[num3]).v1.y);
            &&vertex.color.r = (byte) ((&&(this.Quads[num3]).c1.r * &color.r) / 0xff);
            &&vertex.color.g = (byte) ((&&(this.Quads[num3]).c1.g * &color.g) / 0xff);
            &&vertex.color.b = (byte) ((&&(this.Quads[num3]).c1.b * &color.b) / 0xff);
            &&vertex.color.a = (byte) ((&&(this.Quads[num3]).c1.a * &color.a) / 0xff);
            float introduced31 = &rect2.get_xMin();
            &&vertex.uv0.x = Mathf.Lerp(introduced31, &rect2.get_xMax(), &&(this.Quads[num3]).v1.x);
            float introduced32 = &rect2.get_yMin();
            &&vertex.uv0.y = Mathf.Lerp(introduced32, &rect2.get_yMax(), &&(this.Quads[num3]).v1.y);
            vh.AddVert(vertex);
            float introduced33 = &rect.get_xMin();
            &&vertex.position.x = Mathf.Lerp(introduced33, &rect.get_xMax(), &&(this.Quads[num3]).v2.x);
            float introduced34 = &rect.get_yMin();
            &&vertex.position.y = Mathf.Lerp(introduced34, &rect.get_yMax(), &&(this.Quads[num3]).v2.y);
            &&vertex.color.r = (byte) ((&&(this.Quads[num3]).c2.r * &color.r) / 0xff);
            &&vertex.color.g = (byte) ((&&(this.Quads[num3]).c2.g * &color.g) / 0xff);
            &&vertex.color.b = (byte) ((&&(this.Quads[num3]).c2.b * &color.b) / 0xff);
            &&vertex.color.a = (byte) ((&&(this.Quads[num3]).c2.a * &color.a) / 0xff);
            float introduced35 = &rect2.get_xMin();
            &&vertex.uv0.x = Mathf.Lerp(introduced35, &rect2.get_xMax(), &&(this.Quads[num3]).v2.x);
            float introduced36 = &rect2.get_yMin();
            &&vertex.uv0.y = Mathf.Lerp(introduced36, &rect2.get_yMax(), &&(this.Quads[num3]).v2.y);
            vh.AddVert(vertex);
            float introduced37 = &rect.get_xMin();
            &&vertex.position.x = Mathf.Lerp(introduced37, &rect.get_xMax(), &&(this.Quads[num3]).v3.x);
            float introduced38 = &rect.get_yMin();
            &&vertex.position.y = Mathf.Lerp(introduced38, &rect.get_yMax(), &&(this.Quads[num3]).v3.y);
            &&vertex.color.r = (byte) ((&&(this.Quads[num3]).c3.r * &color.r) / 0xff);
            &&vertex.color.g = (byte) ((&&(this.Quads[num3]).c3.g * &color.g) / 0xff);
            &&vertex.color.b = (byte) ((&&(this.Quads[num3]).c3.b * &color.b) / 0xff);
            &&vertex.color.a = (byte) ((&&(this.Quads[num3]).c3.a * &color.a) / 0xff);
            float introduced39 = &rect2.get_xMin();
            &&vertex.uv0.x = Mathf.Lerp(introduced39, &rect2.get_xMax(), &&(this.Quads[num3]).v3.x);
            float introduced40 = &rect2.get_yMin();
            &&vertex.uv0.y = Mathf.Lerp(introduced40, &rect2.get_yMax(), &&(this.Quads[num3]).v3.y);
            vh.AddVert(vertex);
            vh.AddTriangle(num, num + 1, num + 2);
            vh.AddTriangle(num + 2, num + 3, num);
            num += 4;
            num3 -= 1;
        Label_0BB0:
            if (num3 >= 0)
            {
                goto Label_04E8;
            }
        Label_0BB8:
            return;
        }
    }
}

