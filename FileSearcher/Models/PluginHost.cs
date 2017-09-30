using PluginCommon;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace FileSearcher.Models
{
    public class PluginHost
    {
        public PluginHost(ContentControl contentControl, ComboBox comboBox)
        {
            _comboBox = comboBox;
            _contentControl = contentControl;
        }

        private ComboBox _comboBox;
        private ContentControl _contentControl;

        [ImportMany(typeof(PluginCommon.IView), AllowRecomposition = true)]
        public IEnumerable<Lazy<PluginCommon.IView, IPluginMetadata>> Plugins;

        [ImportMany(typeof(IPlugin), AllowRecomposition = true)]
        public IEnumerable<Lazy<IPlugin>> SearchMethod;

        public void RefreshPlugins(ObservableCollection<string> PluginNames, ObservableCollection<IView> pluginControls, List<IPlugin> methods)
        {
            if (PluginNames != null)
            {
                PluginNames.Clear();
            }
            if(pluginControls!=null)
            {
                pluginControls.Clear();
            }
            if(methods!=null)
            {
                methods.Clear();
            }
            try
            {
                AggregateCatalog catalog = new AggregateCatalog();
                string pluginsPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                pluginsPath = Path.Combine(pluginsPath, "plugins");
                if (!Directory.Exists(pluginsPath))
                    Directory.CreateDirectory(pluginsPath);
                catalog.Catalogs.Add(new DirectoryCatalog(pluginsPath, "Plugin*.dll"));
                CompositionContainer container = new CompositionContainer(catalog);
                container.ComposeParts(this);

                foreach (var pluginControl in Plugins)
                {
                    pluginControls.Add(pluginControl.Value);
                    PluginNames.Add(pluginControl.Metadata.Name);
                }

                foreach (var method in SearchMethod)
                {
                    methods.Add(method.Value);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Plugin isn't valid. Error: " + ex.Message, "", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        public IView ChangePlugins(ObservableCollection<IView> pluginControls,ref int pluginIndex)
        {
            pluginIndex = 0;
            if (pluginControls.Count != 0)
            {
                if (_comboBox.SelectedIndex != -1)
                {
                    pluginIndex = _comboBox.SelectedIndex;
                }
                var pluginView = pluginControls[pluginIndex];
                _contentControl.Content = pluginView;
                return pluginView;
            }
            return null;
        }
    }
}
