   M S B A M L   `   `   `  ����D  @TrackStar, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:TrackStar.MVVM.ViewTrackStar.MVVM.View  5      ;"clr-namespace:TrackStar.ConvertersTrackStar.Converters  5      #    TrackStar.MVVM.View.HomePanel   5      W SPresentationCore, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5      R NSystem.Xaml, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      3local!clr-namespace:TrackStar.MVVM.View     5      9
converters"clr-namespace:TrackStar.Converters     5        � 	Resources  5      4   .TrackStar.Converters.UriToBitmapImageConverter   UriToBitmapImageConverter��%   &	          5   
   6/   5      .��5      � 
������ .+�5   
   ��   BlackGradientStyle#���  6   5      ]� 5   
   $
��True��6   
������ .+�5      � $	��200��6   $��Bottom=�6     � RowDefinitions 5      �� 5      $��* �6$   �� 5      $��* �6$    � ColumnDefinitions 5      �� 5      $��* �6'   �� 5      $��* �6'   �� 5      $��* �6'   .+�5      ��   
HomeGenre1#��[ 6   $��0��6<   $��0��6I   .��6Y   Action6a   �� 5        
HomeGenre2#��[ 6   $��0��6<   $��1��6I   .��6Y   Horror6a   �� 5         
HomeGenre3#��[ 6   $��0��6<   $��2��6I   .��6Y   Comedy6a   �� 5!        
HomeGenre4#��[ 6   $��1��6<   $��0��6I   .��6Y   	Mystery6b   �� 5"        
HomeGenre5#��[ 6   $��1��6<   $��1��6I   .��6Y   	Romance6b   �� 5#        
HomeGenre6#��[ 6   $��1��6<   $��2��6I   .��6Y   	Animation6d   5$      �� 5'      $
��Auto=�6   $��Disabled=�6>   $��5q�6g   $��Stretch=�6s   $��Right=�6�   .��5)      �� $	��300��6   .+�5*      �� $
��Wrap=�6    
��������� 5+      $�TotalResults: ��6   $
��Bold"�6'    �� 5,      �6   �� *SearchList.totalResults+5-      �� 5/      $
��Wrap=�6    
�������$��Suggested results��6G   6b   �� 51      
������ $��Stretch=�6b   $
��Auto=�52      ��51      �� *SearchList.Search+��53      �� 54      .R�55   "   �� .+�56   &   �� $��50��57      $��50��6(   $��Center=�63   $��Center=�6P   ��56   ,   �� *Poster+ �� 	Converter# [  57   P   �� 58   &   
�������$
��Bold"�6Y   ��6,   �� *Title+6m   59   #   5;      5=      5>      5@      �� 5C      .+�5D      ��   HomePageFilmTitle#��[ 68   ��6   �� *Movie.Title+68   � 5E       5F      �� 5G      $��2* �6+   68   �� 5H      $��2* �6+   68    5J      �� 5K      $��1* �6(   66   �� 5L      $��1* �6(   66   .+�5O      �� $��0��6   $��0��6-   $��2��6:   $��10q�6K   
������ 
�������$��5q�5R       J� Effect 5S      3  -System.Windows.Media.Effects.DropShadowEffect  5U         Color$ White��5V         ShadowDepth$ 0Q�5W         
BlurRadius$ 10Q�5X         Opacity$	 0.8Q�5Y      ._�5\      �� $��0��6    $��0��60   $��10q�6=   $��Center=�6�   $��Center=�6�   ��6I   �� *Movie.Poster+# [  	 � LayoutTransform	 5^      �� 5_   "   
 �� ScaleX$	
 0.8Q�61    �� ScaleY$	 0.8Q�6>    5a        5b   "   $ White��5c      $ 0Q�5d      $	 100Q�5e      $	 0.8Q�5f      5h      5j      �� 5k      $
��Auto=�6#   $��Disabled=�6G   $	��Top=�6p   $��0��6�   $��1��6�   $��2��6�   .��5l      �� .+�5m      �� 
�������.��5n   "   �� $
��Wrap=�6j   ��6,   �� *Movie.Director+ �� StringFormat$ Director: {0}��6j   5o      �� 5q      
�������.��5r   "   �� $
��Wrap=�6f   ��6,   �� *Movie.Writer+$ Writer: {0}��6f   5s      �� 5u      
�������.��5v   "   �� $
��Wrap=�6h   ��6,   �� *Movie.Country+$ Country: {0}��6h   5w      �� 5x      
�������$
��Wrap=�5z      ��5y      �� *
Movie.Plot+$ 	Plot: {0}��5z      5{      5}      5      5�      5�      5�      5�      