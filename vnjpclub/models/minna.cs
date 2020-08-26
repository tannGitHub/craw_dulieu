using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vnjpclub.models
{
    public class minna
    {
        int id;
        int no;
        string bai;
        string url;
	    string url_tu_vung;
	    string url_ngu_phap;
	    string url_luyen_doc;
	    string url_hoi_thoai;
	    string url_luyen_nghe;
	    string url_bai_tap;
	    string url_han_tu;
	    string url_kiem_tra;
        string url_tham_khao;
        int is_crawler;

        public int No { get => no; set => no = value; }
        public int Id { get => id; set => id = value; }
        public string Bai { get => bai; set => bai = value; }
        public string Url { get => url; set => url = value; }
        public string Url_tu_vung { get => url_tu_vung; set => url_tu_vung = value; }
        public string Url_ngu_phap { get => url_ngu_phap; set => url_ngu_phap = value; }
        public string Url_luyen_doc { get => url_luyen_doc; set => url_luyen_doc = value; }
        public string Url_hoi_thoai { get => url_hoi_thoai; set => url_hoi_thoai = value; }
        public string Url_luyen_nghe { get => url_luyen_nghe; set => url_luyen_nghe = value; }
        public string Url_bai_tap { get => url_bai_tap; set => url_bai_tap = value; }
        public string Url_han_tu { get => url_han_tu; set => url_han_tu = value; }
        public string Url_kiem_tra { get => url_kiem_tra; set => url_kiem_tra = value; }
        public string Url_tham_khao { get => url_tham_khao; set => url_tham_khao = value; }
        public int Is_crawler { get => is_crawler; set => is_crawler = value; }
    }
}
