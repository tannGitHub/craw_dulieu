using System;
using System.Collections.Generic;
using System.Data.SQLite;
using vnjpclub.models;

namespace vnjpclub
{
    class controller
    {
        SQLiteConnection cnn;

        public controller()
        {

        }
        private bool connection()
        {
            string connetionString = "Data Source=D:\\c#\\vnjpclub\\db\\vnjpclub.db";
            cnn = new SQLiteConnection(connetionString);
            try
            {
                cnn.Open();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool insert_minna(string bai, string url, string url_tu_vung, string url_ngu_phap,string url_luyen_doc, string url_hoi_thoai, string url_luyen_nghe,
                                string url_bai_tap,string url_han_tu,string url_kiem_tra,string url_tham_khao)
        {
            try
            {
                connection();
                string cmdText = "INSERT INTO minna (bai, url, url_tu_vung, url_ngu_phap, url_luyen_doc, url_hoi_thoai, url_luyen_nghe, url_bai_tap, url_han_tu, url_kiem_tra, url_tham_khao) " +
                                            "VALUES (@bai, @url, @url_tu_vung, @url_ngu_phap, @url_luyen_doc, @url_hoi_thoai, @url_luyen_nghe, @url_bai_tap, @url_han_tu, @url_kiem_tra, @url_tham_khao)";
                SQLiteCommand cmd = new SQLiteCommand(cmdText, cnn);
                cmd.Parameters.AddWithValue("@bai", bai);
                cmd.Parameters.AddWithValue("@url", url);
                cmd.Parameters.AddWithValue("@url_tu_vung", url_tu_vung);
                cmd.Parameters.AddWithValue("@url_ngu_phap", url_ngu_phap);
                cmd.Parameters.AddWithValue("@url_luyen_doc", url_luyen_doc);
                cmd.Parameters.AddWithValue("@url_hoi_thoai", url_hoi_thoai);
                cmd.Parameters.AddWithValue("@url_luyen_nghe", url_luyen_nghe);
                cmd.Parameters.AddWithValue("@url_bai_tap", url_bai_tap);
                cmd.Parameters.AddWithValue("@url_han_tu", url_han_tu);
                cmd.Parameters.AddWithValue("@url_kiem_tra", url_kiem_tra);
                cmd.Parameters.AddWithValue("@url_tham_khao", url_tham_khao);
                cmd.ExecuteNonQuery();
                cnn.Close();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public List<minna> get_minnas()
        {
            List<minna> ls = new List<minna>();
            try
            {
                connection();
                string cmdText = "SELECT * FROM minna;";
                SQLiteCommand cmd = new SQLiteCommand(cmdText, cnn);
                SQLiteDataReader reader = cmd.ExecuteReader();

                int i = 0;
                while (reader.Read())
                {
                    i++;
                    minna mn = new minna();
                    mn.No = i;
                    mn.Id = reader.GetInt16(0);
                    mn.Bai = reader.GetString(1);
                    mn.Url = reader.GetString(2);
                    mn.Url_tu_vung = reader.GetString(3);
                    mn.Url_ngu_phap = reader.GetString(4);
                    mn.Url_luyen_doc = reader.GetString(5);
                    mn.Url_hoi_thoai = reader.GetString(6);
                    mn.Url_luyen_nghe = reader.GetString(7);
                    mn.Url_bai_tap = reader.GetString(8);
                    mn.Url_han_tu = reader.GetString(9);
                    mn.Url_kiem_tra = reader.GetString(10);
                    mn.Url_tham_khao = reader.GetString(11);
                    mn.Is_crawler = reader.GetInt16(12);
                    ls.Add(mn);
                }
                cnn.Close();

                return ls;
            }
            catch (Exception)
            {
                return ls;
            }
        }
        public bool insert_tu_vung(int minna_id, string tu_vung, string han_tu, string am_han, string phat_am, string nghia)
        {
            try
            {
                connection();
                string cmdText = "INSERT INTO tu_vung (minna_id, tu_vung, han_tu, am_han, phat_am, nghia) VALUES (@minna_id, @tu_vung, @han_tu, @am_han, @phat_am, @nghia)";
                SQLiteCommand cmd = new SQLiteCommand(cmdText, cnn);
                cmd.Parameters.AddWithValue("@minna_id", minna_id);
                cmd.Parameters.AddWithValue("@tu_vung", tu_vung);
                cmd.Parameters.AddWithValue("@han_tu", han_tu);
                cmd.Parameters.AddWithValue("@am_han", am_han);
                cmd.Parameters.AddWithValue("@phat_am", phat_am);
                cmd.Parameters.AddWithValue("@nghia", nghia);
                cmd.ExecuteNonQuery();
                cnn.Close();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
