using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AttendanceCheck.Tables
{
    class StudentsInLec
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Lec_id { get; set; }
        public string Student_id { get; set; }
        public string Acadimic_id { get; set; }
        public string Student_name { get; set; }
        public string FingerPrintId { get; set; }
    }
}
