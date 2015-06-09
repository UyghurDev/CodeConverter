using System;
using System.Collections.Generic;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class CodeConverter_Default : System.Web.UI.Page
{
    string strPath="";
    protected void Page_Load(object sender, EventArgs e)
    {
        strPath = Server.MapPath("") + "\\CodeTables\\";

        if (!Page.IsPostBack)
        {
           DirectoryInfo dir = new System.IO.DirectoryInfo(strPath);
           FileInfo[] fis = dir.GetFiles("*.xml");
         
           foreach (FileInfo fi in fis)
           {
               net.UyghurDev.Text.CodeConvert cc = new net.UyghurDev.Text.CodeConvert(strPath + "\\" + fi.Name);
               ListItem li=new ListItem(cc.Name,fi.Name);
               ddlCodeTables.Items.Add(li);
           }
        }
    }

    protected void Convert_Click(object sender, EventArgs e)
    {
        net.UyghurDev.Text.CodeConvert cc = new net.UyghurDev.Text.CodeConvert(strPath + "\\" + ddlCodeTables.SelectedValue);
        ltrlConvertedText.Text = cc.ToConvert(txtSource.Text.Replace("\n","<br>"));
    }
}
