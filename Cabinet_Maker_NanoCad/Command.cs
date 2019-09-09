using System.Windows;
using Core;
using Newtonsoft.Json;
using Teigha.Runtime;

namespace Cabinet_Maker_NanoCad
{
    public class Command
    {

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

            var z = Clipboard.GetData("nano");
            var t = JsonConvert.DeserializeObject < Cabinet >(z.ToString());

            r.drawC(t);
            
        }
    }
}
