using System;
using System.Collections.Generic;
using System.Linq;
using static ClsCommon.Params_Common;
using static ClsCommon.Params_Get;

namespace ClsCommon
{
    public class ClsCommon
    {
        //テスト用の開始コマンド
        static void Main(string[] args)
        {
            var Ret = TestCalc0();
            Console.WriteLine(Ret);
            Console.ReadLine();
        }
        public static string TestCalc0()
        {
            //テスト情報を作成
            var GetUri1 = new GetUri();
            GetUri1.Add(Joken.Ggenre, ((int)EGenre.ハイファンタジー).ToString());//ジャンル指定
            GetUri1.Add(Joken.Gorder, EOrder.Ghyoka.ToString());//総合ポイントの高い順 ※文字列

            //処理開始
            var REFPARAMS = GetMain.GetStart(GetUri1);

            //テスト情報１について・・・
            var Ret1 = REFPARAMS[0].allcount;//全タイトル数
            var Ret2 = REFPARAMS[1].title;//１つ目のタイトルのタイトル
            var Ret3 = REFPARAMS[1].story;//１つ目のタイトルのタイトル

            return Ret1 + "/" + Ret2 + "/" + Ret3;
        }

        //テスト用２
        public static List<ModelSetParams> TestCalc()
        {
            //テスト情報サンプルを作成
            var GetUri1 = new GetUri();
            GetUri1.Add(Joken.Ggenre, ((int)EGenre.ハイファンタジー).ToString());//ジャンル指定
            //GetUri1.Add(Joken.Gorder, EOrder.Ghyoka.ToString());//総合ポイントの高い順 ※文字列
            GetUri1.Add(Joken.Gorder, EOrder.Gweekly.ToString());//週間ユニークユーザの多い順 ※文字列
            GetUri1.Add(Joken.Gorder, EOrder.Glengthasc.ToString());
            GetUri1.Add(Joken.Guserid, "172188");//作者のユーザーID

            //処理開始
            var REFPARAMS1 = GetMain.GetStart(GetUri1);
            List<ModelSetParams> Msp = REFPARAMS1.Select(t => new ModelSetParams(t)).ToList();

            //作者処理開始
            int endrange = GetMain.MAXTITLE;
            if(REFPARAMS1.Count() < GetMain.MAXTITLE) endrange = REFPARAMS1.Count();
            UserParamsSet(ref Msp, 1, endrange);
            return Msp;
        }

        /// <summary>
        /// 作者処理
        /// 必要なときに必要な量リクエストするようにしないとパンクする　＆　うまく並列でやらないと遅い　ため、このような構造に
        /// 取ってきた項目の作者を見て、別のリストを再取得
        /// </summary>
        /// <param name="_refMsp">ref 作者処理をするパラメータコレクション  </param>
        /// <param name="startno">処理をする範囲開始 デフォルト1</param>
        /// <param name="endno">処理をする範囲終了 デフォルトmax</param>
        public static void UserParamsSet(ref List<ModelSetParams> _refMsp, int startno = 1, int endno = GetMain.MAXTITLE)
        {
            if (endno - startno < 1) { return; }//範囲チェック

            var _GetUri = new List<GetUri>();
            for (var i = startno; i < endno; i++)
            {
                var _GU = new GetUri();
                _GU.Add(Joken.Guserid, _refMsp[i].ps.userid);//作者のユーザーID
                _GU.Add(Joken.Gorder, EOrder.Ghyoka.ToString());//総合ポイントの高い順 ※文字列
                _GetUri.Add(_GU);
            }

            //範囲開始-範囲終了までのリクエストを発行　複数リクエスト,並列であることに注意
            var ulist = GetMain.GetUser(_GetUri);
            for (var i = startno; i < endno; i++)
            {
                _refMsp[i].UD = ulist[i - startno];
                if (_refMsp[i]._url == ulist[i - startno].most_valuable_url)
                {
                    _refMsp[i].UD.mostflg = true;//他の代表作がない場合
                }
            }

            return;
        }
    }
}
