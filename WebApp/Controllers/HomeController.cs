
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
        //Dictionary<int, IReadOnlyList<Params_Set>>
        /// <summary>
        /// 取得結果コレクションをViewにセットする
        /// </summary>
        /// <param name="_SetParams"></param>
        public void SetViewData(IReadOnlyList<Params_Set> _SetParams)
        {
            //各詳細
            int i = 0;
            foreach (var _sp in _SetParams)
            {
                if (i == 0)
                {
                    //全件数
                    ViewData["allcount"] = _sp.allcount;
                    i++;
                    continue;
                }

                ModelSetParams msp = new ModelSetParams(_sp);

                ViewData["title" + i.ToString()] = _sp.title;
                ViewData["ncode" + i.ToString()] = _sp.ncode;
                ViewData["userid" + i.ToString()] = _sp.userid;
                ViewData["writer" + i.ToString()] = _sp.writer;
                ViewData["story" + i.ToString()] = _sp.story;
                //ViewData["gensaku" + i.ToString()] = _sp.gensaku;
                ViewData["keyword" + i.ToString()] = _sp.keyword;
                ViewData["general_firstup" + i.ToString()] = _sp.general_firstup;
                ViewData["general_lastup" + i.ToString()] = _sp.general_lastup;
                ViewData["noveltype" + i.ToString()] = _sp.noveltype;
                ViewData["length" + i.ToString()] = _sp.length;
                //ViewData["time" + i.ToString()] = _sp.time;
                //ViewData["pc_or_k" + i.ToString()] = _sp.pc_or_k;
                ViewData["global_point" + i.ToString()] = _sp.global_point;
                ViewData["daily_point" + i.ToString()] = _sp.daily_point;
                ViewData["weekly_point" + i.ToString()] = _sp.weekly_point;
                ViewData["monthly_point" + i.ToString()] = _sp.monthly_point;
                ViewData["quarter_point" + i.ToString()] = _sp.quarter_point;
                ViewData["yearly_point" + i.ToString()] = _sp.yearly_point;
                ViewData["fav_novel_cnt" + i.ToString()] = _sp.fav_novel_cnt;
                ViewData["impression_cnt" + i.ToString()] = _sp.impression_cnt;
                ViewData["review_cnt" + i.ToString()] = _sp.review_cnt;
                ViewData["all_point" + i.ToString()] = _sp.all_point;
                ViewData["all_hyoka_cnt" + i.ToString()] = _sp.all_hyoka_cnt;
                ViewData["sasie_cnt" + i.ToString()] = _sp.sasie_cnt;
                ViewData["kaiwaritu" + i.ToString()] = _sp.kaiwaritu;
                ViewData["novelupdated_at" + i.ToString()] = _sp.novelupdated_at;
                //ViewData["updated_at" + i.ToString()] = _sp.updated_at;

                ViewData["_biggenre" + i.ToString()] = msp._biggenre;
                ViewData["_genre" + i.ToString()] = msp._genre;
                ViewData["_end" + i.ToString()] = msp._end;
                ViewData["_is" + i.ToString()] = msp._is;
                ViewData["_url" + i.ToString()] = msp._url;

                i++;
            }
        }
    }
}