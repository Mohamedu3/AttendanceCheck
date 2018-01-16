using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AttendanceCheck.Tables
{
    class Students
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Acadimic_id { get; set; }
        public string name { get; set; }
        public string FingerPrint { get; set; }
    }
}
