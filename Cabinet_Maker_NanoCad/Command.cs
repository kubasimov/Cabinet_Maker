using CoreS;
using Newtonsoft.Json;
using NLog;
using System.Windows;
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
            
            var clipboardText = Clipboard.GetText();
            
            var deserializeText = JsonConvert.DeserializeObject<Cabinet>(clipboardText.ToString());
            
            DrawC.DrawFront(deserializeText);

            
        }

        [CommandMethod("SzafkaClipboardGora")]
        public static void SzafkaClipboardGora()
        {
            var clipboardText = Clipboard.GetText();

            var deserializeText = JsonConvert.DeserializeObject<Cabinet>(clipboardText.ToString());

            DrawC.DrawFront(deserializeText);
        }
    }
}
