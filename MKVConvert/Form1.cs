using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using System.Text;

namespace MKVConvert
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private void btnSD_Click(object sender, EventArgs e)
        {
            EncoderSD();
        }

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
                        //psi.Arguments = "-i \"" + currentFile + "\" -t 1 -o \"" + currentFile + "-output.mkv\" -f mkv -O -w " + txtBxSDWid.Text + " -l " + txtBxSDHei.Text + " --modulus 8 -e x264 -b " + txtBxSDBR.Text + 
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

                    if (prc.HasExited)
                    {
                        if (prc.ExitCode == 0)
                        {
                            // Sort file out
                            if (File.Exists(currentFile + "-output.mkv"))
                            {
                                File.Delete(currentFile);

                                string finalName = string.Empty;
                                if (currentFile.EndsWith("ts"))
                                    finalName = currentFile.Replace(currentFile.Substring((currentFile.Length - 2), 2), "mkv");
                                else
                                    finalName = currentFile.Replace(currentFile.Substring((currentFile.Length - 3), 3), "mkv");

                                File.Move(currentFile + "-output.mkv", finalName);
                            }
                        }
                        else
                        {
                            if (Directory.Exists(@"F:\Failed\"))
                            {
                                File.Create(@"F:\Failed\" + currentFile + "-failed.mkv");
                            }
                            else
                            {
                                //MessageBox.Show("Unable to convert file");
                            }
                        }
                    }
                }
            }

            string[] afterDir = Directory.GetFiles(txtBxSource.Text, "*.*");
            long afterSize = sizes(afterDir);

            txtBxAfter.Text = InGB(afterSize);

            MessageBox.Show("Job completed");
        }

        private static long sizes(string[] path)
        {
            long temp = 0;

            foreach (string name in path)
            {
                FileInfo info = new FileInfo(name);
                temp += info.Length;
            }

            return temp;
        }

        private static string InGB(long bytes)
        {
            string[] Suffix = { "B", "KB", "MB", "GB", "TB" };
            int i;
            double dblSByte = bytes;
            for (i = 0; i < Suffix.Length && bytes >= 1024; i++, bytes /= 1024)
            {
                dblSByte = bytes / 1024.0;
            }

            return String.Format("{0:0.##} {1}", dblSByte, Suffix[i]);
        }

        private void btnHD_Click(object sender, EventArgs e)
        {
            EncoderHD();
        }

        private void EncoderHD(bool copyAudio = true)
        {
            txtBxBefore.Text = "";
            txtBxAfter.Text = "";

            string[] beforeDir = Directory.GetFiles(txtBxSource.Text, "*.*");
            long beforeSize = sizes(beforeDir);

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

                    if (prc.HasExited)
                    {
                        if (prc.ExitCode == 0)
                        {
                            // Sort file out
                            if (File.Exists(currentFile + "-output.mkv"))
                            {
                                File.Delete(currentFile);

                                string finalName = string.Empty;
                                if (currentFile.EndsWith("ts"))
                                    finalName = currentFile.Replace(currentFile.Substring((currentFile.Length - 2), 2), "mkv");
                                else
                                    finalName = currentFile.Replace(currentFile.Substring((currentFile.Length - 3), 3), "mkv");

                                File.Move(currentFile + "-output.mkv", finalName);
                            }
                        }
                        else
                        {
                            if (Directory.Exists(@"F:\Failed\"))
                            {
                                File.Create(@"F:\Failed\" + currentFile + "-failed.mkv");
                            }
                            else
                            {
                                MessageBox.Show("Unable to convert file");
                            }
                        }
                    }

                    string[] afterDir = Directory.GetFiles(txtBxSource.Text, "*.*");
                    long afterSize = sizes(afterDir);
                    
                    txtBxAfter.Text = InGB(afterSize);
                }
            }
        }
        private void btnHD2HD_Click(object sender, EventArgs e)
        {
            EncoderHD2HD();
        }

        private void EncoderHD2HD(bool copyAudio = true)
        {
            txtBxBefore.Text = "";
            txtBxAfter.Text = "";

            string[] beforeDir = Directory.GetFiles(txtBxSource.Text, "*.*");
            long beforeSize = sizes(beforeDir);

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

                    if (prc.HasExited)
                    {
                        if (prc.ExitCode == 0)
                        {
                            // Sort file out
                            if (File.Exists(currentFile + "-output.mkv"))
                            {
                                File.Delete(currentFile);

                                string finalName = string.Empty;
                                if (currentFile.EndsWith("ts"))
                                    finalName = currentFile.Replace(currentFile.Substring((currentFile.Length - 2), 2), "mkv");
                                else
                                    finalName = currentFile.Replace(currentFile.Substring((currentFile.Length - 3), 3), "mkv");

                                File.Move(currentFile + "-output.mkv", finalName);
                            }
                        }
                        else
                        {
                            if (Directory.Exists(@"F:\Failed\"))
                            {
                                File.Create(@"F:\Failed\" + currentFile + "-failed.mkv");
                            }
                            else
                            {
                                MessageBox.Show("Unable to convert file");
                            }
                        }
                    }

                    string[] afterDir = Directory.GetFiles(txtBxSource.Text, "*.*");
                    long afterSize = sizes(afterDir);

                    txtBxAfter.Text = InGB(afterSize);
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
            string[] beforeDir = Directory.GetFiles(txtBxSource.Text, "*.*");
            long beforeSize = sizes(beforeDir);

            txtBxBefore.Text = InGB(beforeSize);

            txtBxAfter.Clear();
        }

    }
}