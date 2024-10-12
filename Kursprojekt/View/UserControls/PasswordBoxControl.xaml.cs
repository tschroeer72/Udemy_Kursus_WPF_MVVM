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

namespace Kursprojekt.View.UserControls;

/// <summary>
/// Interaktionslogik für PasswordBoxControl.xaml
/// </summary>
public partial class PasswordBoxControl : UserControl
{
    public PasswordBoxControl()
    {
        InitializeComponent();
    }

    bool IsPasswordTextChangingFromGui;

    public string PasswordText
    {
        get { return (string)GetValue(PasswordTextProperty); }
        set { SetValue(PasswordTextProperty, value); }
    }

    public static readonly DependencyProperty PasswordTextProperty =
        DependencyProperty.Register(nameof(PasswordText), typeof(string), typeof(PasswordBoxControl),
            new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, 
                PropertyChangedCallBack, null, false, UpdateSourceTrigger.PropertyChanged));

    private static void PropertyChangedCallBack(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is PasswordBoxControl ctlPasswordBox)
        {
            ctlPasswordBox.UpdatePassword();
        }
    }

    public void UpdatePassword()
    {
        if (IsPasswordTextChangingFromGui) return;

        PasswordBox.Password = PasswordText;
    }

    private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
    {
        IsPasswordTextChangingFromGui = true;
        PasswordText = PasswordBox.Password;
        IsPasswordTextChangingFromGui = false;
    }

    //HintText
    public string HintText
    {
        get { return (string)GetValue(HintTextProperty); }
        set { SetValue(HintTextProperty, value); }
    }

    // Using a DependencyProperty as the backing store for HintText.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty HintTextProperty =
        DependencyProperty.Register(nameof(HintText), typeof(string), typeof(PasswordBoxControl), new PropertyMetadata(string.Empty));
}