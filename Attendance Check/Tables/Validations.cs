using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;

namespace AttendanceCheck.Tables
{
    public static class Validations
    {
        public static bool result;

        public static bool CheckTextBox(params TextBox[] textboxs)
        {
            var result = false;
            foreach (var textBox in textboxs.Where(w => w.Text.Equals("")))
            {
                result = textBox.Text.Equals("");
            }
            return result;
        }

        public static bool CheckPasswordBox(params PasswordBox[] passwordboxs)
        {
            var result = false;
            foreach (var passwordBox in passwordboxs.Where(w => w.Password.Equals("")))
            {
                result = passwordBox.Password.Equals("");
            }
            return result;
        }

        public static async Task MessageConfirmDeleteoUpdatePerson(string message)
        {
            var dialog = new MessageDialog(message);
            dialog.Commands.Add(new UICommand("No", new UICommandInvokedHandler(Command)));
            dialog.Commands.Add(new UICommand("Yes", new UICommandInvokedHandler(Command)));
            await dialog.ShowAsync();
        }

        public static async Task Tryagain(string message)
        {
            var dialog = new MessageDialog(message);
            dialog.Commands.Add(new UICommand("No", new UICommandInvokedHandler(Command)));
            dialog.Commands.Add(new UICommand("Yes", new UICommandInvokedHandler(Command)));
            await dialog.ShowAsync();
        }

        private static void Command(IUICommand command)
        {
            if (command.Label.Equals("Yes"))
            {
                result = true;
            }
        }
    }
}