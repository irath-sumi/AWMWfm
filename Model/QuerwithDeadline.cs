using AWM.Core.Models;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AWMWfm.Model
{
    public class QuerwithDeadline:Concern
    {
        public DateTime DeadLine { get; set; }
    }
}
