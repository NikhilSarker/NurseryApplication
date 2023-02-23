using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NurseryApplication1.Models.ViewModels
{
    public class DetailsCategory
    {
        public CategoryDto SelectedCategory { get; set; }

        public IEnumerable<TreeDto> RelatedTrees { get; set; }  
    }
}