using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NurseryApplication1.Models
{
    public class Caretaker
    {
        [Key]
        public int CaretakerId { get; set; }
        public string CaretakerLastName { get; set; }
        public string CaretakerFirstName { get; set; }
        public ICollection<Tree> Trees { get; set; }
    }

    public class CaretakerDto
    {
        public int CaretakerId { get; set; }
        public string CaretakerFirstName { get; set; }
        public string CaretakerLastName { get; set; }




    }
}