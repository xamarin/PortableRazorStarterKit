using System;
using System.Collections.Generic;

namespace PortableCongress
{
    public class Bill
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int PoliticianId { get; set; }
        public string ThomasLink { get; set; }
    }
}