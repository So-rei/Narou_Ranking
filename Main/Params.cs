using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
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

        /// <summary>
        /// タイトル
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 小説コード（URLと直結）
        /// </summary>
        public string ncode { get; set; }
        /// <summary>
        /// ユーザーID
        /// </summary>
        public string userid { get; set; }
        /// <summary>
        /// ユーザー名（ペンネームが設定できるため、ユーザーIDと１：１ではない）
        /// </summary>
        public string writer { get; set; }
        /// <summary>
        /// あらすじ
        /// </summary>
        public string story { get; set; }
        /// <summary>
        /// 大ジャンル
        /// </summary>
        public string biggenre { get; set; }
        /// <summary>
        /// 小ジャンル
        /// </summary>
        public string genre { get; set; }
        /// <summary>
        /// ■未使用項目
        /// </summary>
        public string gensaku { get; set; }
        /// <summary>
        /// キーワード　
        /// </summary>
        public string keyword { get; set; }
        /// <summary>
        /// 初回掲載日
        /// </summary>
        public string general_firstup { get; set; }
        /// <summary>
        /// 最終掲載日
        /// </summary>
        public string general_lastup { get; set; }
        /// <summary>
        /// 連載=1　短編=2
        /// </summary>
        public string noveltype { get; set; }
        /// <summary>
        /// 短編or完結=0 連載=1
        /// </summary>
        public string end { get; set; }
        /// <summary>
        /// 短編=1 連載=話数
        /// </summary>
        public string general_all_no { get; set; }
        /// <summary>
        /// 文字数
        /// </summary>
        public string length { get; set; }
        public string time { get; set; }
        /// <summary>
        /// 長期連載停止中=1
        /// </summary>
        public string isstop { get; set; }
        public string isr15 { get; set; }
        public string isbl { get; set; }
        public string isgl { get; set; }
        public string iszankoku { get; set; }
        public string istensei { get; set; }
        public string istenni { get; set; }
        public string pc_or_k { get; set; }
        /// <summary>
        /// 総合評価ポイント(ブックマーク数×2)+評価ポイント) 
        /// </summary>
        public string global_point { get; set; }
        public string daily_point { get; set; }
        public string weekly_point { get; set; }
        public string monthly_point { get; set; }
        public string quarter_point { get; set; }
        public string yearly_point { get; set; }
        public string fav_novel_cnt { get; set; }
        public string impression_cnt { get; set; }
        public string review_cnt { get; set; }
        /// <summary>
        /// 評価ポイント　※受付停止中だと0ptになる
        /// </summary>
        public string all_point { get; set; }
        public string all_hyoka_cnt { get; set; }
        public string sasie_cnt { get; set; }
        public string kaiwaritu { get; set; }
        public string novelupdated_at { get; set; }
        public string updated_at { get; set; }
        public string novel_type { get; set; }//「_」対策
    }

    /// <summary>
    /// Params_Set情報をもとに、表示値を定義するクラス
    /// </summary>
    public class ModelSetParams
    {
        /// <summary>
        /// サーバーから受け取る１つの作品の基本情報(json形式)
        /// </summary>
        [JsonIgnore]
        public readonly Params_Set ps;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="_ps"></param>
        public ModelSetParams(Params_Set _ps)
        {
            ps = _ps;
        }


        [JsonIgnore]
        public string _biggenre
        {
            get
            {
                return ((Params_Common.EBiggenre)(Convert.ToInt32(ps.biggenre))).ToString();
            }
        }

        [JsonIgnore]
        public string _genre
        {
            get
            {
                return ((Params_Common.EGenre)(Convert.ToInt32(ps.genre))).ToString();
            }
        }

        [JsonIgnore]
        public string _end
        {
            get
            {
                if (ps.novel_type == "1")
                {
                    return "全" + ps.general_all_no + "部分" + (ps.end == "0" ? "(完結済)" : "");
                }
                else
                {
                    return "短編";
                }
            }
        }

        [JsonIgnore]
        public string _isstop
        {
            get
            {
                return (ps.isstop == "1" ? "長期連載停止中" : "");
            }
        }
        [JsonIgnore]
        public string _isr15
        {
            get
            {
                return (ps.isr15 == "1" ? "R15" : "");
            }
        }
        [JsonIgnore]
        public string _isbl
        {
            get
            {
                return (ps.isbl == "1" ? "ボーイズラブ" : "");
            }
        }
        [JsonIgnore]
        public string _isgl
        {
            get
            {
                return (ps.isgl == "1" ? "ガールズラブ" : "");
            }
        }
        [JsonIgnore]
        public string _iszankoku
        {
            get
            {
                return (ps.iszankoku == "1" ? "残酷な描写あり" : "");
            }
        }
        [JsonIgnore]
        public string _istensei
        {
            get
            {
                return (ps.istensei == "1" ? "異世界転生" : "");
            }
        }
        [JsonIgnore]
        public string _istenni
        {
            get
            {
                return (ps.istenni == "1" ? "異世界転移" : "");
            }
        }
        [JsonIgnore]
        public string _issasie
        {
            get
            {
                return (Convert.ToInt32(ps.sasie_cnt) > 0 ? "挿絵有" : "");
            }
        }

        [JsonIgnore]
        public string _isbook
        {
            get
            {
                foreach (var s in ClsCommon.ISBOOK)
                {
                    if ((ps.keyword + ps.story).Contains(s)) return "書籍化";
                }
                return "";
            }
        }
        [JsonIgnore]
        public string _iscomic
        {
            get
            {
                foreach (var s in ClsCommon.ISCOMIC)
                {
                    if ((ps.keyword + ps.story).Contains(s)) return "コミカライズ";
                }
                return "";
            }
        }
        [JsonIgnore]
        public string _is
        {
            get
            {
                var s = "";
                s += (ps.isstop == "1" ? "長期連載停止中" : "");
                s += (ps.isr15 == "1" ? "R15" : "");
                s += (ps.isbl == "1" ? "ボーイズラブ" : "");
                s += (ps.isgl == "1" ? "ガールズラブ" : "");
                s += (ps.iszankoku == "1" ? "残酷な描写あり" : "");
                s += (ps.istensei == "1" ? "異世界転生" : "");
                s += (ps.istenni == "1" ? "異世界転移" : "");
                s += (Convert.ToInt32(ps.sasie_cnt) > 0 ? "挿絵有" : "");
                return s;
            }
        }

        /// <summary>
        ///タイトル(URL)
        /// </summary>
        [JsonIgnore]
        public string _url
        {
            get
            {
                return ClsCommon.NOVELURL + ps.ncode + "/";
            }
        }

        /// <summary>
        /// 作者別情報
        /// 必要になってから手動でセットすること！
        /// </summary>
        [JsonIgnore]
        public UserDetail UD { get; set; } = new UserDetail();
    }

    /// <summary>
    ///作者別情報
    /// </summary>
    public class UserDetail
    {
        /// <summary>
        /// コンストラクタ(null回避用
        /// </summary>
        public UserDetail()
        {
        }
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="_ps"></param>
        public UserDetail(IReadOnlyList<Params_Set> _ps)
        {
            //var mostvaluable = _ps.First(t => Convert.ToInt32(t.all_point) == (_ps.Max(s => Convert.ToInt32(s.all_point))));//あらかじめ評価Pでソートしているのでコレは不要になった
            var mostvaluable = _ps[1];
            userid = mostvaluable.userid;
            most_valuable_title = mostvaluable.title;
            most_valuable_url = mostvaluable.ncode;
            most_point = Convert.ToInt32(mostvaluable.global_point);

            //連載かつ長期連載停止中／全作品数(10話以下or短編は3個で1カウント) =エタ率
            eternal_per = 0;
            var bunsi = _ps.Count(t => t.novel_type == "1" && t.isstop == "1");
            if (bunsi > 0)
            {
                eternal_per = 100 * bunsi /
                              (_ps.Count(t => t.novel_type == "1" && Convert.ToInt32(t.general_all_no) > 10) + _ps.Count(t => t.novel_type == "2" || (t.novel_type == "1" && Convert.ToInt32(t.general_all_no) < 11)) / 3);
            }

            _ps = null;//動作効率のためコレクション解放
        }

        /// <summary>
        /// 作者コード
        /// </summary>
        public string userid { get; private set; } = "";

        /// <summary>
        /// その作者のうち、最も売れた(ポイントが多い)タイトルが今表示しているタイトルと同じ場合はtrue
        /// 後でセットする
        /// </summary>
        public bool mostflg { get; set; } = false;

        /// <summary>
        /// その作者のうち、最も売れた(ポイントが多い)タイトル
        /// </summary>
        public string most_valuable_title { get; private set; } = "";

        /// <summary>
        /// その作者のうち、最も売れた(ポイントが多い)タイトルのポイント
        /// </summary>
        public int most_point { get; private set; } = 0;

        /// <summary>
        /// その作者のうち、最も売れたタイトルのコードとURL
        /// </summary>
        private string _most_valuable_ncode = "";
        public string most_valuable_url
        {
            get
            {
                return ClsCommon.NOVELURL + this._most_valuable_ncode + "/";
            }
            private set
            {
                _most_valuable_ncode = value;
            }
        }

        /// <summary>
        /// その作者のエタ率(短編は除く)
        /// </summary>
        public double eternal_per { get; private set; } = 0;
    }
}
