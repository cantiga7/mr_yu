using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Drawing;
using System.Windows.Forms;



using System.IO;
using System.Net;
using System.Configuration;


public static class clsFunctions
{

    public static void SetSetting(string key, string value)
    {
        Configuration configuration = ConfigurationManager.OpenExeConfiguration(System.Reflection.Assembly.GetExecutingAssembly().Location);
        configuration.AppSettings.Settings[key].Value = value;
        configuration.Save(ConfigurationSaveMode.Full, false);
        ConfigurationManager.RefreshSection("appSettings");
    }

    public static void btnSetup(Form frmSetup ,Button[,] btn_Setup, Panel pnlLocation, string ControlName, int Width, int Height)
    {
        int x = 0;

        for (int i = 0; i < btn_Setup.GetLength(0); i++)
        {

            for (int j = 0; j < btn_Setup.GetLength(1); j++)
            {
                //set index for button array
                x++;
                int index = x;

                // instance the control
                btn_Setup[i, j] = new Button();
                // set some initial properties
                btn_Setup[i, j].Name = ControlName + "_" + i.ToString() + "_" + j.ToString();
                btn_Setup[i, j].Text = x.ToString(); //"";
                btn_Setup[i, j].AccessibleDescription = ControlName;
                btn_Setup[i, j].Tag = x.ToString(); //"";
                btn_Setup[i, j].Cursor = Cursors.Hand;
               


                //switch (x)
                //{
                //    case 10:
                //        btn_Setup[i, j].Text = "OK"; //"";
                //        break;
                //    case 11:
                //        btn_Setup[i, j].Text = "0"; //"";
                //        break;
                //    case 12:
                //        btn_Setup[i, j].Text = "CANCEL"; //"";
                //        break;
                //    default:
                //        btn_Setup[i, j].Text = x.ToString(); //"";
                //        break;
                //}


                // add to form
                frmSetup.Controls.Add(btn_Setup[i, j]);

                btn_Setup[i, j].Parent = pnlLocation;
                // set position and size
                btn_Setup[i, j].Location = new Point(0 + j * Width, 0 + i * Height);
                btn_Setup[i, j].Size = new Size(Width, Height);

                //btn_Setup[i, j].Click += (sender, e) => this.Display(index);
                //btn_Setup[i, j].Click += new EventHandler(btn_Array_Click);
            }
        }


    }

    public static void lblSetup(Form frmSetup, Label[,] lbl_Setup, Panel pnlLocation, string ControlName, int Width, int Height)
    {
        int x = 0;

        for (int i = 0; i < lbl_Setup.GetLength(0); i++)
        {

            for (int j = 0; j < lbl_Setup.GetLength(1); j++)
            {
                //set index for button array
                x++;
                int index = x;

                // instance the control
                lbl_Setup[i, j] = new Label();
                // set some initial properties
                lbl_Setup[i, j].Name = ControlName + "_" + i.ToString() + "_" + j.ToString();
                lbl_Setup[i, j].Text = x.ToString(); //"";
                lbl_Setup[i, j].AccessibleDescription = ControlName;
                lbl_Setup[i, j].Tag = x.ToString(); //"";
                //lbl_Setup[i, j].BorderStyle = BorderStyle.FixedSingle;
  
                // add to form
                frmSetup.Controls.Add(lbl_Setup[i, j]);

                lbl_Setup[i, j].Parent = pnlLocation;
                // set position and size
                lbl_Setup[i, j].Location = new Point(0 + j * Width, 0 + i * Height);
                lbl_Setup[i, j].Size = new Size(Width, Height);

                //btn_Setup[i, j].Click += (sender, e) => this.Display(index);
                //btn_Setup[i, j].Click += new EventHandler(btn_Array_Click);
            }
        }


    }

