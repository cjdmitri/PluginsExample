
using System.Reflection;

namespace PluginsExample
{
    internal class Program
    {
        static void Main(string[] args)
        {
               string pathFolder = Path.Combine(Environment.CurrentDirectory, "Plugins");
               Console.WriteLine($"Каталог с плагинами: {pathFolder}");

               //Проверяем наличие каталога с плагинами
               if (Directory.Exists(pathFolder))
               {
                    /*
                     Проход по всем файлам в каталоге, с расширением .dll
                    и название которых начинается на Plugin
                     */
                    foreach(var filePlugin in Directory.GetFiles(pathFolder, "Plugin*.dll"))
                    {
                         RunMethodPlugin(filePlugin);
                    }
               }
               else
                    Console.WriteLine($"Каталога с плагинами не существует...");

               Console.Read();
        }

          /// <summary>
          /// Выполняет метод Run, содержащийся в классе ClassPlugin сборки плагина
          /// </summary>
          /// <param name="pluginPath"></param>
          private static void RunMethodPlugin(string pluginPath)
          {
               //Загружаем сборку
               Assembly asm = Assembly.LoadFrom(pluginPath);
               //получаем класс ClassPlugin
               Type? type = asm.GetType("ClassPlugin");
               if (type != null)
               {
                    //получаем метод Run
                    MethodInfo? run = type.GetMethod("Run", BindingFlags.Public | BindingFlags.Static);
                    if (run != null)
                    {
                         object? result = run.Invoke(null, new object[] { "Привет!!" });
                         Console.WriteLine(result);
                    }
               }
          }
    }
}