using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using Hardcodet.Wpf.TaskbarNotification;


namespace frpc客户端
{
    public partial class MainWindow : Window
    {
        private Process process;
        private readonly string frpcPath = @"C:\frp\frpc.exe";
        private readonly string frpcConfigPath = @"C:\frp\frpc.toml";
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
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = frpcPath,
                Arguments = $"-c {frpcConfigPath}",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true,
                WindowStyle = ProcessWindowStyle.Hidden
            };

            process = new Process { StartInfo = startInfo };
            process.OutputDataReceived += (s, e) => UpdateOutput(e.Data);
            process.ErrorDataReceived += (s, e) => UpdateOutput(e.Data);
            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            // 中止命令行进程
            if (process != null && !process.HasExited)
            {
                process.Kill();
                outputTextBox.AppendText("已终止Frpc");
            }
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

    }
}
