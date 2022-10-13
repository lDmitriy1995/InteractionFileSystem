using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InteractionFileSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //1.Получить расширения всех файлов, 
            string path = @"C:\Users\Admin\Desktop\File C#";

            PrintDirNames(path);

            int k = 0;

            foreach (string  item in GetAllFiles(path))
            {
                Console.WriteLine((k++)+": " +item);
            }

           // и предложить пользователю выбрать необходимый формат,
            int ch = 0;
            Console.WriteLine("Выберите необходимый формат: ");
            ch= Convert.ToInt32(Console.ReadLine());


            DirectoryInfo dir = new DirectoryInfo(path);
            int count = dir.GetFiles("*"+GetAllFiles(path)[ch]).Count();
            Console.WriteLine("Файлов с расширением = " + count);

            GetName(path);
        }

        public static List <string> GetAllFiles(string path)
        {
            List<string> filesExt = new List<string>();
            //1.Получить расширения всех файлов, 


            //    и предложить пользователю выбрать необходимый формат, 
            //    с которым нужно будет работать. При этом нужно учесть, 
            //    что пользователь может выбрать больше одного формата или все.

            if (string.IsNullOrWhiteSpace(path))
            {
                Console.WriteLine("Укажите путь");
            }
            else
            {
                DirectoryInfo dir = new DirectoryInfo(path);
                if(!dir.Exists)
                    Console.WriteLine("Указанный путь не корректный");
                else
                {
                    //Получить расширение всех файлов 
                   

                    foreach (FileInfo item in dir.GetFiles())
                    {
                        if(!filesExt.Contains(item.Extension))
                        filesExt.Add(item.Extension);
                    }

                    //Dictionary<string, FileInfo> filesDic = 
                    //    new Dictionary<string, FileInfo>();

                    //foreach (FileInfo item in dir.GetFiles())
                    //{
                    //    if(!filesDic.ContainsKey(item.Extension))
                    //        filesDic.Add(item.Extension, item);
                    //}

                }
            }
            return filesExt;

        }

        public static void GetName(string path)
        {
            DirectoryInfo dir = new DirectoryInfo(path);
            List<string> pattern = new List<string>() { "-","_"};

            foreach (FileInfo f  in dir.GetFiles())
            {
                if(f.Name.Contains("-"))
                    f.MoveTo(dir.FullName + @"\" +f.Name.Replace("-", "+"));
            }
        }
        
        public static void PrintFileNames(string path)
        {
            DirectoryInfo dir = new DirectoryInfo(path);

            foreach (FileInfo file in dir.GetFiles())
            {
                Console.WriteLine("--->" + file.Name);
            }
            
        }

        public static void PrintDirNames(string path)
        {
            DirectoryInfo dir = new DirectoryInfo(path);

            foreach(DirectoryInfo item in dir.GetDirectories())
            {
                Console.WriteLine($"{dir.FullName}");
                PrintDirNames(item.FullName);
            }
        }























        public static void Exmpl01()
        {
            FileStream fs = new FileStream(@"C:\Users\Admin\Desktop\File C#\Text.txt",
                FileMode.Create);

            FileStream fs2 = new FileStream(@"C:\Users\Admin\Desktop\File C#\Text1.txt",
                FileMode.Create, FileAccess.Write);

            FileStream fs3 = new FileStream(@"C:\Users\Admin\Desktop\File C#\Text2.txt",
                FileMode.Create, FileAccess.Write,FileShare.None);
            fs3.Close();

        }


        public static void Exmpl02()
        {
            FileInfo f = new FileInfo(@"C:\Users\Admin\Desktop\File C#\Text4.txt");
            
            if (!f.Exists)
            {
                FileStream fs = f.Create();
                fs.Close();
            }

            using (FileStream fs = f.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None))
            {

            }
        }

        public static void Exmpl03()   //Работа с директориями
        {
            DirectoryInfo dir = new DirectoryInfo(".");

            DirectoryInfo dir2 = new DirectoryInfo(@"C:\Users\Admin\Desktop\File C#\Test");
            dir2.Create();

        }
        public static void Exmpl04()
        {

            DirectoryInfo dir = new DirectoryInfo(@"C:\Users\Admin\Desktop\File C#\Test");
            Console.WriteLine("Информация о каталоге");
            Console.WriteLine("Полный путь: {0} \n"+" Название папки: {1}\n"+"\n Родительский каталог: {2}", 
                dir.FullName,dir.Name, dir.Parent);

            foreach (FileInfo file in dir.GetFiles())
            {
                Console.WriteLine("->> " + file.Name);

            }
            FileInfo[] foleHtml = dir.GetFiles("*.html", SearchOption.AllDirectories);
        } // Информация о каталогах

        public static void Exmpl05()  // Cчитывание информации из файла
        {
            string path = @"C:\Users\Admin\Desktop\File C#\Test\Текстовый документ.txt";
            try
            {

                 //CЧитываем весь файл
                using (StreamReader sr = new StreamReader(path))
                {
                    Console.WriteLine(sr.ReadToEnd());

                }


                using (StreamReader sr = new StreamReader(path))
                {
                    string line;
                    while((line = sr.ReadLine()) != null)
                    {
                        Console.WriteLine(line);
                    }
                }

                using (StreamReader sr = new StreamReader(path))
                {
                    char[] array = new char[4];
                    sr.Read(array, 0, 4);
                    Console.WriteLine(array);
                }



            }
            catch (Exception ex)
            {

                
            }
        }

        public static void Exmpl06() //Запись в файл 
        {
            string path = @"C:\Users\Admin\Desktop\File C#\Test\Текстовый документ.txt";

            using (StreamWriter sw = new StreamWriter(path,true,Encoding.Default))
            {
                sw.Write("\n------>"+DateTime.Now.ToLongTimeString());
            }

        }


        public static void Exmpl07() // Информация о диске 
        {
            DriveInfo[] di = DriveInfo.GetDrives();
            foreach (var item in di)
            {
                Console.WriteLine(item.Name);
            }
            DriveInfo c = new DriveInfo("C");
            Console.WriteLine("Общий размер: " +c.TotalSize);
            Console.WriteLine("Свободное пространство: " +c.AvailableFreeSpace);
            Console.WriteLine("Формат устройства: " +c.DriveFormat);
            Console.WriteLine("Тип устройства: " +c.DriveType);
            Console.WriteLine("Имя: " +c.Name);


        }

    }
}
