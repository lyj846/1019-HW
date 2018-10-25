using System;
using _1019.Models;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using _1019_hw;

namespace _1019
{
    public class Program
    {
        static void Main(string[] args)
        {
            ImportService importService = new ImportService();
            var nodes = importService.FindOpenData();
            importService.ImportToDb(nodes);
            ShowOpenData(nodes);
            Console.ReadKey();
        }

        public static void ShowOpenData(List<OpenData> nodes)
        {
            Console.WriteLine(string.Format("共收到{0}筆的資料", nodes.Count));
            nodes.GroupBy(node => node.稅目別).ToList()
                .ForEach(group =>
                {
                    var key = group.Key;
                    var groupDatas = group.ToList();
                    var message = $"稅目別:{key},共有{groupDatas.Count()}筆資料";
                    Console.WriteLine(message);
                });
        }
        
    }
}