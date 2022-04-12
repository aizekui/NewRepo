using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Reflection;

namespace WpfApp1
{
    public static class Init
    {
        public static bool Login = true;
        public static int Numc { get; set; } = 0;
        public static readonly string _data = Environment.CurrentDirectory + @"\game\dpk\data.spk";
        public static readonly string _lang = Environment.CurrentDirectory + @"\game\dpk\lang\zh_tw.spk";
        public static readonly string _scn = Environment.CurrentDirectory + @"\game\dpk\res\scn\__scn3.spk";
        public static readonly string _exe = Environment.CurrentDirectory + @"\game\release_djs\TenProxy.dat";
        public static readonly string _xml = Environment.CurrentDirectory + @"\game\release_djs\update_cfg.xml";
        public static readonly string _init = Environment.CurrentDirectory + @"\game\cfg\client\init.xml";
        public static readonly string _dj2 = Environment.CurrentDirectory + @"\game\release_djs\dj2.exe";
        public static readonly string _logexe = Environment.CurrentDirectory + @"\game\release_djs\patcher.config";
        public static readonly string _videos = Environment.CurrentDirectory + @"\game\avi\vides.mp4";

        public static MainWindow? _form;
        public static HttpClient? httpClient;
        public static string? Getmd5;
        public const string IP_xml = "123.129.217.71";
        public const string port = "877";
        public const string IP = "http://" + IP_xml + ":" + port + "/";
        public const string Filedrv = @"BackName/";
        public static Dictionary<object, object>? GetProperties<T>(T t)
        {
            var ret = new Dictionary<object, object>();
            if (t == null) { return null; }
            PropertyInfo[] properties = t.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
            if (properties.Length <= 0) { return null; }
            foreach (PropertyInfo item in properties)
            {
                string name = item.Name;
                object value = item.GetValue(t, null);
                if (item.PropertyType.IsValueType || item.PropertyType.Name.StartsWith("String"))
                {
                    ret.Add(name, value);
                }
            }
            return ret;
        }
    }
    public class Backlist
    {
        public static string Videos { get; set; } = File.Exists(Init._videos) ? Init._videos : @"E:\刀剑2\game\avi\vides.mp4";
        public static string Dj2 { get; set; } = File.Exists(Init._dj2) ? Init._dj2 : @"E:\刀剑2\game\release_djs\dj2.exe";
        public static string Dj2_log { get; set; } = File.Exists(Init._logexe) ? Init._logexe : @"E:\刀剑2\game\release_djs\patcher.config";
        public static string Iinit { get; set; } = File.Exists(Init._init) ? Init._init : @"E:\刀剑2\game\cfg\client\init.xml";
        public static string? Xxml { get; set; } = File.Exists(Init._xml) ? Init._xml : @"E:\刀剑2\game\release_djs\update_cfg.xml";
        public static string? Eexe { get; set; } = File.Exists(Init._exe) ? Init._exe : @"E:\刀剑2\game\release_djs\TenProxy.dat";
        public string? zh_tw { get; set; } = File.Exists(Init._lang) ? Init._lang : @"E:\刀剑2\game\dpk\lang\zh_tw.spk";
        public string? data { get; set; } = File.Exists(Init._data) ? Init._data : @"E:\刀剑2\game\dpk\data.spk";
        public string? __scn3 { get; set; } = File.Exists(Init._scn) ? Init._scn : @"E:\刀剑2\game\dpk\res\scn\__scn3.spk";
    }
    public class RootObject
    {
        public int? Status { get; set; }
        public string? Msg { get; set; }
        public string? __scn3 { get; set; }
        public string? zh_tw { get; set; }
        public string? data { get; set; }
    }
}
