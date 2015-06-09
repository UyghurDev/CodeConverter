<%@ Page Title="كود ئالماشتۇرۇش(Code Convert)" Language="C#" MasterPageFile="~/Common/Public.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="CodeConverter_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  <table cellpadding="0" cellspacing="0" style="width: 100%">
        <tr>
            <td style="text-align: center">
                <br />
                كود ئالماشتۇرۇش<br />
                <hr class="HorzentalLineHeader" dir="rtl" />
            </td>
        </tr>
        <tr>
            <td>
                <table cellpadding="0" dir="rtl" 
                    style="border-collapse: collapse; width: 600px; border: 1px solid #d5ddf3" align="center">
                    <tr>
                        <td >
                            <asp:Label ID="lblCodeTable" runat="server" Text="ئالماشتۇرىدىغان كود شەكلىنى تاللاڭ:"></asp:Label>
                            <asp:DropDownList ID="ddlCodeTables" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td >
                            <asp:TextBox ID="txtSource" runat="server" Height="200px" TextMode="MultiLine" 
                                Width="600px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center">
                            <asp:Button ID="btnConvert" runat="server" OnClick="Convert_Click" Text="ئالماشتۇرۇش" />
                        </td>
                                        </tr>
                                        <tr>
                                            <td bgcolor="#CCCCFF">
                                                <asp:Literal ID="ltrlConvertedText" runat="server"></asp:Literal>
                                            </td>
                                        </tr>
                                    </table>
               
                <br />
                <p style="text-align:right;">
                <br />
                مۇناسىۋەتلىك ئۇلىنىشلار<hr align="right" class="HorzentalLineLinks" />
                
                </p>
                <p style="text-align:right;">
                <a href="https://github.com/Sarwan/UniversalCodeConverter" style="text-align:right;">ئۇنۋىرسال كود ئالماشتۇرغۇچ ئەسلى كودى</a><br />
                <a href="https://github.com/Sarwan/LatinCodeConverter" style="text-align:right;">لاتىنچە كود ئالماشتۇرغۇچ ئەسلى كودى</a>
                </p>
            </td>
        </tr>
    </table>
     <a href="https://github.com/Sarwan/UniversalCodeConverter"><img style="position: absolute; top: 0; left: 0; border: 0;" src="forkme_left_green_007200.png" alt="Universal Code Converter Source Code"></a>
     <a href="https://github.com/Sarwan/LatinCodeConverter"><img style="position: absolute; top: 0; right: 0; border: 0;" src="forkme_right_darkblue_121621.png" alt="Latin Code Converter Source Coe"></a>
</asp:Content>

