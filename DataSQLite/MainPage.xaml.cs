using DataSQLite.Entity;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace DataSQLite
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private ObservableCollection<User> listUser;
        internal ObservableCollection<User> ListUser { get => listUser; set => listUser = value; }
        public MainPage()
        {
            this.ListUser = new ObservableCollection<User>();
            this.InitializeComponent();
            if (search.Text == "")
            {
                ShowAll();
            }

        }
        private void ShowAll()
        {
            //User entries = new User();
            ListUser.Clear();
            using (SqliteConnection db =
                new SqliteConnection("Filename=UserData.db"))
            {
                db.Open();
                SqliteCommand selectCommand = new SqliteCommand
                    ("SELECT * from MyTable", db);
                SqliteDataReader query = selectCommand.ExecuteReader();
                while (query.Read())
                {
                    Debug.WriteLine(query.GetString(0));
                    Debug.WriteLine(query.GetString(1));
                    Debug.WriteLine(query.GetString(2));
                    ListUser.Add(new User
                    {
                        phone = query.GetString(0),
                        name = query.GetString(1),
                        email = query.GetString(2)
                    });
                }
                db.Close();
            }
        }

        private void ShowByPhone(string phone)
        {
            //User entries = new User();
            ListUser.Clear();
            using (SqliteConnection db =
                new SqliteConnection("Filename=UserData.db"))
            {
                db.Open();
                SqliteCommand selectCommand = new SqliteCommand
                    ("SELECT * from MyTable where Phone LIKE '"+phone+"%'", db);
                SqliteDataReader query = selectCommand.ExecuteReader();
                while (query.Read())
                {
                    var user = new User()
                    {
                        phone = query.GetString(0),
                        name = query.GetString(1),
                        email = query.GetString(2)
                    };
                    ListUser.Add(user);
                }
                db.Close();
            }
        }
        private void Do_Save(object sender, RoutedEventArgs e)
        {
            User user = new User()
            {
                name = name.Text,
                email = email.Text,
                phone = phone.Text
            };
            Handle.DataHandle.AddData(user);
            ShowAll();
        }

        private void Do_Filter(object sender, KeyRoutedEventArgs e)
        {
            ShowByPhone(search.Text);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ShowAll();
        }
    }
}
