using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.ServiceProcess;

namespace writeLog
{
    public partial class Service1 : ServiceBase
    {
        static DataSet dt = new DataSet();
        //static string file_tranfers = @"D:\mySQL\DCM";
        static string file_Origin;
        static string file_Desstination;
        static int timeStart;
        static int timeEnd;
        static bool state = true;

        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            using (StreamReader r = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "\\config.json"))
            {
                string json = r.ReadToEnd();
                List<Read> reads = JsonConvert.DeserializeObject<List<Read>>(json);
                foreach (var item in reads)
                {
                    file_Origin = item.file_Origin; //file ban dau
                    file_Desstination = item.file_Desstination;
                    timeStart = item.timeStart;
                    timeEnd = item.timeEnd;
                }
            }
            if (file_Origin == "" || file_Desstination == "")
            {
                writeLog.write("Error: file configure.json", "", "", DateTime.Now);
                return;
            }
            FileSystemWatcher watcher = new FileSystemWatcher(file_Origin);
            watcher.EnableRaisingEvents = true;
            watcher.IncludeSubdirectories = true;
            //xu ly su thay doi cua file
            watcher.Created += watcher_Created;

            Console.Read();
        }

        protected override void OnStop()
        {
        }

        public static void RunCmd(Service1 obj)
        {
            obj.OnStart(null);
            obj.OnStop();
        }

        public static void DirectoryCopy(string sourceDirectory, string targetDirectory, bool copySubDirs)
        {
            DirectoryInfo dir = new DirectoryInfo(sourceDirectory);

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirectory);
            }

            DirectoryInfo[] dirs = dir.GetDirectories();
            // If the destination directory doesn't exist, create it.
            if (!Directory.Exists(targetDirectory))
            {
                Directory.CreateDirectory(targetDirectory);
            }

            //// Get the files in the directory and copy them to the new location.
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string temppath = Path.Combine(targetDirectory, file.Name);
                file.CopyTo(temppath, false);
            }

            // If copying subdirectories, copy them and their contents to new location.
            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string temppath = Path.Combine(targetDirectory, subdir.Name);
                    DirectoryCopy(subdir.FullName, temppath, copySubDirs);
                }
            }
        }
        #region
        //public static void copyFolder(string sourceDirectory, string targetDirectory)
        //{
        //    //var diSource = sourceDirectory;
        //    //var diTarget = targetDirectory;
        //    string fileName = Path.GetFileName(sourceDirectory);
        //    copy(sourceDirectory, targetDirectory, fileName);
        //}
        #endregion
        #region
        //public static void copy(string source, string target, string fileCopy)
        //{
        //    string fileName = fileCopy;
        //    string sourcePath = source;
        //    string targetPath = target;

        //    string sourceFile = System.IO.Path.Combine(sourcePath, fileName);
        //    string destFile = System.IO.Path.Combine(targetPath, fileName);

        //    System.IO.Directory.CreateDirectory(targetPath);
        //    System.IO.File.Copy(sourceFile, destFile, true);

        //    if (System.IO.Directory.Exists(sourcePath))
        //    {
        //        string[] files = System.IO.Directory.GetFiles(sourcePath);

        //        foreach (string s in files)
        //        {
        //            fileName = System.IO.Path.GetFileName(s);
        //            destFile = System.IO.Path.Combine(targetPath, fileName);
        //            System.IO.File.Copy(s, destFile, true);
        //        }
        //    }
        //    else
        //    {
        //        Console.WriteLine("Source path does not exist!");
        //    }
        //}
        #endregion

        private static void watcher_Created(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine(e.FullPath);
            string abc = e.FullPath.Replace(file_Origin, "");
            string cba = file_Desstination + abc;
            writeLog.write("", e.FullPath + "\n", null, DateTime.Now);
            #region
            //foreach (string item in subdirectoryEntries)
            //{
            //    if (item.Contains(".json"))
            //    {
            //        if (!File.Exists(item))
            //        {
            //            copyFolder(file_Origin, file_Desstination);
            //        }
            //    }
            //    else if (item.Contains(".dcm"))
            //    {
            //        if (!File.Exists(item))
            //        {
            //            copyFolder(file_Origin, file_Desstination);
            //        }
            //    }
            //    else if (item.Contains(".png"))
            //    {
            //        if (!File.Exists(item))
            //        {
            //            copyFolder(file_Origin, file_Desstination);
            //        }
            //    }
            //    else
            //    {
            //        if (!File.Exists(item))
            //        {
            //            copyFolder(file_Origin, file_Desstination);
            //        }
            //    }
            //}
            #endregion
            #region test
            //int count = dir.GetFiles().Length;
            //if (count > 0)
            //{
            //}

            //var aaaa = e.FullPath + "\\" + ".json";
            //if (e.FullPath == e.FullPath + "\\" + ".json")
            //{
            //    var a = e.FullPath;
            //    copyFolder(file_Origin, file_Desstination);
            //}
            //else if(e.FullPath == e.FullPath + "\\" + ".dcm")
            //{
            //    //var b = e.FullPath + "\\" + ".dcm";
            //    copyFolder(file_Origin, file_Desstination);
            //}    
            //else if(e.FullPath == e.FullPath + "\\" + ".png")
            //{
            //    copyFolder(file_Origin, file_Desstination);
            //}
            //else
            //{
            //    copyFolder(file_Origin, file_Desstination);
            //}
            //var sourcePath = file_Origin;
            //var targetPath = file_Desstination;
            //copyFolder(sourcePath, targetPath);

            //int cout = dir.GetFiles().Length;
            //if (cout > 0)
            //{
            //var sourcePath = file_Origin;
            //var targetPath = file_Desstination;

            //foreach (var file in Directory.GetFiles(targetPath))
            //{
            //    if (File.Exists(targetPath + "\\" + file))
            //    {
            //        copyFolder(sourcePath, targetPath);
            //    }
            //    else
            //    {
            //        copyFolder(sourcePath, targetPath);
            //    }
            //}
            //}
            #endregion
            #region old
            //if (file.Contains(".json"))
            //{
            //    if (!File.Exists(file))
            //    {
            //        copyFolder(file_Origin, file_Desstination);
            //    }
            //}
            //else if (file.Contains(".dcm"))
            //{
            //    if (!File.Exists(file))
            //    {
            //        copyFolder(file_Origin, file_Desstination);
            //    }
            //}
            //else if (file.Contains(".png"))
            //{
            //    if (!File.Exists(file))
            //    {
            //        copyFolder(file_Origin, file_Desstination);
            //    }
            //}
            //else
            //{
            //    if (!File.Exists(file))
            //    {
            //        Directory.CreateDirectory(file);
            //        //copyFolder(file, file_Desstination);
            //        Console.WriteLine("Bạn vừa tạo file : " + file);
            //    }
            //}
            #endregion

            try
            {
                /*  
                 Tạo 1 biến state kiểu bool mặc định true
                nếu trong khoảng thời gian timestart đến timeend và state=true
                copy hết đường dẫn trong file log.txt và đặt lại file log.txt rỗng
                 */

                int h = DateTime.Now.Hour;

                #region
                if ((h >= timeStart && h <= timeEnd) && state == true)
                {
                    state = false;
                    Service1.readTxt();
                    writeLog.write("", e.FullPath + "\n", null, DateTime.Now);
                }
                else if ((h >= timeStart && h <= timeEnd) && state == false)
                {
                    writeLog.write("", e.FullPath + "\n", null, DateTime.Now);
                }
                else
                {
                    state = true;
                    writeLog.write("", e.FullPath + "\n", null, DateTime.Now);
                }
                #endregion
                #region
                //var newPath = target + "\\" + pick;
                //if(file.HasFlag(FileAttributes.Directory))
                //{
                //    if (Directory.Exists(target))
                //    {
                //        var newFolder = target;
                //        //Directory.CreateDirectory(newFolder);
                //        string[] fol = Directory.GetDirectories(file_Desstination);
                //        foreach (string item in fol)
                //        {
                //            newFolder += item;
                //        }
                //    }
                //}
                //else
                //{

                //}
                //string picklist = Path.GetDirectoryName(e.FullPath);
                //string file = Path.GetFileName(e.FullPath);

                //FileAttributes attr = File.GetAttributes(e.FullPath);
                //if (attr.HasFlag(FileAttributes.Directory))
                //{
                //    string newFolder = file_Desstination + "\\" + file;
                //    if (!Directory.Exists(newFolder))
                //    {
                //        DirectoryInfo info = Directory.CreateDirectory(newFolder);
                //    }
                //    //DirectoryInfo directory =  Directory.CreateDirectory(item);
                //    var soure = Path.Combine(picklist, file);
                //    var target = Path.Combine(file_Desstination, file);
                //    File.Copy(soure, target, true);
                //}
                //else
                //{

                //}
                //var soure = Path.Combine(picklist, file);
                //var target = Path.Combine(file_Desstination, file);
                //FileAttributes a = File.GetAttributes(file_Desstination);
                //string newFolder = file_Desstination + "\\" + file;
                //if (a.HasFlag(FileAttributes.Directory))
                //{
                //    if (!Directory.Exists(newFolder))
                //    {
                //        Directory.CreateDirectory(newFolder);
                //    }
                //}
                //else if (soure.Contains(".dcm"))
                //{
                //    var createFile = Directory.CreateDirectory(file);
                //    var newPath = Path.Combine(file_Desstination, createFile.FullName);
                //    File.Copy(soure, target, true);
                //}
                #endregion
            }
            catch (DirectoryNotFoundException dirNotFound)
            {
                Console.WriteLine(dirNotFound.Message);
            }
            #region
            //string[] subdirectoryEntries = Directory.GetDirectories(file_Origin);

            //foreach (string item in subdirectoryEntries)
            //{
            //    if (!File.Exists(file))
            //    {
            //        if (file.Contains(".dcm"))
            //        {
            //            var newFile = Directory.CreateDirectory(file);
            //            //copyFolder(file, file_Desstination);
            //            Console.WriteLine("Create file : " + file);

            //            copyFolder(file_Origin, file_Desstination);
            //        }
            //    }
            //}
            #endregion
        }

        public static bool readTxt()
        {
            try
            {
                string[] lines = File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + "\\log.txt");

                foreach (string item in lines)
                {
                    FileAttributes file = File.GetAttributes(item);
                    string a = item.Replace(file_Origin, "");
                    string b = file_Desstination + a;
                    if (file.HasFlag(FileAttributes.Directory))
                    {
                        if (!Directory.Exists(b))
                        {
                            Directory.CreateDirectory(b);
                            Service1.DirectoryCopy(b, file_Desstination, true);
                            //File.Copy(b, file_Desstination, true);
                        }
                    }
                    else
                    {
                        File.Copy(item, b, true);
                    }
                }
                File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "\\log.txt", String.Empty);
            }
            catch(IOException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return true;
        }
    }

    public class writeLog
    {
        public static void write(string text, string name, string oldname, DateTime date)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "log.txt";

            if (System.IO.File.Exists(path))
            {
                Console.WriteLine(text + " " + name + " " + oldname + " " + DateTime.Now.ToString() + Environment.NewLine);
                System.IO.File.AppendAllText(path, name);
            }
        }
    }

    public class Read
    {
        public string file_Origin;
        public string file_Desstination;
        public int timeStart;
        public int timeEnd;
    }
}
