using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AttendanceCheck.Tables
{
    class Accounts
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string username { get; set; }
        public string fname { get; set; }
        public string lname { get; set; }
        public string password { get; set; }
        public string type { get; set; }
    }
}
