using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NurseryApplication1.Models.ViewModels
{
    public class DetailsCaretaker
    {
        public CaretakerDto SelectedCaretaker { get; set; }

        public IEnumerable<TreeDto> KeptTrees { get; set; } 

    }
}