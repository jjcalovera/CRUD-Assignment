using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_Assignment.Components
{
    class Connection
    {
        Components.Value val = new Components.Value();

        public string conString()
        {
            return String.Format("datasource = {0}; username = {1}; password = {2}; port = {3}; database = {4};",
                                    val.serverName, val.serverUser, val.serverPass, val.port, val.database);
        }
    }
}
