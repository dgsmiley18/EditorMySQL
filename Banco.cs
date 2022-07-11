using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySQLcrud;

namespace MySQLcrud
{
    public class Banco
    {
        public string Server { get; set; }
        public string Port { get; set; }
        public string Database { get; set; }
        public string Uid { get; set; }
        public string Pwd { get; set; }
    }
}
