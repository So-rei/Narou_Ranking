using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static ClsCommon.Params_Get;

namespace ClsCommon
{
    /// <summary>
    /// Getリクエスト条件
    /// 詳細は https://dev.syosetu.com/man/api/
    /// </summary>
    public class GetUri
    {
        //Getのための検索条件を蓄える
        private Dictionary<Joken, string> JokenList = new Dictionary<Joken, string>();
        //Getしたい項目一覧を蓄える
        private string _GetParamList;
        public string GetParamList
        {
            get { return this._GetParamList == "" ? "" : "of=" + this._GetParamList; }
            set { this._GetParamList = value; }
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public GetUri()
        {
            //初期設定
            Add(Joken.Ggzip, "5");//圧縮率max
            Add(Joken.Gout, "json");//json形式
            Add(Joken.Glim, "5");//500作品まで出力
            GetParamList = "";//複数のときは-でつなげる
        }

        /// <summary>
        /// Getコマンド追加
        /// </summary>
        /// <param name="_ptn"></param>
        /// <param name="param"></param>
        public void Add(Joken _ptn, string param)
        {
            JokenList.Remove(_ptn);//重複してたら消す
            JokenList.Add(_ptn, param);
        }

        /// <summary>
        /// Getコマンド吐き出す
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            //項目を1つの文字列として出力　G削除、=,&セットが必要
            StringBuilder sRet = new StringBuilder("");
            JokenList.ToList().ForEach(s => sRet.Append(s.Key.ToString().TrimStart('G') + "=" + s.Value + "&"));
            sRet.Append(GetParamList);
            return sRet.ToString().TrimEnd('&');
        }
    }
}
