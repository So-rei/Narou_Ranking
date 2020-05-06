
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static ClsCommon.ClsCommon;
using ClsCommon;
using WebApp.Models;
using System.Web.UI.DataVisualization.Charting;
using System.Threading.Tasks;

namespace WebApp.Controllers
{
    public partial class HomeController : Controller
    {

        public async Task<ActionResult> Index()
        {
            //チャートクリア
            ChartModels.ChartFile = new Dictionary<int, FileContentResult>();

            //データ取得
            var msp = TestCalc();

            //ビューとチャートにデータを設定する
            var t1 = SetChartData(msp);
            var t2 = SetViewData(msp);

            await Task.WhenAll(t1, t2);

            return View("Index");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult RenderChartData(int irank)
        {
            if (ChartModels.ChartFile != null && ChartModels.ChartFile.ContainsKey(irank)) { return ChartModels.ChartFile[irank]; }
            return null;
        }
    }




    public partial class HomeController : Controller
    {
        /// <summary>
        /// 取得結果コレクションをViewにセットする
        /// </summary>
        /// <param name="_SetParams"></param>
        public Task SetViewData(IReadOnlyList<ModelSetParams> _ModelSetParams)
        {
            //各詳細
            var tasks = new List<Task>(); // TaskをまとめるListを作成

            int i = 0;
            foreach (var _msp in _ModelSetParams)
            {
                if (i == 0)
                {
                    //全件数
                    ViewData["allcount"] = _msp.ps.allcount;
                    //表示件数
                    if (Convert.ToInt32(_msp.ps.allcount) < MAXTITLE)
                    {
                        ViewData["viewcount"] = Convert.ToInt32(_msp.ps.allcount)+1;
                    }
                    else
                    {
                        ViewData["viewcount"] = MAXTITLE;
                    }
                    i++;
                    continue;
                }

                var task = Task.Run(() =>
                {
                    //jsonからそのままの項目-----------------------------------------------------------
                    ViewData["title" + i.ToString()] = _msp.ps.title;
                    ViewData["ncode" + i.ToString()] = _msp.ps.ncode;
                    ViewData["userid" + i.ToString()] = _msp.ps.userid;
                    ViewData["writer" + i.ToString()] = _msp.ps.writer;
                    ViewData["story" + i.ToString()] = _msp.ps.story;
                    //ViewData["gensaku" + i.ToString()] = _sp.gensaku;
                    ViewData["keyword" + i.ToString()] = _msp.ps.keyword;
                    ViewData["general_firstup" + i.ToString()] = _msp.ps.general_firstup;
                    ViewData["general_lastup" + i.ToString()] = _msp.ps.general_lastup;
                    ViewData["noveltype" + i.ToString()] = _msp.ps.noveltype;
                    ViewData["length" + i.ToString()] = _msp.ps.length;
                    //ViewData["time" + i.ToString()] = _sp.time;
                    //ViewData["pc_or_k" + i.ToString()] = _sp.pc_or_k;
                    ViewData["global_point" + i.ToString()] = _msp.ps.global_point;
                    ViewData["daily_point" + i.ToString()] = _msp.ps.daily_point;
                    ViewData["weekly_point" + i.ToString()] = _msp.ps.weekly_point;
                    ViewData["monthly_point" + i.ToString()] = _msp.ps.monthly_point;
                    ViewData["quarter_point" + i.ToString()] = _msp.ps.quarter_point;
                    ViewData["yearly_point" + i.ToString()] = _msp.ps.yearly_point;
                    ViewData["fav_novel_cnt" + i.ToString()] = _msp.ps.fav_novel_cnt;
                    ViewData["impression_cnt" + i.ToString()] = _msp.ps.impression_cnt;
                    ViewData["review_cnt" + i.ToString()] = _msp.ps.review_cnt;
                    ViewData["all_point" + i.ToString()] = _msp.ps.all_point;
                    ViewData["all_hyoka_cnt" + i.ToString()] = _msp.ps.all_hyoka_cnt;
                    ViewData["sasie_cnt" + i.ToString()] = _msp.ps.sasie_cnt;
                    ViewData["kaiwaritu" + i.ToString()] = _msp.ps.kaiwaritu;
                    //ViewData["novelupdated_at" + i.ToString()] = _msp.ps.novelupdated_at;
                    //ViewData["updated_at" + i.ToString()] = _sp.updated_at;

                    //計算処理を入れた項目--------------------------------------------------------------
                    ViewData["_biggenre" + i.ToString()] = _msp._biggenre;
                    ViewData["_genre" + i.ToString()] = _msp._genre;
                    ViewData["_end" + i.ToString()] = _msp._end;
                    ViewData["_is" + i.ToString()] = _msp._is;
                    ViewData["_isstop" + i.ToString()] = _msp._isstop;
                    ViewData["_isr15" + i.ToString()] = _msp._isr15;
                    ViewData["_isbl" + i.ToString()] = _msp._isbl;
                    ViewData["_isgl" + i.ToString()] = _msp._isgl;
                    ViewData["_iszankoku" + i.ToString()] = _msp._iszankoku;
                    ViewData["_istensei" + i.ToString()] = _msp._istensei;
                    ViewData["_istenni" + i.ToString()] = _msp._istenni;
                    ViewData["_issasie" + i.ToString()] = _msp._issasie;
                    ViewData["_isbook" + i.ToString()] = _msp._isbook;
                    ViewData["_iscomic" + i.ToString()] = _msp._iscomic;
                    ViewData["_url" + i.ToString()] = _msp._url;

                    //作者詳細項目----------------------------------------------------------------------
                    ViewData["mostflg" + i.ToString()] = _msp.UD.mostflg;
                    ViewData["most_valuable_title" + i.ToString()] = _msp.UD.most_valuable_title;
                    ViewData["most_valuable_url" + i.ToString()] = _msp.UD.most_valuable_url;
                    ViewData["most_point" + i.ToString()] = _msp.UD.most_point;
                    ViewData["eternal_per" + i.ToString()] = _msp.UD.eternal_per;

                    i++;
                });
                tasks.Add(task); // を、Listにまとめる
            }

            return Task.WhenAll(tasks);
        }

