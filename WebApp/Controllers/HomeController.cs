
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static ClsCommon.ClsCommon;
using ClsCommon;
using WebApp.Models;

namespace WebApp.Controllers
{
    public partial class HomeController : Controller
    {
        public ActionResult Index()
        {
            //
            var ret = TestCalc();
            SetViewData(ret);
            //

            return View();
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
    }




    public partial class HomeController : Controller
    {
        /// <summary>
        /// 取得結果コレクションをViewにセットする
        /// </summary>
        /// <param name="_SetParams"></param>
        public void SetViewData(IReadOnlyList<ModelSetParams> _ModelSetParams)
        {
            //各詳細
            int i = 0;
            foreach (var _msp in _ModelSetParams)
            {
                if (i == 0)
                {
                    //全件数
                    ViewData["allcount"] = _msp.ps.allcount;
                    i++;
                    continue;
                }

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
            }
        }
    }
}