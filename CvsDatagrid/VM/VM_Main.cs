using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
using System.Data;

namespace CvsDatagrid.VM
{
    public class VM_Main : ObservableRecipient
    {
        public ICommand Cmd_OpenFile { get; }
        public ICommand Cmd_SaveFile { get; }
        public string? FilePath { get; private set; }
        public DataTable TableData { get; private set; }
        public VM_Main()
        {
            FilePath = null;
            Cmd_OpenFile = new RelayCommand(OpenFile);
            Cmd_SaveFile = new RelayCommand(SaveFile);
        }

        private void OpenFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (!string.IsNullOrEmpty(AppSettings.Default.InitialPath))
            {
                openFileDialog.InitialDirectory = AppSettings.Default.InitialPath;
            }
            else
            {
                openFileDialog.InitialDirectory = Environment.CurrentDirectory;
            }
            bool? result = openFileDialog.ShowDialog();
            if (result == true)
            {
                string fileName = openFileDialog.FileName;
                if (File.Exists(fileName))
                {
                    FileInfo fileInfo = new FileInfo(fileName);
                    AppSettings.Default.InitialPath = fileInfo.DirectoryName;
                    AppSettings.Default.Save();
                    CvsReader cvsReader = new CvsReader(fileName);
                    if (!cvsReader.Error)
                    {
                        FilePath = fileName;
                        TableData = new DataTable();
                        foreach (string header in cvsReader.Headers)
                        {
                            var dataColumn = new DataColumn(header);
                            dataColumn.ColumnName = header;
                            TableData.Columns.Add(dataColumn);
                        }
                        foreach (string[] row in cvsReader.Rows)
                        {
                            TableData.Rows.Add(row);
                        }
                        OnPropertyChanged(nameof(TableData));
                    }
                    else
                    {
                        FilePath = null;
                    }

                }
            }

        }

        private void SaveFile()
        {
            if (FilePath != null)
            {
            }
        }
    }
}