        public async Task<bool> SetChartData(List<ModelSetParams> _ModelSetParams)
        {
            var Ret = new Dictionary<int, FileContentResult>();
            await Task.Run(() =>
            {
                object lockToken = new object();

                int num = _ModelSetParams.Count();
                Parallel.For(1, num, i =>
                {
                    var cone = Chart(_ModelSetParams[i]);
                    lock (lockToken)
                    {
                        Ret.Add(i, cone);
                    }
                });
                ChartModels.ChartFile = Ret;
            });
            return true;
        }

        public FileContentResult Chart(ModelSetParams _msp)
        {
            //設定する値を計算
            //ポイント数 ...10^2=20点,10^3=40点,10^4=70点,10^5=100点
            int i1 = Convert.ToInt32(_msp.ps.global_point);
            double p1;
            if (i1 < Math.Pow(10, 2))
            {
                p1 = i1 / 5;
            }
            else if (i1 < Math.Pow(10, 3))
            {
                p1 = 20 + 20 * (Math.Log10(i1) - 2);
            }
            else if (i1 < Math.Pow(10, 4))
            {
                p1 = 40 + 30 * (Math.Log10(i1) - 3);
            }
            else
            {
                p1 = 70 + 30 * (Math.Log10(i1) - 4);
            }

            //ポイント信頼性
            //１．ポイントのうちブックマークが占める割合が中心(65%)より低い or 80%より多いとマイナス
            //２．評価ポイント／評価数　が10に近すぎる(9.1↑) or 低すぎる(8.5↓)とマイナス　中心8.8
            double p2 = 100;
            int i2_1 = 100 * (Convert.ToInt32(_msp.ps.fav_novel_cnt) * 2 / Convert.ToInt32(_msp.ps.global_point));
            double i2_2 = (Convert.ToInt32(_msp.ps.all_point) / Convert.ToInt32(_msp.ps.all_hyoka_cnt));
            if (i2_1 < 65) { p2 -= i2_1 * 4; }
            if (i2_2 > 9.1) { p2 -= Math.Pow(2, 10 * (i2_2 - 9.1)); }
            if (i2_2 < 8.8) { p2 -= 2 * 10 * (8.8 - i2_2); }

            //感想数+レビュー数
            //感想：10^2=20点,10^3=40点,10^4=70点,10^5=100点
            //＋総合評価/100よりも感想が多いと、隠れた名作ということで傾斜配点をつける・・・2倍多かったら上記点数×1.3,10倍で1.9倍
            //レビュー：１レビュー+0.5点(20点まで)
            double p3;
            var i3_1 = Convert.ToInt32(_msp.ps.impression_cnt);
            if (i3_1 < Math.Pow(10, 2))
            {
                p3 = i3_1 / 5;
            }
            else if (i3_1 < Math.Pow(10, 3))
            {
                p3 = 20 + 20 * (Math.Log10(i3_1) - 2);
            }
            else if (i1 < Math.Pow(10, 4))
            {
                p3 = 40 + 30 * (Math.Log10(i3_1) - 3);
            }
            else
            {
                p3 = 70 + 30 * (Math.Log10(i3_1) - 4);
            }
            double i3_2 = i3_1 / (i1 / 100);
            if (i3_2 > 1) { p3 *= 1 + 0.3 * Math.Pow(i3_2 - 1,0.5); }
            if (Convert.ToInt32(_msp.ps.review_cnt) * 0.5 < 20)
            {
                p3 += Convert.ToInt32(_msp.ps.review_cnt) * 0.5;
            } 
            else
            {
                p3 += 20;
            }

            //更新速度
            //完結は100
            //連載中は以下
            //１）文字数 / (連載開始日付～最終更新日)　={1日あたりの更新文字数} が100文字=20点,1000文字=80点,2000文字～100点
            //２）最終更新日～現在日付の差がx週間あくごとに-x^3点[x≦4] y週間あくごとに-60-y点[y≧4]
            double p4;
            double i4 = Convert.ToInt32(_msp.ps.length) / (Convert.ToDateTime(_msp.ps.novelupdated_at) - Convert.ToDateTime(_msp.ps.general_firstup)).Days;
            if (_msp.ps.end == "2")
            {
                p4 = 100;
            }
            else
            {
                if (i4 < Math.Pow(10, 2))
                {
                    p4 = i4 / 5;
                }
                else if (i4 < Math.Pow(10, 3))
                {
                    p4 = 20 + 60 * (Math.Log10(i4) - 2);
                }
                else if (i4 < 2000)
                {
                    p4 = 80 + 20 * (Math.Log10(i4 / 2) - 2);
                }
                else
                {
                    p4 = 100;
                }
                double i4_2 = (DateTime.Now - Convert.ToDateTime(_msp.ps.novelupdated_at)).Days / 7;
                if (i4_2 < 4)
                {
                    p4 -= Math.Pow(3, i4_2);
                }
                else
                {
                    p4 -= (60 + i4_2);
                }
            }

            //未使用項目1
            double p5 = 100;

            // レーダーチャートを作成
            var chart = new Chart
            {
                Height = 300,
                Width = 300,
                ImageType = ChartImageType.Png,
                ChartAreas =
                {
                    new ChartArea
                    {
                        Name = "Default"
                    }
                },
                Series =
                {
                    new Series
                    {
                        Name = "サンプル1",
                        ChartType = SeriesChartType.Radar,
                        Points =
                        {
                            new DataPoint
                            {
                                AxisLabel = "ポイント数",
                                YValues = new double[] { p1 }
                            },
                            new DataPoint
                            {
                                AxisLabel = "ポイント信頼性",
                                YValues = new double[] { p2 }
                            },
                            new DataPoint
                            {
                                AxisLabel = "感想/レビュー数",
                                YValues = new double[] { p3 }
                            },
                            new DataPoint
                            {
                                AxisLabel = "更新速度",
                                YValues = new double[] { p4 }
                            },
                            new DataPoint
                            {
                                AxisLabel = "未使用項目1",
                                YValues = new double[] { p5 }
                            }
                        }
                    }
                }
            };
            //chart.AlignDataPointsByAxisLabel("D,C,B,A,AA,S");

            using (var stream = new System.IO.MemoryStream())
            {
                chart.SaveImage(stream);

                return File(stream.ToArray(), "image/png");
            }
        }
    }
}