using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderWorkflow.Domain
{
    public class CmsFactory
    {
        public Cms GetCms(int cmsId)
        {
            switch (cmsId)
            {
                case 5:
                    return null;
            }

            return null;
        }
    }
}
