using _1019.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace _1019_hw
{
    public class ImportService
    {
        public List<OpenData> FindOpenData()
        {

            List<OpenData> result = new List<OpenData>();

            string baseDir = Directory.GetCurrentDirectory();
            //@"taipeiFactory.xml"
            //var xml = XElement.Load(System.IO.Path.Combine(baseDir, @"https://data.kcg.gov.tw/dataset/a1f496df-8fc1-424f-83c3-6c76b0c14496/resource/e4c6fda4-b261-4d70-af9f-f92c9390e75c/download/xml75.xml"));
            var xml = XElement.Load(@"https://data.kcg.gov.tw/dataset/a1f496df-8fc1-424f-83c3-6c76b0c14496/resource/e4c6fda4-b261-4d70-af9f-f92c9390e75c/download/xml75.xml");
            var nodes = xml.Descendants("各項稅捐徵課費用").ToList();
            result = nodes
                .Where(x => !x.IsEmpty).ToList()
                .Select(node =>
                {
                    OpenData item = new OpenData();
                   // 資料年度,統計項目,稅目別,資料單位,值
                    item.資料年度 = getValue(node, "資料年度");
                    item.統計項目 = getValue(node, "統計項目");
                    item.稅目別 = getValue(node, "稅目別");
                    item.資料單位 = getValue(node, "資料單位");
                    item.值 = getValue(node, "值");
                 
                    return item;
                }).ToList();

            return result;
        }

        public void ImportToDb(List<OpenData> openDatas)
        {
            Repository.OpenDataRepository Repository = new Repository.OpenDataRepository();
            openDatas.ForEach(item => {
                Repository.Insert(item);
            });
        }

        private string getValue(XElement node, string header)
        {
            return node.Element(header)?.Value?.Trim();
        }
    }
}
