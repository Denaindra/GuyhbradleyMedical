using System.Drawing;
using Microsoft.Maui.Graphics;
using Color = Microsoft.Maui.Graphics.Color;

namespace MauiDotNET8.Helpers
{
    public static class AccessResourceDictionary
    {
        public static Color GetResourcesValue(string key)
        {
            if (Application.Current.Resources.TryGetValue("Primary", out var colorVal)) { }
            return (Color)colorVal;
        }
    }
}
