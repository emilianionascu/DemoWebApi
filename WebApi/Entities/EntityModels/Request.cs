using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.EntityModels
{
    public class Request
    {
        public int Id { get; set; }
        public int Index { get; set; }
        public string Name { get; set; }
        public int? Visits { get; set; }
        public DateTime Date { get; set; }
    }
}
