   M S B A M L   `   `   `  ����M  ISpanzuratoarea_MVP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullK*clr-namespace:Spanzuratoarea_MVP.ViewModelSpanzuratoarea_MVP.ViewModel  5   	   (    "Spanzuratoarea_MVP.View.MainWindow   5   	   W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   9vm*clr-namespace:Spanzuratoarea_MVP.ViewModel     5   	     � Title$  
MainWindow��5   	   $	��450��6   $	��800��6)      UserContext � DataContext# [  5	   	   .��5
      � .+�5   
   �� -   5        $
��List��5      $��0,24,556,114q�6e    �� SelectedIndex 6   �� *SelectedIndex+��6G   �� *Users+��5      �� 5      .R�5      �� ��6   �� *Name+5      5      �� 5   
   $��New User��6   $
��Left=�6$   $��
49,349,0,0q�6?   $	��Top=�6S   $��31��6k   ��6w   �� *	NewUser+�� 5   
   $��Delete User��6   $��Right=�6'   $��0,349,482,0q�6C   $	��Top=�6X   $��31��6p   $��69��6|   ��6�   �� *
RemoveUser+�� 5   
   $
��Play��6   $
��Left=�6    $��500,349,0,0q�6;   $	��Top=�6P   $��31��6h   ��6t   �� *
PlayButton+�� 5   
   $��Cancel��6   $
��Left=�6"   $��727,349,0,0q�6=   $	��Top=�6R   $��31��6j   ��6v   �� *Cancel+�� 5   
   $-��'Welcome to Hangman! Enjoy this game!   ��6   $
��Left=�6B   $��
334,10,0,0q�6]   $	��Top=�6q   $	��334��6�   
������ $��Center=�6�   ���	#FF998888$��Bookman Old Style)�6�   $��16(�6  �� 5   
   $
��Left=�6I   $	��100��6d   $��
465,83,0,0q�6q   $	��Top=�6�   $	��100��6�   ��6   �� *SelectedItem.Image+ �� ElementName$
 List��6�   5      5      