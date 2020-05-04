using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClsCommon
{
    /// <summary>
    /// Get用の項目の値リスト
    /// </summary>
    public class Params_Get
    {

        /// <summary>
        /// Getコマンド一覧 予約語と被るものがあるので"G"と先頭につけて回避している
        /// </summary>
        public enum Joken
        {
            //基本----------------------------------------
            Ggzip = 0,
            Gout,
            Gof,
            Glim,
            Gorder,
            Gst,
            //条件抽出------------------------------------
            Gword,
            Gnotword,
            //以下４つはGwordまたはnotwordで選択したときのみ 1または0
            Gtitle,
            Gex,
            Gkeyword,
            Gwname,
            /// <summary>enum.EBiggenre</summary>
            Gbiggenre,
            /// <summary>enum.EBiggenre</summary>
            Gnotbiggenre,
            /// <summary>enum.EGenre</summary>
            Ggenre,
            /// <summary>enum.EGenre</summary>
            Gnotgenre,
            //登録必須タグ--------------------------------
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
            //文字数---------------------------------------
            Gminlen,
            Gmaxlen,
            Glength,//minlenまたはmaxlenと併用はできません
            Gkaiwaritu,
            Gsasie,
            //特殊-----------------------------------------
            GNcode,
            /// <summary>
            /// 小説タイプ Etype
            /// </summary>
            Gtype,
            /// <summary>
            /// 文体指定 Ebuntai
            /// </summary>
            Gbuntai,
            /// <summary>
            /// 1：長期連載停止中を除きます　2：長期連載停止中のみ取得します
            /// </summary>
            Gstop,
            /// <summary>
            /// thisweek：今週(日曜日の午前0時はじまり)
            /// lastweek：先週
            /// sevenday：過去7日間(7日前の午前0時はじまり)
            /// thismonth：今月
            /// lastmonth：先月
            /// </summary>
            Glastup,
        }

        public string[,] Etype =
        {
            {"短編","t" },
            {"連載中","r" },
            {"完結済連載小説","er" },
            {"すべての連載小説(連載中および完結済)","re" },
            {"短編と完結済連載小説","ter" }
        };
        public string[,] Ebuntai =
        {
            {"字下げされておらず、連続改行が多い作品","1" },
            {"字下げされていないが、改行数は平均な作品","2" },
            {"字下げが適切だが、連続改行が多い作品","4" },
            {"字下げが適切でかつ改行数も平均な作品 ","6" },
        };

        /// <summary>
        /// ソート名　予約語と被るものがあるので"G"と先頭につけて回避している
        /// </summary>
        public enum EOrder
        {
            Gnew,//新着更新順
            Gfavnovelcnt,    //ブックマーク数の多い順
            Greviewcnt,//   レビュー数の多い順
            Ghyoka,//   総合ポイントの高い順
            Ghyokaasc,//    総合ポイントの低い順
            Gdailypoint,//日間ポイントの高い順
            Gweeklypoint,//   週間ポイントの高い順
            Gmonthlypoint,// 月間ポイントの高い順
            Gquarterpoint,// 四半期ポイントの高い順
            Gyearlypoint,//     年間ポイントの高い順
            Gimpressioncnt,//感想の多い順
            Ghyokacnt,// 評価者数の多い順
            Ghyokacntasc,//    評価者数の少ない順
            Gweekly,// 週間ユニークユーザの多い順 ..毎週火曜日早朝リセット(前週の日曜日から土曜日分)
            Glengthdesc,// 小説本文の文字数が多い順
            Glengthasc,// 小説本文の文字数が少ない順
            Gncodedesc,//  新着投稿順
            Gold,// 更新が古い順
        }

        //-----------------------------------------------------------------------------------------------------

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

    }
    public class Params_Common
    {
        public enum EBiggenre
        {
            恋愛 = 1,
            ファンタジー = 2,
            文芸 = 3,
            SF = 4,
            その他 = 99,
            ノンジャンル = 98,
        }
        public enum EGenre
        {
            異世界恋愛 = 101,
            現実世界恋愛 = 102,
            ハイファンタジー = 201,
            ローファンタジー = 202,
            純文学 = 301,
            ヒューマンドラマ = 302,
            歴史 = 303,
            推理 = 304,
            ホラー = 305,
            アクション = 306,
            コメディー = 307,
            VRゲーム = 401,
            宇宙 = 402,
            空想科学 = 403,
            パニック = 404,
            童話 = 9901,
            詩 = 9902,
            エッセイ = 9903,
            リプレイ = 9904,
            その他 = 9999,
            ノンジャンル = 9801,
        }
    }

    /// <summary>
    /// GetParamと対である、Outされるパラメーター
    /// シリアライズオブジェクト
    /// </summary>
    public class Params_Set
    {
        /// <summary>
        /// 全小説出力数(件数) コレクション１個目にはコレのみセットされる
        /// </summary>
        public string allcount { get; set; }

        //コレクション２個目以降は以下の項目がセットされる

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
        public string novel_type { get; set; }//「_」対策

    }
}
