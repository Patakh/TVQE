using System; 
using System.Collections.Generic; 

namespace TVQE.Models
{
    public class CallTickets
    {
        public Queue<string> QueueAudioFiles { get; set; }
        public string Prefix { get; set; }
        public string Number { get; set; }
        public string WindowName { get; set; }

        public CallTickets(string prefix, string number, string windowName)
        {

            Prefix = prefix;
            Number = number;
            WindowName = windowName;
            int nuberT = Convert.ToInt32(number);
            var path = $"{System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory)))}\\Озвучка\\";
            QueueAudioFiles = new Queue<string>(new List<string>
                {
                    $"{path}ВНИМАНИЕ",
                    $"{path}КЛИЕНТА",
                    $"{path}НОМЕР" ,
                    $"{path}{prefix}"
                });

            //алгоритм для создания путей к файлам голоса

            string[] listAudio = nuberT switch
            {
                int n when n <= 20 || n % 10 == 0 && n < 100 || n % 100 == 0  =>
                new string[] { path + number },
                int n when n < 100 || n % 100 <= 20 || n % 10 == 0 =>
                new string[]{
                    (path + (n < 100 ? $"{number[0]}0" : $"{number[0]}00")),
                    (path + (n < 100 ? number[1] : n % 100 <= 20 ? $"{number[1]}{number[2]}" : $"{number[1]}0"))
                },
                _ => new string[]{
                    $"{path}{number[0]}00",
                    $"{path}{number[1]}0",
                    $"{path}{number[2]}"
                }
            }; 

            QueueAudioFiles.Enqueue($"{path}ПРИГЛАШАЮТПРОЙТИ");
            QueueAudioFiles.Enqueue($"{path}КОКНУ");
            QueueAudioFiles.Enqueue($"{path}{windowName.Split("№")[1]}");

        }
    }
}
