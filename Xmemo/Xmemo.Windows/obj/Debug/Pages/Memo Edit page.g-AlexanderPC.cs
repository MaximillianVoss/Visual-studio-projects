﻿

#pragma checksum "C:\Users\Александр\OneDrive\Projects\Xmemo\Xmemo\Xmemo.Windows\Pages\Memo Edit page.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "7C1EAF1D5E26A2C076F31A6F40DC5ACE"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Xmemo
{
    partial class Memo_Edit_page : global::Windows.UI.Xaml.Controls.Page, global::Windows.UI.Xaml.Markup.IComponentConnector
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
 
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                #line 73 "..\..\Pages\Memo Edit page.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.show_btn_Click;
                 #line default
                 #line hidden
                break;
            case 2:
                #line 60 "..\..\Pages\Memo Edit page.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.Selector)(target)).SelectionChanged += this.Color_picker_SelectionChanged;
                 #line default
                 #line hidden
                break;
            case 3:
                #line 78 "..\..\Pages\Memo Edit page.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.Done_Click;
                 #line default
                 #line hidden
                break;
            }
            this._contentLoaded = true;
        }
    }
}

