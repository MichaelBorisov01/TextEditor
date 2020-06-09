using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace WordPadMin
{
    public partial class Form1 : Form
    {
       // private FileData data = new FileData();
    
        public Form1()
        {
            InitializeComponent();
            InitHandlers();
            FileChanged += UpdateFilePath;
           
        }

        private void UpdateFilePath(object sender, string args)
        {
            this.Text = args;
        }
        private void InitHandlers()
        {
         ContentUpdate += OnContentUpdate;
        }
        private void OnContentUpdate(object sender, string args)
        {
            richTextBox1.Text = args;
        }
        private FileInfo currentFile = null;
        private FileInfo CurrentFile
        {
            get { return currentFile; }
            set
            {
                currentFile = value;
                FileChanged.Invoke(this, currentFile.FullName);
            }
        }
        private string content;
        private SaveFileDialog saveDialog = new SaveFileDialog();
        private OpenFileDialog openDialog = new OpenFileDialog();
        public RichTextBox richText = new RichTextBox();
        private bool saved = true;


        //private EventHandler<string> ContentLoaded;
        public EventHandler<string> ContentUpdate;
        public EventHandler<string> FileChanged;

        public string Content
        {
            get { return content; }
            set
            {
                content = value;
                ContentUpdate.Invoke(this, content);
            }
        }

       

        //  private bool showdialog = true;


        private void Form1_Load(object sender, EventArgs e)
        {
           
        }

        private void Clear_Click(object sender, EventArgs e)
        {
            richTextBox1.ResetText();
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        

           private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {   openDialog.Filter = "Text Files (*.txt)|*.txt|RTF(*.rtf)|*.rtf |FB2(*.fb2)| *.fb2|DOCX(*.docx)|*.docx|All files(*.*)|*.*";
            openDialog.AddExtension = true;
            openDialog.RestoreDirectory = true;
            openDialog.FilterIndex = 5;
            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.ResetText();
                // Code to write the stream goes here.
                CurrentFile = new FileInfo(openDialog.FileName);
                
               StreamReader  sr = currentFile.OpenText();
               
                string line = sr.ReadLine();
                sr.Close();
                sr = currentFile.OpenText();
                if (line.StartsWith("{\\rtf"))
                {
                    richTextBox1.Rtf = sr.ReadToEnd();
                }
                else
                {
                    Content = sr.ReadToEnd();
                }
                content = richTextBox1.Rtf;
                sr.Close();
            }
        }

        /*  //
             FileInfo selected = new FileInfo("Unnamed.txt");
           StreamReader reader = File.OpenText(filename);
             while (!reader.EndOfStream)
             {
                 richTextBox1.Text += reader.ReadLine() + '\n';
             }
            richTextBox1.LoadFile(openDialog.FileName, RichTextBoxStreamType.PlainText);
             string fileText = System.IO.File.ReadAllText(filename, Encoding.GetEncoding(1251));
             richTextBox1.Text = fileText;
            */


        /* public void SaveAs()
         {
             SaveFileDialog saveDialog = new SaveFileDialog();
             string filename = saveDialog.FileName;
             Stream stream;
             if(saveDialog.ShowDialog()==DialogResult.OK)
             {
                 if((stream=saveDialog.OpenFile()) !=null)
                 {
                     stream.Close();
                     currentFile = new FileInfo(saveDialog.FileName);
                     Save();
                 }
             }
         }*/

        private void SaveAs(object sender, EventArgs e)
        {
            
                  
                saveDialog.Filter = "Text Files (*.txt)|*.txt|RTF(*.rtf)|*.rtf |FB2(*.fb2)| *.fb2|DOCX(*.docx)|*.docx|All files(*.*)|*.*";
                saveDialog.AddExtension = true;
                saveDialog.FilterIndex = 5;
                saveDialog.RestoreDirectory = true;
                                        
            Stream stream;

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                if ((stream = saveDialog.OpenFile()) != null)
                {
                    // Code to write the stream goes here.
                    stream.Close();
                    CurrentFile = new FileInfo(saveDialog.FileName);
                    Save(sender,e);
                 
                }
            }
           
           // MessageBox.Show("Файл сохранен");
            
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {            richTextBox1.Copy();        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {            richTextBox1.Paste();        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {            richTextBox1.Cut();        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {            richTextBox1.Undo();        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {            richTextBox1.SelectAll();        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        { 
                System.Windows.Forms.Application.Exit();
                System.Diagnostics.Process.Start(@"C:/Users/Shatcung/source/repos/WordPadMin/bin/Debug/WordPadMin.exe");

            
        }
       
        
        private void richTextBox1_TextChanged(object sender, EventArgs e)
        { }

  
        private void Save(object sender, EventArgs e)
        {
           if (currentFile == null || !currentFile.Exists)
            {
                SaveAs(sender,e);
                return;
            }
            StreamWriter writer = currentFile.CreateText();
            if (currentFile.Extension.ToUpper() == ".RTF")
            {
                writer.Write(richTextBox1.Rtf);
           
            }
            else
            {
                writer.Write(richTextBox1.Text);
              
            }
            writer.Close();
            
        }

        public void NewFile(object sender, EventArgs e)
        {
            if (!saved)
            {
                CurrentFile = new FileInfo("Unnamed");
            }
            else
            {
                DialogResult result = MessageBox.Show("Are you sure want to exit file?", "Unnamed", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                switch (result){
                    case DialogResult.Yes:
                        Save(sender, e);
                        break;
                    case DialogResult.No:
                        saved = true;
                        CurrentFile = new FileInfo("Unname");
                        break;
                    case DialogResult.Cancel:
                        return;
                }
                CurrentFile = new FileInfo("Unnamed");
                richTextBox1.Text = " ";
                saved = true;
            }
        }
 
    }

   


}
