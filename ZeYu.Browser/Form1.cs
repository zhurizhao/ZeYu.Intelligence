using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WebKit;

namespace ZeYu.Browser
{
    public partial class Form1 : Form
    {
        string url="http://www.cocopass.com";
        string title = "泽宇智能管理工具";
        WebKit.WebKitBrowser browser = new WebKit.WebKitBrowser();
        public Form1()
        {
            InitializeComponent();
            init();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.browser.Navigate(this.url);
            this.browser.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(webKitBrowser1_DocumentCompleted);
        }
        void webKitBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            //this.browser.GetScriptManager.ScriptObject = new myClass();
        }

        void init()
        {
            this.title= System.Configuration.ConfigurationSettings.AppSettings.Get("title");
            this.url= System.Configuration.ConfigurationSettings.AppSettings.Get("url");
            this.Text = title;
            browser = new WebKitBrowser();
            //browser.Dock = DockStyle.Fill;
            //browser.IsScriptingEnabled = true;
            this.Controls.Add(browser);
        }
    }
}
