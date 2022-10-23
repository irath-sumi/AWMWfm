using System;
using System.Collections.Generic;
using System.Text;

namespace AWM.Core.Models
{
    public class Concern
    {
        public string ID { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string QueryText { get; set; }
        public string TypeId { get; set; }
        public string Priority { get; set; }
        public Boolean IsAssigned { get; set; }
        public string AssginedTo { get; set; }

    }
}
