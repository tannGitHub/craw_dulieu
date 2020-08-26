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
    public partial class frmCraw : Form
    {
        controller controller = new controller();
        List<minna> minnas = new List<minna>();
        String commonPath = String.Empty;
        String sach_mina_cu = "https://www.vnjpclub.com/minna-no-nihongo-1998/";

        string home_url = "https://www.vnjpclub.com";
        List<string> error = new List<string>();
        public frmCraw()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            commonPath = @"D:\Craw";
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


        private void btnMinaCu_Click(object sender, EventArgs e)
        {
            minnacu();
        }
        private void minnacu()
        {
            IWebDriver driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            try
            {
                driver.Navigate().GoToUrl(sach_mina_cu);
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
                    sach_mina_cu + "/bai-" + i.ToString() + "-tu-vung.html",
                    sach_mina_cu + "/bai-" + i.ToString() + "-ngu-phap.html",
                    sach_mina_cu + "/bai-" + i.ToString() + "-luyen-doc.html",
                    sach_mina_cu + "/bai-" + i.ToString() + "-hoi-thoai.html",
                    sach_mina_cu + "/bai-" + i.ToString() + "-luyen-nghe.html",
                    sach_mina_cu + "/bai-" + i.ToString() + "-bai-tap.html",
                    sach_mina_cu + "/bai-" + i.ToString() + "-han-tu.html",
                    sach_mina_cu + "/bai-" + i.ToString() + "-kiem-tra.html",
                    sach_mina_cu + "/bai-" + i.ToString() + "-tham-khao.html"
                    );
                }

                driver.Close();
                Thread.Sleep(5000);
            }
            catch (Exception e)
            {
                writeLog("minna: error: " + e.ToString());
            }
            System.IO.File.WriteAllLines(commonPath + @"\Error_minna.txt", error);
        }

        private void btnTuVung_Click(object sender, EventArgs e)
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
                            save_mp3(home_url + phat_am, commonPath + @"\db\minna\tu_vung\mp3\" + mp3 + ".mp3");
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
                            save_mp3(home_url + phat_am, commonPath + @"\db\minna\tu_vung\mp3\" + mp3 + ".mp3");
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

        private void btnNguPhap_Click(object sender, EventArgs e)
        {
            ngu_phap();
        }

        private void btnLuyenDoc_Click(object sender, EventArgs e)
        {
            luyen_doc();
        }

        private void btnHoiThoai_Click(object sender, EventArgs e)
        {
            hoi_thoai();
        }

        private void btnLuyenNghe_Click(object sender, EventArgs e)
        {
            luyen_nghe();
        }

        private void btnBaiTap_Click(object sender, EventArgs e)
        {
            bai_tap();
        }

        private void btnHanTu_Click(object sender, EventArgs e)
        {
            han_tu();
        }

        private void btnKiemTra_Click(object sender, EventArgs e)
        {
            kiem_tra();
        }

        private void kiem_tra()
        {
            throw new NotImplementedException();
        }

        private void ngu_phap()
        {
            List<string> ls = new List<string>();
            for (int i = 1; i <= 50; i++)
            {
                IWebDriver driver = new ChromeDriver();
                driver.Manage().Window.Maximize();
                driver.Navigate().GoToUrl(sach_mina_cu + "/bai-"  + i.ToString() + "-ngu-phap.html");
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
            System.IO.File.WriteAllLines(commonPath + @"\ngu_phap.txt", ls);

        }

        private void luyen_doc()
        {
            List<string> ls = new List<string>();
            for (int i = 1; i <= 50; i++)
            {
                IWebDriver driver = new ChromeDriver();
                driver.Manage().Window.Maximize();
                driver.Navigate().GoToUrl(sach_mina_cu + "/bai-" + i.ToString() + "-luyen-doc.html");
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
            System.IO.File.WriteAllLines(commonPath + @"\luyen_doc.txt", ls);
        }

        private void hoi_thoai()
        {
            List<string> ls = new List<string>();
            for (int i = 1; i <= 50; i++)
            {
                IWebDriver driver = new ChromeDriver();
                driver.Manage().Window.Maximize();
                driver.Navigate().GoToUrl(sach_mina_cu + "/bai-" + i.ToString() + "-hoi-thoai.html");
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
            System.IO.File.WriteAllLines(commonPath + @"\hoi_thoai.txt", ls);
        }

        private void luyen_nghe()
        {
            List<string> ls = new List<string>();
            for (int i = 1; i <= 50; i++)
            {
                IWebDriver driver = new ChromeDriver();
                driver.Manage().Window.Maximize();
                driver.Navigate().GoToUrl(sach_mina_cu + "/bai-" + i.ToString() + "-luyen-nghe.html");
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
            System.IO.File.WriteAllLines(commonPath + @"\luyen_nghe.txt", ls);
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
                    driver.Navigate().GoToUrl(sach_mina_cu + "/bai-" + i.ToString() + "-bai-tap.html");
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
            System.IO.File.WriteAllLines(commonPath + @"\bai_tap.txt", ls);
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
                    driver.Navigate().GoToUrl(sach_mina_cu + "/bai-" + i.ToString() + "-han-tu.html");
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
            System.IO.File.WriteAllLines(commonPath + @"\han_tu.txt", ls);
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
                driver.Navigate().GoToUrl(sach_mina_cu + "/bai-" + i.ToString() + "-tham-khao.html");
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
            System.IO.File.WriteAllLines(commonPath + @"\tham_khao.txt", ls);
        }

        private void btnThamKhao_Click(object sender, EventArgs e)
        {
            tham_khao();
        }
    }
}