    public static void DataGridViewSetup(DataGridView _DataGridView, DataTable _DataTable, string _Group = "", string _Branch = "")
    {
        try
        {
            _DataGridView.Columns.Clear();

            _DataGridView.MultiSelect = true;
            _DataGridView.RowTemplate.Resizable = DataGridViewTriState.False;
            _DataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            _DataGridView.DefaultCellStyle.SelectionBackColor = Color.FromArgb(234, 241, 246);
            _DataGridView.DefaultCellStyle.SelectionForeColor = Color.Black;

            _DataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;

            _DataGridView.BackgroundColor = Color.FromArgb(234, 241, 246);
            _DataGridView.GridColor = Color.Gray;
            _DataGridView.BorderStyle = BorderStyle.FixedSingle;
            _DataGridView.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            _DataGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            _DataGridView.CellBorderStyle = DataGridViewCellBorderStyle.Single;
            _DataGridView.RowHeadersWidth = 50;

            _DataGridView.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 8);
            _DataGridView.RowsDefaultCellStyle.Font = new Font("Segoe UI", 8);
            _DataGridView.RowHeadersDefaultCellStyle.Font = new Font("Segoe UI", 8, FontStyle.Bold);
            //'.RowTemplate.Height = 15
            //'.Rows.Add(10)
            _DataGridView.RowHeadersVisible = false;
            _DataGridView.AllowUserToAddRows = false;
            _DataGridView.AllowUserToDeleteRows = false;


            if (_Group != "Permission")
            {
                _DataGridView.DataSource = _DataTable;
            }
           


            foreach (DataGridViewColumn column in _DataGridView.Columns)
            {
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                column.Width = 100;
                column.DefaultCellStyle.BackColor = Color.FromArgb(234, 241, 246);
                column.DefaultCellStyle.SelectionBackColor = Color.FromArgb(254, 240, 158);
                column.ReadOnly = false;

                

            }


            switch (_Group)
            {
                case "Amortization":
                    _DataGridView.Columns[3].DefaultCellStyle.Format = "N2";
                    _DataGridView.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                    _DataGridView.Columns[4].DefaultCellStyle.Format = "N2";
                    _DataGridView.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                    _DataGridView.Columns[5].DefaultCellStyle.Format = "N2";
                    _DataGridView.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                    _DataGridView.Columns[6].DefaultCellStyle.Format = "N2";
                    _DataGridView.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                    break;
          
                default:
                    break;
            }



        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
         
    }


    /// <summary>
    /// Upload a file using FTP
    /// </summary>
    /// <param name="FTPServer">The server to upload to</param>
    /// <param name="remotePath">The remote path in the server</param>
    /// <param name="fileToUpload">The path to the local uploaded file</param>
    /// <param name="user">User to log onto the FTP server</param>
    /// <param name="password">Password to log onto the FTP server</param>
    /// <returns>The status of the upload</returns>
    public static bool FTPUpload(string FTPServer, string remotePath, string fileToUpload, string user, string password)
    {
        try
        {
            //Get FTP web resquest object.
            FtpWebRequest request = FtpWebRequest.Create(new Uri(@"ftp://" + FTPServer + @"/" + remotePath + @"/" + Path.GetFileName(fileToUpload))) as FtpWebRequest;
            request.UseBinary = true;
            request.KeepAlive = false;
            request.Method = WebRequestMethods.Ftp.UploadFile;
            if (!string.IsNullOrEmpty(user) && !string.IsNullOrEmpty(password))
                request.Credentials = new NetworkCredential(user, password);
            //Get physical file
            FileInfo fi = new FileInfo(fileToUpload);
            Byte[] contents = new Byte[fi.Length];
            //Read file
            FileStream fs = fi.OpenRead();
            fs.Read(contents, 0, Convert.ToInt32(fi.Length));
            fs.Close();
            //Write file contents to FTP server
            Stream rs = request.GetRequestStream();
            rs.Write(contents, 0, Convert.ToInt32(fi.Length));
            rs.Close();
            FtpWebResponse response = request.GetResponse() as FtpWebResponse;
            string statusDescription = response.StatusDescription;
            response.Close();
            return true;
            //return statusDescription;
        }
        catch
        {
            //throw new Exception("Error uploading to URL " + "ftp://" + FTPServer + @"/" + remotePath + @"/" + Path.GetFileName(fileToUpload), e);
            return false;
        }

    }


