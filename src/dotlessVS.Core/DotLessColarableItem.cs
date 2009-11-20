using System;
using Microsoft.VisualStudio.TextManager.Interop;

namespace LessProject.DotLessIntegration
{
    internal class DotLessColarableItem : IVsColorableItem
    {
        public int GetDefaultColors(COLORINDEX[] piForeground, COLORINDEX[] piBackground)
        {
            throw new NotImplementedException();
        }

        public int GetDefaultFontFlags(out uint pdwFontFlags)
        {
            throw new NotImplementedException();
        }

        public int GetDisplayName(out string pbstrName)
        {
            throw new NotImplementedException();
        }
    }
}