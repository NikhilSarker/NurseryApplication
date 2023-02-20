using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NurseryApplication1.Models
{
    public class Tree
    {
        [Key]
        public int TreeId { get; set; }
        public string TreeName { get; set; }
        //Height is in inches
        public int TreeHeight { get; set; }

        //A tree belongs to one category
        //A category can have many trees
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public ICollection<Caretaker> Caretakers { get; set; }
    }


    public class TreeDto
    {
        public int TreeId { get; set; }
        public string TreeName { get; set; }

        //Height is in inches
        public int TreeHeight { get; set; }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}