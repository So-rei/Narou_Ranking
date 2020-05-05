using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Threading.Tasks;
using Newtonsoft.Json;
using static ClsCommon.Params_Get;
using static ClsCommon.Params_Set;

namespace ClsCommon
{
    public class GetMain
    {

        /// <summary>
        /// 取得開始[1リクエスト]
        /// </summary>
        /// <param name="_Uri"></param>
        public static IReadOnlyList<Params_Set> GetStart(GetUri _GetUri)
        {
            var retone = ReqqGetOne(_GetUri.ToString());
            return retone;
        }

        /// <summary>
        /// 取得開始[複数リクエスト]　※作者別結果用の並列処理
        /// </summary>
        /// <param name="_Uri"></param>
        public static Dictionary<int,UserDetail> GetUser(IReadOnlyList<GetUri> _GetUri)
        {
            var ret = new Dictionary<int, UserDetail>();
            var num = _GetUri.Count();

            object lockToken = new object();

            Parallel.For(0, num, i =>
                 {
                     var retone = ReqqGetOne(_GetUri[i].ToString());
                     UserDetail uone = new UserDetail(retone);

                     lock (lockToken)
                     {
                         ret.Add(i,uone);
                     }
                 });
            return ret;
        }

        /// <summary>
        /// httpsに対して取得GETリクエスト送信、受信、受信内容をgzip解凍、jsonデシリアライズして結果リストを返す
        /// </summary>
        /// <param name="sUri"></param>
        /// <returns></returns>
        private static IReadOnlyList<Params_Set> ReqqGetOne(string sUri)
        {
            using (var wc = new WebClient())
            {
                // SSL/TLSに対応
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                ServicePointManager.ServerCertificateValidationCallback =
                    new RemoteCertificateValidationCallback(delegate { return true; });

                // GETパラメータの設定
                var ps = new System.Collections.Specialized.NameValueCollection();
                ps.Add("キー", "バリュー");
                wc.QueryString = ps;

                // ヘッダーの設定（Chromeのふりをする設定）
                wc.Headers.Add(HttpRequestHeader.UserAgent, "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/63.0.3239.132 Safari/537.36");

                // 通信・取得
                var resData = wc.OpenRead(ClsCommon.APIURL + sUri);

                //圧縮しないのならコレで良い
                //using (var sr = new StreamReader(resData, Encoding.GetEncoding("UTF-8")))
                //{
                //    var html = sr.ReadToEnd();
                //}

                //圧縮するので解除モードのGZipStreamで取得
                var ret = new List<byte>();
                using (var gzipStrm = new GZipStream(resData, CompressionMode.Decompress))
                {
                    byte[] buffer = new byte[1024];
                    while (true)
                    {
                        //書庫から展開されたデータを読み込む
                        int readSize = gzipStrm.Read(buffer, 0, buffer.Length);
                        //最後まで読み込んだ時は、ループを抜ける
                        if (readSize == 0)
                            break;
                        //展開先のファイルに書き込む
                        ret.AddRange(buffer);
                    }
                }

                var s = System.Text.Encoding.GetEncoding("UTF-8").GetString(ret.ToArray());

                //jsonデシリアライズ 1ヶ目には検索件数が入っている
                var dOutParam = Deserialize(new List<Params_Set>().AsEnumerable(), s);

                //なぜか最後の]より後ろにまだ続きがある場合がある？文字列の異常なので末尾を削除してリトライ
                if (dOutParam==null)
                {
                    var _s = s.Remove(s.LastIndexOf(']'))+ "]";
                    dOutParam = Deserialize(new List<Params_Set>().AsEnumerable(), _s);
                }
                return (IReadOnlyList<Params_Set>)dOutParam;
            }
        }

        /// <summary>
        /// json文字列→デシリアライズ→List<T>に返却
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="jMaster"></param>
        private static IEnumerable<T> Deserialize<T>(IEnumerable<T> T1, string text) where T : class
        {
            try
            {
                return JsonConvert.DeserializeObject<IEnumerable<T>>(text);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
