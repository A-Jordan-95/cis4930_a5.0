using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CS_ABET.Controllers
{
    public class BaseEC
    {
        internal string ConnectionString { get
            {
                return "Server=.;Database=ABET_DB;Trusted_Connection=True;";
            } }
    }
}