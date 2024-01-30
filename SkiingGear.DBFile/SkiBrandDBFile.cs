﻿using MularczykMrowczynski.SkiingGear.Interfaces;

namespace MularczykMrowczynski.SkiingGear.SkiingGear.DBFile
{
    [Serializable]
    internal class SkiBrandDBFile: ISkiBrand
    {
        
        public int BrandId { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public int FoundationYear { get; set; }
    }
}
