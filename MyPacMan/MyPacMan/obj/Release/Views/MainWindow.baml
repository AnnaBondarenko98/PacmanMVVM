   M S B A M L   `   `   `  ���� C  ?MyPacMan, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:MyPacManMyPacMan      MyPacMan.MainWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     c _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  "localclr-namespace:MyPacMan  -     � Title$  
MainWindow��$��434.5��$	��626��.��� $��0,0,3.333,51.333q� J� RenderTransformOrigin$ 0.448,0.585&�.+�c� -    c�Name$ MainMenuToolBar��$
��Left=�$	��Top=�$��25��$	��616��
��� ���.@��� -    ��Name$ TopPlayersButton��$��Show Records��$	��100��$��22��$��0,-1,0,-1.8q�����  �� Path$ TopPlayersCommand��� -   $��NameTextBox��$
��Left=�$��23��$��
192,44,0,0q� �� TextWrapping$
 Wrap=�$	��Top=�$	��120������ $ [Player.Name]��� $��Enter your name��$
��Left=�$��	69,41,0,0q�$	��Top=��� -   $ NewPlayerButton��$��Apply��$��346,44,161,277q�$ 0.503,0.848&����� $ NewPlayerCommand�����  �� ElementName$ NameTextBox��   MyPacMan.MyCanvas  -     Name$ Canvas��$��140,82,200,0q�
������.  (System.Windows.Interactivity.Interaction   Triggers /  )System.Windows.Interactivity.EventTrigger  	   	EventName$	 Loaded��.  (System.Windows.Interactivity.TriggerBase
   Actions.
 6  0System.Windows.Interactivity.InvokeCommandAction     Command �� $ CanvasLoadedCommand� �� UpdateSourceTrigger$ PropertyChanged=�   CommandParameter �� $ Canvas���� -   $��StartButton��$��Start��$
��Left=�$��
24,106,0,0q�$	��Top=�$��75������ $ PlayCommand����� $ Canvas���� -   $��Check��$��Some hard game��$
��Left=�$��
10,146,0,0q�$	��Top=����� $ CheckHardGame����� $ Check��