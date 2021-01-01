using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace boyut.TerminalWebServis.ResourceAccess.Entities
{
    public class OraContext:DbContext
    {
        public OraContext() : base("NRVSContext")
        {

        }
    }
}