using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AttendanceCheck.Tables
{
    class Lectures
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string LectureName { get; set; }
        public string Account_id { get; set; }
        public string Datetime { get; set; }
        public string Duration { get; set; }
        public string Room { get; set; }
    }
}
