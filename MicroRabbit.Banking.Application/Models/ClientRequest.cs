using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MicroRabbit.Banking.Application.Models
{
    
    public class ClientRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Password { get; set; }
        public string Mail { get; set; }
        public string Phone { get; set; }
    }
}
