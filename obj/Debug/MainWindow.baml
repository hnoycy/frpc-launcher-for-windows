   M S B A M L   `   `   `  ����H  Dfrpc客户端, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:frpc客户端frpc客户端  5   	       frpc客户端.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:frpc客户端     5   	   _ [Hardcodet.NotifyIcon.Wpf, Version=1.1.0.0, Culture=neutral, PublicKeyToken=682384a853a08aad)tb http://www.hardcodet.net/taskbar  6-   -   5          � Title$  
MainWindow��5
   	   $	��450��6   $	��800��6)    � Icon$3 -pack://application:,,,/Resources/img/frpc.ico��5   	    � 	Resources 5      3  -Hardcodet.Wpf.TaskbarNotification.TaskbarIcon   
NotifyIcon   
IconSource   ToolTipText��%t   &	          5   
   $3 -pack://application:,,,/Resources/img/frpc.ico��6,   $
 Frpc��6g   $y�Visible=�6z   5      .��5      �  � RowDefinitions 5   
   �� 5      $	��20* �6   �� 5      $	��10* �6   �� 5      $	��70* �6   .+�5   
   � $��0��6    � ColumnDefinitions 5      �� 5      $	��50* �6#   �� 5      $	��50* �6#   .+�5      �� $��0��6   .+�5       �� $��frpc.exe Path:��6   �� 5!      -   5         ��Name$ frpcPathTextBox��5!      $	��300��63   $��0,0,0,10q�6?   $��
.\frpc.exe��6Q   �� 5"      -   5        $��浏览��5"      6&   5#      �� 5&      $��1��6   $��10,0,0,0q�6)   .+�5'      �� $��frpc.toml Path:��6   �� 5(      -   5        $ frpcConfigPathTextBox��5(      $	��300��69   $��0,0,0,10q�6E   $��.\frpc.toml;.\frpc1.toml��6W   �� 5)      -   5        $��浏览��5)      6&   5*      5+      �� 5-   
   -   5         ��Name$ 	RunButton��5-      $��运行��6$   $��1��62   $��Stretch=�6W   $��Stretch=�6s   �� 5.   
   -   5        $ outputTextBox��5.      $��2��6)   "	 �� VerticalScrollBarVisibility$
	 Auto=�66   
 �� 
IsReadOnly
 . $��Stretch=�6k   $��Stretch=�6�   5/      50      