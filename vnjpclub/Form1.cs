using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using vnjpclub.models;

namespace vnjpclub
{
    public partial class Form1 : Form
    {
        controller controller = new controller();
        List<minna> minnas = new List<minna>();

        string home_url = "https://www.vnjpclub.com";
        List<string> error = new List<string>();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            refresh();
        }

        private void refresh()
        {
            minnas = controller.get_minnas();
            dataGridView1.DataSource = minnas;
        }

        private void writeLog(string err)
        {
            error.Add(DateTime.Now.ToString() + ": " +err);
            error.Add("======================================================================================================================================");
        }


        private void button9_Click(object sender, EventArgs e)
        {
            minna();
        }
        private void minna()
        {
            IWebDriver driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            try
            {
                driver.Navigate().GoToUrl("https://www.vnjpclub.com/minna-no-nihongo/");
                var htmlDocument = new HtmlAgilityPack.HtmlDocument();
                htmlDocument.LoadHtml(driver.PageSource);
                var divs = htmlDocument.DocumentNode.Descendants("div").Where(node => node.GetAttributeValue("class", "").Equals("cat-items")).ToList();
                var trs = divs[0].Descendants("tr");
                int i = 0;
                foreach (var tr in trs)
                {
                    i++;
                    var bai = tr.Descendants("a").FirstOrDefault().InnerHtml;
                    var url = home_url + tr.Descendants("a").FirstOrDefault().Attributes["href"].Value;
                    controller.insert_minna(bai.Trim(), url.Trim(),
                    "https://www.vnjpclub.com/minna-no-nihongo/bai-" + i.ToString() + "-tu-vung.html",
                    "https://www.vnjpclub.com/minna-no-nihongo/bai-" + i.ToString() + "-ngu-phap.html",
                    "https://www.vnjpclub.com/minna-no-nihongo/bai-" + i.ToString() + "-luyen-doc.html",
                    "https://www.vnjpclub.com/minna-no-nihongo/bai-" + i.ToString() + "-hoi-thoai.html",
                    "https://www.vnjpclub.com/minna-no-nihongo/bai-" + i.ToString() + "-luyen-nghe.html",
                    "https://www.vnjpclub.com/minna-no-nihongo/bai-" + i.ToString() + "-bai-tap.html",
                    "https://www.vnjpclub.com/minna-no-nihongo/bai-" + i.ToString() + "-han-tu.html",
                    "https://www.vnjpclub.com/minna-no-nihongo/bai-" + i.ToString() + "-kiem-tra.html",
                    "https://www.vnjpclub.com/minna-no-nihongo/bai-" + i.ToString() + "-tham-khao.html"
                    );
                }

                driver.Close();
                Thread.Sleep(5000);
            }
            catch (Exception e)
            {
                writeLog("minna: error: " + e.ToString());
            }
            System.IO.File.WriteAllLines(@"D:\c#\vnjpclub\Error_minna.txt", error);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (var mn in minnas)
            {
                tu_vung(mn.Id, mn.Url_tu_vung);
            }
        }

