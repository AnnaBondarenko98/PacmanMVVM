   M S B A M L   `   `   `  ����C  ?MyPacMan, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:MyPacManMyPacMan  5   	   F BLocalizator, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null>.clr-namespace:Localizator;assembly=LocalizatorLocalizator 5	   	       MyPacMan.MainWindow   5   
   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Edxmvvm2http://schemas.devexpress.com/winfx/2008/xaml/mvvm     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   c _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5   	   "localclr-namespace:MyPacMan  5   	   6l.clr-namespace:Localizator;assembly=Localizator  5	   	     � 
ResizeMode$  NoResize=�5   	    � Title$ 
MainWindow��5   	   $��434.5��6   $	��626��6+    � 	Resources 5         BooleanToVisibility$  Localizator.LocalisationHelper 
 Helper��%:   &	        &	      �� 5   
   6'     5   
   60   5      .��5      � -   5        $��MyGrid��5      $��0,0,4.667,51.667q�6    J� RenderTransformOrigin$ 0.448,0.585&�63   .+�5   
   c� -   5         c�Name$ MainMenuToolBar��5      $
��Left=�6+   $	��Top=�6F   $��25��6^   $	��616��6j   
��� ���.@�5      �� -   5         ��Name$ TopPlayersButton��5   o   $	��100��6�   $��22��6�   $��0,-1,0,-1.8q�6�   ��6   ��  �� Source# [  �� Path$&  [MainResources.TopPlayersButton]���6�   �� $ TopPlayersCommand�6�   �� 5      -   5        $ PluginButton��5   k   $	��100��6�   $��22��6�   $��0,-1,0,-1.8q�6�   ��6   �� # [ $" [MainResources.PluginButton]���6�   �� $ CheckHardGame���6�   ��  �� ElementName$ PluginButton��6�   5      o� 5   
   -   5        	 o�Name$	 MyMenu��5      $��21��6   $	��Top=�6+   $	��145��6C   
��� ���$��
461,1,10,0q�6c   .@�5      m� -   5        $��
MyMenuItem��5      $��22��6y   $��0q�6�   ��6)   �� *[MainResources.Language_Btn]+# [ .@�5      m� $��Русский��6   
 m� Command
 6,   �� *RussianCommand+m� 5      $��English��6   
 6,   �� *EnglishCommand+5      5         MyPacMan.MyCanvas  5   
   -   5          Name$ Canvas��5      $��140,82,223.334,10.334q�6)   
��� � �.  (System.Windows.Interactivity.Interaction   Triggers 5      /  )System.Windows.Interactivity.EventTrigger  5          	EventName$ Loaded��6!   .  (System.Windows.Interactivity.TriggerBase   Actions. 5!      6  0System.Windows.Interactivity.InvokeCommandAction     Command 6,   �� $ CanvasLoadedCommand� �� UpdateSourceTrigger$ PropertyChanged=�   CommandParameter 6~   �� $ Canvas��6�   5"      5%      �� 5&   
   -   5        $��StartButton��5&   f   $
��Left=�6y   $��
24,106,0,0q�6�   $	��Top=�6�   $��75��6�   ��6   �� # [ $! [MainResources.StartButton]���6�   �� $ PlayCommand���6�   �� $ NameTextBox���� 5'   
   -	   5        $��	LifeTitle��5'      $
��Left=�6t   $��400,108,0,0q�6�   $	��Top=�6�   ��6!   �� # [ $ [MainResources.LifeTitle]�6�   �� 5(   
   -
   5        $��	lifeLabel��5(      $
��Left=�6E   $��506,106,0,0q�6`   $	��Top=�6u   ��6!   �� $ 
LifesCount�6u   �� 5)   
   -   5        $��	TimeLabel��5)      $�� ��6"   $
��Left=�6/   $��417,191,0,0q�6J   $	��Top=�6_   �� 5*   
   -   5        $��	TimeLimit��5*      $
��Left=�6u   $��399,146,0,0q�6�   $	��Top=�6�   ��6"   �� # [ $ [MainResources.TimeLimit]�6�   5,       5-        5.   
   $ KeyDown��6   . 5/         6$   �� $ Window_KeyDownCommand�$ PropertyChanged=� 6x   �� $ Canvas��6�   50      53      