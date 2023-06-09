
using System.Windows;

namespace photoAlbum
{
    public class HelpButton : CaptionButton
    {
        static HelpButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(HelpButton), new FrameworkPropertyMetadata(typeof(HelpButton)));
        }
    }
}
