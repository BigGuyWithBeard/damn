using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BigB
{
    public partial class Form1 : Form
    {

        int directoryCount =0;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnScan_Click(object sender, EventArgs e)
        {
            string targetFolder = txtTargetFolder.Text;


            btnScan.Enabled=false;
            treeView1.Enabled=btnScan.Enabled ;
            txtTargetFolder.Enabled= btnScan.Enabled ;
            this.Cursor = Cursors.WaitCursor;
         Application.DoEvents();

            ScanDirectory(targetFolder);

 
            btnScan.Enabled=true;
            treeView1.Enabled=btnScan.Enabled ;
            txtTargetFolder.Enabled= btnScan.Enabled ;
            this.Cursor = Cursors.Default;
             Application.DoEvents();   


            MessageBox.Show("Completed");
        }

        private void ScanDirectory(string targetFolder)
        {

            // as we have no root nodes, create one for the targetFolder:
                  string text =System.IO.Path.GetFileName( targetFolder);
                string key = targetFolder;       
            TreeNode libraryNode = treeView1.Nodes.Add(text, key);

            ScanDirectory(targetFolder, libraryNode);

            libraryNode.Expand();
        }


            private void ScanDirectory(string targetFolder, TreeNode rootNode)
            {
           // scan the defined folder for folders:

            toolStripStatusLabel2.Text=$"Scanning '{targetFolder}'...";
            Application.DoEvents();

            foreach (string directory in System.IO.Directory.EnumerateDirectories(targetFolder))
            {
                string text =System.IO.Path.GetFileName( directory);
                string key = directory;
               TreeNode newNode= rootNode.Nodes.Add(key, text);
                directoryCount++;

 

                if(System.IO.Directory.GetDirectories(directory).Length > 0)
                {
  ScanDirectory(directory , newNode);
                }
                    toolStripStatusLabel1.Text=$"{directoryCount} directories scanned";
                  Application.DoEvents();          
            }
            }
        
    }
}
