   M S B A M L   `   `   `  ����C  ?DllMeter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5(clr-namespace:DllMeter;assembly=DllMeterDllMeter  5   	   D @TestMeter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:TestMeter	TestMeter 5   	      TestMeter.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   :meter(clr-namespace:DllMeter;assembly=DllMeter     5   	   )localclr-namespace:TestMeter     5   	     � Title$  Test new meter control��5	   	   $	��600��6(   $	��800��65    � WindowStartupLocation$ CenterScreen=�5
   	    � 	Resources 5       �� Source�� 5   
   $ Dictionary1.xaml?�6   %    .��5      �� 
����� �$��Center=�6#   .+�5   
   �� $��10q�6   .+�5         DllMeter.SmartMeter  -   5          Name$ Meter1��5        5      -   5        $ Meter2��5       
  Meter2#��[  6/   5   :   5      �� 5   
    �� Orientation$ 
Horizontal=�6   $��	10 50 0 0q�6.   .+�5        $��0 0 0 0q�6      SM_Background2$ Black��60      SM_Background3$
 Lime��6G      SM_Foreground1$ White��6]   	   SM_Description$	 	Vancouver��5      
   
SM_Minimum$
 10.11Q�6:      
SM_Maximum$ 20.222Q�6M      SM_MeterValue$ 14Q�6a      SM_Scale$ .5Q�5         SM_Threshold$
 17.5Q�6-     5      $��0 0 0 0q�6   $	 North Vancouver��60   $
 1Q�6Q   $ 2Q�6`   $
 1.75Q�6o   $	 .75Q�5      $
 1.75Q�6.     5         SM_Background1$ Orange��6   $ Bisque��67   $
 Lime��6O   $	 Red��5      $	 South Surrey��64   $	
 100Q�6R   $	 800Q�6c   $	 275Q�6t   $	 625Q�5       �� 5!      -   5         ��Name$ btnQuit��5!   $   $
��Quit��6   $��80��5"      $��30��6    $
��Bold"�6,    � Cursor$
 Hand��6>    �� IsCancel . $��10q�6\   5#      5$      5%      