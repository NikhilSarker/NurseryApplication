using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NurseryApplication1.Models.ViewModels
{
    public class DetailsTree
    {
        public TreeDto SelectedTree { get; set; }

        public IEnumerable<CaretakerDto> ResponsibleCaretakers { get; set; }
        public IEnumerable<CaretakerDto> AvailableCaretakers { get; set; }
    }
}