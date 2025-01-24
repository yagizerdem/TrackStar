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
    /// Interaction logic for SearchPanel.xaml
    /// </summary>
    public partial class SearchPanel : UserControl
    {
        public static readonly DependencyProperty ViewModelProperty =
   DependencyProperty.Register(
       "ViewModel",
       typeof(SearchViewModel),
       typeof(SearchPanel),
       new PropertyMetadata(null));
        public SearchViewModel ViewModel
        {
            get => (SearchViewModel)GetValue(ViewModelProperty);
            set => SetValue(ViewModelProperty, value);
        }

        public SearchPanel()
        {
            InitializeComponent();
        }
    }
}
