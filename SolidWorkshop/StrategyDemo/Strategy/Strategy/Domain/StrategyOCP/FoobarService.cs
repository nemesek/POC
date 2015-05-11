using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strategy.Domain.StrategyOCP
{
    public class FoobarService
    {
        public string GetAction(int cmsId)
        {
            if (cmsId == 1)
            {
                return string.Format("I am doing foo for {0}", cmsId);
            }
            else
            {
                return string.Format("I am doing bar for {0}", cmsId);
            }
        }

        //public virtual string GetAction(int cmsId)
        //{
        //    if (cmsId == 1)
        //    {
        //        return string.Format("I am doing foo for {0}", cmsId);
        //    }
        //    else
        //    {
        //        return string.Format("I am doing bar for {0}", cmsId);
        //    }
        //}
    }
}
