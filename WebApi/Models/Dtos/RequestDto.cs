using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Dtos
{
    public class RequestDto
    {
        public int Index { get; set; }
        public string Name { get; set; }
        public int? Visits { get; set; }
        public DateTime Date { get; set; }
    }
}
