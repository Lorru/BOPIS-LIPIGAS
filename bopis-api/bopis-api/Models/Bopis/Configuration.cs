using System;
using System.Collections.Generic;

namespace bopis_api.Models.Bopis
{
    public partial class Configuration
    {
        public long Id { get; set; }
        public string Description { get; set; }
        public string Value { get; set; }
        public string Key { get; set; }
        public string DataType { get; set; }
        public bool ReadOnly { get; set; }
        public bool Status { get; set; }
    }
}
