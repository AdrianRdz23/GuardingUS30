using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuardingUS30.Models
{
    public class VisitorsHomeModel
    {
        //Create a visitor Model
        public VisitorModel visitors { get; set; }

        //Create a home Model
        public HomeModel home { get; set; }

        public List<String[]> homeModelInfo { get; set; }

        public VisitorsHomeModel(VisitorModel visitors, List<String[]> homeModelInfo)
        {
            this.visitors = visitors;
            this.homeModelInfo = homeModelInfo;
        }

        public VisitorsHomeModel()
        {

        }
    }
}
