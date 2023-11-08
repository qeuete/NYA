
using System.Diagnostics;
using System.Dynamic;
using System.IO;
using System.Runtime.InteropServices;

namespace ConsoleApp7
{
    internal class Program
    {
        static void Main()
        {
            DriveInfo[] disks = DriveInfo.GetDrives();
            foreach (DriveInfo disk in disks)
            {
                Console.WriteLine("  " + $"Диск {disk.Name}:Общий объем {disk.TotalSize / (1024 * 1024 * 1024)} ГБ. Свободно {disk.AvailableFreeSpace / (1024 * 1024 * 1024)} ГБ ");
            }
            int pos = Menu.Show(0, 1);
            if (pos == -1)
            {
                return;
            }
            string disksks = disks[pos].Name;
            ShowDirectories(disksks);
        }
        static void ShowDirectories(string path)
        {
            string[] directories = Directory.GetDirectories(path);
            string[] files = Directory.GetFiles(path);
            int data = 0;
            try
            {
                int a = 0;
                do
                {
                    Console.Clear();
                    foreach (string d in directories)
                    {                      
                        DirectoryInfo info = new DirectoryInfo(d);
                        Console.WriteLine("  " + $"Папка: {info.Name}, Дата создания: {info.CreationTime}, Атрибуты: {info.Attributes}");
                    }
                    foreach (string f in files)
                    {
                        DirectoryInfo info = new DirectoryInfo(f);
                        Console.WriteLine("  " + $"Файл: {info.Name}, Дата создания: {info.CreationTime}, Атрибуты: {info.Attributes}");
                    }
                    int long1 = directories.Length + files.Length;
                    int selected = Menu.Show(0, 150);
                    if (selected == -1)
                        Console.Clear();
                    {
                        return;
                    }

                    a = selected;
                    ShowDirectories(directories[selected]);
                }
                while (a != 3);
            }
            catch (IndexOutOfRangeException)
            {
                try
                {
                    OpenFile(files[data]);
                    Console.Clear();
                }
                catch (IndexOutOfRangeException)
                {
                    Console.Clear();
                    Console.WriteLine("Ошибка открытия файла");
                    Thread.Sleep(1000);
                }
            }
            catch (UnauthorizedAccessException)
            {
                Console.Clear();
                Console.WriteLine("Ошибка: доступ запрещен");
                Thread.Sleep(1000);
                Main();
            }
        }
        private static void OpenFile(string pathFile)
        {
            Process.Start(new ProcessStartInfo { FileName = pathFile, UseShellExecute = true });
            Console.Clear();
            Main();
        }
    }
}
   