using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace AracKiralamaUygulaması
{
    internal class VeriTabaniBaglantisi
    {
       public SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-4DI2K7S\SQLEXPRESS;Initial Catalog=DbAracKiralama;Integrated Security=True");

    }

}
