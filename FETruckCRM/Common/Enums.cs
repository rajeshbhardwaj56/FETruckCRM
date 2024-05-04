using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FETruckCRM.Common
{
    public enum PaymentTypeEnum
    {
        PREPAID = 1,
        POSTPAID = 2
    }
    public enum RolesEnum
    {
        TeamManager = 1,
        TeamLead = 2,
        TeamMember = 3,
    }


    public enum UserRolesEnum
    {
        SuperAdmin = 1,
        Administrator = 2,
        SaleRepresentative = 3,
        Dispatcher = 4,
        ComplianceAuditor = 1002,
        Accounting = 1003,
        Operations = 1004,

    }
}