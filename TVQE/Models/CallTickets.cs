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

            var path = System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory)));

            if (Convert.ToInt32(number) % 100 == 0)
            {
                AudioFiles = new List<string>()
                {
                    path+"\\Озвучка\\ВНИМАНИЕ.mp3",
                    path+"\\Озвучка\\КЛИЕНТА.mp3",
                    path+"\\Озвучка\\НОМЕР.mp3",
                    path+"\\Озвучка\\" + prefix + ".mp3",
                    path+"\\Озвучка\\" + number + ".mp3",
                    path+"\\Озвучка\\ПРИГЛАШАЮТПРОЙТИ.mp3",
                    path+"\\Озвучка\\КОКНУ.mp3",
                    path+"\\Озвучка\\"+windowName.Split("№")[1] +".mp3",
                };
            }
            else
           if (Convert.ToInt32(number) % 100 % 10 == 0)
            {
                AudioFiles = new List<string>()
                {
                    path+"\\Озвучка\\ВНИМАНИЕ.mp3",
                    path+"\\Озвучка\\КЛИЕНТА.mp3",
                    path+"\\Озвучка\\НОМЕР.mp3",
                    path+"\\Озвучка\\" + prefix + ".mp3",
                    path+"\\Озвучка\\" + number[0] + "00.mp3",
                    path+"\\Озвучка\\" + number[1] + "0.mp3",
                    path+"\\Озвучка\\ПРИГЛАШАЮТПРОЙТИ.mp3",
                    path+"\\Озвучка\\КОКНУ.mp3",
                    path+"\\Озвучка\\"+windowName.Split("№")[1] +".mp3",
                };
            }
            else 
            if (Convert.ToInt32(number) % 10 == 0 && Convert.ToInt32(number) < 100 )
            {
                AudioFiles = new List<string>()
                {
                    path+"\\Озвучка\\ВНИМАНИЕ.mp3",
                    path+"\\Озвучка\\КЛИЕНТА.mp3",
                    path+"\\Озвучка\\НОМЕР.mp3",
                    path+"\\Озвучка\\" + prefix + ".mp3",
                    path+"\\Озвучка\\" + number + ".mp3",
                    path+"\\Озвучка\\ПРИГЛАШАЮТПРОЙТИ.mp3",
                    path+"\\Озвучка\\КОКНУ.mp3",
                    path+"\\Озвучка\\"+windowName.Split("№")[1] +".mp3",
                };
            }
            else  
            if (Convert.ToInt32(number) % 100 > 10 && Convert.ToInt32(number) % 100 < 20)
            {
                AudioFiles = new List<string>()
                 {
                    path+"\\Озвучка\\ВНИМАНИЕ.mp3",
                    path+"\\Озвучка\\КЛИЕНТА.mp3",
                    path+"\\Озвучка\\НОМЕР.mp3",
                    path+"\\Озвучка\\" + prefix + ".mp3",
                    path+"\\Озвучка\\" + number[0] + "00.mp3",
                    path+"\\Озвучка\\" + number[1]+number[2] + ".mp3",
                    path+"\\Озвучка\\ПРИГЛАШАЮТПРОЙТИ.mp3",
                    path+"\\Озвучка\\КОКНУ.mp3",
                    path+"\\Озвучка\\"+windowName.Split("№")[1] +".mp3",
                };
            }
            else
            if (Convert.ToInt32(number) % 100 < 10)
            {
                AudioFiles = new List<string>()
                 {
                    path+"\\Озвучка\\ВНИМАНИЕ.mp3",
                    path+"\\Озвучка\\КЛИЕНТА.mp3",
                    path+"\\Озвучка\\НОМЕР.mp3",
                    path+"\\Озвучка\\" + prefix + ".mp3",
                    path+"\\Озвучка\\" + number[0] + "00.mp3",
                    path+"\\Озвучка\\" + number[2] + ".mp3",
                    path+"\\Озвучка\\ПРИГЛАШАЮТПРОЙТИ.mp3",
                    path+"\\Озвучка\\КОКНУ.mp3",
                    path+"\\Озвучка\\"+windowName.Split("№")[1] +".mp3",
                };
            } 
            else
            if (Convert.ToInt32(number) > 100)
            {
                AudioFiles = new List<string>()
                 {
                    path+"\\Озвучка\\ВНИМАНИЕ.mp3",
                    path+"\\Озвучка\\КЛИЕНТА.mp3",
                    path+"\\Озвучка\\НОМЕР.mp3",
                    path+"\\Озвучка\\" + prefix + ".mp3",
                    path+"\\Озвучка\\" + number[0] + "00.mp3",
                    path+"\\Озвучка\\" + number[1] + "0.mp3",
                    path+"\\Озвучка\\" + number[2] + ".mp3",
                    path+"\\Озвучка\\ПРИГЛАШАЮТПРОЙТИ.mp3",
                    path+"\\Озвучка\\КОКНУ.mp3",
                    path+"\\Озвучка\\"+windowName.Split("№")[1] +".mp3",
                };
            }
            else
            if (Convert.ToInt32(number) > 20)
            {
                AudioFiles = new List<string>()
                 {
                    path+"\\Озвучка\\ВНИМАНИЕ.mp3",
                    path+"\\Озвучка\\КЛИЕНТА.mp3",
                    path+"\\Озвучка\\НОМЕР.mp3",
                    path+"\\Озвучка\\" + prefix + ".mp3",
                    path+"\\Озвучка\\" + number[0] + "0.mp3",
                    path+"\\Озвучка\\" + number[1] + ".mp3",
                    path+"\\Озвучка\\ПРИГЛАШАЮТПРОЙТИ.mp3",
                    path+"\\Озвучка\\КОКНУ.mp3",
                    path+"\\Озвучка\\"+windowName.Split("№")[1] +".mp3",
                };
            }
            else
            if (Convert.ToInt32(number) < 20)
            {
                AudioFiles = new List<string>()
                {
                    path+"\\Озвучка\\ВНИМАНИЕ.mp3",
                    path+"\\Озвучка\\КЛИЕНТА.mp3",
                    path+"\\Озвучка\\НОМЕР.mp3",
                    path+"\\Озвучка\\" + prefix + ".mp3",
                    path+"\\Озвучка\\" + number + ".mp3",
                    path+"\\Озвучка\\ПРИГЛАШАЮТПРОЙТИ.mp3",
                    path+"\\Озвучка\\КОКНУ.mp3",
                    path+"\\Озвучка\\"+windowName.Split("№")[1] +".mp3",
                };
            }
        }
    }
}
