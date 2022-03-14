using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuardingUS30.Models
{
    public class UserHomeModel
    {
        public UserModel userModel { get; set; }

        public HomeModel homeModel { get; set; }

        public List<String[]> homeModelNames { get; set; }

        public UserHomeModel(HomeModel homeModel, List<String[]> homeModelNames)
        {
            this.homeModel = homeModel;
            this.homeModelNames = homeModelNames;
        }

        public UserHomeModel()
        {

        }
    }
}
