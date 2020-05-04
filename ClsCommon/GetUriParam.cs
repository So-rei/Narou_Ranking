using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClsCommon
{
    /// <summary>
    /// Get用の項目の値リスト
    /// </summary>
    public class GetUriParam
    {

        /// <summary>
        /// Getコマンド一覧 予約語と被るものがあるので"G"と先頭につけて回避している
        /// </summary>
        public enum Joken
        {
            //基本----------------
            Ggzip,
            Gout,
            Gof,
            Glim,
            Gorder,
            Gst,
            //条件抽出------------
            Gword,
            Gnotword,
            Gtitle,
            Gex,
            Gkeyword,
            Gwname,
            Gbiggenre,
            Gnotbiggenre,
            Ggenre,
            Gnotgenre,
            //登録必須タグ--------
            Guserid,//ユーザID
            Gisr15,
            Gisbl,
            Gisgl,
            Giszankoku,
            Gistensei,
            Gistenni,
            Gistt,
            Gisnor15,
            Gisnobl,
            Gisnogl,
            Gisnozankoku,
            Gisnotensei,
            Gisnotenni,
            Gisnott,
            //文字数---------------
            Gminlen,
            Gmaxlen,
            Glength,
            Gkaiwaritu,
            Gsasie,
            //特殊-----------------
            GNcode,
            Gtype,//小説タイプ
            Gbuntai,//文体指定
            Gstop,
            Glastup,
        }
        /// <summary>
        /// Getしたい情報を指定するときのコマンド
        /// </summary>
        public enum GetParam
        {
            t,
            n,
            u,
            w,
            s,
            bg,
            g,
            k,
            gf,
            gl,
            nt,
            e,
            ga,
            l,
            ti,
            i,
            ir,
            ibl,
            igl,
            izk,
            its,
            iti,
            p,
            gp,
            dp,
            wp,
            mp,
            qp,
            yp,
            f,
            imp,
            r,
            a,
            ah,
            sa,
            ka,
            nu,
            ua,
        }

        /// <summary>
        /// GetParamと対である、Outされるパラメーター
        /// </summary>
        public class OutParam
        {
            public string allcount { get; set; }
            public string title { get; set; }
            public string ncode { get; set; }
            public string userid { get; set; }
            public string writer { get; set; }
            public string story { get; set; }
            public string biggenre { get; set; }
            public string genre { get; set; }
            public string gensaku { get; set; }
            public string keyword { get; set; }
            public string general_firstup { get; set; }
            public string general_lastup { get; set; }
            public string noveltype { get; set; }

            public string end { get; set; }
            public string general_all_no { get; set; }
            public string length { get; set; }
            public string time { get; set; }
            public string isstop { get; set; }
            public string isr15 { get; set; }
            public string isbl { get; set; }
            public string isgl { get; set; }
            public string iszankoku { get; set; }
            public string istensei { get; set; }
            public string istenni { get; set; }
            public string pc_or_k { get; set; }
            public string global_point { get; set; }
            public string daily_point { get; set; }
            public string weekly_point { get; set; }
            public string monthly_point { get; set; }
            public string quarter_point { get; set; }
            public string yearly_point { get; set; }
            public string fav_novel_cnt { get; set; }
            public string impression_cnt { get; set; }
            public string review_cnt { get; set; }
            public string all_point { get; set; }
            public string all_hyoka_cnt { get; set; }
            public string sasie_cnt { get; set; }
            public string kaiwaritu { get; set; }
            public string novelupdated_at { get; set; }
            public string updated_at { get; set; }

            [JsonIgnore]
            public string novel_type { get { return noveltype; } set { this.noveltype = value; } }//「_」対策
        }
    }
}
