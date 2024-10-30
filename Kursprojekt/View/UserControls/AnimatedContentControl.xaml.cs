using MahApps.Metro.Controls;
using System.ComponentModel;
using System.Windows.Controls;

namespace Kursprojekt.View.UserControls
{
    /// <summary>
    /// Interaktionslogik für AnimatedContentControl.xaml
    /// </summary>
    public partial class AnimatedContentControl : UserControl
    {
        public AnimatedContentControl()
        {
            InitializeComponent();

            //Alternative zu Animation
            //DependencyPropertyDescriptor.FromProperty(ContentProperty, typeof(ContentControlEx)).AddValueChanged(PagePlace, ContentChanged);
        }

        //Alternative zu Animation
        //private void ContentChanged(object? sender, EventArgs e)
        //{
        //    MetroTabItem.IsSelected = false;
        //    MetroTabItem.IsSelected = true;
        //}

        public void ShowPage(UserControl ucPage)
        {
            PagePlace.Content = null;
            PagePlace.Content = ucPage;

            MetroTabItem.IsSelected = false;
            MetroTabItem.IsSelected = true;
        }
    }
}
