using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AutoCompleteTextBox.Editors;

namespace ShortCuts.Core.Helpers
{
    public class ShortCutSuggetionProvider : ISuggestionProvider
    {
        #region Constructor
        public ShortCutSuggetionProvider() { }

        #endregion

        #region Public Properties
        public IEnumerable<DAL.Models.ShortCutModel> ShortCutList { get; set; }
        public bool IsWinShortCut { get; set; }
        #endregion

        #region Public Methods
        public IEnumerable GetSuggestions(string filter)
        {
            var resultShortCut = new List<DAL.Models.ShortCutModel>();
            try
            {
                if (string.IsNullOrEmpty(filter)) return null;
                if (IsWinShortCut)
                {
                    if (ShortCutList?.FirstOrDefault()?.WinKey.Count() > 0)
                    {
                        resultShortCut = ShortCutList?.Where(x => x.WinKey.ToLower().Contains(filter.ToLower())).OrderBy(x => x.WinKey).ToList();
                    }
                    return resultShortCut;
                }
                else
                {
                    if (ShortCutList?.FirstOrDefault()?.MacKey.Count() > 0)
                    {
                        resultShortCut = ShortCutList?.Where(x => x.MacKey.ToLower().Contains(filter.ToLower())).OrderBy(x => x.MacKey).ToList();
                    }
                    return resultShortCut;
                }

            }
            catch (Exception ex)
            {
                ex.ToString();
            }

            return resultShortCut;
        }
        #endregion
    }
}
