using ClsCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace WebApp.Models
{
    public static class ChartModels
    {
        /// <summary>
        /// チャート作成が重いので、並列処理で作成して記憶しておく
        /// </summary>
        public static Dictionary<int, FileContentResult> ChartFile;
    }
}