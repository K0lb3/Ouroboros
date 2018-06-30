namespace SRPG
{
    using System;

    public class OptionData
    {
        private VolumeData mVolume;

        public OptionData()
        {
            this.mVolume = new VolumeData();
            base..ctor();
            return;
        }

        public VolumeData Volume
        {
            get
            {
                return this.mVolume;
            }
        }
    }
}

