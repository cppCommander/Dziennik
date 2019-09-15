using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel; // CancelEventArgs

namespace Dziennik
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    
    public partial class MainWindow : Window
    {
        
        bool isDataDirty = false;
        double FontS = 12;

        private void DateTimePicker1_CalendarClosed(object sender, RoutedEventArgs e)
        {
            bool b2 = false;
            using (StreamReader file = new StreamReader("WriteLines.txt"))
            {
                string ln;

                while ((ln = file.ReadLine()) != null)
                {
                    if (ln == DateTimePicker1.ToString())
                    {
                        Textbox1.Text = file.ReadLine();
                        b2 = true;
                    }
                }
                file.Close();
            }
            if (b2 == false)
                if (this.isDataDirty)
                {
                    string msg = "Data is dirty. Close without saving?";
                    MessageBoxResult result =
                      MessageBox.Show(
                        msg,
                        "Data App",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Warning);
                    if (result == MessageBoxResult.No)
                    {
                        // If user doesn't want to close, cancel closure
                    }
                    else
                    {
                        Textbox1.Text = "";
                        this.isDataDirty = false;
                    }
                }
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string messageBoxText = "There is entry on this date, do you want to override?";
            string caption = "Word Processor";
            MessageBoxButton button = MessageBoxButton.YesNoCancel;
            MessageBoxImage icon = MessageBoxImage.Warning;
            int b1=0;
            using (StreamReader file = new StreamReader("WriteLines.txt"))
            {
                string ln;

                while ((ln = file.ReadLine()) != null)
                {
                    if (ln == DateTimePicker1.ToString())
                    {
                        b1 = 1;
                    }
                }
                file.Close();
            }

            if (b1 == 1)
            {
                using (StreamReader file = new StreamReader("WriteLines.txt"))
                {
                    string ln;

                    while ((ln = file.ReadLine()) != null)
                    {
                        if (ln == DateTimePicker1.ToString())
                        {

                            MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);
                            switch (result)
                            {
                                case MessageBoxResult.Yes:
                                    b1 = 2;
                                    break;
                                case MessageBoxResult.No:
                                    // User pressed No button
                                    // ...
                                    break;
                                case MessageBoxResult.Cancel:
                                    // User pressed Cancel button
                                    // ...
                                    break;
                            }
                        }
                    }
                    file.Close();
                }
            }

            if(b1 == 2)
            {
                List<string> quotelist = File.ReadAllLines("WriteLines.txt").ToList();
                for(int i=0; i < quotelist.Count; i++)
                {
                    if(quotelist[i]== DateTimePicker1.ToString())
                        {
                        quotelist.RemoveRange(i, 2);
                        break;
                    }
                            
                }
                File.WriteAllLines("WriteLines.txt", quotelist.ToArray());
                using (StreamWriter writetext = File.AppendText("WriteLines.txt"))
                {


                    writetext.WriteLine(DateTimePicker1.ToString());
                    writetext.WriteLine(Textbox1.Text);
                }
                this.isDataDirty = false;
            }
            else
            {
                using (StreamWriter writetext = File.AppendText("WriteLines.txt"))
                {
                

                    writetext.WriteLine(DateTimePicker1.ToString());
                    writetext.WriteLine(Textbox1.Text);
                }
                this.isDataDirty = false;
            }
        }

        private void FontSize_Click(object sender, RoutedEventArgs e)
        {

            FontS*=1.25;
            Textbox1.FontSize = FontS;
        }

        private void FontSize1_Click(object sender, RoutedEventArgs e)
        {

            FontS /= 1.25;
            Textbox1.FontSize = FontS;
        }

        private void Textbox1_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.isDataDirty = true;
        }

        void DataWindow_Closing(object sender, CancelEventArgs e)
        {
            if (this.isDataDirty)
            {
                string msg = "Data is dirty. Close without saving?";
                MessageBoxResult result =
                  MessageBox.Show(
                    msg,
                    "Data App",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);
                if (result == MessageBoxResult.No)
                {
                    // If user doesn't want to close, cancel closure
                    e.Cancel = true;
                }
                else if(result == MessageBoxResult.Yes)
                {
                        Textbox1.Text = "";
                }
            }
        }
           
    }
}

    
