using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace PlantDelightPortal.Classes
{
    public class clsSignUp
    {
        string ConString = ConfigurationManager.ConnectionStrings["PlantDelightPortalDB"].ConnectionString;

    }
}