        private void tu_vung(int id, string url)
        {
            IWebDriver driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl(url);
            var htmlDocument = new HtmlAgilityPack.HtmlDocument();
            htmlDocument.LoadHtml(driver.PageSource);
            driver.Close();
            var tbody = htmlDocument.DocumentNode.Descendants("tbody").ToList();
            var trs = tbody[0].Descendants("tr");
            int i = 0;
            foreach (var tr in trs)
            {
                int minna_id = id;
                string tu_vung = "";
                string han_tu = "";
                string am_han = "";
                string phat_am = "";
                string nghia = ""; 

                try
                {
                    var tds = tr.Descendants("td").ToList();
                    string mp3 = id.ToString() + "_" + i++;
                    if (tds.Count == 5)
                    {
                        tu_vung = tds[0].InnerHtml != null ? tds[0].InnerHtml : "";
                        han_tu = tds[1].InnerHtml != null ? tds[1].InnerHtml : "";
                        am_han = tds[2].InnerHtml != null ? tds[2].InnerHtml : "";
                        phat_am = tds[3].InnerHtml != null ? tds[3].Descendants("audio").FirstOrDefault().Attributes["src"].Value : "";
                        if (phat_am != "")
                        {
                            save_mp3(home_url + phat_am, @"D:\c#\vnjpclub\db\minna\tu_vung\mp3\" + mp3 + ".mp3");
                        }
                        nghia = tds[4].InnerHtml;
                        controller.insert_tu_vung(minna_id, tu_vung, han_tu, am_han, mp3, nghia);
                    }
                    else if (tds.Count == 3)
                    {
                        tu_vung = tds[0].InnerHtml != null ? tds[0].InnerHtml : "";
                        phat_am = tds[1].InnerHtml != null ? tds[1].Descendants("audio").FirstOrDefault().Attributes["src"].Value : "";
                        if (phat_am != "")
                        {
                            save_mp3(home_url + phat_am, @"D:\c#\vnjpclub\db\minna\tu_vung\mp3\" + mp3 + ".mp3");
                        }
                        nghia = tds[2].InnerHtml;
                        controller.insert_tu_vung(minna_id, tu_vung, han_tu, am_han, mp3, nghia);
                    }
                }
                catch (Exception)
                {
                    tu_vung = "＜練習 C＞";
                }
            }

        }

        private void save_mp3(string myWebUrlFile, string myLocalFilePath)
        {
            using (var client = new WebClient())
            {
                client.DownloadFile(myWebUrlFile, myLocalFilePath);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ngu_phap();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            luyen_doc();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            hoi_thoai();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            luyen_nghe();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            bai_tap();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            han_tu();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            tham_khao();
        }


        private void ngu_phap()
        {
            List<string> ls = new List<string>();
            for (int i = 1; i <= 50; i++)
            {
                IWebDriver driver = new ChromeDriver();
                driver.Manage().Window.Maximize();
                driver.Navigate().GoToUrl("https://www.vnjpclub.com/minna-no-nihongo/bai-" + i.ToString() + "-ngu-phap.html");
                var htmlDocument = new HtmlAgilityPack.HtmlDocument();
                htmlDocument.LoadHtml(driver.PageSource);
                var divs = htmlDocument.DocumentNode.Descendants("div").Where(node => node.GetAttributeValue("class", "").Equals("tab_container")).ToList();
                var table = divs[0].InnerHtml.Trim();

                ls.Add("Bài " + i.ToString() + " - Ngữ pháp");
                ls.Add(table);
                ls.Add("------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
                driver.Close();
                Thread.Sleep(5000);
            }
            System.IO.File.WriteAllLines(@"D:\c#\vnjpclub\ngu_phap.txt", ls);

        }

        private void luyen_doc()
        {
            List<string> ls = new List<string>();
            for (int i = 1; i <= 50; i++)
            {
                IWebDriver driver = new ChromeDriver();
                driver.Manage().Window.Maximize();
                driver.Navigate().GoToUrl("https://www.vnjpclub.com/minna-no-nihongo/bai-" + i.ToString() + "-luyen-doc.html");
                var htmlDocument = new HtmlAgilityPack.HtmlDocument();
                htmlDocument.LoadHtml(driver.PageSource);
                var divs = htmlDocument.DocumentNode.Descendants("div").Where(node => node.GetAttributeValue("class", "").Equals("tab_container")).ToList();
                var table = divs[0].InnerHtml.Trim();

                ls.Add("Bài " + i.ToString() + " - Luyện đọc");
                ls.Add(table);
                ls.Add("------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
                driver.Close();
                Thread.Sleep(5000);
            }
            System.IO.File.WriteAllLines(@"D:\c#\vnjpclub\luyen_doc.txt", ls);
        }

        private void hoi_thoai()
        {
            List<string> ls = new List<string>();
            for (int i = 1; i <= 50; i++)
            {
                IWebDriver driver = new ChromeDriver();
                driver.Manage().Window.Maximize();
                driver.Navigate().GoToUrl("https://www.vnjpclub.com/minna-no-nihongo/bai-" + i.ToString() + "-hoi-thoai.html");
                var htmlDocument = new HtmlAgilityPack.HtmlDocument();
                htmlDocument.LoadHtml(driver.PageSource);
                var divs = htmlDocument.DocumentNode.Descendants("div").Where(node => node.GetAttributeValue("class", "").Equals("tab_container")).ToList();
                var table = divs[0].InnerHtml.Trim();

                ls.Add("Bài " + i.ToString() + " - Hội thoại");
                ls.Add(table);
                ls.Add("------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
                driver.Close();
                Thread.Sleep(5000);
            }
            System.IO.File.WriteAllLines(@"D:\c#\vnjpclub\hoi_thoai.txt", ls);
        }

        private void luyen_nghe()
        {
            List<string> ls = new List<string>();
            for (int i = 1; i <= 50; i++)
            {
                IWebDriver driver = new ChromeDriver();
                driver.Manage().Window.Maximize();
                driver.Navigate().GoToUrl("https://www.vnjpclub.com/minna-no-nihongo/bai-" + i.ToString() + "-luyen-nghe.html");
                var htmlDocument = new HtmlAgilityPack.HtmlDocument();
                htmlDocument.LoadHtml(driver.PageSource);
                var divs = htmlDocument.DocumentNode.Descendants("div").Where(node => node.GetAttributeValue("class", "").Equals("tab_container")).ToList();
                var table = divs[0].InnerHtml.Trim();

                ls.Add("Bài " + i.ToString() + " - Luyện nghe");
                ls.Add(table);
                ls.Add("------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
                driver.Close();
                Thread.Sleep(5000);
            }
            System.IO.File.WriteAllLines(@"D:\c#\vnjpclub\luyen_nghe.txt", ls);
        }

        private void bai_tap()
        {
            List<string> ls = new List<string>();
            for (int i = 1; i <= 50; i++)
            {
                try
                {
                    IWebDriver driver = new ChromeDriver();
                    driver.Manage().Window.Maximize();
                    driver.Navigate().GoToUrl("https://www.vnjpclub.com/minna-no-nihongo/bai-" + i.ToString() + "-bai-tap.html");
                    var htmlDocument = new HtmlAgilityPack.HtmlDocument();
                    htmlDocument.LoadHtml(driver.PageSource);
                    var divs = htmlDocument.DocumentNode.Descendants("div").Where(node => node.GetAttributeValue("class", "").Equals("tab_container")).ToList();
                    var table = divs[0].InnerHtml.Trim();

                    ls.Add("Bài " + i.ToString() + " - Bài tập");
                    ls.Add(table);
                    ls.Add("------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
                    driver.Close();
                    Thread.Sleep(5000);
                }
                catch (Exception e)
                {
                    writeLog("bai_tap: i = " + i + ", error: " + e.ToString());
                }
            }
            System.IO.File.WriteAllLines(@"D:\c#\vnjpclub\bai_tap.txt", ls);
        }

        private void han_tu()
        {
            List<string> ls = new List<string>();
            for (int i = 1; i <= 50; i++)
            {
                try
                {
                    IWebDriver driver = new ChromeDriver();
                    driver.Manage().Window.Maximize();
                    driver.Navigate().GoToUrl("https://www.vnjpclub.com/minna-no-nihongo/bai-" + i.ToString() + "-han-tu.html");
                    var htmlDocument = new HtmlAgilityPack.HtmlDocument();
                    htmlDocument.LoadHtml(driver.PageSource);
                    var divs = htmlDocument.DocumentNode.Descendants("div").Where(node => node.GetAttributeValue("id", "").Equals("khungchinhgiua")).ToList();
                    var table = divs[0].Descendants("table").FirstOrDefault().InnerHtml.Trim();

                    ls.Add("Bài " + i.ToString() + " - Hán từ");
                    ls.Add(table);
                    ls.Add("------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
                    driver.Close();
                    Thread.Sleep(5000);
                }
                catch (Exception e)
                {
                    writeLog("han_tu: i = " + i + ", error: " + e.ToString());
                }
            }
            System.IO.File.WriteAllLines(@"D:\c#\vnjpclub\han_tu.txt", ls);
        }

        private void tham_khao()
        {
            List<string> ls = new List<string>();
            for (int i = 1; i <= 50; i++)
            {
                try
                {
                    IWebDriver driver = new ChromeDriver();
                driver.Manage().Window.Maximize();
                driver.Navigate().GoToUrl("https://www.vnjpclub.com/minna-no-nihongo/bai-" + i.ToString() + "-tham-khao.html");
                var htmlDocument = new HtmlAgilityPack.HtmlDocument();
                htmlDocument.LoadHtml(driver.PageSource);
                var divs = htmlDocument.DocumentNode.Descendants("div").Where(node => node.GetAttributeValue("id", "").Equals("khungchinhgiua")).ToList();
                var table = divs[0].Descendants("table").FirstOrDefault().InnerHtml.Trim();

                ls.Add("Bài " + i.ToString() + " - Tham khảo");
                ls.Add(table);
                ls.Add("------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
                driver.Close();
                Thread.Sleep(5000);
                }
                catch (Exception e)
                {
                    writeLog("tham_khao: i = " + i + ", error: " + e.ToString());
                }
            }
            System.IO.File.WriteAllLines(@"D:\c#\vnjpclub\tham_khao.txt", ls);
        }

        
    }
}
