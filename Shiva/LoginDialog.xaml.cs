using Shiva.Shared.BaseControllers;
using Shiva.Shared.Data;
using Shiva.Shared.ServerCommunication;
using Shiva.Shared.Utility;
using GnosisControls;
using ShivaWPF3.UtilityWPF;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
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
using System.Windows.Shapes;
using System.Threading;
using Shiva.Shared.Interfaces;

namespace Shiva
{
    /// <summary>
    /// Interaction logic for LoginDialog.xaml
    /// </summary>
    public partial class LoginDialog : Window, IGnosisLoginDialog
    {
        private string username;
        private string password;
        private string server;
        private string systemURL;
       // private GnosisParentWindow parentWindow;
        private CircularProgressBar loadingAnimation;
        private bool connected;

        public LoginDialog()
        {
            GlobalData.Singleton.Login = this;
            connected = false;

            InitializeComponent();
            txtServer.Text = "BHEEMA";
            txtSystemURL.Text = "www.gnosis.co.nz";
            txtUserName.Text = "Sukhajata";
            txtPassword.Password = "Sukhajata";

            loadingAnimation = new CircularProgressBar();
        }

        private void btnOK_Click(Object sender, RoutedEventArgs e)
        {
            server = txtServer.Text;
            username = txtUserName.Text;
            password = txtPassword.Password;
            systemURL = txtSystemURL.Text;

            ThreadStart start = new ThreadStart(ConnectToServer);
            Thread t = new Thread(start);
            t.SetApartmentState(ApartmentState.STA);
            t.Start();
            

            //BackgroundWorker worker = new BackgroundWorker();
            //worker.DoWork += ConnectServer_Background;
            //worker.RunWorkerCompleted += ConnectServer_Completed;
            //worker.RunWorkerAsync();


            gridOuter.Children.Clear();
            viewLoading.Child = loadingAnimation;
            viewLoading.Visibility = Visibility.Visible;

           

            //Dispatcher.BeginInvoke((Action)(() =>
            //{

            //    GlobalData.Singleton.DatabaseConnection = new ServerConnection(
            //       username,
            //       password,
            //       server,
            //       systemURL,
            //       "Gnosis_Dev_27_7",
            //       "Desktop",
            //       Environment.MachineName,
            //       new GnosisErrorHandlerWPF(),
            //       new GnosisIOHelperWPF());

            //    connected = GlobalData.Singleton.DatabaseConnection.Connect();

            //    if (connected)
            //    {
            //        GlobalData.Singleton.ApplicationManager = new GnosisApplicationManager(
            //                                       //   parentWindow,
            //                                       GnosisController.OrientationType.LANDSCAPE,
            //                                       // new GnosisImplementationCreatorWPF(),
            //                                       //  new GnosisIOHelperWPF(),
            //                                       new GnosisSystemCommandsWPF(),
            //                                       new StyleHelper());

            //        GlobalData.Singleton.SystemController.SetupSystem();
            //        GlobalData.Singleton.SystemController.SetupUI();

            //    }

            //    ((GnosisParentWindow)GlobalData.Singleton.ParentWindowImplementation).Show();

            //    this.Close();

            //}));

        }

        //private void ConnectServer_Background(object sender, DoWorkEventArgs e)
        //{
        //    GlobalData.Singleton.DatabaseConnection = new ServerConnection(
        //        username,
        //        password,
        //        server,
        //        systemURL,
        //        "Gnosis_Dev_27_7",
        //        "Desktop",
        //        Environment.MachineName,
        //        new GnosisErrorHandlerWPF(),
        //        new GnosisIOHelperWPF());

        //    //Dispatcher.Invoke((Action)(() =>
        //    //{
        //        connected = GlobalData.Singleton.DatabaseConnection.Connect();

        //        if (connected)
        //        {
        //            GlobalData.Singleton.ApplicationManager = new GnosisApplicationManager(
        //                                         //  parentWindow,
        //                                           GnosisController.OrientationType.LANDSCAPE,
        //                                           // new GnosisImplementationCreatorWPF(),
        //                                         //  new GnosisIOHelperWPF(),
        //                                           new GnosisSystemCommandsWPF(),
        //                                           new StyleHelper());

        //            GlobalData.Singleton.SystemController.SetupSystem();

        //          //  Dispatcher.BeginInvoke((Action)(() =>
        //           // {
        //            GlobalData.Singleton.SystemController.SetupUI();
        //           // }));



        //            //ExcelPropertyComparer propertyComparer = new ExcelPropertyComparer();
        //        }

        //   // }));


        //   ((GnosisParentWindow)GlobalData.Singleton.ParentWindowImplementation).Show();

        //}

        private void ConnectToServer()
        {
            GlobalData.Singleton.DatabaseConnection = new ServerConnection(
                username,
                password,
                server,
                systemURL,
                "Gnosis_Dev_27_7",
                "Desktop",
                Environment.MachineName,
                new GnosisErrorHandlerWPF(),
                new GnosisIOHelperWPF());

            
            connected = GlobalData.Singleton.DatabaseConnection.Connect();

            if (connected)
            {
                GlobalData.Singleton.ApplicationManager = new GnosisApplicationManager(
                                               //  parentWindow,
                                               GnosisController.OrientationType.LANDSCAPE,
                                               // new GnosisImplementationCreatorWPF(),
                                               //  new GnosisIOHelperWPF(),
                                               new GnosisSystemCommandsWPF(),
                                               new StyleHelper());

                GlobalData.Singleton.SystemController.SetupSystem();

                GlobalData.Singleton.SystemController.SetupUI();

                ((LoginDialog)GlobalData.Singleton.Login).Dispatcher.Invoke((Action)(() =>
                {
                    ((LoginDialog)GlobalData.Singleton.Login).Hide();
                }));

                GnosisParentWindow parentWindow = (GnosisParentWindow)GlobalData.Singleton.ParentWindowImplementation;
                parentWindow.Closed += (sender2, e2) => parentWindow.Dispatcher.InvokeShutdown();
                parentWindow.Show();

                System.Windows.Threading.Dispatcher.Run();

               

            }


        }

        private void ConnectServer_Completed(object sender, RunWorkerCompletedEventArgs e)
        {
            ((GnosisParentWindow)GlobalData.Singleton.ParentWindowImplementation).Show();
            this.Close();
        }

        private void btnCancel_Click(Object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void chkWindowsCredentials_Checked(object sender, RoutedEventArgs e)
        {
            txtUserName.IsEnabled = false;
            txtPassword.IsEnabled = false;
            lblUserName.Foreground = Brushes.LightGray;
            lblPassword.Foreground = Brushes.LightGray;
        }

        private void chkWindowsCredentials_Unchecked(object sender, RoutedEventArgs e)
        {
            txtUserName.IsEnabled = true;
            txtPassword.IsEnabled = true;
            lblUserName.Foreground = Brushes.Black;
            lblPassword.Foreground = Brushes.Black;
        }
    }
}
