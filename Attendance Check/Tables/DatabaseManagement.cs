using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml.Controls;

namespace AttendanceCheck.Tables
{
    class DatabaseManagement
    {
        public static async void CreateDatabase()
        {
            var Account = await ConnectionDb().CreateTableAsync<Accounts>();
            var Lecture = await ConnectionDb().CreateTableAsync<Lectures>();
            var Student = await ConnectionDb().CreateTableAsync<Students>();
            var StudentInLec = await ConnectionDb().CreateTableAsync<StudentsInLec>();
        }
        public static SQLiteAsyncConnection ConnectionDb()
        {
            var conn = new SQLite.SQLiteAsyncConnection(Path.Combine(ApplicationData.Current.LocalFolder.Path, "O6uAttendance.db"), true);
            return conn;
        }
        //Account table Functions
        public async static Task SearchAdminUsername(TextBlock box2)
        {
            var _Accounts = new List<Accounts>();
            var query = ConnectionDb().Table<Accounts>();
            var result = await query.ToListAsync();
            
            if (result.Count() != 0)
            {
                box2.Text = "create";
            }
            else
            {
                box2.Text = "not created";
            }
        }
        public async static void InsertAccountData(string _username, string _fname, string _lname, string _password, string _type)
        {
            var newAccount = new Accounts
            {
                username = _username,
                fname = _fname,
                lname = _lname,
                password = _password,
                type = _type
            };

            await ConnectionDb().InsertAsync(newAccount);
        }
        public async static Task LoadAllDoctorsData(ListView list)
        {
            var account = new List<Accounts>();
            var query = ConnectionDb().Table<Accounts>();
            var result = await query.ToListAsync();

            foreach (var _account in result)
            {
                account.Add(new Accounts { Id = _account.Id, username = _account.username, fname = _account.fname, lname = _account.lname, password = _account.password, type = _account.type });
            }
            list.ItemsSource = account;
        }
        public async static Task LoadSpacificAccountData(ListView box)
        {
            var Accounts = new List<Accounts>();
            var query = ConnectionDb().Table<Accounts>();
            var result = await query.ToListAsync();

            foreach (var _Account in result)
            {
                if (_Account.username == gloablvalue.SearchAccountUsername)
                {
                    Accounts.Add(new Accounts { Id = _Account.Id, username = _Account.username, fname = _Account.fname, lname = _Account.lname, type = _Account.type, password = _Account.password });
                }
            }
            box.ItemsSource = Accounts;
        }
        public async static Task SearchUsername(TextBlock box2)
        {
            var _Accounts = new List<Accounts>();
            var query = ConnectionDb().Table<Accounts>();
            var result = await query.ToListAsync();

            foreach (var _Account in result)
            {
                if (_Account.username == gloablvalue.VaildationUsername)
                {
                    box2.Text = _Account.username;
                    break;
                }
                else
                {
                    box2.Text = "Not Found";
                }
            }
        }
        public async static Task SearchUsernamePassword(TextBlock UserStatus, TextBlock PasswordStatus, TextBlock IdStatus, TextBlock fnameStatus, TextBlock lnameStatus, TextBlock type)
        {
            var _Accounts = new List<Accounts>();
            var query = ConnectionDb().Table<Accounts>();
            var result = await query.ToListAsync();

            foreach (var _Account in result)
            {
                if ((_Account.username == gloablvalue.VaildationLoginUsername) && (_Account.password == gloablvalue.VaildationLoginPassword))
                {
                    IdStatus.Text = _Account.Id.ToString();
                    fnameStatus.Text = _Account.fname;
                    lnameStatus.Text = _Account.lname;
                    UserStatus.Text = _Account.username;
                    PasswordStatus.Text = _Account.password;
                    type.Text = _Account.type;
                    break;
                }
                else if ((_Account.username == gloablvalue.VaildationLoginUsername) && (_Account.password != gloablvalue.VaildationLoginPassword))
                {
                    UserStatus.Text = _Account.username;
                    PasswordStatus.Text = _Account.password;
                    break;
                }
            }
        }
        public async static Task SearchAccountusername(TextBlock box)
        {
            var _Accounts = new List<Accounts>();
            var query = ConnectionDb().Table<Accounts>();
            var result = await query.ToListAsync();

            foreach (var _Account in result)
            {
                if (_Account.username == gloablvalue.SearchAccountUsername)
                {
                    box.Text = _Account.username;
                    break;
                }
                else
                {
                    box.Text = "Not Found";
                }
            }
        }
        public async static void UpdateAllAccountData(string _username, string _fname, string _lname, string _Opassword, string _Npassword)
        {
            var UpdateAllAccount = await ConnectionDb().Table<Accounts>().Where(w => w.username.Equals(_username) && w.fname.Equals(_fname) && w.lname.Equals(_lname) && w.password.Equals(_Opassword)).FirstOrDefaultAsync();
            UpdateAllAccount.fname = _fname;
            UpdateAllAccount.lname = _lname;
            UpdateAllAccount.password = _Npassword;
            await ConnectionDb().UpdateAsync(UpdateAllAccount);
        }
        public async static void UpdateAccountData(string _username, string _password, string _newpassword)
        {
            var updateAccount = await ConnectionDb().Table<Accounts>().Where(w => w.username.Equals(_username) && w.password.Equals(_password)).FirstOrDefaultAsync();
            updateAccount.password = _newpassword;
            await ConnectionDb().UpdateAsync(updateAccount);
        }
        public async static void DeleteAccount(string _username)
        {
            var deleteAccount = await ConnectionDb().Table<Accounts>().Where(w => w.username.Equals(_username)).FirstOrDefaultAsync();
            deleteAccount.username = _username;
            await ConnectionDb().DeleteAsync(deleteAccount);
        }
        //Lectures table Functions
        public async static void InsertLectureData(string _lecname, string _Account_id, string _Datetime, string _Duration, string _Room)
        {
            var newLecture = new Lectures
            {
                LectureName = _lecname,
                Account_id = _Account_id,
                Datetime = _Datetime,
                Duration = _Duration,
                Room = _Room
            };

            await ConnectionDb().InsertAsync(newLecture);
        }
        public async static Task LoadAllLecturesData(GridView box)
        {
            var Lectures = new List<Lectures>();
            var query = ConnectionDb().Table<Lectures>();
            var result = await query.ToListAsync();

            foreach (var _Lectures in result)
            {
                if (_Lectures.Account_id == gloablvalue.CurrentId)
                {
                    Lectures.Add(new Lectures { Id = _Lectures.Id, LectureName = _Lectures.LectureName, Account_id = _Lectures.Account_id, Datetime = _Lectures.Datetime, Duration = _Lectures.Duration, Room = _Lectures.Room });
                }
            }
            box.ItemsSource = Lectures;
        }
        public async static void UpdateLectureData(string _lecname, string _Datetime, string _Room)
        {
            var updateLecture = await ConnectionDb().Table<Lectures>().Where(w => w.LectureName.Equals(_lecname) && w.Datetime.Equals(_Datetime) && w.Room.Equals(_Room)).FirstOrDefaultAsync();
            updateLecture.LectureName = _lecname;
            updateLecture.Datetime = _Datetime;
            updateLecture.Room = _Room;
            await ConnectionDb().UpdateAsync(updateLecture);
        }
        public async static void DeleteLecture(string _lecname)
        {
            var deleteLecture = await ConnectionDb().Table<Lectures>().Where(w => w.LectureName.Equals(_lecname)).FirstOrDefaultAsync();
            deleteLecture.LectureName = _lecname;
            await ConnectionDb().DeleteAsync(deleteLecture);
        }
        public async static Task CountAllLecturesData(TextBlock box)
        {
            var lectures = new List<Lectures>();
            var query = ConnectionDb().Table<Lectures>();
            var result = await query.ToListAsync();
            box.Text = (result.Count() + 1).ToString();
        }
        //Students table Functions
        public async static void InsertAllStudentsData(string _Acadimic_id, string _name, string _FingerPrint)
        {
            var newStudent = new Students
            {
                Acadimic_id = _Acadimic_id,
                name = _name,
                FingerPrint = _FingerPrint
            };

            await ConnectionDb().InsertAsync(newStudent);
        }
        public async static Task LoadAllStudentsData(ListView box)
        {
            var Students = new List<Students>();
            var query = ConnectionDb().Table<Students>();
            var result = await query.ToListAsync();

            foreach (var _Students in result)
            {
                Students.Add(new Students { Id = _Students.Id, Acadimic_id = _Students.Acadimic_id, name = _Students.name, FingerPrint = _Students.FingerPrint });
            }
            box.ItemsSource = Students;
        }
        public async static Task CountAllStudentsData(TextBlock box)
        {
            var Students = new List<Students>();
            var query = ConnectionDb().Table<Students>();
            var result = await query.ToListAsync();
            box.Text = result.Count().ToString();
        }
        public async static Task LoadSpacificStudentData(ListView box)
        {
            var Students = new List<Students>();
            var query = ConnectionDb().Table<Students>();
            var result = await query.ToListAsync();

            foreach (var _Students in result)
            {
                if (_Students.Acadimic_id == gloablvalue.SearchStudentId)
                {
                    Students.Add(new Students { Id = _Students.Id, Acadimic_id = _Students.Acadimic_id, name = _Students.name, FingerPrint = _Students.FingerPrint });
                }
            }
            box.ItemsSource = Students;
        }
        public async static void UpdateStudentData(string _AcadimicId, string _name)
        {
            var updateStudent = await ConnectionDb().Table<Students>().Where(w => w.Acadimic_id.Equals(_AcadimicId) && w.name.Equals(_name)).FirstOrDefaultAsync();
            updateStudent.Acadimic_id = _AcadimicId;
            updateStudent.name = _name;
            await ConnectionDb().UpdateAsync(updateStudent);
        }
        public async static void DeleteStudent(string _AcadimicId)
        {
            var deleteStudent = await ConnectionDb().Table<Students>().Where(w => w.Acadimic_id.Equals(_AcadimicId)).FirstOrDefaultAsync();
            deleteStudent.Acadimic_id = _AcadimicId;
            await ConnectionDb().DeleteAsync(deleteStudent);
        }
        public async static Task SearchStudentId(TextBlock box)
        {
            var _students = new List<Students>();
            var query = ConnectionDb().Table<Students>();
            var result = await query.ToListAsync();

            foreach (var _student in result)
            {
                if (_student.Acadimic_id == gloablvalue.SearchStudentId)
                {
                    box.Text = _student.Acadimic_id;
                    break;
                }
                else
                {
                    box.Text = "Not Found";
                }
            }
        }
        //StudentsInLec table Functions
        public async static void InsertAllStudentsInLecData(string _Lec_id, string _Student_id, string _Acadimic_id, string _Student_name, string _FingerPrintId)
        {
            var newStudentsInLec = new StudentsInLec
            {
                Lec_id = _Lec_id,
                Student_id = _Student_id,
                Acadimic_id = _Acadimic_id,
                Student_name = _Student_name,
                FingerPrintId = _FingerPrintId
            };

            await ConnectionDb().InsertAsync(newStudentsInLec);
        }
        public async static Task SearchStudentFingerPrint(TextBlock box0, TextBlock box1, TextBlock box2, TextBlock box3)
        {
            var _Students = new List<Students>();
            var query = ConnectionDb().Table<Students>();
            var result = await query.ToListAsync();
            if (result.Count() != 0)
            {
                foreach (var _Student in result)
                {
                    if (_Student.FingerPrint == gloablvalue.attendid)
                    {
                        box0.Text = _Student.Id.ToString();
                        box1.Text = _Student.Acadimic_id;
                        box2.Text = _Student.name;
                        box3.Text = _Student.FingerPrint;
                    }
                    else
                    {
                        box0.Text = "Not Found";
                        box1.Text = "Not Found";
                        box2.Text = "Not Found";
                        box3.Text = "Not Found";
                    }
                }
            }
            else
            {
                box0.Text = "NULL";
                box1.Text = "NULL";
                box2.Text = "NULL";
                box3.Text = "NULL";
            }
        }
        public async static Task CheckAttend(TextBlock box)
        {
            var _Student = new List<StudentsInLec>();
            var query = ConnectionDb().Table<StudentsInLec>();
            var result = await query.ToListAsync();

            if (result.Count() != 0)
            {
                foreach (var _RStudent in result)
                {
                    if ((_RStudent.Lec_id == gloablvalue.NewLecId) && (_RStudent.FingerPrintId == gloablvalue.attendid))
                    {
                        box.Text = "Found"; //7dr abl kda
                        break;
                    }
                    else
                    {
                        box.Text = "Not Found";
                    }
                }
            }
            else
            {
                box.Text = "NULL";
            }
        }
        public async static Task LoadAllStudentsInLecData(ListView box)
        {
            var Students = new List<StudentsInLec>();
            var query = ConnectionDb().Table<StudentsInLec>();
            var result = await query.ToListAsync();

            foreach (var _StudentsInLec in result)
            {
                if (_StudentsInLec.Lec_id == gloablvalue.CurrentLecId)
                {
                    Students.Add(new StudentsInLec { Id = _StudentsInLec.Id, Lec_id = _StudentsInLec.Lec_id, Student_id = _StudentsInLec.Student_id, Acadimic_id = _StudentsInLec.Acadimic_id, Student_name = _StudentsInLec.Student_name, FingerPrintId = _StudentsInLec.FingerPrintId });
                }
            }
            box.ItemsSource = Students;
        }
        public async static Task LoadSpacificStudentInLecData(ListView box)
        {
            var Students = new List<StudentsInLec>();
            var query = ConnectionDb().Table<StudentsInLec>();
            var result = await query.ToListAsync();

            foreach (var _StudentsInLec in result)
            {
                if (_StudentsInLec.Acadimic_id == gloablvalue.SearchStudentId)
                {
                    Students.Add(new StudentsInLec { Id = _StudentsInLec.Id, Lec_id = _StudentsInLec.Lec_id, Student_id = _StudentsInLec.Student_id, Acadimic_id = _StudentsInLec.Acadimic_id, Student_name = _StudentsInLec.Student_name, FingerPrintId = _StudentsInLec.FingerPrintId });
                }
            }
            box.ItemsSource = Students;
        }
        public async static Task SearchStudentsInLecData(TextBlock box)
        {
            var Students = new List<StudentsInLec>();
            var query = ConnectionDb().Table<StudentsInLec>();
            var result = await query.ToListAsync();

            foreach (var _StudentsInLec in result)
            {
                {
                    if (_StudentsInLec.Acadimic_id == gloablvalue.attendid)
                    {
                        box.Text = _StudentsInLec.Acadimic_id;
                        break;
                    }
                    else
                    {
                        box.Text = "Not Found";
                    }
                }
            }
        }
        //public async static Task LoadSpacificStudentData(GridView box)
        //{

        //    var Students = new List<Students>();
        //    var query = ConnectionDb().Table<Students>();
        //    var result = await query.ToListAsync();

        //    foreach (var _Students in result)
        //    {
        //        if (_Students.Acadimic_id == gloablvalue.SearchStudentId)
        //        {
        //            Students.Add(new Students { Id = _Students.Id, Acadimic_id = _Students.Acadimic_id, name = _Students.name, FingerPrint = _Students.FingerPrint });
        //        }
        //    }
        //    box.ItemsSource = Students;
        //}
        //public async static void UpdateLectureData(string _lecname, string _Datetime, string _Room)
        //{
        //    var updateLecture = await ConnectionDb().Table<Lectures>().Where(w => w.LectureName.Equals(_lecname) && w.Datetime.Equals(_Datetime) && w.Room.Equals(_Room)).FirstOrDefaultAsync();
        //    updateLecture.LectureName = _lecname;
        //    updateLecture.Datetime = _Datetime;
        //    updateLecture.Room = _Room;
        //    await ConnectionDb().UpdateAsync(updateLecture);
        //}

        //public async static void DeleteLecture(string _lecname)
        //{
        //    var deleteLecture = await ConnectionDb().Table<Lectures>().Where(w => w.LectureName.Equals(_lecname)).FirstOrDefaultAsync();
        //    deleteLecture.LectureName = _lecname;
        //    await ConnectionDb().DeleteAsync(deleteLecture);
        //}


        ////Students table Functions
        //public async static void InsertAllAttendanceStudentsData(string _Lec_id, string _StudentId, string _name, string )
        //{
        //    var newStudentInLec = new StudentsInLec
        //    {
        //        Acadimic_id = _Acadimic_id,
        //        name = _name,
        //        FingerPrint = _FingerPrint
        //    };

        //    await ConnectionDb().InsertAsync(newStudentInLec);
        //}

        //public async static Task LoadAllStudentsData(GridView box)
        //{
        //    var Students = new List<Students>();
        //    var query = ConnectionDb().Table<Students>();
        //    var result = await query.ToListAsync();

        //    foreach (var _Students in result)
        //    {
        //        Students.Add(new Students { Id = _Students.Id, Acadimic_id = _Students.Acadimic_id, name = _Students.name, FingerPrint = _Students.FingerPrint });
        //    }
        //    box.ItemsSource = Students;
        //}
    }
}