    /// <summary>
    /// Download a file using FTP
    /// </summary>
    /// <param name="FTPServer">The server to download from</param>
    /// <param name="remotePath">The remote path in the server</param>
    /// <param name="fileNameToDownload">The remote file name</param>
    /// <param name="saveToLocalPath">The path to the folder to download to</param>
    /// <param name="user">User to log onto the FTP server</param>
    /// <param name="password">Password to log onto the FTP server</param>
    /// <returns>The status of the download</returns>
    public static bool FTPDownload(string FTPServer, string remotePath, string fileNameToDownload, string saveToLocalPath, string user, string password)
    {
        try
        {
            //Get FTP web resquest object.
            FtpWebRequest request = FtpWebRequest.Create(new Uri(@"ftp://" + FTPServer + @"/" + remotePath + @"/" + fileNameToDownload)) as FtpWebRequest;
            request.UseBinary = true;
            request.KeepAlive = false;
            request.Method = WebRequestMethods.Ftp.DownloadFile;
            if (!string.IsNullOrEmpty(user) && !string.IsNullOrEmpty(password))
                request.Credentials = new NetworkCredential(user, password);
            FtpWebResponse response = request.GetResponse() as FtpWebResponse;
            Stream responseStream = response.GetResponseStream();
            FileStream outputStream = new FileStream(saveToLocalPath + "\\" + fileNameToDownload, FileMode.Create);
            int bufferSize = 1024;
            int readCount;
            byte[] buffer = new byte[bufferSize];
            readCount = responseStream.Read(buffer, 0, bufferSize);
            while (readCount > 0)
            {
                outputStream.Write(buffer, 0, readCount);
                readCount = responseStream.Read(buffer, 0, bufferSize);
            }
            string statusDescription = response.StatusDescription;
            responseStream.Close();
            outputStream.Close();
            response.Close();
            return true;
            //return statusDescription;
        }
        catch
        {
            //throw new Exception("Error uploading to URL " + "ftp://" + FTPServer + @"/" + remotePath + @"/" + Path.GetFileName(fileToUpload), e);
            return false;
        }

    }


    public static bool CreateBranchFolder(string path)
    {
        try
        {
            bool folderExists = Directory.Exists(path);
            if (folderExists == false)
            {
                Application.DoEvents();
                Directory.CreateDirectory(path);
                return true;
            }
            return false;
        }
        catch
        {
            return false;
        }

    }

    public static string FormAccess(string _UserID, string _Module)
    {
        try
        {
            if (_UserID == "1")
            {
                return "Full";
            }

            DataTable _DataTable;
            string _SQLSyntax;
            _SQLSyntax = @"SELECT A.Access FROM OUAS A WHERE A.UserID = '" + _UserID + "' AND A.Module = '" + _Module + "'";
            _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);


            return clsSQLClientFunctions.GetData(_DataTable, "Access","0");
        }
        catch
        {
            return "No Access";
        }

    }

    public static DialogResult ShowInputDialog(ref string input)
    {
        System.Drawing.Size size = new System.Drawing.Size(300, 70);
        Form inputBox = new Form();

        inputBox.Font = new Font("Segoe UI", 9);
        inputBox.StartPosition = FormStartPosition.CenterScreen;
        inputBox.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
        inputBox.ClientSize = size;
        inputBox.Text = "Input Data";

        System.Windows.Forms.TextBox textBox = new TextBox();
        textBox.Size = new System.Drawing.Size(size.Width - 10, 23);
        textBox.Location = new System.Drawing.Point(5, 5);
        textBox.Text = input;
        inputBox.Controls.Add(textBox);

        Button okButton = new Button();
        okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
        okButton.Name = "okButton";
        okButton.Size = new System.Drawing.Size(75, 23);
        okButton.Text = "&OK";
        okButton.Location = new System.Drawing.Point(size.Width - 80 - 80, 39);
        inputBox.Controls.Add(okButton);

        Button cancelButton = new Button();
        cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        cancelButton.Name = "cancelButton";
        cancelButton.Size = new System.Drawing.Size(75, 23);
        cancelButton.Text = "&Cancel";
        cancelButton.Location = new System.Drawing.Point(size.Width - 80, 39);
        inputBox.Controls.Add(cancelButton);

        inputBox.AcceptButton = okButton;
        inputBox.CancelButton = cancelButton;

        DialogResult result = inputBox.ShowDialog();
        input = textBox.Text;
        return result;
    }


    public static string SplitString(string _Value, char _Delimited)
    {
        string str = null;
        string[] strArr = null;
        str = _Value;
        char[] splitchar = { _Delimited };
        strArr = str.Split(splitchar);

        return strArr[0].Trim();
    }


    public static string ConfigValue(int _Code)
    {
        string _SqlSyntax;
        DataTable _DataTable = new DataTable();
        _SqlSyntax = "SELECT U_Value FROM [@SYSCON] Z WHERE Z.CODE = '" + _Code + "'";
        _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSAPConnection, _SqlSyntax);

        return clsSQLClientFunctions.GetData(_DataTable, "U_Value", "0");
    }

}

