using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NurseryApplication1.Models.ViewModels
{
    public class UpdateTree
    {
       public TreeDto SelectedTree { get; set; }

       public IEnumerable<CategoryDto> CategoriesOptions { get; set; }
    }
}