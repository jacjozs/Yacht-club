using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yacht_club.Moduls
{
    /// <summary>
    /// Használt projekt
    /// https://www.codeproject.com/Articles/26535/WPF-Autocomplete-Textbox-Control
    /// </summary>
    public class AutoCompleteEntry
    {
        private string[] keywordStrings;
        public string DisplayName;
        public int Id;

        public string[] KeywordStrings
        {
            get
            {
                if (keywordStrings == null)
                {
                    keywordStrings = new string[] { DisplayName };
                }
                return keywordStrings;
            }
        }

        public AutoCompleteEntry(int id, string name, params string[] keywords)
        {
            Id = id;
            DisplayName = name;
            keywordStrings = keywords;
        }

        public override string ToString()
        {
            return DisplayName;
        }
    }
}
