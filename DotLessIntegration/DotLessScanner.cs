using Microsoft.VisualStudio.Package;

namespace LessProject.DotLessIntegration
{
    public class DotLessScanner : IScanner
    {
        private string src;
        public void SetSource(string source, int offset)
        {
            src = source.Substring(offset);
        }

        public bool ScanTokenAndProvideInfoAboutIt(TokenInfo tokenInfo, ref int state)
        {
            if (tokenInfo.EndIndex == src.Length) return false;
            tokenInfo.Color = TokenColor.Keyword;
            tokenInfo.Type = TokenType.Keyword;
            tokenInfo.Trigger = TokenTriggers.None;
            tokenInfo.EndIndex = src.Length;
            return true;
        }
    }
}