using System;
using System.Collections.Generic;

namespace BusBoard.Postcode
{
    public class PostcodeInfo
    {
        public List<LocationInfo> result { get; set; }
        
    }
    
    public class LocationInfo
    {
        public decimal longitude { get; set; }
        public decimal latitude { get; set; }

        public override string ToString()
        {
            return $"{latitude}, {longitude}";
        }
    }
   
}