using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using Hardcodet.Wpf.TaskbarNotification;
using System.Collections.Generic;

namespace frpc客户端
{
    public partial class MainWindow : Window
    {
        private List<Process> processes = new List<Process>();
        private string frpcPath = @"C:\frp\frpc.exe";
        private string frpcConfigPath = @"C:\frp\frpc.toml";
        private TaskbarIcon notifyIcon;

        public MainWindow()
        {
            InitializeComponent();

            // 初始化托盘图标
            notifyIcon = (TaskbarIcon)FindResource("NotifyIcon");
            notifyIcon.TrayMouseDoubleClick += NotifyIcon_TrayMouseDoubleClick;

        }

        private void RunButton_Click(object sender, RoutedEventArgs e)
        {
            if(RunButton.Content.ToString() == "运行")
            {
                StartFrpClient();
                RunButton.Content = "停止";
            }
            else
            {
                StopButton_Click(null, null);
                RunButton.Content = "运行";
            }

        }

        private void StartFrpClient()
        {

            frpcPath = frpcPathTextBox.Text;
            frpcConfigPath = frpcConfigPathTextBox.Text;
            string[] pathArray = frpcConfigPath.Split(';');

            foreach (string path in pathArray)
            {
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = frpcPath,
                    Arguments = $"-c {path}",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    WindowStyle = ProcessWindowStyle.Hidden
                };


                Process process = new Process { StartInfo = startInfo };
                process.OutputDataReceived += (s, e) => UpdateOutput(e.Data);
                process.ErrorDataReceived += (s, e) => UpdateOutput(e.Data);
                process.Start();
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();

                processes.Add(process);
            }
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (var process in processes)
            {
                // 中止命令行进程
                if (process != null && !process.HasExited)
                {
                    process.Kill();
                    outputTextBox.AppendText("已终止Frpc\n");
                }
            }
            processes.Clear();
        }

        private void UpdateOutput(string message)
        {
            // 在UI线程上更新文本框内容
            Dispatcher.Invoke(() =>
            {
                outputTextBox.AppendText(message + Environment.NewLine);
                outputTextBox.ScrollToEnd();
            });
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // 在窗口关闭时调用停止按钮的点击事件
            StopButton_Click(null, null);
        }

        private void NotifyIcon_TrayMouseDoubleClick(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Minimized)
            {
                WindowState = WindowState.Normal; // 将窗口状态设置为正常
            }

            if (Visibility == Visibility.Visible)
            {
                Visibility = Visibility.Collapsed; // 隐藏窗口
            }
            else
            {
                Show(); // 使用 Show 方法显示窗口
                Activate(); // 激活窗口
            }
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
            if (WindowState == WindowState.Minimized)
            {
                NotifyIcon_TrayMouseDoubleClick(null, null);
            }
        }

        private void BrowseFrpExePath(object sender, RoutedEventArgs e)
        {
            string selectedPath = BrowseFilePath();
            if (!string.IsNullOrEmpty(selectedPath))
            {
                frpcPathTextBox.Text = selectedPath;
            }
        }

        private void BrowseFrpConfigPath(object sender, RoutedEventArgs e)
        {
            string selectedPath = BrowseFilePathToml();
            if (!string.IsNullOrEmpty(selectedPath))
            {
                frpcConfigPathTextBox.Text = selectedPath;
            }
        }

        private string BrowseFilePath()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "frpc应用程序 (*.exe)|*.exe|All Files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                return openFileDialog.FileName;
            }
            return null;
        }

        private string BrowseFilePathToml()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "frpc toml文件 (*.toml)|*.toml|All Files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                return openFileDialog.FileName;
            }
            return null;
        }
        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            // 执行你想要的关闭时的代码
            StopButton_Click(null, null);
        }

    }

}

