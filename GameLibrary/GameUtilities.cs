using System.Globalization;
using System.Text;

namespace GameLibrary
{
    public static class GameUtilities
    {  
        private static string RemoveAccents(this string url)
        {
            string normalizedString = url.Normalize(NormalizationForm.FormD);
            string urlWithoutAccents = string.Empty;

            foreach (char c in normalizedString)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                {   

                    urlWithoutAccents += c;
                }
            }

            return urlWithoutAccents;
        }

        public static string StringTransform(this string url)
        {
            //if (name is null) throw new ArgumentNullException(nameof(name));
            ArgumentNullException.ThrowIfNull(url); //uguale a sopra

            return url.Trim().Trim(['-', '_']).Replace(' ', '-').Replace('_', '-').ToUpper().RemoveAccents().ReplaceChar();
        }

        private static string ReplaceChar(this string url)
        {
            string gameID = string.Empty;
            char temp = '-'; // creo un char temporaneo, il - è come viene inizializato temp
            foreach (char c in url)
            {
                if ((char.IsLetterOrDigit(c) || c == '&') || (c == '-' && c != temp))//se c è nei caratteri consentiti, o è = a & passa, poi se è = a - e temp è diverso da c passa
                {
                    gameID += c;
                    temp = c;//ogni volta che il ciclo passa i controlli riscrive temp cosi da avere sempre il carattere passato prima
                }

            }
            return gameID;
        }
    }
}
