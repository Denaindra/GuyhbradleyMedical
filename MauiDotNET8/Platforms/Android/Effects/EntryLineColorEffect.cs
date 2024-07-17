using Android.Graphics;
using Android.Widget;
using MauiDotNET8.Behavior;
using MauiDotNET8.Platforms.Android.Effects;
using Microsoft.Maui.Controls.Compatibility.Platform.Android;
using Microsoft.Maui.Controls.Platform;
using System.ComponentModel;


[assembly: ResolutionGroupName("HamptonMedical")]
[assembly: ExportEffect(typeof(EntryLineColorEffect), "EntryLineColorEffect")]
namespace MauiDotNET8.Platforms.Android.Effects
{
    public class EntryLineColorEffect : PlatformEffect
    {
        EditText control;
        protected override void OnAttached()
        {
            try
            {
                control = Control as EditText;
                UpdateLineColor();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Cannot set property on attached control. Error: ", ex.Message);
            }
        }

        protected override void OnDetached()
        {
            control = null;
        }

        protected override void OnElementPropertyChanged(PropertyChangedEventArgs args)
        {
            if (args.PropertyName == LineColorBehavior.LineColorProperty.PropertyName)
            {
                UpdateLineColor();
            }
        }

        private void UpdateLineColor()
        {
            try
            {
                if (control != null)
                {
                    control.Background.SetColorFilter(LineColorBehavior.GetLineColor(Element).ToAndroid(), PorterDuff.Mode.SrcAtop);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
