   M S B A M L   `   `   `  ����C  ?MyPacMan, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:MyPacManMyPacMan  5   	       MyPacMan.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   c _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5   	   "localclr-namespace:MyPacMan  5   	   -   5          � Title$  
MainWindow��5	   	   $��434.5��6   $	��626��6+   .��5
      � $��0,0,3.333,51.333q�6    J� RenderTransformOrigin$ 0.448,0.585&�6%   .+�5   
   c� -   5         c�Name$ MainMenuToolBar��5      $
��Left=�6+   $	��Top=�6F   $��25��6^   $	��616��6j   
��� ���.@�5      �� -   5         ��Name$ TopPlayersButton��5   ,   $��Show Records��6   $	��100��6F   $��22��6R   $��0,-1,0,-1.8q�6�   ��6^   ��  �� Path$ TopPlayersCommand�6�   5      �� 5   
   -   5        $��NameTextBox��5      $
��Left=�6K   $��23��6f   $��
192,44,0,0q�6r    �� TextWrapping$
 Wrap=�6�   $	��Top=�6�   $	��120��6�   ��6&   �� $ [Player.Name]�6�   �� 5   
   $��Enter your name��6   $
��Left=�6*   $��	69,41,0,0q�6E   $	��Top=�6X   �� 5   
   -   5        $ NewPlayerButton��5   !   $��Apply��6   $��346,44,161,277q�6�   $ 0.503,0.848&�6�   ��6:   �� $ NewPlayerCommand���6e   ��  �� ElementName$ NameTextBox��6�      MyPacMan.MyCanvas  5   
   -   5          Name$ Canvas��5      $��140,82,200,0q�6)   
������.  (System.Windows.Interactivity.Interaction   Triggers 5      /  )System.Windows.Interactivity.EventTrigger  5      	   	EventName$	 Loaded��6!   .  (System.Windows.Interactivity.TriggerBase
   Actions.
 5      6  0System.Windows.Interactivity.InvokeCommandAction     Command 6,   �� $ CanvasLoadedCommand� �� UpdateSourceTrigger$ PropertyChanged=�   CommandParameter 6~   �� $ Canvas��6�   5      5      �� 5   
   -   5        $��StartButton��5   !   $��Start��6   $
��Left=�64   $��
24,106,0,0q�6O   $	��Top=�6c   $��75��6{   ��6�   �� $ PlayCommand���6�   �� $ Canvas���� 5   
   -   5        $��Check��5      $��Some hard game��6"   $
��Left=�6;   $��
10,146,0,0q�6V   $	��Top=�6j   ��6�   �� $ CheckHardGame���6�   �� $ Check��5      5      