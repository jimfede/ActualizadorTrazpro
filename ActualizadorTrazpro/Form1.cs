using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ActualizadorTrazpro
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label1.Text = "";
            progressBar1.Value = 0;
            CopyFolderContents("\\\\10.26.32.13\\ActualizacionTrazPro\\EjecutablesRotulos\\Desosado", "c:\\TrazproTest");
            /*CopyFolderContents("\\\\10.26.32.13\\ActualizacionTrazPro\\EjecutablesRotulos\\Desosado", "c:\\TrazproTest");
            CopyFolderContents("\\\\10.26.32.13\\ActualizacionTrazPro\\EjecutablesTrazPro", "c:\\TrazproTest");*/

        }

        public void CopyFolderContents(string sourceFolder, string destinationFolder)
        {
            CopyFolderContents(sourceFolder, destinationFolder, "*.*", false, false);
        }

        public void CopyFolderContents(string sourceFolder, string destinationFolder, string mask)
        {
            CopyFolderContents(sourceFolder, destinationFolder, mask, false, false);
        }

        public void CopyFolderContents(string sourceFolder, string destinationFolder, string mask, Boolean createFolders, Boolean recurseFolders)
        {
            try
            {

                if (!sourceFolder.EndsWith(@"\")) { sourceFolder += @"\"; }
                if (!destinationFolder.EndsWith(@"\")) { destinationFolder += @"\"; }

                var exDir = sourceFolder;
                var dir = new DirectoryInfo(exDir);
                SearchOption so = (recurseFolders ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);

                foreach (string sourceFile in Directory.GetFiles(dir.ToString(), mask, so))
                {
                    FileInfo srcFile = new FileInfo(sourceFile);
                    string srcFileName = srcFile.Name;

                    // Create a destination that matches the source structure
                    FileInfo destFile = new FileInfo(destinationFolder + srcFile.FullName.Replace(sourceFolder, ""));

                    if (!Directory.Exists(destFile.DirectoryName) && createFolders)
                    {
                        Directory.CreateDirectory(destFile.DirectoryName);
                    }

                    if (srcFile.LastWriteTime > destFile.LastWriteTime || !destFile.Exists)
                    {
                        File.Copy(srcFile.FullName, destFile.FullName, true);

                        progressBar1.PerformStep();
                    }
                                        
                }

                label1.Text = "Actualización completada";
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message + Environment.NewLine + Environment.NewLine + ex.StackTrace);
            }
        }

    }
}
