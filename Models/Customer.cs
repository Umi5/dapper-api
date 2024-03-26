using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dapper_api.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;   
    }
}