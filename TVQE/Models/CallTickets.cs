using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TVQE.Models
{
    public class CallTickets
    {
        public List<string> AudioFiles { get; set; }
        public string Prefix {  get; set; }
        public string Number { get; set; }
        public string WindowName { get; set; }

        public CallTickets(string prefix,string number,string windowName) {

            Prefix = prefix;
            Number = number;
            WindowName = windowName;
            int nuberT = Convert.ToInt32(number);
            var path = System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory)));
            AudioFiles = new List<string>()
                {
                    path+"\\Озвучка\\ВНИМАНИЕ.mp3",
                    path+"\\Озвучка\\КЛИЕНТА.mp3",
                    path+"\\Озвучка\\НОМЕР.mp3" ,
                    path + "\\Озвучка\\" + prefix + ".mp3"
                };

            if (nuberT>20 && nuberT < 100 && nuberT % 10 == 0)
            { 
                AudioFiles.Add(path + "\\Озвучка\\" + number + ".mp3"); 
            }
            else
            if (nuberT < 21)
            { 
                AudioFiles.Add(path + "\\Озвучка\\" + number + ".mp3");
            }
            else 
           if (nuberT % 100 == 0)
            { 
                AudioFiles.Add(path + "\\Озвучка\\" + number + ".mp3");
            }
            else
           if (nuberT % 100 % 10 == 0)
            { 
                AudioFiles.Add(path + "\\Озвучка\\" + number[0] + "00.mp3");
                AudioFiles.Add(path + "\\Озвучка\\" + number[1] + "0.mp3");
            }
            else 
            if (nuberT % 10 == 0 && nuberT < 100 )
            { 
                AudioFiles.Add(path + "\\Озвучка\\" + number + ".mp3");
            }
            else  
            if (nuberT % 100 > 10 && nuberT % 100 < 20)
            { 
                AudioFiles.Add(path + "\\Озвучка\\" + number[0] + "00.mp3");
                AudioFiles.Add(path + "\\Озвучка\\" + number[1] + number[2] + ".mp3");
            }
            else
            if (nuberT % 100 < 10)
            {
                AudioFiles.Add(path + "\\Озвучка\\" + number[0] + "00.mp3");
                AudioFiles.Add(path + "\\Озвучка\\" + number[2] + ".mp3");
            } 
            else
            if (nuberT > 100)
            { 
                AudioFiles.Add(path + "\\Озвучка\\" + number[0] + "00.mp3");
                AudioFiles.Add(path + "\\Озвучка\\" + number[1] + "0.mp3");
                AudioFiles.Add(path + "\\Озвучка\\" + number[2] + ".mp3");
            }
            else
            if (nuberT > 20)
            {
                AudioFiles.Add(path + "\\Озвучка\\" + number[0] + "0.mp3");
                AudioFiles.Add(path + "\\Озвучка\\" + number[1] + ".mp3");
            }
            else
            if (nuberT < 20)
            {
                AudioFiles.Add(path + "\\Озвучка\\" + number + ".mp3");
            } 
            AudioFiles.Add(path + "\\Озвучка\\ПРИГЛАШАЮТПРОЙТИ.mp3");
            AudioFiles.Add(path + "\\Озвучка\\КОКНУ.mp3");
            AudioFiles.Add(path + "\\Озвучка\\" + windowName.Split("№")[1] + ".mp3");

        }
    }
}
