using System.Windows;
using CoreS;
using Newtonsoft.Json;
using NLog;
using Teigha.Runtime;

namespace Cabinet_Maker_NanoCad
{
    public class Command
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        [CommandMethod("Szafka")]
        public static void Szafka()
        {
            //rysowanie szafki z pliku

            //var filename = @"D:\Hasla\settings.json";
            //_settings = File.Exists(filename)
            //    ? JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText(filename))
            //: new Dictionary<string, string>();

            var frm = new Start();
            frm.Show();
        }

        [CommandMethod("SzafkaClipboard")]
        public static void szafkaClipboard()
        {
            var r = new DrawC();
            //System.IO.File.WriteAllText(@"C:\TEST\WriteText.txt", "SzafkaClipboard\n");

            var z = Clipboard.GetText();
            //System.IO.File.AppendAllText(@"C:\TEST\WriteText.txt", z.ToString());

            var t = JsonConvert.DeserializeObject<Cabinet>(z.ToString());
            //System.IO.File.AppendAllText(@"C:\TEST\WriteText.txt", t.CabinetElements.Count.ToString());

            r.drawC(t);
            
        }
    }
}
