   M S B A M L   `   `   `  ���� X  TRoblox Legacy Place Converter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullM+clr-namespace:Roblox_Legacy_Place_ConvertorRoblox_Legacy_Place_Convertor  .    (Roblox_Legacy_Place_Convertor.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     =local+clr-namespace:Roblox_Legacy_Place_Convertor       � Title$#  Roblox Legacy Place Converter��$	��228��$	��890�� � Icon$ Assets/converterLogo.ico��$	��890��$	��228������  �� EndPoint$ 0.5,1&� �� 
StartPoint$ 0.5,0&�.���  � Color$ 	#FF0F2630�� � Offset$ 0Q�� $ 	#FF235970��$ 1Q�.��� .+��� $A��;Converts new places in a way that old clients can open them��$��	10,10,0,0q�$	��Top=�$��16(�$��35��$
��Left=�$	��440��
��������� -    ��Name$ PlaceSelectedLabel��$��Place selected: None��$
��Left=�$��	10,45,0,0q�$	��Top=�
��������� -    ��Name$ BrowseButton��$��Browse��$
��Left=�$��	15,71,0,0q�$	��Top=�$��75��$��24���� -   $ ConvertButton��$��CONVERT��$
��Left=�$��
15,104,0,0q�$	��Top=�$	��125��$��31��$
��Bold"�� -    �Name$ ProgressBar��$��15��$��150,104,0,0q�$	��Top=�	 � Value$	 0Q�$
��Left=�$	��290���� -   $ ProgressLabel��$�� ��$
��Left=�$��145,117,0,0q�$	��Top=�$��28��
���������Z� �� $��Made by BakonBot��$
��Left=�$
��Left=�$��
11,145,0,0q�$	��Top=�
��������� $
��Left=�$	��Top=�$��121,150,0,0q��� -   
 ��Name$
 GithubHyperlink��$8��2https://github.com/BakonBot/legacy-place-converter?����	#FF48A3FF.�Github�� $
��Left=�$��26��$��
379,84,0,0q�$	��Top=�$	��162�� J� RenderTransformOrigin$ 0.5,0.5&�z�V� .~��� �� ��  �� Angle$	 -90Q�T� �� -    ��Name$ ColorCheckbox��$��Include colors��$
��Left=�$��
474,49,0,0q�$	��Top=� d� 	IsChecked$
 TrueY�$��15��$��SemiExpanded&�
��������� -   $ UnionCheckbox��$9��3Include Union data (may prevent place from opening)��$
��Left=�$��
474,69,0,0q�$	��Top=�
��������� $��Advanced Settings��$��
474,10,0,0q�$	��Top=�$��16(�$��35��$
��Left=�$	��397��
��������� -	   $ ScriptConvertCheckbox��$L��FConvert scripts to old format (fixes places from being unable to open)��$
��Left=�$��
474,89,0,0q�$	��Top=�
�������$
 TrueY��� -
   $ ConvertFoldersCheckbox��$9��3Convert folders into models (fixes missing objects)��$
��Left=�$��474,109,0,0q�$	��Top=�
�������$
 TrueY��� $E��?If you don't know what you're doing, don't change anything here��$��
620,15,0,0q�$	��Top=�$��12(�$��35��$
��Left=�$	��237�����	#FFCBCBCB$��WrapWithOverflow=�$��SemiBold"��� -   $ ChangeRbxassetidCheckbox��$D��>Change rbxassetid to longer variant (fixes assets not loading)��$
��Left=�$��474,129,0,0q�$	��Top=�
�������$
 TrueY