﻿using PluginCommon;
using System.ComponentModel.Composition;
using System.Windows.Controls;

namespace PluginTxt
{
    [Export(typeof(IView)), PartCreationPolicy(CreationPolicy.Any)]
    [ExportMetadata("Name", "TXT Plugin")]
    public partial class SubstringSearchControl : UserControl, IView
    {
        public SubstringSearchControl()
        {
            InitializeComponent();
        }

        public string SpecialAttribute
        {
            get
            {
               return textBoxSubstring.Text;
            }

            set
            {
                textBoxSubstring.Text=value;
            }
        }
    }
}
