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
    }
}
