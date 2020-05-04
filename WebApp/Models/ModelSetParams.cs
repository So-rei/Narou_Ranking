using ClsCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace WebApp.Models
{
    public class ModelSetParams
    {
        Params_Set ps;
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
        public string _is
        {
            get
            {
                var s = "";
                s += (ps.isstop == "1" ? "　【長期連載停止中" : "");
                s += (ps.isr15 == "1" ? "　【R15】" : "");
                s += (ps.isbl == "1" ? "　【ボーイズラブ】" : "");
                s += (ps.isgl == "1" ? "　【ガールズラブ】" : "");
                s += (ps.iszankoku == "1" ? "　【残酷な描写あり】" : "");
                s += (ps.istensei == "1" ? "　【異世界転生】" : "");
                s += (ps.istenni == "1" ? "　【異世界転移】" : "");
                s += (Convert.ToInt32(ps.sasie_cnt) > 0 ? "　【挿絵有】" : "");
                return s;
            }
        }
        [JsonIgnore]
        public string _url
        {
            get
            {
                return GetMain.NOVELURL + ps.ncode + "/";
            }
        }
    }
}