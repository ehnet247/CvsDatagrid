using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data;

namespace CvsDatagrid
{
    public class CvsReader
    {
        public string FileName { get; set; }
        public string[] Headers { get; private set; }
        public List<string[]> Rows{ get; private set; }
        public bool Error { get; private set; }
        public CvsReader(string fileName)
        {
            FileName = fileName;
            Error = false;
            if (File.Exists(FileName))
            {
                List<string>? lines = null;
                try
                {
                    lines = File.ReadLines(fileName).ToList();
                }
                catch (Exception ex)
                {
                    Error = true;
                }
                if ((!Error) && (lines != null) && (!string.IsNullOrEmpty(lines[0])))
                {
                    try
                    {
                        Headers = lines[0].Split(";");
                    }
                    catch (FormatException ex)
                    {
                        Error = true;
                    }
                }
                // Continue with data rows
                if (!Error)
                {
                    Rows = new List<string[]>();
                    for (int i = 1; i < lines.Count; i++)
                    {
                        string[]? row = null;
                        try
                        {
                            row = lines[i].Split(";");
                        }
                        catch (FormatException ex)
                        {
                            Error = true;
                        }
                        if ((!Error) && (row != null))
                        {
                            Rows.Add(row);
                        }
                    }
                }
            }
            else
            {
                Error = true;
            }
        }
    }
}
