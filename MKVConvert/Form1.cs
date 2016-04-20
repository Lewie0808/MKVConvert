using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace MKVConvert
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;

        }

        private void btnSD_Click(object sender, EventArgs e)
        {
            EncoderSD();   
        }


        //private void EncoderSD(int width, int height, int bitrate, bool copyAudio = true)
        private void EncoderSD(bool copyAudio = true)
        {
            string[] files = Directory.GetFiles(txtBxSource.Text, "*.*");

            foreach (string currentFile in files)
            {
                if (currentFile.EndsWith("avi") || currentFile.EndsWith("mp4") || currentFile.EndsWith("mkv") || currentFile.EndsWith("ts"))
                {
                    
                    // Create launch settings
                    ProcessStartInfo psi = new ProcessStartInfo();
                    psi.FileName = @"C:\Program Files\Handbrake\HandBrakeCLI.exe";
                    if (copyAudio)
                    {
                        psi.Arguments = "-i \"" + currentFile + "\" -t 1 -o \"" + currentFile + "-output.mkv\" -f mkv -O -w " + txtBxSDWid.Text + " -l " + txtBxSDHei.Text + " --modulus 8 -e x264 -b " + txtBxSDBR.Text + " -2 --vfr -a 1 -E faac -B 160 -6 dpl2 -R Auto -D 0 --gain=0 --audio-copy-mask none --audio-fallback ffac3 -x weightp=1:subq=10:rc-lookahead=10:trellis=2:b-adapt=2:psy-rd=1.00,0.10 --verbose=1";
                    }
                    else
                    {
                        //psi.Arguments = "-i \"" + currentFile + "\" -t 1 -o \"" + currentFile + "-output.mkv\" -f mkv -O -w " + width + " -l " + height + " --modulus 8 -e x264 -b " + bitrate + " -2 --vfr -a 1 -E faac -B 160 -6 dpl2 -R Auto -D 0 --gain=0 --audio-copy-mask none --audio-fallback ffac3 -x weightp=1:subq=10:rc-lookahead=10:trellis=2:b-adapt=2:psy-rd=1.00,0.10 --verbose=1";
                    }

                    // Create process based on settings
                    Process prc = new Process();
                    prc.StartInfo = psi;
                    prc.Start();

                    // Wait for process to exit
                    while (!prc.HasExited)
                    {
                        Thread.Sleep(500);
                    }

                    // Sort file out
                    if (File.Exists(currentFile + "-output.mkv"))
                    {
                        if (Directory.Exists(@"F:\Convert"))
                        {
                            File.Move(currentFile + "-output.mkv", @"F:\Convert\" + Path.GetFileName(currentFile));
                        }
                        else
                        {
                            MessageBox.Show("Convert folder is missing or inaccessible");
                        }

                        string[] filePaths = Directory.GetFiles(txtBxSource.Text);
                        foreach (string filePath in filePaths)
                        {
                            File.Delete(filePath);
                        }
                    }

                    string sourcePath = @"F:\Convert\";
                    string directoryPath = string.Empty;

                    directoryPath = (txtBxSource.Text.EndsWith("\\")) ? Path.GetDirectoryName(txtBxSource.Text) : Path.GetDirectoryName(txtBxSource.Text + "\\");

                    directoryPath += "\\";

                    foreach (var srcPath in Directory.GetFiles(sourcePath, "*.*", SearchOption.AllDirectories))
                    {
                        File.Copy(srcPath, srcPath.Replace(sourcePath, directoryPath), true);
                    }
                }
            }
        }

        private void btnHD_Click(object sender, EventArgs e)
        {
            EncoderHD();
        }

        private void EncoderHD(bool copyAudio = true)
        {
            string[] files = Directory.GetFiles(txtBxSource.Text, "*.*");

            foreach (string currentFile in files)
            {
                if (currentFile.EndsWith("avi") || currentFile.EndsWith("mp4") || currentFile.EndsWith("mkv"))
                {
                    // Create launch settings
                    ProcessStartInfo psi = new ProcessStartInfo();
                    psi.FileName = @"C:\Program Files\Handbrake\HandBrakeCLI.exe";
                    if (copyAudio)
                    {
                        psi.Arguments = "-i \"" + currentFile + "\" -t 1 -o \"" + currentFile + "-output.mkv\" -f mkv -O -e x264 -b " + txtBxHDBR.Text + " -2 --vfr -a 1 -E faac -B 160 -6 dpl2 -R Auto -D 0 --gain=0 --audio-copy-mask none --audio-fallback ffac3 -x weightp=1:subq=10:rc-lookahead=10:trellis=2:b-adapt=2:psy-rd=1.00,0.10 --verbose=1";
                    }
                    else
                    {
                        
                    }

                    // Create process based on settings
                    Process prc = new Process();
                    prc.StartInfo = psi;
                    prc.Start();

                    // Wait for process to exit
                    while (!prc.HasExited)
                    {
                        Thread.Sleep(500);
                    }

                    // Sort file out
                    if (File.Exists(currentFile + "-output.mkv"))
                    {
                        if (Directory.Exists(@"F:\Convert"))
                        {
                            File.Move(currentFile + "-output.mkv", @"F:\Convert\" + Path.GetFileName(currentFile));
                        }
                        else
                        {
                            MessageBox.Show("Convert folder is missing or inaccessible");
                        }

                        string[] filePaths = Directory.GetFiles(txtBxSource.Text);
                        foreach (string filePath in filePaths)
                        {
                            File.Delete(filePath);
                        }
                    }
                    //string sourcePath = @"F:\Convert";
                    //string targetPath = txtBxSource.Text;
                    //if (!Directory.Exists(targetPath))
                    //{
                    //    Directory.CreateDirectory(targetPath);
                    //}
                    //foreach (var srcPath in Directory.GetFiles(sourcePath))
                    //{
                    //    File.Copy(srcPath, srcPath.Replace(sourcePath, targetPath), true);
                    //}
                }
            }
        }
        private void btnHD2HD_Click(object sender, EventArgs e)
        {
            EncoderHD2HD();
        }

        private void EncoderHD2HD(bool copyAudio = true)
        {
            string[] files = Directory.GetFiles(txtBxSource.Text, "*.*");

            foreach (string currentFile in files)
            {
                if (currentFile.EndsWith("avi") || currentFile.EndsWith("mp4") || currentFile.EndsWith("mkv"))
                {
                    // Create launch settings
                    ProcessStartInfo psi = new ProcessStartInfo();
                    psi.FileName = @"C:\Program Files\Handbrake\HandBrakeCLI.exe";
                    if (copyAudio)
                    {
                        psi.Arguments = "-i \"" + currentFile + "\" -t 1 -o \"" + currentFile + "-output.mkv\" -f mkv -O -w " + txtBxHD2HDWid + " -l " + txtBxHD2HDHei + " --modulus 8 -e x264 -b " + txtBxHD2HDBR + " -2 --vfr -a 1 -E faac -B 160 -6 dpl2 -R Auto -D 0 --gain=0 --audio-copy-mask none --audio-fallback ffac3 -x weightp=1:subq=10:rc-lookahead=10:trellis=2:b-adapt=2:psy-rd=1.00,0.10 --verbose=1";
                    }
                    else
                    {

                    }

                    // Create process based on settings
                    Process prc = new Process();
                    prc.StartInfo = psi;
                    prc.Start();

                    // Wait for process to exit
                    while (!prc.HasExited)
                    {
                        Thread.Sleep(500);
                    }

                    // Sort file out
                    if (File.Exists(currentFile + "-output.mkv"))
                    {
                        if (Directory.Exists(@"F:\Convert"))
                        {
                            File.Move(currentFile + "-output.mkv", @"F:\Convert\" + Path.GetFileName(currentFile));
                        }
                        else
                        {
                            MessageBox.Show("Convert folder is missing or inaccessible");
                        }

                        string[] filePaths = Directory.GetFiles(txtBxSource.Text);
                        foreach (string filePath in filePaths)
                        {
                            File.Delete(filePath);
                        }
                    }
                    //string sourcePath = @"F:\Convert";
                    //string targetPath = txtBxSource.Text;
                    //if (!Directory.Exists(targetPath))
                    //{
                    //    Directory.CreateDirectory(targetPath);
                    //}
                    //foreach (var srcPath in Directory.GetFiles(sourcePath))
                    //{
                    //    File.Copy(srcPath, srcPath.Replace(sourcePath, targetPath), true);
                    //}
                }
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnSourceBrow_Click(object sender, EventArgs e)
        {
            string folderPath = "";
            FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                folderPath = folderBrowserDialog1.SelectedPath;
                txtBxSource.Text = folderPath;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
