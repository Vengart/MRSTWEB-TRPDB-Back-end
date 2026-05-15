using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orichalcum.DataAccess
{
    public class DbSession
    {
        public static string ConnectionStrings { get; set; } =
            "Server=DAMNANOTHERLENO;Database=OrichalcumDb;Trusted_Connection=True;TrustServerCertificate=True;";
    }
}