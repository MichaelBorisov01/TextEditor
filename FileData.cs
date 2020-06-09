using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace WordPadMin
{
        class FileData
        {
            public FileInfo file;

            private string content;

            public string Content
            {
                get => content;
                set
                {
                    content = value;
                    ContentUpdate?.Invoke(this, value);
                }
            }

            public EventHandler<string> ContentUpdate;

            public FileData()
            {
                file = new FileInfo("Unnamed");
                Content = "";
            }

            public FileData(string filename)
            {
                file = new FileInfo(filename);
                Content = file.OpenText().ReadToEnd();
            }
        }
}

