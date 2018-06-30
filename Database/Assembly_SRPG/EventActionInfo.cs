namespace SRPG
{
    using System;
    using System.Runtime.InteropServices;
    using UnityEngine;

    public class EventActionInfo : Attribute
    {
        public string Name;
        public string Description;
        public Color32 DefaultColor;
        public Color32 FocusColor;

        public EventActionInfo(string name, string description, int defaultColor, int focusColor)
        {
            base..ctor();
            this.Name = name;
            this.Description = description;
            this.DefaultColor = new Color32((byte) ((defaultColor >> 0x10) & 0xff), (byte) ((defaultColor >> 8) & 0xff), (byte) (defaultColor & 0xff), 0xff);
            this.FocusColor = new Color32((byte) ((focusColor >> 0x10) & 0xff), (byte) ((focusColor >> 8) & 0xff), (byte) (focusColor & 0xff), 0xff);
            return;
        }
    }
}

