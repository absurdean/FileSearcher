using PluginCommon;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Windows.Controls;

namespace PluginMp3
{
    [Export(typeof(IView)), PartCreationPolicy(CreationPolicy.Any)]
    [ExportMetadata("Name", "MP3 Plugin")]
    public partial class MP3SearchControl : UserControl, IView
    {
        public MP3SearchControl()
        {
            InitializeComponent();
        }
        public string SpecialAttribute
        {
            get
            {
                return textBoxMP3Header.Text;
            }

            set
            {
                textBoxMP3Header.Text = value;
            }
        }
    
}
}
