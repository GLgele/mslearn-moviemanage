using Microsoft.SqlServer.Server;
using Newtonsoft.Json;
namespace RazorPagesMovie.Utils;
public static class Trans
{
    private static string? lang;
    private static Dictionary<string, string> dict = new();
    private static readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
    private static StreamReader? file;
    public static void Init()
    {
        logger.Info("Initializing Translation.");
        if (lang == "zh_cn")
        {
            try
            {
                file = new StreamReader(new FileStream("assets/lang/zh_cn.json", FileMode.Open, FileAccess.Read, FileShare.Read));
                var source = file.ReadToEnd();
                file.Close();
                source = source.Replace("\r", "").Replace("\n", "").Replace("\t", "");
                JsonTextReader reader = new(new StringReader(source));
                string tmp1 = "";
                string tmp2 = "";
                while (reader.Read())
                {
                    if (reader.Value != null)
                    {
                        //Console.WriteLine("Token: {0}, Value: {1}", reader.TokenType, reader.Value);
                        if (reader.TokenType == JsonToken.PropertyName)
                        {
                            tmp1 = reader.Value.ToString();
                        }
                        else if (reader.TokenType == JsonToken.String)
                        {
                            tmp2 = reader.Value.ToString();
                            dict[tmp1] = tmp2;
                            tmp1 = "";
                            tmp2 = "";
                        }
                    }
                }
            }
            catch (IOException)
            {
                logger.Error("Can't find language file: " + lang);
                lang = "en_us";
            }
        }
        else
        {
            lang = "en_us";
        }
        file.Dispose();
    }
    public static void Init(string l)
    {
        lang = l;
        Init();
    }
    public static string Tr(string s)
        {
            try
            {
                return dict[s];
            }
            catch (Exception)
            {
                logger.Warn(string.Format("Translate string not found! Source:({0})", s));
                return s;
            }
        }
}
