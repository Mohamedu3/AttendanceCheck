using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceCheck.Tables
{
    public class gloablvalue
    {
        public static string DeviceName = "";
        public static string DeviceState = "-1";

        public static string VaildationUsername = "";
        public static string VaildationLoginUsername = "";
        public static string VaildationLoginPassword = "";

        public static string CurrentId;
        public static string Currentusername;
        public static string Currentfname;
        public static string Currentlname;
        public static string Currentpassword;
        public static string Currenttype;

        public static string CurrentLecId;
        public static string CurrentLecName;
        public static string CurrentLecDatetime;
        public static string CurrentLecDuration;
        public static string CurrentLecRoom;

        public static string NewLecId = "";

        public static string SearchStudentId = "";
        public static string SearchAccountUsername = "";

        public static string RecivedText = "";
        public static string Status = "";

        public static string attendid = "";

        public static string PageType = "";
        public static string Acc_id = "";
        public static string Acc_username = "";
        public static string Acc_fname = "";
        public static string Acc_lname = "";
        public static string Acc_type = "";
        public static string Acc_password = "";

        public static string Student_acadimicid = "";
        public static string Student_name = "";

        public static string checksend(string command)
        {
            switch (command)
            {
                case "a":
                    return "fingertest";
                case "b":
                    return "Found fingerprint sensor!";
                case "c":
                    return "Did not find fingerprint sensor :(";
                case "d":
                    return "Ready to enroll a fingerprint!";
                case "e":
                    return "Enrolling ID";
                case "f":
                    return "Creating model for #";
                case "g":
                    return "Waiting for valid finger to enroll";
                case "h":
                    return "Image taken";
                case "i":
                    return "Communication error";
                case "j":
                    return "Imaging error";
                case "k":
                    return "Unknown error";
                case "l":
                    return "Image converted";
                case "m":
                    return "Image too messy";
                case "n":
                    return "Could not find fingerprint features";
                case "o":
                    return "Remove finger";
                case "p":
                    return "ID = ";
                case "q":
                    return "Place same finger again";
                case "r":
                    return "Creating model";
                case "s":
                    return "Prints matched!";
                case "t":
                    return "Fingerprints did not match";
                case "u":
                    return "Stored!";
                case "v":
                    return "Could not store in that location";
                case "w":
                    return "Error writing to flash";
                case ".":
                    return "Waiting..";
                default:
                    return command;
            }
        }
    }
}