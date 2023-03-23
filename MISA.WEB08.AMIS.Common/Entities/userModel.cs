using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB08.AMIS.Common.Entities
{
    public class userModel
    {
        public string? id { get; set; }
        public string account { get; set; }
        public string password { get; set; }
        public string role { get; set; }
        public string? created_at { get; set; }
    }
}
