using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.Dtos.Dtos.FeatureDtos
{
    public class CreateFeatureDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
    }
}
