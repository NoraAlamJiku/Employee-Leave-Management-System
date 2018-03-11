using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LMSApp.Gateway;
using LMSApp.Models;

namespace LMSApp.Manager
{
    public class ValidationManager
    {
        ValidationGateway vGateway = new ValidationGateway();

        public bool IsLeaveTaken(LeaveTaken leaveTaken)
        {
            return vGateway.IsLeaveTaken(leaveTaken);
        }

    }
}