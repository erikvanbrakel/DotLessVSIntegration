using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.Package;
using Microsoft.VisualStudio.TextManager.Interop;

namespace LessProject.DotLessIntegration
{
    [Guid("2A870B66-0DFA-4470-A873-048FB220B4DF")]
    public class DotLessLanguage : LanguageService
    {
        private LanguagePreferences preferences;
        private DotLessScanner scanner;
        private DotLessColarableItem[] colorableItems;

        /// <summary>
        /// Returns a <see cref="T:Microsoft.VisualStudio.Package.LanguagePreferences"/> object for this language service.
        /// </summary>
        /// <returns>
        /// If successful, returns a <see cref="T:Microsoft.VisualStudio.Package.LanguagePreferences"/> object; otherwise, returns a null value.
        /// </returns>
        public override LanguagePreferences GetLanguagePreferences()
        {
            if (preferences == null)
            {
                preferences = new LanguagePreferences(this.Site, typeof(DotLessLanguage).GUID, this.Name);
                preferences.Init();
            }
            return preferences;
        }

        /// <summary>
        /// Returns a single instantiation of a parser.
        /// </summary>
        /// <returns>
        /// If successful, returns an <see cref="T:Microsoft.VisualStudio.Package.IScanner"/> object; otherwise, returns a null value.
        /// </returns>
        /// <param name="buffer">[in] An <see cref="T:Microsoft.VisualStudio.TextManager.Interop.IVsTextLines"/> representing the lines of source to parse.
        ///                 </param>
        public override IScanner GetScanner(IVsTextLines buffer)
        {
            if (scanner == null)
            {
                scanner = new DotLessScanner();
            }
            return scanner;
        }

        /// <summary>
        /// Parses the source based on the specified <see cref="T:Microsoft.VisualStudio.Package.ParseRequest"/> object.
        /// </summary>
        /// <returns>
        /// If successful, returns an <see cref="T:Microsoft.VisualStudio.Package.AuthoringScope"/> object; otherwise, returns a null value.
        /// </returns>
        /// <param name="req">[in] The <see cref="T:Microsoft.VisualStudio.Package.ParseRequest"/> describing how to parse the source file.
        ///                 </param>
        public override AuthoringScope ParseSource(ParseRequest req)
        {
            if (null == req)
            {
                throw new ArgumentNullException("req");
            }
            return null;
        }

        public override void OnIdle(bool periodic)
        {
            Source src = GetSource(LastActiveTextView);
            if (src != null && src.LastParseTime == Int32.MaxValue)
            {
                src.LastParseTime = 0;
            }
            base.OnIdle(periodic);
        }

        public override int GetItemCount(out int count)
        {
            count = colorableItems.Length;
            return Microsoft.VisualStudio.VSConstants.S_OK;
        }

        public override int GetColorableItem(int index, out IVsColorableItem item)
        {
            if (index < 1)
            {
                throw new ArgumentOutOfRangeException("index");
            }
            item = colorableItems[index - 1];
            return Microsoft.VisualStudio.VSConstants.S_OK;
        }

        public override string Name
        {
            get { return "DotLess"; }
        }
    }
}