   M S B A M L   `   `   `  ���� C  ?TOYOTA 2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:Alta_Keyboard.UControlAlta_Keyboard.UControl  3clr-namespace:toyota.UIControltoyota.UIControl  ;"clr-namespace:toyota.Assets.Themestoyota.Assets.Themes  ?$clr-namespace:TOYOTA_2.Assets.ThemesTOYOTA_2.Assets.Themes  '    !Alta_Keyboard.UControl.UIFacebook   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     6local$clr-namespace:Alta_Keyboard.UControl     a ]Awesomium.Windows.Controls, Version=1.7.4.2, Culture=neutral, PublicKeyToken=7a34e179b8b61c39,awe"http://schemas.awesomium.com/winfx  I EWpfAnimatedGif, Version=1.4.9.0, Culture=neutral, PublicKeyToken=null,gif"http://wpfanimatedgif.codeplex.com  .	UIControlclr-namespace:toyota.UIControl  /Themes"clr-namespace:toyota.Assets.Themes  2Themes1$clr-namespace:TOYOTA_2.Assets.Themes  -     >�Name$  UIWindow�� J� 	AllowDrop . .���� -    ��Name$ UIRoot��}�. ���� *Width+ �� ElementName$ UIWindow������ *Height+$ UIWindow��.+��� -   $ UIFrame1�� �� Left$ 0������ *Width+$ UIRoot������ *Height+$ UIRoot��.+��� $<��6/TOYOTA 2;component/Assets/Images/Background_frame.png������ *Width+$ UIRoot������ *Height+$ UIRoot��� 
������ $	��572��$	��659��$ 12�� � RowDefinitions �� $	��64* ��� $��2 ��� $
��323* ��� $
��183* �.+��� .+��� $7��1/TOYOTA 2;component/Assets/Images/header_user.png��$	��659��$��64��$��UniformToFill=��� -   $��45��$ 25��
 �� Top$ 9��$	��158��$0��*/TOYOTA 2;component/Assets/Images/logo.png�� J� IsManipulationEnabled . �� $��60��$��91��$	 396��$ 2������ *ImageSource+ J� OpacityMask �� 	 �� ImageSource$0	 */TOYOTA 2;component/Assets/Images/mask.png��+  %Awesomium.Windows.Controls.WebControl  -   
  Name$
 	UIWebview��$��2��$��2��$��0,0,0,10q��� -   $ UILayout_Action��$	 212��$	 395��$��208.5,100,208.5,37.5q�$��3��$y�Hidden=�.+��� $
��Left=�$��35��$��3��$
��Wrap=�$<��6Vui lòng chọn nút "Save Changes" để thay cover.��$	��Top=�$	��386��$��
Myriad Pro)�$��18.667(����	#FF949494$	 -70��$	 -40���� -   $	��101��$7��1/TOYOTA 2;component/Assets/Images/button_back.png��$��40��$ 10�� . �� -   $	��101��$7��1/TOYOTA 2;component/Assets/Images/button_next.png��$��40��$	 131�� . �� $��
QUAY LẠI�� J� IsHitTestVisible .  $��Center=�$��Center=�$ 23��$��22��$ 8��$��85��$��
Myriad Pro)�$��13(�
��������� $��HOÀN TẤT�� .  $��Center=�$��Center=�$	 131��$��22��$ 9��$��85��$��
Myriad Pro)�$��13(�
��������� -	    ��Name$ 
UILoadding��$��25��$��24�� . $	 205��$ 35��"  WpfAnimatedGif.ImageBehavior   AnimatedSource$  Assets/Images/loadding.gif���� -
   $ UIPopup_Facebook��$	��572��$	��659��$ 12��
������ $y�Hidden=�.+��� $	��572��$	��659��$<��6/TOYOTA 2;component/Assets/Images/Background_popup.png���� $	 173��$��WrapWithOverflow=� �� TextAlignment$ Center=�$	 133��$��
Myriad Pro)�
�������$��16(�$��SemiBold"�$	��325��$��WordEllipsis=�&   System.Windows.Media.TextOptions   TextHintingMode$ Fixed=��� $.�(Bạn có muốn cập nhật thông tin���� �� $=�7và công nghệ mới nhất từ  Toyota mỗi ngày?���� -   $��25��$	 360��$	 194��$	��100��$5��//TOYOTA 2;component/Assets/Images/button_ok.png�� . �� {�.  $	 360�� .  $��NoWrap=� �� Padding$ 0,6,0,0q�$��
ĐỒNG Ý��$	 194��$��25��$ Center=�$	��100��$��
Myriad Pro)�$��13(����	#FFF5E9E9�� -   $��25��$	 221��$	 194��$	��100��$9��3/TOYOTA 2;component/Assets/Images/button_cancel.png�� . �� $	 221�� .  $��NoWrap=�$ 0,6,0,0q�$��	ĐỂ SAU��$	 194��$��25��$ Center=�$	��100��$��
Myriad Pro)�$��13(����	#FF949494�� -   $ UIImage��$ 0��}�. $y�Visible=����� *Width+$ UIRoot������ *Height+$ UIRoot��.+��� $<��6/TOYOTA 2;component/Assets/Images/Background_frame.png������ *Width+$ UIRoot������ *Height+$ UIRoot���� $��53��$	 169��$	��340��$8��2/TOYOTA 2;component/Assets/Images/header_frame.png���� $%��Chọn khung hình bạn thích��$	 194��$	��296��$��53��$��Center=�$��Center=�$��
Myriad Pro)�$��16(�$
��Bold"�
��������� $	��142��$ 56��$	 106��$	��213��$��UniformToFill=����� *ImageSource+�� $	��178��$ 52��$ 74��$	��222��$E��?/TOYOTA 2;component/Assets/Images/background_image_original.png���� $��Hình ảnh gốc��$��Center=�$��Center=�$ 52��$ 74��$	��222��$��Italic$����	#FF949494$��
Myriad Pro)�$��14(��� $	��188��$	 338��$ 74��$	��290��.+�+   %toyota.Assets.Themes.Theme_Timeline_0  -     Name$ UITimeline_View��+   %toyota.Assets.Themes.Theme_Timeline_0   TouchBehavior$ TouchBehavior_Event��$	��179��$ 4��$	��283��$ 4���� $+��%Assets/Themes/Images/border_image.png��$	��188��$	��290�� .  {�.  �� -   $	��167��$	 378��$	 106��$	��262��$C��=/TOYOTA 2;component/Assets/Images/Background_image_design.png�� . $y�Hidden=�   toyota.UIControl.bar  -     Name$
 bar1��$	 410��$	 263��$��10��$	��150���� -   $ UIRight1��$��45��$	 620��$	 145��$��54��$8��2/TOYOTA 2;component/Assets/Images/button_right.png�� . �� -   $ UILeft1��$��45��$	 294��$	 145��$��54��$7��1/TOYOTA 2;component/Assets/Images/button_left.png��$y�Hidden=� . (   "toyota.Assets.Themes.Theme_Cover_0  -     Name$ UICover_View��(   "toyota.Assets.Themes.Theme_Cover_0   TouchBehavior$ TouchBehavior_Event��$	 292��$ 69��$	��210��$	��548���� $	��216��$ 66��$	 290��$	��554��{�.   .  $0��*Assets/Themes/Images/border_image_land.png��  -   $
 bar2��$	 256��$	 511��$��10��$	��170���� -   $ UIRight2��$��45��$	 610��$	 374��$��54��$8��2/TOYOTA 2;component/Assets/Images/button_right.png�� . �� -   $ UILeft2��$��45��$ 20��$	 374��$��54��$7��1/TOYOTA 2;component/Assets/Images/button_left.png��$y�Hidden=� . �� -   $ UIFooter_Facebook��$	��663��$��61��$ 10��$	 511��$y�Visible=�.+��� -   $ UIImagePost��$��33��$	 153��$ 18��$	��170��$9��3/TOYOTA 2;component/Assets/Images/button_normal.png�� . �� $��CHIA SẺ LÊN FACEBOOK��$	 153��$ 18�� .  $	��170��$��33��$��Center=�$��Center=�
�������$��
Myriad Pro)��� -   $ UIImageCover��$��33��$	 353��$ 18��$	��195��$6��0/TOYOTA 2;component/Assets/Images/button_big.png�� . �� $ ��ĐẶT LÀM FACEBOOK COVER��$	 353�� .  $ 18��$	��195��$��33��$��Center=�$��Center=�
�������$��
Myriad Pro)��� -   $ UIFooter_Mail��$	��663��$��61��$ 10��$	 511��$y�	Collapsed=�.+��� -   $ UIButton_SendEmail��$��33��$	 250��$ 18��$	��170��$9��3/TOYOTA 2;component/Assets/Images/button_normal.png�� . �� $��
GỬI MAIL��$��Center=�$��Center=�$	 255��$ 21��$��30��$	��168��
�������$��
Myriad Pro)�{�.   .  �� -   $ UIFrame4��$
 -683������ *Width+$ UIRoot������ *Height+$ UIRoot��.+��� $<��6/TOYOTA 2;component/Assets/Images/Background_popup.png��$ 0������ *Width+$ UIRoot������ *Height+$ UIRoot���� $	 143��$
��Wrap=�$ Center=�$	 170��
�������$��
Myriad Pro)�$��17(�$��SemiBold"�$ 5q� �� 
LineHeight$ 20��$	��376���� $4�.Cảm ơn bạn đã tham gia chương trình!���� �� $D�>Vui lòng liên hệ với nhân viên của chúng tôi để���� �� $%�nhận hình ảnh của bạn.���� -   $��33��$	 351��$	 266��$	��100��$7��1/TOYOTA 2;component/Assets/Images/button_next.png�� . �� $��HOÀN TẤT��$	 351��$	 267��$��32��$��90�� .  $��Center=�$��Center=�
�������$��
Myriad Pro)�$��13(��� -   $��33��$	 206��$	 266��$	��100��$7��1/TOYOTA 2;component/Assets/Images/button_back.png�� . �� $��
QUAY LẠI��$	 219��$	 267��$��32��$��87�� .  $��Center=�$��Center=�
�������$��
Myriad Pro)�$��13(��� -   $ UIFrame5��$
 -683������ *Width+$ UIRoot������ *Height+$ UIRoot��.+��� $<��6/TOYOTA 2;component/Assets/Images/Background_popup.png������ *Width+$ UIRoot������ *Height+$ UIRoot���� -     ��Name$ UIMsg��$	 129��$��WrapWithOverflow=�$Y��SHình ảnh của bạn đã được in ra! Bạn không thể đổi theme khác!��$ Center=�$	 174��
�������$��
Myriad Pro)�$��15(�$��SemiBold"�$ 5q�$ 20���� -!   $��33��$	 452��$	 209��$	��100��$7��1/TOYOTA 2;component/Assets/Images/button_next.png�� . �� $��OK��$	 452��$	 210��$��32��$��90�� .  $��Center=�$��Center=�
�������$��
Myriad Pro)�$��13(��� -"   $ UIFrame6��$
 -683��}�. ���� *Width+$ UIRoot������ *Height+$ UIRoot��.+��� $<��6/TOYOTA 2;component/Assets/Images/Background_popup.png������ *Width+$ UIRoot������ *Height+$ UIRoot���� $	 139��$
��Wrap=�$ Center=�$	 169��
�������$��
Myriad Pro)�$��17(�$��SemiBold"�$ 5q�$ 20��$	��411���� $4�.Cảm ơn bạn đã tham gia chương trình!���� �� $B�<Hình ảnh đã được gửi vào hộp thư của bạn.���� �� $D�>Vui lòng liên hệ với nhân viên của chúng tôi để���� �� $%�nhận hình ảnh của bạn.���� -#   $��33��$	 296��$	 269��$	��100��$7��1/TOYOTA 2;component/Assets/Images/button_next.png�� . �� $��HOÀN TẤT��$	 296��$	 270��$��32��$��90�� .  $��Center=�$��Center=�
�������$��
Myriad Pro)�$��13(��� -$   $y�Hidden=�$��33��$	 218��$	 266��$	��100��$7��1/TOYOTA 2;component/Assets/Images/button_back.png�� . �� $y�Hidden=�$��
QUAY LẠI��$	 231��$	 267��$��32��$��87�� .  $��Center=�$��Center=�
�������$��
Myriad Pro)�$��13(��� -%   $ UIFrame0��$
 -683��}�. ���� *Width+$ UIRoot������ *Height+$ UIRoot��.+��� $<��6/TOYOTA 2;component/Assets/Images/Background_frame.png������ *Width+$ UIRoot������ *Height+$ UIRoot���� $	��659��$��64��.+��� $7��1/TOYOTA 2;component/Assets/Images/header_user.png��$	��659��$��64��$��UniformToFill=�$ 12���� -&   $��45��$ 25��$ 9��$	��158��$0��*/TOYOTA 2;component/Assets/Images/logo.png�� . �� $��60��$��91��$	 396��$ 2������ *ImageSource+ �� $0	 */TOYOTA 2;component/Assets/Images/mask.png���� $ Center=�$
��Left=�$��66��$��2��$
��Wrap=�$	��Top=�$	��490�����	#FF949494$��
Myriad Pro)�$��16(�$ 18��$ 71.667��$	 214���� $�CHÀO MỪNG BẠN���� $� ���� -'    ��Name$ 
UITextName��$�Giang��
���  ���� �� $G�AĐÃ THAM GIA HOẠT ĐỘNG CHỤP HÌNH TẠI TOYOTA VIỆT NAM���� -(   $
��Left=�$��40��$��2��$	��Top=�$	��110��$#��Assets/Images/button_next.png��$	 360��$	 282�� . �� -)   $
��Left=�$��40��$��2��$	��Top=�$	��110��$#��Assets/Images/button_back.png��$	 164��$	 280�� . �� $��TIẾP THEO�� .  {�.  $��Center=�$��Center=�$��30��$	 360��$	 282��$	��100��
�������$��
Myriad Pro)��� $��
QUAY LẠI�� .  {�.  $��Center=�$��Center=�$��30��$	 174��$	 283��$	��100��
�������$��
Myriad Pro)�  -*   $ UIImage_Timeline��$	 683��$ 0��$	��768��$
��1152��  -+   $ UIImage_Cover��$	 688��$	��316��$	��852��$ 3���� -,   $	����$��Center=�$��Center=�$��29��$	 627��$ 11��$��35��$3��-/TOYOTA 2;component/Assets/Fonts/#FontAwesome)�$��21.333(����	#FFFB0000 . ��Z� 