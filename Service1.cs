using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace writeLog
{
    public partial class Service1 : ServiceBase
    {
        public DataSet dt = new DataSet();
        //static string file_tranfers = @"D:\mySQL\DCM";
        static string file_Origin;
        static string file_Desstination;

        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            using (StreamReader r = new StreamReader(@"D:\mySQL\config.json"))
            {
                string json = r.ReadToEnd();
                List<Read> reads = JsonConvert.DeserializeObject<List<Read>>(json);
                foreach (var item in reads)
                {
                    file_Origin = item.file_Origin; //file ban dau
                    file_Desstination = item.file_Desstination;
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

        #region
        public static void copyFile(string sourceDirectory, string targetDirectory, string fileName)
        {
            string sourceFile = System.IO.Path.Combine(sourceDirectory, fileName);
            string destFile = System.IO.Path.Combine(targetDirectory, fileName);

            System.IO.Directory.CreateDirectory(targetDirectory);

            //System.IO.File.Copy(sourceFile, destFile, true);
            if (System.IO.Directory.Exists(sourceDirectory))
            {
                string[] files = System.IO.Directory.GetFiles(sourceDirectory);

                foreach (string s in files)
                {
                    fileName = System.IO.Path.GetFileName(s);
                    destFile = System.IO.Path.Combine(targetDirectory, fileName);
                    System.IO.File.Copy(s, destFile, true);
                }
            }
            else
            {
                Console.WriteLine("Source path does not exist!");
            }
        }
        #endregion
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
            string cba= file_Desstination+abc;

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
                FileAttributes file = File.GetAttributes(e.FullPath);
                string getFolder = Path.GetDirectoryName(e.FullPath);
                string getFile = Path.GetFileName(e.FullPath);
                var soure = Path.Combine(getFolder , getFile);
                var target = Path.Combine(file_Desstination , getFile);
            
                if(file.HasFlag(FileAttributes.Directory))
                {
                    if(!Directory.Exists(target))
                    {
                        DirectoryInfo a = Directory.CreateDirectory(cba);
                    }
                }
                else
                {
                    File.Copy(soure, cba, true);
                }
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
    }

    public class writeLog
    {
        public static void write(string text, string name, string oldname, DateTime date)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "log.txt";

            if (System.IO.File.Exists(path))
            {
                Console.WriteLine(text + " " + name + " " + oldname + " " + DateTime.Now.ToString() + Environment.NewLine);
                System.IO.File.AppendAllText(path, text + " " + name + " " + oldname + " " + DateTime.Now.ToString() + Environment.NewLine);
            }
        }
    }

    public class Read
    {
        public string file_Origin;
        public string file_Desstination;
    }
}
