using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TrackStar.MVVM.ViewModel;

namespace TrackStar.MVVM.View
{
    /// <summary>
    /// Interaction logic for SettingsPanel.xaml
    /// </summary>
    public partial class SettingsPanel : UserControl
    {
        public static readonly DependencyProperty ViewModelProperty =
DependencyProperty.Register(
"ViewModel",
typeof(SettingsViewModel),
typeof(SettingsPanel),
new PropertyMetadata(null));
        public SettingsViewModel ViewModel
        {
            get => (SettingsViewModel)GetValue(ViewModelProperty);
            set => SetValue(ViewModelProperty, value);
        }


        public SettingsPanel()
        {
            InitializeComponent();
        }
    }
}
