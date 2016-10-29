using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FooMVCAppModern.DTO
{
    public class FooDTO
    {
        public int Id { get; set; }
        public DateTime? LastUpdate { get; set; }
        public bool Active { get; set; }
        public bool OtherFlag { get; set; }
        public int FKFirstNotNull { get; set; }
        public int? FKSecondNull { get; set; }
        public int? FKThridDefaultValue { get; set; }
        public string Labels { get; set; }
    }
}