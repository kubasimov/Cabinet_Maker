using CoreS;
using CoreS.Model;
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
            var frm = new Start();
            frm.Show();
        }

        [CommandMethod("SzafkaClipboard")]
        public static void szafkaClipboard()
        {
            
            var clipboardText = Clipboard.GetText();
            
            var deserializeText = JsonConvert.DeserializeObject<CabinetModelDTO>(clipboardText.ToString());
            
            DrawC.DrawFront(deserializeText);

            
        }

        [CommandMethod("SzafkaClipboardGora")]
        public static void SzafkaClipboardGora()
        {
            var clipboardText = Clipboard.GetText();

            var deserializeText = JsonConvert.DeserializeObject<CabinetModelDTO>(clipboardText.ToString());

            DrawC.DrawTop(deserializeText);
        }
    }
}
